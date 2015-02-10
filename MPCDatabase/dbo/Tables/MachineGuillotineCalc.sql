CREATE TABLE [dbo].[MachineGuillotineCalc] (
    [Id]              BIGINT IDENTITY (1, 1) NOT NULL,
    [MethodId]        BIGINT NULL,
    [PaperWeight1]    BIGINT NULL,
    [PaperThroatQty1] BIGINT NULL,
    [PaperWeight2]    BIGINT NULL,
    [PaperThroatQty2] BIGINT NULL,
    [PaperWeight3]    BIGINT NULL,
    [PaperThroatQty3] BIGINT NULL,
    [PaperWeight4]    BIGINT NULL,
    [PaperThroatQty4] BIGINT NULL,
    [PaperWeight5]    BIGINT NULL,
    [PaperThroatQty5] BIGINT NULL,
    CONSTRAINT [PK_tbl_machine_guillotinecalc] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tbl_lookup_methods_tbl_machine_guillotinecalc] FOREIGN KEY ([MethodId]) REFERENCES [dbo].[LookupMethod] ([MethodId])
);

