/*
    Module with the view model for the Product.
*/
define("product/product.viewModel",
    ["jquery", "amplify", "ko", "product/product.dataservice", "product/product.model", "common/pagination", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation) {
        var ist = window.ist || {};
        ist.product = {
            viewModel: (function () {
                var // the view 
                    view,
                    // #region Arrays
                    // Items
                    products = ko.observableArray([]),
                    // Items to Relate
                    productsToRelate = ko.observableArray([]),
                    // Error List
                    errorList = ko.observableArray([]),
                    // #endregion Arrays
                    // #region Busy Indicators
                    isLoadingProducts = ko.observable(false),
                    // Is List View Active
                    isListViewVisible = ko.observable(false),
                    // Is Grid View Active
                    isGridViewVisible = ko.observable(true),
                    // Is Product Editor Visible
                    isProductDetailsVisible = ko.observable(false),
                    // #endregion Busy Indicators
                    // #region Observables
                    // Item File Types
                    itemFileTypes = {
                        thumbnail: 1,
                        grid: 2,
                        imagePath: 3
                    },
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
                    // Pagination For Item Relater Dialog
                    itemRelaterPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, productsToRelate)),
                    // Current Page - Editable
                    currentPageCustom = ko.computed({
                        read: function () {
                            return pager().currentPage();
                        },
                        write: function (value) {
                            if (!value) {
                                return;
                            }
                            var page = parseInt(value);
                            if (page === pager().currentPage() || !pager().isPageValid(page)) {
                                return;
                            }
                            pager().currentPage(page);
                            getItems();
                        }
                    }),
                    // Item Actions
                    itemActions = {
                        onSaveVideo: function () {
                            closeVideoDialog();
                        },
                        onSelectRelatedItem: function () {
                            closeRelatedItemDialog();
                        }
                    },
                    // Selected Job Description
                    selectedJobDescription = ko.observable(),
                    // #region Utility Functions
                    toggleView = function (data, e) {
                        view.changeView(e);
                    },
                    // Set List View Active
                    setListViewActive = function () {
                        isListViewVisible(true);
                        isGridViewVisible(false);
                    },
                    // Set Grid View Active
                    setGridViewActive = function () {
                        isListViewVisible(false);
                        isGridViewVisible(true);
                    },
                    // Create New Product
                    createProduct = function () {
                        selectedProduct(model.Item.Create({}, itemActions));
                        openProductEditor();
                    },
                    // Edit Product
                    editProduct = function (data) {
                        getItemById(data.id(), openProductEditor);
                    },
                    // Open Editor
                    openProductEditor = function () {
                        isProductDetailsVisible(true);
                        view.initializeDropZones();
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
                        selectedProduct(model.Item.Create({}, itemActions));
                        resetVideoCounter();
                        isProductDetailsVisible(false);
                    },
                    // On Archive
                    onArchiveProduct = function (item) {
                        confirmation.afterProceed(function () {
                            archiveProduct(item.id());
                        });
                        confirmation.show();
                    },
                    // Active Video Item
                    activeVideo = ko.observable(),
                    // Added Video Counter
                    videoCounter = -1,
                    // Reset Video Counter
                    resetVideoCounter = function () {
                        videoCounter = -1;
                    },
                    // On Add Video
                    onAddVideo = function () {
                        // Open Video Dialog  
                        openVideoDialog();
                        activeVideo(model.ItemVideo.Create({ VideoId: 0, ItemId: selectedProduct().id() }));
                    },
                    onEditVideo = function (itemVideo) {
                        // Open Video Dialog  
                        openVideoDialog();
                        activeVideo(itemVideo);
                    },
                    // Save Video
                    saveVideo = function () {
                        if (activeVideo().id() === 0) { // Add
                            activeVideo().id(videoCounter);
                            selectedProduct().addVideo(activeVideo());
                            videoCounter -= 1;
                            return;
                        }
                        closeVideoDialog();
                    },
                    // Open Video Dialog
                    openVideoDialog = function () {
                        // Reset Current Video Item
                        activeVideo(model.ItemVideo.Create({}));
                        view.showVideoDialog();
                    },
                    // Close Video Dialog
                    closeVideoDialog = function () {
                        view.hideVideoDialog();
                    },
                    // On Add Related Item
                    onAddRelatedItem = function () {
                        // Reset Filter & Pager
                        filterTextForRelatedItems(undefined);
                        // Reset Pager
                        itemRelaterPager().reset();
                        // Get Items 
                        getItemsToRelate(openRelatedItemDialog);
                    },
                    // Open Related Item Dialog
                    openRelatedItemDialog = function () {
                        view.showRelatedItemDialog();
                    },
                    // Close Related Item Dialog
                    closeRelatedItemDialog = function () {
                        view.hideRelatedItemDialog();
                    },
                    // Select Job Description
                    selectJobDescription = function (jobDescription, e) {
                        selectedJobDescription(e.currentTarget.id);
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);

                        pager(new pagination.Pagination({ PageSize: 5 }, products, getItems));

                        itemRelaterPager(new pagination.Pagination({ PageSize: 5 }, productsToRelate, getItemsToRelate)),

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
                    // Map Products To Relate
                    mapProductsToRelate = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            if (item.ItemId !== selectedProduct().id()) { // Can't Relate Product with itself
                                itemsList.push(model.ItemRelatedItem.Create({
                                    RelatedItemId: item.ItemId,
                                    RelatedItemName: item.ProductName,
                                    RelatedItemCode: item.ProductCode
                                }));
                            }
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(productsToRelate(), itemsList);
                        productsToRelate.valueHasMutated();
                    },
                    // Filter Products to Relate
                    filterProductsToRelate = function () {
                        // Reset Pager
                        itemRelaterPager().reset();
                        // Get Items to Relate
                        getItemsToRelate();
                    },
                    // Reset Filtered Products
                    resetFilteredProducts = function () {
                        // Reset Text 
                        filterTextForRelatedItems(undefined);
                        // Filter Record
                        filterProductsToRelate();
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
                    onSaveProduct = function () {
                        if (!doBeforeSave()) {
                            return;
                        }

                        saveProduct(closeProductEditor);
                    },
                    // Do Before Save
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedProduct().isValid()) {
                            selectedProduct().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                    // Get Item From List
                    getItemFromList = function (id) {
                        return products.find(function (item) {
                            return item.id() === id;
                        });
                    },
                    // Save Product
                    saveProduct = function (callback) {
                        dataservice.saveItem(selectedProduct().convertToServerData(), {
                            success: function (data) {
                                if (!selectedProduct().id()) {
                                    // Update Id
                                    selectedProduct().id(data.ItemId);

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
                                    }
                                }

                                toastr.success("Saved Successfully.");

                                if (callback && typeof callback === "function") {
                                    callback();
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Save Product. Error: " + response);
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
                    // Get Items To Relate
                    getItemsToRelate = function (callback) {
                        dataservice.getItems({
                            SearchString: filterTextForRelatedItems(),
                            PageSize: itemRelaterPager().pageSize(),
                            PageNo: itemRelaterPager().currentPage()
                        }, {
                            success: function (data) {
                                productsToRelate.removeAll();
                                if (data && data.TotalCount > 0) {
                                    itemRelaterPager().totalCount(data.TotalCount);
                                    mapProductsToRelate(data.Items);
                                }
                                if (callback && typeof callback === "function") {
                                    callback();
                                }
                            },
                            error: function (response) {
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
                                    selectedProduct(model.Item.Create(data, itemActions));

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
                    isListViewVisible: isListViewVisible,
                    isGridViewVisible: isGridViewVisible,
                    isProductDetailsVisible: isProductDetailsVisible,
                    pager: pager,
                    errorList: errorList,
                    currentPageCustom: currentPageCustom,
                    filterText: filterText,
                    pageHeader: pageHeader,
                    filterTextForRelatedItems: filterTextForRelatedItems,
                    itemRelaterPager: itemRelaterPager,
                    activeVideo: activeVideo,
                    productsToRelate: productsToRelate,
                    selectedJobDescription: selectedJobDescription,
                    itemFileTypes: itemFileTypes,
                    // Utility Methods
                    initialize: initialize,
                    resetFilter: resetFilter,
                    filterProducts: filterProducts,
                    resetFilteredProducts: resetFilteredProducts,
                    filterProductsToRelate: filterProductsToRelate,
                    toggleView: toggleView,
                    setListViewActive: setListViewActive,
                    setGridViewActive: setGridViewActive,
                    editProduct: editProduct,
                    createProduct: createProduct,
                    onSaveProduct: onSaveProduct,
                    onCloseProductEditor: onCloseProductEditor,
                    onArchiveProduct: onArchiveProduct,
                    closeVideoDialog: closeVideoDialog,
                    onAddVideo: onAddVideo,
                    onEditVideo: onEditVideo,
                    onAddRelatedItem: onAddRelatedItem,
                    saveVideo: saveVideo,
                    selectJobDescription: selectJobDescription
                    // Utility Methods

                };
            })()
        };
        return ist.product.viewModel;
    });
