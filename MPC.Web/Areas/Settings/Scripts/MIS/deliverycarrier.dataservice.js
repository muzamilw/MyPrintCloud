/*
    Data service module with ajax calls to the server
*/
define("deliverycarrier/deliverycarrier.dataservice", function () {

	// Data service for forecast 
	var dataService = (function () {
	    var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Prefixes
                    amplify.request.define('getDeliveryCarrierDetail', 'ajax', {
                        url: ist.siteUrl + '/Api/Deliverycarrier',
                        dataType: 'json',
                        type: 'GET'
                    });

                    isInitialized = true;
                }
            },
            // Get Prefixes by organisation Id 
            getDeliveryCarrierDetail = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getDeliveryCarrierDetail',
                    success: callbacks.success,
                    error: callbacks.error,
                });

            };

           

		return {
		    getDeliveryCarrierDetail: getDeliveryCarrierDetail
		    
		};
	})();

	return dataService;
});