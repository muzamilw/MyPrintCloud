/*
    View for the Inventory. Used to keep the viewmodel clear of UI related logic
*/
define("inventory/inventory.view",
    ["jquery", "inventory/inventory.viewModel"], function ($, inventoryViewModel) {

        var ist = window.ist || {};

        // View 
        ist.inventory.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#inventoryBinding")[0],
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
        })(inventoryViewModel);

        // Initialize the view model
        if (ist.inventory.view.bindingRoot) {
            inventoryViewModel.initialize(ist.inventory.view);
        }
        return ist.inventory.view;
    });