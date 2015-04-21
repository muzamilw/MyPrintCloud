define("common/reportManager.viewModel",
    ["jquery", "amplify", "ko", "common/reportManager.dataservice", "common/reportManager.model"], function ($, amplify, ko, dataservice, model) {
        var ist = window.ist || {};
        ist.reportManager = {
            viewModel: (function () {
                var// The view 
                    view,
                    // True if we are loading data
                    isLoading = ko.observable(false),
                    outputTo = ko.observable(false),
                     reportcategoriesList = ko.observableArray([])
                   
                    
                    // Show the dialog
                    show = function (CategoryId) {

                        if (CategoryId != undefined && CategoryId != null && CategoryId != 0) {
                            dataservice.getreportcategories({
                                CategoryId: CategoryId,
                            }, {
                                success: function (data) {
                                    
                                    reportcategoriesList.push(model.ReportCategory(data));
                                },
                                error: function (response) {
                                    toastr.error("Failed to Load . Error: " + response);
                                }
                            });

                            
                        }


                        isLoading(true);
                        view.show();
                    },
                    // Hide the dialog
                    hide = function () {
                        // Reset Call Backs
                        resetDialog();
                        view.hide();
                    },
                   
                    
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                    };

                return {
                    isLoading: isLoading,
                    reportcategoriesList: reportcategoriesList,
                    outputTo:outputTo,
                    initialize: initialize,
                    show: show,
                    hide: hide
                };
            })()
        };

        return ist.reportManager.viewModel;
    });