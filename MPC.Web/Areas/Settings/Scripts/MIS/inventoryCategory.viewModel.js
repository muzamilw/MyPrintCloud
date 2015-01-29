/*
    Module with the view model for the My Organization.
*/
define("inventoryCategory/inventoryCategory.viewModel",
    ["jquery", "amplify", "ko", "inventoryCategory/inventoryCategory.dataservice", "inventoryCategory/inventoryCategory.model", "common/confirmation.viewModel", "common/pagination"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination) {
        var ist = window.ist || {};
        ist.inventoryCategory = {
            viewModel: (function () {
                var
                    //View
                    view,
                    //stock Categories List
                    stockCategories = ko.observableArray([]),
                    //Is Loading stock Categories
                    isLoadingStockCategories = ko.observable(false),
                    //Sort On
                    sortOn = ko.observable(1),
                    //Sort In Ascending
                    sortIsAsc = ko.observable(true),
                    //Pager
                    pager = ko.observable(),
                    //Search Filter
                    searchFilter = ko.observable(),
                    // Editor View Model
                    editorViewModel = new ist.ViewModel(model.InventoryCategory),
                    //Selected Paper Sheet
                    selectedStockCategory = editorViewModel.itemForEditing,
                    //Selected Stock Sub Category
                    selectedStockSubCategory = ko.observable(),
                    //Template To Use
                    templateToUse = function (stockCategory) {
                        return (stockCategory === selectedStockCategory() ? 'editStockCategoryTemplate' : 'itemStockCategoryTemplate');
                    },
                    //Make Edittable
                    makeEditable = ko.observable(false),
                    //Create New Stock Category
                    createNewStockCategory = function () {
                        var stockCategory = new model.InventoryCategory();
                        stockCategory.itemSizeCustom(false);
                        editorViewModel.selectItem(stockCategory);
                        selectedStockCategory(stockCategory);
                        isStockCategoryEditorVisible(true);
                    },
                    //On Edit Click Of Stock Category
                    onEditItem = function (item) {
                        editorViewModel.selectItem(item);
                        openEditDialog();
                    },
                    //To Show/Hide Edit Section
                    isStockCategoryEditorVisible = ko.observable(false),
                    //Delete Stock Category
                    deleteStockCategory = function (stockCategory) {
                        dataservice.deleteStockCategory({
                            StockCategoryId: stockCategory.categoryId(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    stockCategories.remove(stockCategory);
                                    toastr.success(" Deleted Successfully !");
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Delete . Error: " + response);
                            }
                        });
                    },
                    //GET Stock Categories
                    getStockCategories = function () {
                        isLoadingStockCategories(true);
                        dataservice.getStockCategories({
                            StockCategoryFilterText: searchFilter(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            SortBy: sortOn(),
                            IsAsc: sortIsAsc()
                        }, {
                            success: function (data) {
                                stockCategories.removeAll();
                                if (data != null) {
                                    pager().totalCount(data.RowCount);
                                    _.each(data.StockCategories, function (item) {
                                        var module = model.InventoryCategory.Create(item);
                                        stockCategories.push(module);
                                    });
                                }
                                isLoadingStockCategories(false);
                            },
                            error: function (response) {
                                isLoadingStockCategories(false);
                                toastr.error("Error: Failed To load Stock Categories" + response);
                            }
                        });
                    },
                    //Do Before Save
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedStockCategory().isValid()) {
                            selectedStockCategory().errors.showAllMessages();
                            flag = false;
                        }
                        if (selectedStockCategory().stockSubCategories().length > 0) {
                            _.each(selectedStockCategory().stockSubCategories(), function (item) {
                                if (!item.isValid()) {
                                    item.errors.showAllMessages();
                                    flag = false;
                                }
                            });
                        }

                        return flag;
                    },
                    //Save Stock Category
                    saveStockCategory = function (item) {
                        if (selectedStockCategory() != undefined && doBeforeSave()) {
                            if (selectedStockCategory().categoryId() > 0) {
                                saveEdittedStockCategory();
                            } else {
                                saveNewStockCategory(item);
                            }
                        }
                    },
                    //Save NEW Stock Category
                    saveNewStockCategory = function () {
                        dataservice.saveNewStockCategory(model.InventoryCategory().convertToServerData(selectedStockCategory()), {
                            success: function (data) {
                                selectedStockCategory().categoryId(data.CategoryId);
                                stockCategories.splice(0, 0, selectedStockCategory());
                                isStockCategoryEditorVisible(false);
                                toastr.success("Successfully save.");
                            },
                            error: function (response) {
                                toastr.error("Error: Failed to save." + response);
                            }
                        });
                    },
                    //Save EDIT Stock Category
                    saveEdittedStockCategory = function () {
                        dataservice.saveStockCategory(model.InventoryCategory().convertToServerData(selectedStockCategory()), {
                            success: function () {
                                isStockCategoryEditorVisible(false);
                                toastr.success("Successfully save.");
                            },
                            error: function (response) {
                                toastr.error("Failed to Update . Error: " + response);
                                isStockCategoryEditorVisible(false);
                            }
                        });
                    },
                    //Open Stock Category Dialog
                    openEditDialog = function () {
                        isStockCategoryEditorVisible(true);
                        getStockCategoryForEditting();
                    },
                    //Get Stock Categogy For editting
                    getStockCategoryForEditting = function () {
                        dataservice.getStockCategories({
                            StockCategoryId: selectedStockCategory().categoryId()
                        }, {
                            success: function (data) {
                                selectedStockCategory().stockSubCategories.removeAll();
                                selectedStockSubCategory(undefined);
                                if (data != null) {
                                    selectedStockCategory(model.InventoryCategory.Create(data.StockCategories[0]));
                                }
                                isLoadingStockCategories(false);
                            },
                            error: function (response) {
                                isLoadingStockCategories(false);
                                toastr.error("Failed to Load Stock Categories . Error: " + response);
                            }
                        });
                    },
                    //Close Stock Category Dialog
                    closeEditDialog = function () {
                        if (selectedStockCategory().hasChanges()) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(saveStockCategory);
                            confirmation.afterCancel(function () {
                                if (selectedStockCategory() != undefined) {
                                    if (selectedStockCategory().categoryId() > 0) {
                                        isStockCategoryEditorVisible(false);
                                    } else {
                                        isStockCategoryEditorVisible(false);
                                        stockCategories.remove(selectedStockCategory());
                                    }
                                    editorViewModel.revertItem();
                                }
                            });
                            confirmation.show();
                            return;
                        }
                    },
                    ///*** Stock Sub Categories Region ***

                    // Select a Sub Category
                    selectSubCategory = function (stockSubCategory) {
                        if (selectedStockSubCategory() !== stockSubCategory) {
                            selectedStockSubCategory(stockSubCategory);
                        }
                    },
                    // Template Chooser For Stock Sub Categories
                    templateToUseStockSubCategories = function (stockSubCategory) {
                        return (stockSubCategory === selectedStockSubCategory() ? 'editStockSubCategoryTemplate' : 'itemStockSubCategoryTemplate');
                    },
                     //Create Stock Sub Category
                     onCreateNewStockSubCategory = function () {
                         var stockSubCategory = selectedStockCategory().stockSubCategories()[0];
                         //Create Stock Categories for the very First Time
                         if (stockSubCategory == undefined) {
                             selectedStockCategory().stockSubCategories.splice(0, 0, new model.InventorySubCategory());
                             selectedStockSubCategory(selectedStockCategory().stockSubCategories()[0]);
                         }
                             //If There are already stock categories in list
                         else {
                             if (!stockSubCategory.isValid()) {
                                 stockSubCategory.errors.showAllMessages();
                             }
                             else {
                                 selectedStockCategory().stockSubCategories.splice(0, 0, new model.InventorySubCategory());
                                 selectedStockSubCategory(selectedStockCategory().stockSubCategories()[0]);
                             }
                         }
                     },
                     // Delete a Stock Sub Category
                    onDeleteStockSubCategory = function (stockSubCategory) {
                        // if (stockSubCategory.categoryId() > 0) {
                        selectedStockCategory().stockSubCategories.remove(stockSubCategory);
                        return;
                        // }
                    },

                //Initialize
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    pager(pagination.Pagination({ PageSize: 10 }, stockCategories, getStockCategories));
                    getStockCategories();
                };

                return {
                    stockCategories: stockCategories,
                    isLoadingStockCategories: isLoadingStockCategories,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    searchFilter: searchFilter,
                    editorViewModel: editorViewModel,
                    selectedStockCategory: selectedStockCategory,
                    selectedStockSubCategory: selectedStockSubCategory,
                    templateToUse: templateToUse,
                    makeEditable: makeEditable,
                    createNewStockCategory: createNewStockCategory,
                    onEditItem: onEditItem,
                    isStockCategoryEditorVisible: isStockCategoryEditorVisible,
                    deleteStockCategory: deleteStockCategory,
                    getStockCategories: getStockCategories,
                    doBeforeSave: doBeforeSave,
                    saveStockCategory: saveStockCategory,
                    saveNewStockCategory: saveNewStockCategory,
                    saveEdittedStockCategory: saveEdittedStockCategory,
                    openEditDialog: openEditDialog,
                    getStockCategoryForEditting: getStockCategoryForEditting,
                    closeEditDialog: closeEditDialog,
                    selectSubCategory: selectSubCategory,
                    templateToUseStockSubCategories: templateToUseStockSubCategories,
                    onCreateNewStockSubCategory: onCreateNewStockSubCategory,
                    onDeleteStockSubCategory: onDeleteStockSubCategory,
                    initialize: initialize
                };
            })()
        };
        return ist.inventoryCategory.viewModel;
    });
