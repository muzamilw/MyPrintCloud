CREATE TABLE [dbo].[MediaLibrary] (
    [MediaId]   BIGINT          IDENTITY (1, 1) NOT NULL,
    [FilePath]  NVARCHAR (MAX)  NOT NULL,
    [FileName]  NVARCHAR (1000) NOT NULL,
    [FileType]  NVARCHAR (100)  NOT NULL,
    [CompanyId] BIGINT          NOT NULL,
    CONSTRAINT [PK_MediaLibrary] PRIMARY KEY CLUSTERED ([MediaId] ASC),
    CONSTRAINT [FK_MediaLibrary_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId])
);

