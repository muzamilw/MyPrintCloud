CREATE TABLE [dbo].[ListingAgent] (
    [AgentId]    BIGINT        IDENTITY (1, 1) NOT NULL,
    [MemberId]   BIGINT        NULL,
    [AgentOrder] INT           NULL,
    [ListingId]  BIGINT        NULL,
    [UserRef]    VARCHAR (50)  NULL,
    [Name]       VARCHAR (255) NULL,
    [Admin]      BIT           NULL,
    [Email]      VARCHAR (100) NULL,
    [Phone]      VARCHAR (20)  NULL,
    [Phone2]     VARCHAR (20)  NULL,
    [Mobile]     VARCHAR (20)  NULL,
    [Deleted]    BIT           NULL,
    CONSTRAINT [PK_ListingAgent] PRIMARY KEY CLUSTERED ([AgentId] ASC)
);

