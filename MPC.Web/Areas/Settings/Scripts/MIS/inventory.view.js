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
                 // Hide Add Stock Qty Dialog
                hideAddStockQtyDialog = function () {
                    $("#addStockQtyModel").modal("hide");
                },
                // Show Add Stock Qty Dialog
                showAddStockQtyDialog = function () {
                    $("#addStockQtyModel").modal("show");
                    initializeLabelPopovers();
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
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                gotoElement: gotoElement,
                initializeLabelPopovers: initializeLabelPopovers,
                hideAddStockQtyDialog: hideAddStockQtyDialog,
                showAddStockQtyDialog: showAddStockQtyDialog
            };
        })(inventoryViewModel);

        // Initialize the view model
        if (ist.inventory.view.bindingRoot) {
            inventoryViewModel.initialize(ist.inventory.view);
        }
        return ist.inventory.view;
    });