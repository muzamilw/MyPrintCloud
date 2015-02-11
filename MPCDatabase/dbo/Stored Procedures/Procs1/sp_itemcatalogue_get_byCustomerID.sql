CREATE PROCEDURE dbo.sp_itemcatalogue_get_byCustomerID
(@CustomerID int,
@GeneralCustomer int)
                  AS
select * from tbl_finishedgoods_catalogue where (CustomerID=@CustomerID or CustomerID=@GeneralCustomer)  and isdisabled=0

RETURN