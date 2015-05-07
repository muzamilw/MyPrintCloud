/*
    Data service module with ajax calls to the server
*/
define("common/itemDetail.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {
                    // Define request to get base data
                    amplify.request.define('getBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemDetailBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Print Plan for section screen
                    amplify.request.define('getPTV', 'ajax', {
                        url: ist.siteUrl + '/Api/DrawPtv',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
             // get Stock items
            getPTV = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPTV',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // Get base data
            getBaseData = function(callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseData',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            };
           

        return {
            getBaseData: getBaseData,
            getPTV: getPTV,
        };
    })();

    return dataService;
});