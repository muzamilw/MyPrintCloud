CREATE TABLE [dbo].[SectionInkCoverage] (
    [Id]              INT IDENTITY (1, 1) NOT NULL,
    [SectionId]       INT NULL,
    [InkOrder]        INT NULL,
    [InkId]           INT NULL,
    [CoverageGroupId] INT NULL,
    [Side]            INT NULL,
    CONSTRAINT [PK_tbl_section_inkcoverage] PRIMARY KEY CLUSTERED ([Id] ASC)
);

