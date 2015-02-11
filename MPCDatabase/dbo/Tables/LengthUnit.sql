CREATE TABLE [dbo].[LengthUnit] (
    [Id]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [UnitName] VARCHAR (100) NULL,
    [MM]       FLOAT (53)    NULL,
    [CM]       FLOAT (53)    NULL,
    [Inch]     FLOAT (53)    NULL,
    CONSTRAINT [PK_tbl_lengthconversion] PRIMARY KEY CLUSTERED ([Id] ASC)
);

