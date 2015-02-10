CREATE TABLE [dbo].[CampaignClickThrough] (
    [ClickThroughId]       INT           IDENTITY (1, 1) NOT NULL,
    [CampaignId]           INT           NOT NULL,
    [ClickThruCountTotal]  INT           NULL,
    [ClickThruCount]       INT           NULL,
    [ClickThruUpdateField] VARCHAR (255) NULL,
    [ClickThruText]        VARCHAR (255) NULL,
    [ClickThruURL]         VARCHAR (255) NULL,
    [AutoResponseId]       INT           NULL,
    [StoredProcedure]      NTEXT         NULL,
    CONSTRAINT [PK_tbl_campaign_clickthroughs] PRIMARY KEY CLUSTERED ([ClickThroughId] ASC)
);

