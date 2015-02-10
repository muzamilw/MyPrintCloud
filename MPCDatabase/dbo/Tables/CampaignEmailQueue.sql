CREATE TABLE [dbo].[CampaignEmailQueue] (
    [EmailQueueId]     INT            IDENTITY (1, 1) NOT NULL,
    [To]               VARCHAR (200)  NULL,
    [Cc]               VARCHAR (200)  NULL,
    [EmailFrom]        VARCHAR (200)  NULL,
    [Type]             SMALLINT       NULL,
    [Subject]          VARCHAR (200)  NULL,
    [Body]             TEXT           NULL,
    [Images]           TEXT           NULL,
    [SendDateTime]     DATETIME       CONSTRAINT [DF_tbl_Emails_MailBox_SendDateTime] DEFAULT (getdate()) NULL,
    [IsDeliverd]       TINYINT        CONSTRAINT [DF_tbl_MailBox_IsDevliverd] DEFAULT (0) NULL,
    [SMTPUserName]     VARCHAR (50)   CONSTRAINT [DF_tbl_Emails_MailBox_SMTPUserName] DEFAULT ('') NULL,
    [SMTPPassword]     VARCHAR (50)   CONSTRAINT [DF_tbl_Emails_MailBox_SMTPPassword] DEFAULT ('') NULL,
    [SMTPServer]       VARCHAR (50)   CONSTRAINT [DF_tbl_Emails_MailBox_SMTPServer] DEFAULT ('') NOT NULL,
    [ErrorResponse]    NVARCHAR (MAX) NULL,
    [FileAttachment]   NVARCHAR (MAX) NULL,
    [AttemptCount]     INT            NULL,
    [ToName]           VARCHAR (100)  NULL,
    [FromName]         VARCHAR (100)  NULL,
    [CampaignReportId] INT            NULL,
    [OrganisationId]   BIGINT         NULL,
    CONSTRAINT [PK_tbl_MailBox] PRIMARY KEY CLUSTERED ([EmailQueueId] ASC)
);

