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
                    amplify.request.define('getInventoriesList', 'ajax', {
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
                    // Define request to delete Inventory
                    amplify.request.define('deleteInventory', 'ajax', {
                        url: ist.siteUrl + '/Api/Inventory',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    // Define request to Get Inventory By Id
                    amplify.request.define('getInventoryById', 'ajax', {
                        url: ist.siteUrl + '/Api/Inventory',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to save Inventory
                    amplify.request.define('saveInventory', 'ajax', {
                        url: ist.siteUrl + '/Api/Inventory',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });

                    isInitialized = true;
                }
            },

            // get Stock items
            getInventoriesList = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getInventoriesList',
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
            // delete Inventory
            deleteInventory = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteInventory',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             // Get Inventory Detail By ID
            getInventoryById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getInventoryById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },

           // Save Inventory
            saveInventory = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveInventory',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };

        return {
            getInventoriesList: getInventoriesList,
            getInventoryBase: getInventoryBase,
            deleteInventory: deleteInventory,
            saveInventory: saveInventory,
            getInventoryById: getInventoryById,
        };
    })();

    return dataService;
});