run cmd as administrator

sc create ITNVHTTPListenerService binPath= "C:\Apps\ITNVHTTPListenerService\ITNVHTTPListenerService.exe"

PAY ATTENSION : after binPath= MUST BE space
run this command from administrator cmd

sc description ITNVHTTPListenerService "ITNVHTTPListenerService"

sc delete ITNVHTTPListenerService