set NO_PAUSE=1
set BUILD_MODE=Developer
call BuildLinux64Headless.bat
call UploadLinux64HeadlessRSync.bat
call RestartLinuxServer.bat
pause