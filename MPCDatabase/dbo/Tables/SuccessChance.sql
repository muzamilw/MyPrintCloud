CREATE TABLE [dbo].[SuccessChance] (
    [SuccessChanceId] INT          IDENTITY (1, 1) NOT NULL,
    [Description]     VARCHAR (50) NULL,
    [Percentage]      INT          NULL,
    CONSTRAINT [PK_tbl_success_chance] PRIMARY KEY CLUSTERED ([SuccessChanceId] ASC)
);

