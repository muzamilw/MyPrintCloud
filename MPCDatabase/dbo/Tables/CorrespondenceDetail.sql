CREATE TABLE [dbo].[CorrespondenceDetail] (
    [DetailId]         INT  IDENTITY (1, 1) NOT NULL,
    [CorrespondenceId] INT  NOT NULL,
    [Discriptions]     TEXT NULL,
    CONSTRAINT [PK_tbl_Correspondence_Details] PRIMARY KEY CLUSTERED ([DetailId] ASC)
);

