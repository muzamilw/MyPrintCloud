-- =============================================
-- Author:        Muz
-- Create date: 
-- Description:   
--    sp_SearchBrokersByPostCode 'sl3',2
-- =============================================
CREATE PROCEDURE [dbo].[sp_SearchBrokersByPostCode] 
      -- Add the parameters for the stored procedure here
      @postcode nvarchar(100) = 0,
      @mode int = 0
      
AS
BEGIN
      -- SET NOCOUNT ON added to prevent extra result sets from
      -- interfering with SELECT statements.
      SET NOCOUNT ON;

    -- Insert statements for procedure here
    
    set @postcode = LOWER( @postcode)
    
     
     if ( @mode = 1 )
     begin
		     
		    
			--select @postcode,@postcode2
		    
			declare @exclusive int
			set @exclusive = 0
		    
		    
		    
		    
			select @exclusive = COUNT(*)  from tbl_contactcompanies c
			  inner join tbl_PC_PostCodesBrokers PCB on PCB.ContactCompanyID = c.ContactCompanyID and pcb.Status = 1 and c.IsCustomer = 4 and c.isArchived = 0 and c.IsDisabled = 0
			  inner join tbl_PC_PostCodes PC on PCB.OutPostCodeID = PC.OutPostCodeID
			  where  ( @postcode <> '' and  pc.OutPostCode = @postcode)  --or ( @city <> '' and lower(pc.City) like '%' + @city+'%' )
		      
		      
			if ( @exclusive  > 0 )
			begin
					-- in case of exclusive only show 1 record, the top most one
					select top 1 c.ContactCompanyID, c.Name as ContactCompanyName, c.WebAccessCode, c.Image as Logo, cc.FirstName + ' '  + cc.LastName as ContactPerson, 
					c.HomeContact,  ad.Address1, ad.Address2, ad.City, ad.Country, ad.State, ad.PostCode, pc.OutPostCode  from tbl_contactcompanies c
		            
		            
					inner join tbl_addresses ad on c.ContactCompanyID = ad.ContactCompanyID and ad.IsDefaultAddress = 1 and c.IsCustomer = 4 and c.isArchived = 0 and c.IsDisabled = 0
					inner join tbl_contacts cc on c.ContactCompanyID  = cc.ContactCompanyID and cc.IsDefaultContact = 1
					inner join tbl_PC_PostCodesBrokers PCB on PCB.ContactCompanyID = c.ContactCompanyID and pcb.Status = 1
					inner join tbl_PC_PostCodes PC on PCB.OutPostCodeID = PC.OutPostCodeID
					where  ( @postcode <> '' and  pc.OutPostCode = @postcode)  --or ( @city <> '' and lower(pc.City) like '%' + @city+'%' )
					order by PCB.Status, ContactCompanyName
			  end
			  else
			  begin
		      
					select c.ContactCompanyID, c.Name as ContactCompanyName, c.WebAccessCode, c.Image as Logo, cc.FirstName + ' '  + cc.LastName as ContactPerson, 
					c.HomeContact,  ad.Address1, ad.Address2, ad.City, ad.Country, ad.State, ad.PostCode, pc.OutPostCode  from tbl_contactcompanies c
		            
		            
					inner join tbl_addresses ad on c.ContactCompanyID = ad.ContactCompanyID and ad.IsDefaultAddress = 1 and c.IsCustomer = 4 and c.isArchived = 0 and c.IsDisabled = 0
					inner join tbl_contacts cc on c.ContactCompanyID  = cc.ContactCompanyID and cc.IsDefaultContact = 1
					inner join tbl_PC_PostCodesBrokers PCB on PCB.ContactCompanyID = c.ContactCompanyID
					inner join tbl_PC_PostCodes PC on PCB.OutPostCodeID = PC.OutPostCodeID
					where  ( @postcode <> '' and  pc.OutPostCode = @postcode)  --or ( @city <> '' and lower(pc.City) like '%' + @city+'%' )
					order by PCB.Status, ContactCompanyName
		      
		      
			  end
      end
      else if ( @mode = 2)
      begin
      
		select distinct c.ContactCompanyID, c.Name as ContactCompanyName, c.WebAccessCode, c.Image as Logo, cc.FirstName + ' '  + cc.LastName as ContactPerson, 
					c.HomeContact,  ad.Address1, ad.Address2, ad.City, ad.Country, ad.State, ad.PostCode, '' as OutPostCode  from tbl_contactcompanies c
		            
		            
					inner join tbl_addresses ad on c.ContactCompanyID = ad.ContactCompanyID and ad.IsDefaultAddress = 1 and c.IsCustomer = 4
					inner join tbl_contacts cc on c.ContactCompanyID  = cc.ContactCompanyID and cc.IsDefaultContact = 1
					--inner join tbl_PC_PostCodesBrokers PCB on PCB.ContactCompanyID = c.ContactCompanyID
					--inner join tbl_PC_PostCodes PC on PCB.OutPostCodeID = PC.OutPostCodeID
					--where  ( @postcode <> '' and  pc.OutPostCode = @postcode)  --or ( @city <> '' and lower(pc.City) like '%' + @city+'%' )
					where c.isArchived = 0 and c.IsDisabled = 0
					order by ContactCompanyName
      
      end
      
      
END