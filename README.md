# Sales Date Prediction

Este repositorio contiene un sistema completo de predicción de fechas de ventas que incluye:

- **Backend**: API REST desarrollada en .NET Core 8.0 con Entity Framework y Dapper
- **Frontend**: Aplicación Angular 20 con Angular Material
- **Visualización**: Gráfica interactiva desarrollada con D3.js
- **Pruebas**: Suite completa de pruebas unitarias con xUnit

## 📋 Requisitos del Sistema

### Software Necesario
- **.NET 8.0 SDK** - Para el backend
- **Node.js 18+** - Para el frontend Angular
- **npm o yarn** - Gestor de paquetes de Node.js
- **SQL Server** - Base de datos (StoreSample)
- **Navegador web moderno** - Para visualizar las aplicaciones

### Puertos Requeridos
- **Puerto 8080** - Backend API (.NET Core)
- **Puerto 4200** - Frontend Angular
- **Puerto 3000** - Visualización D3.js (opcional, se ejecuta directamente)

## 🚀 Instrucciones de Ejecución Completa

### Paso 1: Preparación del Entorno

1. **Clonar el repositorio**:
   ```bash
   git clone https://github.com/jecaicedo/sales-date-prediction
   cd sales-date-prediction
   ```

2. **Verificar instalaciones**:
   ```bash
   # Verificar .NET
   dotnet --version
   
   # Verificar Node.js
   node --version
   npm --version
   ```

### Paso 2: Configuración de la Base de Datos

1. **Instalar SQL Server** (si no está instalado)
   - SQL Server Express, Developer Edition o cualquier versión compatible
   - SQL Server Management Studio (SSMS) para ejecutar scripts SQL

2. **Crear la base de datos StoreSample** usando el script incluido:
   ```bash
   # Opción A: Usando SQL Server Management Studio (SSMS)
   # 1. Abrir SSMS y conectarse al servidor SQL Server
   # 2. Abrir el archivo database/DBSetup.sql
   # 3. Ejecutar todo el script (F5 o botón Execute)
   
   # Opción B: Usando sqlcmd desde línea de comandos
   sqlcmd -S localhost -E -i database/DBSetup.sql
   
   # Opción C: Usando PowerShell
   Invoke-Sqlcmd -ServerInstance localhost -InputFile "database/DBSetup.sql"
   ```

3. **Verificar la instalación de la base de datos**:
   - La base de datos `StoreSample` debe crearse automáticamente
   - Se crearán las tablas: Customers, Employees, Products, Orders, OrderDetails, Shippers
   - Se insertarán datos de prueba en todas las tablas

4. **Verificar la cadena de conexión** en `backend/SalesDatePrediction.API/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=StoreSample;Trusted_Connection=true;TrustServerCertificate=true;"
     }
   }
   ```

**Nota**: El archivo `database/DBSetup.sql` contiene:
- Creación de la base de datos `StoreSample`
- Creación de esquemas (HR, Production, Sales)
- Creación de todas las tablas necesarias
- Inserción de datos de prueba completos
- Configuración de relaciones entre tablas

### Paso 3: Ejecución del Backend (.NET Core)

#### Opción A: Script Automático (Recomendado)
```bash
cd backend
# Doble clic en run-api.bat
# O ejecutar desde terminal:
run-api.bat
# O ejecutar:
.\run-api.bat
```

#### Opción B: Comando Manual
```bash
cd backend/SalesDatePrediction.API
dotnet restore
dotnet build
dotnet run
```

**Verificación**: El backend estará disponible en `http://localhost:8080`
- Swagger UI: `http://localhost:8080/swagger`
- Health check: `http://localhost:8080/api/customers`

### Paso 4: Ejecución del Frontend (Angular)

1. **Instalar dependencias**:
   ```bash
   cd frontend/sales-date-prediction-app
   npm install
   ```

2. **Iniciar la aplicación**:
   ```bash
   npm start
   # O alternativamente:
   ng serve --port 4200
   ```

**Verificación**: El frontend estará disponible en `http://localhost:4200`

### Paso 5: Visualización D3.js

1. **Abrir la visualización**:
   ```bash
   cd d3-visualization
   # Abrir index.html en el navegador
   # O usar un servidor local:
   python -m http.server 3000
   # Luego abrir: http://localhost:3000
   ```

**Verificación**: La visualización estará disponible en `http://localhost:3000`

## 🧪 Ejecución de Pruebas

### Pruebas del Backend
```bash
cd backend
# Opción A: Script automático
run-tests.bat
# O ejecuta:
.\run-tests.bat

# Opción B: Comando manual
dotnet test
```

### Pruebas del Frontend
```bash
cd frontend/sales-date-prediction-app
npm test
```

## 📡 Endpoints de la API

