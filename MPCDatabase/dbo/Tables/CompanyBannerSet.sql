CREATE TABLE [dbo].[CompanyBannerSet] (
    [CompanySetId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [SetName]        NVARCHAR (200) NULL,
    [CompanyId]      BIGINT         NULL,
    [OrganisationId] BIGINT         NULL,
    CONSTRAINT [PK__CompanyS__C74013C2E667DA8C] PRIMARY KEY CLUSTERED ([CompanySetId] ASC),
    CONSTRAINT [FK_CompanySet_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId])
);

