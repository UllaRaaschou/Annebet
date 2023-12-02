
//using System.Net;
//using WPFApp.Models;

//CREATE PROC sp_AddCustomer
//@firstName Nvarchar(50),
//@lastName Nvarchar(50),
//@address Nvarchar(100),
//@phone Nvarchar(10),
//@email Nvarchar(100)

//AS
//BEGIN
//INSERT INTO dbo.CUSTOMER (FirstName, LastName, Address, Phone, Email) 
//VALUES(@firstName, @lastName, @address, @phone, @email);
//SELECT SCOPE_IDENTITY() AS NewId;
//END;

//CREATE PROC sp_UpdateCustomer
//@firstName Nvarchar(50),
//@lastName Nvarchar(50),
//@address Nvarchar(100),
//@phone Nvarchar(10),
//@email Nvarchar(100),
//@customerId Int

//AS
//BEGIN
//UPDATE dbo.CUSTOMER SET FirstName = @firstName, LastName = @lastname,
//[Address] = @address, Phone = @phone, Email = @email WHERE PK_CustomerId = @customerId
//END;


//CREATE PROC sp_GetCustomerById
//@customerId Int
//AS
//BEGIN
//SELECT FirstName, LastName, [Address], Phone, Email, PK_CustomerId from dbo.Customer WHERE PK_CustomerId = @customerId
//END;


//CREATE PROC sp_GetCustomerById
//@customerId Int
//AS
//BEGIN
//SELECT PK_CustomerId, FirstName, LastName, [Address], Phone, Email from dbo.Customer WHERE PK_CustomerId = @customerId
//END;

//CREATE PROC sp_GetAllCustomers
//AS
//BEGIN
//SELECT * FROM dbo.CUSTOMER 
//END;