/*
    Module with the view model for the My Organization.
*/
define("reportBanner/reportBanner.viewModel",
    ["jquery", "amplify", "ko", "reportBanner/reportBanner.dataservice", "reportBanner/reportBanner.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.reportNote = {
            viewModel: (function () {
                var // the view 
                    view,
                    // Active
                    SelectedCompany = ko.observable(),
                    SelectedReportNote = ko.observable(),
                    SelectOrderReportNote = ko.observable(),
                    SelectedInvoiceReportNote = ko.observable(),
                    SelectedPurchaseOrderReportNote = ko.observable(),
                    SelectedDeliveryReportNote = ko.observable(),
                    StoreName = ko.observable(),
                    errorList = ko.observableArray([]),
                    Stores = ko.observableArray([]),
                    CompanyReportNotes = ko.observableArray([]),
                    // #region Busy Indicators
                    isLoading = ko.observable(false),
                    
                    // #endregion Busy Indicators
                    // #region Observables
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        getBase();
                    },
                    //Get Prefix
                    getBase = function (callBack) {
                        isLoading(true);
                        dataservice.getStores({
                            success: function (data) {

                                if (callBack && typeof callBack === 'function') {
                                    callBack();
                                }
                                Stores.removeAll();
                                ko.utils.arrayPushAll(Stores(), data);
                                Stores.valueHasMutated();
                                getBannerByStoreId(Stores()[0]);
                                StoreName(Stores()[0].StoreName);
                            },
                            error: function () {
                                toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                            }
                        });
                    },
                      ImageFilesLoadedCallback = function (file, data) {
                          SelectedReportNote().estimateBannerBytes(data);
                    
                      },
                       ImageFilesOrderLoadedCallback = function (file, data) {
                           SelectOrderReportNote().orderBannerBytes(data);

                       },
                        ImageFilesInvoiceLoadedCallback = function (file, data) {
                            SelectedInvoiceReportNote().invoiceBannerBytes(data);

                        },
                         ImageFilesPurchaseLoadedCallback = function (file, data) {
                             SelectedPurchaseOrderReportNote().purchaseBannerBytes(data);

                         },
                          ImageFilesDeliveryLoadedCallback = function (file, data) {
                              SelectedDeliveryReportNote().deliveryBannerBytes(data);

                          },

                      checkStoreBannerUpload = function (store) {
                          if ((SelectedReportNote().estimateBannerBytes() != null) || (SelectOrderReportNote().orderBannerBytes() != null) || (SelectedInvoiceReportNote().invoiceBannerBytes() != null) || (SelectedPurchaseOrderReportNote().purchaseBannerBytes() != null) || (SelectedDeliveryReportNote().deliveryBannerBytes() != null)) {
                              confirmation.messageText("Are you sure you want to discard banners uploaded for previous store?");
                              confirmation.afterProceed(function () {
                                  getBannerByStoreId(store);

                              });
                              confirmation.afterCancel(function () {
                                  return;
                              });
                              confirmation.show();
                              return;
                            //  getBannerByStoreId(store);
                          }
                          else
                          {
                              getBannerByStoreId(store);
                          }
                              
                      },

                    // get banner by id
                    getBannerByStoreId = function (store) {
                        StoreName(store.StoreName);
                        isLoading(true);
                        var id = store.StoreID;
                        dataservice.getReportNote(
                            { CompanyID: id },
                            {
                                success: function (data) {
                                    
                                    _.each(data, function (item) {
                                        if (item.ReportCategoryId == 3) {
                                            SelectedReportNote(model.ReportNote.Create(item));
                                        }
                                        else if (item.ReportCategoryId == 12) {
                                            SelectOrderReportNote(model.ReportNote.Create(item));
                                        }
                                        else if (item.ReportCategoryId == 13) {
                                            SelectedInvoiceReportNote(model.ReportNote.Create(item));
                                        }
                                        else if (item.ReportCategoryId == 5) {
                                            SelectedPurchaseOrderReportNote(model.ReportNote.Create(item));
                                        }
                                        else if (item.ReportCategoryId == 6) {
                                            SelectedDeliveryReportNote(model.ReportNote.Create(item));
                                        }

                                    });
                                    
                               

                            },
                            error: function () {
                                toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                            }
                        });

                    },

                ConvertListToServer = function () {
                    CompanyReportNotes.removeAll();
                    CompanyReportNotes.push(SelectedReportNote().convertToServerData());
                    CompanyReportNotes.push(SelectOrderReportNote().convertToServerData());
                    CompanyReportNotes.push(SelectedInvoiceReportNote().convertToServerData());
                    CompanyReportNotes.push(SelectedPurchaseOrderReportNote().convertToServerData());
                    CompanyReportNotes.push(SelectedDeliveryReportNote().convertToServerData());
                },
                    // Save Notes
                    
                                     
                    saveNotes = function () {
                        ConvertListToServer();
                        
                        dataservice.saveReportNote(
                            { ReportsBanners: CompanyReportNotes() },
                            {
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
                    };
                // #endregion Service Calls

                return {
                    // Observables
                    Stores: Stores,
                    StoreName:StoreName,
                    SelectedReportNote: SelectedReportNote,
                    SelectOrderReportNote: SelectOrderReportNote,
                    SelectedInvoiceReportNote : SelectedInvoiceReportNote,
                    SelectedPurchaseOrderReportNote : SelectedPurchaseOrderReportNote,
                    SelectedDeliveryReportNote: SelectedDeliveryReportNote,
                    ImageFilesLoadedCallback:ImageFilesLoadedCallback,
                    ImageFilesOrderLoadedCallback: ImageFilesOrderLoadedCallback,
                    ImageFilesInvoiceLoadedCallback: ImageFilesInvoiceLoadedCallback, 
                    ImageFilesPurchaseLoadedCallback:ImageFilesPurchaseLoadedCallback,
                    ImageFilesDeliveryLoadedCallback:ImageFilesDeliveryLoadedCallback,
                    errorList: errorList,
                    CompanyReportNotes:CompanyReportNotes,
                    // Utility Methods
                    initialize: initialize,
                    getBannerByStoreId: getBannerByStoreId,
                    saveNotes: saveNotes,
                    checkStoreBannerUpload: checkStoreBannerUpload
                };
            })()
        };
        return ist.reportNote.viewModel;
    });
