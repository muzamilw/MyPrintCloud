/*
    View for the tariff Type. Used to keep the viewmodel clear of UI related logic
*/
define("crm/crm.view",
    ["jquery", "crm/crm.viewModel"], function ($, crmViewModel) {

        var ist = window.ist || {};

        // View 
        ist.crm.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
               // binding root
               bindingRoot = $("#crmBindingRoot")[0],
            // Initialize
            initialize = function () {
                if (!bindingRoot) {
                    return;
                }
            };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
            };
        })(crmViewModel);

        // Initialize the view model
        if (ist.crm.view.bindingRoot) {
            crmViewModel.initialize(ist.crm.view);
        }
        return ist.crm.view;
    });