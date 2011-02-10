echo off

echo Cleaning previous build
del *.dll /q
del *.pdb /q
del *.xml /q
del *.chm /q

echo Inserting ESystem dependece
copy ..\..\MetarDecoderSVL\bin\Debug\ESystem.* .\

echo Inserting Metar Decoder project
copy ..\..\MetarDecoderSVL\bin\Debug\ENG.* .\
REM copy ..\..\MetarDecoderSVL\Documentation\Help\MetarDecoder.chm .\

echo Inserting Metar Downloader project
copy ..\..\MetarDownloaderSVL\bin\Debug\ENG.* .\
REM copy ..\..\MetarDownloaderSVL\Documentation\Help\MetarDownloader.chm .\

echo Release created
echo Documentation for Silverlight not included !
Pause