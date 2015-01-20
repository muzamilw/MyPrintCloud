/*
    Module with the view model for the My Organization.
*/
define("costcenter/costcenter.viewModel",
    ["jquery", "amplify", "ko", "costcenter/costcenter.dataservice", "costcenter/costcenter.model", "common/confirmation.viewModel", "common/pagination"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination) {
        var ist = window.ist || {};
        ist.costcenter = {
            viewModel: (function () {
                var // the view 
                    view,
                    // Active
                    costCentersList = ko.observableArray([]),
                    errorList = ko.observableArray([]),
                    // #region Busy Indicators
                    isLoadingCostCenter = ko.observable(false),
                    // #endregion Busy Indicators
                    sortOn = ko.observable(1),
                    //Sort In Ascending
                    sortIsAsc = ko.observable(true),
                    //Pager
                    pager = ko.observable(),
                    //Search Filter
                    searchFilter = ko.observable(),
                    editorViewModel = new ist.ViewModel(model.CostCenter),
                    selectedCostCenter = editorViewModel.itemForEditing,
                    templateToUse = function(paperSheet) {
                        return (paperSheet === selectedPaperSheet() ? 'editPaperSheetTemplate' : 'itemPaperSheetTemplate');
                    },
                    makeEditable = ko.observable(false),
                    createNewCostCenter = function() {
                        var oCostCenter = new model.CostCenter();
                        editorViewModel.selectItem(oCostCenter);
                        openEditDialog();
                    },
                    //Delete Cost Center
                    deleteCostCenter = function(oCostCenter) {
                        dataservice.deleteCostCenter({
                            CostCentreId: oCostCenter.CostCentreId(),
                        }, {
                            success: function(data) {
                                if (data != null) {
                                    costCentersList.remove(oCostCenter);
                                    toastr.success(" Deleted Successfully !");
                                }
                            },
                            error: function(response) {
                                toastr.error("Failed to Delete . Error: " + response);
                            }
                        });
                    },
                    onDeleteCostCenter = function(oCostCenter) {
                        if (!oCostCenter.CostCentreId()) {
                            costCentersList.remove(oCostCenter);
                            return;
                        }
                        // Ask for confirmation
                        confirmation.afterProceed(function() {
                            deleteCostCenter(oCostCenter);
                        });
                        confirmation.show();
                    },
                    getCostCenters = function() {
                        isLoadingCostCenter(true);
                        dataservice.getCostCentersList({
                            CostCenterFilterText: searchFilter(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            SortBy: sortOn(),
                            IsAsc: sortIsAsc()
                        }, {
                            success: function(data) {
                                costCentersList.removeAll();
                                if (data != null) {
                                    pager().totalCount(data.RowCount);
                                    _.each(data.costCentersList, function(item) {
                                        var module = model.costCenterClientMapper(item);
                                        costCentersList.push(module);
                                    });
                                }
                                isLoadingCostCenter(false);
                            },
                            error: function(response) {
                                isLoadingCostCenter(false);
                                toastr.error("Error: Failed to Load Cost Centers Data." + response);
                            }
                        });
                    },
                    //Do Before Save
                    doBeforeSave = function() {
                        var flag = true;
                        if (!selectedCostCenter().isValid()) {
                            selectedCostCenter().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                    //Save Cost Center
                    saveCostCenter = function(item) {
                        if (selectedCostCenter() != undefined && doBeforeSave()) {
                            if (selectedCostCenter().costCentreId() > 0) {
                                saveEdittedCostCenter();
                            } else {
                                saveNewCostCenter(item);
                            }
                        }
                    },
                    //Save NEW Cost Center
                    saveNewCostCenter = function() {
                        dataservice.saveNewCostCenter(selectedCostCenter().convertToServerData(), {
                            success: function(data) {
                                selectedCostCenter().costCenterId(data.costCenterId);
                                costCentersList.splice(0, 0, selectedCostCenter());
                                view.hideCostCenterDialog();
                                toastr.success("Successfully save.");
                            },
                            error: function(response) {
                                toastr.error("Failed to save." + response);
                            }
                        });
                    },
                    //Save EDIT Cost Center
                    saveEdittedCostCenter = function() {
                        dataservice.saveCostCenter(selectedCostCenter().convertToServerData(), {
                            success: function(data) {
                                var newItem = model.costCenterClientMapper(data);
                                var newObjtodelete = costCentersList.find(function(temp) {
                                    return temp.costCenterId() == newItem.costCenterId();
                                });
                                costCentersList.remove(newObjtodelete);
                                costCentersList.push(newItem);
                                view.hideCostCenterDialog();
                                toastr.success("Successfully save.");
                            },
                            error: function(exceptionMessage, exceptionType) {
                                if (exceptionType === ist.exceptionType.CaresGeneralException) {
                                    toastr.error(exceptionMessage);
                                } else {
                                    toastr.error("Failed to save.");
                                }
                            }
                        });
                    },
                    //On Edit Click Of Cost Center
                    onEditItem = function(item) {
                        editorViewModel.selectItem(item);
                        openEditDialog();
                    },
                    openEditDialog = function() {
                        view.showCostCenterDialog();
                    },
                    closeEditDialog = function () {
                        if (selectedCostCenter() != undefined) {
                            if (selectedCostCenter().costCenterId() > 0) {
                                view.hideCostCenterDialog();
                            } else {
                                view.hideCostCenterDialog();
                                costCentersList.remove(selectedCostCenter());
                            }
                            editorViewModel.revertItem();
                        }
                    },
                    // #region Observables
                    // Initialize the view model
                    initialize = function(specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, costCentersList, getCostCenters));
                        getCostCenters();
                    };
                    
                return {
                    // Observables
                    costCentersList: costCentersList,
                    selectedCostCenter: selectedCostCenter,
                    isLoadingCostCenter: isLoadingCostCenter,
                    deleteCostCenter: deleteCostCenter,
                    onDeleteCostCenter: onDeleteCostCenter,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    templateToUse: templateToUse,
                    makeEditable: makeEditable,
                    createNewCostCenter: createNewCostCenter,
                    getCostCenters: getCostCenters,
                    doBeforeSave: doBeforeSave,
                    saveCostCenter: saveCostCenter,
                    saveNewCostCenter: saveNewCostCenter,
                    saveEdittedCostCenter: saveEdittedCostCenter,
                    openEditDialog: openEditDialog,
                    closeEditDialog: closeEditDialog,
                    searchFilter: searchFilter,
                    editorViewModel: editorViewModel,
                    onEditItem: onEditItem,
                    initialize: initialize,
                };
            })()
        };
        return ist.costcenter.viewModel;
    });
