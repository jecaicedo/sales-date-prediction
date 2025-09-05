@echo off
echo ========================================
echo   Sales Date Prediction API
echo ========================================
echo.
echo Iniciando la API en http://localhost:8080
echo.
echo Endpoints disponibles:
echo - http://localhost:8080/api/customers
echo - http://localhost:8080/api/customers/predictions
echo - http://localhost:8080/api/products
echo - http://localhost:8080/api/employees
echo - http://localhost:8080/api/shippers
echo - http://localhost:8080/api/orders/customer/{id}
echo - http://localhost:8080/api/orders/{id}
echo.
echo Presiona Ctrl+C para detener la API
echo ========================================
echo.

cd SalesDatePrediction.API
dotnet run --project SalesDatePrediction.API.csproj

pause
