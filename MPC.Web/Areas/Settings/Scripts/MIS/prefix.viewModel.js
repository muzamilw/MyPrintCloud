/*
    Module with the view model for the My Organization.
*/
define("prefix/prefix.viewModel",
    ["jquery", "amplify", "ko", "prefix/prefix.dataservice", "prefix/prefix.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.prefix = {
            viewModel: (function () {
                var // the view 
                    view,
                    // Active
                    selectedPrefix = ko.observable(),
                    errorList = ko.observableArray([]),
                    // #region Busy Indicators
                    isLoadingPrefix = ko.observable(false),
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
                        isLoadingPrefix(true);
                        dataservice.getPrefixesDetail({
                            success: function(data) {
                               // getPrefixByOrganisationId();
                                if (callBack && typeof callBack === 'function') {
                                    callBack();
                                }
                                var pfx = model.prefixClientMapper(data);
                                selectedPrefix(pfx);
                                isLoadingPrefix(false);
                                selectedPrefix().reset();
                            },
                            error: function() {
                                toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                            }
                        });
                    },
                    
                    // Save Prefixes
                    onSavePrefixes = function (prefix) {
                        errorList.removeAll();
                        if (doBeforeSave()) {
                            savePrefixes(prefix);
                        }
                    },
                    // Do Before Logic
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedPrefix().isValid()) {
                            selectedPrefix().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                  
                    savePrefixes = function (prefix) {
                        dataservice.savePrefixes(model.prefixServerMapper(prefix), {
                            success: function (data) {
                                selectedPrefix().reset();
                                toastr.success("Successfully save.");
                            },
                            error: function (exceptionMessage, exceptionType) {

                                if (exceptionType === ist.exceptionType.CaresGeneralException) {

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
                    selectedPrefix: selectedPrefix,
                    errorList: errorList,
                    // Utility Methods
                    initialize: initialize,
                    // Utility Methods
                    onSavePrefixes: onSavePrefixes,
                };
            })()
        };
        return ist.prefix.viewModel;
    });
