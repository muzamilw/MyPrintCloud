
create PROCEDURE [dbo].[sp_products_get_productbyid]
(@ProductID int)
AS
	SELECT tbl_profile.QtyPrefix,tbl_profile.ID,tbl_profile.TypeID,tbl_profile_type.ParentID as ParentTypeID,tbl_profile.Name,tbl_profile.PressID,
          tbl_profile.IsPlateSupplied, tbl_profile.IsFilmSupplied,tbl_profile.IsPressrestrictionApplied,tbl_profile.GuilotineID,tbl_profile.IsFirstTrim,tbl_profile.IsSecondTrim,tbl_profile.IsPaperSupplied,
         tbl_profile.IsWholePack,tbl_profile.WorkingSheetSizeID,tbl_profile.WorkingSheetSizeIsCustom,tbl_profile.WorkingSheetSizeHeight,
         tbl_profile.WorkingSheetSizeWidth,tbl_profile.ItemSheetSizeID,tbl_profile.ItemSheetSizeIsCustom,tbl_profile.ItemSheetSizeHeight,
         tbl_profile.ItemSheetSizeWidth,tbl_profile.IsItemGutterApplied,tbl_profile.ItemGutterHorizontal,tbl_profile.ItemGutterVertical,
         tbl_profile.IsDoubleSided,tbl_profile.IsWorknTurn,tbl_profile.IsWorknTunble,tbl_profile.IsMultipleQty,tbl_profile.IsRunonQty,
         tbl_profile.RunonQty,tbl_profile.Qty1,tbl_profile.Qty2,tbl_profile.Qty3,tbl_profile.BookletPages,tbl_profile.Side1Inks,
         tbl_profile.Side2Inks,tbl_profile.IsInkPrompted,tbl_profile.Description,tbl_profile.PadLeafQty,tbl_profile.NCRTopPaperID,
         tbl_profile.NCRBottomPaperID,tbl_profile.NCRMiddlePaperID,tbl_profile.NCRTotalParts,tbl_machines.maximumsheetwidth,tbl_machines.maximumsheetheight,tbl_machines.minimumsheetheight,tbl_machines.minimumsheetwidth,tbl_machines.MachineName AS PressName,
         tbl_Guilotine.MachineName AS GuilotineName,tbl_stockitems.ItemName AS PaperName,tbl_middleNcrPaper.ItemName as NcrMiddlePaper,
         tbl_topNcrPaper.ItemName as NcrTopPaper,tbl_bottomNcrPaper.ItemName as NcrBottomPaper, tbl_stockitems.ItemSizeCustom,tbl_stockitems.ItemSizeID,tbl_stockitems.ItemSizeHeight,tbl_stockitems.ItemSizeWidth,tbl_machines.ColourHeads,tbl_machines.IsMaxColorLimit,
         tbl_stockitems.ItemWeight,tbl_machine_cylinders.Circumference,tbl_machine_cylinders.Width,tbl_stockitems.RollStandards,tbl_stockitems.RollWidth,tbl_stockitems.RollLength
         FROM tbl_stockitems tbl_middleNcrPaper 
         RIGHT OUTER JOIN tbl_profile ON (tbl_middleNcrPaper.StockItemID = tbl_profile.NCRMiddlePaperID) 
         LEFT OUTER JOIN tbl_machines tbl_Guilotine ON (tbl_profile.GuilotineID = tbl_Guilotine.MachineID) 
         LEFT OUTER JOIN tbl_machines ON (tbl_profile.PressID = tbl_machines.MachineID) 
         LEFT OUTER JOIN tbl_machine_cylinders ON (tbl_machines.CylinderSizeID =tbl_machine_cylinders.ID) 
         LEFT OUTER JOIN tbl_stockitems ON (tbl_machines.DefaultPaperid = tbl_stockitems.StockItemID) 
         LEFT OUTER JOIN tbl_stockitems tbl_topNcrPaper ON (tbl_profile.NCRTopPaperID = tbl_topNcrPaper.StockItemID) 
         LEFT OUTER JOIN tbl_stockitems tbl_bottomNcrPaper ON (tbl_profile.NCRBottomPaperID = tbl_bottomNcrPaper.StockItemID) 
         LEFT OUTER JOIN tbl_profile_type ON (tbl_profile.TypeID = tbl_profile_type.ID)
         where tbl_profile.ID=@ProductID
	RETURN