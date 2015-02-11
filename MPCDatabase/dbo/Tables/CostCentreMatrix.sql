CREATE TABLE [dbo].[CostCentreMatrix] (
    [MatrixId]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (50)  NOT NULL,
    [Description]  VARCHAR (255) NULL,
    [RowsCount]    INT           CONSTRAINT [DF__tbl_costc__RowsC__5812160E] DEFAULT (0) NOT NULL,
    [ColumnsCount] INT           CONSTRAINT [DF__tbl_costc__Colum__59063A47] DEFAULT (0) NOT NULL,
    [CompanyId]    INT           CONSTRAINT [DF__tbl_costc__Compa__59FA5E80] DEFAULT (0) NOT NULL,
    [SystemSiteId] INT           CONSTRAINT [DF__tbl_costc__Syste__5AEE82B9] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_costcentrematrices] PRIMARY KEY CLUSTERED ([MatrixId] ASC)
);

