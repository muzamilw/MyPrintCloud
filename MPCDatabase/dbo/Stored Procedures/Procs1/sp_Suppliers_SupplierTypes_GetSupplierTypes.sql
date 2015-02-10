CREATE PROCEDURE [dbo].[sp_Suppliers_SupplierTypes_GetSupplierTypes]

AS
SELECT TypeID,TypeName,IsFixed FROM tbl_contactcompanytypes order by TypeName
	RETURN