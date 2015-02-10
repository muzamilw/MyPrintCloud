CREATE PROCEDURE dbo.sp_speedweight_get_bymethodid
(
         @MethodID int)
                  AS
select ID,MethodID,hourlyCost,hourlyPrice,SheetsQty1,SheetsQty2,
         SheetsQty3,SheetsQty4,SheetsQty5,SheetWeight1,speedqty11,speedqty12,speedqty13,speedqty14,speedqty15,
        SheetWeight2,speedqty21,speedqty22,speedqty23,speedqty24,speedqty25,SheetWeight3,speedqty31,speedqty32,
        speedqty33,speedqty34,speedqty35 from tbl_machine_speedweightlookup where MethodID=@MethodID
             RETURN