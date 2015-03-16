define("sectionflags/sectionflags.view",
    ["jquery", "sectionflags/sectionflags.viewModel"], function ($, sectionflagsViewModel) {

        var ist = window.ist || {};

        // View 
        ist.sectionflags.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#sectionflagsBinding")[0],
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
        })(sectionflagsViewModel);

        // Initialize the view model
        if (ist.sectionflags.view.bindingRoot) {
            sectionflagsViewModel.initialize(ist.sectionflags.view);
        }
        return ist.sectionflags.view;
    });