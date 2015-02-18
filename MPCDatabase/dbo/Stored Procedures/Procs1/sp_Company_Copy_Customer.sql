CREATE PROCEDURE dbo.sp_Company_Copy_Customer

	(
		@OldSiteID int,
		@NewSiteID int,
		@DepartmentID int,
		@IsCustomerCopy int
	)

AS
	Declare @CString as varchar(500)
		if @IsCustomerCopy <> 0 
			set	@Cstring = 'Declare Customer Cursor 
										for Select CustomerID from tbl_customers Where SystemSiteID=' + convert(varchar(150),@OldSiteID)
		else
			set	@Cstring = 'Declare Customer Cursor 
										for Select CustomerID from tbl_customers Where IsGeneralCustomer<>0 and SystemSiteID=' + convert(varchar(150),@OldSiteID)
			

	Exec(@Cstring)

	Declare @CustomerID int
	Declare @AddressID int
	Declare @NewCustomerID int
	Declare @NewAddressID int
	Declare @CategoryID int
	Declare @NewCategoryID int
	
	Open Customer
	FETCH NEXT from Customer into @CustomerID
	
	While @@Fetch_Status = 0
		BEGIN
		
			insert into tbl_customers SELECT     AccountNumber, CustomerName, URL, CreditReference, CreditLimit, Terms, CustomerTypeID, DefaultNominalCode, DefaultTill, DefaultMarkUpID, 
                      AccountOpenDate, AccountManagerID, CustomerStatus, IsCustomer, Notes, ISBN, NotesLastUpdatedDate, NotesLastUpdatedBy, AccountOnHandDesc, 
                      AccountStatusID, IsDisabled, LockedBy, AccountBalance, CustomerCreationDate, VATRegNumber, IsParaentCompany, ParaentCompanyID, 
                      @NewSiteID, VATRegReference, FlagID, CustomerImage, IsEmailSubscription, IsMailSubscription, IsEmailFormat, IsAllowWebAccess, HomeContact, 
                      AbountUs, ContactUs, IsGeneralCustomer, WebAccessAdminUserName, WebAccessAdminPassword, WebAccessAdminPasswordHint, 
                      IsShowFinishedGoodPrices, IsReed, @DepartmentID,CustomerSalesPerson
					  FROM tbl_customers where CustomerID=@CustomerID
					  
					  Select @NewCustomerID=@@Identity from tbl_customers
					  
					  insert into #Customers values (@CustomerID,@NewCustomerID)
					  
						
					  Declare CustomerCategory Cursor for Select ID from tbl_finishgood_categories where CustomerID=@CustomerID			  
					  open  CustomerCategory
					  	FETCH NEXT from CustomerCategory into @CategoryID
							While @@Fetch_Status = 0
								BEGIN
									insert into tbl_finishgood_categories SELECT     ItemLibrarayGroupName, Image, Thumbnail, ContentType, Description1, Description2, LockedBy, @NewCustomerID, ParentID, DisplayOrder
												FROM         tbl_finishgood_categories	where ID=@CategoryID
									
									Select 	@NewCategoryID = @@Identity
				
				  				    insert into #CustomerCategories values (@CategoryID,@NewCategoryID)
									
									update tbl_finishgood_categories set 
																	ParentID=IsNull((Select [New_ID] from #CustomerCategories where Old_ID=ParentID),0) 
																	where ID=@CategoryID
									
									FETCH NEXT from CustomerCategory into @CategoryID
								End 
					  close CustomerCategory
					  deallocate CustomerCategory
						
					  
					  
					  Declare Address Cursor for Select AddressID from tbl_customeraddresses where CustomerID=@CustomerID			  
					  Open Address
							FETCH NEXT from Address into @AddressID
							While @@Fetch_Status = 0
								BEGIN
								
									--Inserting Addresses
									insert into tbl_customeraddresses SELECT     @NewCustomerID, AddressName, Address1, Address2, Address3, City, StateID, CountryID, PostCode, Fax, Email, URL, Tel1, Tel2, Extension1, Extension2, 
																		Reference, FAO, IsDefaultAddress, IsDefaultShippingAddress
																		FROM         tbl_customeraddresses	Where AddressID=@AddressID
											
									Select @NewAddressID=@@Identity From tbl_customeraddresses
									
									--Inserting Contacts 
									insert into tbl_customercontacts SELECT     @NewAddressID, @NewCustomerID, FirstName, MiddleName, LastName, Title, HomeTel1, HomeTel2, HomeExtension1, HomeExtension2, Mobile, Pager, Email, FAX,
																		JobTitle, Department, DOB, Notes, IsDefaultContact, HomeAddress1, HomeAddress2, HomeCity, HomeStateID, HomePostCode, HomeCountryID
																		FROM         tbl_customercontacts where CustomerID=@CustomerID and AddressID=@AddressID
																		
									FETCH NEXT from Address into @AddressID									
								END
					  Close Address
					  Deallocate Address
					  
					  FETCH NEXT from Customer into @CustomerID
		END		
		
	Close Customer
	Deallocate Customer		
RETURN