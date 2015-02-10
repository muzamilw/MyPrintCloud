CREATE TABLE [dbo].[AccountType] (
    [TypeId]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (100) NOT NULL,
    [Description] VARCHAR (255) NULL,
    CONSTRAINT [PK_tbl_accounttype] PRIMARY KEY CLUSTERED ([TypeId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [indx_TypeID]
    ON [dbo].[AccountType]([TypeId] ASC);

