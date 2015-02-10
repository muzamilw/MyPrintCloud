CREATE TABLE [dbo].[Estimate] (
    [EstimateId]                         BIGINT           IDENTITY (1, 1) NOT NULL,
    [Estimate_Code]                      VARCHAR (100)    NULL,
    [Estimate_Name]                      VARCHAR (255)    NULL,
    [EnquiryId]                          INT              NULL,
    [CompanyId]                          BIGINT           CONSTRAINT [DF__tbl_estim__Custo__1C873BEC] DEFAULT ((0)) NOT NULL,
    [ContactId]                          BIGINT           CONSTRAINT [DF__tbl_estim__Conta__1D7B6025] DEFAULT ((0)) NULL,
    [StatusId]                           SMALLINT         CONSTRAINT [DF_tbl_estimates_Status] DEFAULT ((1)) NULL,
    [Estimate_Total]                     FLOAT (53)       NULL,
    [Estimate_ValidUpto]                 INT              CONSTRAINT [DF__tbl_estim__Estim__1E6F845E] DEFAULT ((0)) NULL,
    [UserNotes]                          TEXT             NULL,
    [LastUpdatedBy]                      UNIQUEIDENTIFIER NULL,
    [CreationDate]                       DATETIME         NULL,
    [CreationTime]                       DATETIME         CONSTRAINT [DF__tbl_estim__Creat__2057CCD0] DEFAULT ('1999-11-30') NOT NULL,
    [Created_by]                         UNIQUEIDENTIFIER NULL,
    [SalesPersonId]                      UNIQUEIDENTIFIER NULL,
    [HeadNotes]                          NVARCHAR (2000)  NULL,
    [FootNotes]                          NVARCHAR (2000)  NULL,
    [EstimateDate]                       DATETIME         NULL,
    [ProjectionDate]                     DATETIME         NULL,
    [Greeting]                           VARCHAR (50)     NULL,
    [AccountNumber]                      VARCHAR (50)     NULL,
    [OrderNo]                            VARCHAR (50)     NULL,
    [SuccessChanceId]                    INT              NULL,
    [LockedBy]                           INT              CONSTRAINT [DF__tbl_estim__Locke__2334397B] DEFAULT ((0)) NOT NULL,
    [AddressId]                          INT              CONSTRAINT [DF__tbl_estim__Addre__24285DB4] DEFAULT ((0)) NOT NULL,
    [CompanyName]                        VARCHAR (255)    NOT NULL,
    [SectionFlagId]                      INT              CONSTRAINT [DF__tbl_estima__flag__251C81ED] DEFAULT ((1)) NOT NULL,
    [SourceId]                           INT              CONSTRAINT [DF_tbl_estimates_SourceID] DEFAULT ((1)) NULL,
    [ProductId]                          INT              CONSTRAINT [DF_tbl_estimates_ProductID] DEFAULT ((1)) NULL,
    [IsInPipeLine]                       BIT              NULL,
    [Order_Code]                         VARCHAR (50)     NULL,
    [Order_Date]                         DATETIME         NULL,
    [Order_CreationDateTime]             DATETIME         NULL,
    [Order_DeliveryDate]                 DATETIME         NULL,
    [Order_ConfirmationDate]             DATETIME         NULL,
    [Order_Status]                       SMALLINT         CONSTRAINT [DF__tbl_estim__Order__28ED12D1] DEFAULT ((0)) NOT NULL,
    [Order_CompletionDate]               DATETIME         NULL,
    [OrderManagerId]                     UNIQUEIDENTIFIER NULL,
    [ArtworkByDate]                      DATETIME         NULL,
    [DataByDate]                         DATETIME         NULL,
    [TargetPrintDate]                    DATETIME         NULL,
    [StartDeliveryDate]                  DATETIME         NULL,
    [PaperByDate]                        DATETIME         NULL,
    [TargetBindDate]                     DATETIME         NULL,
    [FinishDeliveryDate]                 DATETIME         NULL,
    [Classification1Id]                  INT              NULL,
    [Classification2ID]                  INT              NULL,
    [IsOfficialOrder]                    INT              NULL,
    [CustomerPO]                         VARCHAR (50)     NULL,
    [OfficialOrderSetBy]                 UNIQUEIDENTIFIER NULL,
    [OfficialOrderSetOnDateTime]         DATETIME         NULL,
    [IsCreditApproved]                   INT              NULL,
    [CreditLimitForJob]                  FLOAT (53)       NULL,
    [CreditLimitSetBy]                   INT              NULL,
    [CreditLimitSetOnDateTime]           DATETIME         NULL,
    [IsJobAllowedWOCreditCheck]          INT              NULL,
    [AllowJobWOCreditCheckSetBy]         INT              NULL,
    [AllowJobWOCreditCheckSetOnDateTime] DATETIME         NULL,
    [NotesUpdateDateTime]                DATETIME         NULL,
    [NotesUpdatedByUserId]               INT              NULL,
    [OrderSourceId]                      SMALLINT         NULL,
    [IsRead]                             BIT              CONSTRAINT [DF_tbl_estimates_IsRead] DEFAULT ((0)) NOT NULL,
    [EstimateSentTo]                     SMALLINT         CONSTRAINT [DF_tbl_estimates_EstimateSentTo] DEFAULT ((0)) NULL,
    [EstimateValueChanged]               BIT              CONSTRAINT [DF_tbl_estimates_EstimateValueChanged] DEFAULT ((0)) NULL,
    [NewItemAdded]                       BIT              CONSTRAINT [DF_tbl_estimates_NewItemAdded] DEFAULT ((0)) NULL,
    [isEstimate]                         BIT              CONSTRAINT [DF__tbl_estim__isEst__3712ABA4] DEFAULT ((1)) NULL,
    [isDirectSale]                       BIT              CONSTRAINT [DF__tbl_estim__isDir__64D97654] DEFAULT ((1)) NULL,
    [LastUpdateDate]                     DATETIME         NULL,
    [NominalCode]                        INT              NULL,
    [BillingAddressId]                   INT              NULL,
    [DeliveryCostCenterId]               INT              NULL,
    [DeliveryCost]                       FLOAT (53)       NULL,
    [DeliveryCompletionTime]             INT              NULL,
    [VoucherDiscountRate]                FLOAT (53)       NULL,
    [ReportSignedBy]                     UNIQUEIDENTIFIER NULL,
    [InvoiceId]                          INT              NULL,
    [OrderReportSignedBy]                INT              NULL,
    [OrderReportLastPrinted]             DATETIME         NULL,
    [EstimateReportLastPrinted]          DATETIME         NULL,
    [isEmailSent]                        BIT              CONSTRAINT [DF__tbl_estim__isEma__5DC26471] DEFAULT ((0)) NULL,
    [DiscountVoucherID]                  INT              NULL,
    [ClientStatus]                       SMALLINT         NULL,
    [RefEstimateId]                      BIGINT           NULL,
    [XeroAccessCode]                     VARCHAR (50)     NULL,
    [OrganisationId]                     BIGINT           NULL,
    CONSTRAINT [PK_tbl_estimates] PRIMARY KEY CLUSTERED ([EstimateId] ASC),
    CONSTRAINT [FK_tbl_estimates_tbl_contactcompanies] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId]),
    CONSTRAINT [FK_tbl_estimates_tbl_contacts] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[CompanyContact] ([ContactId]),
    CONSTRAINT [FK_tbl_estimates_tbl_enquiries] FOREIGN KEY ([EnquiryId]) REFERENCES [dbo].[tbl_enquiries] ([EnquiryId]),
    CONSTRAINT [FK_tbl_estimates_tbl_Statuses] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([StatusId])
);


GO
ALTER TABLE [dbo].[Estimate] NOCHECK CONSTRAINT [FK_tbl_estimates_tbl_contacts];


GO
ALTER TABLE [dbo].[Estimate] NOCHECK CONSTRAINT [FK_tbl_estimates_tbl_enquiries];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Field will set to True if an item is added after the Estimate is sent.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Estimate', @level2type = N'COLUMN', @level2name = N'NewItemAdded';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Field used for Sales Person Pipeline', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Estimate', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Field Used for sales person pipeline', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Estimate', @level2type = N'COLUMN', @level2name = N'SourceId';

