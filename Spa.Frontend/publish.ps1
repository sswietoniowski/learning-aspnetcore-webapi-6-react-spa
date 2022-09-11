Remove-Item -Path ..\Spa.ReactHost\wwwroot\static -Force -Recurse
npm run build
xcopy build\ ..\Spa.ReactHost\wwwroot\ /s /y /q