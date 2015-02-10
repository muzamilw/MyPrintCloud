CREATE TABLE [dbo].[UserEmailEvent] (
    [EventUserId]  INT NOT NULL,
    [SystemUserId] INT NULL,
    [EmailEventId] INT NULL,
    CONSTRAINT [PK_tbl_UserEmailEvents] PRIMARY KEY CLUSTERED ([EventUserId] ASC)
);

