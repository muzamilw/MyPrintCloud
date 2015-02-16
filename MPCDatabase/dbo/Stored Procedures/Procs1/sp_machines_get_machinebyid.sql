CREATE PROCEDURE [dbo].[sp_machines_get_machinebyid]
(@MachineID int)
AS
	select MachineID, MachineName, MachineCatID, ColourHeads, Isnull(isPerfecting,0) as isPerfecting, SetupCharge, WashupPrice, 
	WashupCost, MinInkDuctqty, worknturncharge, MakeReadyCost, Isnull(DefaultFilmid,0) as DefaultFilmid, Isnull(DefaultPlateid,0) as DefaultPlateid,
	 DefaultPaperid, Isnull(isfilmused,0) as isfilmused, Isnull(isplateused,0) as isplateused, isnull(ismakereadyused,0) as ismakereadyused,
	  Isnull(iswashupused,0) as iswashupused, maximumsheetweight, 
	 maximumsheetheight, maximumsheetwidth, minimumsheetheight, minimumsheetwidth, gripdepth, 
	 gripsideorientaion, gutterdepth, headdepth, Va, PressSizeRatio, Description, Priority, 
	 Isnull(DirectCost,0) as DirectCost, MinimumCharge, CostPerCut, PricePerCut, Isnull(IsAdditionalOption,0) as IsAdditionalOption, 
	 IsDisabled, LockedBy, 
	 Isnull(CylinderSizeID,0) as CylinderSizeID, Isnull(MaxItemAcrossCylinder,0) as MaxItemAcrossCylinder, Isnull(Web1MRCost,0) as Web1MRCost,
	  Isnull(Web1MRPrice,0) as Web1MRPrice,
	 Isnull(Web2MRCost,0) as Web2MRCost, Isnull(Web2MRPrice,0) as Web2MRPrice,
	  Isnull(ReelMRCost,0) as ReelMRCost, ReelMRPrice, Isnull(IsMaxColorLimit,0) as IsMaxColorLimit, PressUtilization, MakeReadyPrice, 
	  InkChargeForUniqueColors, CompanyID, Isnull(FlagID,0) as FlagID, IsScheduleable, SystemSiteID, SpoilageType, 
	  SetupTime, TimePerCut, MakeReadyTime, WashupTime, Isnull(ReelMakereadyTime,0) as ReelMakereadyTime 
	from tbl_machines  where MachineID=@MachineID
        RETURN