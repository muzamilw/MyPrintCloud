CREATE PROCEDURE dbo.sp_Compnay_Copy_Machines

	(
		@OldSiteID int,
		@NewSiteID int
	)

AS
	/* SET NOCOUNT ON */

	
			DECLARE @OldCylinderID int
			DECLARE @NewCylinderID int
			DECLARE Cyliner_Cursor CURSOR for SELECT ID FROM tbl_machine_cylinders WHERE  SystemSiteID=@OldSiteID
			
			
			OPEN Cyliner_Cursor
			
				FETCH NEXT FROM Cyliner_Cursor into @OldCylinderID
			
				WHILE @@FETCH_STATUS=0
					BEGIN
			
						INSERT INTO tbl_machine_cylinders 
						SELECT Name, Circumference, Width, @NewSiteID
						FROM tbl_machine_cylinders where ID=@OldCylinderID
						
						Select @NewCylinderID=@@Identity
						
						INSERT into #CylindersTempTable VALUES(@OldCylinderID,@NewCylinderID)
						
						FETCH NEXT FROM Cyliner_Cursor into @OldCylinderID
					END
			
						
						
						/*SELECT * from #CylindersTempTable*/
			
			CLOSE Cyliner_Cursor
			DEALLOCATE Cyliner_Cursor
						
		/* machine cylinders END */
		
		/* Machines */
		
			DECLARE @OldMachineID int
			DECLARE @NewMachineID int
			DECLARE Machine_Cursor CURSOR for SELECT MachineID FROM tbl_machines WHERE  SystemSiteID=@OldSiteID
			
			OPEN Machine_Cursor
			
				FETCH NEXT FROM Machine_Cursor into @OldMachineID
			
				WHILE @@FETCH_STATUS=0
					BEGIN
			
						INSERT INTO tbl_machines 
						SELECT MachineName, MachineCatID, ColourHeads, isPerfecting, SetupCharge, WashupPrice, WashupCost, MinInkDuctqty, worknturncharge, MakeReadyCost, 
						IsNull((Select [NewID] from #TempTableInventory where OldID=tbl_machines.DefaultFilmid),0),
						IsNull((Select [NewID] from #TempTableInventory where OldID=tbl_machines.DefaultPlateid),0),
						IsNull((Select [NewID] from #TempTableInventory where OldID=tbl_machines.DefaultPaperid),0),
						isfilmused, isplateused, ismakereadyused, iswashupused, maximumsheetweight, maximumsheetheight, 
						maximumsheetwidth, minimumsheetheight, minimumsheetwidth, gripdepth, gripsideorientaion, gutterdepth, headdepth, 
						IsNull((Select [New_ID] from #MarkUpTempTable where Old_ID=tbl_machines.Va),0), 
						PressSizeRatio, 
						Description, Priority, DirectCost, MinimumCharge, CostPerCut, PricePerCut, IsAdditionalOption, IsDisabled, LockedBy, 
						IsNull((Select NewCID from #CylindersTempTable where OldCID=tbl_machines.CylinderSizeID),0), 
						MaxItemAcrossCylinder, Web1MRCost, Web1MRPrice, Web2MRCost, Web2MRPrice, ReelMRCost, ReelMRPrice, IsMaxColorLimit, PressUtilization, 
						MakeReadyPrice, InkChargeForUniqueColors, 0, FlagID, IsScheduleable, @NewSiteID, SpoilageType, SetupTime, TimePerCut, 
						MakeReadyTime, WashupTime, ReelMakereadyTime
						FROM tbl_machines WHERE  MachineID=@OldMachineID
						
						Select @NewMachineID=@@Identity

						--Copy Machine Spoilages
						Insert into tbl_machine_spoilage SELECT     @NewMachineID, SetupSpoilage, RunningSpoilage, NoOfColors
						FROM  tbl_machine_spoilage where MachineID=@OldMachineID
				
						-- Copy if machine is guilotine into ptv
						insert into tbl_machine_guilotine_ptv SELECT     NoofSections, NoofUps, Noofcutswithoutgutters, Noofcutswithgutters, @NewMachineID
						FROM         tbl_machine_guilotine_ptv where GuilotineID=@OldMachineID
						
						--Copy Machine Ink Coverage
						insert into tbl_machine_ink_coverage SELECT IsNull((Select [NewID] from #TempTableInventory where OldID=tbl_machine_ink_coverage.SideInkOrder),0), IsNull((Select [New_ID] from #InkCoverageTempTable where Old_ID=tbl_machine_ink_coverage.SideInkOrderCoverage),0),@NewMachineID 
						FROM tbl_machine_ink_coverage where MachineID=@OldMachineID
						
						--Copy Machine Lookup Methods
						insert into tbl_machine_lookup_methods SELECT     @NewMachineID, IsNull((Select NewMethodID from #LookUpMethodsTable where OldMethodID=tbl_machine_lookup_methods.MethodID),0), DefaultMethod
						FROM         tbl_machine_lookup_methods where MachineID=@OldMachineID
						
						
						INSERT into #MachinesTempTable VALUES(@OldMachineID,@NewMachineID)
										
						FETCH NEXT FROM Machine_Cursor into @OldMachineID
					END
																			
				CLOSE Machine_Cursor
				DEALLOCATE Machine_Cursor
		
	RETURN