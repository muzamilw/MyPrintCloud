

Create Procedure usp_update_company_imagepaths

@StoreLogo varchar(200) ,
@StoreBackgroung varchar(200) ,
@StoreWaterMark varchar(200) ,
@StoreContactLogos varchar(MAX) ,
@StorePageBanners varchar(MAX) ,
@StoreID int ,
@isWaterMark bit 

As
Begin

	if @isWaterMark = 1
	Begin
		update tbl_contactcompanies 
		set image = @StoreLogo,
		StoreBackgroundImage = @StoreBackgroung
		where contactcompanyid = @StoreID
	End
	Else
	Begin
		update tbl_contactcompanies 
		set image = @StoreLogo,
		StoreBackgroundImage = @StoreBackgroung,
		WatermarkText = @StoreWaterMark
	where contactcompanyid = @StoreID
	End


	If @StoreContactLogos <> ''
	Begin
		DECLARE @ContactImagePaths AS TABLE(
			 ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
			 imagePath varchar(200)
			 );
		     
		insert into @ContactImagePaths (imagePath)
		select  * from fb_Split_String(@StoreContactLogos,',')

		DECLARE @ContactDetails AS TABLE(
			 ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
			 contactID int
			 );
		
		insert into @ContactDetails (contactid)
		select contactid from tbl_contacts where contactcompanyid = @StoreID and image is not null
		
		Declare @counter int = 1
		Declare @loop int = (select count (*) from @ContactDetails)
		
		While @counter <= @loop
		Begin
			update tbl_contacts 
			set image = (select imagePath from @ContactImagePaths where rowid = @counter)
			where contactid = (select contactid from @ContactDetails where rowid = @counter)
			Set @counter =@counter +1;
		End

	End

	If @StorePageBanners <> ''
	Begin
		DECLARE @BannersImagePaths AS TABLE(
		ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
		imagePath varchar(200)
		);

		insert into @BannersImagePaths (imagePath)
		select  * from fb_Split_String(@StorePageBanners,',')
		
		DECLARE @BannersDetail AS TABLE(
		ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
		bannerid varchar(200)
		);
		insert into @BannersDetail (bannerid)
		select PageBannerID from tbl_cmsPageBanners where contactcompanyid = @StoreID
		
		Declare @bannerCounter int = 1
		Declare @bannerCount int = (select count(*) from @BannersDetail)
		
		While @bannerCounter <= @bannerCount
		Begin
			update tbl_cmsPageBanners 
			set imageURL = (select imagePath from @BannersImagePaths where rowid = @bannerCounter)
			where PageBannerID = (select bannerid from @BannersDetail where rowid = @bannerCounter)
			Set @bannerCounter = @bannerCounter + 1;
		End
		
	End
End