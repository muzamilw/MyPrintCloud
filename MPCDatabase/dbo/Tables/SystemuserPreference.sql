CREATE TABLE [dbo].[SystemuserPreference] (
    [SystemUserPreferenceId] INT           IDENTITY (1, 1) NOT NULL,
    [SystemUserId]           INT           NULL,
    [CallColor]              VARCHAR (50)  NULL,
    [AppointmentColor]       VARCHAR (50)  NULL,
    [NextActionColor]        VARCHAR (50)  NULL,
    [OtherActionColor]       VARCHAR (50)  NULL,
    [SaleColor]              VARCHAR (50)  NULL,
    [EventColor]             VARCHAR (50)  NULL,
    [ToDoColor]              VARCHAR (50)  NULL,
    [ShowPublic]             BIT           CONSTRAINT [DF_tbl_systemuser_preferences_ShowPublic] DEFAULT (0) NULL,
    [RecordRetrievalLimit]   INT           NULL,
    [SchedulingPreference]   IMAGE         NULL,
    [EnquiryRefreshTime]     SMALLINT      NULL,
    [EstimatesRefreshTime]   SMALLINT      NULL,
    [OrdersRefreshTime]      SMALLINT      NULL,
    [JobsRefreshTime]        SMALLINT      NULL,
    [SchedulingRefreshTime]  SMALLINT      NULL,
    [CRMReminderRefreshTime] SMALLINT      CONSTRAINT [DF_tbl_systemuser_preferences_ReminderRefreshTime] DEFAULT (1) NULL,
    [DefaultCalendar]        SMALLINT      CONSTRAINT [DF_tbl_systemuser_preferences_DefaultCanlendar] DEFAULT (1) NULL,
    [SmtpServer]             VARCHAR (200) NULL,
    [SmtpUserName]           VARCHAR (200) NULL,
    [SmtpPassword]           VARCHAR (200) NULL,
    [Pop3Server]             VARCHAR (200) NULL,
    [Pop3UserName]           VARCHAR (200) NULL,
    [Pop3Password]           VARCHAR (200) NULL,
    CONSTRAINT [PK_tbl_systemuser_preferences] PRIMARY KEY CLUSTERED ([SystemUserPreferenceId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Field uses enum to hold the default calendar which appears first time when user opens the CRM Section calendar section', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SystemuserPreference', @level2type = N'COLUMN', @level2name = N'DefaultCalendar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Field to hold the information of each user reminder settings time for Reminders in Minutes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SystemuserPreference', @level2type = N'COLUMN', @level2name = N'CRMReminderRefreshTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Will show the public activities in CRM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SystemuserPreference', @level2type = N'COLUMN', @level2name = N'ShowPublic';

