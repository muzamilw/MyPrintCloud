CREATE TABLE [dbo].[EmailEvent] (
    [EmailEventId] INT           IDENTITY (1, 1) NOT NULL,
    [EventName]    VARCHAR (100) NOT NULL,
    [Description]  VARCHAR (500) NULL,
    [EventType]    INT           NULL,
    CONSTRAINT [PK_tbl_EmailEvents] PRIMARY KEY CLUSTERED ([EmailEventId] ASC)
);

