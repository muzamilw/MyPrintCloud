/*
    View for the tariff Type. Used to keep the viewmodel clear of UI related logic
*/
define("crm/contacts.view",
    ["jquery", "crm/contacts.viewModel"], function ($, contactsViewModel) {
        var ist = window.ist || {};
        // View 
        ist.contacts.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // binding root
                bindingRoot = $("#contactsBindingRoot")[0],
                // contact profile binding root
                contactProfileBindingRoot = $("#myContactProfileModal")[0],
                // Initialize
                initialize = function() {
                    if (!bindingRoot) {
                        return;
                    }
                },
                // Show Contact Company the dialog
                showCompanyContactDetailDialog = function() {
                    $("#myContactProfileModal").modal("show");
                },
                // Hide Company Contact the dialog
                hideCompanyContactDialog = function() {
                    $("#myContactProfileModal").modal("hide");
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                showCompanyContactDetailDialog: showCompanyContactDetailDialog,
                hideCompanyContactDialog: hideCompanyContactDialog,
                contactProfileBindingRoot: contactProfileBindingRoot
            };
        })(contactsViewModel);

        // Initialize the view model
        if (ist.contacts.view.bindingRoot) {
            contactsViewModel.initialize(ist.contacts.view);
        }
        return ist.contacts.view;
    });