/*
    Data service module with ajax calls to the server
*/
define("reportBanner/reportBanner.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Prefixes
                    //amplify.request.define('getPrefixesDetail', 'ajax', {
                    //    url: ist.siteUrl + '/Api/Prefix',
                    //    dataType: 'json',
                    //    type: 'GET'
                    //});
                    // Define request to get Prefixes
                    amplify.request.define('getStores', 'ajax', {
                        url: ist.siteUrl + '/Api/ReportNote',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getReportNote', 'ajax', {
                        url: ist.siteUrl + '/Api/ReportNote',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });
                    
                    // Define request to save invoice
                    amplify.request.define('saveReportNote', 'ajax', {
                        url: ist.siteUrl + '/Api/ReportNote',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    isInitialized = true;
                }
            },
           
            getStores = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getStores',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // save ReportNote
            saveReportNote = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveReportNote',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },

                getReportNote = function (param, callbacks) {
                    initialize();
                    return amplify.request({
                        resourceId: 'getReportNote',
                        success: callbacks.success,
                        error: callbacks.error,
                        data: param
                    });
            };

        return {
            getStores: getStores,
            saveReportNote: saveReportNote,
            getReportNote : getReportNote
        };
    })();

    return dataService;
});