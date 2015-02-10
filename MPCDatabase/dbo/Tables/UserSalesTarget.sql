CREATE TABLE [dbo].[UserSalesTarget] (
    [UserSalesTargetId] INT              IDENTITY (1, 1) NOT NULL,
    [SalesTargetTypeId] INT              NULL,
    [SystemUserId]      UNIQUEIDENTIFIER NULL,
    [StartDate]         DATETIME         NULL,
    [EndDate]           DATETIME         NULL,
    [SalesTarget]       FLOAT (53)       NULL,
    [Month]             INT              NULL,
    [Year]              INT              NULL,
    CONSTRAINT [PK_tbl_user_sales_targets] PRIMARY KEY CLUSTERED ([UserSalesTargetId] ASC)
);

