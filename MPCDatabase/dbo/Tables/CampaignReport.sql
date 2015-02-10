CREATE TABLE [dbo].[CampaignReport] (
    [CampaignReportId] INT            IDENTITY (1, 1) NOT NULL,
    [CampaignId]       INT            NULL,
    [StartDate]        DATETIME       NULL,
    [EndDate]          DATETIME       NULL,
    [TotalCount]       INT            NULL,
    [TotalDeliverd]    INT            NULL,
    [TotalFailed]      INT            NULL,
    [Discription]      VARCHAR (800)  NULL,
    [Report]           VARCHAR (8000) NULL,
    [OrganisationId]   BIGINT         NULL,
    CONSTRAINT [PK_tbl_CampaignReports_1] PRIMARY KEY CLUSTERED ([CampaignReportId] ASC)
);

