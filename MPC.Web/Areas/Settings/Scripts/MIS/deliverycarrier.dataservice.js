﻿/*
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

                    // Define request to get DeliveryCarrier
                    amplify.request.define('getDeliveryCarrierDetail', 'ajax', {
                        url: ist.siteUrl + '/Api/Deliverycarrier',
                        dataType: 'json',
                        type: 'GET'
                    }),
                    // Define request to Save DeliveryCarrier
                    amplify.request.define('saveDeliveryCarrier', 'ajax', {
                        url: ist.siteUrl + '/Api/Deliverycarrier',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });

                    isInitialized = true;
                }
            },
            // GetAll DeliveryCarrier By Page
            getDeliveryCarrierDetail = function (callbacks) {
                initialize();
                return amplify.request
                ({
                    resourceId: 'getDeliveryCarrierDetail',
                    success: callbacks.success,
                    error: callbacks.error
                    
                });

            },
            
            // Save Delivery Carrier
	        saveDeliveryCarrier = function (param, callbacks) {
	            initialize();
	            return amplify.request({
	                resourceId: 'saveDeliveryCarrier',
	                success: callbacks.success,
	                error: callbacks.error,
	                data: param
	            });
	        };
             

           

		return {
		    getDeliveryCarrierDetail: getDeliveryCarrierDetail,
		    saveDeliveryCarrier: saveDeliveryCarrier,
		    
		    
		};
	})();

	return dataService;
});