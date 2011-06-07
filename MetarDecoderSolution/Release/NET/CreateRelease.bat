echo off

echo Cleaning previous build
del *.dll /q
del *.pdb /q
del *.xml /q
del *.chm /q

echo Inserting -tutorial- project content
copy "..\..\Tutorial\bin\Debug\*.*" .\

echo Inserting documentation
copy "..\..\ENG.WMOCodes.Documentation\Help\Documentation.chm .\

echo Release created
Pause

rem PS C:\Documents and Settings\Marek Vajgl> copy -path "C:\Documents and Settings\
rem Marek Vajgl\Dokumenty\Visual Studio 2010\Projects\MetarDecoderSolution\Tutorial"
rem  -destination "C:\Documents and Settings\Marek Vajgl\Dokumenty\Visual Studio 201
rem 0\Projects\MetarDecoderSolution\Release\NET" -recurse