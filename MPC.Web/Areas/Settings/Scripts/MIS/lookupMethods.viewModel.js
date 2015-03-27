define("lookupMethods/lookupMethods.viewModel",
    ["jquery", "amplify", "ko", "lookupMethods/lookupMethods.dataservice", "lookupMethods/lookupMethods.model", "common/confirmation.viewModel", "common/sharedNavigation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, sharedNavigationVM) {
        var ist = window.ist || {};
        ist.lookupMethods = {

            viewModel: (function () {
                var
                    view,
                    errorList = ko.observableArray([]),
                   // isEditorVisible = ko.observable(false),
                    lookupClickCharge = ko.observable(),
                    lookupSpeedWeight = ko.observable(),
                    lookupPerHour = ko.observable(),
                    lookupClickChargeZones = ko.observable(),
                    lookupGuillotineClickCharge = ko.observable(),
                    lookupMeterPerHourClickCharge = ko.observable(),
                    lookupClickChargeList = ko.observableArray([]),
                    lookupSpeedWeightList = ko.observableArray([]),
                    lookupPerHourList = ko.observableArray([]),
                    lookupClickChargeZonesList = ko.observableArray([]),
                    lookupGuillotineClickChargeList = ko.observableArray([]),
                    lookupMeterPerHourClickChargeList = ko.observableArray([]),
                    
                    selectedlookup = ko.observable(),
                    selectedClickCharge = ko.observable(),
                    selectedSpeedWeight = ko.observable(),
                    selectedPerHour = ko.observable(),
                    selectedClickChargeZones = ko.observable(),
                    selectedGuillotineClickCharge = ko.observable(),
                    selectedMeterPerHourClickCharge = ko.observable(),
                    isClickChargeEditorVisible = ko.observable(),
                    isSpeedWeightEditorVisible = ko.observable(),
                    isPerHourEditorVisible = ko.observable(),
                    isClickChargeZonesEditorVisible = ko.observable(),
                    isGuillotineClickChargeEditorVisible = ko.observable(),
                    isMeterPerHourClickChargeEditorVisible = ko.observable(),
                    IsSelected = ko.observable(),
                     initialize = function (specifiedView) {
                         view = specifiedView;
                         ko.applyBindings(view.viewModel, view.bindingRoot);

                         GetLookupList();
                     },
                     AddGuiltineLookup = function () {
                         selectedGuillotineClickCharge().GuillotinePTVList.push(model.GuillotineClickPTV());
                     }
                DeleteLookup = function (olookup) {
                    if (!(selectedlookup().MethodId() > 0)) {
                        selectedGuillotineClickCharge().GuillotinePTVList.remove(oGuillotinePTV);


                        if (selectedlookup().Type() == 1) {
                            lookupClickChargeList.remove(olookup);
                        }
                        else if (selectedlookup().Type() == 3) {
                            lookupSpeedWeightList.remove(olookup);
                        } else if (selectedlookup().Type() == 4) {
                            lookupPerHourList.remove(olookup);
                        } else if (selectedlookup().Type() == 5) {
                            lookupClickChargeZonesList.remove(olookup);
                        } else if (selectedlookup().Type() == 6) {
                            lookupGuillotineClickChargeList.remove(olookup);
                        } else if (selectedlookup().Type() == 8) {
                            lookupMeterPerHourClickChargeList.remove(olookup);
                        }


                        return;
                    }
                    // Ask for confirmation

                    confirmation.messageText("Do you want to Detele this Item?");
                    confirmation.afterProceed(function () {
                        dataservice.deleteLookup({
                            LookupMethodId: selectedlookup().MethodId()
                        },
                        {
                            success: function (data) {
                                if (data) {


                                    if (selectedlookup().Type() == 1) {
                                        lookupClickChargeList.remove(selectedlookup());
                                    }
                                    else if (selectedlookup().Type() == 3) {
                                        lookupSpeedWeightList.remove(selectedlookup());
                                    } else if (selectedlookup().Type() == 4) {
                                        lookupPerHourList.remove(selectedlookup());
                                    } else if (selectedlookup().Type() == 5) {
                                        lookupClickChargeZonesList.remove(selectedlookup());
                                    } else if (selectedlookup().Type() == 6) {
                                        lookupGuillotineClickChargeList.remove(selectedlookup());
                                    } else if (selectedlookup().Type() == 8) {
                                        lookupMeterPerHourClickChargeList.remove(selectedlookup());
                                    }
                                    errorList.removeAll();
                                    selectedSpeedWeight(null);
                                    selectedClickCharge(null);
                                    selectedClickChargeZones(null);
                                    selectedMeterPerHourClickCharge(null);
                                    selectedPerHour(null);
                                    selectedGuillotineClickCharge(null);
                                    isClickChargeEditorVisible(false);
                                    isSpeedWeightEditorVisible(false);
                                    isPerHourEditorVisible(false);
                                    isClickChargeZonesEditorVisible(false);
                                    isGuillotineClickChargeEditorVisible(false);
                                    isMeterPerHourClickChargeEditorVisible(false);
                                    IsSelected(false);
                                    toastr.success(" Deleted Successfully !");
                                } else {
                                    toastr.error("Failed to Delete Lookup");
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Delete Lookup" + response);
                            }
                        });
                    });
                    confirmation.afterCancel(function () {
                        //navigateToUrl(element);
                    });
                    confirmation.show();




                }


                DeleteGuillotinePTV = function (oGuillotinePTV) {
                    if (!(oGuillotinePTV.Id()) > 0) {
                        selectedGuillotineClickCharge().GuillotinePTVList.remove(oGuillotinePTV);
                        return;
                    }
                    // Ask for confirmation

                    confirmation.messageText("Do you want to Detele this Item?");
                    confirmation.afterProceed(function () {
                        dataservice.deleteLookup({
                            GuillotinePTVId: oGuillotinePTV.Id()
                        },
                        {
                            success: function (data) {
                                selectedGuillotineClickCharge().GuillotinePTVList.remove(oGuillotinePTV);

                                //_.each(selectedGuillotineClickCharge().GuillotinePTVList(), function (item) {
                                //    item.reset;
                                //});
                                selectedGuillotineClickCharge().reset;
                                toastr.success(" Deleted Successfully !");

                            },
                            error: function (response) {
                                toastr.error("Failed to Delete Item" + response);
                            }
                        });
                    });
                    confirmation.afterCancel(function () {
                        //navigateToUrl(element);
                    });
                    confirmation.show();




                }
                AddLookup = function (Id) {
                    selectedlookup(null);
                    selectedSpeedWeight(null);
                    selectedClickCharge(null);
                    selectedClickChargeZones(null);
                    selectedMeterPerHourClickCharge(null);
                    selectedPerHour(null);
                    selectedGuillotineClickCharge(null);
                    isClickChargeEditorVisible(false);
                    isSpeedWeightEditorVisible(false);
                    isPerHourEditorVisible(false);
                    isClickChargeZonesEditorVisible(false);
                    isGuillotineClickChargeEditorVisible(false);
                    isMeterPerHourClickChargeEditorVisible(false);
                    selectedlookup(model.lookupMethod());
                    if (Id == 1) {
                        isClickChargeEditorVisible(true);
                        selectedClickCharge(model.ClickChargeLookup());
                        selectedlookup().Type(1);
                        sharedNavigationVM.initialize(selectedClickCharge, function (saveCallback) { saveLookup(saveCallback); });
                    } else if (Id == 3) {
                        isSpeedWeightEditorVisible(true);
                        selectedSpeedWeight(model.SpeedWeightLookup());
                        sharedNavigationVM.initialize(selectedSpeedWeight, function (saveCallback) { saveLookup(saveCallback); });
                        selectedlookup().Type(3);
                    } else if (Id == 4) {
                        isPerHourEditorVisible(true);
                        selectedPerHour(model.PerHourLookup());
                        selectedlookup().Type(4);
                        sharedNavigationVM.initialize(selectedPerHour, function (saveCallback) { saveLookup(saveCallback); });
                    } else if (Id == 5) {
                        isClickChargeZonesEditorVisible(true);
                        selectedClickChargeZones(model.ClickChargeZone());
                        selectedlookup().Type(5);
                        sharedNavigationVM.initialize(selectedClickChargeZones, function (saveCallback) { saveLookup(saveCallback); });
                    } else if (Id == 6) {
                        isGuillotineClickChargeEditorVisible(true);
                        selectedGuillotineClickCharge(model.GuillotineCalc());
                       // selectedGuillotineClickCharge().GuillotinePTVList.removeAll();
                        selectedlookup().Type(6);
                        sharedNavigationVM.initialize(selectedGuillotineClickCharge, function (saveCallback) { saveLookup(saveCallback); });
                    } else if (Id == 8) {
                        isMeterPerHourClickChargeEditorVisible(true);
                        selectedMeterPerHourClickCharge(model.MeterPerHourLookup());
                        selectedlookup().Type(8);
                        sharedNavigationVM.initialize(selectedMeterPerHourClickCharge, function (saveCallback) { saveLookup(saveCallback); });
                    }



                }
                GetLookupList = function () {

                    dataservice.GetLookup({

                    }, {
                        success: function (data) {
                            lookupClickChargeList.removeAll();
                            lookupSpeedWeightList.removeAll();
                            lookupPerHourList.removeAll();
                            lookupClickChargeZonesList.removeAll();
                            lookupGuillotineClickChargeList.removeAll();
                            lookupMeterPerHourClickChargeList.removeAll();

                            if (data != null) {

                                _.each(data, function (item) {

                                    if (item.MethodId == 1) {
                                        lookupClickCharge(item.Name); // model.lookupupListClientMapper(item);
                                    }
                                    else if (item.MethodId == 3) {
                                        lookupSpeedWeight(item.Name); // model.lookupupListClientMapper(item);
                                    } else if (item.MethodId == 4) {
                                        lookupPerHour(item.Name); // model.lookupupListClientMapper(item);
                                    } else if (item.MethodId == 5) {
                                        lookupClickChargeZones(item.Name); // model.lookupupListClientMapper(item);
                                    } else if (item.MethodId == 6) {
                                        lookupGuillotineClickCharge(item.Name); // model.lookupupListClientMapper(item);
                                    } else if (item.MethodId == 8) {
                                        lookupMeterPerHourClickCharge(item.Name); // model.lookupupListClientMapper(item);
                                    } else {
                                        var module = model.lookupClientMapper(item);
                                        if (module.Type() == 1) {
                                            lookupClickChargeList.push(module);
                                        }
                                        else if (module.Type() == 3) {
                                            lookupSpeedWeightList.push(module);
                                        } else if (module.Type() == 4) {
                                            lookupPerHourList.push(module);
                                        } else if (module.Type() == 5) {
                                            lookupClickChargeZonesList.push(module);
                                        } else if (module.Type() == 6) {
                                            lookupGuillotineClickChargeList.push(module);
                                        } else if (module.Type() == 8) {
                                            lookupMeterPerHourClickChargeList.push(module);
                                        }




                                    }



                                });
                            }

                        },
                        error: function (response) {

                            toastr.error("Error: Failed to Load Lookup List Data." + response);
                        }
                    });
                },
                GetMachineLookupById = function (olookup) {


                    if (isClickChargeEditorVisible() || isSpeedWeightEditorVisible() || isPerHourEditorVisible() || isClickChargeZonesEditorVisible() || isGuillotineClickChargeEditorVisible() || isMeterPerHourClickChargeEditorVisible()) {

                        return oncloseEditor(olookup);

                    } else {
                        onCancal(olookup);

                    }









                },
                onCancal = function (olookup) {
                    selectedSpeedWeight(null);
                    selectedClickCharge(null);
                    selectedClickChargeZones(null);
                    selectedMeterPerHourClickCharge(null);
                    selectedPerHour(null);
                    selectedGuillotineClickCharge(null);
                    isClickChargeEditorVisible(false);
                    isSpeedWeightEditorVisible(false);
                    isPerHourEditorVisible(false);
                    isClickChargeZonesEditorVisible(false);
                    isGuillotineClickChargeEditorVisible(false);
                    isMeterPerHourClickChargeEditorVisible(false);
                    IsSelected(false);
                    dataservice.GetLookup({
                        MethodId: olookup.MethodId(),
                    }, {
                        success: function (data) {
                            selectedlookup(olookup);
                            IsSelected(true);
                            if (data.ClickChargeLookup != null) {
                                isClickChargeEditorVisible(true);

                                selectedClickCharge(model.ClickChargeLookup(data.ClickChargeLookup));
                                sharedNavigationVM.initialize(selectedClickCharge, function (saveCallback) { saveLookup(saveCallback); });
                            } else if (data.ClickChargeZone != null) {
                                isClickChargeZonesEditorVisible(true);
                                selectedClickChargeZones(model.ClickChargeZone(data.ClickChargeZone));
                                sharedNavigationVM.initialize(selectedClickChargeZones, function (saveCallback) { saveLookup(saveCallback); });
                            } else if (data.GuillotineCalc != null) {
                                isGuillotineClickChargeEditorVisible(true);
                                selectedGuillotineClickCharge(model.GuillotineCalc(data.GuillotineCalc, data.GuilotinePtv));
                                sharedNavigationVM.initialize(selectedGuillotineClickCharge, function (saveCallback) { saveLookup(saveCallback); });
                               
                            } else if (data.MeterPerHourLookup != null) {
                                isMeterPerHourClickChargeEditorVisible(true);
                                selectedMeterPerHourClickCharge(model.MeterPerHourLookup(data.MeterPerHourLookup));
                                sharedNavigationVM.initialize(selectedMeterPerHourClickCharge, function (saveCallback) { saveLookup(saveCallback); });
                            } else if (data.PerHourLookup != null) {
                                isPerHourEditorVisible(true);
                                selectedPerHour(model.PerHourLookup(data.PerHourLookup));
                                sharedNavigationVM.initialize(selectedPerHour, function (saveCallback) { saveLookup(saveCallback); });
                            } else if (data.SpeedWeightLookup != null) {
                                isSpeedWeightEditorVisible(true);
                                selectedSpeedWeight(model.SpeedWeightLookup(data.SpeedWeightLookup));
                                sharedNavigationVM.initialize(selectedSpeedWeight, function (saveCallback) { saveLookup(saveCallback); });

                            }

                        },
                        error: function (response) {

                            toastr.error("Error: Failed to Load Lookup List Data." + response);
                        }
                    });
                }

                oncloseEditor = function (olookup) {
                    if (selectedSpeedWeight() != null && selectedSpeedWeight().hasChanges() || selectedClickCharge() != null && selectedClickCharge().hasChanges() || selectedClickChargeZones() != null && selectedClickChargeZones().hasChanges() || selectedMeterPerHourClickCharge() != null && selectedMeterPerHourClickCharge().hasChanges() || selectedPerHour() != null && selectedPerHour().hasChanges() || selectedGuillotineClickCharge() != null && selectedGuillotineClickCharge().hasChanges()) {

                        confirmation.messageText("Do you want to save changes?");
                        confirmation.afterProceed(saveLookup);
                        confirmation.afterCancel(function () {
                            //selectedSpeedWeight().reset();
                            onCancal(olookup);
                            return true;
                        });
                        confirmation.show();
                        return false;
                    } else {
                        onCancal(olookup);
                    }

                },

                doBeforeSave = function () {
                    var flag = true;
                    if (!selectedlookup().isValid()) {
                        selectedlookup().errors.showAllMessages();
                        setValidationSummary(selectedlookup());
                        flag = false;
                    }

                    return flag;
                },
                saveLookup = function () {
                    if (selectedlookup() != undefined && doBeforeSave()) {
                        if (selectedlookup().MethodId() > 0) {
                            saveEdittedLookup();
                        }
                        else {

                            saveNewLookup();
                        }
                    }
                },
                saveEdittedLookup = function () {

                    dataservice.saveLookup(model.lookupServerMapper(selectedlookup(), selectedClickCharge(), selectedClickChargeZones(), selectedSpeedWeight(), selectedPerHour(), selectedMeterPerHourClickCharge(), selectedGuillotineClickCharge(), selectedGuillotineClickCharge()!=null?selectedGuillotineClickCharge().GuillotinePTVList():null), {
                        success: function (data) {
                            errorList.removeAll();
                            selectedSpeedWeight(null);
                            selectedClickCharge(null);
                            selectedClickChargeZones(null);
                            selectedMeterPerHourClickCharge(null);
                            selectedPerHour(null);
                            selectedGuillotineClickCharge(null);
                            isClickChargeEditorVisible(false);
                            isSpeedWeightEditorVisible(false);
                            isPerHourEditorVisible(false);
                            isClickChargeZonesEditorVisible(false);
                            isGuillotineClickChargeEditorVisible(false);
                            isMeterPerHourClickChargeEditorVisible(false);
                            IsSelected(false);
                            toastr.success("Successfully Saved.");

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
                saveNewLookup = function () {

                    dataservice.saveNewLookup(model.lookupServerMapper(selectedlookup(), selectedClickCharge(), selectedClickChargeZones(), selectedSpeedWeight(), selectedPerHour(), selectedMeterPerHourClickCharge(), selectedGuillotineClickCharge(), selectedGuillotineClickCharge() != null ? selectedGuillotineClickCharge().GuillotinePTVList() : null), {
                        success: function (data) {
                            errorList.removeAll();
                            selectedSpeedWeight(null);
                            selectedClickCharge(null);
                            selectedClickChargeZones(null);
                            selectedMeterPerHourClickCharge(null);
                            selectedPerHour(null);
                            selectedGuillotineClickCharge(null);
                            isClickChargeEditorVisible(false);
                            isSpeedWeightEditorVisible(false);
                            isPerHourEditorVisible(false);
                            isClickChargeZonesEditorVisible(false);
                            isGuillotineClickChargeEditorVisible(false);
                            isMeterPerHourClickChargeEditorVisible(false);
                            IsSelected(false);
                            if (data != null) {
                                var module = model.lookupClientMapper(data);
                                if (module.Type() == 1) {
                                    lookupClickChargeList.push(module);
                                }
                                else if (module.Type() == 3) {
                                    lookupSpeedWeightList.push(module);
                                } else if (module.Type() == 4) {
                                    lookupPerHourList.push(module);
                                } else if (module.Type() == 5) {
                                    lookupClickChargeZonesList.push(module);
                                } else if (module.Type() == 6) {
                                    lookupGuillotineClickChargeList.push(module);
                                } else if (module.Type() == 8) {
                                    lookupMeterPerHourClickChargeList.push(module);
                                }

                            }

                            toastr.success("Successfully Saved.");

                        },
                        error: function (exceptionMessage, exceptionType) {
                            if (exceptionType === ist.exceptionType.MPCGeneralException) {
                                toastr.error(exceptionMessage);
                            } else {
                                toastr.error("Failed to save.");
                            }
                        }
                    });
                }

                return {
                    initialize: initialize,
                    errorList: errorList,

                    GetLookupList: GetLookupList,
                    lookupClickCharge: lookupClickCharge,
                    lookupSpeedWeight: lookupSpeedWeight,
                    lookupPerHour: lookupPerHour,
                    lookupClickChargeZones: lookupClickChargeZones,
                    lookupGuillotineClickCharge: lookupGuillotineClickCharge,
                    lookupMeterPerHourClickCharge: lookupMeterPerHourClickCharge,
                    lookupClickChargeList: lookupClickChargeList,
                    lookupSpeedWeightList: lookupSpeedWeightList,
                    lookupPerHourList: lookupPerHourList,
                    lookupClickChargeZonesList: lookupClickChargeZonesList,
                    lookupGuillotineClickChargeList: lookupGuillotineClickChargeList,
                    lookupMeterPerHourClickChargeList: lookupMeterPerHourClickChargeList,
                    isClickChargeEditorVisible: isClickChargeEditorVisible,
                    GetMachineLookupById: GetMachineLookupById,
                    selectedClickCharge: selectedClickCharge,
                    selectedlookup: selectedlookup,
                    selectedSpeedWeight: selectedSpeedWeight,
                    selectedPerHour: selectedPerHour,
                    selectedClickChargeZones: selectedClickChargeZones,
                    selectedGuillotineClickCharge: selectedGuillotineClickCharge,
                    selectedMeterPerHourClickCharge: selectedMeterPerHourClickCharge,
                    saveLookup: saveLookup,
                    doBeforeSave: doBeforeSave,
                    saveEdittedLookup: saveEdittedLookup,
                    saveNewLookup: saveNewLookup,
                    isSpeedWeightEditorVisible: isSpeedWeightEditorVisible,
                    isPerHourEditorVisible: isPerHourEditorVisible,
                    isClickChargeZonesEditorVisible: isClickChargeZonesEditorVisible,
                    isGuillotineClickChargeEditorVisible: isGuillotineClickChargeEditorVisible,
                    isMeterPerHourClickChargeEditorVisible: isMeterPerHourClickChargeEditorVisible,
                    
                    AddLookup: AddLookup,
                    AddGuiltineLookup: AddGuiltineLookup,
                    DeleteGuillotinePTV: DeleteGuillotinePTV,
                    IsSelected: IsSelected,
                    DeleteLookup: DeleteLookup,
                    oncloseEditor: oncloseEditor,
                    onCancal: onCancal
                }
            })()
        };
        return ist.lookupMethods.viewModel;

    });
