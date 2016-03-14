/*
    View for Phrase Library. Used to keep the viewmodel clear of UI related logic
*/
define("common/systemUser.view",
    ["jquery", "common/systemUser.viewModel"], function ($) {

        var ist = window.ist || {};
        // View 
        ist.systemUser.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#systemUserDialog")[0],
                // Show Activity the dialog
                showSystemUserDialog = function () {
                    $("#systemUserDialog").modal("show");
                },
                // Hide Activity the dialog
                hideSystemUserDialog = function () {
                    $("#systemUserDialog").modal("hide");
                };
           
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                showSystemUserDialog: showSystemUserDialog,
                hideSystemUserDialog: hideSystemUserDialog
               
            };
        })(ist.systemUser.viewModel);

        // Initialize the view model
        if (ist.systemUser.view.bindingRoot) {
            ist.systemUser.viewModel.initialize(ist.systemUser.view);
        }
    });
