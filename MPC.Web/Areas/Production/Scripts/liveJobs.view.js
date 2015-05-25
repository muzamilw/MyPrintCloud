/*
    View for the Live Jobs. Used to keep the viewmodel clear of UI related logic
*/
define("liveJobs/liveJobs.view",
    ["jquery", "liveJobs/liveJobs.viewModel"], function ($, liveJobsViewModel) {

        var ist = window.ist || {};

        // View 
        ist.liveJobs.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                    // Binding root used with knockout
                bindingRoot = $("#liveJobsBinding")[0],

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

            };
        })(liveJobsViewModel);

        // Initialize the view model
        if (ist.liveJobs.view.bindingRoot) {
            liveJobsViewModel.initialize(ist.liveJobs.view);
        }
        return ist.liveJobs.view;
    });
