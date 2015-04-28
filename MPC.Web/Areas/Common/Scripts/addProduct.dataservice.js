/*
    Data service module with ajax calls to the server
*/
define("common/addProduct.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Items By CompanyId
                    amplify.request.define('getItemsByCompanyId', 'ajax', {
                        url: ist.siteUrl + '/Api/OrderRetailStore',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Items Details By Item Id
                    amplify.request.define('getItemsDetailsByItemId', 'ajax', {
                        url: ist.siteUrl + '/Api/OrderRetailStoreDetail',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
             // get Items Detail by id
            getItemsDetailsByItemId = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getItemsDetailsByItemId',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // get Store by id
            getItemsByCompanyId = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getItemsByCompanyId',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };

        return {
            getItemsByCompanyId: getItemsByCompanyId,
            getItemsDetailsByItemId: getItemsDetailsByItemId
        };
    })();

    return dataService;
});