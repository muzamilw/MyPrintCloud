CREATE TABLE [dbo].[InkCoverageGroup] (
    [CoverageGroupId] INT          IDENTITY (1, 1) NOT NULL,
    [GroupName]       VARCHAR (50) NULL,
    [Percentage]      FLOAT (53)   NULL,
    [IsFixed]         INT          CONSTRAINT [DF__tbl_ink_c__IsFix__5E54FF49] DEFAULT (0) NOT NULL,
    [SystemSiteId]    INT          CONSTRAINT [DF__tbl_ink_c__Syste__603D47BB] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_ink_coverage_groups] PRIMARY KEY CLUSTERED ([CoverageGroupId] ASC)
);

