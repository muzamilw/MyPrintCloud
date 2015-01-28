/*
    Module with the view model for the Machine List.
*/
define("machine/machine.viewModel",
    ["jquery", "amplify", "ko", "machine/machine.dataservice", "machine/machine.model", "common/confirmation.viewModel", "common/pagination"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination) {
        var ist = window.ist || {};
        ist.machine = {
            viewModel: (function () {
                var // the view 
                    view,
                    // Active
                    machineList = ko.observableArray([]),
                    errorList = ko.observableArray([]),
                    // #region Busy Indicators
                    isLoadingMachineList = ko.observable(false),
                    // #endregion Busy Indicators
                    sortOn = ko.observable(1),
                    //Sort In Ascending
                    sortIsAsc = ko.observable(true),
                    //Pager
                    pager = ko.observable(),
                    //Search Filter
                    searchFilter = ko.observable(),
                    isEditorVisible = ko.observable(),
                    selectedMachine = ko.observable(),
                    templateToUse = function (omachine) {
                        return (omachine === selectedMachine() ? 'editMachineTemplate' : 'itemMachineTemplate');
                    },
                    makeEditable = ko.observable(false),
                    createNewMachine = function () {
                        var oMachine = new model.machine();
                        editorViewModel.selectItem(oMachine);
                        openEditDialog();
                    },
                    //Delete Cost Center
                    //deleteMachine = function (oCostCenter) {
                    //    dataservice.deleteMachine({
                    //        CostCentreId: oCostCenter.CostCentreId(),
                    //    }, {
                    //        success: function (data) {
                    //            if (data != null) {
                    //                machineList.remove(oCostCenter);
                    //                toastr.success(" Deleted Successfully !");
                    //            }
                    //        },
                    //        error: function (response) {
                    //            toastr.error("Failed to Delete . Error: " + response);
                    //        }
                    //    });
                    //},
                    //onDeleteCostCenter = function (oCostCenter) {
                    //    if (!oCostCenter.CostCentreId()) {
                    //        machineList.remove(oCostCenter);
                    //        return;
                    //    }
                    //    // Ask for confirmation
                    //    confirmation.afterProceed(function () {
                    //        deleteCostCenter(oCostCenter);
                    //    });
                    //    confirmation.show();
                    //},
                    getMachines = function () {
                        isLoadingMachineList(true);
                        dataservice.GetMachineList({
                            MachineFilterText: searchFilter(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            SortBy: sortOn(),
                            IsAsc: sortIsAsc()
                        }, {
                            success: function (data) {
                                machineList.removeAll();
                                if (data != null) {
                                    pager().totalCount(data.RowCount);
                                    _.each(data.machine, function (item) {
                                        var module = model.machineListClientMapper(item);
                                        machineList.push(module);
                                    });
                                }
                                isLoadingMachineList(false);
                            },
                            error: function (response) {
                                isLoadingMachineList(false);
                                toastr.error("Error: Failed to Load Machine List Data." + response);
                            }
                        });
                    },
                    //Do Before Save
                    //doBeforeSave = function () {
                    //    var flag = true;
                    //    if (!selectedMachine().isValid()) {
                    //        selectedMachine().errors.showAllMessages();
                    //        flag = false;
                    //    }
                    //    return flag;
                    //},
                    ////Save Cost Center
                    //saveCostCenter = function (item) {
                    //    if (selectedMachine() != undefined && doBeforeSave()) {
                    //        if (selectedMachine().costCentreId() > 0) {
                    //            saveEdittedCostCenter();
                    //        } else {
                    //            saveNewCostCenter(item);
                    //        }
                    //    }
                    //},
                    ////Save NEW Cost Center
                    //saveNewCostCenter = function () {
                    //    dataservice.saveNewCostCenter(selectedMachine().convertToServerData(), {
                    //        success: function (data) {
                    //            selectedMachine().costCenterId(data.costCenterId);
                    //            machineList.splice(0, 0, selectedMachine());
                    //            view.hideCostCenterDialog();
                    //            toastr.success("Successfully save.");
                    //        },
                    //        error: function (response) {
                    //            toastr.error("Failed to save." + response);
                    //        }
                    //    });
                    //},
                    ////Save EDIT Cost Center
                    //saveEdittedCostCenter = function () {
                    //    dataservice.saveCostCenter(selectedMachine().convertToServerData(), {
                    //        success: function (data) {
                    //            var newItem = model.costCenterClientMapper(data);
                    //            var newObjtodelete = machineList.find(function (temp) {
                    //                return temp.costCenterId() == newItem.costCenterId();
                    //            });
                    //            machineList.remove(newObjtodelete);
                    //            machineList.push(newItem);
                    //            view.hideCostCenterDialog();
                    //            toastr.success("Successfully save.");
                    //        },
                    //        error: function (exceptionMessage, exceptionType) {
                    //            if (exceptionType === ist.exceptionType.CaresGeneralException) {
                    //                toastr.error(exceptionMessage);
                    //            } else {
                    //                toastr.error("Failed to save.");
                    //            }
                    //        }
                    //    });
                    //},
                    ////On Edit Click Of Cost Center
                    //onEditItem = function (oCostCenter) {
                    //    errorList.removeAll();
                    //    // selectedMachine(oCostCenter);
                    //    dataservice.getCostCentreById({
                    //        id: oCostCenter.costCentreId(),
                    //    }, {
                    //        success: function (data) {
                    //            if (data != null) {
                    //                selectedMachine(model.costCenterClientMapper(data));
                    //                selectedMachine().reset();
                    //                showCostCenterDetail();
                    //            }
                    //        },
                    //        error: function (response) {
                    //            toastr.error("Failed to load Detail . Error: ");
                    //        }
                    //    });
                    //},
                    //openEditDialog = function () {
                    //    view.showCostCenterDialog();
                    //},
                    //closeEditDialog = function () {
                    //    if (selectedMachine() != undefined) {
                    //        if (selectedMachine().costCenterId() > 0) {
                    //            isEditorVisible(false);
                    //            view.hideCostCenterDialog();
                    //        } else {
                    //            isEditorVisible(false);
                    //            view.hideCostCenterDialog();
                    //            machineList.remove(selectedMachine());
                    //        }
                    //        editorViewModel.revertItem();
                    //    }
                    //},
                    //// close CostCenter Editor
                    //closeCostCenterDetail = function () {
                    //    isEditorVisible(false);
                    //},
                    //// Show CostCenter Editor
                    //showCostCenterDetail = function () {
                    //    isEditorVisible(true);
                    //},
                    // #region Observables
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, machineList, getMachines));
                        getMachines();
                    };

                return {
                    // Observables
                    machineList: machineList,
                    selectedMachine: selectedMachine,
                    isLoadingMachineList: isLoadingMachineList,
                    //deleteCostCenter: deleteCostCenter,
                    //onDeleteCostCenter: onDeleteCostCenter,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    templateToUse: templateToUse,
                    makeEditable: makeEditable,
                    //createNewCostCenter: createNewCostCenter,
                    getMachines: getMachines,
                    //doBeforeSave: doBeforeSave,
                    //saveCostCenter: saveCostCenter,
                    //saveNewCostCenter: saveNewCostCenter,
                   // saveEdittedCostCenter: saveEdittedCostCenter,
                    //openEditDialog: openEditDialog,
                    //closeEditDialog: closeEditDialog,
                    searchFilter: searchFilter,
                    //onEditItem: onEditItem,
                    initialize: initialize
                    //isEditorVisible: isEditorVisible,
                    //closeCostCenterDetail: closeCostCenterDetail,
                    //showCostCenterDetail: showCostCenterDetail
                };
            })()
        };
        return ist.machine.viewModel;
    });
