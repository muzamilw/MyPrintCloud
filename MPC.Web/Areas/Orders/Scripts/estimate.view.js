/*
    View for the Order. Used to keep the viewmodel clear of UI related logic
*/
define("order/estimate.view",
    ["jquery", "order/order.viewModel"], function ($, orderViewModel) {

        var ist = window.ist || {};
        ist.estimate = window.ist.estimate || {};

        // View 
        ist.estimate.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Order State [pending]
                orderstate = ko.observable(0),
                // Binding root used with knockout
                bindingRoot = $("#estimateBinding")[0],
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

                    var target = $(element);
                    target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                    if (target.length) {
                        $('html,body').animate({
                            scrollTop: (target.offset().top - 50)
                        }, 1000);
                        return false;
                    }
                },
                // Show inventory dialog
                showInventoryItemDialog = function () {
                    $("#inventoryItem").modal("show");
                },
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
                // Show Inquiry Detail Item Dialog
                showInquiryDetailItemDialog = function () {
                    $("#InquiryDetailItemDialog").modal("show");
                },
               // Hide Inquiry Detail Item Dialog
                hideInquiryDetailItemDialog = function () {
                    $("#InquiryDetailItemDialog").modal("hide");
                },
                // Show Progress To Order Dialog
                showProgressToOrderDialog = function () {
                    $("#progressToOrderDialog").modal('show');
                },
                //Hide Progress To Order Dialog
                hideProgressToOrderDialog = function () {
                    $("#progressToOrderDialog").modal('hide');
                },
                setOrderState = function (state, isFromEstimate) {
                    orderstate(state);
                    $(function () {
                        // set up an array to hold the order Status
                        var orderStatusArray = ["Pending Order", "Confirmed Start", "In Production", "Shipped & Invoiced"];

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
                                value: orderstate() !== 0 ? orderstate() - 4 : orderstate()
                            })

                            // add pips with the labels set to "months"
                            .slider("pips", {
                                rest: "label",
                                labels: orderStatusArray
                            })

                            // and whenever the slider changes, lets echo out the month
                            .on("slidechange", function (e, ui) {
                                orderstate((ui.value) + 4);
                                //alert("You selected " + orderStatusArray[ui.value] + " (" + ui.value + ")");
                            });
                    });
                },
                // Show Sheet Plan Image the dialog
                showSheetPlanImageDialog = function () {
                    $("#sheetPlanModal").modal("show");
                },
                // Show Sheet Plan Image the dialog
                hideSheetPlanImageDialog = function () {
                    $("#sheetPlanModal").modal("show");
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
                //Show Estimate Run Wizard Modal
                showEstimateRunWizard = function () {
                    $("#estimateRunWizard").modal('show');
                },
                //Hide Estimate Run Wizard Modal
                hideEstimateRunWizard = function () {
                    $("#estimateRunWizard").modal('hide');
                },

                // Show Inks Dialog
                showInksDialog = function () {
                    $("#inkDialogModel").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Inks Dialog
                hideInksDialog = function () {
                    $("#inkDialogModel").modal("hide");
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
                showSheetPlanImageDialog: showSheetPlanImageDialog,
                hideSheetPlanImageDialog: hideSheetPlanImageDialog,
                showInventoryItemDialog: showInventoryItemDialog,
                hideInventoryItemDialog: hideInventoryItemDialog,
                showEstimateRunWizard: showEstimateRunWizard,
                hideEstimateRunWizard: hideEstimateRunWizard,
                showSectionCostCenterDialogModel: showSectionCostCenterDialogModel,
                hideSectionCostCenterDialogModel: hideSectionCostCenterDialogModel,
                showInquiryDetailItemDialog: showInquiryDetailItemDialog,
                hideInquiryDetailItemDialog: hideInquiryDetailItemDialog,
                showProgressToOrderDialog: showProgressToOrderDialog,
                hideProgressToOrderDialog: hideProgressToOrderDialog,
                showInksDialog: showInksDialog,
                hideInksDialog: hideInksDialog
            };
        })(orderViewModel);

        // Initialize the view model
        if (ist.estimate.view.bindingRoot) {
            orderViewModel.initializeEstimate(ist.estimate.view);
        }
        return ist.estimate.view;
    });
