Remove-Item -Path ..\Spa.Backend\wwwroot\static -Force -Recurse
npm run build
xcopy build\ ..\Api\wwwroot\ /s /y /q