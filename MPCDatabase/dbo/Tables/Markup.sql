CREATE TABLE [dbo].[Markup] (
    [MarkUpId]       BIGINT       IDENTITY (1, 1) NOT NULL,
    [MarkUpName]     VARCHAR (50) NULL,
    [MarkUpRate]     FLOAT (53)   NULL,
    [IsFixed]        INT          CONSTRAINT [DF__tbl_marku__IsFix__1CDC41A7] DEFAULT ((0)) NULL,
    [IsDefault]      BIT          NULL,
    [OrganisationId] BIGINT       CONSTRAINT [DF_tbl_markup_UserDomainKey] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tbl_markup] PRIMARY KEY CLUSTERED ([MarkUpId] ASC)
);

