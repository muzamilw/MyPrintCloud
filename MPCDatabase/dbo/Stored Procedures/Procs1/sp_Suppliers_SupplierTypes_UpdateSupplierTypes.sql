﻿CREATE PROCEDURE dbo.sp_Suppliers_SupplierTypes_UpdateSupplierTypes

AS
	SELECT TypeName FROM tbl_suppliertypes
	RETURN