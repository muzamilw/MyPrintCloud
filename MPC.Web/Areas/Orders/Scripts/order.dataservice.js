/*
    Data service module with ajax calls to the server
*/
define("order/order.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get order by id
                    amplify.request.define('getBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/Order',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get orders
                    amplify.request.define('getOrders', 'ajax', {
                        url: ist.siteUrl + '/Api/Order',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get order by id
                    amplify.request.define('getOrder', 'ajax', {
                        url: ist.siteUrl + '/Api/Order',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });

                    // Define request to save Order
                    amplify.request.define('saveOrder', 'ajax', {
                        url: ist.siteUrl + '/Api/Order',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });

                    // Define request to archive Order
                    amplify.request.define('archiveOrder', 'ajax', {
                        url: ist.siteUrl + '/Api/Order',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'DELETE'
                    });
                    
                    // Define request to clone Order
                    amplify.request.define('cloneOrder', 'ajax', {
                        url: ist.siteUrl + '/Api/OrderClone',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });

                    isInitialized = true;
                }
            },
            // Get base data
            getBaseData = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseData',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Get Order by id 
            getOrder = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getOrder',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Get Orders
            getOrders = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getOrders',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Archive Order
            archiveOrder = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'archiveOrder',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // Save Order
            saveOrder = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveOrder',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // Clone Order
            cloneOrder = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'cloneOrder',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };

        return {
            getOrder: getOrder,
            getOrders: getOrders,
            saveOrder: saveOrder,
            archiveOrder: archiveOrder,
            getBaseData: getBaseData,
            cloneOrder: cloneOrder
        };
    })();

    return dataService;
});