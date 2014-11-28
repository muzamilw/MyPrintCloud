define(["ko", "underscore", "underscore-ko"], function (ko) {
    var
 // ReSharper disable once InconsistentNaming
    InventoryListView = function (specifiedStockItemId, specifiedName, specifiedWeight, specifiedPerQtyQty, specifiedSizecolour, specifiedCategoryName,
                            specifiedSubCategoryName, specifiedWeightUnitName, specifiedFullCategoryName) {
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
            fullCategoryName = ko.observable(specifiedFullCategoryName),
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
            convertToServerData: convertToServerData,
        };
        return self;
    };
    //Create Factory 
    InventoryListView.Create = function (source) {
        return new InventoryListView(source.StockItemId, source.ItemName, source.ItemWeight, source.PerQtyQty, source.FlagColor, source.CategoryName,
                              source.SubCategoryName, source.WeightUnitName, source.FullCategoryName);
    };
    return {
        InventoryListView: InventoryListView,
    };
});