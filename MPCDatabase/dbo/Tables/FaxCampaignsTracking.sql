CREATE TABLE [dbo].[FaxCampaignsTracking] (
    [FAXCampaignTrackingId] INT          IDENTITY (1, 1) NOT NULL,
    [FAXCampaignId]         INT          NOT NULL,
    [ReplyCode]             VARCHAR (50) NULL,
    [FAXNumber]             VARCHAR (50) NULL,
    [IsDeliverd]            BIT          NULL,
    [IsWithError]           INT          NOT NULL,
    [Id]                    INT          NULL,
    [ContactId]             INT          NULL,
    [MailFlag]              SMALLINT     NULL,
    [Date]                  DATETIME     NULL,
    CONSTRAINT [PK_tbl_FAXCampaignsTracking] PRIMARY KEY CLUSTERED ([FAXCampaignTrackingId] ASC, [FAXCampaignId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1-Customer 0-Supplier 2-CustomerContact 3-SupplierContact 4 CustomerAddress 5 SupplierAddress', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FaxCampaignsTracking', @level2type = N'COLUMN', @level2name = N'MailFlag';

