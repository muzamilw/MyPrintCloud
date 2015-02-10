CREATE TABLE [dbo].[CostCentreType] (
    [TypeId]     INT          IDENTITY (1, 1) NOT NULL,
    [TypeName]   VARCHAR (50) NOT NULL,
    [IsSystem]   SMALLINT     CONSTRAINT [DF__tbl_costc__IsSys__10566F31] DEFAULT (0) NOT NULL,
    [IsExternal] SMALLINT     CONSTRAINT [DF__tbl_costc__IsExt__114A936A] DEFAULT (0) NOT NULL,
    [CompanyId]  INT          NULL,
    CONSTRAINT [PK_tbl_costcentretypes] PRIMARY KEY CLUSTERED ([TypeId] ASC)
);

