define("paperSheet/paperSheet.view",
    ["jquery", "paperSheet/paperSheet.viewModel"], function ($, paperSheetViewModel) {

    	var ist = window.ist || {};

    	// View 
    	ist.paperSheet.view = (function (specifiedViewModel) {
    		var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#paperSheetBinding")[0],
                // Show Activity the dialog
                showPaperSheetDialog = function () {
                    $("#paperSheetDialog").modal("show");
                },
                // Hide Activity the dialog
                hidePaperSheetDialog = function () {
                    $("#paperSheetDialog").modal("hide");
                },
                // Initialize Label Popovers
                initializeLabelPopovers = function () {
                    // ReSharper disable UnknownCssClass
                    $('.bs-example-tooltips a').popover();
                    // ReSharper restore UnknownCssClass
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
    			showPaperSheetDialog: showPaperSheetDialog,
    			hidePaperSheetDialog: hidePaperSheetDialog,
    			initializeLabelPopovers: initializeLabelPopovers
    		};
    	})(paperSheetViewModel);

    	// Initialize the view model
    	if (ist.paperSheet.view.bindingRoot) {
    		paperSheetViewModel.initialize(ist.paperSheet.view);
    	}
    	return ist.paperSheet.view;
    });