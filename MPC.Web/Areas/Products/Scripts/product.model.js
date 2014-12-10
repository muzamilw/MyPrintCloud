/*
    Module with the model for the Product
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var
     // Item Entity
    // ReSharper disable InconsistentNaming
    Item = function (specifiedId, specifiedName, specifiedCode, specifiedProductName, specifiedProductCode, specifiedThumbnail, specifiedMinPrice,
        specifiedIsArchived, specifiedIsPublished, specifiedProductCategoryName, specifiedIsEnabled, specifiedIsFeatured, specifiedIsFinishedGoods,
        specifiedSortOrder, specifiedIsStockControl, specifiedIsVdpProduct) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId || 0),
            // Name
            name = ko.observable(specifiedName || undefined),
            // Code
            code = ko.observable(specifiedCode || undefined),
            // Product Name
            productName = ko.observable(specifiedProductName || undefined),
            // Product Name For Grid
            productNameForGrid = ko.computed(function () {
                if (!productName()) {
                    return "";
                }
                return productName().length > 30 ? productName().substring(0, 29) : productName();
            }),
            // Product Code
            productCode = ko.observable(specifiedProductCode || undefined),
            // thumbnail
            thumbnail = ko.observable(specifiedThumbnail || undefined),
            // mini Price
            miniPrice = ko.observable(specifiedMinPrice || undefined),
            // is archived
            isArchived = ko.observable(specifiedIsArchived || undefined),
            // Is Archived Ui
            isArchivedUi = ko.computed(function () {
                return isArchived() ? "Yes" : "No";
            }),
            // is published
            isPublished = ko.observable(specifiedIsPublished || undefined),
            // Is Published Ui
            isPublishedUi = ko.computed(function () {
                return isPublished() ? "Yes" : "No";
            }),
            // product Category Name
            productCategoryName = ko.observable(specifiedProductCategoryName || undefined),
            // is featured
            isFeatured = ko.observable(specifiedIsFeatured || undefined),
            // is finished goods
            isFinishedGoods = ko.observable(specifiedIsFinishedGoods || undefined),
            // is finished goods for ui
            isFinishedGoodsUi = ko.computed({
                read: function() {
                    if (!isFinishedGoods()) {
                        return '1';
                    }
                    return  '' + isFinishedGoods();
                },
                write: function (value) {
                    var finishedGoods = parseInt(value);
                    if (parseInt(value) === isFinishedGoods()) {
                        return;
                    }

                    isFinishedGoods(finishedGoods);
                }
            }),
            // is vdp product
            isVdpProduct = ko.observable(specifiedIsVdpProduct || undefined),
            // is stock control
            isStockControl = ko.observable(specifiedIsStockControl || undefined),
            // is enabled
            isEnabled = ko.observable(specifiedIsEnabled || undefined),
            // sort order
            sortOrder = ko.observable(specifiedSortOrder || undefined),
            // Item Vdp Prices
            itemVdpPrices = ko.observableArray([]),
            // Can Add Item Vdp Price
            canAddItemVdpPrice = ko.computed(function() {
                return itemVdpPrices.length < 15;
            }),
            // Add Item Vdp Price
            addItemVdpPrice = function () {
                itemVdpPrices.push(ItemVdpPrice.Create({ ItemId: id() }));
            },
            // Remove Item Vdp Price
            removeItemVdpPrice = function (itemVdpPrice) {
                itemVdpPrices.remove(itemVdpPrice);
            },
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0 && itemVdpPrices.filter(function (itemVdpPrice) {
                    return !itemVdpPrice.isValid();
                }).length === 0;
            }),
            // True if the product has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                name: name,
                code: code,
                productName: productName,
                productCode: productCode,
                isArchived: isArchived,
                isPublished: isPublished,
                isEnabled: isEnabled,
                isFeatured: isFeatured,
                isVdpProduct: isVdpProduct,
                isStockControl: isStockControl,
                sortOrder: sortOrder,
                isFinishedGoods: isFinishedGoods,
                itemVdpPrices: itemVdpPrices
            }),
            // Item Vdp Prices has changes
            itemVdpPriceListHasChanges = ko.computed(function () {
                var itemVdpPriceItem = itemVdpPrices.find(function (itemVdpPrice) {
                    return itemVdpPrice.hasChanges();
                });
                if (!itemVdpPriceItem) {
                    return false;
                }

                return itemVdpPriceItem.hasChanges();
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty() || itemVdpPriceListHasChanges();
            }),
            // Reset
            reset = function () {
                itemVdpPrices.each(function (itemVdpPrice) {
                    return itemVdpPrice.reset();
                });
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    ItemId: id(),
                    ItemCode: code(),
                    ProductCode: productCode(),
                    ProductName: productName(),
                    IsArchived: isArchived(),
                    IsEnabled: isEnabled(),
                    IsPublished: isPublished(),
                    IsFeatured: isFeatured(),
                    IsVdpProduct: isVdpProduct(),
                    IsStockControl: isStockControl(),
                    SortOrder: sortOrder(),
                    IsFinishedGoods: isFinishedGoods(),
                    ItemVdpPrices: itemVdpPrices.map(function (itemVdpPrice) {
                        return itemVdpPrice.convertToServerData();
                    })
                }
            };

        return {
            id: id,
            name: name,
            code: code,
            productName: productName,
            productNameForGrid: productNameForGrid,
            productCode: productCode,
            thumbnail: thumbnail,
            miniPrice: miniPrice,
            isArchived: isArchived,
            isPublished: isPublished,
            isArchivedUi: isArchivedUi,
            isPublishedUi: isPublishedUi,
            productCategoryName: productCategoryName,
            isEnabled: isEnabled,
            isFeatured: isFeatured,
            isVdpProduct: isVdpProduct,
            isStockControl: isStockControl,
            sortOrder: sortOrder,
            isFinishedGoods: isFinishedGoods,
            isFinishedGoodsUi: isFinishedGoodsUi,
            itemVdpPrices: itemVdpPrices,
            canAddItemVdpPrice: canAddItemVdpPrice,
            addItemVdpPrice: addItemVdpPrice,
            removeItemVdpPrice: removeItemVdpPrice,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            itemVdpPriceListHasChanges: itemVdpPriceListHasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Item VdpPrice Entity
    ItemVdpPrice = function (specifiedId, specifiedClickRangeTo, specifiedClickRangeFrom, specifiedPricePerClick, specifiedSetupCharge, specifiedItemId) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId || 0),
            // click Range To
            clickRangeTo = ko.observable(specifiedClickRangeTo || undefined),
            // Click Range From
            clickRangeFrom = ko.observable(specifiedClickRangeFrom || undefined),
            // Price Per Click
            pricePerClick = ko.observable(specifiedPricePerClick || undefined),
            // Setup Charge
            setupCharge = ko.observable(specifiedSetupCharge || undefined),
            // Item Id
            itemId = ko.observable(specifiedItemId || 0),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // True if the Item Vdp Price has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                clickRangeFrom: clickRangeFrom,
                clickRangeTo: clickRangeTo,
                pricePerClick: pricePerClick,
                setupCharge: setupCharge
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    ItemVdpPriceId: id(),
                    ItemId: itemId(),
                    ClickRangeFrom: clickRangeFrom(),
                    ClickRangeTo: clickRangeTo(),
                    PricePerClick: pricePerClick(),
                    SetupCharge: setupCharge()
                }
            };

        return {
            id: id,
            itemId: itemId,
            clickRangeFrom: clickRangeFrom,
            clickRangeTo: clickRangeTo,
            pricePerClick: pricePerClick,
            setupCharge: setupCharge,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    };

    // Item Vdp Price Factory
    ItemVdpPrice.Create = function (source) {
        return new ItemVdpPrice(source.ItemVdpPriceId, source.ClickRangeTo, source.ClickRangeFrom, source.PricePerClick, source.SetupCharge, source.ItemId);
    }

    // Item Factory
    Item.Create = function (source) {
        var item = new Item(source.ItemId, source.ItemName, source.ItemCode, source.ProductName, source.ProductCode, source.ThumbnailPath, source.MinPrice,
            source.IsArchived, source.IsPublished, source.ProductCategoryName, source.IsEnabled, source.IsFeatured, source.IsFinishedGoods, source.SortOrder,
            source.IsStockControl, source.IsVdpProduct);

        // Map Item Vdp Prices if any
        if (source.ItemVdpPrices && source.ItemVdpPrices.length > 0) {
            var itemVdpPrices = [];

            _.each(source.ItemVdpPrices, function(itemVdpPrice) {
                itemVdpPrices.push(ItemVdpPrice.Create(itemVdpPrice));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(item.itemVdpPrices(), itemVdpPrices);
            item.itemVdpPrices.valueHasMutated();
        }

        // Reset State to Un-Modified
        item.reset();

        return item;
    }

    return {
        // Item Constructor
        Item: Item,
        // Item Vdp Price Constructor
        ItemVdpPrice: ItemVdpPrice
    };
});