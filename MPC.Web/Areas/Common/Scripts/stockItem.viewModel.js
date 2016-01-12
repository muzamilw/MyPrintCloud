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
                    // Stock Categories
                    categories = ko.observableArray([]),

                    // Stock sub Categories
                    subCategories = ko.observableArray([]),
                    // Stock Dialog Filter
                    stockDialogFilter = ko.observable(),
                    // Stock Dialog Cat Filter
                    stockDialogCatFilter = ko.observable(),

                     // Stock Dialog Sub Cat Filter
                    stockDialogSubCatFilter = ko.observable(),
                    // Is Category Filter Visible
                    isCategoryFilterVisible = ko.observable(),
                    // Is Base Data Loaded
                    isBaseDataLoaded = ko.observable(false),
                    // To stop multiple calss
                    isPageLoaded = ko.observable(false),
                    // Pagination For Press Dialog
                    stockDialogPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, stockItems)),
                    //Company Tax Rate
                    companyTaxRate = null,
                    paperType = null,
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
                       // stockDialogCatFilter(undefined);
                    },
                     currency = ko.observable(),
                    // Show
                    show = function (afterSelectCallback, stockCategoryId, isStockCategoryFilterVisible, currencySmb, companyTaxRateParam, paperTypeParam) {
                        currency(currencySmb);
                        resetStockItems();
                        view.showDialog();
                        companyTaxRate = companyTaxRateParam;
                        paperType = paperTypeParam;
                        if (stockCategoryId) {
                            // reset field
                            isPageLoaded(false);
                            stockDialogCatFilter(stockCategoryId);
                        }
                        if (isStockCategoryFilterVisible !== null && isStockCategoryFilterVisible !== undefined) {
                            isCategoryFilterVisible(isStockCategoryFilterVisible);
                        }
                        afterSelect = afterSelectCallback;
                       

                        getStockItems();
                        if (!isBaseDataLoaded())
                        getStockCategories();
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
                        // Subscribe Category Filter Change
                        stockDialogCatFilter.subscribe(function () {
                            if (isPageLoaded()) {  // prevents multiple calls when page loads
                                searchStockItems();
                            }
                        });

                        stockDialogSubCatFilter.subscribe(function () {
                            if (isPageLoaded()) {  // prevents multiple calls when page loads
                                searchStockItems();
                            }
                        });
                    },
                    // Map Stock Items 
                    mapStockItems = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            item.CompanyTaxRate = companyTaxRate;
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
                            SubCategoryId: stockDialogSubCatFilter(),
                            PaperType: paperType
                        }, {
                            success: function (data) {
                                stockItems.removeAll();
                                
                                if (data && data.TotalCount > 0) {
                                    mapStockItems(data.StockItems);
                                    stockDialogPager().totalCount(data.TotalCount);
                                }
                                isPageLoaded(true);
                            },
                            error: function (response) {
                                toastr.error("Failed to load stock items" + response);
                            }
                        });
                    },

                     // Filtered based on category Id
                    filteredSubCategoriesForDetail = ko.computed(function () {
                        if (subCategories().length === 0) {
                            return [];
                        }
                        return subCategories.filter(function (subCategory) {
                            return subCategory.CategoryId === (stockDialogCatFilter() !== undefined ? stockDialogCatFilter() : 0);
                        });
                    }),


                    // Get StockCategories
                    getStockCategories = function () {
                        dataservice.getStockCategories({

                        }, {
                            success: function (data) {
                              
                                if (data) {
                                    categories.removeAll();
                                    _.each(data.StockCategories, function (item) {
                                        categories.push(item);
                                    });

                                    subCategories.removeAll();
                                    _.each(data.StockSubCategories, function (item) {
                                        subCategories.push(item);
                                    });
                                }
                                isBaseDataLoaded(true);
                            },
                            error: function (response) {
                                toastr.error("Failed to load stock categories" + response);
                            }
                        });
                    };

                //// Get StockSubCategories
                //getStockSubCategories = function () {
                //    dataservice.getStockCategories({
                //        SearchString: stockDialogFilter(),
                //        PageSize: stockDialogPager().pageSize(),
                //        PageNo: stockDialogPager().currentPage(),
                //        CategoryId: stockDialogCatFilter()
                //    }, {
                //        success: function (data) {

                //            if (data) {
                //                subCategories.removeAll();
                //                _.each(data, function (item) {
                //                    subCategories.push(item);
                //                });
                //            }
                //            isBaseDataLoaded(true);
                //        },
                //        error: function (response) {
                //            toastr.error("Failed to load stock sub categories" + response);
                //        }
                //    });
                //};

                return {
                    //Arrays
                    stockDialogFilter: stockDialogFilter,
                    stockDialogCatFilter: stockDialogCatFilter,
                    stockDialogSubCatFilter: stockDialogSubCatFilter,
                    stockItems: stockItems,
                    searchStockItems: searchStockItems,
                    resetStockItems: resetStockItems,
                    stockDialogPager: stockDialogPager,
                    //Utilities
                    isCategoryFilterVisible:isCategoryFilterVisible,
                    companyTaxRate: companyTaxRate,
                    onSelectStockItem: onSelectStockItem,
                    initialize: initialize,
                    categories:categories,
                    show: show,
                    currency: currency,
                    filteredSubCategoriesForDetail: filteredSubCategoriesForDetail
                };
            })()
        };

        return ist.stockItem.viewModel;
    });

