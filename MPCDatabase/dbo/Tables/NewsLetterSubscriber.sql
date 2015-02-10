CREATE TABLE [dbo].[NewsLetterSubscriber] (
    [SubscriberId]     INT           IDENTITY (1, 1) NOT NULL,
    [Email]            VARCHAR (150) NULL,
    [FirstName]        VARCHAR (150) NULL,
    [LastName]         VARCHAR (150) NULL,
    [Status]           INT           CONSTRAINT [DF_tbl_NewsLetterSubscribers_Status_1] DEFAULT ((1)) NOT NULL,
    [SubscriptionCode] VARCHAR (50)  NOT NULL,
    [SubscribeDate]    DATETIME      CONSTRAINT [DF_tbl_NewsLetterSubscribers_SubscribeDate_1] DEFAULT (getdate()) NOT NULL,
    [UnSubscribeDate]  DATETIME      NULL,
    [ContactId]        BIGINT        NULL,
    [ContactCompanyID] INT           NULL,
    [FlagId]           INT           NULL,
    CONSTRAINT [PK_tbl_NewsLetterSubscribers_1] PRIMARY KEY CLUSTERED ([SubscriberId] ASC),
    CONSTRAINT [FK_tbl_NewsLetterSubscribers_tbl_Contacts] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[CompanyContact] ([ContactId])
);

