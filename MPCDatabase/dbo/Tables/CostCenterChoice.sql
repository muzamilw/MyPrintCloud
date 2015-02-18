CREATE TABLE [dbo].[CostCenterChoice] (
    [CostCenterChoiceId] INT           IDENTITY (1, 1) NOT NULL,
    [ChoiceLabel]        VARCHAR (255) NULL,
    [ChoiceValue]        FLOAT (53)    NULL,
    [CostCenterId]       INT           NULL,
    [CostCenterOption]   INT           NULL,
    CONSTRAINT [PK_tbl_CostCenterChoices] PRIMARY KEY CLUSTERED ([CostCenterChoiceId] ASC)
);

