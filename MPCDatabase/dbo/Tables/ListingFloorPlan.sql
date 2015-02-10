CREATE TABLE [dbo].[ListingFloorPlan] (
    [FloorPlanId]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [ListingId]         BIGINT        NULL,
    [ImageURL]          VARCHAR (255) NULL,
    [PDFURL]            VARCHAR (255) NULL,
    [LastMode]          DATETIME      NULL,
    [ClientFloorplanID] VARCHAR (100) NULL,
    CONSTRAINT [PK_ListingFloorPlan] PRIMARY KEY CLUSTERED ([FloorPlanId] ASC)
);

