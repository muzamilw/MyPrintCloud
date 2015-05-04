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
                    errorList = ko.observableArray([]),
                    Stores = ko.observableArray([]),
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

                            },
                            error: function () {
                                toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                            }
                        });
                    },

                    // get banner by id
                    getBannerByStoreId = function (store) {
                        isLoading(true);
                        var id = store.StoreID;
                        dataservice.getReportNote(
                            { CompanyID: id },
                            {
                                success: function (data) {
                                    if (data.ReportCategoryId == 3)
                                    {
                                        SelectedReportNote(model.ReportNote.Create(data));
                                    }
                                    else if(data.ReportCategoryId == 12)
                                    {
                                        SelectOrderReportNote(model.ReportNote.Create(data));
                                    }
                                    else if(data.ReportCategoryId == 13)
                                    {
                                        SelectedInvoiceReportNote(model.ReportNote.Create(data));
                                    }
                                    else if(data.ReportCategoryId == 5)
                                    {
                                        SelectedPurchaseOrderReportNote(model.ReportNote.Create(data));
                                    }
                                    else if(data.ReportCategoryId == 6)
                                    {
                                        SelectedDeliveryReportNote(model.ReportNote.Create(data));
                                    }
                               

                            },
                            error: function () {
                                toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                            }
                        });

                    };

                    
                    // Save Prefixes
                    //onSavePrefixes = function (prefix) {
                    //    errorList.removeAll();
                    //    if (doBeforeSave()) {
                    //        savePrefixes(prefix);
                    //    }
                    //},
                    // Do Before Logic
                    //doBeforeSave = function () {
                    //    var flag = true;
                    //    if (!selectedPrefix().isValid()) {
                    //        selectedPrefix().errors.showAllMessages();
                    //        flag = false;
                    //    }
                    //    return flag;
                    //},
                  
                    //savePrefixes = function (prefix) {
                    //    dataservice.savePrefixes(model.prefixServerMapper(prefix), {
                    //        success: function (data) {
                    //            selectedPrefix().reset();
                    //            toastr.success("Successfully save.");
                    //        },
                    //        error: function (exceptionMessage, exceptionType) {

                    //            if (exceptionType === ist.exceptionType.MPCGeneralException) {

                    //                toastr.error(exceptionMessage);

                    //            } else {

                    //                toastr.error("Failed to save.");

                    //            }

                    //        }
                    //    });
                    //};
                // #endregion Service Calls

                return {
                    // Observables
                    Stores: Stores,
                    SelectedReportNote: SelectedReportNote,
                    SelectOrderReportNote: SelectOrderReportNote,
                    SelectedInvoiceReportNote : SelectedInvoiceReportNote,
                    SelectedPurchaseOrderReportNote : SelectedPurchaseOrderReportNote,
                    SelectedDeliveryReportNote : SelectedDeliveryReportNote,
                    errorList: errorList,
                    // Utility Methods
                    initialize: initialize,
                    getBannerByStoreId: getBannerByStoreId
                };
            })()
        };
        return ist.reportNote.viewModel;
    });
