CREATE TABLE [dbo].[CompanyBanner] (
    [CompanyBannerId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [PageId]          INT           NULL,
    [ImageURL]        VARCHAR (200) NULL,
    [Heading]         VARCHAR (100) NULL,
    [Description]     VARCHAR (500) NULL,
    [ItemURL]         VARCHAR (200) NULL,
    [ButtonURL]       VARCHAR (200) NULL,
    [isActive]        BIT           CONSTRAINT [DF_tbl_cmsPageBanners_isActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]       INT           NULL,
    [CreateDate]      DATETIME      CONSTRAINT [DF_tbl_cmsPageBanners_CreatDate] DEFAULT (getdate()) NULL,
    [ModifyId]        INT           NULL,
    [ModifyDate]      DATETIME      NULL,
    [CompanySetId]    BIGINT        NULL,
    CONSTRAINT [PK_tbl_cmsPageBanners] PRIMARY KEY CLUSTERED ([CompanyBannerId] ASC),
    CONSTRAINT [FK_CompanyBanner_CompanySet] FOREIGN KEY ([CompanySetId]) REFERENCES [dbo].[CompanyBannerSet] ([CompanySetId])
);

