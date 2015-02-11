CREATE TABLE [dbo].[Inquiry] (
    [InquiryId]              INT           IDENTITY (1, 1) NOT NULL,
    [Title]                  VARCHAR (100) NOT NULL,
    [ContactId]              BIGINT        NOT NULL,
    [CreatedDate]            DATETIME      CONSTRAINT [DF_tbl_Inquiry_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [SourceId]               INT           NULL,
    [ContactCompanyId]       INT           NULL,
    [RequireByDate]          DATETIME      NULL,
    [SystemUserId]           INT           NULL,
    [Status]                 INT           NULL,
    [IsDirectInquiry]        BIT           NULL,
    [FlagId]                 INT           NULL,
    [InquiryCode]            VARCHAR (100) NULL,
    [CreatedBy]              INT           NULL,
    [BrokerContactCompanyId] INT           NULL,
    [OrganisationId]         BIGINT        NULL,
    CONSTRAINT [PK_tbl_Inquiry] PRIMARY KEY CLUSTERED ([InquiryId] ASC),
    CONSTRAINT [FK_tbl_Inquiry_tbl_contacts] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[CompanyContact] ([ContactId])
);

