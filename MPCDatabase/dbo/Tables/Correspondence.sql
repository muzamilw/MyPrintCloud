CREATE TABLE [dbo].[Correspondence] (
    [CorrespondenceId] INT           IDENTITY (1, 1) NOT NULL,
    [RefTableName]     VARCHAR (100) NULL,
    [RefFieldName]     VARCHAR (100) NULL,
    [RefKeyId]         INT           NULL,
    [Type]             SMALLINT      NULL,
    [Reference]        VARCHAR (100) NULL,
    [ReferenceId]      INT           NULL,
    [ReferenceType]    SMALLINT      NULL,
    [Title]            VARCHAR (255) NULL,
    [Date]             DATETIME      NULL,
    [Direction]        BIT           NULL,
    [Address]          VARCHAR (255) NULL,
    [UId]              INT           NULL,
    CONSTRAINT [PK_tbl_Emails_Correspondence] PRIMARY KEY CLUSTERED ([CorrespondenceId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1-Out', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Correspondence', @level2type = N'COLUMN', @level2name = N'Direction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calling Session  1-Campagins', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Correspondence', @level2type = N'COLUMN', @level2name = N'ReferenceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1-EMail Campagin', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Correspondence', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Correspondence', @level2type = N'COLUMN', @level2name = N'RefKeyId';

