CREATE TABLE [dbo].[CustomizedFieldsData] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [FieldID]    INT           NOT NULL,
    [CustomerId] INT           CONSTRAINT [DF__tbl_custo__Custo__531856C7] DEFAULT (0) NOT NULL,
    [ISCustomer] SMALLINT      CONSTRAINT [DF__tbl_custo__ISCus__540C7B00] DEFAULT (0) NOT NULL,
    [Value]      VARCHAR (255) NULL,
    [CompanyId]  INT           CONSTRAINT [DF__tbl_custo__Compa__55009F39] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_customizedfieldsdata] PRIMARY KEY CLUSTERED ([Id] ASC)
);

