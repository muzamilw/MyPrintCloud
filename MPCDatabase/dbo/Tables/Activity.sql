CREATE TABLE [dbo].[Activity] (
    [ActivityId]          INT           IDENTITY (1, 1) NOT NULL,
    [ActivityTypeId]      INT           CONSTRAINT [DF__tbl_activ__Activ__060DEAE8] DEFAULT (0) NOT NULL,
    [ActivityCode]        VARCHAR (50)  NULL,
    [ActivityRef]         VARCHAR (500) NULL,
    [ActivityDate]        DATETIME      NULL,
    [ActivityTime]        DATETIME      CONSTRAINT [DF__tbl_activ__Activ__07020F21] DEFAULT ('1999-11-30') NOT NULL,
    [ActivityStartTime]   DATETIME      NULL,
    [ActivityEndTime]     DATETIME      NULL,
    [ActivityProbability] INT           NULL,
    [ActivityPrice]       INT           CONSTRAINT [DF__tbl_activ__Activ__07F6335A] DEFAULT (0) NULL,
    [ActivityUnit]        INT           CONSTRAINT [DF__tbl_activ__Activ__08EA5793] DEFAULT (0) NULL,
    [ActivityNotes]       NTEXT         NULL,
    [IsActivityAlarm]     INT           CONSTRAINT [DF__tbl_activ__IsAct__09DE7BCC] DEFAULT (0) NOT NULL,
    [AlarmDate]           DATETIME      NULL,
    [AlarmTime]           DATETIME      CONSTRAINT [DF__tbl_activ__Alarm__0AD2A005] DEFAULT ('1999-11-30') NULL,
    [ActivityLink]        INT           CONSTRAINT [DF__tbl_activ__Activ__0BC6C43E] DEFAULT (1) NOT NULL,
    [IsCustomerActivity]  BIT           NULL,
    [ContactId]           INT           NULL,
    [SupplierContactId]   INT           NULL,
    [ProspectContactId]   INT           NULL,
    [SystemUserId]        INT           CONSTRAINT [DF__tbl_activ__Syste__0DAF0CB0] DEFAULT (0) NOT NULL,
    [IsPrivate]           BIT           NULL,
    [IsComplete]          INT           CONSTRAINT [DF__tbl_activ__IsCom__0F975522] DEFAULT (0) NOT NULL,
    [CompletionDate]      DATETIME      NULL,
    [CompletionTime]      DATETIME      CONSTRAINT [DF__tbl_activ__Compl__108B795B] DEFAULT ('1999-11-30') NULL,
    [CompletionSuccess]   INT           NULL,
    [CompletionResult]    VARCHAR (100) NULL,
    [CompletedBy]         INT           NULL,
    [IsFollowedUp]        INT           NULL,
    [FollowedActivityId]  INT           NULL,
    [LastModifiedDate]    DATETIME      NULL,
    [LastModifiedtime]    DATETIME      CONSTRAINT [DF__tbl_activ__LastM__117F9D94] DEFAULT ('1999-11-30') NULL,
    [LastModifiedBy]      INT           NULL,
    [CreatedBy]           INT           CONSTRAINT [DF_tbl_activity_CreatedBy] DEFAULT (0) NULL,
    [CampaignId]          INT           NULL,
    [IsLocked]            SMALLINT      CONSTRAINT [DF__tbl_activ__IsLoc__1273C1CD] DEFAULT (0) NULL,
    [LockedBy]            INT           CONSTRAINT [DF_tbl_activity_LockedBy] DEFAULT (0) NULL,
    [SystemSiteId]        INT           CONSTRAINT [DF_tbl_activity_SystemSiteID] DEFAULT (0) NULL,
    [ContactCompanyId]    INT           NULL,
    [ProductTypeId]       INT           NULL,
    [SourceId]            INT           NULL,
    [FlagId]              INT           NULL,
    CONSTRAINT [PK_tbl_activity] PRIMARY KEY CLUSTERED ([ActivityId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [CompletedBy]
    ON [dbo].[Activity]([CompletedBy] ASC);


GO
CREATE NONCLUSTERED INDEX [SupplierContactID]
    ON [dbo].[Activity]([SupplierContactId] ASC);


GO
CREATE NONCLUSTERED INDEX [CustomerContactID]
    ON [dbo].[Activity]([ContactId] ASC);


GO
CREATE NONCLUSTERED INDEX [SystemUserID]
    ON [dbo].[Activity]([SystemUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [ActivityID]
    ON [dbo].[Activity]([ActivityTypeId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Field will hold the UserID who created the Activity', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Activity', @level2type = N'COLUMN', @level2name = N'CreatedBy';

