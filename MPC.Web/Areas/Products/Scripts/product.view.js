/*
    View for the Product. Used to keep the viewmodel clear of UI related logic
*/
define("product/product.view",
    ["jquery", "product/product.viewModel"], function ($, productViewModel) {

        var ist = window.ist || {};

        // View 
        ist.product.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#productBinding")[0],
                // Change View - List/Grid View
                changeView = function(element) {
                    var elementId = element.currentTarget.id;
                    if (elementId === "listViewIcon") {
                        if (viewModel.isListViewVisible()) {
                            return;
                        }

                        viewModel.setListViewActive();
                    }
                    else if (elementId === "gridViewIcon") {
                        if (viewModel.isGridViewVisible()) {
                            return;
                        }

                        viewModel.setGridViewActive();
                    }
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
                changeView: changeView
            };
        })(productViewModel);

        // Initialize the view model
        if (ist.product.view.bindingRoot) {
            productViewModel.initialize(ist.product.view);
        }
        return ist.product.view;
    });
