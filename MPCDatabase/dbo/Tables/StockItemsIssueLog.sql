CREATE TABLE [dbo].[StockItemsIssueLog] (
    [Id]                        BIGINT   IDENTITY (1, 1) NOT NULL,
    [ItemId]                    INT      NULL,
    [SectionCostCentreId]       INT      NULL,
    [SectionCostCentreDetailId] INT      NULL,
    [StockID]                   INT      NULL,
    [IssuedQty]                 INT      NULL,
    [IssuedBy]                  INT      NULL,
    [IssueDateTime]             DATETIME NULL
);

