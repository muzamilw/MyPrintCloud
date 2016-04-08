
//define("emails/emails.view",
//    ["jquery", "emails/emails.viewModel"], function ($) {

//        var ist = window.ist || {};
//        // View 
//        ist.emails.view = (function (specifiedViewModel) {
//            var // View model 
//                viewModel = specifiedViewModel,
//                // Binding root used with knockout
//                bindingRoot = $("#emailsBinding")[0];
                
//            return {
//                bindingRoot: bindingRoot,
//                viewModel: viewModel,
                
               
//            };
//        })(ist.emails.viewModel);

//        // Initialize the view model
//        if (ist.emails.view.bindingRoot) {
//            ist.emails.viewModel.initialize(ist.emails.view);
//        }
//        return ist.emails.view;
//    });

define("emails/emails.view",
    ["jquery", "emails/emails.viewModel"], function ($, emailsViewModel) {

        var ist = window.ist || {};

        // View 
        ist.emails.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#emailsBinding")[0],
                showEmailCamapaignDialog = function () {
                    $("#addEditCampaignEmailModal").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Email Camapaign the dialog
                hideEmailCamapaignDialog = function () {
                    $("#addEditCampaignEmailModal").modal("hide");
                },
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
                showEmailCamapaignDialog: showEmailCamapaignDialog,
                hideEmailCamapaignDialog: hideEmailCamapaignDialog

            };
        })(emailsViewModel);

        // Initialize the view model
        if (ist.emails.view.bindingRoot) {
            emailsViewModel.initialize(ist.emails.view);
        }
        return ist.emails.view;
    });