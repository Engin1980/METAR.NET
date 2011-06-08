$solutionPath = "C:\Documents and Settings\Marek Vajgl\Dokumenty\Visual Studio 2010\Projects\MetarDecoderSolution\"
$silverlightPath = "_SVL\"
$tempPath = "C:\TEMP\MetarDecoderSolution\"
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
}

function Process-Projects {
  param ($folder)
  Copy-ESystem_Extensions $folder"ENG.WMOCodes\"$debugPath  
  Get-ChildItem $folder ENG* | ForEach-Object { Copy-Output $folder $_.Name }
}

Write-Host === Preparation ===
if (Test-Path $tempPath -PathType container) {
  Write-Host   ... deleting old content
  Remove-Item $tempPath* -Force -Recurse 
} else {
  Write-Host   ... creating temp folder
  $__pom = New-Item $tempPath -ItemType directory }    


Write-Host === Main copy - Silverlight ===
Write-Host   ... libraries
$source = $solutionPath + $silverlightPath
Process-Projects $source


Write-Host   ... packing
sl $tempPath
zip -9 -q -r METAR_SVL.zip *

Write-Host   ... copy result zip file
$source = $tempPath + "METAR_SVL.zip"
$target = $solutionPath + $releasePath
Copy-Item $source -Destination $target -Force

Write-Host ... done **

