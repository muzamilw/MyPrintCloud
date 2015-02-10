CREATE TABLE [dbo].[BusinessTypes] (
    [TypeId]          INT          IDENTITY (1, 1) NOT NULL,
    [TypeName]        VARCHAR (50) NOT NULL,
    [TypeDescription] VARCHAR (50) NULL,
    CONSTRAINT [PK_tbl_businesstypes] PRIMARY KEY CLUSTERED ([TypeId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [TypeID]
    ON [dbo].[BusinessTypes]([TypeId] ASC);

