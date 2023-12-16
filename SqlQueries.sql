
CREATE PROC sp_AddCustomer
@firstName Nvarchar(50),
@lastName Nvarchar(50),
@address Nvarchar(100),
@phone Nvarchar(10),
@email Nvarchar(100)

AS
BEGIN
INSERT INTO dbo.CUSTOMER (FirstName, LastName, [Address], Phone, Email) 
VALUES(@firstName, @lastName, @address, @phone, @email);
SELECT SCOPE_IDENTITY() AS NewId;
END;

GO

CREATE PROC sp_UpdateCustomer
@customerId int,
@firstName Nvarchar(50),
@lastName Nvarchar(50),
@address Nvarchar(100),
@phone Nvarchar(10),
@email Nvarchar(100)
AS
BEGIN
UPDATE dbo.CUSTOMER 
SET FirstName = @firstName, 
	LastName = @lastname,
	[Address] = @address, 
	Phone = @phone, 
	Email = @email 
WHERE PK_CustomerId = @customerId
END;

GO

CREATE PROC sp_GetCustomerById
@customerId Int
AS
BEGIN
SELECT FirstName, LastName, [Address], Phone, Email, PK_CustomerId from dbo.Customer WHERE PK_CustomerId = @customerId
END;

GO


CREATE PROC sp_GetCustomerById
@customerId Int
AS
BEGIN
SELECT PK_CustomerId, FirstName, LastName, [Address], Phone, Email from dbo.Customer WHERE PK_CustomerId = @customerId
END;

GO

CREATE PROC sp_GetAllCustomers
AS
BEGIN
SELECT * FROM dbo.CUSTOMER 
END;

GO 

CREATE PROC sp_GetAllCustomers
@firstName Nvarchar(50),
@lastName Nvarchar(50)
AS
BEGIN
SELECT PK_CustomerId, [Address], Phone, Email FROM dbo.CUSTOMER WHERE FirstName = @firstName AND LastName = @lastName
END;

GO 

CREATE PROC sp_DeleteCustomerById
@customerId Int
AS
BEGIN
Delete FROM dbo.CUSTOMER WHERE PK_CustomerId = @customerId
END;

GO

CREATE PROC sp_AddSalesItem
@category Nvarchar(50),
@type Nvarchar(50),
@name Nvarchar(100),
@description Nvarchar(max),
@price decimal(18,0)

AS
BEGIN
INSERT INTO dbo.SALESITEM_PRODUCT_TREATMENT (Category, [Type], [Name], [Description], Price) 
VALUES(@category, @type, @name, @description, @price);
SELECT @@identity AS NewId;
END;

GO

CREATE PROC sp_GetAllSalesItemsFromCategoryAndTypeAndName
@category Nvarchar(50),
@type Nvarchar(50),
@name Nvarchar(100)
AS
BEGIN
SELECT PK_SalesItemId_Product_Treatment, [Description], [Price] FROM dbo.SALESITEM_PRODUCT_TREATMENT 
WHERE Category = @category AND [Type] = @type AND [Name] like '%' +@name + '%'
END;

GO

CREATE PROC sp_GetAllSalesItems
AS
BEGIN
SELECT * FROM dbo.SALESITEM_PRODUCT_TREATMENT
END;

GO

CREATE PROC sp_UpdateSalesItem
@salesItemId int,
@category Nvarchar(50),
@type Nvarchar(50),
@name Nvarchar(100),
@description Nvarchar(max),
@price decimal(18,0)
AS
BEGIN
UPDATE dbo.SALESITEM_PRODUCT_TREATMENT 
SET Category = @category, 
	[Type] = @type,
	[Name] = @name, 
	[Description] = @description, 
	Price = @price 
WHERE PK_SalesItemId_Product_Treatment = @salesItemId
END;

GO

CREATE PROC sp_GetSalesItemFromId
@id int
AS
BEGIN
SELECT Category, [Type], [Name], [Description], Price FROM dbo.SALESITEM_PRODUCT_TREATMENT
WHERE PK_SalesItemId_Product_Treatment = @id
END;

GO

CREATE PROC sp_DeleteSalesItemById
@salesItemId Int
AS
BEGIN
Delete FROM dbo.SALESITEM_PRODUCT_TREATMENT 
WHERE PK_SalesItemId_Product_Treatment = @salesItemId
END;