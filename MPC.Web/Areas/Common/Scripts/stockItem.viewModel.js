/*
    Module with the view model for Stock Item
*/
define("common/stockItem.viewModel",
    ["jquery", "amplify", "ko", "common/stockItem.dataservice", "common/stockItem.model", "common/pagination"], function ($, amplify, ko, dataservice, model,
    pagination) {
        var ist = window.ist || {};
        ist.stockItem = {
            viewModel: (function () {
                var // The view 
                    view,
                    // Stock Items
                    stockItems = ko.observableArray([]),
                    // Stock Dialog Filter
                    stockDialogFilter = ko.observable(),
                    // Stock Dialog Cat Filter
                    stockDialogCatFilter = ko.observable(),
                    // Is Category Filter Visible
                    isCategoryFilterVisible = ko.observable(),
                    // Pagination For Press Dialog
                    stockDialogPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, stockItems)),
                    // Search Stock Items
                    searchStockItems = function () {
                        stockDialogPager().reset();
                        getStockItems();
                    },
                    // Reset Stock Items
                    resetStockItems = function () {
                        // Reset Text 
                        resetStockDialogFilters();
                        // Reset Callback
                        afterSelect = null;
                        // Reset List
                        stockItems.removeAll();
                        // Reset Pager
                        stockDialogPager().reset();
                    },
                    // after selection
                    afterSelect = null,
                    // Reset Stock Dialog Filters
                    resetStockDialogFilters = function () {
                        // Reset Text 
                        stockDialogFilter(undefined);
                        // Reset Category
                        stockDialogCatFilter(undefined);
                    },
                    // Show
                    show = function (afterSelectCallback, stockCategoryId, isStockCategoryFilterVisible) {
                        resetStockItems();
                        view.showDialog();
                        if (stockCategoryId) {
                            stockDialogCatFilter(stockCategoryId);
                        }
                        if (isStockCategoryFilterVisible !== null && isStockCategoryFilterVisible !== undefined) {
                            isCategoryFilterVisible(isStockCategoryFilterVisible);
                        }
                        
                        afterSelect = afterSelectCallback;
                        getStockItems();
                    },
                    // On Select Stock Item
                    onSelectStockItem = function (stockItem) {
                        if (afterSelect && typeof afterSelect === "function") {
                            afterSelect(stockItem);
                        }

                        view.hideDialog();
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        stockDialogPager(new pagination.Pagination({ PageSize: 5 }, stockItems, getStockItems));
                    },
                    // Map Stock Items 
                    mapStockItems = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.StockItem.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(stockItems(), itemsList);
                        stockItems.valueHasMutated();
                    },
                    // Get Stock Items
                    getStockItems = function () {
                        dataservice.getStockItems({
                            SearchString: stockDialogFilter(),
                            PageSize: stockDialogPager().pageSize(),
                            PageNo: stockDialogPager().currentPage(),
                            CategoryId: stockDialogCatFilter(),
                        }, {
                            success: function (data) {
                                stockItems.removeAll();
                                if (data && data.TotalCount > 0) {
                                    stockDialogPager().totalCount(data.TotalCount);
                                    mapStockItems(data.StockItems);
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load stock items" + response);
                            }
                        });
                    };

                return {
                    //Arrays
                    stockDialogFilter: stockDialogFilter,
                    stockDialogCatFilter: stockDialogCatFilter,
                    stockItems: stockItems,
                    searchStockItems: searchStockItems,
                    resetStockItems: resetStockItems,
                    stockDialogPager: stockDialogPager,
                    //Utilities
                    onSelectStockItem: onSelectStockItem,
                    initialize: initialize,
                    show: show
                };
            })()
        };

        return ist.stockItem.viewModel;
    });

