CREATE TABLE [dbo].[WorkflowPreference] (
    [Id]                          INT IDENTITY (1, 1) NOT NULL,
    [SystemSiteId]                INT CONSTRAINT [DF__tbl_workf__Syste__35FCF52C] DEFAULT (0) NOT NULL,
    [ShowTaxInformation]          BIT CONSTRAINT [DF__tbl_workf__ShowT__36F11965] DEFAULT (0) NOT NULL,
    [ShippingNoteordertojob]      BIT CONSTRAINT [DF__tbl_workf__Shipp__37E53D9E] DEFAULT (0) NOT NULL,
    [CopyJobDescriptionToInvoice] BIT CONSTRAINT [DF__tbl_workf__CopyJ__38D961D7] DEFAULT (0) NOT NULL,
    [EstimateTitleMandatory]      BIT CONSTRAINT [DF__tbl_workf__Estim__39CD8610] DEFAULT (0) NOT NULL,
    [GoodsOrderForm]              BIT CONSTRAINT [DF__tbl_workf__Goods__3AC1AA49] DEFAULT (0) NOT NULL,
    [RoundSellingPrice]           BIT CONSTRAINT [DF__tbl_workf__Round__3BB5CE82] DEFAULT (0) NOT NULL,
    [ShippingNoteIquirytoorder]   BIT CONSTRAINT [DF__tbl_workf__Shipp__3CA9F2BB] DEFAULT (0) NOT NULL,
    [ShowTax2AndTax3Information]  BIT CONSTRAINT [DF__tbl_workf__ShowT__3D9E16F4] DEFAULT (0) NOT NULL,
    [Nearest]                     INT CONSTRAINT [DF__tbl_workf__Neare__3E923B2D] DEFAULT (0) NOT NULL,
    [ItemTitleMandatory]          BIT CONSTRAINT [DF__tbl_workf__ItemT__3F865F66] DEFAULT (0) NOT NULL,
    [OrderTitleMandatory]         BIT CONSTRAINT [DF__tbl_workf__Order__407A839F] DEFAULT (0) NOT NULL,
    [ShowZeroValueCostCentre]     BIT NULL,
    CONSTRAINT [PK_tbl_workflowpreferences] PRIMARY KEY CLUSTERED ([Id] ASC)
);

