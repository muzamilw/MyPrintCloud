CREATE TABLE [dbo].[StockCategory] (
    [CategoryId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [Code]           VARCHAR (5)   NULL,
    [Name]           VARCHAR (50)  NULL,
    [Description]    VARCHAR (255) NULL,
    [fixed]          SMALLINT      CONSTRAINT [DF__tbl_stock__fixed__2CA8951C] DEFAULT ((0)) NOT NULL,
    [ItemWeight]     SMALLINT      CONSTRAINT [DF__tbl_stock__ItemW__2D9CB955] DEFAULT ((0)) NOT NULL,
    [ItemColour]     SMALLINT      CONSTRAINT [DF__tbl_stock__ItemC__2E90DD8E] DEFAULT ((0)) NOT NULL,
    [ItemSizeCustom] SMALLINT      CONSTRAINT [DF__tbl_stock__ItemS__2F8501C7] DEFAULT ((0)) NOT NULL,
    [ItemPaperSize]  SMALLINT      CONSTRAINT [DF__tbl_stock__ItemP__30792600] DEFAULT ((0)) NOT NULL,
    [ItemCoatedType] SMALLINT      CONSTRAINT [DF__tbl_stock__ItemC__316D4A39] DEFAULT ((0)) NOT NULL,
    [ItemCoated]     SMALLINT      NULL,
    [ItemExposure]   SMALLINT      CONSTRAINT [DF__tbl_stock__ItemE__32616E72] DEFAULT ((0)) NOT NULL,
    [ItemCharge]     SMALLINT      CONSTRAINT [DF__tbl_stock__ItemC__335592AB] DEFAULT ((0)) NOT NULL,
    [recLock]        SMALLINT      CONSTRAINT [DF__tbl_stock__recLo__3449B6E4] DEFAULT ((0)) NOT NULL,
    [TaxId]          INT           NULL,
    [Flag1]          SMALLINT      NULL,
    [Flag2]          SMALLINT      NULL,
    [Flag3]          SMALLINT      NULL,
    [Flag4]          SMALLINT      NULL,
    [OrganisationId] BIGINT        CONSTRAINT [DF__tbl_stock__Compa__353DDB1D] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tbl_stockcategories] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [CategoryID]
    ON [dbo].[StockCategory]([CategoryId] ASC);

