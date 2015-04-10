/*
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
                    deliverycarrierlist = ko.observableArray([]),
                    errorList = ko.observableArray([]),
                    ////pagervariables
                    //pager = ko.obervable(),
                    //sortOn = ko.observable(1),
                    //sortOnAsc = ko.observable(true),
                    // #region Busy Indicators
                    editorViewModel = new ist.ViewModel(model.DeliveryCarrier),
                    //Selected Paper Sheet
                    selectedCarrier = editorViewModel.itemForEditing,
                    isLoadingdeliverycarrier = ko.observable(false),

                createNewDeliveryDialog = function ()
                {
                    var deliverycarrier = new model.DeliveryCarrier();
                    editorViewModel.selectItem(deliverycarrier);
                    openDialog();
                },
                openDialog = function ()
                {
                    view.showDeliveryCarrierDialog();
                },
                // #endregion Busy Indicators
                // #region Observables
                // Initialize the view model
                initialize = function (specifiedView)
                {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    //pager(pagination.Pagination({ PageSize: 10 }, deliverycarrierlist, getPaperSheets));
                    getBase();
                },
                //Get DeliveryCarrier
                getBase = function (callBack) {
                    isLoadingdeliverycarrier(true);
                    dataservice.getDeliveryCarrierDetail({
                        success: function (data)
                        {
                            if (data != null)
                            {
                                deliverycarrierlist.removeAll();
                                _.each(data, function (item)
                                {
                                    var module = model.deliverycarrierClientMapper(item);
                                    deliverycarrierlist.push(module);
                                });
                            }

                            isLoadingdeliverycarrier(false);
                        },
                        error: function () {
                            toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                        }
                    });
                },
               
                openEditDialog = function (item)
                {
                    if (item != null)
                    {
                        editorViewModel.selectItem(item);
                        openDialog();
                    }
                },
                onsaveDeliveryCarrier = function (item)
                {
                   saveDeliveryCarrier(item);
                },
                saveDeliveryCarrier = function (item) {
                    dataservice.saveDeliveryCarrier(model.deliverycarrierServermapper(item), {
                        success: function (data)
                        {
                            getBase();
                            toastr.success("Successfully save.");
                        },
                        error: function (exceptionMessage, exceptionType)
                        {
                            if (exceptionType === ist.exceptionType.MPCGeneralException)
                            {
                                toastr.error(exceptionMessage);
                            }
                            else
                            {
                                toastr.error("Failed to save.");
                            }

                        }
                    });
                };
                
                    
                // #endregion Service Calls
                return {
                    // Observables
                    deliverycarrierlist: deliverycarrierlist,
                    selectedCarrier: selectedCarrier,
                    createNewDeliveryDialog: createNewDeliveryDialog,
                    openEditDialog:openEditDialog,
                    errorList: errorList,
                    onsaveDeliveryCarrier: onsaveDeliveryCarrier,
                    // Utility Methods
                    initialize: initialize,
                    openDialog: openDialog
                    
                };
            })()
        };
        return ist.deliverycarrier.viewModel;
    });
