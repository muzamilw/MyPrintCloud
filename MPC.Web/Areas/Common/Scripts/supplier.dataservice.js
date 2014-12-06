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
            };

        return {
            getSuppliers: getSuppliers,
        };
    })();

    return dataService;
});