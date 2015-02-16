CREATE PROCEDURE dbo.sp_speedweight_insert
(
         @MethodID int,
         @SheetsQty1 int,
         @SheetsQty2 int,
         @SheetsQty3 int,
         @SheetsQty4 int,
         @SheetsQty5 int,
         @SheetWeight1 float,
         @speedqty11 int,
         @speedqty12 int, 
         @speedqty13 int,
         @speedqty14 int,
         @speedqty15 int,
         @SheetWeight2 float,
         @speedqty21 int,
         @speedqty22 int,
         @speedqty23 int,
         @speedqty24 int,
         @speedqty25 int,
         @SheetWeight3 float,
         @speedqty31 int,
         @speedqty32 int,
         @speedqty33 int,
         @speedqty34 int,
         @speedqty35 int,
         @hourlyCost float,
         @hourlyPrice float)
         AS
insert into tbl_machine_speedweightlookup (MethodID,SheetsQty1,SheetsQty2,
         SheetsQty3,SheetsQty4,SheetsQty5,SheetWeight1,speedqty11,speedqty12,speedqty13,speedqty14,speedqty15,
         SheetWeight2,speedqty21,speedqty22,speedqty23,speedqty24,speedqty25,SheetWeight3,speedqty31,speedqty32,
         speedqty33,speedqty34,speedqty35,hourlyCost,hourlyPrice) VALUES (
         @MethodID,@SheetsQty1,@SheetsQty2,@SheetsQty3,@SheetsQty4,@SheetsQty5,@SheetWeight1,@speedqty11,@speedqty12, 
         @speedqty13,@speedqty14,@speedqty15,@SheetWeight2,@speedqty21,@speedqty22,@speedqty23,@speedqty24,@speedqty25,
         @SheetWeight3,@speedqty31,@speedqty32,@speedqty33,@speedqty34,@speedqty35,@hourlyCost,@hourlyPrice);
         Select @@Identity as LookUpID
             RETURN