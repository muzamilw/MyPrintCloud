CREATE TABLE [dbo].[ProductMarketBriefQuestion] (
    [MarketBriefQuestionId] INT            IDENTITY (1, 1) NOT NULL,
    [ItemId]                INT            NULL,
    [QuestionDetail]        NVARCHAR (500) NULL,
    [SortOrder]             INT            CONSTRAINT [DF_tbl_ProductMarketBriefQuestions_SortOrder] DEFAULT ((0)) NOT NULL,
    [isMultipleSelction]    BIT            NULL,
    CONSTRAINT [PK_tbl_ProductMarketBriefQuestions] PRIMARY KEY CLUSTERED ([MarketBriefQuestionId] ASC)
);

