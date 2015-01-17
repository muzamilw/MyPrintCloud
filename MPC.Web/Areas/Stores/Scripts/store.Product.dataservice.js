define("stores/store.Product.dataservice", function () {
    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {
                    // Define request to get items
                    amplify.request.define('getItems', 'ajax', {
                        url: ist.siteUrl + '/Api/Item',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get base data
                    amplify.request.define('getBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Store
                    amplify.request.define('getCompanyProduct', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyProduct',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get item by id
                    amplify.request.define('getItem', 'ajax', {
                        url: ist.siteUrl + '/Api/Item',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });
                    // Define request to get stock items
                    amplify.request.define('getStockItems', 'ajax', {
                        url: ist.siteUrl + '/Api/StockItems',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get item price matrices for Item by FlagId
                    amplify.request.define('getItemPriceMatricesForItemByFlagId', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemPriceMatrix',
                        dataType: 'json',
                        type: 'GET'
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
            // Get Items
            getItems = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getItems',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Get Item by id 
            getItem = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getItem',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
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
            
            // Get ItemPriceMatrices For Item By FlagId
            getItemPriceMatricesForItemByFlagId = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getItemPriceMatricesForItemByFlagId',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Get Company Product
            getCompanyProduct = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCompanyProduct',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };


        return {
            getBaseData: getBaseData,
            getItem: getItem,
            getCompanyProduct: getCompanyProduct,
            getStockItems: getStockItems,
            getItemPriceMatricesForItemByFlagId: getItemPriceMatricesForItemByFlagId,
            getItems: getItems
        };
    })();

    return dataService;
});