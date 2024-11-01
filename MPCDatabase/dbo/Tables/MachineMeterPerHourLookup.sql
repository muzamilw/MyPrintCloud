﻿CREATE TABLE [dbo].[MachineMeterPerHourLookup] (
    [Id]           BIGINT     IDENTITY (1, 1) NOT NULL,
    [MethodId]     BIGINT     NULL,
    [SheetsQty1]   BIGINT     NULL,
    [SheetsQty2]   BIGINT     NULL,
    [SheetsQty3]   BIGINT     NULL,
    [SheetsQty4]   BIGINT     NULL,
    [SheetsQty5]   BIGINT     NULL,
    [SheetWeight1] BIGINT     NULL,
    [speedqty11]   BIGINT     NULL,
    [speedqty12]   BIGINT     NULL,
    [speedqty13]   BIGINT     NULL,
    [speedqty14]   BIGINT     NULL,
    [speedqty15]   BIGINT     NULL,
    [SheetWeight2] BIGINT     NULL,
    [speedqty21]   BIGINT     NULL,
    [speedqty22]   BIGINT     NULL,
    [speedqty23]   BIGINT     NULL,
    [speedqty24]   BIGINT     NULL,
    [speedqty25]   BIGINT     NULL,
    [SheetWeight3] BIGINT     NULL,
    [speedqty31]   BIGINT     NULL,
    [speedqty32]   BIGINT     NULL,
    [speedqty33]   BIGINT     NULL,
    [speedqty34]   BIGINT     NULL,
    [speedqty35]   BIGINT     NULL,
    [hourlyCost]   FLOAT (53) NULL,
    [hourlyPrice]  FLOAT (53) NOT NULL,
    CONSTRAINT [PK_tbl_machine_meterperhourlookup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tbl_lookup_methods_tbl_machine_meterperhourlookup] FOREIGN KEY ([MethodId]) REFERENCES [dbo].[LookupMethod] ([MethodId])
);

