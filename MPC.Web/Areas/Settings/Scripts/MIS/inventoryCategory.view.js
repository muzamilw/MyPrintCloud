/*
    View for the tariff Type. Used to keep the viewmodel clear of UI related logic
*/
define("inventoryCategory/inventoryCategory.view",
    ["jquery", "inventoryCategory/inventoryCategory.viewModel"], function ($, inventoryCategoryViewModel) {

        var ist = window.ist || {};

        // View 
        ist.inventoryCategory.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#inventoryCategoriesBinding")[0],
                // Show Activity the dialog
                showInventoryCategoryDialog = function () {
                    $("#inventoryCategoryDialog").modal("show");
                },
                // Hide Activity the dialog
                hideInventoryCategoryDialog = function () {
                    $("#inventoryCategoryDialog").modal("hide");
                },
                // Initialize Label Popovers
                initializeLabelPopovers = function () {
                    // ReSharper disable UnknownCssClass
                    $('.bs-example-tooltips a').popover();
                    // ReSharper restore UnknownCssClass
                },
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
                viewModel: viewModel,
                showInventoryCategoryDialog: showInventoryCategoryDialog,
                hideInventoryCategoryDialog: hideInventoryCategoryDialog,
                initializeLabelPopovers: initializeLabelPopovers
            };
        })(inventoryCategoryViewModel);

        // Initialize the view model
        if (ist.inventoryCategory.view.bindingRoot) {
            inventoryCategoryViewModel.initialize(ist.inventoryCategory.view);
        }
        return ist.inventoryCategory.view;
    });