CREATE TABLE [dbo].[RegistrationQuestion] (
    [QuestionId] INT           IDENTITY (1, 1) NOT NULL,
    [Question]   VARCHAR (100) NULL,
    CONSTRAINT [PK_tbl_Registration_Questions] PRIMARY KEY CLUSTERED ([QuestionId] ASC)
);

