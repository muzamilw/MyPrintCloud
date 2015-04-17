define(["ko", "underscore", "underscore-ko"], function () {
   // #region __________________  I N V E N T O R Y   ______________________

    // ReSharper disable once InconsistentNaming
    var StockItem = function (specifiedId, specifiedname, specifiedWeight, specifiedPackageQty, specifiedPerQtyQty, specifiedPrice) {

        return {
            id: specifiedId,
            name: specifiedname,
            itemWeight: specifiedWeight,
            packageQty: specifiedPackageQty,
            perQtyQty: specifiedPerQtyQty,
            price: specifiedPrice
        };
    };

    StockItem.Create = function (source) {
        var stockItem = new StockItem(
            source.StockItemId,
            source.ItemName,
            source.ItemWeight,
            source.PackageQty,
            source.perQtyQty || 0,
            source.PackCostPrice === -9999 ? 0 : source.PackCostPrice
            );
        return stockItem;
    };
    // #endregion __________________   I N V E N T O R Y    ______________________

    return {
        // StockItem Constructor
        StockItem: StockItem
    };
});