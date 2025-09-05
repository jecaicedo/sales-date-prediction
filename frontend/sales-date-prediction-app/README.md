# Sales Date Prediction App

Una aplicación Angular que permite visualizar predicciones de fechas de ventas para clientes, con funcionalidades de búsqueda, paginación y gestión de órdenes.

## Características

- **Vista Principal**: Lista de clientes con predicciones de fechas de ventas
- **Búsqueda**: Filtrado de clientes por nombre desde el servidor
- **Paginación**: Navegación por páginas con diferentes tamaños
- **Ordenamiento**: Ordenamiento por cualquier columna de la tabla
- **Modales**: 
  - Vista de órdenes del cliente
  - Formulario para crear nueva orden
- **Diseño Responsivo**: Adaptable a diferentes tamaños de pantalla

## Estructura del Proyecto

```
src/
├── app/
│   ├── components/
│   │   ├── sales-prediction/          # Componente principal
│   │   ├── orders-modal/              # Modal de órdenes
│   │   └── new-order-modal/          # Modal de nueva orden
│   ├── services/
│   │   └── customer.service.ts       # Servicio para API de clientes
│   ├── data/
│   │   └── mock-customers.ts         # Datos de prueba
│   └── ...
```

## Instalación y Configuración

### Prerrequisitos
- Node.js (versión 18 o superior)
- npm o yarn
- Angular CLI

### Instalación
1. Clona el repositorio
2. Instala las dependencias:
   ```bash
   npm install
   ```

### Configuración del Servidor
El servicio está configurado para conectarse al endpoint:
```
http://localhost:8080/api/customers/predictions
```

Para usar datos de prueba durante el desarrollo, el servicio ya está configurado con datos mock.

## Uso

### Iniciar la Aplicación
```bash
npm start
```

La aplicación estará disponible en `http://localhost:4200`

### Funcionalidades

#### Vista Principal
- Muestra una tabla con información de clientes
- Columnas: Customer Name, Last Order Date, Next Predicted Order
- Botones de acción: VIEW ORDERS y NEW ORDER

#### Búsqueda
- Campo de búsqueda en la parte superior derecha
- Filtra clientes por nombre en tiempo real
- Búsqueda del lado del servidor (configurable)

#### Paginación
- Control de paginación en la parte inferior
- Opciones de elementos por página: 5, 10, 25, 50
- Navegación con flechas y números de página

#### Ordenamiento
- Click en cualquier encabezado de columna para ordenar
- Indicadores visuales de dirección de ordenamiento
- Ordenamiento por: Customer Name, Last Order Date, Next Predicted Order

#### Modales

**Modal de Órdenes:**
- Muestra las órdenes del cliente seleccionado
- Tabla con: Order ID, Order Date, Total Amount, Status
- Paginación independiente
- Estados de órdenes con colores distintivos

**Modal de Nueva Orden:**
- Formulario para crear nueva orden
- Campos: Order Date, Total Amount, Status, Notes
- Validación de formulario
- Información del cliente en contexto

## Configuración de Producción

Para usar el servidor real en lugar de datos de prueba:

1. Edita `src/app/services/customer.service.ts`
2. Descomenta las líneas del código HTTP real
3. Comenta el código de datos mock

```typescript
getCustomerPredictions(searchTerm?: string): Observable<CustomerPrediction[]> {
  let params = new HttpParams();
  if (searchTerm && searchTerm.trim()) {
    params = params.set('customerName', searchTerm.trim());
  }
  
  return this.http.get<CustomerPrediction[]>(`${this.baseUrl}/customers/predictions`, { params });
}
```

## Estructura de Datos

### CustomerPrediction
```typescript
interface CustomerPrediction {
  custID: number;
  customerName: string;
  lastOrderDate: string;        // Formato ISO: "2007-05-22T00:00:00"
  nextPredictedOrder: string;    // Formato ISO: "2007-07-23T00:00:00"
}
```

### Order
```typescript
interface Order {
  orderID: number;
  orderDate: string;
  totalAmount: number;
  status: string;
}
```

## Tecnologías Utilizadas

- **Angular 20**: Framework principal
- **Angular Material**: Componentes UI
- **RxJS**: Programación reactiva
- **TypeScript**: Lenguaje de programación
- **CSS3**: Estilos personalizados

## Desarrollo

### Estructura de Componentes
- Componentes standalone (Angular 17+)
- Signals para manejo de estado reactivo
- Formularios reactivos para validación
- Servicios inyectables para lógica de negocio

### Estilos
- CSS personalizado con diseño responsive
- Paleta de colores consistente
- Animaciones y transiciones suaves
- Compatibilidad con dispositivos móviles

## Contribución

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.