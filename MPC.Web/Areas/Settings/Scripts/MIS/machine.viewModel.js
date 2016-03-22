/*
    Module with the view model for the Machine List.
*/
define("machine/machine.viewModel",
    ["jquery", "amplify", "ko", "machine/machine.dataservice", "machine/machine.model", "common/confirmation.viewModel", "common/pagination", "common/stockItem.viewModel", "lookupMethods/lookupMethods.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, stockDialog,lookupMethodViewModel) {
        var ist = window.ist || {};
        ist.machine = {
            viewModel: (function () {
                var // the view 
                    view,
                    // Active
                    machineList = ko.observableArray([]),
                    errorList = ko.observableArray([]),
                    stockItemList = ko.observableArray([]),
                    MachineType = ko.observable(),
                    stockItemgPager = ko.observable(),
                    // #region Busy Indicators
                    isLoadingMachineList = ko.observable(false),
                    // #endregion Busy Indicators
                    sortOn = ko.observable(1),
                    //Sort In Ascending
                    sortIsAsc = ko.observable(true),
                    // machine lookup methods
                    machinelookups = ko.observableArray([]),
                    machineInkCovergeList = ko.observableArray([]),
                    //Pager
                    pager = ko.observable(),
                    currentSpeedWeight = ko.observableArray([]),
                    currentClickChargeZone = ko.observableArray([]),
                    //Search Filter
                    searchFilter = ko.observable(),
                    isEditorVisible = ko.observable(),
                    selectedMachine = ko.observable(),
                    categoryID = ko.observable(),
                    isGuillotineList = ko.observable(),
                    UpdatedPapperStockID = ko.observable(),
                    MachineName = ko.observable(),
                   // templateToUse = 'itemMachineTemplate',
                    makeEditable = ko.observable(false),
                    LookupMethodHasChange = ko.observable(false);
                   machinehasChanges = ko.computed(function () {
                         
                       var hasChanges = false;
                       var ClickChargeChange = false;
                       var MeterPerHour = false;
                       var GuillotineCharge = false;
                       var isSpeedWeightChange = false;

                       if (selectedMachine()) {
                           hasChanges = selectedMachine().hasChanges();
                       }
                       var pagetype = Request.QueryString("type").toString();

                       if (pagetype != null) {
                           if (pagetype == 'press') {

                               if (selectedMachine())
                               {
                                   if (selectedMachine().MachineId() > 0)
                                   {
                                       if (selectedMachine().isSheetFed() == true || selectedMachine().isSheetFed() == "true") {
                                           
                                           if (MachineType() == 4) {
                                               if (lookupMethodViewModel.selectedSpeedWeight() && lookupMethodViewModel.selectedSpeedWeight().Id() != undefined) {
                                                   isSpeedWeightChange = lookupMethodViewModel.selectedSpeedWeight().hasChanges();
                                               }
                                           } else {
                                               if (lookupMethodViewModel.selectedClickChargeZones()) {
                                                   ClickChargeChange = lookupMethodViewModel.selectedClickChargeZones().hasChanges();
                                               }
                                           }
                                           
                                       }
                                       else {
                                           if (lookupMethodViewModel.selectedMeterPerHourClickCharge()) {
                                               MeterPerHour = lookupMethodViewModel.selectedMeterPerHourClickCharge().hasChanges();
                                           }
                                       }
                                   }
                                       
                                 
                               }
                              
                              

                           }
                           else
                           {
                               if (lookupMethodViewModel.selectedGuillotineClickCharge()) {
                                   GuillotineCharge = lookupMethodViewModel.selectedGuillotineClickCharge().hasChanges();
                               }
                           }
                       }
                       
                    
                     
                      
                       
                       return hasChanges || ClickChargeChange || MeterPerHour || GuillotineCharge || isSpeedWeightChange;
                         
                     }),
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
                        // $("#btnCreateNewMachine").html('Create New Guillotine');
                        
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

                        confirmation.messageText("WARNING - This item will be archived from the system and you won't be able to use it");
                        confirmation.afterProceed(function () {
                            dataservice.deleteMachine({
                                machineId: oMachine.MachineId()
                            },
                            {
                                success: function (data) {
                                    machineList.remove(oMachine);
                                    toastr.success(" Deleted Successfully !");
                                    isEditorVisible(false);
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

                                    _.each(data.machine, function (item) {
                                        var module = model.machineListClientMapper(item);
                                        machineList.push(module);
                                    });
                                    pager().totalCount(data.RowCount);
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
                        else if (selectedMachine().isplateused() && (selectedMachine().DefaultPlateId() == undefined || selectedMachine().DefaultPlateId() <= 0)) {
                            toastr.error("Error: Please select default plate if press require plates.");
                            flag = false;
                        }
                        return flag;
                    },
                    onCloseMachineEditor = function () {
                        if (selectedMachine().hasChanges() || lookupMethodViewModel.selectedGuillotineClickCharge().hasChanges() || lookupMethodViewModel.selectedClickChargeZones().hasChanges() || lookupMethodViewModel.selectedMeterPerHourClickCharge().hasChanges()) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(saveMachine);
                            confirmation.afterCancel(function () {
                                selectedMachine().reset();
                                lookupMethodViewModel.selectedClickChargeZones().reset();
                                lookupMethodViewModel.selectedMeterPerHourClickCharge().reset();
                                lookupMethodViewModel.selectedGuillotineClickCharge().reset();

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
                   subscribeMachineChange = function () {
                       selectedMachine().ColourHeads.subscribe(function (value) {
                           if (value !== selectedMachine().ColourHeads()) {
                               selectedMachine().ColourHeads(value);
                           }
                           filterInkCoverage(value);
                       });
                   },
                   filterInkCoverage = function (count) {
                       if (selectedMachine().MachineId() > 0) {
                           if (count < selectedMachine().MachineInkCoverages().length) {
                               selectedMachine().MachineInkCoverages().splice(count, selectedMachine().MachineInkCoverages().length - count);
                               selectedMachine().MachineInkCoverages.valueHasMutated();
                           }
                           else if (count > selectedMachine().MachineInkCoverages().length) {
                               var number = count - selectedMachine().MachineInkCoverages().length;
                               var inkList = selectedMachine().MachineInkCoverages()[0].StockItemforInkList();
                               var coverageList = selectedMachine().MachineInkCoverages()[0].InkCoveragItems();
                               var newCoverage = { Id: 0, SideInkOrder: inkList[0].StockItemId, SideInkOrderCoverage: coverageList[0].CoverageGroupId, MachineId: selectedMachine().MachineId() };
                               for (i = 0; i < number; i++) {
                                   selectedMachine().MachineInkCoverages().push(model.MachineInkCoveragesListClientMapper(newCoverage, inkList, coverageList));
                                 }
                               selectedMachine().MachineInkCoverages.valueHasMutated();
                           }
                           
                       } else {
                           selectedMachine().MachineInkCoverages.removeAll();
                           ko.utils.arrayPushAll(selectedMachine().MachineInkCoverages(), machineInkCovergeList.take(count));
                           selectedMachine().MachineInkCoverages.valueHasMutated();
                       }
                       
                       
                   },
                 

                    createNewMachine = function (oMachine) {
                        errorList.removeAll();
                        dataservice.getMachineById({
                            IsGuillotine: isGuillotineList(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    lookupMethodViewModel.CurrencySymbol(data.CurrencySymbol);
                                    lookupMethodViewModel.WeightUnit(data.WeightUnit == "kg" ? "gsm" : data.WeightUnit);
                                    lookupMethodViewModel.LengthUnit(data.LengthUnit);
                                    
                                    selectedMachine(model.newMachineClientMapper(data, onCalculationMethodChange));
                                    selectedMachine().isSheetFed("true");
                                    selectedMachine().IsSpotColor("true");
                                    selectedMachine().reset();
                                    ko.utils.arrayPushAll(machineInkCovergeList(), selectedMachine().MachineInkCoverages());
                                    machineInkCovergeList.valueHasMutated();
                                    
                                    subscribeMachineChange();
                                    var pagetype = Request.QueryString("type").toString();

                                    if (pagetype != null) {
                                        if (pagetype == 'press') {
                                            $("#isSheetFedRadio").css("display", "block");
                                            
                                            currentClickChargeZone.removeAll();
                                            currentSpeedWeight.removeAll();
                                            currentClickChargeZone.push(lookupMethodViewModel.selectedClickChargeZones());
                                        }
                                        else {
                                            $("#isSheetFedRadio").css("display", "none");
                                        }
                                    }
                                    showMachineDetail();
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
                            if (selectedMachine().isSheetFed() == "true") {
                                if (selectedMachine().isClickChargezone() == "true") {
                                    MachineType(0);
                                } else {
                                    MachineType(4);
                                }
                            }
                            if (selectedMachine().MachineId() > 0) {
                                
                                saveEdittedMachine();

                                //var pagetype = Request.QueryString("type").toString();

                                //if (pagetype != null) {
                                //    if (pagetype == 'press') {
                                //        if(selectedMachine().isSheetFed() == true || selectedMachine().isSheetFed() == "true")
                                //        {
                                //            lookupMethodViewModel.saveEdittedLookup(selectedMachine().LookupMethodId(), lookupMethodViewModel.selectedClickChargeZones(), null, null);
                                //        }
                                //        else{
                                //            lookupMethodViewModel.saveEdittedLookup(selectedMachine().LookupMethodId(), null, null, lookupMethodViewModel.selectedMeterPerHourClickCharge());
                                //        }

                                //    }else
                                //    {
                                //        lookupMethodViewModel.saveEdittedLookup(selectedMachine().LookupMethodId(), null, lookupMethodViewModel.selectedGuillotineClickCharge(), null);
                                //    }
                                //}
                                
                            }
                            else {
                                if (isGuillotineList()) {
                                    selectedMachine().MachineCatId(4);
                                } else {
                                    selectedMachine().MachineCatId(2);
                                }
                                var pagetype = Request.QueryString("type").toString();

                                if (pagetype != null) {
                                    if (pagetype == 'press') {
                                        if (selectedMachine().isPressUseInks() == undefined) {
                                            selectedMachine().isPressUseInks(true);
                                        }

                                        if(selectedMachine().isSheetFed() == true || selectedMachine().isSheetFed() == "true")
                                        {
                                            //GuilotinePtv

                                            //lookupMethodViewModel.selectedMeterPerHourClickCharge(null);
                                            //lookupMethodViewModel.selectedGuillotineClickCharge(null);
                                            //saveNewMachine(lookupMethodViewModel.selectedClickChargeZones(), lookupMethodViewModel.selectedGuillotineClickCharge(), lookupMethodViewModel.selectedMeterPerHourClickCharge(), lookupMethodViewModel.selectedGuillotineClickCharge() != null ? lookupMethodViewModel.selectedGuillotineClickCharge().GuillotinePTVList() : null);

                                            //MachineType(5);
                                            var speedWeightLookup = null;
                                            if (MachineType() == 4) {
                                                speedWeightLookup = lookupMethodViewModel.selectedSpeedWeight();
                                                speedWeightLookup = model.speedWeightconvertToServerData(speedWeightLookup);
                                                saveNewMachine(lookupMethodViewModel.selectedClickChargeZones(), null, null, 4, speedWeightLookup);
                                            } else {
                                                saveNewMachine(lookupMethodViewModel.selectedClickChargeZones(), null, null, 5);
                                            }
                                            

                                        }
                                        else
                                        {
                                            //lookupMethodViewModel.selectedMeterPerHourClickCharge(null);
                                            //lookupMethodViewModel.selectedGuillotineClickCharge(null);
                                            //MachineType(8);
                                            var meter = lookupMethodViewModel.oMeterPerHour();
                                            saveNewMachine(null, null, meter, 8);
                                        }
                                    }
                                    else { // case of guiltotine
                                       // MachineType(6);
                                        saveNewMachine(null, lookupMethodViewModel.selectedGuillotineClickCharge(), null, 6);
                                    }
                                }

                                
                              //  lookupMethodViewModel.saveNewLookup(lookupMethodViewModel.selectedClickChargeZones(), lookupMethodViewModel.selectedGuillotineClickCharge(), lookupMethodViewModel.selectedMeterPerHourClickCharge());
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
                            selectedMachine().DefaultPlateId(null);
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
                   onCalculationMethodChange = function () {
                       if (selectedMachine() != undefined && selectedMachine().isClickChargezone() == "true") {
                           if (currentSpeedWeight() == undefined || currentSpeedWeight().length == 0) {
                               currentSpeedWeight.push(model.SpeedWeightLookup.create());
                               lookupMethodViewModel.SetLookupMethod(currentSpeedWeight(), 4, null);
                               ko.cleanNode($("#divlookupMethodBinding")[0]);
                               ko.applyBindings(ist.lookupMethods.view.viewModel, $("#divlookupMethodBinding")[0]);
                           }
                       } else {
                           if (currentClickChargeZone() == undefined || currentClickChargeZone().length == 0) {
                               currentClickChargeZone.push(model.NewClickChargeZoneLookup());
                               lookupMethodViewModel.SetLookupMethod(currentClickChargeZone(), 1, null);
                               ko.cleanNode($("#divlookupMethodBinding")[0]);
                               ko.applyBindings(ist.lookupMethods.view.viewModel, $("#divlookupMethodBinding")[0]);
                           }
                       }
                       
                   },
                    //Save EDIT Machine
                    saveEdittedMachine = function () {
                        //var zone = lookupMethodViewModel.oClickChargeZoneServerMapper();
                        
                        
                        
                        var zone = lookupMethodViewModel.selectedClickChargeZones();
                        var meter = lookupMethodViewModel.oMeterPerHour();
                        var guillotine = lookupMethodViewModel.oGuillotineZone();
                        var speedWeightLookup = null;
                        if (MachineType() == 4) {
                            var speedWeight = lookupMethodViewModel.selectedSpeedWeight();
                            speedWeightLookup = model.speedWeightconvertToServerData(speedWeight);
                        }
                        

                        
                        dataservice.saveMachine(
                           
                            model.machineServerMapper(selectedMachine(), lookupMethodViewModel.selectedClickChargeZones(), meter, guillotine, MachineType(), speedWeightLookup),
                            
                         {
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
                                        machine.isDigital(selectedMachine().isDigitalPress());
                                        machine.isSheetFed(selectedMachine().isSheetFed());
                                        //if (machine.LookupMethodId() != selectedMachine().LookupMethodId()) {
                                        //    _.each(selectedMachine().lookupList(), function (lookupItm) {
                                        //        if (lookupItm && lookupItm.MethodId == selectedMachine().LookupMethodId()) {
                                        //            machine.LookupMethodName(lookupItm.Name);
                                        //        }

                                        //    });
                                        //}

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
                    saveNewMachine = function (mClickChargeZone,mSelectedGuillotineClickCharge,mMeterPerHour,mType, speedWeight) {

                        dataservice.saveNewMachine(model.machineServerMapper(selectedMachine(), mClickChargeZone, mMeterPerHour, mSelectedGuillotineClickCharge, mType, speedWeight), {
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

                                lookupMethodViewModel.selectedClickChargeZones().reset();
                                lookupMethodViewModel.selectedMeterPerHourClickCharge().reset();
                                lookupMethodViewModel.selectedGuillotineClickCharge().reset();
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

                     // Delete a Markup
                    onDeleteLookup = function (lookup) {


                        machinelookups.remove(lookup);
                        _.each(machinelookups(), function (item) {
                            if ((item.id() === lookup.id())) {
                                machinelookups.remove(item);
                            }
                        });


                    },


                    onEditItem = function (oMachine) {
                        errorList.removeAll();
                        var machieId = oMachine.MachineId();
                        dataservice.getMachineById({
                            id: machieId,
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    selectedMachine(model.machineClientMapper(data, onCalculationMethodChange));
                                   // selectedMachine().lookupMethod(model.lookupMethod(data.machine.LookupMethod));
                                    //lookupMethodViewModel.GetSpeedWeightLookup(data.machine.LookupMethod.MachineSpeedWeightLookups[0]);
                                    //selectedMachine().lookupMethod().SpedWeightLookups[0](data.machine.LookupMethod);
                                    lookupMethodViewModel.CurrencySymbol(selectedMachine().CurrencySymbol());
                                    //$("#isSheetFedRadio").css("display", "none");
                                    
                                    var pagetype = Request.QueryString("type").toString();

                                    if (pagetype != null) {
                                        if (pagetype == 'press') {
                                            if (data.machine.IsDigitalPress != false)
                                                selectedMachine().isDigitalPress("true");
                                            else {
                                                selectedMachine().isDigitalPress("false");
                                            }
                                            if (data.machine.isSheetFed == true) {
                                                selectedMachine().isSheetFed("true");
                                                currentClickChargeZone.removeAll();
                                                currentSpeedWeight.removeAll();
                                                if (data.machine.LookupMethod.Type == 5) { // Click Charge Zone applied
                                                    MachineType(0);
                                                    selectedMachine().isClickChargezone("true");
                                                    lookupMethodViewModel.SetLookupMethod(data.machine.LookupMethod.MachineClickChargeZones, 1, null);
                                                    currentClickChargeZone.push(lookupMethodViewModel.selectedClickChargeZones());
                                                } else { //Speed Weight applied
                                                    MachineType(4);
                                                    selectedMachine().isClickChargezone("false");
                                                    var lookupArr = [];
                                                    var speedWeightLookup = model.SpeedWeightLookup(data.machine.LookupMethod.MachineSpeedWeightLookups[0]);
                                                    lookupArr.push(speedWeightLookup);
                                                    lookupMethodViewModel.SetLookupMethod(lookupArr, 4, null);
                                                    currentSpeedWeight.push(lookupMethodViewModel.selectedSpeedWeight());
                                                }
                                                
                                            }
                                            else
                                            {
                                                MachineType(1);
                                                lookupMethodViewModel.SetLookupMethod(data.machine.LookupMethod.MachineMeterPerHourLookups, 2, null);
                                            }
                                        }
                                        else {
                                            MachineType(2);
                                            lookupMethodViewModel.SetLookupMethod(data.machine.LookupMethod.MachineGuillotineCalcs, 3, data.GuilotinePtv);
                                        }
                                    }


                                    if (data.machine.isSheetFed == true) {
                                        $("#meterPerHour").css("display", "none");
                                        $("#clickChargeZoneSection").css("display", "block");


                                        
                                        //lookupMethodViewModel.isClickChargeZonesEditorVisible(true);
                                        //lookupMethodViewModel.isMeterPerHourClickChargeEditorVisible(false);

                                    }
                                    else {

                                        $("#meterPerHour").css("display", "block");
                                        $("#clickChargeZoneSection").css("display", "none");
                                        
                                        //lookupMethodViewModel.isClickChargeZonesEditorVisible(false);
                                        //lookupMethodViewModel.isMeterPerHourClickChargeEditorVisible(true);
                                    }

                                    if (data.machine.IsSpotColor == true) {
                                       
                                        selectedMachine().IsSpotColor("true");
                                    }
                                    else {

                                        selectedMachine().IsSpotColor("false");
                                    }



                                  

                                    selectedMachine().reset();
                                    showMachineDetail();
                                    subscribeMachineChange();

                                    //  lookupMethodViewModel.isClickChargeZonesEditorVisible(true);

                                    ////Machine lookups
                                    //machinelookups.removeAll();
                                    //var machinesLookupList = [];
                                    //_.each(data.MachineLookupMethods, function (item) {
                                    //    var lookupmethods = new model.MachineLookupClientMapper(item);
                                    //    machinesLookupList.push(lookupmethods);
                                    //});
                                    //ko.utils.arrayPushAll(machinelookups(), machinesLookupList);
                                    //machinelookups.valueHasMutated();


                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load Detail . Error: ");
                            }
                        });
                    },




                    onLookupMethodTabClick = function (oMachine) {

                      
                       

                        var pagetype = Request.QueryString("type").toString();

                        if (pagetype != null) {

                           
                            //if (oMachine.MachineId() > 0) {
                            //    lookupMethodViewModel.GetMachineLookupById(oMachine.LookupMethodId(), MachineType);
                            //}

                            if (pagetype == 'press') {


                                if (selectedMachine().isSheetFed() == "true" || selectedMachine().isSheetFed() == true) {
                                    if (selectedMachine().isClickChargezone() == "true" || selectedMachine().isClickChargezone() == true) {
                                        lookupMethodViewModel.isClickChargeZonesEditorVisible(true);
                                        lookupMethodViewModel.isMeterPerHourClickChargeEditorVisible(false);
                                        lookupMethodViewModel.isSpeedWeightVisible(false);
                                    } else {
                                        lookupMethodViewModel.isClickChargeZonesEditorVisible(false);
                                        lookupMethodViewModel.isMeterPerHourClickChargeEditorVisible(false);
                                        lookupMethodViewModel.isSpeedWeightVisible(true);
                                    }
                                    

                                    
                                }
                                else {
                                    lookupMethodViewModel.isMeterPerHourClickChargeEditorVisible(true);
                                    lookupMethodViewModel.isClickChargeZonesEditorVisible(false);
                                    lookupMethodViewModel.isSpeedWeightVisible(false);
                                }


                            }
                            else if (pagetype == 'guillotine') {
                                lookupMethodViewModel.isGuillotineClickChargeEditorVisible(true);
                                lookupMethodViewModel.isClickChargeZonesEditorVisible(false);
                                lookupMethodViewModel.isMeterPerHourClickChargeEditorVisible(false);
                                lookupMethodViewModel.isSpeedWeightVisible(false);

                            }

                        }


                    },





                     //onCreateNewMachineLookupMethodId = function () {
                     //    var lookups = machinelookups()[0];
                     //    if ((machinelookups().length === 0) || (lookups !== undefined && lookups !== null && lookups.MachineId() !== undefined && lookups.MethodId() !== undefined && lookups.isValid())) {
                     //        //var newMarkup = model.Markup();
                     //        //newMarkup.id(idCounter() - 1);
                     //        //newMarkup.rate(0);
                     //        ////New Id
                     //        //idCounter(idCounter() - 1);
                     //        //markups.splice(0, 0, newMarkup);
                     //        //filteredMarkups.splice(0, 0, newMarkup);
                     //        //selectedMarkup(filteredMarkups()[0]);
                     //        //selectedMyOrganization().flagForChanges("Changes occur");
                     //    }
                     //    if (lookups !== undefined && lookups !== null && !lookups.isValid()) {
                     //        lookups.errors.showAllMessages();
                     //    }
                     //},


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
                    isLookupMethodInitialize = false,
                    showMachineDetail = function () {

                        //if (!isLookupMethodInitialize)
                        //{
                            ko.cleanNode($("#divlookupMethodBinding")[0]);
                            ko.applyBindings(ist.lookupMethods.view.viewModel, $("#divlookupMethodBinding")[0]);
                            isLookupMethodInitialize = true;
                       // }
                        
                            isEditorVisible(true);
                        
                        view.initializeLabelPopovers();
                    },
                     // #region Observables
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        isGuillotineList(false);
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);

                        
                        pager(pagination.Pagination({ PageSize: 10 }, machineList, getMachines));

                        var pagetype = Request.QueryString("type").toString();

                        if (pagetype != null) {
                            if (pagetype == 'press') {
                                MachineName("Press Name");
                                isGuillotineList(false);
                                getMachines();
                                
                                 
                                $("#isSheetFedRadio").css("display", "block");
                            }
                            else if (pagetype == 'guillotine')
                            {
                                MachineName("Guillotine Name");
                                isGuillotineList(true);
                                getMachines();
                                $("#isSheetFedRadio").css("display", "none");
                            }
                        }
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
                    stockItemgPager: stockItemgPager,
                    makeEditable: makeEditable,
                    getMachines: getMachines,
                    doBeforeSave: doBeforeSave,
                    saveMachine: saveMachine,
                    errorList: errorList,
                    saveEdittedMachine: saveEdittedMachine,

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
                    onismakereadyusedChange: onismakereadyusedChange,
                    oniswashupusedChange: oniswashupusedChange,
                    createNewMachine: createNewMachine,
                    saveNewMachine: saveNewMachine,
                    machinelookups: machinelookups,
                    onDeleteLookup: onDeleteLookup,
                    // onCreateNewMachineLookupMethodId: onCreateNewMachineLookupMethodId,
                    MachineName: MachineName,
                    lookupMethodViewModel: lookupMethodViewModel,
                    onLookupMethodTabClick: onLookupMethodTabClick,
                    machinehasChanges: machinehasChanges,
                    LookupMethodHasChange: LookupMethodHasChange,
                    currentSpeedWeight: currentSpeedWeight,
                    currentClickChargeZone: currentClickChargeZone
            };
            })()
        };
        return ist.machine.viewModel;
    });
