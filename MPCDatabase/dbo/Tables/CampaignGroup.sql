CREATE TABLE [dbo].[CampaignGroup] (
    [CampaignGroupId] INT IDENTITY (1, 1) NOT NULL,
    [CampaignId]      INT NULL,
    [GroupId]         INT NULL,
    CONSTRAINT [PK_tbl_campaign_groups] PRIMARY KEY CLUSTERED ([CampaignGroupId] ASC)
);

