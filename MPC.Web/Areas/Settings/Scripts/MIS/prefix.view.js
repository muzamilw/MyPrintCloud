define("prefix/prefix.view",
    ["jquery", "prefix/prefix.viewModel"], function ($, prefixViewModel) {

        var ist = window.ist || {};

        // View 
        ist.prefix.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#prefixesBinding")[0],
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
        })(prefixViewModel);

        // Initialize the view model
        if (ist.prefix.view.bindingRoot) {
            prefixViewModel.initialize(ist.prefix.view);
        }
        return ist.prefix.view;
    });