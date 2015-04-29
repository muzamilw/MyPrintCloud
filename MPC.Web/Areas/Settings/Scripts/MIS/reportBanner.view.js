define("reportBanner/reportBanner.view",
    ["jquery", "reportBanner/reportBanner.viewModel"], function ($, reportNoteViewModel) {

        var ist = window.ist || {};

        // View 
        ist.reportNote.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#reportBannerBinding")[0],
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
        })(reportNoteViewModel);

        // Initialize the view model
        if (ist.reportNote.view.bindingRoot) {
            reportNoteViewModel.initialize(ist.reportNote.view);
        }
        return ist.reportNote.view;
    });