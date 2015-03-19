/*
    View for the Order. Used to keep the viewmodel clear of UI related logic
*/
define("order/order.view",
    ["jquery", "order/order.viewModel"], function ($, orderViewModel) {

        var ist = window.ist || {};

        // View 
        ist.order.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#orderBinding")[0],
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
                 // Show Cost Centers the dialog
                showCostCentersDialog = function () {
                    $("#costCenters").modal("show");
                },
                 // Hide Cost Centers the dialog
                hideRCostCentersDialog = function () {
                    $("#costCenters").modal("hide");
                },   
                // Show Item Detail Dialog
                showItemDetailDialog = function () {
                    $("#orderItemDetailDialog").modal('show');
                },
                // Hide Item Detail Dialog
                hideItemDetailDialog = function () {
                    $("#orderItemDetailDialog").modal('hide');
                },
                // Show Section Detail Dialog
                showSectionDetailDialog = function () {
                    $("#orderSectionDetailDialog").modal('show');
                },
                // Hide Section Detail Dialog
                hideSectionDetailDialog = function () {
                    $("#orderSectionDetailDialog").modal('hide');
                },
               // Show Cost Centers Quantity the dialog
                showCostCentersQuantityDialog = function () {
                    $("#costCentersQuanity").modal("show");
                },
               // Hide Cost Centers Quantity the dialog
                hideCostCentersQuantityDialog = function () {
                    $("#costCentersQuanity").modal("hide");
                },
                //#region Product From Retail Store Dialog
                //Show Product From Retail Store Modal
                showProductFromRetailStoreModal = function () {
                    $("#productFromRetailStoreModal").modal('show');
                },
                //Hide Product From Retail Store Modal
                hideProductFromRetailStoreModal = function () {
                    $("#productFromRetailStoreModal").modal('hide');
                },
                //#endregion
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

            $(function () {
                // set up an array to hold the months
                var months = ["Pending Order", "Confirmed Start", "In Production", "Ship", "Invoice"];
                var activeMonth = new Date().getMonth();
                $(".slider").slider().slider("pips");
                $(".slider")

                    // activate the slider with options
                    .slider({
                        min: 0,
                        max: months.length - 1,
                        value: 2
                    })

                    // add pips with the labels set to "months"
                    .slider("pips", {
                        rest: "label",
                        labels: months
                    })

                    // and whenever the slider changes, lets echo out the month
                    .on("slidechange", function (e, ui) {
                        //  $("#labels-months-output").text("You selected " + months[ui.value] + " (" + ui.value + ")");
                    });
            });

            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                gotoElement: gotoElement,
                showItemDetailDialog: showItemDetailDialog,
                hideItemDetailDialog: hideItemDetailDialog,
                showSectionDetailDialog: showSectionDetailDialog,
                hideSectionDetailDialog: hideSectionDetailDialog,
                showProductFromRetailStoreModal: showProductFromRetailStoreModal,
                hideProductFromRetailStoreModal: hideProductFromRetailStoreModal,
                showCostCentersDialog: showCostCentersDialog,
                hideRCostCentersDialog: hideRCostCentersDialog,
                showCostCentersQuantityDialog: showCostCentersQuantityDialog,
                hideCostCentersQuantityDialog: hideCostCentersQuantityDialog,
                initializeLabelPopovers: initializeLabelPopovers
            };
        })(orderViewModel);

        // Initialize the view model
        if (ist.order.view.bindingRoot) {
            //orderViewModel.currentScreen($("#CallingMethod").val() == "" ? "0" : $("#CallingMethod").val());
            orderViewModel.initialize(ist.order.view);

        }
        return ist.order.view;
    });
