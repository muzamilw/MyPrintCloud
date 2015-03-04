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
                // Show Item Detail Dialog
                showItemDetailDialog = function() {
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
                //#region Product From Retail Store Dialog
                //Show Product From Retail Store Modal
                showProductFromRetailStoreModal = function() {
                    $("#productFromRetailStoreModal").modal('show');
                },
                //Hide Product From Retail Store Modal
                hideProductFromRetailStoreModal = function() {
                    $("#productFromRetailStoreModal").modal('hide');
                },
                //#endregion
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
                hideProductFromRetailStoreModal: hideProductFromRetailStoreModal
            };
        })(orderViewModel);

        // Initialize the view model
        if (ist.order.view.bindingRoot) {
            orderViewModel.initialize(ist.order.view);
        }
        return ist.order.view;
    });