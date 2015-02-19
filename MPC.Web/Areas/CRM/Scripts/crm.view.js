﻿/*
    View for the tariff Type. Used to keep the viewmodel clear of UI related logic
*/
define("crm/crm.view",
    ["jquery", "crm/crm.viewModel"], function ($, crmViewModel) {

        var ist = window.ist || {};
        //Setting flag to false, it indicates that current screen is Suppliers
        crmViewModel.isProspectOrCustomerScreen(true);
        // View 
        ist.crm.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
               // binding root
               bindingRoot = $("#crmBindingRoot")[0],
               // Show Addressnthe dialog
                showAddressDialog = function () {
                    $("#myAddressSetModalForCrm").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Address the dialog
                hideAddressDialog = function () {
                    $("#myAddressSetModalForCrm").modal("hide");
                },
                // Show Contact Company the dialog
                showCompanyContactDialog = function () {
                    $("#myContactProfileModalForCrm").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Company Contact the dialog
                hideCompanyContactDialog = function () {
                    $("#myContactProfileModalForCrm").modal("hide");
                },
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
                showAddressDialog: showAddressDialog,
                hideAddressDialog: hideAddressDialog,
                showCompanyContactDialog: showCompanyContactDialog,
                hideCompanyContactDialog: hideCompanyContactDialog,
                gotoElement: gotoElement,
                initializeLabelPopovers: initializeLabelPopovers
            };
        })(crmViewModel);

        // Initialize the view model
        if (ist.crm.view.bindingRoot) {
            crmViewModel.initialize(ist.crm.view);
        }
        return ist.crm.view;
    });