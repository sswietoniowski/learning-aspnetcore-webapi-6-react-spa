Remove-Item -Path ..\Spa.Backend\wwwroot\static -Force -Recurse
npm run build
xcopy build\ ..\Spa.Backend\wwwroot\ /s /y /q