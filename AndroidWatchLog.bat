call app_info_setup.bat
adb devices
adb logcat *:S Unity:D |findstr "RTLOG"
pause