define(["ko", "underscore", "underscore-ko"], function (ko) {
    var
    InventoryListView = function (specifiedStockItemId, specifiedName, specifiedWeight, specifiedPerQtyQty, specifiedSizecolour, specifiedCategoryName,
                            specifiedSubCategoryName, specifiedWeightUnitName, specifiedFullCategoryName, specifiedSupplierCompanyName) {
        var
            self,
            //Unique ID
            id = ko.observable(specifiedStockItemId),
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
                    StockItemId: id(),
                }
            };
        self = {
            id: id,
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
    var StockItem = function (specifiedItemId, specifiedItemName, specifiedItemCode, specifiedSupplierId, specifiedCategoryId, specifiedSubCategoryId,
        specifiedBarCode, specifiedInStock, specifiedDescription, specifiedCreatedDate, specifiedFlagId, specifiedStatusId, specifiedIsDisabled, specifiedPaperTypeId,
        specifiedItemSizeSelectedUnitId, specifiedPerQtyQty, specifiedItemSizeCustom) {
        var
            self,
            //item Id
            itemId = ko.observable(specifiedItemId),
            //Item Name
            itemName = ko.observable(specifiedItemName).extend({ required: true }),
            //Item Code
            itemCode = ko.observable(specifiedItemCode),
            //Supplier Id
            supplierId = ko.observable(specifiedSupplierId).extend({ required: true }),
            //Category Id
            categoryId = ko.observable(specifiedCategoryId).extend({ required: true }),
            //Sub Category Id
            subCategoryId = ko.observable(specifiedSubCategoryId),
            //Bar Code
            barCode = ko.observable(specifiedBarCode),
            //in Stock
            inStock = ko.observable(specifiedInStock),
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
            perQtyQty = ko.observable(specifiedPerQtyQty),
            //Item Size Custom
            itemSizeCustom = ko.observable(specifiedItemSizeCustom),
            //header computed Value based on selection unit size itm 
            headerComputedValue = ko.observable(),
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
    //Stock Item For Client Factory
    StockItem.CreateForClient = function (source) {
        return new StockItem(source.StockItemId, source.ItemName, source.ItemCode, source.SupplierId, source.CategoryId, source.SubCategoryId, source.BarCode,
         source.inStock, source.ItemDescription, source.StockCreated, source.FlagID, source.Status, source.isDisabled, source.PaperType, source.ItemSizeSelectedUnit,
            source.PerQtyType, source.ItemSizeCustom);
    };
    // Stock Item Factory
    StockItem.Create = function () {
        return new StockItem(0, "", "", undefined, undefined, undefined, "",
         0, "", undefined, undefined, undefined, false, "1", 2, 0, true);
    };
    //Create Factory 
    InventoryListView.Create = function (source) {
        return new InventoryListView(source.StockItemId, source.ItemName, source.ItemWeight, source.PerQtyQty, source.FlagColor, source.CategoryName,
                              source.SubCategoryName, source.WeightUnitName, source.FullCategoryName, source.SupplierCompanyName);
    };
    return {
        InventoryListView: InventoryListView,
        StockItem: StockItem,
    };
});