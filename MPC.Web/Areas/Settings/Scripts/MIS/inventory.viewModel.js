/*
    Module with the view model for the Inventory.
*/
define("inventory/inventory.viewModel",
    ["jquery", "amplify", "ko", "inventory/inventory.dataservice", "inventory/inventory.model", "common/confirmation.viewModel", "common/pagination"
        , "common/supplier.model", "common/supplier.viewModel", "common/sharedNavigation.viewModel", "common/reportManager.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, supplierModel, supplierVm, sharedNavigationVM, reportManager) {
        var ist = window.ist || {};
        ist.inventory = {
            viewModel: (function () {
                var
                    //View
                    view,
                    //Active Inventory
                    selectedInventory = ko.observable(model.StockItem()),
                    //slected list view selected item copy
                    selectedInventoryCopy = ko.observable(),
                    //Active Cost Item
                    selectedCostItem = ko.observable(),
                    //Active price Item
                    selectedPriceItem = ko.observable(),
                    //Is Loading Paper Sheet
                    isLoadingInventory = ko.observable(false),
                    ///Weight Unit
                    weightUnit = ko.observable(),
                    //is Inventory Editor Visible
                    isInventoryEditorVisible = ko.observable(false),
                    //Currency Symbol
                    currencySymbol = ko.observable(),
                    //Sort On
                    sortOn = ko.observable(1),
                    //Sort In Ascending
                    sortIsAsc = ko.observable(true),
                    //Pager
                    pager = ko.observable(),
                    //Search Filter
                    searchFilter = ko.observable(),
                    //Organisation culture region
                    orgRegion = ko.observable(),
                    //Supplier Filter
                    supplierFilter = ko.observable(),
                    //Category Filter
                    categoryFilter = ko.observable(),
                    //Sub category filter
                    subCategoryFilter = ko.observable(),
                    // bind Label
                    weightLabel = ko.observable(),
                    // #region Arrays
                    //Paper Sheets
                    inventories = ko.observableArray([]),
                    //Stock Categories
                    categories = ko.observableArray([]),
                    //Sub Categories
                    subCategories = ko.observableArray([]),
                    //Paper sizes 
                    paperSizes = ko.observableArray([]),
                    //section Flags
                    sectionFlags = ko.observableArray([]),
                    //weigh tUnits
                    weightUnits = ko.observableArray([]),
                    //Length Units
                    lengthUnits = ko.observableArray([]),
                    //Paper Basis Areas
                    paperBasisAreas = ko.observableArray([]),
                    //units
                    units = ko.observableArray([
                        { Id: 4, Text: 'Sq Meter' },
                        { Id: 1, Text: 'Sheets' },
                        { Id: 2, Text: '100 (lbs)' },
                        { Id: 3, Text: 'Ton' },
                        { Id: 5, Text: 'Sq Feet' },
                        { Id: 6, Text: 'Items' }
                    ]),
                    //Status List
                    statusList = ko.observableArray([
                        { Id: 1, Text: 'Availability' },
                        { Id: 2, Text: 'Discontinued' }
                    ]),
                    //Filtered Units
                    filteredUnits = ko.observableArray([]),
                    //Cost price list items
                    costPriceList = ko.observableArray([]),
                    //Cost List
                    costList = ko.observableArray([]),
                    //PriceList
                    priceList = ko.observableArray([]),
                    //Filtered Sub Categories
                   // filteredSubCategories = ko.observableArray([]),
                    //Error List
                    errorList = ko.observableArray([]),
                    // #endregion Arrays

                    // #region Utility Functions
                    // Inventory Has Changes
                    inventoryHasChanges = new ko.dirtyFlag({
                        costPriceList: costPriceList,

                    }),
                     openReport = function (isFromEditor) {
                         reportManager.show(ist.reportCategoryEnums.Inventory, isFromEditor == true ? true : false, 0);
                     },
                    // Has Changes
                    hasChangesOnInventory = ko.computed(function () {
                        return ((selectedInventory() && selectedInventory().hasChanges()) || inventoryHasChanges.isDirty());
                    }),
                    // Delete a Inventory
                    onDeleteInventory = function (inventory) {
                        if (!inventory.itemId()) {
                            inventories.remove(inventory);
                            return;
                        }
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            deleteInventory(inventory);
                        });
                        confirmation.show();
                    },
                    //Delete Cost Item
                    onDeleteCostItem = function (costItem) {
                        costPriceList.remove(costItem);
                    },
                    //Delete Price Item
                    onDeletePriceItem = function (priceItem) {
                        costPriceList.remove(priceItem);
                    },
                    // Delete Inventory
                    deleteInventory = function (inventory) {
                        dataservice.deleteInventory(inventory.convertToServerData(), {
                            success: function () {
                                inventories.remove(inventory);
                                toastr.success("Stock item successfully removed.");
                            },
                            error: function () {
                                toastr.error("Failed to remove stock item.");
                            }
                        });
                    },
                    //Edit Inventory
                    onEditInventory = function (inventory) {
                        errorList.removeAll();
                        selectedInventoryCopy(inventory);
                        dataservice.getInventoryById({
                            id: inventory.itemId(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    selectedInventory(model.StockItem.CreateForClient(data));
                                    costPriceList.removeAll();
                                    _.each(data.StockCostAndPrices, function (item) {
                                        costPriceList.push(model.StockCostAndPrice.CreateForClient(item));
                                    });

                                    if (selectedInventory().paperTypeId() === 1) {
                                        selectedInventory().paperType("Sheet");
                                    }
                                    if (selectedInventory().paperTypeId() === 2) {
                                        selectedInventory().paperType("Roll Paper");
                                    }

                                    if (selectedInventory().IsImperical() == true)
                                    {
                                        weightLabel("lbs");
                                    }
                                    else {
                                        weightLabel("kg");
                                    }
                                    selectedInventory().reset();

                                    inventoryHasChanges.reset();
                                    showInventoryEditor();
                                    sharedNavigationVM.initialize(selectedInventory, function (saveCallback) { onSaveInventory(saveCallback); });
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load Detail . Error: ");
                            }
                        });
                    },
                    //Get Inventories
                    getInventoriesListItems = function () {
                        isLoadingInventory(true);
                        dataservice.getInventoriesList({
                            Region: orgRegion(),
                            SearchString: searchFilter(),
                            CategoryId: categoryFilter(),
                            SubCategoryId: subCategoryFilter(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            SortBy: sortOn(),
                            IsAsc: sortIsAsc()
                        }, {
                            success: function (data) {
                                if (data) {
                                    inventories.removeAll();
                                    _.each(data.StockItems, function (item) {
                                        inventories.push(new model.InventoryListView.Create(item));
                                    });
                                    pager().totalCount(data.TotalCount);
                                }
                                isLoadingInventory(false);

                            },
                            error: function () {
                                isLoadingInventory(false);
                                toastr.error("Failed to load inventories.");
                            }
                        });
                    },

                    // Get Base
                    getBase = function () {
                        dataservice.getInventoryBase({
                            success: function (data) {
                                // Orgnaisation region
                                orgRegion(data.Region);
                                getInventoriesListItems();
                                //Categories
                                categories.removeAll();
                                ko.utils.arrayPushAll(categories(), data.StockCategories);
                                categories.valueHasMutated();
                                //Stock Sub Categories
                                subCategories.removeAll();
                                ko.utils.arrayPushAll(subCategories(), data.StockSubCategories);
                                subCategories.valueHasMutated();
                                //Paper Sizes
                                paperSizes.removeAll();
                                ko.utils.arrayPushAll(paperSizes(), data.PaperSizes);
                                paperSizes.valueHasMutated();
                                //section Flags
                                sectionFlags.removeAll();
                                ko.utils.arrayPushAll(sectionFlags(), data.SectionFlags);
                                sectionFlags.valueHasMutated();
                                //Weight Units
                                weightUnits.removeAll();
                                ko.utils.arrayPushAll(weightUnits(), data.WeightUnits);
                                weightUnits.valueHasMutated();
                                //Length Units
                                lengthUnits.removeAll();
                                ko.utils.arrayPushAll(lengthUnits(), data.LengthUnits);
                                lengthUnits.valueHasMutated();
                                //Paper Basis Areas
                                paperBasisAreas.removeAll();
                                ko.utils.arrayPushAll(paperBasisAreas(), data.PaperBasisAreas);
                                paperBasisAreas.valueHasMutated();

                                currencySymbol(data.CurrencySymbol);
                                weightUnit(data.WeightUnit);

                                if(data.IsImperical == true)
                                {
                                    weightLabel("lbs");
                                }
                                else
                                {
                                    weightLabel("kg");
                                }
                            },
                            error: function () {
                                toastr.error("Failed to base data.");
                            }
                        });
                    },
                    //Like Sheets,sq Meter dropdown filtration
                    unitFirtration = ko.computed(function () {
                        if (selectedInventory() !== undefined) {
                            filteredUnits.removeAll();
                            ko.utils.arrayPushAll(filteredUnits(), units());
                            filteredUnits.valueHasMutated();
                            if (selectedInventory().itemId !== undefined && selectedInventory().paperTypeId !== undefined) {
                                //If Selected Sheet
                                if (selectedInventory().paperTypeId().toString() === "1") {
                                    _.each(filteredUnits(), function (item) {
                                        if (item.Id === 4 || item.Id === 5 || item.Id === 6) {
                                            filteredUnits.remove(item);
                                        }
                                    });
                                }
                                //If Selected Roll Paper
                                if (selectedInventory().paperTypeId().toString() === "2") {
                                    _.each(filteredUnits(), function (item) {
                                        if (item.Id === 1 || item.Id === 6) {
                                            filteredUnits.remove(item);
                                        }
                                    });
                                }
                            }
                        }
                    }, this),

                    //Sub category filteration based on Category
                    filteredSubCategories = ko.computed(function () {
                        if (subCategories().length === 0) {
                            return [];
                        }
                        return subCategories.filter(function (subCategory) {
                            return subCategory.CategoryId === categoryFilter();
                        });
                    }),
                    // Filtered based on category Id
                    filteredSubCategoriesForDetail = ko.computed(function () {
                        if (subCategories().length === 0) {
                            return [];
                        }
                        return subCategories.filter(function (subCategory) {
                            return subCategory.CategoryId === selectedInventory().categoryId();
                        });
                    });
                //On select Supplier
                selectedSupplier = ko.computed(function () {
                    if (supplierVm.selectedSupplier() !== undefined) {
                        selectedInventory().supplierId(supplierVm.selectedSupplier().supplierId());
                        selectedInventory().supplierName(supplierVm.selectedSupplier().name());
                    }
                }, this),
                //Calculate pack cost
                packCost = ko.computed(function () {
                    if (selectedInventory() !== undefined && costPriceList().length > 0) {
                        _.each(costPriceList(), function (item) {
                            //selectedInventory().perQtyQty();
                            //item.costPrice();
                            //selectedInventory().packageQty();
                            item.packCostPrice(((item.costPrice() === undefined ? 0 : item.costPrice()) / (selectedInventory().perQtyQty() === undefined ? 1 : selectedInventory().perQtyQty())) * selectedInventory().packageQty());
                        });

                    }
                }, this),
                //Compute value for table header
                headerComputedValue = ko.computed(function () {
                    if (selectedInventory() !== undefined && selectedInventory().perQtyType !== undefined) {
                        if (selectedInventory().perQtyType() === 1) {
                            selectedInventory().headerComputedValue(selectedInventory().perQtyQty() + " Sheets");
                        }
                        if (selectedInventory().perQtyType() === 2) {
                            selectedInventory().headerComputedValue("100 (lbs)");
                        }
                        if (selectedInventory().perQtyType() === 3) {
                            selectedInventory().headerComputedValue("Ton");
                        }
                        if (selectedInventory().perQtyType() === 4) {
                            selectedInventory().headerComputedValue("Sq Meter)");
                        }
                        if (selectedInventory().perQtyType() === 5) {
                            selectedInventory().headerComputedValue("Sq Feet");
                        }
                        if (selectedInventory().perQtyType() === 6) {
                            selectedInventory().headerComputedValue("Items)");
                        }
                    }
                }, this),
                //Call function for Save Invetry
                onSaveInventory = function (callback) {
                    errorList.removeAll();
                    saveInventory(callback);
                },
                // Do Before Logic
                doBeforeSave = function () {
                    var flag = true;
                    if (!selectedInventory().isValid()) {
                        selectedInventory().errors.showAllMessages();
                        if (selectedInventory().itemName.error != null) {
                            errorList.push({ name: "Stock Name", element: selectedInventory().itemName.domElement });
                        }
                        if (selectedInventory().supplierId.error != null) {
                            errorList.push({ name: "Supplier Name", element: selectedInventory().supplierName.domElement });
                        }

                        if (selectedInventory().perQtyQty.error != null) {
                            errorList.push({ name: "Per Quantity", element: selectedInventory().perQtyQty.domElement });
                        }
                        if (selectedInventory().itemSizeHeight.error != null) {
                            errorList.push({ name: "Height", element: selectedInventory().itemSizeHeight.domElement });
                        }
                        if (selectedInventory().itemSizeWidth.error != null) {
                            errorList.push({ name: "Width", element: selectedInventory().itemSizeWidth.domElement });
                        }
                        if (selectedInventory().packageQty.error != null) {
                            errorList.push({ name: "Package In", element: selectedInventory().packageQty.domElement });
                        }
                        if (selectedInventory().rollWidth.error != null) {
                            errorList.push({ name: "Roll Width", element: selectedInventory().rollWidth.domElement });
                        }
                        if (selectedInventory().rollLength.error != null) {
                            errorList.push({ name: "Roll Length", element: selectedInventory().rollLength.domElement });
                        }
                        if (selectedInventory().itemWeight.error != null) {
                            errorList.push({ name: "Weight", element: selectedInventory().itemWeight.domElement });
                        }
                        if (selectedInventory().inkAbsorption.error != null) {
                            errorList.push({ name: "Ink Absorption", element: selectedInventory().inkAbsorption.domElement });
                        }
                        if (selectedInventory().reOrderLevel.error != null) {
                            errorList.push({ name: "Reorder Level", element: selectedInventory().reOrderLevel.domElement });
                        }
                        if (selectedInventory().reorderQty.error != null) {
                            errorList.push({ name: "Reorder Quantity", element: selectedInventory().reorderQty.domElement });
                        }
                        flag = false;
                    }
                    return flag;
                },
                // Go To Element
              gotoElement = function (validation) {
                  view.gotoElement(validation.element);
              },
                // Do Before Logic
                doBeforeCostAndPrice = function () {
                    var flag = true;
                    //_.each(costPriceList(), function (costPrice, index) {
                    //    if (!costPrice.isValid() && !isInvalidPeriod()) {
                    //        costPrice.errors.showAllMessages();
                    //        if (flag) {
                    //            errorList.push({ indexId: index, tabId: 1, name: "Cost Or Price Missing Fields" });
                    //        }
                    //        flag = false;
                    //    }
                    //});

                    // Show Cost Price Item Errors
                    var itemcostPriceInvalid = costPriceList.find(function (itemcostPrice) {
                        return !itemcostPrice.isValid();
                    });
                    if (itemcostPriceInvalid) {
                        if (itemcostPriceInvalid.costPrice.error) {
                            errorList.push({ name: "Cost/Price", element: itemcostPriceInvalid.costPrice.domElement });
                            flag = false;
                        }
                        if (itemcostPriceInvalid.fromDate.error) {
                            errorList.push({ name: "From Date", element: itemcostPriceInvalid.rate.domElement });
                            flag = false;
                        }
                        if (itemcostPriceInvalid.toDate.error) {
                            errorList.push({ name: "To Date", element: itemcostPriceInvalid.toDate.domElement });
                            flag = false;
                        }
                    }
                    return flag;
                },
                // Save Inventory
                saveInventory = function (callback) {
                    if (doBeforeSave() && doBeforeCostAndPrice()) {
                        if (selectedInventory().stockCostAndPriceListInInventory().length > 0) {
                            selectedInventory().stockCostAndPriceListInInventory.removeAll();
                        }
                        _.each(costPriceList(), function (item) {
                            selectedInventory().stockCostAndPriceListInInventory.push(item.convertToServerData());
                        });
                        supplierVm.selectedSupplier(undefined);


                        dataservice.saveInventory(selectedInventory().convertToServerData(orgRegion()), {
                            success: function (data) {
                                //For Add New
                                if (selectedInventory().itemId() === 0) {
                                    var inventoryResponse = new model.InventoryListView.Create(data);
                                    inventories.splice(0, 0, inventoryResponse);
                                } else {
                                    selectedInventoryCopy().name(data.ItemName);
                                    selectedInventoryCopy().weight(data.ItemWeight);
                                    selectedInventoryCopy().perQtyQty(data.PerQtyQty);
                                    selectedInventoryCopy().colour(data.FlagColor);
                                    selectedInventoryCopy().categoryName(data.CategoryName);
                                    selectedInventoryCopy().subCategoryName(data.SubCategoryName);
                                    selectedInventoryCopy().weightUnitName(data.WeightUnitName);
                                    selectedInventoryCopy().fullCategoryName(data.FullCategoryName);
                                    selectedInventoryCopy().supplierCompanyName(data.SupplierCompanyName);
                                    selectedInventoryCopy().packCostPrice(data.PackCostPrice || '');
                                }
                                isInventoryEditorVisible(false);
                                sharedNavigationVM.reset();
                                if (callback && typeof callback === "function") {
                                    callback();
                                }
                                toastr.success("Saved Successfully.");
                            },
                            error: function (exceptionMessage, exceptionType) {

                                if (exceptionType === ist.exceptionType.MPCGeneralException) {

                                    toastr.error(exceptionMessage);

                                } else {

                                    toastr.error("Failed to save.");

                                }

                            }
                        });
                    }
                },
                //Create New Inventory Item
                createInventory = function () {
                    supplierVm.selectedSupplier(undefined);
                    costPriceList.removeAll();
                    errorList.removeAll();
                    selectedInventory(model.StockItem.Create());
                    //Add default cost and price rows
                    var cost = model.StockCostAndPrice.Create();
                    selectCostItem(cost);
                    costPriceList.splice(0, 0, cost);
                    var price = model.StockCostAndPrice.Create();
                    price.costOrPriceIdentifier(-1);
                    selectedPriceItem(price);
                    costPriceList.splice(0, 0, price);
                    // selectedInventory().reset();
                    showInventoryEditor();
                    sharedNavigationVM.initialize(selectedInventory, function (saveCallback) { onSaveInventory(saveCallback); });
                },
                // close Inventory Editor
                closeInventoryEditor = function () {
                    if (selectedInventory().hasChanges()) {
                        confirmation.messageText("Do you want to save changes?");
                        confirmation.afterProceed(onSaveInventory);
                        confirmation.afterCancel(function () {
                            isInventoryEditorVisible(false);
                            selectedInventory().reset();
                            sharedNavigationVM.reset();
                        });
                        confirmation.show();
                        return;
                    }
                    isInventoryEditorVisible(false);
                },
                // Show Inventory Editor
                showInventoryEditor = function () {
                    isInventoryEditorVisible(true);
                    view.initializeLabelPopovers();
                },
                // Template Chooser
                templateToUse = function (costItem) {
                    return (costItem === selectedCostItem() ? 'editCostTemplate' : 'itemCostTemplate');
                },
                // Select a Cost Item
                selectCostItem = function (costItem) {
                    if (selectedCostItem() !== undefined && !selectedCostItem().isValid()) {
                        return;
                    }
                    if (selectedCostItem() !== costItem) {
                        selectedCostItem(costItem);
                    }
                },
                // Template Chooser For Price
                templateToUseForPrice = function (priceItem) {
                    return (priceItem === selectedPriceItem() ? 'editPriceTemplate' : 'itemPriceTemplate');
                },
                // Select a Price Item
                selectPriceItem = function (priceItem) {
                    if (selectedPriceItem() != undefined && !selectedPriceItem().isValid()) {
                        return;
                    }
                    if (selectedPriceItem() !== priceItem) {
                        selectedPriceItem(priceItem);
                    }
                },
                //Create New Cost Item
                createCostItem = function () {

                    if (selectedCostItem() !== undefined && !selectedCostItem().isValid()) {
                        return;
                    }
                    var costItem;
                    if (costPriceList().length > 0 && costPriceList()[0].costOrPriceIdentifier() === 0) {
                        costItem = costPriceList()[0];
                    }
                    else {
                        if (costPriceList().length > 1 && costPriceList()[1].costOrPriceIdentifier() !== 0);
                        {
                            costItem = costPriceList()[1];
                        }
                    }
                    var flag = true;
                    if (costItem !== undefined && costItem !== null && !costItem.isValid()) {
                        costItem.errors.showAllMessages();
                        selectedCostItem(costItem);
                        flag = false;
                    }
                    if (flag) {
                        var cost = model.StockCostAndPrice.Create();
                        selectedCostItem(cost);
                        costPriceList.splice(0, 0, cost);
                    }
                },
                //Create New Price Item
                createPriceItem = function () {
                    if (selectedPriceItem() !== undefined && !selectedPriceItem().isValid()) {
                        return;
                    }

                    var priceItem;
                    if (costPriceList().length > 0 && costPriceList()[0].costOrPriceIdentifier() === -1) {
                        priceItem = costPriceList()[0];
                    }
                    else {
                        if (costPriceList().length > 1 && costPriceList()[1].costOrPriceIdentifier() !== -1);
                        {
                            priceItem = costPriceList()[1];
                        }
                    }
                    var flag = true;
                    if (priceItem !== undefined && priceItem !== null && !priceItem.isValid()) {
                        priceItem.errors.showAllMessages();
                        selectedPriceItem(priceItem);
                        flag = false;
                    }
                    if (flag) {
                        var price = model.StockCostAndPrice.Create();
                        price.costOrPriceIdentifier(-1);
                        selectedPriceItem(price);
                        costPriceList.splice(0, 0, price);
                    }
                },
                //Select Tab Click or error link
                selectTab = function (property) {
                    if (property.tabId === 1) {
                        $('#myTab a[href="#tab-CostPrice"]').tab('show');

                        var costPriceItem = costPriceList()[property.indexId];
                        if (costPriceItem.costOrPriceIdentifier() === 0) {
                            selectedCostItem(costPriceItem);
                        } else {
                            selectedPriceItem(costPriceItem);
                        }


                        //$("#" + property.fieldId).focus();
                    }
                    if (property.tabId === 2) {
                        $('#myTab a[href="#tab-Properties"]').tab('show');

                    }
                    if (property.tabId === 3) {
                        $('#myTab a[href="#tab-StockControl"]').tab('show');
                        // var index = property.fieldId.split(',')[1];
                        //selectedChartOfAccounts(chartOfAccounts()[index]);
                    }

                },
                // Filter Inventories
                filterInventories = function () {
                    pager().reset();
                    // Get Inventories
                    getInventoriesListItems();
                },
                // Reset Filter
                resetFilter = function () {
                    // Reset Text 
                    searchFilter(undefined);
                    categoryFilter(undefined);
                    subCategoryFilter(undefined);
                    // Filter Record
                    getInventoriesListItems();
                },

                onAddSupplier = function () {
                    supplierVm.supplierPager().reset();
                    supplierVm.getSuppliers();
                    supplierVm.show();
                },
                onArchiveStock = function() {
                    // Ask for confirmation
                    confirmation.afterProceed(function () {
                        var inventory = selectedInventory();
                        inventory.isDisabled(true);
                        _.each(inventories(), function(itm) {
                            if (itm.itemId() == inventory.itemId()) {
                                inventories.remove(itm);
                               pager().totalCount(inventories().length);
                            }
                                
                        });
                        
                        saveInventory();
                    });
                    confirmation.show();
                },
                //Initialize
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    getBase();
                    pager(pagination.Pagination({ PageSize: 10 }, inventories, getInventoriesListItems));
                    categoryFilter.subscribe(function () {
                        filterInventories();
                    });
                    subCategoryFilter.subscribe(function () {
                        filterInventories();
                    });
                };
                // #endregion Arrays

                return {
                    searchFilter: searchFilter,
                    categoryFilter: categoryFilter,
                    subCategoryFilter: subCategoryFilter,
                    supplierFilter: supplierFilter,
                    isInventoryEditorVisible: isInventoryEditorVisible,
                    selectedInventory: selectedInventory,
                    selectedInventoryCopy: selectedInventoryCopy,
                    selectedCostItem: selectedCostItem,
                    selectedPriceItem: selectedPriceItem,
                    isLoadingInventory: isLoadingInventory,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    //Arrays
                    inventories: inventories,
                    filteredSubCategories: filteredSubCategories,
                    categories: categories,
                    subCategories: subCategories,
                    paperSizes: paperSizes,
                    sectionFlags: sectionFlags,
                    weightUnits: weightUnits,
                    costList: costList,
                    priceList: priceList,
                    units: units,
                    filteredUnits: filteredUnits,
                    paperBasisAreas: paperBasisAreas,
                    lengthUnits: lengthUnits,
                    costPriceList: costPriceList,
                    statusList: statusList,
                    errorList: errorList,
                    //Utility Functiions
                    initialize: initialize,
                    getInventoriesListItems: getInventoriesListItems,
                    onDeleteInventory: onDeleteInventory,
                    closeInventoryEditor: closeInventoryEditor,
                    showInventoryEditor: showInventoryEditor,
                    createInventory: createInventory,
                    onEditInventory: onEditInventory,
                    onSaveInventory: onSaveInventory,
                    templateToUse: templateToUse,
                    selectCostItem: selectCostItem,
                    onDeleteCostItem: onDeleteCostItem,
                    templateToUseForPrice: templateToUseForPrice,
                    selectPriceItem: selectPriceItem,
                    onDeletePriceItem: onDeletePriceItem,
                    createCostItem: createCostItem,
                    createPriceItem: createPriceItem,
                    selectTab: selectTab,
                    filterInventories: filterInventories,
                    resetFilter: resetFilter,
                    onAddSupplier: onAddSupplier,
                    gotoElement: gotoElement,
                    currencySymbol: currencySymbol,
                    orgRegion: orgRegion,
                    hasChangesOnInventory: hasChangesOnInventory,
                    weightUnit: weightUnit,
                    weightLabel: weightLabel,
                    filteredSubCategoriesForDetail: filteredSubCategoriesForDetail,
                    openReport: openReport,
                    onArchiveStock: onArchiveStock
                };
            })()
        };
        return ist.inventory.viewModel;
    });
