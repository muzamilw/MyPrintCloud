CREATE TABLE [dbo].[UserStickyNote] (
    [NoteId]          INT            IDENTITY (1, 1) NOT NULL,
    [NoteDescription] VARCHAR (5000) NULL,
    [UserId]          INT            NULL,
    [CreateDate]      DATETIME       CONSTRAINT [DF_tbl_UserStickyNotes_CreateDate] DEFAULT (getdate()) NOT NULL,
    [ModifyDate]      DATETIME       NULL,
    CONSTRAINT [PK_tbl_UserStickyNotes] PRIMARY KEY CLUSTERED ([NoteId] ASC)
);

