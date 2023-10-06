CREATE PROCEDURE SelectAllCustomers
AS 
SELECT * FROM Customers
GO;

CREATE PROCEDURE SelectCustomer @Id INT
AS
SELECT * FROM Customers WHERE CustomerId = @Id
GO;

CREATE PROCEDURE SelectProductsByCustomerId @Id INT
AS 
SELECT Products.CustomerId, CustomerName, ProductName, Price FROM Products
INNER JOIN Customers ON Customers.CustomerId = Products.CustomerId
WHERE Products.CustomerId = @Id
GO;

CREATE PROCEDURE SelectProductsByCustomerIdAndMaxPrice @Id INT, @Price INT
AS
SELECT Products.CustomerId, CustomerName, ProductName, Price FROM Products
INNER JOIN Customers ON Customers.CustomerId = Products.CustomerId
WHERE Products.CustomerId = @Id AND Price <= @Price
GO;

CREATE PROCEDURE InsertProductByCustomerId @CustomerId INT, @Price INT, @ProductName NVARCHAR(50)
AS
INSERT INTO  Products (ProductName, Price, CustomerId)
VALUES (@ProductName, @Price, @CustomerId)
GO;

