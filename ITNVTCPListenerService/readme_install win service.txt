run cmd as administrator

sc create ITNVHTTPListenerService binPath= "C:\APPS\ITNVHTTPListener\ITNVHTTPListenerService.exe"

PAY ATTENSION : after binPath= MUST BE space
run this command from administrator cmd

sc description ITNVHTTPListenerService "Service gets url parameters and sends them to AxtiveX server"

sc delete ITNVHTTPListenerService 