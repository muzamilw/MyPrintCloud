CREATE TABLE [dbo].[CompanyType] (
    [TypeId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [IsFixed]  BIT           NULL,
    [TypeName] VARCHAR (100) NULL,
    CONSTRAINT [PK_tbl_customertypes] PRIMARY KEY CLUSTERED ([TypeId] ASC)
);

