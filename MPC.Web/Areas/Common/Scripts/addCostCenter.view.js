/*
    View for Add Cost Center Dialog. Used to keep the viewmodel clear of UI related logic
*/
define("common/addCostCenter.view",
    ["jquery", "common/addCostCenter.viewModel"], function ($) {

        var ist = window.ist || {};
        // View 
        ist.addCostCenter.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#costCenters")[0],

                 // Show Cost Centers Quantity the dialog
                showCostCentersQuantityDialog = function () {
                    $("#costCentersQuanity").modal("show");
                },
               // Hide Cost Centers Quantity the dialog
                hideCostCentersQuantityDialog = function () {
                    $("#costCentersQuanity").modal("hide");
                },
                // Show Add Cost Center the dialog
                showDialog = function () {
                    $("#costCentersMain").modal("show");
                },
                // Hide Add Cost Center the dialog
                hideDialog = function () {
                    $("#costCentersMain").modal("hide");
                };
            
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                showDialog: showDialog,
                hideDialog: hideDialog,
                showCostCentersQuantityDialog: showCostCentersQuantityDialog,
                hideCostCentersQuantityDialog: hideCostCentersQuantityDialog
            };
            
        })(ist.addCostCenter.viewModel);

        // Initialize the view model
        if (ist.addCostCenter.view.bindingRoot) {
            ist.addCostCenter.viewModel.initialize(ist.addCostCenter.view);
        }
    });
