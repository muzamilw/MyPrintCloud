define("stores/store.Product.dataservice", function () {
    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Store
                    amplify.request.define('getStores', 'ajax', {
                        url: ist.siteUrl + '/Api/Company',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
            // save Store
        getStores = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getStores',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        };

        return {
            getStores: getStores,
        };
    })();

    return dataService;
});