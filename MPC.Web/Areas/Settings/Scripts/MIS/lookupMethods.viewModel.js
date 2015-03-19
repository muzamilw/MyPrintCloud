define("lookupMethods/lookupMethods.viewModel",
    ["jquery", "amplify", "ko", "lookupMethods/lookupMethods.dataservice", "lookupMethods/lookupMethods.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.lookupMethods = {

            viewModel: (function () {
                var
                    view,
                    errorList = ko.observableArray([]),
                    isEditorVisible = ko.observable(),
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
                     initialize = function (specifiedView) {
                         view = specifiedView;
                         ko.applyBindings(view.viewModel, view.bindingRoot);

                         GetLookupList();
                     },
                GetLookupList = function () {

                    dataservice.GetLookupList({

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
                                        lookupClickCharge = model.lookupupListClientMapper(item);
                                    }
                                    else if (item.MethodId == 3) {
                                        lookupSpeedWeight = model.lookupupListClientMapper(item);
                                    } else if (item.MethodId == 4) {
                                        lookupPerHour = model.lookupupListClientMapper(item);
                                    } else if (item.MethodId == 5) {
                                        lookupClickChargeZones = model.lookupupListClientMapper(item);
                                    } else if (item.MethodId == 6) {
                                        lookupGuillotineClickCharge = model.lookupupListClientMapper(item);
                                    } else if (item.MethodId == 8) {
                                        lookupMeterPerHourClickCharge = model.lookupupListClientMapper(item);
                                    } else {
                                        var module = model.lookupupListClientMapper(item);
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
                }

                return {
                    initialize:initialize,
                    errorList: errorList,
                    isEditorVisible:isEditorVisible,
                    GetLookupList: GetLookupList,
                    lookupClickChargeList: lookupClickChargeList,
                    lookupSpeedWeightList: lookupSpeedWeightList,
                    lookupPerHourList: lookupPerHourList,
                    lookupClickChargeZonesList: lookupClickChargeZonesList,
                    lookupGuillotineClickChargeList: lookupGuillotineClickChargeList,
                    lookupMeterPerHourClickChargeList: lookupMeterPerHourClickChargeList
                }
            })()
        };
        return ist.lookupMethods.viewModel;

    });
