/*
    View for the tariff Type. Used to keep the viewmodel clear of UI related logic
*/
define("crm/crm.supplier.view",
    ["jquery", "crm/crm.supplier.viewModel"], function ($, crmSupplierViewModel) {

    	var ist = window.ist || {};

    	// View 
    	ist.crmSupplier.view = (function (specifiedViewModel) {
    		var // View model 
                viewModel = specifiedViewModel,
               // binding root
               bindingRoot = $("#crmSuppliers")[0],
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
    	})(crmSupplierViewModel);

    	// Initialize the view model
    	if (ist.crmSupplier.view.bindingRoot) {
    	    crmSupplierViewModel.initialize(ist.crmSupplier.view);
    	}
    	return ist.crmSupplier.view;
    });