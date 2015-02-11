CREATE TABLE [dbo].[RaveReview] (
    [ReviewId]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [ReviewBy]       VARCHAR (100) NULL,
    [Review]         NTEXT         NULL,
    [ReviewDate]     DATETIME      CONSTRAINT [DF_tbl_RaveReviews_ReviewDate] DEFAULT (getdate()) NULL,
    [isDisplay]      BIT           NULL,
    [SortOrder]      INT           NULL,
    [OrganisationId] BIGINT        NULL,
    [CompanyId]      BIGINT        NULL,
    CONSTRAINT [PK_tbl_RaveReviews] PRIMARY KEY CLUSTERED ([ReviewId] ASC),
    CONSTRAINT [FK_RaveReview_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId])
);

