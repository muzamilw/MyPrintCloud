CREATE TABLE [dbo].[InquiryAttachment] (
    [AttachmentId]    INT           IDENTITY (1, 1) NOT NULL,
    [OrignalFileName] VARCHAR (500) NOT NULL,
    [AttachmentPath]  VARCHAR (MAX) NOT NULL,
    [InquiryId]       INT           NOT NULL,
    [Extension]       VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_tbl_Inquiry_Attachments] PRIMARY KEY CLUSTERED ([AttachmentId] ASC),
    CONSTRAINT [FK_tbl_Inquiry_Attachments_tbl_Inquiry] FOREIGN KEY ([InquiryId]) REFERENCES [dbo].[Inquiry] ([InquiryId])
);

