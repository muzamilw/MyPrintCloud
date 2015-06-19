define(["ko", "underscore", "underscore-ko"], function (ko) {
    var
    InventoryListView = function (specifiedStockItemId, specifiedName, specifiedWeight, specifiedPerQtyQty, specifiedSizecolour, specifiedCategoryName,
                            specifiedSubCategoryName, specifiedWeightUnitName, specifiedFullCategoryName, specifiedSupplierCompanyName
                            , specifiedRegion) {
        var
            self,
            //Unique ID
            itemId = ko.observable(specifiedStockItemId),
            //Name
            name = ko.observable(specifiedName),
            //Weight
            weight = ko.observable(specifiedWeight),
            //Per quantity
            perQtyQty = ko.observable(specifiedPerQtyQty),
             //Region
            region = ko.observable(specifiedRegion),
            //Flag Color
            colour = ko.observable(specifiedSizecolour),
            //Stock Category Name
            categoryName = ko.observable(specifiedCategoryName),
            //Stock Sub category Name
            subCategoryName = ko.observable(specifiedSubCategoryName),
            //Selected Unit Name
            weightUnitName = ko.observable(specifiedWeightUnitName),
            //category + Sub Category Name
            fullCategoryName = ko.observable(specifiedFullCategoryName),
            // Per pack Cost within current date
            packCostPrice = ko.observable(specifiedFullCategoryName || 0).extend({ numberInput: ist.numberFormat }),
            ///Supplier Company Name
            supplierCompanyName = ko.observable(specifiedSupplierCompanyName),
            convertToServerData = function () {
                return {
                    StockItemId: itemId(),
                };
            };
        self = {
            itemId: itemId,
            name: name,
            weight: weight,
            perQtyQty: perQtyQty,
            colour: colour,
            categoryName: categoryName,
            subCategoryName: subCategoryName,
            weightUnitName: weightUnitName,
            packCostPrice: packCostPrice,
            fullCategoryName: fullCategoryName,
            supplierCompanyName: supplierCompanyName,
            convertToServerData: convertToServerData,
        };
        return self;
    };

    //Stock Item Entity
    var StockItem = function (specifiedItemId, specifiedItemName, specifiedItemCode, specifiedSupplierId, specifiedCategoryId, specifiedSubCategoryId,
        specifiedBarCode, specifiedInStock, specifiedDescription, specifiedCreatedDate, specifiedFlagId, specifiedStatusId, specifiedIsDisabled, specifiedPaperTypeId,
        specifiedItemSizeSelectedUnitId, specifiedPerQtyQty, specifiedItemSizeCustom, specifiedStockLocation, specifiedItemSizeId, specifiedItemSizeHeight,
        specifiedItemSizeWidth, specifiedPerQtyType, specifiedPackageQty, specifiedRollWidth, specifiedRollLength, specifiedReOrderLevel, specifiedReorderQty,
        specifiedItemWeight, specifiedItemColour, specifiedInkAbsorption, specifiedPaperBasicAreaId, specifiedItemCoated, specifiedItemCoatedType,
        specifiedItemWeightSelectedUnit, specifiedAllocated, specifiedOnOrder, specifiedLastOrderQty, specifiedLastOrderDate, specifiedSupplierName, specifiedIsImperical,
        specifiedisAllowBackOrder, specifiedThresholdLevel) {
        var self,
            //item Id
            itemId = ko.observable(specifiedItemId === undefined ? 0 : specifiedItemId),
            //Item Name
            itemName = ko.observable(specifiedItemName).extend({ required: true }),
            //Item Code
            itemCode = ko.observable(specifiedItemCode),
            //Supplier Id
            supplierId = ko.observable(specifiedSupplierId).extend({ required: true }),
            //SupplierName
            supplierName = ko.observable(specifiedSupplierName).extend({ required: true }),
            //Category Id
            categoryId = ko.observable(specifiedCategoryId),
            //Sub Category Id
            subCategoryId = ko.observable(specifiedSubCategoryId),
            //Bar Code
            barCode = ko.observable(specifiedBarCode),
            //in Stock
            inStock = ko.observable(specifiedInStock || 0).extend({ number: true }),
            //Item Description
            description = ko.observable(specifiedDescription),
            //Created Date
            createdDate = ko.observable(specifiedCreatedDate !== undefined ? moment(specifiedCreatedDate).format(ist.datePattern) : undefined),
            //Flag ID
            flagId = ko.observable(specifiedFlagId),
            //Status ID
            statusId = ko.observable(specifiedStatusId),
            //Is Disabled
            isDisabled = ko.observable(specifiedIsDisabled),
            //Paper Type ID
            paperTypeId = ko.observable((specifiedPaperTypeId == undefined || specifiedPaperTypeId === null) ? "1" : specifiedPaperTypeId.toString()),
            //Item Size Selected Unit Id
            itemSizeSelectedUnitId = ko.observable(specifiedItemSizeSelectedUnitId),
            //perQtyQty
            perQtyQty = ko.observable(specifiedPerQtyQty).extend({ number: true }),
            //Item Size Custom
            itemSizeCustom = ko.observable(specifiedItemSizeCustom),
            //Stock Location
            stockLocation = ko.observable(specifiedStockLocation),
            //Item Size Id
            itemSizeId = ko.observable(specifiedItemSizeId),
            //Item Size Height
            itemSizeHeight = ko.observable(specifiedItemSizeHeight).extend({ number: true }),
            //Item Size Width
            itemSizeWidth = ko.observable(specifiedItemSizeWidth).extend({ number: true }),
            //Per Qty Type
            perQtyType = ko.observable(specifiedPerQtyType),
            //Package Qty
            packageQty = ko.observable(specifiedPackageQty === undefined ? 100 : specifiedPackageQty).extend({ number: true }),
            //Roll Width
            rollWidth = ko.observable(specifiedRollWidth).extend({ number: true }),
            //Roll Length
            rollLength = ko.observable(specifiedRollLength).extend({ number: true }),
            //ReOrder Level
            reOrderLevel = ko.observable(specifiedReOrderLevel).extend({ number: true }),
            //Reorder Qty
            reorderQty = ko.observable(specifiedReorderQty).extend({ number: true }),
            //Item Weight
            itemWeight = ko.observable(specifiedItemWeight).extend({ number: true }),
            //Item Colour
            itemColour = ko.observable(specifiedItemColour),
            //Ink Absorption
            inkAbsorption = ko.observable(specifiedInkAbsorption).extend({ number: true }),
            //Paper Basic Area Id
            paperBasicAreaId = ko.observable(specifiedPaperBasicAreaId),
            //Item Coated
            itemCoated = ko.observable(specifiedItemCoated),
            //Item Coated Type
            itemCoatedType = ko.observable(specifiedItemCoatedType),
            //Item Weight Selected Unit
            itemWeightSelectedUnit = ko.observable(specifiedItemWeightSelectedUnit),
            //Allocated
            allocated = ko.observable(specifiedAllocated == undefined || specifiedAllocated == null ? 0 : specifiedAllocated),
            //On Order
            onOrder = ko.observable(specifiedOnOrder == undefined || specifiedOnOrder == null ? 0 : specifiedOnOrder),
            //Last Order Qty
            lastOrderQty = ko.observable(specifiedLastOrderQty == undefined || specifiedLastOrderQty == null ? 0 : specifiedLastOrderQty),
            //Last Order Date
            lastOrderDate = ko.observable(specifiedLastOrderDate),

            // is empirical
            IsImperical = ko.observable(specifiedIsImperical),
            //header computed Value based on selection unit size itm 
        headerComputedValue = ko.observable(),
        //Stock Cost And Price List
        stockCostAndPriceListInInventory = ko.observableArray([]),
        // Item Stock Update Histories
        itemStockUpdateHistories = ko.observableArray([]),
        //Paper Type
        paperType = ko.observable(),
        // is Allow Back Order
        isAllowBackOrder = ko.observable(specifiedisAllowBackOrder || false),
        // Thres hold Level
        thresholdLevel = ko.observable(specifiedThresholdLevel),
        // Errors
        errors = ko.validation.group({
            itemName: itemName,
            inStock: inStock,
            perQtyQty: perQtyQty,
            itemSizeHeight: itemSizeHeight,
            itemSizeWidth: itemSizeWidth,
            packageQty: packageQty,
            rollWidth: rollWidth,
            rollLength: rollLength,
            reOrderLevel: reOrderLevel,
            reorderQty: reorderQty,
            itemWeight: itemWeight,
            inkAbsorption: inkAbsorption,
            supplierId: supplierId,
            supplierName: supplierName,
        }),
        // Is Valid 
        isValid = ko.computed(function () {
            return errors().length === 0 ? true : false;
        }),

        // True if the booking has been changed
        // ReSharper disable InconsistentNaming
        dirtyFlag = new ko.dirtyFlag({
            itemName: itemName,
            itemCode: itemCode,
            supplierId: supplierId,
            categoryId: categoryId,
            subCategoryId: subCategoryId,
            barCode: barCode,
            inStock: inStock,
            description: description,
            createdDate: createdDate,
            flagId: flagId,
            statusId: statusId,
            isDisabled: isDisabled,
            paperTypeId: paperTypeId,
            itemSizeSelectedUnitId: itemSizeSelectedUnitId,
            perQtyQty: perQtyQty,
            itemSizeCustom: itemSizeCustom,
            stockLocation: stockLocation,
            itemSizeId: itemSizeId,
            itemSizeHeight: itemSizeHeight,
            itemSizeWidth: itemSizeWidth,
            perQtyType: perQtyType,
            packageQty: packageQty,
            rollWidth: rollWidth,
            rollLength: rollLength,
            reOrderLevel: reOrderLevel,
            reorderQty: reorderQty,
            itemWeight: itemWeight,
            itemColour: itemColour,
            inkAbsorption: inkAbsorption,
            paperBasicAreaId: paperBasicAreaId,
            itemCoated: itemCoated,
            itemCoatedType: itemCoatedType,
            itemWeightSelectedUnit: itemWeightSelectedUnit,
            allocated: allocated,
            onOrder: onOrder,
            lastOrderQty: lastOrderQty,
            lastOrderDate: lastOrderDate,
            headerComputedValue: headerComputedValue,
            supplierName: supplierName,
            IsImperical: IsImperical,
            isAllowBackOrder: isAllowBackOrder,
            thresholdLevel: thresholdLevel,
            stockCostAndPriceListInInventory: stockCostAndPriceListInInventory,
            itemStockUpdateHistories: itemStockUpdateHistories,
        }),
        // Has Changes
        hasChanges = ko.computed(function () {
            return dirtyFlag.isDirty();
        }),
        convertToServerData = function (region) {
            return {
                StockItemId: itemId(),
                ItemName: itemName(),
                ItemCode: itemCode(),
                Region: region,
                SupplierId: supplierId(),
                CategoryId: categoryId(),
                SubCategoryId: subCategoryId(),
                BarCode: barCode(),
                inStock: inStock(),
                FlagID: flagId(),
                ItemDescription: description(),
                StockLocation: stockLocation(),
                PaperType: paperTypeId(),
                ItemSizeId: itemSizeId(),
                ItemSizeCustom: itemSizeCustom() === true ? 1 : 0,
                ItemSizeHeight: itemSizeHeight(),
                ItemSizeWidth: itemSizeWidth(),
                ItemSizeSelectedUnit: itemSizeSelectedUnitId(),
                PerQtyQty: perQtyQty(),
                PerQtyType: perQtyType(),
                PackageQty: packageQty(),
                RollWidth: rollWidth(),
                RollLength: rollLength(),
                ReOrderLevel: reOrderLevel(),
                ReorderQty: reorderQty(),
                ItemWeight: itemWeight(),
                ItemColour: itemColour(),
                InkAbsorption: inkAbsorption(),
                PaperBasicAreaId: paperBasicAreaId(),
                ItemCoated: itemCoated(),
                ItemCoatedType: itemCoatedType(),
                Status: statusId(),
                isDisabled: isDisabled(),
                IsImperical: IsImperical(),
                ItemWeightSelectedUnit: itemWeightSelectedUnit(),
                StockCostAndPrices: stockCostAndPriceListInInventory(),
                isAllowBackOrder: isAllowBackOrder(),
                ItemStockUpdateHistories: []
            }
        },
        // Reset
        reset = function () {
            dirtyFlag.reset();
        };
        self = {
            itemId: itemId,
            itemName: itemName,
            itemCode: itemCode,
            supplierId: supplierId,
            categoryId: categoryId,
            subCategoryId: subCategoryId,
            barCode: barCode,
            inStock: inStock,
            description: description,
            createdDate: createdDate,
            flagId: flagId,
            statusId: statusId,
            isDisabled: isDisabled,
            paperTypeId: paperTypeId,
            itemSizeSelectedUnitId: itemSizeSelectedUnitId,
            perQtyQty: perQtyQty,
            itemSizeCustom: itemSizeCustom,
            stockLocation: stockLocation,
            itemSizeId: itemSizeId,
            itemSizeHeight: itemSizeHeight,
            itemSizeWidth: itemSizeWidth,
            perQtyType: perQtyType,
            packageQty: packageQty,
            rollWidth: rollWidth,
            rollLength: rollLength,
            reOrderLevel: reOrderLevel,
            reorderQty: reorderQty,
            itemWeight: itemWeight,
            itemColour: itemColour,
            inkAbsorption: inkAbsorption,
            paperBasicAreaId: paperBasicAreaId,
            itemCoated: itemCoated,
            itemCoatedType: itemCoatedType,
            itemWeightSelectedUnit: itemWeightSelectedUnit,
            allocated: allocated,
            onOrder: onOrder,
            lastOrderQty: lastOrderQty,
            lastOrderDate: lastOrderDate,
            headerComputedValue: headerComputedValue,
            supplierName: supplierName,
            IsImperical: IsImperical,
            stockCostAndPriceListInInventory: stockCostAndPriceListInInventory,
            paperType: paperType,
            isAllowBackOrder: isAllowBackOrder,
            thresholdLevel: thresholdLevel,
            itemStockUpdateHistories: itemStockUpdateHistories,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    //Stock Cost And Price
    var StockCostAndPrice = function (specifiedCostPriceId, specifiedCostPrice, specifiedPackCostPrice, specifiedFromDate, specifiedToDate, specifiedCostOrPriceIdentifier) {
        var
            self,
            //cost Price Id
            costPriceId = ko.observable(specifiedCostPriceId),
            //Cost Price
            costPrice = ko.observable(specifiedCostPrice).extend({ required: true, number: true }),
            //Pack Cost Price
            packCostPrice = ko.observable(specifiedPackCostPrice),
             //To Date 
            toDate = ko.observable((specifiedToDate === null || specifiedToDate === undefined) ? moment().add('days', 1).toDate() : moment(specifiedToDate, ist.utcFormat).toDate()).extend({ required: true }),
            //From Date
            fromDate = ko.observable((specifiedFromDate === null || specifiedFromDate == undefined) ? moment().toDate() : moment(specifiedFromDate, ist.utcFormat).toDate()).extend({ required: true }),
                //Cost Or Price Identifier
            costOrPriceIdentifier = ko.observable(specifiedCostOrPriceIdentifier),
            // Formatted From Date
             formattedFromDate = ko.computed({
                 read: function () {
                     return fromDate() !== undefined ? moment(fromDate(), ist.datePattern).toDate() : undefined;
                 }
             }),
             // Formatted To Date
             formattedToDate = ko.computed({
                 read: function () {
                     return toDate() !== undefined ? moment(toDate()).format(ist.datePattern) : undefined;
                 }
             }),
              isInvalidPeriod = ko.computed(function () {
                  return toDate() < fromDate();
              }),
             // Errors
            errors = ko.validation.group({
                costPrice: costPrice,
                fromDate: fromDate,
                toDate: toDate,
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // True if the booking has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            convertToServerData = function () {
                return {
                    CostPriceId: costPriceId(),
                    CostPrice: costPrice(),
                    PackCostPrice: packCostPrice(),
                    FromDate: fromDate() === undefined || fromDate() === null ? null : moment(fromDate()).format(ist.utcFormat),
                    ToDate: toDate() === undefined || toDate() === null ? null : moment(toDate()).format(ist.utcFormat),
                    CostOrPriceIdentifier: costOrPriceIdentifier(),
                }
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            costPriceId: costPriceId,
            costPrice: costPrice,
            packCostPrice: packCostPrice,
            fromDate: fromDate,
            toDate: toDate,
            formattedFromDate: formattedFromDate,
            formattedToDate: formattedToDate,
            costOrPriceIdentifier: costOrPriceIdentifier,
            isValid: isValid,
            errors: errors,
            isInvalidPeriod: isInvalidPeriod,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };

    // Item Stock Update History
    var ItemStockUpdateHistory = function (specifiedStockHistoryId, specifiedLastModifiedQty, specifiedModifyEvent, specifiedLastModifiedBy, specifiedLastModifiedDate,
        specifiedLastModifiedByName, specifiedAction) {
        var
            self,
            //cost Price Id
            id = ko.observable(specifiedStockHistoryId),
            lastModifiedQty = ko.observable(specifiedLastModifiedQty),
            modifyEvent = ko.observable(specifiedModifyEvent),
            lastModifiedBy = ko.observable(specifiedLastModifiedBy),
            lastModifiedDate = ko.observable(specifiedLastModifiedDate),
            actionName = ko.observable(specifiedAction),
            lastModifiedByName = ko.observable(specifiedLastModifiedByName),
             // Errors
            errors = ko.validation.group({

            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // True if the booking has been changed
              dirtyFlag = new ko.dirtyFlag({
              }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            convertToServerData = function () {
                return {
                    StockHistoryId: id(),
                    LastModifiedQty: lastModifiedQty(),
                    ModifyEvent: modifyEvent(),
                    LastModifiedBy: lastModifiedBy(),
                    LastModifiedDate: lastModifiedDate() === undefined || lastModifiedDate() === null ? null : moment(lastModifiedDate()).format(ist.utcFormat),
                }
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            id: id,
            lastModifiedQty: lastModifiedQty,
            modifyEvent: modifyEvent,
            lastModifiedBy: lastModifiedBy,
            lastModifiedDate: lastModifiedDate,
            actionName: actionName,
            lastModifiedByName: lastModifiedByName,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };

    //Stock Item For Client Factory
    StockItem.CreateForClient = function (source) {
        var stockItem = new StockItem(source.StockItemId, source.ItemName, source.ItemCode, source.SupplierId, source.CategoryId, source.SubCategoryId, source.BarCode,
         source.inStock, source.ItemDescription, source.StockCreated, source.FlagID, source.Status, source.isDisabled, source.PaperType, source.ItemSizeSelectedUnit,
            source.PerQtyQty, source.ItemSizeCustom, source.StockLocation, source.ItemSizeId, source.ItemSizeHeight, source.ItemSizeWidth, source.PerQtyType, source.PackageQty,
            source.RollWidth, source.RollLength, source.ReOrderLevel, source.ReorderQty, source.ItemWeight, source.ItemColour, source.InkAbsorption, source.PaperBasicAreaId,
            source.ItemCoated, source.ItemCoatedType, source.ItemWeightSelectedUnit, source.Allocated, source.onOrder, source.LastOrderQty, source.LastOrderDate,
            source.SupplierName, source.IsImperical, source.isAllowBackOrder, source.ThresholdLevel);

        _.each(source.ItemStockUpdateHistories, function (item) {
            stockItem.itemStockUpdateHistories.push(ItemStockUpdateHistory.Create(item));
        });
    };
    //Stock Cost And Price Item For Client Factory
    StockCostAndPrice.CreateForClient = function (source) {
        return new StockCostAndPrice(source.CostPriceId, source.CostPrice, source.PackCostPrice, source.FromDate, source.ToDate, source.CostOrPriceIdentifier);
    };
    //Stock Cost And Price Item For Client Factory
    StockCostAndPrice.Create = function () {
        return new StockCostAndPrice(0, 0, 0, null, null, 0);
    };
    // Stock Item Factory
    StockItem.Create = function () {
        return new StockItem(undefined, "", "", undefined, undefined, undefined, "111000011110",
         0, "", undefined, undefined, undefined, false, "1", undefined, 100, false, "", undefined, undefined, undefined, undefined, 100, undefined, undefined, 0, 0, 100, "White", 100,
         undefined, false, "1", undefined, undefined, undefined, undefined, undefined);
    };
    //Create Factory 
    InventoryListView.Create = function (source) {
        var obj = new InventoryListView(source.StockItemId, source.ItemName, source.ItemWeight, source.PerQtyQty, source.FlagColor, source.CategoryName,
                              source.SubCategoryName, source.WeightUnitName, source.FullCategoryName, source.SupplierCompanyName, source.Region);
        obj.packCostPrice(source.PackCostPrice);
        return obj;
    };
    // Item Stock Update History Factory
    ItemStockUpdateHistory.CreateForClient = function (source) {
        return new ItemStockUpdateHistory(source.StockHistoryId, source.LastModifiedQty, source.ModifyEvent, source.LastModifiedBy, source.LastModifiedDate,
            source.LastModifiedByName, source.Action);
    };
    return {
        InventoryListView: InventoryListView,
        StockItem: StockItem,
        StockCostAndPrice: StockCostAndPrice,
        ItemStockUpdateHistory: ItemStockUpdateHistory,
    };
});