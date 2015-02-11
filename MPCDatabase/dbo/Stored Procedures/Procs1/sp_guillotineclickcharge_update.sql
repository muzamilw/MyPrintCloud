CREATE PROCEDURE dbo.sp_guillotineclickcharge_update
(@MethodID int,
@PaperWeight1 float,
@PaperThroatQty1 float,
@PaperWeight2 float,
@PaperThroatQty2 float,
@PaperWeight3 float, 
         @PaperThroatQty3 float,
         @PaperWeight4 float,
         @PaperThroatQty4 float,
         @PaperWeight5 float,
         @PaperThroatQty5 float,@ID int)
AS
update tbl_machine_guillotinecalc set PaperWeight1=@PaperWeight1,
         PaperThroatQty1=@PaperThroatQty1,PaperWeight2=@PaperWeight2,PaperThroatQty2=@PaperThroatQty2,PaperWeight3=@PaperWeight3,PaperThroatQty3=@PaperThroatQty3,PaperWeight4=@PaperWeight4,PaperThroatQty4=@PaperThroatQty4,
         PaperWeight5=@PaperWeight5,PaperThroatQty5=@PaperThroatQty5,MethodID=@MethodID where ID=@ID
         RETURN