/*
    Data service module with ajax calls to the server
*/
define("inventory/inventory.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Stock Items
                    amplify.request.define('getInventories', 'ajax', {
                        url: ist.siteUrl + '/Api/Inventory',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get Inventory base 
                    amplify.request.define('getInventoryBase', 'ajax', {
                        url: ist.siteUrl + '/Api/InventoryBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to delete paper sheet
                    amplify.request.define('deletePaperSheet', 'ajax', {
                        url: ist.siteUrl + '/Api/PaperSheet',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    // Define request to save paper Sheet
                    amplify.request.define('savePaperSheet', 'ajax', {
                        url: ist.siteUrl + '/Api/PaperSheet',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to save New paper Sheet
                    amplify.request.define('saveNewPaperSheet', 'ajax', {
                        url: ist.siteUrl + '/Api/PaperSheet',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'Put'
                    });
                    isInitialized = true;
                }
            },
            // get Stock items
            getInventories = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getInventories',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             // Get My Organization Base
            getInventoryBase = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getInventoryBase',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // delete Paper Sheet
            deletePaperSheet = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deletePaperSheet',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // Save New paper Sheet
            saveNewPaperSheet = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveNewPaperSheet',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
        // Save paper Sheet
        savePaperSheet = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'savePaperSheet',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        };

        return {
            getInventories: getInventories,
            getInventoryBase: getInventoryBase,
        };
    })();

    return dataService;
});