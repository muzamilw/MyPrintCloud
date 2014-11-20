/*
    View for the tariff Type. Used to keep the viewmodel clear of UI related logic
*/
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
                // Initialize
                initialize = function () {
                	if (!bindingRoot) {
                		return;
                	}

                	// Handle Sorting
                	// handleSorting("tariffTypeTable", viewModel.sortOn, viewModel.sortIsAsc, viewModel.getTariffType);
                };
    		initialize();
    		return {
    			bindingRoot: bindingRoot,
    			viewModel: viewModel,
    			showPaperSheetDialog: showPaperSheetDialog,
    			hidePaperSheetDialog: hidePaperSheetDialog
    		};
    	})(paperSheetViewModel);

    	// Initialize the view model
    	if (ist.paperSheet.view.bindingRoot) {
    		paperSheetViewModel.initialize(ist.paperSheet.view);
    	}
    	return ist.paperSheet.view;
    });