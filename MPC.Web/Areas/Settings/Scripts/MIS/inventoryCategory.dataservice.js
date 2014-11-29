define("inventoryCategory/inventoryCategory.dataservice", function () {
    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Stock Categories 
                    amplify.request.define('getStockCategories', 'ajax', {
                        url: ist.siteUrl + '/Api/InventoryCategory',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to delete Stock Category
                    amplify.request.define('deleteStockCategory', 'ajax', {
                        url: ist.siteUrl + '/Api/InventoryCategory',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'DELETE'
                    });
                    // Define request to save Stock Category
                    amplify.request.define('saveStockCategory', 'ajax', {
                        url: ist.siteUrl + '/Api/InventoryCategory',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to save New Stock Category
                    amplify.request.define('saveNewStockCategory', 'ajax', {
                        url: ist.siteUrl + '/Api/InventoryCategory',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'Put'
                    });
                    isInitialized = true;
                }
            },
            // get Stock Categories
            getStockCategories = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getStockCategories',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // delete Stock Category
            deleteStockCategory = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteStockCategory',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // Save New paper Sheet
            saveNewStockCategory = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveNewStockCategory',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
        // save Stock Category
        saveStockCategory = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'saveStockCategory',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        };

        return {
            getStockCategories: getStockCategories,
            deleteStockCategory: deleteStockCategory,
            saveNewStockCategory: saveNewStockCategory,
            saveStockCategory: saveStockCategory,
        };
    })();

    return dataService;
});