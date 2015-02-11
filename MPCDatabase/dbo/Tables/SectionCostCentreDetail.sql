CREATE TABLE [dbo].[SectionCostCentreDetail] (
    [SectionCostCentreDetailId] INT            IDENTITY (1, 1) NOT NULL,
    [SectionCostCentreId]       INT            NULL,
    [StockId]                   BIGINT         NULL,
    [SupplierId]                INT            NULL,
    [Qty1]                      FLOAT (53)     NULL,
    [Qty2]                      FLOAT (53)     NULL,
    [Qty3]                      FLOAT (53)     NULL,
    [CostPrice]                 FLOAT (53)     NULL,
    [ActualQtyUsed]             INT            NULL,
    [StockName]                 NVARCHAR (150) NULL,
    [Supplier]                  NVARCHAR (150) NULL,
    CONSTRAINT [PK_tbl_section_costcentre_detail] PRIMARY KEY CLUSTERED ([SectionCostCentreDetailId] ASC),
    CONSTRAINT [FK_tbl_section_costcentre_detail_tbl_section_costcentres] FOREIGN KEY ([SectionCostCentreId]) REFERENCES [dbo].[SectionCostcentre] ([SectionCostcentreId]),
    CONSTRAINT [FK_tbl_section_costcentre_detail_tbl_stockitems] FOREIGN KEY ([StockId]) REFERENCES [dbo].[StockItem] ([StockItemId])
);

