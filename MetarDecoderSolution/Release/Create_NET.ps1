
$solutionPath = "C:\Documents and Settings\Marek Vajgl\Dokumenty\Visual Studio 2010\Projects\MetarDecoderSolution\"
$tempPath = "C:\TEMP\MetarDecoderSolution\"
$tutorialPath = "Tutorial\"
$releasePath = "Release\"
$debugPath = "Bin\Debug\"

function Copy-ESystem_Extensions {
  param ($sourceFolder)
  Write-Host    ... prerequisity ESystem.Extensions
  Get-ChildItem $sourceFolder "ESystem*" | Copy-Item -Destination $tempPath  
}

function Copy-Output {
  param ($folder, $projectName)
  Write-Host     ... project $folder "==" $projectName
  $source = $folder + $projectName + "\" + $debugPath
  Get-ChildItem $source $projectName* | Copy-Item -Destination $tempPath  
  Get-ChildItem $source | ForEach-Object  { if ($_.Attributes -eq "Directory") { 
    Set-Location $source
    Copy-Item $_ -Destination $tempPath -Recurse -Force } }
}

function Copy-Tutorial {
  param ($folder)
  Write-Host    ... adding tutorial project
  $source = $solutionPath + $tutorialPath
  $target = $tempPath
  Copy-Item $source -Force -Recurse -Destination $target
  Get-ChildItem $target -Include ".svn" -Force -Recurse | Remove-Item -Force -Recurse
}
  

function Process-Projects {
  param ($folder)
  Copy-ESystem_Extensions $folder"ENG.WMOCodes\"$debugPath  
  Get-ChildItem $folder ENG* | ForEach-Object { Copy-Output $folder $_.Name }
  Copy-Tutorial $folder
}

Write-Host === Preparation ===
if (Test-Path $tempPath -PathType container) {
  Write-Host   ... deleting old content
  Remove-Item $tempPath* -Force -Recurse 
} else {
  Write-Host   ... creating temp folder
  $__pom = New-Item $tempPath -ItemType directory }    


Write-Host === Main copy - _NET ===
Write-Host   ... libraries
$source = $solutionPath
Process-Projects $source



Write-Host   ... packing
sl $tempPath
zip -9 -q -r METAR_NET.zip *

Write-Host   ... copy result zip file
$source = $tempPath + "METAR_NET.zip"
$target = $solutionPath + $releasePath
Copy-Item $source -Destination $target -Force

Write-Host ... done **

