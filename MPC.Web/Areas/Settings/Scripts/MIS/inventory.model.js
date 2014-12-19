﻿define(["ko", "underscore", "underscore-ko"], function (ko) {
    var
    InventoryListView = function (specifiedStockItemId, specifiedName, specifiedWeight, specifiedPerQtyQty, specifiedSizecolour, specifiedCategoryName,
                            specifiedSubCategoryName, specifiedWeightUnitName, specifiedFullCategoryName, specifiedSupplierCompanyName
                            ) {
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
            ///Supplier Company Name
            supplierCompanyName = ko.observable(specifiedSupplierCompanyName),
            convertToServerData = function () {
                return {
                    StockItemId: itemId(),
                }
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
        specifiedItemWeightSelectedUnit, specifiedAllocated, specifiedOnOrder, specifiedLastOrderQty, specifiedLastOrderDate
           ) {
        var
            self,
            //item Id
            itemId = ko.observable(specifiedItemId === undefined ? 0 : specifiedItemId),
            //Item Name
            itemName = ko.observable(specifiedItemName).extend({ required: true }),
            //Item Code
            itemCode = ko.observable(specifiedItemCode),
            //Supplier Id
            supplierId = ko.observable(specifiedSupplierId),
            //SupplierName
            supplierName = ko.observable(),
            //Category Id
            categoryId = ko.observable(specifiedCategoryId),
            //Sub Category Id
            subCategoryId = ko.observable(specifiedSubCategoryId),
            //Bar Code
            barCode = ko.observable(specifiedBarCode),
            //in Stock
            inStock = ko.observable(specifiedInStock).extend({ number: true }),
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
            paperTypeId = ko.observable(specifiedPaperTypeId),
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
            packageQty = ko.observable(specifiedPackageQty === undefined ? undefined : specifiedPackageQty).extend({ number: true }),
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
            allocated = ko.observable(specifiedAllocated),
            //On Order
            onOrder = ko.observable(specifiedOnOrder),
            //Last Order Qty
            lastOrderQty = ko.observable(specifiedLastOrderQty),
            //Last Order Date
            lastOrderDate = ko.observable(specifiedLastOrderDate),
            //header computed Value based on selection unit size itm 
        headerComputedValue = ko.observable(),
        //Stock Cost And Price List
        stockCostAndPriceListInInventory = ko.observableArray([]),
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
                StockItemId: itemId(),
                ItemName: itemName(),
                ItemCode: itemCode(),
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
                ItemWeightSelectedUnit: itemWeightSelectedUnit(),
                StockCostAndPrices: stockCostAndPriceListInInventory()
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
            stockCostAndPriceListInInventory: stockCostAndPriceListInInventory,
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
            //From Date
            fromDate = ko.observable(specifiedFromDate).extend({ required: true }),
            //To Date
            toDate = ko.observable(specifiedToDate).extend({ required: true }),
            //Cost Or Price Identifier
            costOrPriceIdentifier = ko.observable(specifiedCostOrPriceIdentifier),
            // Formatted From Date
             formattedFromDate = ko.computed({
                 read: function () {
                     return fromDate() !== undefined ? moment(fromDate()).format(ist.datePattern) : undefined;
                 }
             }),
             // Formatted To Date
             formattedToDate = ko.computed({
                 read: function () {
                     return toDate() !== undefined ? moment(toDate()).format(ist.datePattern) : undefined;
                 }
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
                    FromDate: fromDate() === undefined || fromDate() === null ? undefined : moment(fromDate()).format(ist.utcFormat),
                    ToDate: toDate() === undefined || toDate() === null ? undefined : moment(toDate()).format(ist.utcFormat),
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
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    //Stock Item For Client Factory
    StockItem.CreateForClient = function (source) {
        return new StockItem(source.StockItemId, source.ItemName, source.ItemCode, source.SupplierId, source.CategoryId, source.SubCategoryId, source.BarCode,
         source.inStock, source.ItemDescription, source.StockCreated, source.FlagID, source.Status, source.isDisabled, source.PaperType, source.ItemSizeSelectedUnit,
            source.PerQtyQty, source.ItemSizeCustom, source.StockLocation, source.ItemSizeId, source.ItemSizeHeight, source.ItemSizeWidth, source.PerQtyType, source.PackageQty,
            source.RollWidth, source.RollLength, source.ReOrderLevel, source.ReorderQty, source.ItemWeight, source.ItemColour, source.InkAbsorption, source.PaperBasicAreaId,
            source.ItemCoated, source.ItemCoatedType, source.ItemWeightSelectedUnit, source.Allocated, source.onOrder, source.LastOrderQty, source.LastOrderDate
            );
    };
    //Stock Cost And Price Item For Client Factory
    StockCostAndPrice.CreateForClient = function (source) {
        return new StockCostAndPrice(source.CostPriceId, source.CostPrice, source.PackCostPrice, source.FromDate, source.ToDate, source.CostOrPriceIdentifier);
    };
    //Stock Cost And Price Item For Client Factory
    StockCostAndPrice.Create = function () {
        return new StockCostAndPrice(0, 0, 0, undefined, undefined, 0);
    };
    // Stock Item Factory
    StockItem.Create = function () {
        return new StockItem(undefined, "", "", undefined, undefined, undefined, "111000011110",
         0, "", undefined, undefined, undefined, false, "1", undefined, 100, false, "", undefined, undefined, undefined, undefined, 100, undefined, undefined, 0, 0, 100, "White", 100,
         undefined, false, "1", undefined, undefined, undefined, undefined, undefined);
    };
    //Create Factory 
    InventoryListView.Create = function (source) {
        return new InventoryListView(source.StockItemId, source.ItemName, source.ItemWeight, source.PerQtyQty, source.FlagColor, source.CategoryName,
                              source.SubCategoryName, source.WeightUnitName, source.FullCategoryName, source.SupplierCompanyName);
    };

    return {
        InventoryListView: InventoryListView,
        StockItem: StockItem,
        StockCostAndPrice: StockCostAndPrice,
    };
});