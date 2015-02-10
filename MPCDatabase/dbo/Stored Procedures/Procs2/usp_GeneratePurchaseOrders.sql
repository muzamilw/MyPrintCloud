


CREATE PROCEDURE [dbo].[usp_GeneratePurchaseOrders]
@OrderID as int,
@CreatedBy as int,
@TaxID as int
AS
BEGIN
	
	SET NOCOUNT OFF
	DECLARE @Err int
	
    Declare 
         @ItemsCount as int,
         @Counter as int,
		 @RefItemID as int,
		 @ItemQty as int,
		 @IsExternal as bit,
		 @IsQtyLimit as bit,
		 @AutoCreateUnPostedPO as bit,
		 @QtyLimit as int,
		 @SupplierID as int,
		 @ItemID as int,
		 @SupplierName as varchar(50),
		 @SupplierAdressID as int,
		 @SupplierContactID as int,
		 @ContactCompanyID as int,
		 @Code as varchar(50),
		 @ProductCategoryID as int,
		 @ProductCompleteName as varchar(100),
         @ItemType as int,
         @MarkupVaue as float,
         @NetTotal as float,         
		@POPrefix as varchar(10),
		@POStart as bigint,
		@PONext as varchar(20),
		@NewPO as bigint,
		@UserNotes as nvarchar(2000)

    DECLARE @ITEMS AS TABLE(
     ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     ItemID int,
     RefItemID INT,
     Qty1 int,
     Qty1GrossTotal float,
     ProductName varchar(150),
     ItemCode varchar(50),
     ProductCategoryID INT,
     Qty1Tax1Value float,
     ItemType int,
     Qty1MarkUp1Value float
     )
     
     DECLARE @ITEMDETAIL AS TABLE(
     ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     IsInternalActivity bit,
     IsQtyLimit bit,
     QtyLimit int,
     ItemID int,
     isAutoCreateSupplierPO bit)	
     
     --Declare @SupplierIDs as table(
     --ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     --SupplierID int)
     
    declare @SupplierIDs table
    (
       SupplierID int,
       PurchaseID int,
       LTotalPrice float,
       LTotalTax float,
       LGrandTotal float,
       LNetTotal float
																				   
    )
      declare @LastTotalPrice as float,
		      @LastDiscount as float,
			  @LastTotalTax as float,
		      @LastGrandTotal as float,
			  @LastNetTotal as float															
     
    INSERT INTO @ITEMS  --- insert items in temp table
	SELECT ItemID,RefItemID, Qty1, Qty1GrossTotal,ProductName,ItemCode,ProductCategoryID,Qty1Tax1Value,ItemType,Qty1MarkUp1Value
				FROM tbl_items 
				WHERE EstimateID = @OrderID
    select @UserNotes = usernotes from tbl_estimates where EstimateID = @OrderID			
	-- getting count of items
	select @ItemsCount = count(*) from tbl_items where EstimateID = @OrderID
									
	
	declare @Qty1GrossTotal as float,
	        @Qty1 as int,
	        @ItemCode as varchar(50),
	        @ProductName as varchar(100),
	        @CategoryName as Varchar(100),
	        @Qty1Tax1Value as float,
	        @ProductDetailCount as int,
	        @SupplierTableCount as int,
	        @PurchaseID as int,
	        @PurchaseDetailID as int,
	        @StockItemID1 as int,
	        @Sequence as int,
	        @PricePaperType as int,
	        @SystemSiteID as int,
			@TaxIDGS as int,
	        @NetTax as float
	SET @Counter = 1
    
    -- loop start for each item
    WHILE (@Counter <= @ItemsCount)
    BEGIN
    	
    	            -- getting ref item id of particular item
					SELECT @RefItemID = RefItemID FROM @ITEMS WHERE ROWID = @Counter
					
					-- setting variables/fields from particular item
					Select @ItemID = ItemID,
								@RefItemID = RefItemID,	  
									   @Qty1 = Qty1,
									   @ItemCode = ItemCode,
									   @ProductName = ProductName,
									   @ProductCategoryID = ProductCategoryID,
									   @ItemType = ItemType
									    --@Qty1GrossTotal = Qty1GrossTotal,
									   --@Qty1Tax1Value = Qty1Tax1Value,
									  -- @MarkupVaue = Qty1MarkUp1Value
								from @ITEMS where ROWID = @Counter

					-- if item is stoct
					if (@ItemType = 3) -- stock
					   begin
					       -- select supplierand name   of particular stock item
						   select @SupplierID = (Select TOP 1 isnull(SupplierID,0) from tbl_stockitems where StockItemID = @RefItemID),
						          @ProductCompleteName = (Select TOP 1 isnull(ItemName,'') from tbl_stockitems where StockItemID = @RefItemID)
						          
					       if (@SupplierID > 0)
					           begin    
					              -- getting price of stock item	
								   Select @Qty1GrossTotal = (select top 1 isnull(CostPrice,0) from tbl_stock_cost_and_price where ItemID = @RefItemID and CostOrPriceIdentifier = 0)
                                   if @Qty1GrossTotal is null
                                      begin
                                         select @Qty1GrossTotal = 0
                                      end
							  
							  end
					   end
					else if (@ItemType = 2)
					   begin
					   
					     -- select supplier and name   of particular cost center item
							select @SupplierID = (Select TOP 1 isnull(PreferredSupplierID,0) from tbl_costcentres where CostCentreID = @RefItemID),
							   @ProductCompleteName = (Select TOP 1 isnull(Name,'') from tbl_costcentres where CostCentreID = @RefItemID)
						      if (@SupplierID > 0)
					           begin    	
					           
					                -- getting price of cost center
									declare @CostPrice as float
									select @CostPrice = CostPerUnitQuantity from tbl_costcentres where CostCentreID = @RefItemID
									select @Qty1GrossTotal = @CostPrice / @Qty1
								    if @Qty1GrossTotal is null
                                      begin
                                         select @Qty1GrossTotal = 0
                                      end
							  end
					   end
					else		
					  begin			 
                                -- getting product name
								select @CategoryName = CategoryName from tbl_ProductCategory where ProductCategoryID = @ProductCategoryID
								
								-- making complete name
								select @ProductCompleteName = @CategoryName + ' ' + @ProductName
								
								-- getting item qty
								SELECT @ItemQty = isnull(Qty1,0) FROM @ITEMS WHERE ROWID = @Counter
									
							    -- insert item detail in temp table				
								INSERT INTO @ITEMDETAIL 
										SELECT top 1 isInternalActivity,isQtyLimit,QtyLimit, ItemID, isAutoCreateSupplierPO
										FROM tbl_items_ProductDetails 
										WHERE ItemID = @RefItemID
										
										
										
								SET @ProductDetailCount = ISNULL(@@ROWCOUNT, 0)
								
								-- check product detail count is greater then 1 or not
								if (@ProductDetailCount > 0)
								  begin
									if (@MarkupVaue > 0)
										begin
										   select @MarkupVaue = 0
										end	
										
									
									-- checking is internal flag
									Select @IsExternal = isInternalActivity from @ITEMDETAIL where ItemID = @RefItemID
									if (@IsExternal = 1)
									  begin
										 select * from tbl_items where itemid = @itemid
									  end
									else
									  begin
									        -- check isAutoCreateSupplierPO is On or off
									        select @AutoCreateUnPostedPO =  isAutoCreateSupplierPO from @ITEMDETAIL where itemid = @RefItemID
									        
									        if (@AutoCreateUnPostedPO = 1)
									           begin
														-- check is qty limit flag
														select @IsQtyLimit = IsQtyLimit from  @ITEMDETAIL where ItemID = @RefItemID
														
													
														-- getting qty limit
														select @QtyLimit = isnull(QtyLimit,0) from @ITEMDETAIL where ItemID = @RefItemID
														
														-- getting supplier id for item
														select @SupplierID = (Select TOP 1 isnull(SupplierID,0) from tbl_items_PriceMatrix where ItemID = @RefItemID and SupplierSequence = 1)
							 
													  --  if (@SupplierID is null)
															--select @SupplierID = (Select TOP 1 isnull(SupplierID,0) from tbl_items_PriceMatrix where ItemID = @RefItemID and SupplierSequence = 2)
														
														-- if is qty limit is true
														if(@IsQtyLimit = 1)
														  begin
																-- if order qty is greater than qty limit
																if (@ItemQty > @QtyLimit)
																   begin
																	  select @SupplierID = (Select TOP 1 isnull(SupplierID,0) from tbl_items_PriceMatrix where ItemID = @RefItemID and SupplierSequence = 1)
																	  if (@SupplierID is null)
																		select @SupplierID = (Select TOP 1 isnull(SupplierID,0) from tbl_items_PriceMatrix where ItemID = @RefItemID and SupplierSequence = 2)
																	  
																	 -- insert into @SupplierIDs
															
																	  
																	end
																 else --  else part of qty conditions
																   begin
																	  select @SupplierID = (Select TOP 1 isnull(SupplierID,0) from tbl_items_PriceMatrix where ItemID = @RefItemID and SupplierSequence = 2)
																	   if (@SupplierID is null)
																		select @SupplierID = (Select TOP 1 isnull(SupplierID,0) from tbl_items_PriceMatrix where ItemID = @RefItemID and SupplierSequence = 1)
														               
																   end
																 
													 					
														  end
											  
														 if (@SupplierID > 0)
															begin  
																	select @StockItemID1 = (select top 1 StockItemID1 from tbl_item_sections where itemid = @ItemID and sectionno = 1)
																   
																   
																	 select @Sequence = (select top 1 OptionSequence from tbl_ItemStockOptions where itemid = @RefItemID and StockID = @StockItemID1 and contactcompanyid is null)
																   
																	if (@Sequence = 1)
																	   begin
																		 select @Qty1GrossTotal = (select top 1 PricePaperType1 from tbl_items_PriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	   end
																	else if (@Sequence = 2)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PricePaperType2 from tbl_items_PriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	else if (@Sequence = 3)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PricePaperType3 from tbl_items_PriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	else if (@Sequence = 4)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType4 from tbl_items_PriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	else if (@Sequence = 5)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType5 from tbl_items_PriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	else if (@Sequence = 6)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType6 from tbl_items_PriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	else if (@Sequence = 7)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType7 from tbl_items_PriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	 else if (@Sequence = 8)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType8 from tbl_items_PriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	else if (@Sequence = 9)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType9 from tbl_items_PriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	   else if (@Sequence = 10)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType10 from tbl_items_PriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	else if (@Sequence = 11)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType11 from tbl_items_PriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
													    
														  end -- if supplier > 0 end
											   end -- isAutoPOCreate end
										end	 -- is external end
								  end -- product detail count 
								  
						end -- item end
											  if (@SupplierID > 0)
													   begin
													   
													    			   
																 select @TaxIDGS =  StateTaxID from tbl_company_sites where companyid =  2
																 declare @TaxPercentage as float
																select @TaxPercentage = Tax1 from tbl_taxrate where taxid = @TaxIDGS
																		   
																select @NetTax = (@Qty1GrossTotal * @TaxPercentage) / 100
																		   					   
																select @Qty1Tax1Value = 0
																--select @NetTotal = @Qty1GrossTotal + @NetTax
																			
																  IF EXISTS(SELECT SupplierID From @SupplierIDs where SupplierID = @SupplierID)
																	begin
																		--SET @PurchaseID = (SELECT SCOPE_IDENTITY());
																		
																		declare @TotalPrice as float,
																				@Discount as float,
																				@TotalTax as float,
																				@GrandTotal as float,
																				@AgainNetTotal as float,
																                @FootNotes as varchar(2000),
																                @SystemUserName as varchar(500),
																                @LastPurchaseID as int,
																                 @LTotalPrice as float,
																                  @LTotalTax as float,
																                  @LGrandTotal as float,
																                   @LNetTotal as float
																                
																                select @LastPurchaseID = PurchaseID,
																                       @LTotalPrice = LTotalPrice,
																                       @LTotalTax = LTotalTax,
																                        @LGrandTotal = LGrandTotal,
																                        @LNetTotal = LNetTotal
									
																                
																                 from @SupplierIDs where SupplierID = @SupplierID
																                
																		 -- select @TotalPrice =   
																		--select @TotalPrice = @LastTotalPrice + @Qty1GrossTotal,
																		--	   --@Discount = @LastDiscount + @MarkupVaue,
																		--	   @TotalTax = @LastTotalTax + @NetTax,
																		--	  -- @TotalTax = null,
																		--	   @GrandTotal = @LastGrandTotal + @Qty1GrossTotal,
																		--	   @AgainNetTotal = @LastNetTotal + @Qty1GrossTotal + @NetTax
																			   
																			   
																			   	select @TotalPrice = @LTotalPrice + @Qty1GrossTotal,
																			   --@Discount = @LastDiscount + @MarkupVaue,
																			   @TotalTax = @LTotalTax + @NetTax,
																			  -- @TotalTax = null,
																			   @GrandTotal = @LGrandTotal + @Qty1GrossTotal,
																			   @AgainNetTotal = @LNetTotal + @Qty1GrossTotal + @NetTax
																               
																		--insert into tbl_purchasedetail (ItemID,quantity,PurchaseID,price,TotalPrice,packqty,ItemCode,
																		--	ItemName, Discount, NetTax, freeitems,ItemBalance, TaxID,ServiceDetail) values 
																		--	(@ItemID,0,@PurchaseID, @Qty1GrossTotal, @Qty1GrossTotal, @Qty1, @ItemCode, @ProductName,@MarkupVaue,@Qty1Tax1Value,0,0,@TaxID,@ProductCompleteName)
															                
															                
																			insert into tbl_purchasedetail (ItemID,quantity,PurchaseID,price,TotalPrice,packqty,ItemCode,
																			ItemName, freeitems,ItemBalance, TaxID,ServiceDetail,NetTax) values 
																			(@ItemID,1,@LastPurchaseID, @Qty1GrossTotal, @Qty1GrossTotal, @Qty1, @ItemCode, @ProductName,0,0,@TaxID,@ProductCompleteName,@NetTax)
																		 set @PurchaseDetailID = (SELECT SCOPE_IDENTITY());
																	     
																	    
																		update tbl_purchase
																		set TotalPrice = @TotalPrice,
																			--Discount = @Discount,
																			TotalTax = @TotalTax,
																			GrandTotal = @GrandTotal,
																			NetTotal = @AgainNetTotal where PurchaseID = @LastPurchaseID
																	        
																	   --select @LastTotalPrice = @TotalPrice,
																			 -- --@LastDiscount =  @Discount,
																			 -- @LastTotalTax =  @TotalTax,
																			 -- @LastGrandTotal = @GrandTotal,
																			 -- @LastNetTotal = @AgainNetTotal
																			 
																			 
																	
																			 
																			 	insert into @SupplierIDs values (@SupplierID,@LastPurchaseID,@TotalPrice,@TotalTax,@GrandTotal,@AgainNetTotal)
																		-- total price
																		-- discount
																		-- total tax 
																		-- grand total
																		-- net total
																	     
																	 end
																   else
																	 begin
																		 --- 1p and 1pd create
																		 
																		  select @SupplierName = Name,
																				  @ContactCompanyID = ContactCompanyID
																			from tbl_contactcompanies where ContactCompanyID = @SupplierID
																          
																		   SELECT @SupplierContactID = ContactID,
																				  @SupplierAdressID = AddressID
																		   FROM   tbl_contacts where contactcompanyID = @ContactCompanyID 
																		   
																		   --Naveed Commenting to test the change -- comment starts--																	        
																		   --select @POPrefix = POPrefix,
																				 -- @POStart  = POStart,
																				 -- @PONext = PONext
																		   --from tbl_prefixes where SystemSiteID = 1
																		   
																		   --set @Code = @POPrefix + '-001-' + @PONext
																		   
																		   --set @NewPO = @PONext + 1
																		   
																		   
																		   --Update tbl_prefixes set PONext = @NewPO where SystemSiteID = 1	
																		   		-------mnz comments end----	  
																				
																				--New line added mnz---
																				select @Code = (select top 1 POPrefix + '-001-'+ cast(PONext as varchar) from tbl_prefixes)
																				------ 
																		 --	 select @TaxIDGS =  StateTaxID from tbl_company_sites where companyid =  2
																		 --  declare @TaxPercentage as float
																		 --  select @TaxPercentage = Tax1 from tbl_taxrate where taxid = @TaxIDGS
																		   
																		 --  select @NetTax = (@Qty1GrossTotal * @TaxPercentage) / 100
																		   					   
																		 --  select @Qty1Tax1Value = 0
																			select @NetTotal = @Qty1GrossTotal + @NetTax
																		    
																		   
																		   --insert into tbl_purchase (date_Purchase,TotalPrice,isproduct,Status,Discount,TotalTax,GrandTotal,NetTotal,
																		   --CreatedBy,SupplierID,Code,SupplierContactCompany,SupplierContactAddressID,ContactID) values (GetDate(), @Qty1GrossTotal, 2, 31, @MarkupVaue, @Qty1Tax1Value, @Qty1GrossTotal, @NetTotal, @CreatedBy, @SupplierID,@Code,@SupplierName,
																		   --@SupplierAdressID,@SupplierContactID)  
																		   select @SystemSiteID = CompanySiteID from tbl_company_sites
																	       
																		  
																		   
																           select @FootNotes =  isnull(FootNotes,'') from tbl_report_notes where reportcategoryid = 5 and isdefault = 1 
																		  
																		   
																		   insert into tbl_purchase (date_Purchase,TotalPrice,isproduct,Status,GrandTotal,NetTotal,
																		   CreatedBy,SupplierID,Code,SupplierContactCompany,SupplierContactAddressID,ContactID,SystemSiteID,FootNote,TotalTax, UserNotes) values (GetDate(), @Qty1GrossTotal, 2, 31,  @Qty1GrossTotal, @NetTotal, @CreatedBy, @SupplierID,@Code,@SupplierName,
																		   @SupplierAdressID,@SupplierContactID,@SystemSiteID, @FootNotes,@NetTax, @UserNotes)  
																           
																			SET @PurchaseID = (SELECT SCOPE_IDENTITY());
																            
																			
																			--insert into tbl_purchasedetail (ItemID,quantity,PurchaseID,price,TotalPrice,packqty,ItemCode,
																			--ItemName, Discount, NetTax, freeitems,ItemBalance, TaxID,ServiceDetail) values 
																			--(@ItemID,0,@PurchaseID, @Qty1GrossTotal, @Qty1GrossTotal, @Qty1, @ItemCode, @ProductName,@MarkupVaue,@Qty1Tax1Value,0,0,@TaxID,@ProductCompleteName)
															            
																		   insert into tbl_purchasedetail (ItemID,quantity,PurchaseID,price,TotalPrice,packqty,ItemCode,
																			ItemName,  freeitems,ItemBalance, TaxID,ServiceDetail,NetTax) values 
																			(@ItemID,1,@PurchaseID, @Qty1GrossTotal, @Qty1GrossTotal, @Qty1, @ItemCode, @ProductName,0,0,@TaxIDGS,@ProductCompleteName,@NetTax)
																			set @PurchaseDetailID = (SELECT SCOPE_IDENTITY());
																			
																			--select @LastTotalPrice = @Qty1GrossTotal,
																			--	   @LastTotalTax = @NetTax,
																			--	   @LastGrandTotal = @Qty1GrossTotal,
																			--	   @LastNetTotal = @NetTotal
																			
																			
																				   --@LastDiscount = @MarkupVaue  
																			
																		
																			
																			insert into @SupplierIDs values (@SupplierID,@PurchaseID,@Qty1GrossTotal,@NetTax,@Qty1GrossTotal,@NetTotal)
																			Update top(1) tbl_prefixes set PONext = PONext + 1  --line added by naveed after commenting lines above
																			
																	 end  -- end of else part of exist 
							   end -- end of supplier > 0
							
						
						
		    SET @Counter = @Counter + 1
		 end -- end of while
		
		 SET @Err = @@Error	
		 select @PurchaseDetailID as MSG
	
		RETURN @Err
END