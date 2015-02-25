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
                    // Define request to delete paper sheet
                    amplify.request.define('deletePaperSheet', 'ajax', {
                        url: ist.siteUrl + '/Api/PaperSheet',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    // Define request to save paper Sheet
                    amplify.request.define('savePaperSheet', 'ajax', {
                        url: ist.siteUrl + '/Api/PaperSheet',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to save New paper Sheet
                    amplify.request.define('saveNewPaperSheet', 'ajax', {
                        url: ist.siteUrl + '/Api/PaperSheet',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to get base data
                    amplify.request.define('getBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/PaperSheetBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
            // get Paper Sheets
            getPaperSheets = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPaperSheets',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // delete Paper Sheet
            deletePaperSheet = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deletePaperSheet',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // Save New paper Sheet
            saveNewPaperSheet = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveNewPaperSheet',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // Get base data
            getBaseData = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseData',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
        // Save paper Sheet
        savePaperSheet = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'savePaperSheet',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        };

        return {
            getPaperSheets: getPaperSheets,
            deletePaperSheet: deletePaperSheet,
            saveNewPaperSheet: saveNewPaperSheet,
            savePaperSheet: savePaperSheet,
            getBaseData: getBaseData
        };
    })();

    return dataService;
});