CREATE TABLE [dbo].[ProductMarketBriefAnswer] (
    [MarketBriefAnswerId]   INT            IDENTITY (1, 1) NOT NULL,
    [MarketBriefQuestionId] INT            NULL,
    [AnswerDetail]          NVARCHAR (500) NULL,
    CONSTRAINT [PK_tbl_ProductMarketBriefAnswers] PRIMARY KEY CLUSTERED ([MarketBriefAnswerId] ASC),
    CONSTRAINT [FK_tbl_ProductMarketBriefAnswers_tbl_ProductMarketBriefQuestions] FOREIGN KEY ([MarketBriefQuestionId]) REFERENCES [dbo].[ProductMarketBriefQuestion] ([MarketBriefQuestionId])
);

