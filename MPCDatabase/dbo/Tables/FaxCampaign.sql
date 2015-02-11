CREATE TABLE [dbo].[FaxCampaign] (
    [FAXCampaignID]   INT           IDENTITY (1, 1) NOT NULL,
    [CampaignsID]     INT           NOT NULL,
    [DocumentName]    VARCHAR (50)  NULL,
    [Priority]        VARCHAR (50)  NULL,
    [SenderName]      VARCHAR (50)  NULL,
    [SenderNumber]    VARCHAR (100) NULL,
    [RecipientName]   VARCHAR (50)  NULL,
    [RecipientNumber] VARCHAR (100) NULL,
    [DocumentType]    SMALLINT      NULL,
    [FAXDocument]     IMAGE         NULL,
    CONSTRAINT [PK_tbl_campaigns_FAX] PRIMARY KEY CLUSTERED ([FAXCampaignID] ASC)
);

