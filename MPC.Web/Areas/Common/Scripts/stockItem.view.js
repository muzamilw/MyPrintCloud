/*
    View for Stock Item. Used to keep the viewmodel clear of UI related logic
*/
define("common/stockItem.view",
    ["jquery", "common/stockItem.viewModel"], function ($) {

        var ist = window.ist || {};
        // View 
        ist.stockItem.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#stockDialog")[0],
                // Show StockItem the dialog
                showDialog = function () {
                    $("#stockDialog").modal("show");
                },
                // Hide StockItem the dialog
                hideDialog = function () {
                    $("#stockDialog").modal("hide");
                };
            
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                showDialog: showDialog,
                hideDialog: hideDialog
            };
            
        })(ist.stockItem.viewModel);

        // Initialize the view model
        if (ist.stockItem.view.bindingRoot) {
            ist.stockItem.viewModel.initialize(ist.stockItem.view);
        }
    });
