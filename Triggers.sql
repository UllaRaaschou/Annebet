
CREATE TRIGGER TRG_CheckForDuplicatesANDPhoneLength
ON CUSTOMER
INSTEAD OF INSERT
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM CUSTOMER c
        INNER JOIN INSERTED i ON 
            c.FirstName = i.FirstName
            AND c.LastName = i.LastName
            AND c.Address = i.Address
            AND c.Phone = i.Phone
            AND c.Email = i.Email
    )
    BEGIN
        DECLARE @ErrorMessageExists NVARCHAR(1000);
        SET @ErrorMessageExists = 'Fejl: Duplicate Customer found. INSERT operation aborted.';
        THROW 50000, @ErrorMessageExists, 1;
    END

    ELSE
    BEGIN
        IF EXISTS (
            SELECT 1
            FROM INSERTED
            WHERE LEN(Phone) <> 8 OR Phone IS NULL
        )
        BEGIN
			DECLARE @ErrorMessagePhoneLength NVARCHAR(1000);
			SET @ErrorMessagePhoneLength = 'Phone Number must be 8 characters and not Null. INSERT operation aborted';
            THROW 50000, @ErrorMessagePhoneLength, 1;
        END
	
    ELSE
        BEGIN
            INSERT INTO CUSTOMER (FirstName, LastName, Address, Phone, Email)
            SELECT FirstName, LastName, Address, Phone, Email
            FROM INSERTED;
        END
    END
END;

GO

CREATE TRIGGER TRG_CheckForDuplicatesAndTypeOfSalesItem
ON dbo.SALESITEM_PRODUCT_TREATMENT
INSTEAD OF INSERT
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM dbo.SALESITEM_PRODUCT_TREATMENT s
        INNER JOIN INSERTED i ON 
            s.Category = i.Category
            AND s.Type = i.Type
            AND s.Name = i.Name
           
    )
    BEGIN
        DECLARE @ErrorMessageExists NVARCHAR(1000);
        SET @ErrorMessageExists = 'Fejl: Duplicate product og treatment found. INSERT operation aborted.';
        THROW 50000, @ErrorMessageExists, 1;
    END
    
    IF EXISTS (
        SELECT 1
        FROM INSERTED
        WHERE Category NOT IN ('Product', 'Treatment')
    )
    BEGIN
        DECLARE @ErrorMessageType NVARCHAR(1000);
        SET @ErrorMessageType = 'Fejl: Category skal være enten ''Product'' eller ''Treatment''. INSERT operationen blev afbrudt.';
        THROW 50000, @ErrorMessageType, 1;
	END
	
    INSERT INTO dbo.SALESITEM_PRODUCT_TREATMENT (Category, [Type], [Name], [Description], Price)
    SELECT Category, [Type], [Name], [Description], Price
    FROM INSERTED;
    
END;