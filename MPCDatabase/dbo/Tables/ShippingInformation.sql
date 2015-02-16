CREATE TABLE [dbo].[ShippingInformation] (
    [ShippingId]         INT        IDENTITY (1, 1) NOT NULL,
    [ItemId]             INT        CONSTRAINT [DF__tbl_shipp__ItemI__1D66518C] DEFAULT (0) NULL,
    [AddressId]          INT        CONSTRAINT [DF__tbl_shipp__Addre__1E5A75C5] DEFAULT (0) NULL,
    [Quantity]           INT        CONSTRAINT [DF__tbl_shipp__Quant__1F4E99FE] DEFAULT (0) NULL,
    [Price]              FLOAT (53) CONSTRAINT [DF__tbl_shipp__Price__2042BE37] DEFAULT (0) NULL,
    [DeliveryNoteRaised] BIT        CONSTRAINT [DF_tbl_shippinginformation_DeliveryNoteRaised] DEFAULT (0) NULL,
    [DeliveryDate]       DATETIME   NOT NULL,
    CONSTRAINT [PK_tbl_shippinginformation] PRIMARY KEY CLUSTERED ([ShippingId] ASC)
);

