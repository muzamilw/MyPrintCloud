﻿define(["ko", "underscore", "underscore-ko"], function () {
   // #region Stock Item

    // ReSharper disable once InconsistentNaming
    var StockItem = function (specifiedId, specifiedname,
        specifiedWeight, specifiedPackageQty, specifiedPerQtyQty, specifiedPrice, specifiedCompanyTaxRate, specifyInStock, specifyAllocated, specifyItemCode,specifiedActualPackCost) {

        return {
            id: specifiedId,
            name: specifiedname,
            itemWeight: specifiedWeight,
            packageQty: specifiedPackageQty,
            perQtyQty: specifiedPerQtyQty,
            price: specifiedActualPackCost / specifiedPackageQty,
            priceWithTax: specifiedPrice + (specifiedPrice * (specifiedCompanyTaxRate / 100)),
            inStock: specifyInStock,
            allocated: specifyAllocated,
            code: specifyItemCode,
            actualprice: specifiedPrice
        };
    };

    StockItem.Create = function (source) {
        var stockItem = new StockItem(
            source.StockItemId,
            source.ItemName,
            source.ItemWeight,
            source.PackageQty,
            source.PerQtyQty || 0,
            source.CostPrice === -9999 ? 0 : source.CostPrice,
            source.CompanyTaxRate,
            source.InStock,
            source.Allocated,
            source.ItemCode,
            source.ActualPackCost
            );
        return stockItem;
    };
    // #endregion

    return {
        // StockItem Constructor
        StockItem: StockItem
    };
});