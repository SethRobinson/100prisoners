:Setting no pause causes our .bat files to not do pause commands when they are done
set NO_PAUSE=1
:First, let's customize the directory name we're going to close to, by adding a postfix to make it unique
SET CLONE_DIR_POSTFIX=Linux64
:Now let's actually make it, we'll pass in the postfix as a parm
call CloneToTempDir.bat %CLONE_DIR_POSTFIX%

:Move to build dir
cd ../%APP_NAME%Temp%CLONE_DIR_POSTFIX%
:Do the actual build
call BuildLinux64Headless.bat
call UploadLinux64HeadlessRSync.bat
call RestartLinuxServer.bat
:Move back out of it
cd ..
:Delete the temp dir we were just using
rmdir %APP_NAME%Temp%CLONE_DIR_POSTFIX% /S /Q
pause
exit
