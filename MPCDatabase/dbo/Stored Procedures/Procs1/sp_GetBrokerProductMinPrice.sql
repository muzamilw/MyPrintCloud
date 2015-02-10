CREATE PROCEDURE sp_GetBrokerProductMinPrice 
	@ItemID int,
	@ContactCompanyID int
AS
BEGIN
	SELECT floor (ISNULL(dbo.fn_GetBrokerProductMinPrice(@ItemID, @ContactCompanyID), 0.0)) AS MinPrice
                     

END