La API REST está disponible en `http://localhost:8080` con los siguientes endpoints:

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/customers` | Obtener todos los clientes |
| GET | `/api/customers/{id}` | Obtener cliente por ID |
| GET | `/api/customers/predictions?customerName={name}` | Obtener predicciones de próximas órdenes |
| GET | `/api/products` | Obtener todos los productos |
| GET | `/api/products/{id}` | Obtener producto por ID |
| GET | `/api/employees` | Obtener todos los empleados |
| GET | `/api/employees/{id}` | Obtener empleado por ID |
| GET | `/api/shippers` | Obtener todos los transportistas |
| GET | `/api/shippers/{id}` | Obtener transportista por ID |
| GET | `/api/orders/customer/{id}` | Obtener órdenes por cliente |
| GET | `/api/orders/{id}` | Obtener orden por ID |
| POST | `/api/orders` | Crear nueva orden |

## 🎯 Funcionalidades del Sistema

### Backend (.NET Core)
- **API REST** con arquitectura limpia (Controllers, Services, Repositories)
- **Entity Framework Core** para acceso a datos
- **Dapper** para consultas complejas y optimización
- **Swagger** para documentación automática de la API
- **CORS** configurado para desarrollo
- **Pruebas unitarias** con xUnit (40 pruebas)

### Frontend (Angular)
- **Aplicación SPA** con Angular 20
- **Angular Material** para componentes UI
- **Búsqueda en tiempo real** de clientes
- **Paginación** y ordenamiento de datos
- **Modales** para gestión de órdenes
- **Diseño responsivo** para diferentes dispositivos

### Visualización (D3.js)
- **Gráfica interactiva** con datos dinámicos
- **Entrada de datos** personalizable
- **Visualización en tiempo real** de cambios
- **Interfaz simple** y fácil de usar

## 🔧 Configuración Avanzada

### Variables de Entorno
- **Backend**: Configuración en `appsettings.json` y `appsettings.Development.json`
- **Frontend**: Variables de entorno en `src/environments/`

### CORS y Seguridad
- CORS habilitado para desarrollo (`http://localhost:4200`)
- HTTPS deshabilitado en desarrollo
- Configuración de seguridad lista para producción

## 🛠️ Solución de Problemas Comunes

### Base de Datos
1. **Error al ejecutar DBSetup.sql**: 
   - Verificar que SQL Server esté ejecutándose
   - Asegurar permisos de administrador en SQL Server
   - Verificar que no haya conexiones activas a la base de datos StoreSample
2. **Base de datos no se crea**: 
   - Ejecutar el script completo desde el inicio
   - Verificar que no exista una base de datos StoreSample previa
   - Revisar logs de SQL Server para errores específicos
3. **Datos no se insertan**: 
   - Verificar que el script se ejecute completamente
   - Comprobar que las tablas se crearon correctamente
   - Ejecutar consultas de verificación: `SELECT COUNT(*) FROM Sales.Customers`

### Backend
1. **Puerto 8080 ocupado**: Cambiar puerto en `appsettings.json`
2. **Error de conexión a BD**: 
   - Verificar que SQL Server esté ejecutándose
   - Comprobar la cadena de conexión en `appsettings.json`
   - Verificar que la base de datos StoreSample existe
   - Probar conexión con: `sqlcmd -S localhost -E -Q "USE StoreSample; SELECT 1"`
3. **Dependencias faltantes**: Ejecutar `dotnet restore`

### Frontend
1. **Puerto 4200 ocupado**: Usar `ng serve --port 4201`
2. **Dependencias faltantes**: Ejecutar `npm install`
3. **Error de CORS**: Verificar configuración del backend

### Visualización D3.js
1. **Gráfica no se muestra**: Verificar consola del navegador
2. **Datos no se actualizan**: Verificar formato de entrada (números separados por coma)

## 📊 Estructura de Datos

### CustomerPrediction (API Response)
```json
{
  "custID": 1,
  "customerName": "Customer NRZBB",
  "lastOrderDate": "2008-02-04T00:00:00",
  "nextPredictedOrder": "2008-08-29T00:00:00"
}
```

### Order (API Response)
```json
{
  "orderID": 1,
  "orderDate": "2008-02-04T00:00:00",
  "totalAmount": 150.75,
  "status": "Completed"
}
```

## 🏗️ Arquitectura del Sistema

```
sales-date-prediction/
├── backend/                    # API .NET Core
│   ├── SalesDatePrediction.API/
│   │   ├── Controllers/       # Controladores REST
│   │   ├── Services/          # Lógica de negocio
│   │   ├── Repositories/      # Acceso a datos
│   │   ├── Models/           # Entidades de datos
│   │   └── Data/             # Contexto EF y configuración
│   └── SalesDatePrediction.Tests/  # Pruebas unitarias
├── frontend/                   # Aplicación Angular
│   └── sales-date-prediction-app/
│       ├── src/app/
│       │   ├── components/    # Componentes Angular
│       │   └── services/     # Servicios HTTP
│       └── package.json      # Dependencias Node.js
├── database/                   # Scripts de Base de Datos
│   └── DBSetup.sql           # Script completo de creación y datos
└── d3-visualization/          # Visualización D3.js
    ├── index.html            # Página principal
    ├── script.js             # Lógica D3.js
    └── styles.css            # Estilos CSS
```

### Funcionalidades Implementadas
- ✅ Predicción de fechas de próximas órdenes por cliente
- ✅ API REST completa con documentación Swagger
- ✅ Interfaz web responsiva con búsqueda y paginación
- ✅ Gestión de órdenes con modales interactivos
- ✅ Visualización de datos con gráficas dinámicas
- ✅ Suite completa de pruebas unitarias