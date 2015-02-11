CREATE TABLE [dbo].[SalesCommissionType] (
    [CommissionTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [Description]      VARCHAR (100) NULL,
    CONSTRAINT [PK_tbl_sales_commission_types] PRIMARY KEY CLUSTERED ([CommissionTypeId] ASC)
);

