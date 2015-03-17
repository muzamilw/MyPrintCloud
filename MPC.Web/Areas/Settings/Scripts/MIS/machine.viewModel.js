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
                        $("#btnCreateNewMachine").html('Create New Guillotine');
                        isGuillotineList(true);
                       getMachines();
                    },
                    GetMachineListForAll = function () {
                        $("#btnCreateNewMachine").html('Create New Press');
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
                            isGuillotineList: isGuillotineList()
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
                    createNewMachine = function (oMachine) {
                         errorList.removeAll();
                         dataservice.getMachineById({
                             IsGuillotine: isGuillotineList(),
                         }, {
                             success: function (data) {
                                 if (data != null) {
                                     selectedMachine(model.newMachineClientMapper(data));
                                     selectedMachine().reset();
                                     isEditorVisible(true);

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
                            else {
                                if (isGuillotineList()) {
                                    selectedMachine().MachineCatId(4);
                                        } else {
                                    selectedMachine().MachineCatId(2);
                                        }
                                saveNewMachine();
                            }
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
                                errorList.removeAll();
                                toastr.success("Successfully Saved.");
                                isEditorVisible(false);
                                _.each(machineList(), function (machine) {
                                    if (machine && machine.MachineId() == selectedMachine().MachineId()) {
                                        machine.Description(selectedMachine().Description());
                                        machine.MachineName(selectedMachine().MachineName());
                                        machine.maximumsheetwidth(selectedMachine().maximumsheetwidth());
                                        machine.maximumsheetheight(selectedMachine().maximumsheetheight());
                                        machine.minimumsheetwidth(selectedMachine().minimumsheetwidth());
                                        machine.minimumsheetheight(selectedMachine().minimumsheetheight());
                                        if (machine.LookupMethodId() != selectedMachine().LookupMethodId()) {
                                            _.each(selectedMachine().lookupList(), function (lookupItm) {
                                                if (lookupItm && lookupItm.MethodId == selectedMachine().LookupMethodId()) {
                                                    machine.LookupMethodName(lookupItm.Name);
                                                }

                                            });
                                         }

                                    }
                                });
                               
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
                    saveNewMachine = function () {
                         
                         dataservice.saveNewMachine(model.machineServerMapper(selectedMachine()), {
                             success: function (data) {
                                 selectedMachine().reset();
                                 errorList.removeAll();
                                 
                                 selectedMachine().MachineId(data);
                                 isEditorVisible(false);

                                 toastr.success("Successfully save.");
                                 var module = model.machineListClientMapperSelectedItem(selectedMachine());
                                 
                                     _.each(selectedMachine().lookupList(), function (lookupItm) {
                                         if (lookupItm && lookupItm.MethodId == selectedMachine().LookupMethodId()) {
                                             module.LookupMethodName(lookupItm.Name);
                                         }

                                     });
                                 
                                 machineList.push(module);
                                

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
                                    isEditorVisible(true);
                                    
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load Detail . Error: ");
                            }
                        });
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
                    closeMachineDetail = function () {
                        isEditorVisible(false);
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
                    
                    closeEditDialog: closeEditDialog,
                    searchFilter: searchFilter,
                    onEditItem: onEditItem,
                    initialize: initialize,
                    isEditorVisible: isEditorVisible,
                    closeMachineDetail: closeMachineDetail,
                   
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
                    createNewMachine: createNewMachine,
                    saveNewMachine: saveNewMachine

                  
                };
            })()
        };
        return ist.machine.viewModel;
    });
