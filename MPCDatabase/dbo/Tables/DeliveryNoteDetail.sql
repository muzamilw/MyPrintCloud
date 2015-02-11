CREATE TABLE [dbo].[DeliveryNoteDetail] (
    [DeliveryDetailid] INT        IDENTITY (1, 1) NOT NULL,
    [DeliveryNoteId]   INT        CONSTRAINT [DF_tbl_deliverynote_details_DeliveryNoteID] DEFAULT ((0)) NOT NULL,
    [Description]      TEXT       NULL,
    [ItemId]           BIGINT     CONSTRAINT [DF_tbl_deliverynote_details_ItemId] DEFAULT ((0)) NULL,
    [ItemQty]          INT        CONSTRAINT [DF_tbl_deliverynote_details_ItemQty] DEFAULT ((0)) NULL,
    [GrossItemTotal]   FLOAT (53) CONSTRAINT [DF_tbl_deliverynote_details_GrossItemTotal] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_tbl_deliverynote_details] PRIMARY KEY CLUSTERED ([DeliveryDetailid] ASC),
    CONSTRAINT [FK_tbl_deliverynote_details_tbl_deliverynotes] FOREIGN KEY ([DeliveryNoteId]) REFERENCES [dbo].[DeliveryNote] ([DeliveryNoteId]),
    CONSTRAINT [FK_tbl_deliverynote_details_tbl_items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId]) ON DELETE CASCADE
);

