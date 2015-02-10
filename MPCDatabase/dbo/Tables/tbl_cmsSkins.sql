CREATE TABLE [dbo].[tbl_cmsSkins] (
    [SkinId]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [SkinName]    VARCHAR (50)   NULL,
    [ActualName]  VARCHAR (50)   NULL,
    [Height]      INT            NULL,
    [Width]       INT            NULL,
    [JsFile1Path] NVARCHAR (500) NULL,
    [JsFile2Path] NVARCHAR (500) NULL,
    [Jsfile3Path] NVARCHAR (500) NULL,
    [Jsfile4Path] NVARCHAR (500) NULL,
    CONSTRAINT [PK_tbl_cmsSkins] PRIMARY KEY CLUSTERED ([SkinId] ASC)
);

