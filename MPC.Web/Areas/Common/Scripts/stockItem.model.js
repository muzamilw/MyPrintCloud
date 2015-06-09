define(["ko", "underscore", "underscore-ko"], function () {
   // #region Stock Item

    // ReSharper disable once InconsistentNaming
    var StockItem = function (specifiedId, specifiedname,
        specifiedWeight, specifiedPackageQty, specifiedPerQtyQty, specifiedPrice, specifiedCompanyTaxRate,specifyInStock,specifyAllocated) {

        return {
            id: specifiedId,
            name: specifiedname,
            itemWeight: specifiedWeight,
            packageQty: specifiedPackageQty,
            perQtyQty: specifiedPerQtyQty,
            price: specifiedPrice / (specifiedPerQtyQty || 1),
            priceWithTax: specifiedPrice + (specifiedPrice * (specifiedCompanyTaxRate / 100)),
            inStock: specifyInStock,
            allocated: specifyAllocated

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
            source.CompanyTaxRate,
            source.InStock,
            source.Allocated
            );
        return stockItem;
    };
    // #endregion

    return {
        // StockItem Constructor
        StockItem: StockItem
    };
});