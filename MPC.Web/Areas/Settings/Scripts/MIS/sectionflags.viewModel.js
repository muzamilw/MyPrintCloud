/*
    Module with the view model for the My Organization.
*/
define("sectionflags/sectionflags.viewModel",
    ["jquery", "amplify", "ko", "sectionflags/sectionflags.dataservice", "sectionflags/sectionflags.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.sectionflags = {
            viewModel: (function () {
                var // the view 
                    view,
                    // Active
                    selectedsectionflags = ko.observableArray(),
                    errorList = ko.observableArray([]),
                    // #region Busy Indicators
                    isLoadingsectionflags = ko.observable(false),
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
                        isLoadingsectionflags(true);
                        dataservice.getSectionFlags({
                            success: function (data) {
                                // getPrefixByOrganisationId();
                                if (data != null)
                                {

                                    var Counter = self.data.length;
                                    for (var i = 0; i < Counter; i++)
                                    {
                                        self.selectedsectionflags.push(new model.sectionflags(self.data[i]));
                                    }
                                 
                                }
                                
                            },
                            error: function ()
                            {
                                toastr.error(ist.resourceText.loadBaseDataFailedMsg);
                            }
                        });
                    };

                    

                return {
                    // Observables
                    selectedsectionflags: selectedsectionflags,
                    // Utility Methods
                    initialize: initialize,
                   
                    
                };
            })()
        };
        return ist.sectionflags.viewModel;
    });
