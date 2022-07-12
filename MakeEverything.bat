set NO_PAUSE=1
call BuildLinux64Headless.bat
call UploadLinux64HeadlessRSync.bat
call RestartLinuxServer.bat
call BuildWebGL.bat
call UploadWebGLRsync.bat
call BuildWin64.bat
pause