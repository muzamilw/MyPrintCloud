﻿CREATE TABLE [dbo].[Report] (
    [ReportId]               INT           IDENTITY (1, 1) NOT NULL,
    [Name]                   VARCHAR (50)  NULL,
    [Path]                   VARCHAR (100) NULL,
    [FileName]               VARCHAR (50)  NULL,
    [CategoryId]             INT           CONSTRAINT [DF__tbl_repor__Categ__150615B5] DEFAULT (0) NOT NULL,
    [Description]            VARCHAR (50)  NULL,
    [ReportDataSource]       NTEXT         NULL,
    [NameSpace]              VARCHAR (100) NOT NULL,
    [IsExternal]             INT           CONSTRAINT [DF__tbl_repor__IsExt__15FA39EE] DEFAULT (0) NOT NULL,
    [IsFixed]                INT           CONSTRAINT [DF__tbl_repor__IsFix__16EE5E27] DEFAULT (0) NOT NULL,
    [ReportTemplate]         TEXT          NULL,
    [ReportTemplateOriginal] TEXT          NULL,
    [IsEditable]             SMALLINT      CONSTRAINT [DF__tbl_repor__IsEdi__17E28260] DEFAULT (0) NOT NULL,
    [ParentReportId]         INT           CONSTRAINT [DF__tbl_repor__Paren__18D6A699] DEFAULT (0) NOT NULL,
    [IsByReflection]         SMALLINT      CONSTRAINT [DF__tbl_repor__IsByR__19CACAD2] DEFAULT (0) NOT NULL,
    [CompanyId]              INT           CONSTRAINT [DF_tbl_reports_CompanyID] DEFAULT (0) NOT NULL,
    [IsSystemReport]         SMALLINT      CONSTRAINT [DF_tbl_reports_IsSystemReport] DEFAULT (0) NULL,
    [ReportOrder]            INT           CONSTRAINT [DF_tbl_reports_ReportOrder] DEFAULT (0) NOT NULL,
    [HasSubReport]           BIT           NULL,
    [SubReportTemplate]      TEXT          NULL,
    [SubReportDataSource]    TEXT          NULL,
    [OrganisationId]         BIGINT        NULL,
    CONSTRAINT [PK_tbl_reports] PRIMARY KEY CLUSTERED ([ReportId] ASC),
    CONSTRAINT [FK_tbl_reports_tbl_reportcategory] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[ReportCategory] ([CategoryId])
);

