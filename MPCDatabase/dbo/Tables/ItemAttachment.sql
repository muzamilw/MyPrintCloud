CREATE TABLE [dbo].[ItemAttachment] (
    [ItemAttachmentId] BIGINT       IDENTITY (1, 1) NOT NULL,
    [FileTitle]        VARCHAR (50) NULL,
    [isFromCustomer]   SMALLINT     CONSTRAINT [DF__tbl_item___isFro__7CD98669] DEFAULT ((0)) NULL,
    [CompanyId]        BIGINT       CONSTRAINT [DF__tbl_item___Custo__7DCDAAA2] DEFAULT ((0)) NULL,
    [ContactId]        BIGINT       CONSTRAINT [DF__tbl_item___Conta__7EC1CEDB] DEFAULT ((0)) NULL,
    [SystemUserId]     INT          CONSTRAINT [DF__tbl_item___Syste__7FB5F314] DEFAULT ((0)) NULL,
    [UploadDate]       DATETIME     NULL,
    [UploadTime]       DATETIME     CONSTRAINT [DF__tbl_item___Uploa__00AA174D] DEFAULT ('1999-11-30') NULL,
    [Version]          INT          CONSTRAINT [DF__tbl_item___Versi__019E3B86] DEFAULT ((0)) NULL,
    [ItemId]           BIGINT       CONSTRAINT [DF__tbl_item___ItemI__02925FBF] DEFAULT ((0)) NULL,
    [Comments]         TEXT         NULL,
    [Type]             VARCHAR (50) NULL,
    [IsApproved]       SMALLINT     CONSTRAINT [DF__tbl_item___IsApp__038683F8] DEFAULT ((0)) NULL,
    [FileName]         TEXT         NULL,
    [FolderPath]       TEXT         NULL,
    [FileType]         VARCHAR (50) NULL,
    [ContentType]      VARCHAR (50) NULL,
    [Parent]           INT          NULL,
    [ApproveDate]      DATETIME     NULL,
    CONSTRAINT [PK_tbl_item_attachments] PRIMARY KEY CLUSTERED ([ItemAttachmentId] ASC),
    CONSTRAINT [FK_tbl_item_attachments_tbl_items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId]) ON DELETE CASCADE
);

