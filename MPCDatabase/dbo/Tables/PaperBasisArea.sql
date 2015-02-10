CREATE TABLE [dbo].[PaperBasisArea] (
    [PaperBasisAreaId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [Value]            FLOAT (53)    CONSTRAINT [DF__tbl_paper__Value__35A7EF71] DEFAULT (0) NOT NULL,
    [IsSystem]         SMALLINT      CONSTRAINT [DF__tbl_paper__IsSys__369C13AA] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_paperbasisarea] PRIMARY KEY CLUSTERED ([PaperBasisAreaId] ASC)
);

