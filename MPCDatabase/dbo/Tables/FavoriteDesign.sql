CREATE TABLE [dbo].[FavoriteDesign] (
    [FavoriteDesignId] INT    IDENTITY (1, 1) NOT NULL,
    [TemplateId]       INT    NOT NULL,
    [ItemId]           BIGINT NOT NULL,
    [ContactUserId]    BIGINT NOT NULL,
    [IsFavorite]       BIT    NOT NULL,
    [CategoryId]       INT    NOT NULL,
    CONSTRAINT [PK_tbl_FavoriteDesign] PRIMARY KEY CLUSTERED ([FavoriteDesignId] ASC),
    CONSTRAINT [FK_tbl_FavoriteDesign_tbl_contacts] FOREIGN KEY ([ContactUserId]) REFERENCES [dbo].[CompanyContact] ([ContactId]),
    CONSTRAINT [FK_tbl_FavoriteDesign_tbl_items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId])
);

