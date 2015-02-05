/*
    Module with the view model for the Order.
*/
define("order/order.viewModel",
    ["jquery", "amplify", "ko", "order/order.dataservice", "order/order.model", "common/pagination", "common/confirmation.viewModel",
        "common/sharedNavigation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, shared) {
        var ist = window.ist || {};
        ist.order = {
            viewModel: (function () {
                var // the view 
                    view,
                    // #region Arrays
                    // orders
                    orders = ko.observableArray([]),
                    errorList = ko.observableArray([]),
                    // #endregion Arrays
                    // #region Busy Indicators
                    isLoadingProducts = ko.observable(false),
                    // Is Order Editor Visible
                    isOrderDetailsVisible = ko.observable(false),
                    // filter
                    filterText = ko.observable(),
                    // filter for Related Items
                    filterTextForRelatedItems = ko.observable(),
                    // Active Product
                    selectedProduct = ko.observable(),
                    // Page Header 
                    pageHeader = ko.computed(function () {
                        return selectedProduct() && selectedProduct().productName() ? selectedProduct().productName() : 'Products';
                    }),
                    // Sort On
                    sortOn = ko.observable(1),
                    // Sort Order -  true means asc, false means desc
                    sortIsAsc = ko.observable(true),
                    // Pagination
                    pager = ko.observable(new pagination.Pagination({ PageSize: 5 }, products)),
                    // Create New Product
                    createProduct = function () {
                        selectedProduct(model.Item.Create({}));
                        openProductEditor();
                    },
                    // Edit Product
                    editProduct = function (data) {
                        getItemById(data.id(), openProductEditor);
                    },
                    // Open Editor
                    openProductEditor = function () {
                        
                    },
                    // On Close Editor
                    onCloseProductEditor = function () {
                        if (selectedProduct().hasChanges()) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(onSaveProduct);
                            confirmation.afterCancel(function () {
                                selectedProduct().reset();
                                closeProductEditor();
                            });
                            confirmation.show();
                            return;
                        }
                        closeProductEditor();
                    },
                    // Close Editor
                    closeProductEditor = function () {
                        selectedProduct(model.Item.Create({}));
                        resetVideoCounter();
                        isProductDetailsVisible(false);
                        selectedDesignerCategory(undefined);
                        selectedRegionId(undefined);
                        selectedCategoryTypeId(undefined);
                        errorList.removeAll();
                    },
                    // On Archive
                    onArchiveProduct = function (item) {
                        confirmation.afterProceed(function () {
                            archiveProduct(item.id());
                        });
                        confirmation.show();
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);

                        pager(new pagination.Pagination({ PageSize: 5 }, products, getItems));

                        // Get Base Data
                        getBaseData();

                        // Get Items
                        getItems();

                    },
                    // Map Products 
                    mapProducts = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.Item.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(products(), itemsList);
                        products.valueHasMutated();
                    },
                    // Filter Products
                    filterProducts = function () {
                        // Reset Pager
                        pager().reset();
                        // Get Items
                        getItems();
                    },
                    // Reset Filter
                    resetFilter = function () {
                        // Reset Text 
                        filterText(undefined);
                        // Filter Record
                        filterProducts();
                    },
                    // On Save Product
                    onSaveProduct = function (data, event, navigateCallback) {
                        if (!doBeforeSave()) {
                            return;
                        }

                        saveProduct(closeProductEditor, navigateCallback);
                    },
                    // Do Before Save
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedProduct().isValid()) {
                            selectedProduct().showAllErrors();
                            selectedProduct().setValidationSummary(errorList);
                            flag = false;
                        }
                        return flag;
                    },
                    // On Clone Product
                    onCloneProduct = function(data) {
                        cloneProduct(data, openProductEditor);
                    },
                    // Go To Element
                    gotoElement = function (validation) {
                        view.gotoElement(validation.element);
                    },
                    
                    // Get Base Data
                    getBaseData = function () {
                        dataservice.getBaseData({
                            success: function (data) {
                                if (data) {
                                    
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load base data" + response);
                            }
                        });
                    },
                    // Save Product
                    saveProduct = function (callback, navigateCallback) {
                        var product = selectedProduct().convertToServerData();
                       
                        dataservice.saveItem(product, {
                            success: function (data) {
                                if (!selectedProduct().id()) {
                                    // Update Id
                                    selectedProduct().id(data.ItemId);

                                    // Update Min Price
                                    selectedProduct().miniPrice(data.MinPrice || 0);

                                    // Add to top of list
                                    products.splice(0, 0, selectedProduct());
                                }
                                else {
                                    // Get Item
                                    var item = getItemFromList(selectedProduct().id());
                                    if (item) {
                                        item.productCode(data.ProductCode);
                                        item.productName(data.ProductName);
                                        item.isEnabled(data.IsEnabled);
                                        item.isPublished(data.IsPublished);
                                        item.miniPrice(data.MinPrice || 0);
                                    }
                                }

                                toastr.success("Saved Successfully.");

                                if (callback && typeof callback === "function") {
                                    callback();
                                }
                                
                                if (navigateCallback && typeof navigateCallback === "function") {
                                    navigateCallback();
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Save Product. Error: " + response);
                            }
                        });
                    },
                    // Clone Product
                    cloneProduct = function (item, callback) {
                        dataservice.cloneItem({ ItemId: item.id() }, {
                            success: function (data) {
                                if (data) {
                                    var newItem = model.Item.Create(data);
                                    // Add to top of list
                                    products.splice(0, 0, newItem);
                                    selectedProduct(newItem);
                                    
                                    if (callback && typeof callback === "function") {
                                        callback();
                                    }
                                }

                                toastr.success("Cloned Successfully.");
                            },
                            error: function (response) {
                                toastr.error("Failed to Clone Product. Error: " + response);
                            }
                        });
                    },
                    // archive Product
                    archiveProduct = function () {
                        dataservice.archiveItem({
                            ItemId: selectedProduct().id()
                        }, {
                            success: function () {
                                selectedProduct().isArchived(true);
                                toastr.success("Archived Successfully.");
                            },
                            error: function (response) {
                                toastr.error("Failed to archive Product. Error: " + response);
                            }
                        });
                    },
                    
                    // Get Items
                    getItems = function () {
                        isLoadingProducts(true);
                        dataservice.getItems({
                            SearchString: filterText(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage()
                        }, {
                            success: function (data) {
                                products.removeAll();
                                if (data && data.TotalCount > 0) {
                                    pager().totalCount(data.TotalCount);
                                    mapProducts(data.Items);
                                }
                                isLoadingProducts(false);
                            },
                            error: function (response) {
                                isLoadingProducts(false);
                                toastr.error("Failed to load items" + response);
                            }
                        });
                    },
                    // Get Item By Id
                    getItemById = function (id, callback) {
                        isLoadingProducts(true);
                        dataservice.getItem({
                            id: id
                        }, {
                            success: function (data) {
                                if (data) {
                                    selectedProduct(model.Item.Create(data));

                                    if (callback && typeof callback === "function") {
                                        callback();
                                    }
                                }
                                isLoadingProducts(false);
                            },
                            error: function (response) {
                                isLoadingProducts(false);
                                toastr.error("Failed to load item details" + response);
                            }
                        });
                    };
                // #endregion Service Calls

                return {
                    // Observables
                    selectedProduct: selectedProduct,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    isLoadingProducts: isLoadingProducts,
                    products: products,
                    isProductDetailsVisible: isProductDetailsVisible,
                    pager: pager,
                    errorList: errorList,
                    filterText: filterText,
                    pageHeader: pageHeader,
                    // Utility Methods
                    initialize: initialize,
                    resetFilter: resetFilter,
                    filterProducts: filterProducts,
                    resetFilteredProducts: resetFilteredProducts,
                    editProduct: editProduct,
                    createProduct: createProduct,
                    onSaveProduct: onSaveProduct,
                    onCloseProductEditor: onCloseProductEditor,
                    onArchiveProduct: onArchiveProduct,
                    gotoElement: gotoElement,
                    onCloneProduct: onCloneProduct
                };
            })()
        };
        return ist.order.viewModel;
    });
