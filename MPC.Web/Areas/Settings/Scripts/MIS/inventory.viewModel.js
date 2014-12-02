/*
    Module with the view model for the Inventory.
*/
define("inventory/inventory.viewModel",
    ["jquery", "amplify", "ko", "inventory/inventory.dataservice", "inventory/inventory.model", "common/confirmation.viewModel", "common/pagination"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination) {
        var ist = window.ist || {};
        ist.inventory = {
            viewModel: (function () {
                var
                    //View
                    view,
                    //Active Inventory
                    selectedInventory = ko.observable(),
                      //Is Loading Paper Sheet
                    isLoadingInventory = ko.observable(false),
                       //is Inventory Editor Visible
                    isInventoryEditorVisible = ko.observable(false),
                    //Sort On
                    sortOn = ko.observable(1),
                    //Sort In Ascending
                    sortIsAsc = ko.observable(true),
                    //Pager
                    pager = ko.observable(),
                    //Search Filter
                    searchFilter = ko.observable(),
                    //Category Filter
                    categoryFilter = ko.observable(),
                    //Sub category filter
                    subCategoryFilter = ko.observable(),
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
                    //Registration Questions
                    registrationQuestions = ko.observableArray([]),
                     //units
                    units = ko.observableArray([{ Id: 1, Text: 'Sheets' },
                                                { Id: 2, Text: '100 (lbs)' },
                                                { Id: 3, Text: 'Ton' },
                                                { Id: 4, Text: 'Sq Meter' },
                                                { Id: 5, Text: 'Sq Feet' },
                                                { Id: 6, Text: 'Items' }
                    ]),
                    //Filtered Units
                    filteredUnits = ko.observableArray([]),
                    //Default Cost price list items
                    defaultCostPriceList = ko.observableArray([]),
                    //Cost List
                    costList = ko.observableArray([]),
                    //PriceList
                    priceList = ko.observableArray([]),
                    //Filtered Sub Categories
                    filteredSubCategories = ko.observableArray([]),
                    // #endregion Arrays

                    // #region Utility Functions
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
                     // Delete Inventory
                    deleteInventory = function (inventory) {
                        dataservice.deleteInventory(inventory.convertToServerData(), {
                            success: function () {
                                inventories.remove(inventory);
                                toastr.success("Stock Item Successfully remove.");
                            },
                            error: function () {
                                toastr.error("Failed to remove stock item.");
                            }
                        });
                    },
                    //Edit Inventory
                    onEditInventory = function (inventory) {

                    },
                    //Get Inventories
                    getInventories = function () {
                        isLoadingInventory(true);
                        dataservice.getInventories({
                            SearchString: searchFilter(),
                            CategoryId: categoryFilter(),
                            SubCategoryId: subCategoryFilter(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            SortBy: sortOn(),
                            IsAsc: sortIsAsc()
                        }, {
                            success: function (data) {
                                pager().totalCount(data.TotalCount);
                                inventories.removeAll();
                                var inventoryList = [];
                                _.each(data.StockItems, function (item) {
                                    var inventory = new model.InventoryListView.Create(item);;
                                    inventoryList.push(inventory);

                                });
                                ko.utils.arrayPushAll(inventories(), inventoryList);
                                inventories.valueHasMutated();
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
                                //Registration Questions
                                registrationQuestions.removeAll();
                                ko.utils.arrayPushAll(registrationQuestions(), data.RegistrationQuestions);
                                registrationQuestions.valueHasMutated();

                                var cost = model.StockCostAndPrice.CreateForClient(data.StockCostAndPrice);

                                var price = model.StockCostAndPrice.CreateForClient(data.StockCostAndPrice);

                            },
                            error: function () {
                                toastr.error("Failed to base data.");
                            }
                        });
                    },
                    //
                    unitFirtration = ko.computed(function () {
                        if (selectedInventory() !== undefined) {
                            filteredUnits.removeAll();
                            ko.utils.arrayPushAll(filteredUnits(), units());
                            filteredUnits.valueHasMutated();
                            if (selectedInventory().itemId !== undefined) {
                                //If Selected Sheet
                                if (selectedInventory().itemId() === 0 && selectedInventory().paperTypeId() === "1") {
                                    _.each(filteredUnits(), function (item) {
                                        if (item.Id === 4 || item.Id === 5 || item.Id === 6) {
                                            filteredUnits.remove(item);
                                        }
                                    });
                                }
                                //If Selected Roll Paper
                                if (selectedInventory().itemId() === 0 && selectedInventory().paperTypeId() === "2") {
                                    _.each(filteredUnits(), function (item) {
                                        if (item.Id === 1 || item.Id === 6) {
                                            filteredUnits.remove(item);
                                        }
                                    });
                                }
                            }
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
                    onSaveInventory = function (inventory) {
                        if (doBeforeSave()) {
                            saveInventory(inventory);
                        }
                    },
                     // Do Before Logic
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedInventory().isValid()) {
                            selectedInventory().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                     // Save Inventory
                    saveInventory = function (inventory) {
                        dataservice.saveInventory(selectedInventory().convertToServerData(inventory), {
                            success: function (data) {
                                toastr.success("Successfully save.");
                            },
                            error: function (exceptionMessage, exceptionType) {

                                if (exceptionType === ist.exceptionType.CaresGeneralException) {

                                    toastr.error(exceptionMessage);

                                } else {

                                    toastr.error("Failed to save.");

                                }

                            }
                        });
                    },
                //Get Inventories
                createInventory = function () {
                    selectedInventory(model.StockItem.Create());
                    showInventoryEditor();
                },
                // close Inventory Editor
                 closeInventoryEditor = function () {
                     isInventoryEditorVisible(false);
                 },
                // Show Inventory Editor
                showInventoryEditor = function () {
                    isInventoryEditorVisible(true);
                },
                //Initialize
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    getBase();
                    pager(pagination.Pagination({ PageSize: 5 }, inventories, getInventories));
                    getInventories();
                };
                // #endregion Arrays

                return {
                    searchFilter: searchFilter,
                    isInventoryEditorVisible: isInventoryEditorVisible,
                    selectedInventory: selectedInventory,
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
                    registrationQuestions: registrationQuestions,
                    //Utility Functiions
                    initialize: initialize,
                    getInventories: getInventories,
                    onDeleteInventory: onDeleteInventory,
                    closeInventoryEditor: closeInventoryEditor,
                    showInventoryEditor: showInventoryEditor,
                    createInventory: createInventory,
                    onEditInventory: onEditInventory,
                    onSaveInventory: onSaveInventory,
                };
            })()
        };
        return ist.inventory.viewModel;
    });
