CREATE TABLE [dbo].[CostcentreGroupDetail] (
    [GroupId]      INT    CONSTRAINT [DF__tbl_costc__Group__440B1D61] DEFAULT (0) NOT NULL,
    [CostCentreId] BIGINT CONSTRAINT [DF__tbl_costc__CostC__44FF419A] DEFAULT (270592650896) NOT NULL,
    CONSTRAINT [PK_tbl_costcentre_groupdetails] PRIMARY KEY CLUSTERED ([GroupId] ASC, [CostCentreId] ASC)
);

