CREATE TABLE [dbo].[CostCentreQuestion] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [QuestionString] VARCHAR (150) NULL,
    [Type]           SMALLINT      CONSTRAINT [DF__tbl_costce__Type__619B8048] DEFAULT (0) NULL,
    [DefaultAnswer]  VARCHAR (100) NULL,
    [CompanyId]      INT           CONSTRAINT [DF__tbl_costc__Compa__628FA481] DEFAULT (0) NOT NULL,
    [SystemSiteId]   INT           CONSTRAINT [DF__tbl_costc__Syste__6383C8BA] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_costcentrequestions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

