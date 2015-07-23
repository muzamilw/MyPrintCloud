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

                // Binding root used with knockout
                bindingRootq = $("#dialog-ok")[0],
                // Show the dialog
                show = function () {
                    $("#dialog-confirm").modal("show");
                },
                // Hide the dialog
                hide = function () {
                    $("#dialog-confirm").modal("hide");
                },

                showWarningPopup = function () {
                    $("#dialog-ok").modal("show");
                },
                 // Hide the dialog
                hideWarningPopup = function () {
                    $("#dialog-ok").modal("hide");
                };
                


            return {
                bindingRoot: bindingRoot,
                bindingRootq: bindingRootq,
                viewModel: viewModel,
                show: show,
                hide: hide,
                showWarningPopup: showWarningPopup,
                hideWarningPopup: hideWarningPopup
            };
        })(ist.confirmation.viewModel);

        // Initialize the view model
        if (ist.confirmation.view.bindingRoot) {
            ist.confirmation.viewModel.initialize(ist.confirmation.view);
        }
    });