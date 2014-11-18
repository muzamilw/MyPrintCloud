/*
    View for the tariff Type. Used to keep the viewmodel clear of UI related logic
*/
define("myOrganization/myOrganization.view",
    ["jquery", "myOrganization/myOrganization.viewModel"], function ($, myOrganizationViewModel) {

        var ist = window.ist || {};

        // View 
        ist.myOrganization.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#myOrganizationBinding")[0],
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }

                    // Handle Sorting
                   // handleSorting("tariffTypeTable", viewModel.sortOn, viewModel.sortIsAsc, viewModel.getTariffType);
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
            };
        })(myOrganizationViewModel);

        // Initialize the view model
        if (ist.myOrganization.view.bindingRoot) {
            myOrganizationViewModel.initialize(ist.myOrganization.view);
        }
        return ist.myOrganization.view;
    });