define(["ko", "underscore", "underscore-ko"], function () {
    var
    // Stock Item Entity        
// ReSharper disable InconsistentNaming
    StockItem = function(specifiedId, specifiedName, specifiedCategoryName, specifiedLocation, specifiedWeight, specifiedDescription) {
// ReSharper restore InconsistentNaming
        return {
            id: specifiedId,
            name: specifiedName,
            category: specifiedCategoryName,
            location: specifiedLocation,
            weight: specifiedWeight,
            description: specifiedDescription
        };
    };
    
    // Stock Item Factory
    StockItem.Create = function (source) {
        return new StockItem(source.StockItemId, source.ItemName, source.CategoryName, source.StockLocation, source.ItemWeight, source.ItemDescription);
    };

    return {
        // StockItem Constructor
        StockItem: StockItem
    };
});