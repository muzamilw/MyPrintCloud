CREATE TABLE [dbo].[CampaignImage] (
    [CampaignImageId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [CampaignId]      BIGINT        NULL,
    [ImagePath]       VARCHAR (350) NULL,
    [ImageName]       VARCHAR (200) NULL,
    CONSTRAINT [PK__tbl_Camp__BDA7DECA7A2998F5] PRIMARY KEY CLUSTERED ([CampaignImageId] ASC),
    CONSTRAINT [FK_tbl_CampaignImages_tbl_campaigns] FOREIGN KEY ([CampaignId]) REFERENCES [dbo].[Campaign] ([CampaignId])
);

