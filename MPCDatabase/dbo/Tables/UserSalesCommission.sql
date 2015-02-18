CREATE TABLE [dbo].[UserSalesCommission] (
    [UserCommissionId]      INT           IDENTITY (1, 1) NOT NULL,
    [SalesCommissionTypeId] INT           NULL,
    [CommissionLookUpId]    INT           NULL,
    [SystemUserId]          INT           NULL,
    [FlatValue]             FLOAT (53)    NULL,
    [FlatDescription]       VARCHAR (100) NULL,
    [LastUpdatedDate]       DATETIME      NULL,
    [LastUpdatedBy]         INT           NULL,
    CONSTRAINT [PK_tbl_user_sales_commission] PRIMARY KEY CLUSTERED ([UserCommissionId] ASC)
);

