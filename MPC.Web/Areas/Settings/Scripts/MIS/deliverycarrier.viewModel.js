﻿/*
    Module with the view model for the My Organization.
*/
define("deliverycarrier/deliverycarrier.viewModel",
    ["jquery", "amplify", "ko", "deliverycarrier/deliverycarrier.dataservice", "deliverycarrier/deliverycarrier.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.deliverycarrier = {
            viewModel: (function () {
                var // the view 
                    view,
                    // Active
                    deliverycarrierlist = ko.observableArray([]);
                errorList = ko.observableArray([]),
                deliveryselectedItemDetail = ko.observable(),
                // #region Busy Indicators
                isLoadingdeliverycarrier = ko.observable(false),
                createNewDelivery = function()
                {
                    openDialog();
                },
                openDialog = function()
                {
                    view.showDeliveryCarrierDialog();
                }
               
                // #endregion Busy Indicators
                // #region Observables
                // Initialize the view model
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    getBase();


                },
                //Get DeliveryCarrier
                getBase = function (callBack) {
                    isLoadingdeliverycarrier(true);
                    dataservice.getDeliveryCarrierDetail({
                        success: function (data) {
                            // getPrefixByOrganisationId();
                            if (data != null) {

                                deliverycarrierlist.removeAll();
                                ko.utils.arrayPushAll(deliverycarrierlist(), data);
                                deliverycarrierlist.valueHasMutated();// Use When you Push All Data at One Time
                            }

                            isLoadingdeliverycarrier(false);
                        },
                        error: function () {
                            toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                        }
                    });
                },
                onsaveDeliveryCarrier = function (deliverycarrier) {
                    saveDeliveryCarrier(deliverycarrier);
                },
                saveDeliveryCarrier = function (deliverycarrier) {
                    dataservice.saveDeliveryCarrier(model.deliverycarrier(deliverycarrier), {
                        success: function (data) {
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
               openEditDialog = function (item) {
                   if (item != null) {
                       deliveryselectedItemDetail(model.deliverycarrierClientMapper(item));
                       openDialog();
                       //view.showDeliveryCarrierDialog();
                   }
               };
                    
                // #endregion Service Calls
                return {
                    // Observables
                    deliverycarrierlist: deliverycarrierlist,
                    deliveryselectedItemDetail:deliveryselectedItemDetail,
                    onsaveDeliveryCarrier: onsaveDeliveryCarrier,
                    openEditDialog:openEditDialog,
                    errorList: errorList,
                    // Utility Methods
                    initialize: initialize,
                    openDialog: openDialog
                    
                };
            })()
        };
        return ist.deliverycarrier.viewModel;
    });
