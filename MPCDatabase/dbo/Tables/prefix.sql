﻿CREATE TABLE [dbo].[prefix] (
    [EstimatePrefix]      VARCHAR (5)  NULL,
    [EstimateStart]       BIGINT       NULL,
    [EstimateNext]        BIGINT       NULL,
    [InvoicePrefix]       VARCHAR (5)  NULL,
    [InvoiceStart]        BIGINT       NULL,
    [InvoiceNext]         BIGINT       NULL,
    [JobPrefix]           VARCHAR (5)  NULL,
    [JobStart]            BIGINT       NULL,
    [JobNext]             BIGINT       NULL,
    [POPrefix]            VARCHAR (5)  NULL,
    [POStart]             BIGINT       NULL,
    [PONext]              BIGINT       NULL,
    [GOFPrefix]           VARCHAR (5)  NULL,
    [GOFStart]            BIGINT       NULL,
    [GOFNext]             BIGINT       NULL,
    [ITEMPrefix]          VARCHAR (5)  NOT NULL,
    [ITEMStart]           BIGINT       NULL,
    [ITEMNext]            BIGINT       NULL,
    [DeliveryNPrefix]     VARCHAR (5)  NOT NULL,
    [DeliveryNStart]      BIGINT       NULL,
    [DeliveryNNext]       BIGINT       NULL,
    [JobCardPrefix]       VARCHAR (5)  NOT NULL,
    [JobCardStart]        BIGINT       NULL,
    [JobCardNext]         BIGINT       NULL,
    [GRNPrefix]           VARCHAR (50) NULL,
    [GRNStart]            BIGINT       NULL,
    [GRNNext]             BIGINT       NULL,
    [ProductPrefix]       VARCHAR (50) NULL,
    [ProductStart]        BIGINT       NULL,
    [ProductNext]         BIGINT       NULL,
    [FinishedGoodsPrefix] VARCHAR (50) NOT NULL,
    [FinishedGoodsStart]  BIGINT       CONSTRAINT [DF__tbl_prefi__Finis__44EA3301] DEFAULT ((270592647664.)) NOT NULL,
    [FinishedGoodsNext]   BIGINT       CONSTRAINT [DF__tbl_prefi__Finis__45DE573A] DEFAULT ((270592647520.)) NOT NULL,
    [OrderPrefix]         VARCHAR (50) NOT NULL,
    [OrderStart]          BIGINT       CONSTRAINT [DF__tbl_prefi__Order__46D27B73] DEFAULT ((270592650256.)) NOT NULL,
    [OrderNext]           BIGINT       CONSTRAINT [DF__tbl_prefi__Order__47C69FAC] DEFAULT ((270592650192.)) NOT NULL,
    [EnquiryPrefix]       VARCHAR (50) NOT NULL,
    [EnquiryStart]        BIGINT       CONSTRAINT [DF__tbl_prefi__Enqui__48BAC3E5] DEFAULT ((270592650032.)) NOT NULL,
    [EnquiryNext]         BIGINT       CONSTRAINT [DF__tbl_prefi__Enqui__49AEE81E] DEFAULT ((270592649952.)) NOT NULL,
    [StockItemPrefix]     VARCHAR (50) NULL,
    [StockItemStart]      BIGINT       NULL,
    [StockItemNext]       BIGINT       NULL,
    [SystemSiteID]        INT          CONSTRAINT [DF__tbl_prefi__Syste__4AA30C57] DEFAULT ((0)) NOT NULL,
    [PrefixId]            BIGINT       IDENTITY (1, 1) NOT NULL,
    [DepartmentId]        INT          NULL,
    [MarkupId]            BIGINT       NULL,
    [JobManagerId]        INT          NULL,
    [OrderManagerId]      INT          NULL,
    [OrganisationId]      BIGINT       NULL,
    CONSTRAINT [PK_tbl_prefixes] PRIMARY KEY CLUSTERED ([PrefixId] ASC),
    CONSTRAINT [FK_tbl_prefixes_tbl_markup] FOREIGN KEY ([MarkupId]) REFERENCES [dbo].[Markup] ([MarkUpId])
);

