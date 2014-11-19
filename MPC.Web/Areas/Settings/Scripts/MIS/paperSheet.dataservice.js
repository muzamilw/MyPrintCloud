/*
    Data service module with ajax calls to the server
*/
define("paperSheet/paperSheet.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Paper Sheets 
                    amplify.request.define('getPaperSheets', 'ajax', {
                        url: ist.siteUrl + '/Api/PaperSheet',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get My Organization detail
                    amplify.request.define('getPaperSheetDetail', 'ajax', {
                        url: ist.siteUrl + '/Api/PaperSheet',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to save paper Sheet
                    amplify.request.define('saveMyPaperSheet', 'ajax', {
                        url: ist.siteUrl + '/Api/PaperSheet',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    isInitialized = true;
                }
            },
            // getPaperSheets
            getPaperSheets = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPaperSheets',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // getPaperSheetDetail
            getPaperSheetDetail = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPaperSheetDetail',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // Save My Organization
            saveMyPaperSheet = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveMyPaperSheet',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };

        return {
            getPaperSheets: getPaperSheets,
            getPaperSheetDetail: getPaperSheetDetail,
            saveMyPaperSheet: saveMyPaperSheet,
        };
    })();

    return dataService;
});