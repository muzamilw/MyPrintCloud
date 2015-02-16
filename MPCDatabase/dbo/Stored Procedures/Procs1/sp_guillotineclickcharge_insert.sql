CREATE PROCEDURE dbo.sp_guillotineclickcharge_insert
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
         @PaperThroatQty5 float)
AS
insert into tbl_machine_guillotinecalc (MethodID,PaperWeight1,
         PaperThroatQty1,PaperWeight2,PaperThroatQty2,PaperWeight3,PaperThroatQty3,PaperWeight4,PaperThroatQty4,
         PaperWeight5,PaperThroatQty5) VALUES (
         @MethodID,@PaperWeight1,@PaperThroatQty1,@PaperWeight2,@PaperThroatQty2,@PaperWeight3, 
         @PaperThroatQty3,@PaperWeight4,@PaperThroatQty4,@PaperWeight5,@PaperThroatQty5);
         Select @@Identity as LookUpID
         RETURN