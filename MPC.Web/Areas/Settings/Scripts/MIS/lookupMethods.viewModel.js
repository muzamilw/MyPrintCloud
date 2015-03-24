define("lookupMethods/lookupMethods.viewModel",
    ["jquery", "amplify", "ko", "lookupMethods/lookupMethods.dataservice", "lookupMethods/lookupMethods.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
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
                    GuillotinePTVList = ko.observableArray([]),
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
                     initialize = function (specifiedView) {
                         view = specifiedView;
                         ko.applyBindings(view.viewModel, view.bindingRoot);

                         GetLookupList();
                     },
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

                GetClickChargeById = function (olookup) {
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
                    dataservice.GetLookup({
                        MethodId: olookup.MethodId(),
                    }, {
                        success: function (data) {
                            selectedlookup(olookup);
                            if (data.ClickChargeLookup != null) {
                                isClickChargeEditorVisible(true);
                                selectedClickCharge(model.ClickChargeLookup(data.ClickChargeLookup));
                            } else if (data.ClickChargeZone != null) {
                                isClickChargeZonesEditorVisible(true);
                                selectedClickChargeZones(model.ClickChargeZone(data.ClickChargeZone));
                            } else if (data.GuillotineCalc != null) {
                                isGuillotineClickChargeEditorVisible(true);
                                selectedGuillotineClickCharge(model.GuillotineCalc(data.GuillotineCalc));
                            } else if (data.MeterPerHourLookup != null) {
                                isMeterPerHourClickChargeEditorVisible(true);
                                selectedMeterPerHourClickCharge(model.MeterPerHourLookup(data.MeterPerHourLookup));
                            } else if (data.PerHourLookup != null) {
                                isPerHourEditorVisible(true);
                                selectedPerHour(model.PerHourLookup(data.PerHourLookup));
                            } else if (data.SpeedWeightLookup != null) {
                                isSpeedWeightEditorVisible(true);
                                selectedSpeedWeight(model.SpeedWeightLookup(data.SpeedWeightLookup));

                            }

                        },
                        error: function (response) {

                            toastr.error("Error: Failed to Load Lookup List Data." + response);
                        }
                    });
                },
                 doBeforeSave = function () {
                     var flag = true;
                     if (!selectedlookup().isValid()) {
                         selectedlookup().errors.showAllMessages();
                         setValidationSummary(selectedlookup());
                         flag = false;
                     }
                     switch (selectedlookup.Type()) {
                         case 1:
                             if (!selectedClickCharge().isValid()) {
                                 selectedClickCharge().errors.showAllMessages();
                                 setValidationSummary(selectedClickCharge());
                                 flag = false;
                             }
                             break;
                     }
                     return flag;
                 },
                 saveLookup = function (item) {
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

                      dataservice.saveLookup(model.lookupServerMapper(selectedlookup(), selectedClickCharge(), selectedClickChargeZones(), selectedSpeedWeight(), selectedPerHour(), selectedMeterPerHourClickCharge(), selectedGuillotineClickCharge()), {
                          success: function (data) {
                              errorList.removeAll();
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

                        //dataservice.saveNewMachine(model.machineServerMapper(selectedMachine()), {
                        //    success: function (data) {
                        //        selectedMachine().reset();
                        //        errorList.removeAll();

                        //        selectedMachine().MachineId(data);
                        //        isEditorVisible(false);

                        //        toastr.success("Successfully save.");
                        //        var module = model.machineListClientMapperSelectedItem(selectedMachine());

                        //        _.each(selectedMachine().lookupList(), function (lookupItm) {
                        //            if (lookupItm && lookupItm.MethodId == selectedMachine().LookupMethodId()) {
                        //                module.LookupMethodName(lookupItm.Name);
                        //            }

                        //        });

                        //        machineList.push(module);


                        //    },
                        //    error: function (exceptionMessage, exceptionType) {
                        //        if (exceptionType === ist.exceptionType.MPCGeneralException) {
                        //            toastr.error(exceptionMessage);
                        //        } else {
                        //            toastr.error("Failed to save.");
                        //        }
                        //    }
                        //});
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
                    GetClickChargeById: GetClickChargeById,
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
                    GuillotinePTVList: GuillotinePTVList
                }
            })()
        };
        return ist.lookupMethods.viewModel;

    });
