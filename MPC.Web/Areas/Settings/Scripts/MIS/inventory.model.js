define(["ko", "underscore", "underscore-ko"], function (ko) {
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
        specifiedItemSizeWidth, specifiedPerQtyType, specifiedItemSizeSelectedUnit, specifiedPackageQty
        ) {
        var
            self,
            //item Id
            itemId = ko.observable(specifiedItemId),
            //Item Name
            itemName = ko.observable(specifiedItemName).extend({ required: true }),
            //Item Code
            itemCode = ko.observable(specifiedItemCode),
            //Supplier Id
            supplierId = ko.observable(specifiedSupplierId),
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
            createdDate = ko.observable(specifiedCreatedDate),
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
            //Item Size Selected Unit
            itemSizeSelectedUnit = ko.observable(specifiedItemSizeSelectedUnit),
            //Package Qty
            packageQty = ko.observable(specifiedPackageQty).extend({ number: true }),
            //header computed Value based on selection unit size itm 
            headerComputedValue = ko.observable(),
             // Errors
            errors = ko.validation.group({
                itemName: itemName,
                inStock: inStock,
                perQtyQty: perQtyQty,
                itemSizeHeight: itemSizeHeight,
                itemSizeWidth: itemSizeWidth,
                packageQty: packageQty,

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
                    ItemId: itemId(),
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
            itemSizeSelectedUnit: itemSizeSelectedUnit,
            packageQty: packageQty,
            headerComputedValue: headerComputedValue,
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
            costPrice = ko.observable(specifiedCostPrice),
            //Pack Cost Price
            packCostPrice = ko.observable(specifiedPackCostPrice),
            //From Date
            fromDate = ko.observable(specifiedFromDate),
            //To Date
            toDate = ko.observable(specifiedToDate),
            //Cost Or Price Identifier
            costOrPriceIdentifier = ko.observable(specifiedCostOrPriceIdentifier),

             // Errors
            errors = ko.validation.group({
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
                    FromDate: fromDate(),
                    ToDate: toDate(),
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
            source.PerQtyType, source.ItemSizeCustom);
    };
    //Stock Cost And Price Item For Client Factory
    StockCostAndPrice.CreateForClient = function (source) {
        return new StockItem(source.CostPriceId, source.CostPrice, source.PackCostPrice, source.FromDate, source.ToDate, source.CostOrPriceIdentifier);
    };
    // Stock Item Factory
    StockItem.Create = function () {
        return new StockItem(0, "", "", undefined, undefined, undefined, "111000011110",
         0, "", undefined, undefined, undefined, false, "1", 2, 0, false);
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