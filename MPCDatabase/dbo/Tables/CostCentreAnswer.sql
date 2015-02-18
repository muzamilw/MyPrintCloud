CREATE TABLE [dbo].[CostCentreAnswer] (
    [Id]           INT        IDENTITY (1, 1) NOT NULL,
    [QuestionId]   INT        NULL,
    [AnswerString] FLOAT (53) NULL,
    CONSTRAINT [PK_tbl_costcentreanswers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

