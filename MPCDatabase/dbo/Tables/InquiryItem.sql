CREATE TABLE [dbo].[InquiryItem] (
    [InquiryItemId] INT            IDENTITY (1, 1) NOT NULL,
    [Title]         VARCHAR (100)  NOT NULL,
    [Notes]         VARCHAR (5000) NULL,
    [DeliveryDate]  DATE           NOT NULL,
    [InquiryId]     INT            NOT NULL,
    [ProductId]     INT            NULL,
    CONSTRAINT [PK_tbl_Inquiry_Items] PRIMARY KEY CLUSTERED ([InquiryItemId] ASC),
    CONSTRAINT [FK_tbl_Inquiry_Items_tbl_Inquiry] FOREIGN KEY ([InquiryId]) REFERENCES [dbo].[Inquiry] ([InquiryId])
);

