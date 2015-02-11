CREATE TABLE [dbo].[EmailCampaignTracking] (
    [EmailCampaignTrakingId] INT           IDENTITY (1, 1) NOT NULL,
    [CampaignId]             INT           NOT NULL,
    [EmailAddress]           VARCHAR (100) NULL,
    [ReplyDescription]       VARCHAR (150) NULL,
    [ReplyCode]              VARCHAR (50)  NULL,
    [isDeliverd]             BIT           NULL,
    [isBoucedBack]           BIT           NULL,
    [BouceBackCode]          VARCHAR (50)  NULL,
    [BouceBackDescription]   VARCHAR (150) NULL,
    [isOpened]               BIT           NULL,
    [SubscriptionRequest]    BIT           NULL,
    [UnSubscriptionRequest]  BIT           NULL,
    [Id]                     INT           NULL,
    [ContactId]              INT           NULL,
    [MailFlag]               SMALLINT      NULL,
    [Date]                   DATETIME      NULL,
    CONSTRAINT [PK_tbl_EmailCampaignTracking] PRIMARY KEY NONCLUSTERED ([EmailCampaignTrakingId] ASC, [CampaignId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [CompaginID]
    ON [dbo].[EmailCampaignTracking]([CampaignId] DESC);


GO
CREATE UNIQUE CLUSTERED INDEX [PK]
    ON [dbo].[EmailCampaignTracking]([EmailCampaignTrakingId] ASC);


GO


CREATE   TRIGGER EmailCampaginTracking_ShiftDeliverdEMails ON [dbo].[EmailCampaignTracking] 
FOR UPDATE 


AS

declare @RefTableName varchar(100)
declare @RefFieldName varchar(100)
declare @RefKeyID int
declare @RefranceID int
Declare @Date datetime
Declare @Title varchar(100)
Declare @Address varchar(255)
declare @Message varchar(8000)
Declare @UID int



DECLARE Email_Cursor CURSOR FAST_FORWARD FOR
	SELECT     tbl_campaigns.CampaignName, tbl_campaigns.CampaignID,
		Inserted.[Date], Inserted.EmailAddress, 
		case Inserted.MailFlag
			when 2 then Inserted.ContactID
			when 3 then Inserted.ContactID
			when 4 then Inserted.ID
			when 5 then Inserted.ID
		end as RefKeyID,
		case Inserted.MailFlag
			when 2 then 'tbl_customercontacts'
			when 3 then 'tbl_suppliercontacts'
			when 4 then 'tbl_customers'
			when 5 then 'tbl_suppliers'
		end as RefTableName,
		case Inserted.MailFlag
			when 2 then 'CustomerContactID'
			when 3 then 'SupplierContactID'
			when 4 then 'CustomerID'
			when 5 then 'SupplierID'
		end as RefFieldName,
		
		case tbl_campaigns.MessageType 
			when 0 then convert(varchar(8000),tbl_campaigns.HTMLMessageA)
			when 1 then convert(varchar(8000),tbl_campaigns.PlainTextMessageA)
			when 3 then convert(varchar(8000),tbl_campaigns.HTMLMessageA)
			when 4 then convert(varchar(8000),tbl_campaigns.PlainTextMessageA)
		end as message     , UID         
FROM         Inserted   INNER JOIN
             tbl_campaigns ON Inserted.CampaignID = tbl_campaigns.CampaignID 
	     where isDeliverd=1
	

open Email_Cursor



FETCH NEXT FROM Email_Cursor
	INTO @Title,@RefranceID, @Date,@Address,@RefKeyID,@RefTableName,@RefFieldName,@Message,@UID


WHILE @@FETCH_STATUS = 0

	begin



		INSERT INTO tbl_Correspondence
			(RefTableName, RefFieldName, RefKeyID, Reference, Type, ReferenceID, 
							ReferenceType, Title, [Date], Address,Direction,UID)
		values(@RefTableName,@RefFieldName,@RefKeyID,@Title,1,@RefranceID,1,@Title,getdate(),@Address,1,@UID)
		
		insert into tbl_Correspondence_Details  (Discriptions,CorrespondenceID)
							values (@Message ,  SCOPE_IDENTITY())


		FETCH NEXT FROM Email_Cursor
		INTO @Title,@RefranceID, @Date,@Address,@RefKeyID,@RefTableName,@RefFieldName,@Message,@UID


	end 


close Email_Cursor
deallocate  Email_Cursor
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1-Customer 0-Supplier 2-CustomerContact 3-SupplierContact 4 CustomerAddress 5 SupplierAddress', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmailCampaignTracking', @level2type = N'COLUMN', @level2name = N'MailFlag';

