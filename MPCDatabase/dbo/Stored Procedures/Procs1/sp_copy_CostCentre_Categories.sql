CREATE PROCEDURE dbo.sp_copy_CostCentre_Categories
(
@OldCompanyID int,
@NewCompamyID int,
@CopyOnlySystemDefined bit
)
                  AS

	Declare @OldCatID int
	Declare @NewCatID int
	Declare @CopyString varchar(1000)
	if @CopyOnlySystemDefined = 0
		BEGIN
			set 	@CopyString = 'Declare CostCentreCategory Cursor for Select TypeID from tbl_costcentretypes where CompanyID=' + Convert(varchar(100),@OldCompanyID)
		END	
	else
		BEGIN
			set 	@CopyString = 'Declare CostCentreCategory Cursor for Select TypeID from tbl_costcentretypes where IsSystem = 1 and CompanyID=' + Convert(varchar(100),@OldCompanyID) 
		END
			
	exec(@CopyString)
	
			Open CostCentreCategory
				FETCH NEXT FROM CostCentreCategory into @OldCatID
								While @@FETCH_STATUS = 0 
									BEGIN
										Insert into tbl_costcentretypes SELECT     TypeName, IsSystem, IsExternal, @NewCompamyID
																		FROM         tbl_costcentretypes Where TypeID=@OldCatID
										
										Select @NewCatID=@@Identity 
										
										Insert into  #CostCentreCategories Values (@OldCatID,@NewCatID)
														
										FETCH NEXT FROM CostCentreCategory into @OldCatID
									END
						Close CostCentreCategory
						Deallocate CostCentreCategory
 return