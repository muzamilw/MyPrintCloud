/*
    Module with the view model for the Product.
*/
define("product/product.viewModel",
    ["jquery", "amplify", "ko", "product/product.dataservice", "product/product.model"],
    function ($, amplify, ko, dataservice, model) {
        var ist = window.ist || {};
        ist.product = {
            viewModel: (function () {
                var // the view 
                    view,
                    // Active
                    selectedProduct = ko.observable(),
                    //Orgnization Image
                    orgnizationImage = ko.observable(),
                    // #region Arrays
                    // Items
                    products = ko.observableArray([]),
                    // Error List
                    errorList = ko.observableArray([]),
                    // #endregion Arrays
                    // #region Busy Indicators
                    isLoadingProducts = ko.observable(false),
                    // #endregion Busy Indicators
                    // #region Observables
                    // Sort On
                    sortOn = ko.observable(1),
                    // Sort Order -  true means asc, false means desc
                    sortIsAsc = ko.observable(true),
                    // Pagination
                    pager = ko.observable(),
                    // #region Utility Functions
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);

                        // Get Items
                        getItems();
                    },
                    // Map Products 
                    mapProducts = function(data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.Product.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(products(), itemsList);
                        products.valueHasMutated();
                    },
                    // Get Items
                    getItems = function () {
                        isLoadingProducts(true);
                        dataservice.getItems({}, {
                            success: function (data) {
                                mapProducts(data);
                                isLoadingProducts(false);
                            },
                            error: function (response) {
                                isLoadingProducts(false);
                                toastr.error("Failed to load items" + response);
                            }
                        });
                    };
                    // #endregion Service Calls

                return {
                    // Observables
                    selectedProduct: selectedProduct,
                    orgnizationImage: orgnizationImage,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    isLoadingProducts: isLoadingProducts,
                    products: products,
                    //Arrays
                    errorList: errorList,
                    // Utility Methods
                    initialize: initialize,
                    pager: pager
                    // Utility Methods
                    
                };
            })()
        };
        return ist.product.viewModel;
    });
