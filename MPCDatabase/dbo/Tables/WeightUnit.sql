CREATE TABLE [dbo].[WeightUnit] (
    [Id]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [UnitName] VARCHAR (100) NOT NULL,
    [Pound]    FLOAT (53)    NULL,
    [GSM]      FLOAT (53)    NULL,
    [KG]       FLOAT (53)    NULL,
    CONSTRAINT [PK_tbl_weightconversion] PRIMARY KEY CLUSTERED ([Id] ASC)
);

