/*
    View for the Confirmation. Used to keep the viewmodel clear of UI related logic
*/
define("common/confirmation.view",
    ["jquery", "common/confirmation.viewModel"], function ($) {

        var ist = window.ist || {};
        // View 
        ist.confirmation.view = (function (specifiedViewModel) {
            var// View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#dialog-confirm")[0],
                // Show the dialog
                show = function() {
                    $("#dialog-confirm").modal("show");
                },
                // Hide the dialog
                hide = function() {
                    $("#dialog-confirm").modal("hide");
                };
            
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                show: show,
                hide: hide
            };
        })(ist.confirmation.viewModel);

        // Initialize the view model
        if (ist.confirmation.view.bindingRoot) {
            ist.confirmation.viewModel.initialize(ist.confirmation.view);
        }
    });