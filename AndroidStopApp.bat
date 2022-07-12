call app_info_setup.bat
echo Stopping app
adb shell am force-stop %APP_PACKAGE_NAME%
pause