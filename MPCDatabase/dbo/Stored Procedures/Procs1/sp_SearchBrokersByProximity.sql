---   [sp_SearchBrokersByProximity]   23.69781,120.960515,'tw',1 
--exec [sp_SearchBrokersByProximity]  
CREATE PROCEDURE [dbo].[sp_SearchBrokersByProximity]   
      -- Add the parameters for the stored procedure here  
      @lat varchar(20) = 0,  
      @long varchar(20) = 0,  
      @PostCode nvarchar(100) = 0,  
      @mode int = 0  
        
AS  
BEGIN  
      -- SET NOCOUNT ON added to prevent extra result sets from  
      -- interfering with SELECT statements.  
      SET NOCOUNT ON;  
  
    -- Insert statements for procedure here  
    
    
    
    
    
    
    
      
    --set @postcode = LOWER( @postcode)  
      
       
     --if ( @mode = 1 )  
     --begin  
         
        
																							
        
   --select @postcode,@postcode2  
     
     
   select c.ContactCompanyID, c.Name as ContactCompanyName, c.WebAccessCode, c.Image as Logo, cc.FirstName + ' '  + cc.LastName as ContactPerson,   
     c.HomeContact,  ad.Address1, ad.Address2, ad.City, ad.Country, ad.State, ad.PostCode, ''  as OutPostCode , ad.GeoLatitude, ad.GeoLongitude
     ,CAST('POINT ('+ @long  +' ' + @lat +')' as geography).STDistance(CAST('POINT ('+ ad.GeoLongitude +' ' + ad.GeoLatitude  +')' as geography)) as Distance_Meters
     --,((ACOS(SIN(@lat * PI() / 180) * SIN( cast( ad.GeoLatitude as decimal) * PI() / 180) + COS(@lat * PI() / 180) * COS(cast( ad.GeoLatitude as decimal) * PI() / 180) * COS((@long - cast(ad.GeoLongitude as decimal)) * PI() / 180)) * 180 / PI()) * 60 * 1.1515 * 1.609344) as km
       
     --,3958.75586574 * acos(sin(@lat/57.2957795130823) * sin(cast (ad.GeoLatitude as decimal)/57.2957795130823) + cos(@lat/57.2957795130823) * cos( cast(ad.GeoLatitude as decimal)/57.2957795130823) * cos( cast(ad.GeoLongitude as decimal)/57.2957795130823 - @long/57.2957795130823))km2
     --dbo.DictanceKM(@lat,ad.GeoLatitude, @long, ad.GeoLongitude) as km3, ad.GeoLatitude, ad.GeoLongitude  
       
       
      from   
   tbl_contactcompanies c  
   inner join tbl_addresses ad on c.ContactCompanyID = ad.ContactCompanyID and ad.IsDefaultAddress = 1 and ad.IsDefaultShippingAddress = 1 and ad.GeoLatitude is not null and ad.GeoLongitude is not null and ad.GeoLatitude <> '' and ad.GeoLongitude <> ''
   inner join tbl_contacts cc on c.ContactCompanyID  = cc.ContactCompanyID and cc.IsDefaultContact = 1 
   where c.iscustomer=4 and c.isdisabled<>1--and ISNUMERIC(ad.GeoLatitude)<> 1 and ISNUMERIC(ad.GeoLongitude)<> 1
   order by CAST('POINT ('+ @long  +' ' + @lat +')' as geography).STDistance(CAST('POINT ('+ ad.GeoLongitude  +' ' + ad.GeoLatitude +')' as geography)) ASC
  --order by dbo.F_GREAT_CIRCLE_DISTANCE(@lat,@long, ad.GeoLatitude, ad.GeoLongitude) ASC  
  
        
        
        
  ---------------- declare @exclusive int  
  ---------------- set @exclusive = 0  
        
        
        
        
  ---------------- select @exclusive = COUNT(*)  from tbl_contactcompanies c  
  ----------------   inner join tbl_PC_PostCodesBrokers PCB on PCB.ContactCompanyID = c.ContactCompanyID and pcb.Status = 1 and c.IsCustomer = 4 and c.isArchived = 0 and c.IsDisabled = 0  
  ----------------   inner join tbl_PC_PostCodes PC on PCB.OutPostCodeID = PC.OutPostCodeID  
  ----------------   where  ( @postcode <> '' and  pc.OutPostCode = @postcode)  --or ( @city <> '' and lower(pc.City) like '%' + @city+'%' )  
          
          
  ---------------- if ( @exclusive  > 0 )  
  ---------------- begin  
  ----------------   -- in case of exclusive only show 1 record, the top most one  
  ----------------   select top 1 c.ContactCompanyID, c.Name as ContactCompanyName, c.WebAccessCode, c.Image as Logo, cc.FirstName + ' '  + cc.LastName as ContactPerson,   
  ----------------   c.HomeContact,  ad.Address1, ad.Address2, ad.City, ad.Country, ad.State, ad.PostCode, pc.OutPostCode  from tbl_contactcompanies c  
                
                
  ----------------   inner join tbl_addresses ad on c.ContactCompanyID = ad.ContactCompanyID and ad.IsDefaultAddress = 1 and c.IsCustomer = 4 and c.isArchived = 0 and c.IsDisabled = 0  
  ----------------   inner join tbl_contacts cc on c.ContactCompanyID  = cc.ContactCompanyID and cc.IsDefaultContact = 1  
  ----------------   inner join tbl_PC_PostCodesBrokers PCB on PCB.ContactCompanyID = c.ContactCompanyID and pcb.Status = 1  
  ----------------   inner join tbl_PC_PostCodes PC on PCB.OutPostCodeID = PC.OutPostCodeID  
  ----------------   where  ( @postcode <> '' and  pc.OutPostCode = @postcode)  --or ( @city <> '' and lower(pc.City) like '%' + @city+'%' )  
  ----------------   order by PCB.Status, ContactCompanyName  
  ----------------   end  
  ----------------   else  
  ----------------   begin  
          
  ----------------   select c.ContactCompanyID, c.Name as ContactCompanyName, c.WebAccessCode, c.Image as Logo, cc.FirstName + ' '  + cc.LastName as ContactPerson,   
  ----------------   c.HomeContact,  ad.Address1, ad.Address2, ad.City, ad.Country, ad.State, ad.PostCode, pc.OutPostCode  from tbl_contactcompanies c  
                
                
  ----------------   inner join tbl_addresses ad on c.ContactCompanyID = ad.ContactCompanyID and ad.IsDefaultAddress = 1 and c.IsCustomer = 4 and c.isArchived = 0 and c.IsDisabled = 0  
  ----------------   inner join tbl_contacts cc on c.ContactCompanyID  = cc.ContactCompanyID and cc.IsDefaultContact = 1  
  ----------------   inner join tbl_PC_PostCodesBrokers PCB on PCB.ContactCompanyID = c.ContactCompanyID  
  ----------------   inner join tbl_PC_PostCodes PC on PCB.OutPostCodeID = PC.OutPostCodeID  
  ----------------   where  ( @postcode <> '' and  pc.OutPostCode = @postcode)  --or ( @city <> '' and lower(pc.City) like '%' + @city+'%' )  
  ----------------   order by PCB.Status, ContactCompanyName  
          
          
  ----------------   end  
  ----------------    end  
  ----------------    else if ( @mode = 2)  
  ----------------    begin  
        
  ----------------select distinct c.ContactCompanyID, c.Name as ContactCompanyName, c.WebAccessCode, c.Image as Logo, cc.FirstName + ' '  + cc.LastName as ContactPerson,   
  ----------------   c.HomeContact,  ad.Address1, ad.Address2, ad.City, ad.Country, ad.State, ad.PostCode, '' as OutPostCode  from tbl_contactcompanies c  
                
                
  ----------------   inner join tbl_addresses ad on c.ContactCompanyID = ad.ContactCompanyID and ad.IsDefaultAddress = 1 and c.IsCustomer = 4  
  ----------------   inner join tbl_contacts cc on c.ContactCompanyID  = cc.ContactCompanyID and cc.IsDefaultContact = 1  
  ----------------   --inner join tbl_PC_PostCodesBrokers PCB on PCB.ContactCompanyID = c.ContactCompanyID  
  ----------------   --inner join tbl_PC_PostCodes PC on PCB.OutPostCodeID = PC.OutPostCodeID  
  ----------------   --where  ( @postcode <> '' and  pc.OutPostCode = @postcode)  --or ( @city <> '' and lower(pc.City) like '%' + @city+'%' )  
  ----------------   where c.isArchived = 0 and c.IsDisabled = 0  
  ----------------   order by ContactCompanyName  
        
  ----------------    end  
        
        
END