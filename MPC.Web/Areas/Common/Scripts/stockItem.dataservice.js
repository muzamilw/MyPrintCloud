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

                    // Define request to get stock Categories
                    amplify.request.define('getStockCategories', 'ajax', {
                        url: ist.siteUrl + '/Api/StockCategory',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get  stock sub Categories
                    amplify.request.define('getStockSubCategories', 'ajax', {
                        url: ist.siteUrl + '/Api/StockCategory',
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
            },
        // Get Stock Categories
        getStockCategories = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getStockCategories',
                data: params,
                success: callbacks.success,
                error: callbacks.error,
            });
        };

        // Get Stock Categories
        getStockSubCategories = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getStockSubCategories',
                data: params,
                success: callbacks.success,
                error: callbacks.error,
            });
        };
        
        return {
            getStockItems: getStockItems,
            getStockCategories: getStockCategories,
            getStockSubCategories: getStockSubCategories
        };
    })();

    return dataService;
});