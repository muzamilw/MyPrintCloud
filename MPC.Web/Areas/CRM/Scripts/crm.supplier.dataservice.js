define("crm/crm.supplier.dataservice", function () {
	// Data service for forecast 
	var dataService = (function () {
	    var // True if initialized
	        isInitialized = false,
	        // Initialize
	        initialize = function() {
	            if (!isInitialized) {
	                // Define request to get Suppliers
	                amplify.request.define('getSuppliers', 'ajax', {
	                    url: ist.siteUrl + '/Api/CrmSupplier',
	                    dataType: 'json',
	                    type: 'GET'
	                });
	            };
	        },
	        // get Suppliers
	        getSuppliers = function (params, callbacks) {
	            initialize();
	            return amplify.request({
	                resourceId: 'getSuppliers',
	                success: callbacks.success,
	                error: callbacks.error,
	                data: params
	            });
	        };

		return {
		    getSuppliers: getSuppliers
		};
	})();

	return dataService;
});