define("lookupMethod/lookupMethod.view",
    ["jquery", "lookupMethod/lookupMethod.viewModel"], function ($, lookupMethodViewModel) {//lookupMethodViewModel

        var ist = window.ist || {};

        // View 
        ist.lookupMethod.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#divlookupMethodBinding")[0],

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
                initialize: initialize
            };
        })(lookupMethodViewModel);

        // Initialize the view model
        if (ist.lookupMethod.view.bindingRoot) {
            lookupMethodViewModel.initialize(ist.lookupMethod.view);
        }
        return ist.lookupMethod.view;
    });