CREATE TABLE [dbo].[PageCategory] (
    [CategoryId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [CategoryName] VARCHAR (255) NULL,
    CONSTRAINT [PK_tbl_cmsPageCategory] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);

