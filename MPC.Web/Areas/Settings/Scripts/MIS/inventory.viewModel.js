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
                    //Paper Sheets
                    inventories = ko.observableArray([]),
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
                    //Get Inventories
                  getInventories = function () {

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
                    //pager(pagination.Pagination({ PageSize: 5 }, inventories, getInventories));
                    //getInventories();
                };

                return {
                    searchFilter: searchFilter,
                    isInventoryEditorVisible: isInventoryEditorVisible,
                    inventories: inventories,
                    selectedInventory: selectedInventory,
                    isLoadingInventory: isLoadingInventory,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    initialize: initialize,
                    getInventories: getInventories,
                    onDeleteInventory: onDeleteInventory,
                    closeInventoryEditor: closeInventoryEditor,
                    showInventoryEditor: showInventoryEditor,
                    createInventory: createInventory,
                };
            })()
        };
        return ist.inventory.viewModel;
    });
