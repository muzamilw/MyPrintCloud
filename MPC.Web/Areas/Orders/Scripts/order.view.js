﻿/*
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
                // Order State [pending]
                orderstate = ko.observable(0),
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
                // Show inventory dialog
                //showInventoryItemDialog = function () {
                //    $("#inventoryItem").modal("show");
                //},
                // Hide inventory dialog
                hideInventoryItemDialog = function () {
                    $("#inventoryItem").modal("hide");
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
                    $("#orderCostCentersQuanity").modal("show");
                },
               // Hide Cost Centers Quantity the dialog
                hideCostCentersQuantityDialog = function () {
                    $("#orderCostCentersQuanity").modal("hide");
                },
                setOrderState = function (state, isFromEstimate) {
                    orderstate(state);
                    $(function () {
                        // set up an array to hold the order Status
                        var orderStatusArray = ["Pending Order", "Confirmed Start", "In Production", "Shipped & Invoiced", "Cancelled"];

                        // If Is Order is From Estimate then add Status "Revert to Estimate"
                        if (isFromEstimate) {
                            orderStatusArray.splice(0, 0, "Revert to Estimate");
                        }

                        $(".slider").slider().slider("pips");
                        $(".slider")

                            // activate the slider with options
                            .slider({
                                min: 0,
                                max: orderStatusArray.length - 1,
                                value: orderstate() !== 0 ? (orderstate() === 9 ? orderstate() - 5 : orderstate() - 4) : orderstate()
                                //value: orderstate()
                            })

                            // add pips with the labels set to "months"
                            .slider("pips", {
                                rest: "label",
                                labels: orderStatusArray
                            })

                            // and whenever the slider changes, lets echo out the month
                            .on("slidechange", function (e, ui) {
                                var oldSatate = orderstate();
                                orderstate((ui.value) + 4);
                                if (oldSatate !== orderstate()) {
                                    orderViewModel.onOrderStatusChange(ui.value);
                                    // toastr.success("You selected " + orderStatusArray[ui.value] + " (" + ui.value + ")");
                                }

                            });
                    });
                },
               
                // Show section Cost Center Dialog Model
                showSectionCostCenterDialogModel = function () {
                    $("#sectionCostCenterDialogModel").modal("show");
                },
                // hide section Cost Center Dialog Model
                hideSectionCostCenterDialogModel = function () {
                    $("#sectionCostCenterDialogModel").modal("show");
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
                 //Show Order Pre Payment Modal
                showOrderPrePaymentModal = function () {
                    $("#orderPrePaymentModal").modal('show');
                },
                //Hide Order Pre Payment Modal
                hideOrderPrePaymentModal = function () {
                    $("#orderPrePaymentModal").modal('hide');
                },
                // Show Order Status Progress To Job Dialog
                showOrderStatusProgressToJobDialog = function () {
                    $("#orderStatusProgressToJobModal").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Order Status Progress To Job Dialog
                hideOrderStatusProgressToJobDialog = function () {
                    $("#orderStatusProgressToJobModal").modal("hide");
                },

                //#endregion
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
                showSectionDetailDialog: showSectionDetailDialog,
                hideSectionDetailDialog: hideSectionDetailDialog,
                showProductFromRetailStoreModal: showProductFromRetailStoreModal,
                hideProductFromRetailStoreModal: hideProductFromRetailStoreModal,
                showCostCentersDialog: showCostCentersDialog,
                hideRCostCentersDialog: hideRCostCentersDialog,
                showCostCentersQuantityDialog: showCostCentersQuantityDialog,
                hideCostCentersQuantityDialog: hideCostCentersQuantityDialog,
                initializeLabelPopovers: initializeLabelPopovers,
                showOrderPrePaymentModal: showOrderPrePaymentModal,
                hideOrderPrePaymentModal: hideOrderPrePaymentModal,
                setOrderState: setOrderState,
                orderstate: orderstate,
                //showInventoryItemDialog: showInventoryItemDialog,
                hideInventoryItemDialog: hideInventoryItemDialog,
                showSectionCostCenterDialogModel: showSectionCostCenterDialogModel,
                hideSectionCostCenterDialogModel: hideSectionCostCenterDialogModel,
                showOrderStatusProgressToJobDialog: showOrderStatusProgressToJobDialog,
                hideOrderStatusProgressToJobDialog:hideOrderStatusProgressToJobDialog
            };
        })(orderViewModel);

        // Initialize the view model
        if (ist.order.view.bindingRoot) {
            //orderViewModel.currentScreen($("#CallingMethod").val() == "" ? "0" : $("#CallingMethod").val());
            orderViewModel.initialize(ist.order.view);

        }
        return ist.order.view;
    });
