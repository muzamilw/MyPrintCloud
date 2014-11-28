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
                    //Filtered Sub Categories
                    filteredSubCategories = ko.observableArray([]),
                    // #endregion Arrays

                    // #region Utility Functions
                      // Delete a Inventory
                    onDeleteInventory = function (inventory) {
                        //if (!inventory.id()) {
                        //    inventories.remove(inventory);
                        //    return;
                        //}
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            inventories.remove(inventory);
                        });
                        confirmation.show();
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
                            },
                            error: function () {
                                toastr.error("Failed to base data.");
                            }
                        });
                    },
                   //Get Inventories
                   createInventory = function () {
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
                    initialize: initialize,
                    getInventories: getInventories,
                    onDeleteInventory: onDeleteInventory,
                    closeInventoryEditor: closeInventoryEditor,
                    showInventoryEditor: showInventoryEditor,
                    createInventory: createInventory,
                    onEditInventory: onEditInventory,
                };
            })()
        };
        return ist.inventory.viewModel;
    });
