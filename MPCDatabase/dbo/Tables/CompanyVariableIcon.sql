CREATE TABLE [dbo].[CompanyVariableIcon] (
    [VariableIconId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [VariableId]       BIGINT        NULL,
    [Icon]             VARCHAR (255) NULL,
    [ContactCompanyId] BIGINT        NULL
);

