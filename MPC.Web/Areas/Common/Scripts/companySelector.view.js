/*
    View for Company Selector. Used to keep the viewmodel clear of UI related logic
*/
define("common/companySelector.view",
    ["jquery", "common/companySelector.viewModel"], function ($) {

        var ist = window.ist || {};
        // View 
        ist.companySelector.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#companySelectorDialog")[0],
                // Show companySelector the dialog
                showDialog = function () {
                    $("#companySelectorDialog").modal("show");
                },
                focusFilter = function() {
                    setTimeout(function () {
                        $("#companySelectorDialog input").first().focus();
                    }, 500);
                },
                // Hide companySelector the dialog
                hideDialog = function () {
                    $("#companySelectorDialog").modal("hide");
                };
            
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                showDialog: showDialog,
                hideDialog: hideDialog,
                focusFilter: focusFilter
            };
            
        })(ist.companySelector.viewModel);

        // Initialize the view model
        if (ist.companySelector.view.bindingRoot) {
            ist.companySelector.viewModel.initialize(ist.companySelector.view);
        }
    });
