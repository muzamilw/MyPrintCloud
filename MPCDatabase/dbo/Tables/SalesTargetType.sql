CREATE TABLE [dbo].[SalesTargetType] (
    [SalesTargetTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Description]       VARCHAR (100) NULL,
    [Duration]          INT           CONSTRAINT [DF__tbl_sales__Durat__338A9CD5] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_sales_target_types] PRIMARY KEY CLUSTERED ([SalesTargetTypeId] ASC)
);

