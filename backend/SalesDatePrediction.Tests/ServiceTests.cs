using Moq;
using SalesDatePrediction.API.Models;
using SalesDatePrediction.API.Repositories;
using SalesDatePrediction.API.Services;
using SalesDatePrediction.API.Controllers;
using SalesDatePrediction.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xunit;

namespace SalesDatePrediction.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;
        private readonly EmployeeService _service;

        public EmployeeServiceTests()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ReturnsAllEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee { EmpID = 1, FirstName = "Nancy", LastName = "Davolio" },
                new Employee { EmpID = 2, FirstName = "Andrew", LastName = "Fuller" }
            };

            _mockRepository.Setup(repo => repo.GetAllEmployeesAsync())
                .ReturnsAsync(employees);

            var result = await _service.GetAllEmployeesAsync();

            Assert.Equal(2, result.Count());
            Assert.Equal(1, result.First().EmpID);
            Assert.Equal(2, result.Last().EmpID);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ReturnsEmployee_WhenEmployeeExists()
        {
            var empId = 1;
            var employee = new Employee { EmpID = empId, FirstName = "Nancy", LastName = "Davolio" };

            _mockRepository.Setup(repo => repo.GetEmployeeByIdAsync(empId))
                .ReturnsAsync(employee);

            var result = await _service.GetEmployeeByIdAsync(empId);

            Assert.NotNull(result);
            Assert.Equal(empId, result.EmpID);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ReturnsNull_WhenEmployeeDoesNotExist()
        {
            var empId = 999;

            _mockRepository.Setup(repo => repo.GetEmployeeByIdAsync(empId))
                .ReturnsAsync((Employee?)null);

            var result = await _service.GetEmployeeByIdAsync(empId);

            Assert.Null(result);
        }
    }

    public class ShipperServiceTests
    {
        private readonly Mock<IShipperRepository> _mockRepository;
        private readonly ShipperService _service;

        public ShipperServiceTests()
        {
            _mockRepository = new Mock<IShipperRepository>();
            _service = new ShipperService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllShippersAsync_ReturnsAllShippers()
        {
            var shippers = new List<Shipper>
            {
                new Shipper { ShipperID = 1, CompanyName = "Speedy Express" },
                new Shipper { ShipperID = 2, CompanyName = "United Package" }
            };

            _mockRepository.Setup(repo => repo.GetAllShippersAsync())
                .ReturnsAsync(shippers);

            var result = await _service.GetAllShippersAsync();

            Assert.Equal(2, result.Count());
            Assert.Equal(1, result.First().ShipperID);
            Assert.Equal(2, result.Last().ShipperID);
        }

        [Fact]
        public async Task GetShipperByIdAsync_ReturnsShipper_WhenShipperExists()
        {
            var shipperId = 1;
            var shipper = new Shipper { ShipperID = shipperId, CompanyName = "Speedy Express" };

            _mockRepository.Setup(repo => repo.GetShipperByIdAsync(shipperId))
                .ReturnsAsync(shipper);

            var result = await _service.GetShipperByIdAsync(shipperId);

            Assert.NotNull(result);
            Assert.Equal(shipperId, result.ShipperID);
        }

        [Fact]
        public async Task GetShipperByIdAsync_ReturnsNull_WhenShipperDoesNotExist()
        {
            var shipperId = 999;

            _mockRepository.Setup(repo => repo.GetShipperByIdAsync(shipperId))
                .ReturnsAsync((Shipper?)null);

            var result = await _service.GetShipperByIdAsync(shipperId);

            Assert.Null(result);
        }
    }

    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _service = new ProductService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsAllProducts()
        {
            var products = new List<Product>
            {
                new Product { ProductID = 1, ProductName = "Chai" },
                new Product { ProductID = 2, ProductName = "Chang" }
            };

            _mockRepository.Setup(repo => repo.GetAllProductsAsync())
                .ReturnsAsync(products);

            var result = await _service.GetAllProductsAsync();

            Assert.Equal(2, result.Count());
            Assert.Equal(1, result.First().ProductID);
            Assert.Equal(2, result.Last().ProductID);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsProduct_WhenProductExists()
        {
            var productId = 1;
            var product = new Product { ProductID = productId, ProductName = "Chai" };

            _mockRepository.Setup(repo => repo.GetProductByIdAsync(productId))
                .ReturnsAsync(product);

            var result = await _service.GetProductByIdAsync(productId);

            Assert.NotNull(result);
            Assert.Equal(productId, result.ProductID);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsNull_WhenProductDoesNotExist()
        {
            var productId = 999;

            _mockRepository.Setup(repo => repo.GetProductByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            var result = await _service.GetProductByIdAsync(productId);

            Assert.Null(result);
        }
    }

    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockRepository;
        private readonly CustomerService _service;

        public CustomerServiceTests()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            _service = new CustomerService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllCustomersAsync_ReturnsAllCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer { CustID = 1, CompanyName = "Customer AHPOP" },
                new Customer { CustID = 2, CompanyName = "Customer AHXHT" }
            };

            _mockRepository.Setup(repo => repo.GetAllCustomersAsync())
                .ReturnsAsync(customers);

            var result = await _service.GetAllCustomersAsync();

            Assert.Equal(2, result.Count());
            Assert.Equal(1, result.First().CustID);
            Assert.Equal(2, result.Last().CustID);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ReturnsCustomer_WhenCustomerExists()
        {
            var custId = 1;
            var customer = new Customer { CustID = custId, CompanyName = "Customer AHPOP" };

            _mockRepository.Setup(repo => repo.GetCustomerByIdAsync(custId))
                .ReturnsAsync(customer);

            var result = await _service.GetCustomerByIdAsync(custId);

            Assert.NotNull(result);
            Assert.Equal(custId, result.CustID);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ReturnsNull_WhenCustomerDoesNotExist()
        {
            var custId = 999;

            _mockRepository.Setup(repo => repo.GetCustomerByIdAsync(custId))
                .ReturnsAsync((Customer?)null);

            var result = await _service.GetCustomerByIdAsync(custId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetCustomerPredictionsAsync_ReturnsPredictions()
        {
            var predictions = new List<CustomerPrediction>
            {
                new CustomerPrediction 
                { 
                    CustomerName = "Customer AHPOP", 
                    LastOrderDate = DateTime.Parse("2008-02-04"),
                    NextPredictedOrder = DateTime.Parse("2008-08-29")
                },
                new CustomerPrediction 
                { 
                    CustomerName = "Customer AHXHT", 
                    LastOrderDate = DateTime.Parse("2008-05-05"),
                    NextPredictedOrder = DateTime.Parse("2008-11-30")
                }
            };

            _mockRepository.Setup(repo => repo.GetCustomerPredictionsAsync(null))
                .ReturnsAsync(predictions);

            var result = await _service.GetCustomerPredictionsAsync();

            Assert.Equal(2, result.Count());
            Assert.Equal("Customer AHPOP", result.First().CustomerName);
            Assert.Equal("Customer AHXHT", result.Last().CustomerName);
        }

        [Fact]
        public async Task GetCustomerPredictionsAsync_WithCustomerName_ReturnsFilteredPredictions()
        {
            var customerName = "Customer AHPOP";
            var predictions = new List<CustomerPrediction>
            {
                new CustomerPrediction 
                { 
                    CustID = 1,
                    CustomerName = "Customer AHPOP", 
                    LastOrderDate = DateTime.Parse("2008-02-04"),
                    NextPredictedOrder = DateTime.Parse("2008-08-29")
                }
            };

            _mockRepository.Setup(repo => repo.GetCustomerPredictionsAsync(customerName))
                .ReturnsAsync(predictions);

            var result = await _service.GetCustomerPredictionsAsync(customerName);

            Assert.Single(result);
            Assert.Equal("Customer AHPOP", result.First().CustomerName);
        }
    }

    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockRepository;
        private readonly OrderService _service;

        public OrderServiceTests()
        {
            _mockRepository = new Mock<IOrderRepository>();
            _service = new OrderService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetOrdersByCustomerIdAsync_ReturnsOrders()
        {
            var custId = "1";
            var orders = new List<Order>
            {
                new Order { OrderID = 10248, CustID = 1, OrderDate = DateTime.Parse("2006-07-04") },
                new Order { OrderID = 10249, CustID = 1, OrderDate = DateTime.Parse("2006-07-05") }
            };

            _mockRepository.Setup(repo => repo.GetOrdersByCustomerIdAsync(custId))
                .ReturnsAsync(orders);

            var result = await _service.GetOrdersByCustomerIdAsync(custId);

            Assert.Equal(2, result.Count());
            Assert.Equal(10248, result.First().OrderID);
            Assert.Equal(10249, result.Last().OrderID);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnsOrder_WhenOrderExists()
        {
            var orderId = 10248;
            var order = new Order 
            { 
                OrderID = orderId, 
                CustID = 1, 
                OrderDate = DateTime.Parse("2006-07-04") 
            };

            _mockRepository.Setup(repo => repo.GetOrderByIdAsync(orderId))
                .ReturnsAsync(order);

            var result = await _service.GetOrderByIdAsync(orderId);

            Assert.NotNull(result);
            Assert.Equal(orderId, result.OrderID);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnsNull_WhenOrderDoesNotExist()
        {
            var orderId = 99999;

            _mockRepository.Setup(repo => repo.GetOrderByIdAsync(orderId))
                .ReturnsAsync((Order?)null);

            var result = await _service.GetOrderByIdAsync(orderId);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateOrderAsync_CreatesOrderSuccessfully()
        {
            var order = new Order 
            { 
                CustID = 1, 
                EmpID = 1, 
                OrderDate = DateTime.Now,
                RequiredDate = DateTime.Now.AddDays(7),
                ShipperID = 1,
                Freight = 10.50m,
                ShipName = "Test Customer",
                ShipAddress = "Test Address",
                ShipCity = "Test City",
                ShipCountry = "Test Country"
            };

            var orderDetail = new OrderDetail
            {
                ProductID = 1,
                UnitPrice = 18.00m,
                Qty = 2,
                Discount = 0.0m
            };

            var createdOrder = new Order 
            { 
                OrderID = 10250, 
                CustID = 1, 
                OrderDate = DateTime.Now 
            };

            _mockRepository.Setup(repo => repo.CreateOrderAsync(order, orderDetail))
                .ReturnsAsync(10250);

            _mockRepository.Setup(repo => repo.GetOrderByIdAsync(10250))
                .ReturnsAsync(createdOrder);

            var result = await _service.CreateOrderAsync(order, orderDetail);

            Assert.NotNull(result);
            Assert.Equal(10250, result.OrderID);
            _mockRepository.Verify(repo => repo.CreateOrderAsync(order, orderDetail), Times.Once);
            _mockRepository.Verify(repo => repo.GetOrderByIdAsync(10250), Times.Once);
        }
    }

    public class CustomersControllerTests
    {
        private readonly Mock<ICustomerService> _mockService;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _mockService = new Mock<ICustomerService>();
            _controller = new CustomersController(_mockService.Object);
        }

        [Fact]
        public async Task GetCustomers_ReturnsOkResult_WithCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer { CustID = 1, CompanyName = "Customer AHPOP" },
                new Customer { CustID = 2, CompanyName = "Customer AHXHT" }
            };

            _mockService.Setup(service => service.GetAllCustomersAsync())
                .ReturnsAsync(customers);

            var result = await _controller.GetCustomers();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCustomers = Assert.IsType<List<Customer>>(okResult.Value);
            Assert.Equal(2, returnedCustomers.Count);
        }

        [Fact]
        public async Task GetCustomer_ReturnsOkResult_WhenCustomerExists()
        {
            var customerId = 1;
            var customer = new Customer { CustID = customerId, CompanyName = "Customer AHPOP" };

            _mockService.Setup(service => service.GetCustomerByIdAsync(customerId))
                .ReturnsAsync(customer);

            var result = await _controller.GetCustomer(customerId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCustomer = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(customerId, returnedCustomer.CustID);
        }

        [Fact]
        public async Task GetCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            var customerId = 999;

            _mockService.Setup(service => service.GetCustomerByIdAsync(customerId))
                .ReturnsAsync((Customer?)null);

            var result = await _controller.GetCustomer(customerId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetCustomerPredictions_ReturnsOkResult_WithPredictions()
        {
            var predictions = new List<CustomerPrediction>
            {
                new CustomerPrediction 
                { 
                    CustID = 1,
                    CustomerName = "Customer AHPOP", 
                    LastOrderDate = DateTime.Parse("2008-02-04"),
                    NextPredictedOrder = DateTime.Parse("2008-08-29")
                }
            };

            _mockService.Setup(service => service.GetCustomerPredictionsAsync(null))
                .ReturnsAsync(predictions);

            var result = await _controller.GetCustomerPredictions();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPredictions = Assert.IsType<List<CustomerPrediction>>(okResult.Value);
            Assert.Single(returnedPredictions);
        }

        [Fact]
        public async Task GetCustomerPredictions_WithCustomerName_ReturnsFilteredPredictions()
        {
            var customerName = "Customer AHPOP";
            var predictions = new List<CustomerPrediction>
            {
                new CustomerPrediction 
                { 
                    CustID = 1,
                    CustomerName = "Customer AHPOP", 
                    LastOrderDate = DateTime.Parse("2008-02-04"),
                    NextPredictedOrder = DateTime.Parse("2008-08-29")
                }
            };

            _mockService.Setup(service => service.GetCustomerPredictionsAsync(customerName))
                .ReturnsAsync(predictions);

            var result = await _controller.GetCustomerPredictions(customerName);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPredictions = Assert.IsType<List<CustomerPrediction>>(okResult.Value);
            Assert.Single(returnedPredictions);
            Assert.Equal("Customer AHPOP", returnedPredictions.First().CustomerName);
        }
    }

    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductsController(_mockService.Object);
        }

        [Fact]
        public async Task GetProducts_ReturnsOkResult_WithProducts()
        {
            var products = new List<Product>
            {
                new Product { ProductID = 1, ProductName = "Chai" },
                new Product { ProductID = 2, ProductName = "Chang" }
            };

            _mockService.Setup(service => service.GetAllProductsAsync())
                .ReturnsAsync(products);

            var result = await _controller.GetProducts();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProducts = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(2, returnedProducts.Count);
        }

        [Fact]
        public async Task GetProduct_ReturnsOkResult_WhenProductExists()
        {
            var productId = 1;
            var product = new Product { ProductID = productId, ProductName = "Chai" };

            _mockService.Setup(service => service.GetProductByIdAsync(productId))
                .ReturnsAsync(product);

            var result = await _controller.GetProduct(productId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(productId, returnedProduct.ProductID);
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            var productId = 999;

            _mockService.Setup(service => service.GetProductByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            var result = await _controller.GetProduct(productId);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }

    public class EmployeesControllerTests
    {
        private readonly Mock<IEmployeeService> _mockService;
        private readonly EmployeesController _controller;

        public EmployeesControllerTests()
        {
            _mockService = new Mock<IEmployeeService>();
            _controller = new EmployeesController(_mockService.Object);
        }

        [Fact]
        public async Task GetEmployees_ReturnsOkResult_WithEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee { EmpID = 1, FirstName = "Nancy", LastName = "Davolio" },
                new Employee { EmpID = 2, FirstName = "Andrew", LastName = "Fuller" }
            };

            _mockService.Setup(service => service.GetAllEmployeesAsync())
                .ReturnsAsync(employees);

            var result = await _controller.GetEmployees();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployees = Assert.IsType<List<Employee>>(okResult.Value);
            Assert.Equal(2, returnedEmployees.Count);
        }

        [Fact]
        public async Task GetEmployee_ReturnsOkResult_WhenEmployeeExists()
        {
            var empId = 1;
            var employee = new Employee { EmpID = empId, FirstName = "Nancy", LastName = "Davolio" };

            _mockService.Setup(service => service.GetEmployeeByIdAsync(empId))
                .ReturnsAsync(employee);

            var result = await _controller.GetEmployee(empId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployee = Assert.IsType<Employee>(okResult.Value);
            Assert.Equal(empId, returnedEmployee.EmpID);
        }

        [Fact]
        public async Task GetEmployee_ReturnsNotFound_WhenEmployeeDoesNotExist()
        {
            var empId = 999;

            _mockService.Setup(service => service.GetEmployeeByIdAsync(empId))
                .ReturnsAsync((Employee?)null);

            var result = await _controller.GetEmployee(empId);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }

    public class ShippersControllerTests
    {
        private readonly Mock<IShipperService> _mockService;
        private readonly ShippersController _controller;

        public ShippersControllerTests()
        {
            _mockService = new Mock<IShipperService>();
            _controller = new ShippersController(_mockService.Object);
        }

        [Fact]
        public async Task GetShippers_ReturnsOkResult_WithShippers()
        {
            var shippers = new List<Shipper>
            {
                new Shipper { ShipperID = 1, CompanyName = "Speedy Express" },
                new Shipper { ShipperID = 2, CompanyName = "United Package" }
            };

            _mockService.Setup(service => service.GetAllShippersAsync())
                .ReturnsAsync(shippers);

            var result = await _controller.GetShippers();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedShippers = Assert.IsType<List<Shipper>>(okResult.Value);
            Assert.Equal(2, returnedShippers.Count);
        }

        [Fact]
        public async Task GetShipper_ReturnsOkResult_WhenShipperExists()
        {
            var shipperId = 1;
            var shipper = new Shipper { ShipperID = shipperId, CompanyName = "Speedy Express" };

            _mockService.Setup(service => service.GetShipperByIdAsync(shipperId))
                .ReturnsAsync(shipper);

            var result = await _controller.GetShipper(shipperId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedShipper = Assert.IsType<Shipper>(okResult.Value);
            Assert.Equal(shipperId, returnedShipper.ShipperID);
        }

        [Fact]
        public async Task GetShipper_ReturnsNotFound_WhenShipperDoesNotExist()
        {
            var shipperId = 999;

            _mockService.Setup(service => service.GetShipperByIdAsync(shipperId))
                .ReturnsAsync((Shipper?)null);

            var result = await _controller.GetShipper(shipperId);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }

    public class OrdersControllerTests
    {
        private readonly Mock<IOrderService> _mockService;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            _mockService = new Mock<IOrderService>();
            _controller = new OrdersController(_mockService.Object);
        }

        [Fact]
        public async Task GetOrdersByCustomer_ReturnsOkResult_WithOrders()
        {
            var customerId = "1";
            var orders = new List<Order>
            {
                new Order { OrderID = 10248, CustID = 1, OrderDate = DateTime.Parse("2006-07-04") },
                new Order { OrderID = 10249, CustID = 1, OrderDate = DateTime.Parse("2006-07-05") }
            };

            _mockService.Setup(service => service.GetOrdersByCustomerIdAsync(customerId))
                .ReturnsAsync(orders);

            var result = await _controller.GetOrdersByCustomer(customerId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedOrders = Assert.IsType<List<Order>>(okResult.Value);
            Assert.Equal(2, returnedOrders.Count);
        }

        [Fact]
        public async Task GetOrder_ReturnsOkResult_WhenOrderExists()
        {
            var orderId = 10248;
            var order = new Order 
            { 
                OrderID = orderId, 
                CustID = 1, 
                OrderDate = DateTime.Parse("2006-07-04") 
            };

            _mockService.Setup(service => service.GetOrderByIdAsync(orderId))
                .ReturnsAsync(order);

            var result = await _controller.GetOrder(orderId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedOrder = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(orderId, returnedOrder.OrderID);
        }

        [Fact]
        public async Task GetOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            var orderId = 99999;

            _mockService.Setup(service => service.GetOrderByIdAsync(orderId))
                .ReturnsAsync((Order?)null);

            var result = await _controller.GetOrder(orderId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateOrder_ReturnsCreatedAtAction_WhenOrderIsValid()
        {
            var orderDto = new OrderCreateDto
            {
                CustID = 1,
                EmpID = 1,
                RequiredDate = DateTime.Now.AddDays(7),
                ShipperID = 1,
                Freight = 10.50m,
                ShipName = "Test Customer",
                ShipAddress = "Test Address",
                ShipCity = "Test City",
                ShipCountry = "Test Country",
                ProductID = 1,
                UnitPrice = 18.00m,
                Quantity = 2,
                Discount = 0.0m
            };

            var createdOrder = new Order 
            { 
                OrderID = 10250, 
                CustID = 1, 
                OrderDate = DateTime.Now 
            };

            _mockService.Setup(service => service.CreateOrderAsync(It.IsAny<Order>(), It.IsAny<OrderDetail>()))
                .ReturnsAsync(createdOrder);

            var result = await _controller.CreateOrder(orderDto);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedOrder = Assert.IsType<Order>(createdAtActionResult.Value);
            Assert.Equal(10250, returnedOrder.OrderID);
        }

        [Fact]
        public async Task CreateOrder_ReturnsBadRequest_WhenOrderDtoIsNull()
        {
            OrderCreateDto? orderDto = null;

            var result = await _controller.CreateOrder(orderDto!);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }

    public class IntegrationTests
    {
        [Fact]
        public void Models_HaveCorrectProperties()
        {
            var customer = new Customer { CustID = 1, CompanyName = "Test" };
            var order = new Order { OrderID = 1, CustID = 1, OrderDate = DateTime.Now };
            var product = new Product { ProductID = 1, ProductName = "Test" };
            var employee = new Employee { EmpID = 1, FirstName = "Test", LastName = "Test" };
            var shipper = new Shipper { ShipperID = 1, CompanyName = "Test" };
            var orderDetail = new OrderDetail { OrderID = 1, ProductID = 1, Qty = 1, UnitPrice = 10.0m };

            Assert.Equal(1, customer.CustID);
            Assert.Equal(1, order.OrderID);
            Assert.Equal(1, product.ProductID);
            Assert.Equal(1, employee.EmpID);
            Assert.Equal(1, shipper.ShipperID);
            Assert.Equal(1, orderDetail.OrderID);
        }

        [Fact]
        public void CustomerPrediction_ModelIsValid()
        {
            var prediction = new CustomerPrediction
            {
                CustID = 1,
                CustomerName = "Test Customer",
                LastOrderDate = DateTime.Now.AddDays(-30),
                NextPredictedOrder = DateTime.Now.AddDays(30)
            };

            Assert.Equal(1, prediction.CustID);
            Assert.Equal("Test Customer", prediction.CustomerName);
            Assert.True(prediction.LastOrderDate < prediction.NextPredictedOrder);
        }

        [Fact]
        public void OrderCreateDto_ModelIsValid()
        {
            var orderDto = new OrderCreateDto
            {
                CustID = 1,
                EmpID = 1,
                RequiredDate = DateTime.Now.AddDays(7),
                ShipperID = 1,
                Freight = 10.50m,
                ShipName = "Test Customer",
                ShipAddress = "Test Address",
                ShipCity = "Test City",
                ShipCountry = "Test Country",
                ProductID = 1,
                UnitPrice = 18.00m,
                Quantity = 2,
                Discount = 0.0m
            };

            Assert.Equal(1, orderDto.CustID);
            Assert.Equal(1, orderDto.EmpID);
            Assert.Equal(1, orderDto.ShipperID);
            Assert.Equal(10.50m, orderDto.Freight);
            Assert.Equal("Test Customer", orderDto.ShipName);
        }
    }
}
