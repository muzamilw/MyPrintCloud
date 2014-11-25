﻿/*
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
                    //stock Categories
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
                    editorViewModel = new ist.ViewModel(model.StockCategory),
                    //Selected Paper Sheet
                    selectedStockCategory = editorViewModel.itemForEditing,
                    //Template To Use
                    templateToUse = function (stockCategory) {
                        return (stockCategory === selectedStockCategory() ? 'editStockCategoryTemplate' : 'itemStockCategoryTemplate');
                    },
                    //Make Edittable
                    makeEditable = ko.observable(false),
                    //Create New Stock Category
                    createNewStockCategory = function () {
                        var stockCategory = new model.StockCategory();
                        editorViewModel.selectItem(stockCategory);
                        openEditDialog();
                    },
                    //On Edit Click Of Stock Category
                    onEditItem = function (item) {
                        editorViewModel.selectItem(item);
                        openEditDialog();
                    },
                    //Delete Stock Category
                    deleteStockCategory = function (stockCategory) {
                        dataservice.deleteStockCategory({
                            StockCategoryId: stockCategory.stockCategoryId(),
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
                                        var module = model.stockCategoryServertoClientMapper(item);
                                        stockCategories.push(module);
                                    });
                                }
                                isLoadingStockCategories(false);
                            },
                            error: function () {
                                isLoadingStockCategories(false);
                                toastr.error(ist.resourceText.loadAddChargeDetailFailedMsg);
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
                        return flag;
                    },
                    //Save Stock Category
                    saveStockCategory = function (item) {
                        if (selectedStockCategory() != undefined && doBeforeSave()) {
                            if (selectedStockCategory().stockCategoryId() > 0) {
                                saveEdittedStockCategory();
                            } else {
                                saveNewStockCategory(item);
                            }
                        }
                    },
                    //Save NEW Stock Category
                    saveNewStockCategory = function () {
                        dataservice.saveNewStockCategory(selectedStockCategory().convertToServerData(), {
                            success: function (data) {
                                selectedStockCategory().stockCategoryId(data.StockCategoryId);
                                stockCategories.splice(0, 0, selectedStockCategory());
                                view.hideInventoryCategoryDialog();
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
                    //Save EDIT Stock Category
                    saveEdittedStockCategory = function () {
                        dataservice.saveStockCategory(selectedStockCategory().convertToServerData(), {
                            success: function (data) {
                                var newItem = model.stockCategoryServertoClientMapper(data);
                                var newObjtodelete = stockCategories.find(function (temp) {
                                    return temp.stockCategoryId() == newItem.stockCategoryId();
                                });
                                stockCategories.remove(newObjtodelete);
                                stockCategories.push(newItem);
                                view.hideInventoryCategoryDialog();
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
                    //Open Stock Category Dialog
                    openEditDialog = function () {
                        view.showInventoryCategoryDialog();
                    },
                    //CLose Stock Category Dialog
                    closeEditDialog = function () {
                        if (selectedStockCategory() != undefined) {
                            if (selectedStockCategory().stockCategoryId() > 0) {
                                view.hideInventoryCategoryDialog();
                            } else {
                                view.hideInventoryCategoryDialog();
                                stockCategories.remove(selectedStockCategory());
                            }
                            editorViewModel.revertItem();
                        }
                    },
                    //*** Stock Sub Categories Region ***
                    //Get All Stock Sub Categories By Stock Category Id

                    //Delete Stock Sub Category

                    //Edit Stock Sub Category

                    //*** Stock Sub Categories Region ***
                //Initialize
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    pager(pagination.Pagination({ PageSize: 5 }, stockCategories, getStockCategories));
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
                    templateToUse: templateToUse,
                    makeEditable: makeEditable,
                    createNewStockCategory: createNewStockCategory,
                    onEditItem: onEditItem,
                    deleteStockCategory: deleteStockCategory,
                    getStockCategories: getStockCategories,
                    doBeforeSave: doBeforeSave,
                    saveNewStockCategory: saveNewStockCategory,
                    saveEdittedStockCategory: saveEdittedStockCategory,
                    openEditDialog: openEditDialog,
                    closeEditDialog: closeEditDialog,
                    initialize: initialize
                };
            })()
        };
        return ist.inventoryCategory.viewModel;
    });
