define("stores/store.Product.viewModel",
    ["jquery", "amplify", "ko", "stores/store.Product.dataservice", "stores/stores.model", "common/confirmation.viewModel", "common/pagination", "stores/stores.viewModel", "stores/store.Product.model"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, storeViewModel, storeProductModel) {
        var ist = window.ist || {};
        ist.storeProduct = {
            viewModel: (function () {
                var // #region Arrays
                    // Items
                    products = ko.observableArray([]),
                    // Items to Relate
                    productsToRelate = ko.observableArray([]),
                    // Error List
                    errorList = ko.observableArray([]),
                    // Stock Items
                    stockItems = ko.observableArray([]),
                    // Cost Centres
                    costCentres = ko.observableArray([]),
                    // Section Flags
                    sectionFlags = ko.observableArray([]),
                    // Countries
                    countries = ko.observableArray([]),
                    // States
                    states = ko.observableArray([]),
                    //New Added Products
                    newAddedProducts = ko.observableArray([]),
                    //Editted Products
                    edittedProducts = ko.observableArray([]),
                    //DeletedProducts
                    deletedproducts = ko.observableArray([]),
                    // #endregion Arrays
                // #region Observables
                // #region Busy Indicators
                    isLoadingProducts = ko.observable(false),
                    // Is List View Active
                    isListViewVisible = ko.observable(false),
                    // Is Grid View Active
                    isGridViewVisible = ko.observable(true),
                    // Is Product Editor Visible
                    isProductDetailsVisible = ko.observable(false),
                    // #endregion Busy Indicators
                //#region Region For Store Product Observables
                    newAddedStoreProductId = -1,
                //#endregion
                // Item File Types
                itemFileTypes = {
                    thumbnail: 1,
                    grid: 2,
                    imagePath: 3,
                    file1: 4
                },
                // filter
                filterText = ko.observable(),
                // filter for Related Items
                filterTextForRelatedItems = ko.observable(),
                // Active Product
                selectedProduct = ko.observable(),
                // Sort On
                sortOn = ko.observable(1),
                // Sort Order -  true means asc, false means desc
                sortIsAsc = ko.observable(true),
                // Pagination
                pager = ko.observable(new pagination.Pagination({ PageSize: 5 }, products)),
                // Pagination For Item Relater Dialog
                itemRelaterPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, productsToRelate)),
                // Pagination For Stock Item Dialog
                stockDialogPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, stockItems)),
                // #endregion Observables
                //#region Computed
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
                //#endregion Computed
                //#region Product Screen Work
                    // Item Actions
                itemActions = {
                    onSaveVideo: function () {
                        closeVideoDialog();
                    },
                    onSelectRelatedItem: function () {
                        closeRelatedItemDialog();
                    },
                    onChooseStockItem: function () {
                        openStockItemDialog();
                    },
                    onSelectStockItem: function () {
                        closeStockItemDialog();
                    },
                    onUpdateItemAddonCostCentre: function () {
                        openItemAddonCostCentreDialog();
                    },
                    onSaveItemAddonCostCentre: function () {
                        closeItemAddonCostCentreDialog();
                    },
                    onCostCentreChange: function (costCentreId, activeItemAddonCostCentre) {
                        setCostCentreToActiveItemAddonCostCentre(costCentreId, activeItemAddonCostCentre);
                    },
                    onFlagChange: function (flagId, itemId) {
                        getItemPriceMatricesForItemByFlag(flagId, itemId);
                    }
                },
                // Item State Tax Constructor Params
                itemStateTaxConstructorParams = { countries: countries(), states: states() },
                // Selected Job Description
                selectedJobDescription = ko.observable(),
                // Stock Dialog Filter
                stockDialogFilter = ko.observable(),
                // #region Utility Functions
                toggleView = function (data, e) {
                    ist.stores.view.changeView(e);
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
                    selectedProduct(storeProductModel.Item.Create({}, itemActions, itemStateTaxConstructorParams));
                    openProductEditor();
                },
                //// Edit Product
                //editProduct = function (data) {
                //    if (selectedProduct() !== data) {
                //        selectProduct(data);
                //    }
                //    getItemById(data.id(), openProductEditor);
                //},
                // Open Editor
                openProductEditor = function () {
                    isProductDetailsVisible(true);
                    ist.stores.view.initializeDropZones();
                },
                // On Close EditorisProductDetailsVisible
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
                    selectedProduct(storeProductModel.Item.Create({}, itemActions, itemStateTaxConstructorParams));
                    resetVideoCounter();
                    isProductDetailsVisible(false);
                },
                // On Archive
                onArchiveProduct = function (item) {
                    confirmation.afterProceed(function () {
                        products.remove(item);
                        //#region If Product Is New
                        if (item.id() > 0) {
                            deletedproducts.push(item);
                            _.each(edittedProducts(), function (product) {
                                if (product.id() == item.id()) {
                                    edittedProducts.remove(product);
                                }
                            });
                        }
                        //#endregion
                        //#region If Product Is Coming from DB
                        if (item.id() < 0) {
                            newAddedProducts.remove(item);
                        }
                        //#endregion
                       
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
                    activeVideo(storeProductModel.ItemVideo.Create({ VideoId: 0, ItemId: selectedProduct().id() }));
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
                    activeVideo(storeProductModel.ItemVideo.Create({}));
                    ist.stores.view.showVideoDialog();
                },
                // Close Video Dialog
                closeVideoDialog = function () {
                    ist.stores.view.hideVideoDialog();
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
                    ist.stores.view.showRelatedItemDialog();
                },
                // Close Related Item Dialog
                closeRelatedItemDialog = function () {
                    ist.stores.view.hideRelatedItemDialog();
                },
                // Select Job Description
                selectJobDescription = function (jobDescription, e) {
                    selectedJobDescription(e.currentTarget.id);
                },
                // Search Stock Items
                searchStockItems = function () {
                    stockDialogPager().reset();
                    getStockItems();
                },
                // Reset Stock Items
                resetStockItems = function () {
                    // Reset Text 
                    stockDialogFilter(undefined);
                    // Filter Record
                    searchStockItems();
                },
                // Open Stock Item Dialog
                openStockItemDialog = function () {
                    ist.stores.view.showStockItemDialog();
                    searchStockItems();
                },
                // Close Stock Item Dialog
                closeStockItemDialog = function () {
                    ist.stores.view.hideStockItemDialog();
                },
                // Open Item Addon Cost Centre Dialog
                openItemAddonCostCentreDialog = function () {
                    ist.stores.view.showItemAddonCostCentreDialog();
                },
                // Close Item Addon Cost Centre Dialog
                closeItemAddonCostCentreDialog = function () {
                    ist.stores.view.hideItemAddonCostCentreDialog();
                },
                // Get Cost Centre By Id
                getCostCentreById = function (id) {
                    if (costCentres().length === 0) {
                        return null;
                    }

                    return costCentres.find(function (costCentre) {
                        return costCentre.id === id;
                    });
                },
                // Set Cost Centre to active Item Add on cost centre
                setCostCentreToActiveItemAddonCostCentre = function (costCentreId, activeItemAddonCostCentre) {
                    var costCentre = getCostCentreById(costCentreId);

                    if (!costCentre) {
                        return;
                    }

                    activeItemAddonCostCentre.costCentreName(costCentre.name);
                    activeItemAddonCostCentre.costCentreType(costCentre.type);
                },
                // Map Products 
                mapProducts = function (data) {
                    var itemsList = [];
                    _.each(data, function (item) {
                        itemsList.push(storeProductModel.Item.Create(item));
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
                            itemsList.push(storeProductModel.ItemRelatedItem.Create({
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
                // Map Stock Items 
                mapStockItems = function (data) {
                    var itemsList = [];
                    _.each(data, function (item) {
                        itemsList.push(storeProductModel.StockItem.Create(item));
                    });

                    // Push to Original Array
                    ko.utils.arrayPushAll(stockItems(), itemsList);
                    stockItems.valueHasMutated();
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

                    saveProduct();
                    closeProductEditor();
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
                // Map Cost Centres
                mapCostCentres = function (data) {
                    var itemsList = [];
                    _.each(data, function (item) {
                        itemsList.push(storeProductModel.CostCentre.Create(item));
                    });

                    // Push to Original Array
                    ko.utils.arrayPushAll(costCentres(), itemsList);
                    costCentres.valueHasMutated();
                },
                // Map Countreis
                mapCountries = function (data) {
                    var itemsList = [];
                    _.each(data, function (item) {
                        itemsList.push(storeProductModel.Country.Create(item));
                    });

                    // Push to Original Array
                    ko.utils.arrayPushAll(countries(), itemsList);
                    countries.valueHasMutated();
                },
                // Map State
                mapStates = function (data) {
                    var itemsList = [];
                    _.each(data, function (item) {
                        itemsList.push(storeProductModel.State.Create(item));
                    });

                    // Push to Original Array
                    ko.utils.arrayPushAll(states(), itemsList);
                    states.valueHasMutated();
                },
                // Map Section Flags
                mapSectionFlags = function (data) {
                    var itemsList = [];
                    _.each(data, function (item) {
                        itemsList.push(storeProductModel.SectionFlag.Create(item));
                    });

                    // Push to Original Array
                    ko.utils.arrayPushAll(sectionFlags(), itemsList);
                    sectionFlags.valueHasMutated();
                },
                // Set Item Price Matrices to Current Item against selected Flag
                setItemPriceMatricesToItem = function (itemPriceMatrices) {
                    if (!itemPriceMatrices || itemPriceMatrices.length === 0) {
                        confirmation.messageText("There are no price items against this flag. Do you want to Add New?");
                        confirmation.afterProceed(selectedProduct().setItemPriceMatrices);
                        confirmation.afterCancel(selectedProduct().removeExistingPriceMatrices);
                        confirmation.show();
                        return;
                    }

                    // Set Price Matrix to Item against selected Flag
                    selectedProduct().setItemPriceMatrices(itemPriceMatrices);
                },
                // Get Base Data
                getBaseData = function () {
                    dataservice.getBaseData({
                        success: function (data) {
                            costCentres.removeAll();
                            countries.removeAll();
                            states.removeAll();
                            if (data) {
                                mapCostCentres(data.CostCentres);

                                // Map Countries
                                mapCountries(data.Countries);

                                // Map States
                                mapStates(data.States);

                                // Map Section Flags
                                mapSectionFlags(data.SectionFlags);

                                // Assign countries & states to StateTaxConstructorParam
                                itemStateTaxConstructorParams.countries = countries();
                                itemStateTaxConstructorParams.states = states();
                            }
                        },
                        error: function (response) {
                            toastr.error("Failed to load base data" + response);
                        }
                    });
                },
                //// Save Product
                //saveProduct = function (callback) {
                //    dataservice.saveItem(selectedProduct().convertToServerData(), {
                //        success: function (data) {
                //            if (!selectedProduct().id()) {
                //                // Update Id
                //                selectedProduct().id(data.ItemId);

                //                // Add to top of list
                //                products.splice(0, 0, selectedProduct());
                //            } else {
                //                // Get Item
                //                var item = getItemFromList(selectedProduct().id());
                //                if (item) {
                //                    item.productCode(data.ProductCode);
                //                    item.productName(data.ProductName);
                //                    item.isEnabled(data.IsEnabled);
                //                    item.isPublished(data.IsPublished);
                //                }
                //            }

                //            toastr.success("Saved Successfully.");

                //            if (callback && typeof callback === "function") {
                //                callback();
                //            }
                //        },
                //        error: function (response) {
                //            toastr.error("Failed to Save Product. Error: " + response);
                //        }
                //    });
                //},
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
                // Get Item Price Matrices for Item By Flag
                getItemPriceMatricesForItemByFlag = function (flagId, itemId) {
                    dataservice.getItemPriceMatricesForItemByFlagId({
                        FlagId: flagId,
                        ItemId: itemId
                    }, {
                        success: function (data) {
                            setItemPriceMatricesToItem(data);
                        },
                        error: function (response) {
                            toastr.error("Failed to load item price matrix" + response);
                        }
                    });
                },
                // Get Stock Items
                getStockItems = function () {
                    dataservice.getStockItems({
                        SearchString: stockDialogFilter(),
                        PageSize: stockDialogPager().pageSize(),
                        PageNo: stockDialogPager().currentPage()
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
                },
                // Get Items
                getItems = function (companyId) {
                    isLoadingProducts(true);
                    dataservice.getCompanyProduct({
                        CompanyId: companyId,
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
                                selectedProduct(storeProductModel.Item.Create(data, itemActions, itemStateTaxConstructorParams));

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
                },

            //#endregion
            //#endregion
                //#region Maintaining Store Product List(new, added, delete) For Store One Click save
                selectProduct = function (data) {
                    if (selectedProduct() !== data) {
                        selectedProduct(data);
                    }
                },
                addNewAddedStoreProductId = function() {
                        newAddedStoreProductId = newAddedStoreProductId - 1;
                    },
                saveProduct = function () {
                    //#region New Product Saving
                    if (selectedProduct().id() == undefined || selectedProduct().id() == 0) {
                        selectedProduct().id(newAddedStoreProductId);
                        newAddedProducts.push(selectedProduct());
                        products.push(selectedProduct());
                        addNewAddedStoreProductId();
                        closeProductEditor();
                    }
                    //#endregion
                    //#region Old Product Saving
                    if (selectedProduct().id() > 0) {
                        var match = ko.utils.arrayFirst(edittedProducts(), function (item) {
                            return (selectedProduct().id() === item.id());
                        });
                        _.each(products(), function (item) {
                            if (item.id() == selectedProduct().id()) {
                                item.productName(selectedProduct().productName());
                            }
                        });
                        //if not found in editted products list then push new entry in it
                        if (!match) {
                            edittedProducts.push(selectedProduct());
                        }
                            //else match if match found, update item in editted list
                        else {
                            _.each(edittedProducts(), function (item) {
                                if (item.id() == selectedProduct().id()) {
                                    edittedProducts.remove(item);
                                    edittedProducts.push(selectedProduct());
                                }
                            });
                        }
                    }
                    //#endregion
                    //#region Update New Added Product
                    if (selectedProduct().id() < 0) {
                        _.each(newAddedProducts(), function (item) {
                            if (item.id() == selectedProduct().id()) {
                                newAddedProducts.remove(item);
                                newAddedProducts.push(selectedProduct());
                            }
                        });
                        closeProductEditor();
                    }
                    //#endregion
                },
                // Edit Product
                editProduct = function (data) {
                    if (selectedProduct() !== data) {
                        selectProduct(data);
                    }
                    if (selectedProduct().id() > 0 && !isProductAlreadyEditted()) {
                        getItemById(data.id(), openProductEditor);
                    } else {
                        openProductEditor();
                    }
                },
                isProductAlreadyEditted = function () {
                    var result = false;
                    _.each(edittedProducts(), function (product) {
                        if (product.id() == selectedProduct().id()) {
                            result= true;
                        }
                    });
                    return result;
                },
                //Reset Observables
                resetObservables = function() {
                        products.removeAll();
                        edittedProducts.removeAll();
                        newAddedProducts.removeAll();
                        deletedproducts.removeAll();
                    isProductDetailsVisible(false);
                },
                //#region Maintaining Byte Arrays Function
                //Product Category Thumbnail Files Loaded Callback
                    
                    storeProductThumbnailFileCallback = function (file, data) {
                        selectedProduct().storeProductThumbnailFileBinary(data);
                        selectedProduct().storeProductThumbnailFileName(file.name);
                    },
                    fileCallback = function (file, data) {
                        uploadMultipleFiles(file, data);
                    },
                    
                    storeProductPageBannerFileCallback = function (file, data) {
                        selectedProduct().storeProductPageBannerFileBinary(data);
                        selectedProduct().storeProductPageBannerFileName(file.name);
                    },
                    
                    storeProductGridImageLayoutFileCallback = function (file, data) {
                        selectedProduct().storeProductGridImageLayoutFileBinary(data);
                        selectedProduct().storeProductGridImageLayoutFileName(file.name);
                    },

                    uploadMultipleFiles = function (file, data) {
                        
                        if (selectedProduct().file1() == undefined) {
                            selectedProduct().file1(data);
                            selectedProduct().file1Name(file.name);
                            return;
                        }
                        if (selectedProduct().file2() == undefined) {
                            selectedProduct().file2(data);
                            selectedProduct().file2Name(file.name);
                            return;
                        }
                        if (selectedProduct().file3() == undefined) {
                            selectedProduct().file3(data);
                            selectedProduct().file3Name(file.name);
                            return;
                        }
                        if (selectedProduct().file4() == undefined) {
                            selectedProduct().file4(data);
                            selectedProduct().file4Name(file.name);
                            return;
                        }
                        if (selectedProduct().file5() == undefined) {
                            selectedProduct().file5(data);
                            selectedProduct().file5Name(file.name);
                            return;
                        }
                    },
                    removeAllFilesData= function() {
                        selectedProduct().file1(undefined);
                        selectedProduct().file2(undefined);
                        selectedProduct().file3(undefined);
                        selectedProduct().file4(undefined);
                        selectedProduct().file5(undefined);
                    },
                //#endregion
                //#endregion
                //#region Initialize

                initialize = function (companyId) {
                    pager(new pagination.Pagination({ PageSize: 5 }, products, getItems));
                    itemRelaterPager(new pagination.Pagination({ PageSize: 5 }, productsToRelate, getItemsToRelate)),
                    stockDialogPager(new pagination.Pagination({ PageSize: 5 }, stockItems, getStockItems)),
                    // Get Base Data
                    getBaseData();
                    // Get Items
                    getItems(companyId);
                };
                //#endregion 
                return {
                    //#region Observables
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
                    //pageHeader: pageHeader,
                    filterTextForRelatedItems: filterTextForRelatedItems,
                    itemRelaterPager: itemRelaterPager,
                    activeVideo: activeVideo,
                    productsToRelate: productsToRelate,
                    selectedJobDescription: selectedJobDescription,
                    itemFileTypes: itemFileTypes,
                    stockDialogPager: stockDialogPager,
                    stockDialogFilter: stockDialogFilter,
                    stockItems: stockItems,
                    costCentres: costCentres,
                    sectionFlags: sectionFlags,
                    //#endregion Observables
                    //#region ObservableArrays Of client side list for one click save of store
                    newAddedStoreProductId: newAddedStoreProductId,
                    newAddedProducts: newAddedProducts,
                    edittedProducts: edittedProducts,
                    deletedproducts: deletedproducts,
                    selectProduct: selectProduct,
                    resetObservables: resetObservables,
                    fileCallback: fileCallback,
                    storeProductThumbnailFileCallback: storeProductThumbnailFileCallback,
                    storeProductPageBannerFileCallback: storeProductPageBannerFileCallback ,
                    storeProductGridImageLayoutFileCallback: storeProductGridImageLayoutFileCallback,
                    removeAllFilesData: removeAllFilesData,
                    //#endregion
                    // #region Utility Methods
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
                    selectJobDescription: selectJobDescription,
                    searchStockItems: searchStockItems,
                    resetStockItems: resetStockItems,
                    openStockItemDialog: openStockItemDialog,
                    closeStockItemDialog: closeStockItemDialog,
                    openItemAddonCostCentreDialog: openItemAddonCostCentreDialog,
                    closeItemAddonCostCentreDialog: closeItemAddonCostCentreDialog
                    // #endregion Utility Methods
                };
            })()
        };
    })