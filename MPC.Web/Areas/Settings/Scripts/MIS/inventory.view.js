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
                // Go To Element with Validation Errors
                gotoElement = function (element) {
                    var tab = $(element).closest(".tab-pane");
                    if (!tab) {
                        return;
                    }

                    var liElement = $('a[href=#' + tab.attr('id') + ']');
                    if (!liElement) {
                        return;
                    }

                    liElement.click();

                    // Scroll to Element
                    setTimeout(function () {
                        window.scrollTo($(element).offset().left, $(element).offset().top - 50);
                        // Focus on element
                        $(element).focus();
                    }, 1000);
                },
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
                gotoElement: gotoElement,
            };
        })(inventoryViewModel);

        // Initialize the view model
        if (ist.inventory.view.bindingRoot) {
            inventoryViewModel.initialize(ist.inventory.view);
        }
        return ist.inventory.view;
    });