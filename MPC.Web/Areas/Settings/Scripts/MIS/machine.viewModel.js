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
                    stockItemList = ko.observableArray([]),
                    
                    stockItemgPager = ko.observable(),
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
                    categoryID = ko.observable(),
                    isGuillotineList = ko.observable(),
                    UpdatedPapperStockID = ko.observable(),
                   // templateToUse = 'itemMachineTemplate',
                    makeEditable = ko.observable(false),
                    //createNewMachine = function () {
                    //    var oMachine = new model.machine();
                    //    editorViewModel.selectItem(oMachine);
                    //    openEditDialog();
                    //},
                    //Delete Machine
                    //deleteMachine = function (oMachine) {
                    //    dataservice.deleteMachine({
                    //        CostCentreId: oMachine.CostCentreId(),
                    //    }, {
                    //        success: function (data) {
                    //            if (data != null) {
                    //                machineList.remove(oMachine);
                    //                toastr.success(" Deleted Successfully !");
                    //            }
                    //        },
                    //        error: function (response) {
                    //            toastr.error("Failed to Delete . Error: " + response);
                    //        }
                    //    });
                    //},
                    GetMachineListForGuillotine = function () {
                        isGuillotineList = true;
                       getMachines();
                    },
                    GetMachineListForAll = function () {
                         isGuillotineList = false;
                         getMachines();
                     },
                    onArchiveMachine = function (oMachine) {
                        if (!oMachine.MachineId()) {
                            machineList.remove(oMachine);
                            return;
                        }
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            archiveMachine(oMachine);
                        });
                        confirmation.show();
                    },
                    getStockItemsList = function () {
                        dataservice.getStockItemsList({
                            SearchString: null,
                            PageSize: stockItemgPager().pageSize(),
                            PageNo: stockItemgPager().currentPage(),
                            CategoryId: categoryID,
                        }, {
                            success: function (data) {
                                stockItemList.removeAll();
                                if (data && data.TotalCount > 0) {
                                    stockItemgPager().totalCount(data.TotalCount);
                                    _.each(data.StockItems, function (item) {
                                        var stockItem = model.StockItemMapper(item)
                                        stockItemList.push(stockItem);
                                    });
                                    
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load stock items" + response);
                            }
                        });
                    },
                    getMachines = function () {
                        isLoadingMachineList(true);
                        dataservice.GetMachineList({
                            MachineFilterText: searchFilter(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            SortBy: sortOn(),
                            IsAsc: sortIsAsc(),
                            isGuillotineList: isGuillotineList
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
                    //            view.hideMachineDialog();
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
                    //            view.hideMachineDialog();
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
                    //On Edit Click Of Machine
                    OnSelectDefaultPaper = function (ostockItem) {
                        if (ostockItem.category == "Plates") {
                            $("#ddl-plateid").val(ostockItem.id);
                        } else if (ostockItem.category == "Paper") {
                            $("#ddl-paperSizeId").val(ostockItem.id);
                        }
                        $(".btn-myModal-close").click();

                    }


                    onPapperSizeStockItemPopup = function () {
                        stockItemgPager(new pagination.Pagination({ PageSize: 5 }, stockItemList, getStockItemsList)),
                        categoryID(1);
                        getStockItemsList();
                    }
                    onPlateStockItemPopup = function () {
                        stockItemgPager(new pagination.Pagination({ PageSize: 5 }, stockItemList, getStockItemsList)),
                        categoryID(4);
                        getStockItemsList();
                    }
                    onEditItem = function (oMachine) {
                        errorList.removeAll();
                        dataservice.getMachineById({
                            id: oMachine.MachineId(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    selectedMachine(model.machineClientMapper(data));
                                    selectedMachine().reset();
                                    showMachineDetail();
                                    
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load Detail . Error: ");
                            }
                        });
                    },
                    openEditDialog = function () {
                        view.showMachineDetail();
                    },
                    closeEditDialog = function () {
                        if (selectedMachine() != undefined) {
                            if (selectedMachine().MachineId() > 0) {
                                isEditorVisible(false);
                                view.hideMachineDialog();
                            } else {
                                isEditorVisible(false);
                                view.hideMachineDialog();
                                machineList.remove(selectedMachine());
                            }
                            editorViewModel.revertItem();
                        }
                    },
                    // close CostCenter Editor
                    closeMachineDetail = function () {
                        isEditorVisible(false);
                    },
                    // Show CostCenter Editor
                    showMachineDetail = function () {
                        isEditorVisible(true);
                    },
                    // #region Observables
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        isGuillotineList: false;
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
                    stockItemList: stockItemList,
                    
                    //deleteCostCenter: deleteCostCenter,
                    //onDeleteCostCenter: onDeleteCostCenter,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    stockItemgPager:stockItemgPager,
                  //  templateToUse: templateToUse,
                    makeEditable: makeEditable,
                    //createNewCostCenter: createNewCostCenter,
                    getMachines: getMachines,
                    //doBeforeSave: doBeforeSave,
                    //saveCostCenter: saveCostCenter,
                    //saveNewCostCenter: saveNewCostCenter,
                    // saveEdittedCostCenter: saveEdittedCostCenter,
                    openEditDialog: openEditDialog,
                    closeEditDialog: closeEditDialog,
                    searchFilter: searchFilter,
                    onEditItem: onEditItem,
                    initialize: initialize,
                    isEditorVisible: isEditorVisible,
                    closeMachineDetail: closeMachineDetail,
                    showMachineDetail: showMachineDetail,
                    getStockItemsList: getStockItemsList,
                    onPapperSizeStockItemPopup: onPapperSizeStockItemPopup,
                    onPlateStockItemPopup: onPlateStockItemPopup,
                    categoryID: categoryID,
                    onArchiveMachine: onArchiveMachine,
                    isGuillotineList: isGuillotineList,
                    GetMachineListForGuillotine: GetMachineListForGuillotine,
                    GetMachineListForAll: GetMachineListForAll,
                    OnSelectDefaultPaper: OnSelectDefaultPaper,
                    UpdatedPapperStockID: UpdatedPapperStockID

                  
                };
            })()
        };
        return ist.machine.viewModel;
    });
