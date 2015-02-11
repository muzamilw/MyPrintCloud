CREATE TABLE [dbo].[ListingConjunctionAgent] (
    [ConjunctionAgentId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [FirstName]          VARCHAR (255) NULL,
    [LastName]           VARCHAR (255) NULL,
    [Company]            VARCHAR (255) NULL,
    [Email]              VARCHAR (255) NULL,
    [Mobile]             VARCHAR (50)  NULL,
    [Phone]              VARCHAR (50)  NULL,
    [ListingId]          BIGINT        NULL,
    CONSTRAINT [PK_ListingConjunctionAgent] PRIMARY KEY CLUSTERED ([ConjunctionAgentId] ASC)
);

