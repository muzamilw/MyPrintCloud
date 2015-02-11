CREATE TABLE [dbo].[CompanyDomain] (
    [CompanyDomainId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [Domain]          NVARCHAR (MAX) NULL,
    [CompanyId]       BIGINT         NULL,
    CONSTRAINT [PK__CompanyD__235FFF863B213986] PRIMARY KEY CLUSTERED ([CompanyDomainId] ASC),
    CONSTRAINT [FK_CompanyDomain_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId])
);

