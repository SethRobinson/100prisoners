:This builds the win version in a temp dir, but then copies back the final data to the real dir (build/android dir)
SET BUILDMODE=RELEASE

:Setting no pause causes our .bat files to not do pause commands when they are done
set NO_PAUSE=1
:First, let's customize the directory name we're going to close to, by adding a postfix to make it unique
SET CLONE_DIR_POSTFIX=Android
:Now let's actually make it, we'll pass in the postfix as a parm
call CloneToTempDir.bat %CLONE_DIR_POSTFIX%

:Move to build dir
cd ../%APP_NAME%Temp%CLONE_DIR_POSTFIX%
:Do the actual build
call BuildAndroid.bat
:destroy any old windows builds in our main dir
rmdir ..\%APP_NAME%\build\android /S /Q
:recreate the dir
mkdir ..\%APP_NAME%\build\android
:copy the final build back to our main dir
xcopy build\android ..\%APP_NAME%\build\android /E /F /Y

:Move back out of it
cd ..
:Delete the temp dir we were just using
rmdir %APP_NAME%Temp%CLONE_DIR_POSTFIX% /S /Q
:move back into the main dir
cd %APP_NAME%
call AndroidInstallToDeviceAndRun.bat
if "%NO_PAUSE2%"=="" pause

