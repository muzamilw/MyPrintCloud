define(["ko", "underscore", "underscore-ko"], function () {
   // #region Stock Item

    // ReSharper disable once InconsistentNaming
    var StockItem = function (specifiedId, specifiedname,
        specifiedWeight, specifiedPackageQty, specifiedPerQtyQty, specifiedPrice, specifiedCompanyTaxRate) {

        return {
            id: specifiedId,
            name: specifiedname,
            itemWeight: specifiedWeight,
            packageQty: specifiedPackageQty,
            perQtyQty: specifiedPerQtyQty,
            price: specifiedPrice / (specifiedPerQtyQty || 1),
            priceWithTax: specifiedPrice + (specifiedPrice * (specifiedCompanyTaxRate / 100))
        };
    };

    StockItem.Create = function (source) {
        var stockItem = new StockItem(
            source.StockItemId,
            source.ItemName,
            source.ItemWeight,
            source.PackageQty,
            source.PerQtyQty || 0,
            source.PackCostPrice === -9999 ? 0 : source.PackCostPrice,
            source.CompanyTaxRate
            );
        return stockItem;
    };
    // #endregion

    return {
        // StockItem Constructor
        StockItem: StockItem
    };
});