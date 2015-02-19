/*
    View for the tariff Type. Used to keep the viewmodel clear of UI related logic
*/
define("crm/crm.supplier.view",
    ["jquery", "crm/crm.viewModel"], function ($, crmSupplierViewModel) {

        var ist = window.ist || {};
        ist.crm = ist.crm || {};
        ist.crm.supplier = ist.crm.supplier || {};
        //Setting flag to false, it indicates that current screen is Suppliers
        crmSupplierViewModel.isProspectOrCustomerScreen(false);
    	// View 
    	ist.crm.supplier.view = (function (specifiedViewModel) {
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
    	if (ist.crm.supplier.view.bindingRoot) {
    	    crmSupplierViewModel.initialize(ist.crm.supplier.view);
    	}
    	return ist.crm.supplier.view;
    });