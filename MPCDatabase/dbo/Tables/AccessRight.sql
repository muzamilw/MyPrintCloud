CREATE TABLE [dbo].[AccessRight] (
    [RightId]     INT           IDENTITY (1, 1) NOT NULL,
    [SectionId]   INT           CONSTRAINT [DF__tbl_right__Secti__1CA7377D] DEFAULT (0) NOT NULL,
    [RightName]   VARCHAR (100) NOT NULL,
    [Description] VARCHAR (100) NULL,
    CONSTRAINT [PK_tbl_rights] PRIMARY KEY CLUSTERED ([RightId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [SectionID]
    ON [dbo].[AccessRight]([SectionId] ASC);

