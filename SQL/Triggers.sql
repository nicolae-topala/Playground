CREATE TRIGGER DecrementTotalProducts ON Products
AFTER DELETE AS
BEGIN
    UPDATE Customers
    SET TotalProducts = TotalProducts - 1
    WHERE Customers.CustomerId IN (SELECT CustomerId FROM deleted)
END;

-- CREATE TRIGGER IncrementTotalProducts ON Products
-- AFTER INSERT AS
-- BEGIN
--     UPDATE Customers
--     SET TotalProducts = TotalProducts - 1
--     WHERE Customers.CustomerId IN (SELECT CustomerId FROM inserted)
-- END