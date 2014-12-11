/*
    View for Supplier. Used to keep the viewmodel clear of UI related logic
*/
define("common/supplier.view",
    ["jquery", "common/supplier.viewModel"], function ($) {

        var ist = window.ist || {};
        // View 
        ist.supplier.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#supplierDialog")[0],
                // Show Activity the dialog
                showSupplierDialog = function () {
                    $("#supplierDialog").modal("show");
                },
                // Hide Activity the dialog
                hideSupplierDialog = function () {
                    $("#supplierDialog").modal("hide");
                };

            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                showSupplierDialog: showSupplierDialog,
                hideSupplierDialog: hideSupplierDialog
            };
        })(ist.supplier.viewModel);

        // Initialize the view model
        if (ist.supplier.view.bindingRoot) {
            ist.supplier.viewModel.initialize(ist.supplier.view);
        }
    });