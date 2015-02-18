/*
    View for the tariff Type. Used to keep the viewmodel clear of UI related logic
*/
define("crm/crm.view",
    ["jquery", "crm/crm.viewModel"], function ($, crmViewModel) {

        var ist = window.ist || {};

        // View 
        ist.crm.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
               // binding root
               bindingRoot = $("#crmBindingRoot")[0],
               // Show Addressnthe dialog
                showAddressDialog = function () {
                    $("#myAddressSetModalForCrm").modal("show");
                    //initializeLabelPopovers();
                },
                // Hide Address the dialog
                hideAddressDialog = function () {
                    $("#myAddressSetModalForCrm").modal("hide");
                },
                // Show Contact Company the dialog
                showCompanyContactDialog = function () {
                    $("#myContactProfileModalForCrm").modal("show");
                    //initializeLabelPopovers();
                },
                // Hide Company Contact the dialog
                hideCompanyContactDialog = function () {
                    $("#myContactProfileModalForCrm").modal("hide");
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
                hideCompanyContactDialog: hideCompanyContactDialog
            };
        })(crmViewModel);

        // Initialize the view model
        if (ist.crm.view.bindingRoot) {
            crmViewModel.initialize(ist.crm.view);
        }
        return ist.crm.view;
    });