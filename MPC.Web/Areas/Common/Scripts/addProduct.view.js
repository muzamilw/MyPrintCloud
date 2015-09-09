/*
    View for add Product Dialog. Used to keep the viewmodel clear of UI related logic
*/
define("common/addProduct.view",
    ["jquery", "common/addProduct.viewModel"], function ($) {

        var ist = window.ist || {};
        // View 
        ist.addProduct.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#productFromRetailStoreModal")[0],
                //Show Product From Retail Store Modal
                //Show Product From Retail Store Modal
                showProductFromRetailStoreModal = function() {
                    $("#productFromRetailStoreModal").modal('show');
                },
               hideProductFromRetailStoreModal = function () {
                $("#productFromRetailStoreModal").modal('hide');
            };
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                showProductFromRetailStoreModal: showProductFromRetailStoreModal,
                hideProductFromRetailStoreModal: hideProductFromRetailStoreModal
            };
            
        })(ist.addProduct.viewModel);

        // Initialize the view model
        if (ist.addProduct.view.bindingRoot) {
            ist.addProduct.viewModel.initialize(ist.addProduct.view);
        }
    });
