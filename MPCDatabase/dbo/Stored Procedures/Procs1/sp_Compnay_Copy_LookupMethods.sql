CREATE PROCEDURE dbo.sp_Compnay_Copy_LookupMethods

	(
		@OldSiteID int,
		@NewSiteID int
	)

AS
	/* SET NOCOUNT ON */

	
			DECLARE LookUp_Cursor CURSOR FOR SELECT Type ,MethodID FROM tbl_lookup_methods where SystemSiteID=@OldSiteID

			declare @TypeVar int
			declare @MethodID int
			declare @NewMethodID int

			OPEN LookUp_Cursor

			FETCH NEXT FROM LookUp_Cursor into @TypeVar,@MethodID
			WHILE @@FETCH_STATUS = 0
			BEGIN

				Insert Into tbl_lookup_methods SELECT  Name, Type, LockedBy, CompanyID, FlagID, @NewSiteID FROM tbl_lookup_methods where MethodID=@MethodID
					
				select @NewMethodID=@@Identity
					
						
				IF (@TypeVar = 1)
				
					Insert Into tbl_machine_clickchargelookup SELECT @NewMethodID, SheetCost, Sheets, SheetPrice, TimePerHour FROM tbl_machine_clickchargelookup  where MethodID=@MethodID
					
				IF (@TypeVar = 2)
					
					Insert Into tbl_machine_speedweightlookup SELECT @NewMethodID, SheetsQty1, SheetsQty2 , SheetsQty3 , SheetsQty4 , SheetsQty5 , SheetWeight1, speedqty11 , speedqty12 , speedqty13 , speedqty14 , speedqty15 , 
								SheetWeight2 , speedqty21 , speedqty22 , speedqty23 , speedqty24 , speedqty25 , SheetWeight3 , speedqty31 , speedqty32 , speedqty33 , speedqty34 , speedqty35, 
								hourlyCost, hourlyPrice FROM tbl_machine_speedweightlookup  where MethodID=@MethodID
				
				IF (@TypeVar = 3)
					
					Insert Into tbl_machine_perhourlookup SELECT @NewMethodID, SpeedCost, Speed, SpeedPrice FROM tbl_machine_perhourlookup where MethodID=@MethodID
				
				IF (@TypeVar = 4)
					
					Insert Into tbl_machine_clickchargezone SELECT     @NewMethodID, From1, To1, Sheets1, SheetCost1, SheetPrice1, From2, To2, Sheets2, SheetCost2, SheetPrice2, From3, To3, Sheets3, SheetCost3, 
								SheetPrice3, From4, To4, Sheets4, SheetCost4, SheetPrice4, From5, To5, Sheets5, SheetCost5, SheetPrice5, From6, To6, Sheets6, SheetCost6, 
								SheetPrice6, From7, To7, Sheets7, SheetCost7, SheetPrice7, From8, To8, Sheets8, SheetCost8, SheetPrice8, From9, To9, Sheets9, SheetCost9, 
								SheetPrice9, From10, To10, Sheets10, SheetCost10, SheetPrice10, From11, To11, Sheets11, SheetCost11, SheetPrice11, From12, To12, Sheets12, 
								SheetCost12, SheetPrice12, From13, To13, Sheets13, SheetCost13, SheetPrice13, From14, To14, Sheets14, SheetCost14, SheetPrice14, From15, To15, 
								Sheets15, SheetCost15, SheetPrice15, isaccumulativecharge, IsRoundUp, TimePerHour
								FROM tbl_machine_clickchargezone  where MethodID=@MethodID
				
				IF (@TypeVar = 5)
					
					Insert Into tbl_machine_guillotinecalc SELECT     @NewMethodID, PaperWeight1, PaperThroatQty1, PaperWeight2, PaperThroatQty2, PaperWeight3, PaperThroatQty3, PaperWeight4, PaperThroatQty4, 
								PaperWeight5, PaperThroatQty5 FROM tbl_machine_guillotinecalc where MethodID=@MethodID
				
				IF (@TypeVar = 6)
					
					Insert Into tbl_machine_meterperhourlookup SELECT    @NewMethodID, SheetsQty1, SheetsQty2, SheetsQty3, SheetsQty4, SheetsQty5, SheetWeight1, speedqty11, speedqty12, speedqty13, speedqty14, 
								speedqty15, SheetWeight2, speedqty21, speedqty22, speedqty23, speedqty24, speedqty25, SheetWeight3, speedqty31, speedqty32, speedqty33, 
								speedqty34, speedqty35, hourlyCost, hourlyPrice
								FROM tbl_machine_meterperhourlookup  where MethodID=@MethodID
			  
			INSERT INTO #LookUpMethodsTable VALUES(@MethodID,@NewMethodID)
			    
				FETCH NEXT FROM LookUp_Cursor into @TypeVar,@MethodID
			END

			CLOSE LookUp_Cursor
			DEALLOCATE LookUp_Cursor
	RETURN