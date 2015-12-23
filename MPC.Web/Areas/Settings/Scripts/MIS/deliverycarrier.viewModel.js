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
                    // #region Busy Indicators
                    isLoadingdeliverycarrier = ko.observable(false),
                    editorViewModel = new ist.ViewModel(model.DeliveryCarrier),
                    //Selected Paper Sheet
                    selectedCarrier = editorViewModel.itemForEditing,

                   

                createNewDeliveryDialog = function ()
                {
                    var deliverycarrier = new model.DeliveryCarrier();
                    editorViewModel.selectItem(deliverycarrier);
                    selectedCarrier().readonly(true);

                    openDialog();
                },
                openDialog = function ()
                {
                    view.showDeliveryCarrierDialog();
                },

                //Get DeliveryCarrier
                getBase = function (callBack) {
                    getDeliveryCarrierDetail();
                },
                getDeliveryCarrierDetail = function (callback) {
                    isLoadingdeliverycarrier(true);
                    dataservice.getDeliveryCarrierDetail
                    ({
                       success: function (data) {
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
                openEditDialog = function (item) {
                    if (item != null)
                    {
                        editorViewModel.selectItem(item);
                        openDialog();
                    }
                    
                },
                onsaveDeliveryCarrier = function ()
                {
                    if (selectedCarrier() != undefined && doBeforeSave())
                    { 
                        saveDeliveryCarrier();
                        view.hideDeliveryCarrierDialog();
                    }
                },
                saveDeliveryCarrier = function () {
                    dataservice.saveDeliveryCarrier(model.deliverycarrierServermapper(selectedCarrier()),
                    {
                        success: function (data)
                        {
                            getBase();
                            toastr.success("Successfully save.");
                        },
                        error: function (exceptionMessage, exceptionType) {
                            if (exceptionType === ist.exceptionType.MPCGeneralException) {
                                toastr.error(exceptionMessage);
                            }
                            else
                            {
                                toastr.error("Failed to save.");
                            }

                        }
                    });
                },
                 doBeforeSave = function () {
                     var flag = true;
                     if (!selectedCarrier().isValid())
                     {
                         selectedCarrier().showErrors(true);
                         flag = false;
                     }
                     return flag;
                 },
                  onCloseDeliveryCarrier = function () {
                      if (selectedCarrier().hasChanges())
                      {
                          confirmation.messageText("Do you want to save changes?");
                          confirmation.afterProceed(function(){

                              if (selectedCarrier().isValid())
                              {
                                  saveDeliveryCarrier();
                                  view.hideDeliveryCarrierDialog();
                              }
                              else if(!selectedCarrier().isValid()) 
                              {
                                  doBeforeSave();
                              }
                             
                          });
                          confirmation.afterCancel(function (){
                              view.hideDeliveryCarrierDialog();
                          });
                          confirmation.show();
                          return;
                      }
                      view.hideDeliveryCarrierDialog();
                  },

                  initialize = function (specifiedView)
                  {
                      view = specifiedView;
                      ko.applyBindings(view.viewModel, view.bindingRoot);
                      getBase();
                     
                  };

                return {
                    // Observables
                    
                    deliverycarrierlist: deliverycarrierlist,
                    selectedCarrier: selectedCarrier,
                    createNewDeliveryDialog: createNewDeliveryDialog,
                    //Dialog Boxes
                    openEditDialog: openEditDialog,
                    onCloseDeliveryCarrier:onCloseDeliveryCarrier,
                    errorList: errorList,
                    onsaveDeliveryCarrier: onsaveDeliveryCarrier,
                    // Utility Methods
                    initialize: initialize,
                    openDialog: openDialog,
                    
                    
                };
            })()
        };
        return ist.deliverycarrier.viewModel;
    });
