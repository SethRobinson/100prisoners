call BuildWin64InClonedDir.bat
start build\win\RTUnityTemplate.exe -logFile log1.txt -server localhost -runfullserver
start build\win\RTUnityTemplate.exe -logFile log2.txt -server localhost
start build\win\RTUnityTemplate.exe -logFile log3.txt -server localhost
