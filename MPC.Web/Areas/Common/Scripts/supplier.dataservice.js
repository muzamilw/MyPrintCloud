/*
    Data service module with ajax calls to the server
*/
define("common/supplier.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Get Supplier 
                    amplify.request.define('getSuppliers', 'ajax', {
                        url: ist.siteUrl + '/Api/SupplierForInventory',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Get Base data
                    amplify.request.define('getBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/SupplierBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to save Supplier
                    amplify.request.define('saveSupplier', 'ajax', {
                        url: ist.siteUrl + '/Api/Supplier',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });

                    isInitialized = true;
                }
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
            },
             // Save Supplier
            saveSupplier = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveSupplier',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // get Base Data
            getBaseData = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseData',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            };
        return {
            getSuppliers: getSuppliers,
            getBaseData: getBaseData,
            saveSupplier: saveSupplier,
        };
    })();

    return dataService;
});