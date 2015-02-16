
CREATE PROCEDURE [dbo].[sp_Customer_InsertContactCompanies]
as

--inserting flags

declare @companyID int

select @companyId = companysiteID from tbl_company_sites

declare @FlagID int

select top 1  @FlagID = Sectionflagid from tbl_section_flags where sectionid = 17

declare @NominalCode int


INSERT INTO [tbl_section_flags]
           ([SectionID]
           ,[FlagName]
           ,[FlagColor]
           ,[flagDescription]
           ,[CompanyID]
           ,[FlagColumn]
           ,[isDefault])
    
select 17, rtrim(ltrim(cats.cat)),null,rtrim(ltrim(cats.cat)),@companyId,null,null from

(
Select distinct f23 cat from dbo.tbl_contactcompanies_sandbox

) cats where cats.cat <> ''



--import main company
declare @typeID int

select top 1 @typeID = Typeid from tbl_contactcompanytypes where isfixed=1


INSERT INTO [tbl_contactcompanies]
           ([AccountNumber]
           ,[Name]
           ,[URL]
           ,[CreditLimit]
           ,[Terms]
           ,[TypeID]
           ,[DefaultNominalCode]
           ,[AccountOpenDate]
           ,[Status]
           ,[IsCustomer]
           ,[Notes]
           ,[NotesLastUpdatedDate]
           ,[NotesLastUpdatedBy]
           ,[AccountStatusID]
           ,[IsDisabled]
           ,[CreationDate]
           ,[VATRegNumber]
           ,[FlagID]
           ,[isArchived]
           ,[homecontact])
SELECT 
		[Account Reference]
      ,[Account Name]
      ,[Company Web address]
      ,[Credit Limit]
      ,[Terms]
      ,case when [company type] = 'C' then 52
             when [company type] = 'S' then 93
              END As JobDescriptionTitle1
      --,isnull([Company Nominal Code],'')
      ,isnull(ca.AccountNo,'')
      ,getdate()
      ,0 --status
      ,1 --iscustomer customer
      ,[GREEN IS WHAT WE WOULD LOVE TO UPLOAD TOO_]
      ,getdate() -- notes updtdt
      ,null
      ,0 --account ststus id
      ,0 --isdisabled
      ,getdate() -- creation date
      ,[Vat Registration Number]
      ,@FlagID
      ,0 isarchived,[Company Telephone]
	  FROM [tbl_contactCompanies_SandBox] c
	   left join tbl_chartofaccount ca on ca.AccountNo = c.[Company Nominal Code]

     
--select * from tbl_contactcompanies_sandbox         
         
         
         INSERT INTO [tbl_addresses]
           ([ContactCompanyID]
           ,[AddressName]
           ,[Address1]
           ,[Address2]
           ,[City]
           ,[State]
           ,[Country]
           ,[PostCode]
           ,[Fax]
           ,[Email]
           ,[URL]
           ,[Tel1]
           ,[IsDefaultAddress]
           ,[IsDefaultShippingAddress]
           ,[isArchived]
       )
SELECT 
		c.contactcompanyID
		,'Default'
      ,[Address 1]
      ,[Address 2]
      ,[City]
      ,[State]
      ,[Country]
      ,[ZipCode]
       ,[Company Fax]
       ,[Contact e-mail]
        ,[Company Web address]
      ,[Company Telephone]
     ,1 isdefaultaddress
     ,1 isdefaultshipping
     ,0 isarchived
     
  FROM [tbl_contactCompanies_SandBox] cb
  inner join tbl_contactCompanies c on c.[AccountNumber] = cb.[Account Reference] and c.[Name] = cb.[Account Name]




--inserting Contacts

INSERT INTO [MPC].[dbo].[tbl_contacts]
           ([AddressID]
           ,[ContactCompanyID]
           ,[FirstName]
           ,[Mobile]
           ,[Email]
           ,[FAX]
           ,[IsDefaultContact]
           ,[IsEmailSubscription]
           ,[IsNewsLetterSubscription]
           ,[CreditLimit]
           ,[isArchived]
           )
SELECT 
		d.addressid,
		c.contactcompanyid
		,[Contact Name]
        ,[Contact Tel Number]
        ,[Contact e-mail]
		,[Company Fax]
		,1 isdefaultcontact
		,1 emailsubs
		,1 mailsubs
		, CAST(isnull(nullif([Credit Limit],0),0) as decimal)  AS EMPOWERMENT_PERCENT
		,0 isarchived

  FROM [MPC].[dbo].[tbl_contactCompanies_SandBox] cb
   inner join tbl_contactCompanies c on c.[AccountNumber] = cb.[Account Reference] and c.[Name] = cb.[Account Name]
    inner join tbl_addresses d on d.ContactcompanyID = c.contactcompanyID