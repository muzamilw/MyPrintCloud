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
	                // Define request to Save Address
	                amplify.request.define('saveAddress', 'ajax', {
	                    url: ist.siteUrl + '/Api/Address',
	                    dataType: 'json',
	                    decoder: amplify.request.decoders.istStatusDecoder,
	                    type: 'POST'
	                });
	                // Define request to Save Company Contact
	                amplify.request.define('saveCompanyContact', 'ajax', {
	                    url: ist.siteUrl + '/Api/CompanyContact',
	                    dataType: 'json',
	                    decoder: amplify.request.decoders.istStatusDecoder,
	                    type: 'POST'
	                });
	            };
	        },
            // Save Address
            saveAddress = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveAddress',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // Save Company Contact
            saveCompanyContact = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveCompanyContact',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
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
		    getSuppliers: getSuppliers,
		    saveCompanyContact: saveCompanyContact,
		    saveAddress: saveAddress
		};
	})();

	return dataService;
});