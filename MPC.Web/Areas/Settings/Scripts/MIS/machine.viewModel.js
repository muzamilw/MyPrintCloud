/*
    Module with the view model for the Machine List.
*/
define("machine/machine.viewModel",
    ["jquery", "amplify", "ko", "machine/machine.dataservice", "machine/machine.model", "common/confirmation.viewModel", "common/pagination", "common/stockItem.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, stockDialog) {
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
                    
                    gotoElement = function (validation) {
                        view.gotoElement(validation.element);
                    },
                     setValidationSummary = function (selectedItem) {
                         errorList.removeAll();
                         if (selectedItem.Description.error) {
                             errorList.push({ name: "Description is Required", element: selectedItem.Description.error });
                         }
                         
                     },
                    GetMachineListForGuillotine = function () {
                        isGuillotineList(true);
                       getMachines();
                    },
                    GetMachineListForAll = function () {
                         isGuillotineList(false);
                         getMachines();
                     },
                    onArchiveMachine = function (oMachine) {
                        if (!oMachine.MachineId()) {
                            machineList.remove(oMachine);
                            return;
                        }
                        // Ask for confirmation
  
                        confirmation.messageText("Do you want to Archive this Machine?");
                        confirmation.afterProceed(function () {
                            dataservice.deleteMachine({
                                machineId: oMachine.MachineId()
                            },
                            {
                                success: function (data) {
                                    machineList.remove(oMachine);
                                    toastr.success(" Deleted Successfully !");
                                    
                                },
                                error: function (response) {
                                    toastr.error("Failed to Delete Machine" + response);
                                }
                            });
                        });
                        confirmation.afterCancel(function () {
                            //navigateToUrl(element);
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
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedMachine().isValid()) {
                            selectedMachine().errors.showAllMessages();
                            setValidationSummary(selectedMachine());
                            flag = false;
                        }
                        return flag;
                    },
                    onCloseMachineEditor = function () {
                        if (selectedMachine().hasChanges()) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(saveMachine);
                            confirmation.afterCancel(function () {
                                selectedMachine().reset();
                                CloseMachineEditor();
                            });
                            confirmation.show();
                            return;
                        }
                        CloseMachineEditor();
                    },

                    CloseMachineEditor = function () {
                       isEditorVisible(false);
                        errorList.removeAll();
                    },
                     createNewMachine = function () {
                         var oMachine = new model.machine();
                         selectedMachine(oMachine);
                         openEditDialog();
                     },
                    onEditItem = function () {
                        errorList.removeAll();
                        dataservice.getMachineById({
                            IsGuillotine: isGuillotineList(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    selectedMachine().markupList.removeAll();
                                    ko.utils.arrayPushAll(selectedMachine().markupList(), data.Markups);
                                    selectedMachine().markupList.valueHasMutated();

                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load Detail . Error: ");
                            }
                        });
                    },
                    //Save Machine
                    saveMachine = function (item) {
                        if (selectedMachine() != undefined && doBeforeSave()) {
                            if (selectedMachine().MachineId() > 0) {
                                saveEdittedMachine();
                            }
                            //else {
                            //    saveNewMachine(item);
                            //}
                        }
                    },
                    onplateChange = function () {
                        if (selectedMachine() != undefined && selectedMachine().isplateused()) {
                            selectedMachine().deFaultPlatesName(null);
                            selectedMachine().iswashupused(false);
                            selectedMachine().ismakereadyused(false);
                            selectedMachine().MakeReadyPrice(0);
                            selectedMachine().WashupPrice(0);
                        }
                    },
                    onismakereadyusedChange = function () {
                        if (selectedMachine() != undefined && selectedMachine().ismakereadyused()) {
                            selectedMachine().MakeReadyPrice(0);
                        }
                    },
                    oniswashupusedChange = function () {
                         if (selectedMachine() != undefined && selectedMachine().iswashupused()) {
                             selectedMachine().WashupPrice(0);
                         }
                     },

                    //Save EDIT Machine
                    saveEdittedMachine = function () {
                       
                        dataservice.saveMachine(model.machineServerMapper(selectedMachine()), {
                            success: function (data) {
                                selectedMachine().reset();
                                errorList.removeAll();
                                toastr.success("Successfully save.");

                            },
                            error: function (exceptionMessage, exceptionType) {
                                if (exceptionType === ist.exceptionType.MPCGeneralException) {
                                    toastr.error(exceptionMessage);
                                } else {
                                    toastr.error("Failed to save.");
                                }
                            }
                        });
                    },
                   
                   

                    onPapperSizeStockItemPopup = function () {
                        openStockItemDialog(1);//for Paper
                    },
                    onPlateStockItemPopup = function () {
                        openStockItemDialog(4); //for plate
                    },
                    openStockItemDialog = function (stockCategoryId) {
                        stockDialog.show(function (stockItem) {
                            selectedMachine().onSelectStockItem(stockItem);
                            //onSelectStockItem(stockItem);
                        }, stockCategoryId, false);
                    },
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
                        isEditorVisible(true);
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
                        isGuillotineList(false);
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
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    stockItemgPager:stockItemgPager,
                    makeEditable: makeEditable,
                    getMachines: getMachines,
                    doBeforeSave: doBeforeSave,
                    saveMachine: saveMachine,
                    errorList:errorList,
                    saveEdittedMachine: saveEdittedMachine,
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
                    UpdatedPapperStockID: UpdatedPapperStockID,
                    openStockItemDialog: openStockItemDialog,
                    onCloseMachineEditor: onCloseMachineEditor,
                    CloseMachineEditor: CloseMachineEditor,
                    gotoElement: gotoElement,
                    setValidationSummary: setValidationSummary,
                    onplateChange: onplateChange,
                    onismakereadyusedChange:onismakereadyusedChange,
                    oniswashupusedChange: oniswashupusedChange,
                    createNewMachine: createNewMachine

                  
                };
            })()
        };
        return ist.machine.viewModel;
    });
