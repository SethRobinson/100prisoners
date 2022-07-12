call app_info_setup.bat
echo Installing...
adb install -r build/android/%APP_NAME%.apk
adb shell monkey -p %APP_PACKAGE_NAME% -c android.intent.category.INFO 1
pause