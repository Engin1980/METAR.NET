echo off

echo Cleaning previous build
del *.dll /q
del *.pdb /q
del *.xml /q
del *.chm /q

echo Inserting ESystem dependece
copy ..\..\MetarDecoder\bin\Debug\ESystem.* .\

echo Inserting Metar Decoder project
copy ..\..\MetarDecoder\bin\Debug\ENG.* .\
copy ..\..\MetarDecoder\Documentation\Help\MetarDecoder.chm .\

echo Inserting Metar Downloader project
copy ..\..\MetarDownloader\bin\Debug\ENG.* .\
copy ..\..\MetarDownloader\Documentation\Help\MetarDownloader.chm .\

echo Release created
Pause