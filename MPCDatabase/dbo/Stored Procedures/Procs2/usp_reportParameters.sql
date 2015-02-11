
-- =============================================
-- Author:		<Muhammad Naveed>
-- Create date: <2013/03/20>
-- Description:	<To get record for Report Parameters dynamically>
-- =============================================

--exec  [usp_reportParameters] 'tbl_contactCompanies','Name','ContactCompanyID',''

create PROCEDURE [dbo].[usp_reportParameters] 
(
	@tblName varchar(100),
	@DisplayColumn varchar(100),
	@ValueColumn varchar(100),
	@WhereClause varchar(200)
	
)
    
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @string AS VARCHAR(500)

    SET @string =  'select ' + @ValueColumn + ' As selectedID,' + @DisplayColumn + ' As displayValue' +
		' from ' + @tblName 
    set @string = @string + ' ' + @WhereClause

    EXEC(@string)
END