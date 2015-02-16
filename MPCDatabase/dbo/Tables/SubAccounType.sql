CREATE TABLE [dbo].[SubAccounType] (
    [SubTypeId]   INT           IDENTITY (1, 1) NOT NULL,
    [TypeId]      INT           CONSTRAINT [DF__tbl_subac__TypeI__5792F321] DEFAULT (0) NOT NULL,
    [Name]        VARCHAR (150) NULL,
    [Description] VARCHAR (255) NULL,
    CONSTRAINT [PK_tbl_subaccountype] PRIMARY KEY CLUSTERED ([SubTypeId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [SubTypeID]
    ON [dbo].[SubAccounType]([SubTypeId] ASC);

