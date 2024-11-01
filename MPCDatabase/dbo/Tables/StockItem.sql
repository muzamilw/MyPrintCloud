﻿CREATE TABLE [dbo].[StockItem] (
    [StockItemId]                 BIGINT        IDENTITY (1, 1) NOT NULL,
    [ItemCode]                    VARCHAR (50)  NULL,
    [ItemName]                    VARCHAR (255) NULL,
    [AlternateName]               VARCHAR (255) NULL,
    [ItemWeight]                  INT           NULL,
    [ItemColour]                  VARCHAR (50)  NULL,
    [ItemSizeCustom]              SMALLINT      CONSTRAINT [DF_tbl_stockitems_ItemSizeCustom] DEFAULT ((0)) NULL,
    [ItemSizeId]                  INT           NULL,
    [ItemSizeHeight]              FLOAT (53)    NULL,
    [ItemSizeWidth]               FLOAT (53)    NULL,
    [ItemSizeDim]                 INT           NULL,
    [ItemUnitSize]                INT           NULL,
    [SupplierId]                  BIGINT        NULL,
    [CostPrice]                   FLOAT (53)    NULL,
    [CategoryId]                  BIGINT        CONSTRAINT [DF__tbl_stock__Categ__3AF6B473] DEFAULT ((0)) NULL,
    [SubCategoryId]               BIGINT        CONSTRAINT [DF__tbl_stock__SubCa__3BEAD8AC] DEFAULT ((0)) NULL,
    [LastModifiedDateTime]        DATETIME      NULL,
    [LastModifiedBy]              INT           NULL,
    [StockLevel]                  INT           NULL,
    [PackageQty]                  FLOAT (53)    NULL,
    [Status]                      INT           CONSTRAINT [DF_tbl_stockitems_Status] DEFAULT ((1)) NULL,
    [ReOrderLevel]                FLOAT (53)    CONSTRAINT [DF_tbl_stockitems_ReOrderLevel] DEFAULT ((0)) NULL,
    [StockLocation]               VARCHAR (50)  NULL,
    [ItemCoatedType]              SMALLINT      NULL,
    [ItemExposure]                SMALLINT      CONSTRAINT [DF__tbl_stock__ItemE__3CDEFCE5] DEFAULT ((0)) NULL,
    [ItemExposureTime]            INT           CONSTRAINT [DF_tbl_stockitems_ItemExposureTime] DEFAULT ((0)) NULL,
    [ItemProcessingCharge]        FLOAT (53)    CONSTRAINT [DF_tbl_stockitems_ItemProcessingCharge] DEFAULT ((0)) NULL,
    [ItemType]                    VARCHAR (50)  CONSTRAINT [DF_tbl_stockitems_ItemType] DEFAULT ((0)) NULL,
    [StockCreated]                DATETIME      NULL,
    [PerQtyRate]                  FLOAT (53)    CONSTRAINT [DF_tbl_stockitems_PerQtyRate] DEFAULT ((0)) NULL,
    [PerQtyQty]                   FLOAT (53)    CONSTRAINT [DF_tbl_stockitems_PerQtyQty] DEFAULT ((0)) NULL,
    [ItemDescription]             VARCHAR (255) NULL,
    [LockedBy]                    INT           CONSTRAINT [DF_tbl_stockitems_LockedBy] DEFAULT ((0)) NULL,
    [ReorderQty]                  INT           CONSTRAINT [DF_tbl_stockitems_ReorderQty] DEFAULT ((0)) NULL,
    [LastOrderQty]                INT           CONSTRAINT [DF__tbl_stock__LastO__3DD3211E] DEFAULT ((0)) NULL,
    [LastOrderDate]               DATETIME      NULL,
    [inStock]                     FLOAT (53)    CONSTRAINT [DF__tbl_stock__inSto__3EC74557] DEFAULT ((0)) NULL,
    [onOrder]                     FLOAT (53)    CONSTRAINT [DF__tbl_stock__onOrd__3FBB6990] DEFAULT ((0)) NULL,
    [Allocated]                   FLOAT (53)    CONSTRAINT [DF__tbl_stock__Alloc__40AF8DC9] DEFAULT ((0)) NULL,
    [TaxID]                       INT           CONSTRAINT [DF_tbl_stockitems_TaxID] DEFAULT ((0)) NULL,
    [unitRate]                    FLOAT (53)    CONSTRAINT [DF__tbl_stock__unitR__41A3B202] DEFAULT ((0)) NULL,
    [ItemCoated]                  BIT           NULL,
    [ItemSizeSelectedUnit]        INT           NULL,
    [ItemWeightSelectedUnit]      INT           NULL,
    [FlagID]                      INT           CONSTRAINT [DF__tbl_stock__FlagI__438BFA74] DEFAULT ((0)) NULL,
    [InkAbsorption]               FLOAT (53)    CONSTRAINT [DF__tbl_stock__InkAb__44801EAD] DEFAULT ((0)) NULL,
    [WashupCounter]               INT           CONSTRAINT [DF__tbl_stock__Washu__457442E6] DEFAULT ((0)) NULL,
    [InkYield]                    FLOAT (53)    CONSTRAINT [DF__tbl_stock__InkYi__4668671F] DEFAULT ((0)) NULL,
    [PaperBasicAreaId]            INT           CONSTRAINT [DF__tbl_stock__Paper__475C8B58] DEFAULT ((0)) NULL,
    [PaperType]                   INT           CONSTRAINT [DF__tbl_stock__Paper__4850AF91] DEFAULT ((0)) NULL,
    [PerQtyType]                  INT           CONSTRAINT [DF__tbl_stock__PerQt__4944D3CA] DEFAULT ((0)) NULL,
    [RollWidth]                   FLOAT (53)    CONSTRAINT [DF__tbl_stock__RollW__4A38F803] DEFAULT ((0)) NULL,
    [RollLength]                  FLOAT (53)    CONSTRAINT [DF__tbl_stock__RollL__4B2D1C3C] DEFAULT ((0)) NULL,
    [RollStandards]               INT           CONSTRAINT [DF__tbl_stock__RollS__4C214075] DEFAULT ((0)) NULL,
    [DepartmentId]                INT           CONSTRAINT [DF__tbl_stock__Depar__4D1564AE] DEFAULT ((0)) NULL,
    [InkYieldStandards]           INT           CONSTRAINT [DF__tbl_stock__InkYi__4E0988E7] DEFAULT ((0)) NULL,
    [PerQtyPrice]                 FLOAT (53)    CONSTRAINT [DF__tbl_stock__PerQt__4EFDAD20] DEFAULT ((0)) NULL,
    [PackPrice]                   FLOAT (53)    CONSTRAINT [DF__tbl_stock__PackP__4FF1D159] DEFAULT ((0)) NULL,
    [Region]                      VARCHAR (10)  NULL,
    [isDisabled]                  BIT           NULL,
    [InkStandards]                INT           CONSTRAINT [DF__tbl_stock__InkSt__51DA19CB] DEFAULT ((0)) NULL,
    [BarCode]                     VARCHAR (100) NOT NULL,
    [Image]                       VARCHAR (200) NULL,
    [XeroAccessCode]              VARCHAR (50)  NULL,
    [OrganisationId]              BIGINT        NULL,
    [ThresholdLevel]              INT           NULL,
    [ThresholdProductionQuantity] INT           NULL,
    [isAllowBackOrder]            BIT           NULL,
    CONSTRAINT [PK_tbl_stockitems] PRIMARY KEY CLUSTERED ([StockItemId] ASC),
    CONSTRAINT [FK_StockItem_Company] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Company] ([CompanyId]),
    CONSTRAINT [FK_tbl_stockitems_tbl_stockcategories] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[StockCategory] ([CategoryId]),
    CONSTRAINT [FK_tbl_stockitems_tbl_stocksubcategories] FOREIGN KEY ([SubCategoryId]) REFERENCES [dbo].[StockSubCategory] ([SubCategoryId])
);

