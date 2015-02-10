CREATE TABLE [dbo].[PaginationProfileCostcentreGroup] (
    [Id]                BIGINT IDENTITY (1, 1) NOT NULL,
    [PaginationId]      INT    CONSTRAINT [DF__tbl_pagin__Pagin__31D75E8D] DEFAULT (0) NULL,
    [CostcentreGroupId] INT    CONSTRAINT [DF__tbl_pagin__Costc__32CB82C6] DEFAULT (0) NULL,
    CONSTRAINT [PK_tbl_pagination_profile_costcentre_groups] PRIMARY KEY CLUSTERED ([Id] ASC)
);

