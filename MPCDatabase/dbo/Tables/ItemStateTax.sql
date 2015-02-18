CREATE TABLE [dbo].[ItemStateTax] (
    [ItemStateTaxId] BIGINT     IDENTITY (1, 1) NOT NULL,
    [CountryId]      BIGINT     NULL,
    [StateId]        BIGINT     NULL,
    [TaxRate]        FLOAT (53) NULL,
    [ItemId]         BIGINT     NULL,
    PRIMARY KEY CLUSTERED ([ItemStateTaxId] ASC),
    CONSTRAINT [FK_ItemStateTax_Country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([CountryID]),
    CONSTRAINT [FK_ItemStateTax_Items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId]),
    CONSTRAINT [FK_ItemStateTax_State] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([StateId])
);

