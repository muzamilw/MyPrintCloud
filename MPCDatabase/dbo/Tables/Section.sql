CREATE TABLE [dbo].[Section] (
    [SectionId]    INT           NOT NULL,
    [SectionName]  VARCHAR (100) NOT NULL,
    [SecOrder]     INT           NOT NULL,
    [ParentId]     INT           NULL,
    [href]         VARCHAR (50)  NOT NULL,
    [SectionImage] VARCHAR (50)  NULL,
    [Independent]  BIT           NOT NULL,
    CONSTRAINT [PK_tbl_sections] PRIMARY KEY CLUSTERED ([SectionId] ASC)
);

