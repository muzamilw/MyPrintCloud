/*
    Data service module with ajax calls to the server
*/
define("liveJobs/liveJobs.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Items
                    amplify.request.define('getItems', 'ajax', {
                        url: ist.siteUrl + '/Api/LiveJobs',
                        dataType: 'json',
                        type: 'GET'
                    }),
                    // Define request to get Items
                    amplify.request.define('getBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/LiveJobsBase',
                        dataType: 'json',
                        type: 'GET'
                    }),

                    // Define request to get Items
                    amplify.request.define('downloadArtwork', 'ajax', {
                        url: ist.siteUrl + '/Production/Home/Test',
                        dataType: 'json',
                        type: 'Post'
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
        downloadArtwork = function (callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'downloadArtwork',
                success: callbacks.success,
                error: callbacks.error,
            });
        },


        getBaseData = function (callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getBaseData',
                success: callbacks.success,
                error: callbacks.error
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
            saveItem: saveItem,
            getBaseData: getBaseData,
            downloadArtwork: downloadArtwork

        };
    })();

    return dataService;
});