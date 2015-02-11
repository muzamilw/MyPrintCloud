CREATE TABLE [dbo].[PaperSize] (
    [PaperSizeId]    INT          IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (50) NULL,
    [Height]         FLOAT (53)   NULL,
    [Width]          FLOAT (53)   NULL,
    [SizeMeasure]    INT          NULL,
    [Area]           FLOAT (53)   NULL,
    [IsFixed]        INT          CONSTRAINT [DF__tbl_paper__IsFix__39788055] DEFAULT (0) NOT NULL,
    [Region]         VARCHAR (50) NOT NULL,
    [isArchived]     BIT          NULL,
    [OrganisationId] BIGINT       NULL,
    CONSTRAINT [PK_tbl_papersize] PRIMARY KEY CLUSTERED ([PaperSizeId] ASC)
);

