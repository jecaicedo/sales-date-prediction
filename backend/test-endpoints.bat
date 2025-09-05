@echo off
echo ========================================
echo   Verificando Endpoints de la API
echo ========================================
echo.

echo Verificando que la API esté ejecutándose...
timeout /t 2 /nobreak >nul

echo.
echo Probando endpoint de clientes...
curl -s http://localhost:8080/api/customers >nul
if %errorlevel% equ 0 (
    echo ✅ GET /api/customers - OK
) else (
    echo ❌ GET /api/customers - ERROR
)

echo.
echo Probando endpoint de predicciones...
curl -s http://localhost:8080/api/customers/predictions >nul
if %errorlevel% equ 0 (
    echo ✅ GET /api/customers/predictions - OK
) else (
    echo ❌ GET /api/customers/predictions - ERROR
)

echo.
echo Probando endpoint de productos...
curl -s http://localhost:8080/api/products >nul
if %errorlevel% equ 0 (
    echo ✅ GET /api/products - OK
) else (
    echo ❌ GET /api/products - ERROR
)

echo.
echo Probando endpoint de empleados...
curl -s http://localhost:8080/api/employees >nul
if %errorlevel% equ 0 (
    echo ✅ GET /api/employees - OK
) else (
    echo ❌ GET /api/employees - ERROR
)

echo.
echo ========================================
echo   Verificación completada
echo ========================================
echo.
echo La API está disponible en: http://localhost:8080
echo.
pause
