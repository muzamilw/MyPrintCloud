/*
    View for the Order. Used to keep the viewmodel clear of UI related logic
*/
define("invoice/invoice.view",
    ["jquery", "invoice/invoice.viewModel"], function ($, invoiceViewModel) {

        var ist = window.ist || {};

        // View 
        ist.invoice.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Order State [pending]
                orderstate = ko.observable(0),
                // Binding root used with knockout
                bindingRoot = $("#invoiceBinding")[0],
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

                // Show Item Detail Dialog
                showItemDetailDialog = function () {
                    $("#orderItemDetailDialog").modal('show');

                },
                // Hide Item Detail Dialog
                hideItemDetailDialog = function () {
                    $("#orderItemDetailDialog").modal('hide');
                },

                 // Show Cost Centers Quantity the dialog
                showCostCentersQuantityDialog = function () {
                    $("#orderCostCentersQuanity").modal("show");
                },
               // Hide Cost Centers Quantity the dialog
                hideCostCentersQuantityDialog = function () {
                    $("#orderCostCentersQuanity").modal("hide");
                },
                 // Show Invoice Detail the dialog
                showInvoiceDetailDialog = function () {
                    $("#invoiceDetailDialog").modal("show");
                },
               // Hide Invoice Detail the dialog
                hideInvoiceDetailDialog = function () {
                    $("#invoiceDetailDialog").modal("hide");
                },

                // Initialize Label Popovers
                initializeLabelPopovers = function () {
                    // ReSharper disable UnknownCssClass
                    $('.bs-example-tooltips a').popover();
                    // ReSharper restore UnknownCssClass
                    window.scrollTo(0, 0);
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
                showItemDetailDialog: showItemDetailDialog,
                hideItemDetailDialog: hideItemDetailDialog,
                showCostCentersQuantityDialog: showCostCentersQuantityDialog,
                hideCostCentersQuantityDialog: hideCostCentersQuantityDialog,
                initializeLabelPopovers: initializeLabelPopovers,
                showInvoiceDetailDialog: showInvoiceDetailDialog,
                hideInvoiceDetailDialog: hideInvoiceDetailDialog

            };
        })(invoiceViewModel);

        // Initialize the view model
        if (ist.invoice.view.bindingRoot) {
            invoiceViewModel.initialize(ist.invoice.view);
        }
        return ist.invoice.view;
    });
