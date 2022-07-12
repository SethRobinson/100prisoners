call app_info_setup.bat

SET FILENAME=%APP_NAME%

:Actually do the unity build
rmdir build\android /S /Q
mkdir build\android
call GenerateBuildDate.bat
echo Building project...

:So let's delete these things from the shared stuff that we'd never use in a client webgl build
del /Q Assets\RT\MySQL\RTSqlManager.cs
del /Q Assets\RT\RTNetworkServer.cs

call ../timecmd.bat %UNITY_EXE% -quit -batchmode -logFile log.txt -executeMethod AndroidBuilder.BuildRelease -projectPath %cd%
echo Finished building.


if not exist build/android/%APP_NAME%.apk (
echo Error with build!
start notepad.exe log.txt
%PROTON_DIR%\shared\win\utils\beeper.exe /p
pause
)


if "%NO_PAUSE%"=="" pause
