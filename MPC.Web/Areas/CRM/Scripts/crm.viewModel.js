/*
    Module with the view model for the crm
*/
define("crm/crm.viewModel",
    ["jquery", "amplify", "ko", "crm/crm.dataservice", "crm/crm.model", "common/confirmation.viewModel", "common/pagination", "common/sharedNavigation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, sharedNavigationVm) {
        var ist = window.ist || {};
        ist.crm = {
            viewModel: (function () {
                var //View
                view,
                //Initialize
               initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                };

                return {
                    initialize: initialize
                };
            })()
        };
        return ist.crm.viewModel;
    });
