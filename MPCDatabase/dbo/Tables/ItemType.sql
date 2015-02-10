CREATE TABLE [dbo].[ItemType] (
    [ItemTypeId] INT           NOT NULL,
    [TypeName]   VARCHAR (150) NULL,
    [TypeDesc]   VARCHAR (250) NULL,
    CONSTRAINT [PK_tbl_item_Type] PRIMARY KEY CLUSTERED ([ItemTypeId] ASC)
);

