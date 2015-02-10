CREATE TABLE [dbo].[CompanyCostCentre] (
    [CompanyCostCenterId] BIGINT     IDENTITY (1, 1) NOT NULL,
    [CompanyId]           BIGINT     NULL,
    [CostCentreId]        BIGINT     NULL,
    [BrokerMarkup]        FLOAT (53) NULL,
    [ContactMarkup]       FLOAT (53) NULL,
    [isDisplayToUser]     BIT        NULL,
    [OrganisationId]      BIGINT     NULL,
    CONSTRAINT [PK_tbl_ContactCompanyCostCenters] PRIMARY KEY CLUSTERED ([CompanyCostCenterId] ASC),
    CONSTRAINT [FK_CompanyCostCentre_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId]),
    CONSTRAINT [FK_CompanyCostCentre_CostCentre] FOREIGN KEY ([CostCentreId]) REFERENCES [dbo].[CostCentre] ([CostCentreId])
);

