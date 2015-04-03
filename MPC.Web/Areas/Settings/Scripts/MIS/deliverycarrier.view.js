define("deliverycarrier/deliverycarrier.view",
    ["jquery", "deliverycarrier/deliverycarrier.viewModel"], function ($, carrierdeliveryViewModel) {

        var ist = window.ist || {};

        // View 
        ist.deliverycarrier.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#carrierdeliveryBinding")[0],
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
            };
        })(carrierdeliveryViewModel);

        // Initialize the view model
        if (ist.deliverycarrier.view.bindingRoot) {
            carrierdeliveryViewModel.initialize(ist.deliverycarrier.view);
        }
        return ist.deliverycarrier.view;
    });