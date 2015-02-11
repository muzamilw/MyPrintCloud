CREATE TABLE [dbo].[ActivityType] (
    [ActivityTypeId]      INT          IDENTITY (1, 1) NOT NULL,
    [ActivityName]        VARCHAR (50) NOT NULL,
    [ActivityDescription] VARCHAR (50) NULL,
    [ActivityColor]       VARCHAR (50) NULL,
    CONSTRAINT [PK_tbl_activitytype] PRIMARY KEY CLUSTERED ([ActivityTypeId] ASC)
);

