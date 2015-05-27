/*
    Data service module with ajax calls to the server
*/
define("itemJobStatus/itemJobStatus.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Items
                    amplify.request.define('getItems', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemJobStatus',
                        dataType: 'json',
                        type: 'GET'
                    }),
                    // Define request to Save Item
                    amplify.request.define('saveItem', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemJobStatus',
                        dataType: 'json',
                        type: 'Post'
                    });

                    isInitialized = true;
                }
            },

            // Save Item
            saveItem = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveItem',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            getItems = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getItems',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };


        return {
            getItems: getItems,
            saveItem: saveItem

        };
    })();

    return dataService;
});