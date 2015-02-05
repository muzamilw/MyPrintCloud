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
                    // Cost Center Types
                    costCenterTypes = ko.observableArray([]),
                    // System Users as Resources
                    costCenterResources = ko.observableArray([]),
                    // Nominal Codes
                    nominalCodes = ko.observableArray([]),
                    costCenterVariables = ko.observableArray([]),
                    // Markups
                    markups = ko.observableArray([]),
                    // Cost Center Categories
                    costCenterCategories = ko.observableArray([]),
                    costCenterInstructions = ko.observableArray([]),
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
                    isEditorVisible = ko.observable(),
                    selectedCostCenter = ko.observable(),
                    templateToUse = function(ocostCenter) {
                        return (ocostCenter === selectedCostCenter() ? 'editCostCenterTemplate' : 'itemCostCenterTemplate');
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
                                    _.each(data.CostCenters, function(item) {
                                        var module = model.costCenterListView.Create(item);
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
                    saveCostCenter = function () {
                        errorList.removeAll();
                        if (doBeforeSave()) {
                            saveEdittedCostCenter(callback);
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
                    saveEdittedCostCenter = function (callback) {
                        dataservice.saveCostCenter(model.costCenterServerMapper(selectedCostCenter()), {
                            success: function (data) {
                                if (callback && typeof callback === "function") {
                                    callback();
                                }
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
                    //On Edit Click Of Cost Center
                    onEditItem = function (oCostCenter) {
                        errorList.removeAll();
                        getCostCentersBaseData();
                        dataservice.getCostCentreById({
                            id: oCostCenter.costCenterId(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    selectedCostCenter(model.costCenterClientMapper(data));
                                    costCenterInstructions.removeAll();
                                    _.each(data.CostcentreInstructions, function (item) {
                                        costCenterInstructions.push(model.costCenterInstructions.CreateForClient(item));
                                    selectedCostCenter().reset();
                                    showCostCenterDetail();
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load Detail . Error: ");
                            }
                        });
                    },
                    openEditDialog = function() {
                        view.showCostCenterDialog();
                    },
                    closeEditDialog = function () {
                        if (selectedCostCenter() != undefined) {
                            if (selectedCostCenter().costCenterId() > 0) {
                                isEditorVisible(false);
                                view.hideCostCenterDialog();
                            } else {
                                isEditorVisible(false);
                                view.hideCostCenterDialog();
                                costCentersList.remove(selectedCostCenter());
                            }
                            editorViewModel.revertItem();
                        }
                    },
                    // close CostCenter Editor
                    closeCostCenterDetail = function () {
                        isEditorVisible(false);
                    },
                    // Show CostCenter Editor
                    showCostCenterDetail = function () {
                        isEditorVisible(true);
                    },
                    // Get Base
                    getCostCentersBaseData = function () {
                        dataservice.getBaseData({
                            success: function (data) {
                                //costCenter Calculation Types
                                costCenterTypes.removeAll();
                                ko.utils.arrayPushAll(costCenterTypes(), data.CalculationTypes);
                                costCenterTypes.valueHasMutated();
                                //Cost Center Categories
                                costCenterCategories.removeAll();
                                ko.utils.arrayPushAll(costCenterCategories(), data.CostCenterCategories);
                                costCenterCategories.valueHasMutated();
                                //Nominal Codes
                                nominalCodes.removeAll();
                                ko.utils.arrayPushAll(nominalCodes(), data.NominalCodes);
                                nominalCodes.valueHasMutated();
                                //Markups
                                markups.removeAll();
                                ko.utils.arrayPushAll(markups(), data.Markups);
                                markups.valueHasMutated();
                                //Variables
                                costCenterVariables.removeAll();
                                ko.utils.arrayPushAll(costCenterVariables(), data.CostCentreVariables);
                                costCenterVariables.valueHasMutated();

                                //Cost Center Resources
                                costCenterResources.removeAll();
                                ko.utils.arrayPushAll(costCenterResources(), data.CostCenterResources);
                                costCenterResources.valueHasMutated();
                            },
                            error: function () {
                                toastr.error("Failed to base data.");
                            }
                        });
                    },
                    // #region Observables
                    // Initialize the view model
                    initialize = function(specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, costCentersList, getCostCenters));
                        getCostCenters();
                       // getCostCentersBaseData();
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
                    onEditItem: onEditItem,
                    initialize: initialize,
                    isEditorVisible: isEditorVisible,
                    closeCostCenterDetail: closeCostCenterDetail,
                    showCostCenterDetail: showCostCenterDetail,
                    getCostCentersBaseData: getCostCentersBaseData,
                    costCenterTypes: costCenterTypes,
                    costCenterCategories: costCenterCategories,
                    nominalCodes: nominalCodes,
                    markups: markups,
                    costCenterResources: costCenterResources,
                    costCenterVariables: costCenterVariables,
                    costCenterInstructions: costCenterInstructions
                };
            })()
        };
        return ist.costcenter.viewModel;
    });
