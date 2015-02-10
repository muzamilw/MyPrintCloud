CREATE TABLE [dbo].[ListingVendor] (
    [VendorId]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [FirstName]         VARCHAR (200) NULL,
    [LastName]          VARCHAR (200) NULL,
    [Solutation]        VARCHAR (200) NULL,
    [MailingSolutation] VARCHAR (200) NULL,
    [Company]           VARCHAR (200) NULL,
    [Email]             VARCHAR (255) NULL,
    [Phone]             VARCHAR (50)  NULL,
    [Mobile]            VARCHAR (50)  NULL,
    [ListingId]         BIGINT        NULL,
    CONSTRAINT [PK_ListingVendor] PRIMARY KEY CLUSTERED ([VendorId] ASC)
);

