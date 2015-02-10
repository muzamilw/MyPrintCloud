CREATE PROCEDURE dbo.sp_guillotineclickcharge_get_byMethodID
(@MethodID int)
AS
select ID,MethodID,PaperWeight1,
         PaperThroatQty1,PaperWeight2,PaperThroatQty2,PaperWeight3,PaperThroatQty3,PaperWeight4,PaperThroatQty4,
         PaperWeight5,PaperThroatQty5 from tbl_machine_guillotinecalc where MethodID=@MethodID
        
                 RETURN