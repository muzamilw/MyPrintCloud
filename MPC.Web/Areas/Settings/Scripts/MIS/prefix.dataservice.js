/*
    Data service module with ajax calls to the server
*/
define("prefix/prefix.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Prefixes
                    amplify.request.define('getPrefixesDetail', 'ajax', {
                        url: ist.siteUrl + '/Api/Prefix',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    // Define request to save Prefixes
                    amplify.request.define('savePrefixes', 'ajax', {
                        url: ist.siteUrl + '/Api/Prefix',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    isInitialized = true;
                }
            },
            // Get Prefixes by organisation Id 
            getPrefixesDetail = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPrefixesDetail',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Save Prefixes
            savePrefixes = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'savePrefixes',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };

        return {
            getPrefixesDetail: getPrefixesDetail,
            savePrefixes: savePrefixes,
        };
    })();

    return dataService;
});