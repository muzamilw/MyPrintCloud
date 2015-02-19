/*
    Data service module with ajax calls to the server
*/
define("common/stockItem.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {
                    
                    // Define request to get stock items
                    amplify.request.define('getStockItems', 'ajax', {
                        url: ist.siteUrl + '/Api/StockItems',
                        dataType: 'json',
                        type: 'GET'
                    });

                    isInitialized = true;
                }
            },
            // Get Stock Items
            getStockItems = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getStockItems',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            };
        
        return {
            getStockItems: getStockItems
        };
    })();

    return dataService;
});