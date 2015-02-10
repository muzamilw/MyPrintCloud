CREATE PROCEDURE dbo.sp_lookupmethod_Copy
(@MethodID int)
AS

Declare @ID int
Declare @Type int

	Insert into tbl_lookup_methods SELECT     'Copy of ' + Name, Type, LockedBy, CompanyID, FlagID, SystemSiteID
	FROM         tbl_lookup_methods  where MethodID=@MethodID
	
	Select @ID=@@Identity,@Type=Type from tbl_lookup_methods  where MethodID=@MethodID
	
	If @Type = 1 --Click charge
		Begin
			insert into tbl_machine_clickchargelookup SELECT     @ID, SheetCost, Sheets, SheetPrice, TimePerHour
			FROM         tbl_machine_clickchargelookup where MethodID=@MethodID
		End
	Else if @Type =2 --Speed Weight
		Begin
			insert into tbl_machine_speedweightlookup SELECT     @ID, SheetsQty1, SheetsQty2, SheetsQty3, SheetsQty4, SheetsQty5, SheetWeight1, speedqty11, speedqty12, speedqty13, speedqty14, 
						speedqty15, SheetWeight2, speedqty21, speedqty22, speedqty23, speedqty24, speedqty25, SheetWeight3, speedqty31, speedqty32, speedqty33, 
						speedqty34, speedqty35, hourlyCost, hourlyPrice
						FROM         tbl_machine_speedweightlookup where MethodID=@MethodID
		End
	Else if @Type = 3 --Per Hour
		Begin
		insert into tbl_machine_perhourlookup	SELECT     @ID, SpeedCost, Speed, SpeedPrice
						FROM         tbl_machine_perhourlookup where MethodID=@MethodID
		End
	Else if @Type = 4 --Click Charge Zone
		Begin
		insert into tbl_machine_clickchargezone SELECT     @ID, From1, To1, Sheets1, SheetCost1, SheetPrice1, From2, To2, Sheets2, SheetCost2, SheetPrice2, From3, To3, Sheets3, SheetCost3, 
                      SheetPrice3, From4, To4, Sheets4, SheetCost4, SheetPrice4, From5, To5, Sheets5, SheetCost5, SheetPrice5, From6, To6, Sheets6, SheetCost6, 
                      SheetPrice6, From7, To7, Sheets7, SheetCost7, SheetPrice7, From8, To8, Sheets8, SheetCost8, SheetPrice8, From9, To9, Sheets9, SheetCost9, 
                      SheetPrice9, From10, To10, Sheets10, SheetCost10, SheetPrice10, From11, To11, Sheets11, SheetCost11, SheetPrice11, From12, To12, Sheets12, 
                      SheetCost12, SheetPrice12, From13, To13, Sheets13, SheetCost13, SheetPrice13, From14, To14, Sheets14, SheetCost14, SheetPrice14, From15, To15, 
                      Sheets15, SheetCost15, SheetPrice15, isaccumulativecharge, IsRoundUp, TimePerHour
						FROM         tbl_machine_clickchargezone where MethodID=@MethodID
		End
	Else if @Type = 5 --Guilotine Click charge
		Begin
		Declare @GID int
		insert into tbl_machine_guillotinecalc	SELECT     @ID, PaperWeight1, PaperThroatQty1, PaperWeight2, PaperThroatQty2, PaperWeight3, PaperThroatQty3, PaperWeight4, PaperThroatQty4, 
                     PaperWeight5, PaperThroatQty5	FROM         tbl_machine_guillotinecalc where MethodID=@MethodID
						
		Select @ID = @@Identity
		
		Select @GID=ID from tbl_machine_guillotinecalc where MethodID=@MethodID
		
		
						
		insert into tbl_machine_guilotine_ptv SELECT     NoofSections, NoofUps, Noofcutswithoutgutters, Noofcutswithgutters, @ID
					FROM         tbl_machine_guilotine_ptv where GuilotineID=@GID
		End
	Else if @Type = 6 --Meter per hour
		Begin
			Insert into tbl_machine_meterperhourlookup SELECT     @ID, SheetsQty1, SheetsQty2, SheetsQty3, SheetsQty4, SheetsQty5, SheetWeight1, speedqty11, speedqty12, speedqty13, speedqty14, 
                      speedqty15, SheetWeight2, speedqty21, speedqty22, speedqty23, speedqty24, speedqty25, SheetWeight3, speedqty31, speedqty32, speedqty33, 
                      speedqty34, speedqty35, hourlyCost, hourlyPrice
					  FROM         tbl_machine_meterperhourlookup where MethodID=@MethodID
		End
	
	
 RETURN