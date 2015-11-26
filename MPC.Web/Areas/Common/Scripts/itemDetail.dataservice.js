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
                    // Define request to get Print Plan for section screen
                    amplify.request.define('getPTVCalculation', 'ajax', {
                        url: ist.siteUrl + '/Api/PtvCalculation',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Best Press list for section screen with run wizard button
                    amplify.request.define('getBestPress', 'ajax', {
                        url: ist.siteUrl + '/Api/BestPress',
                        dataType: 'json',
                        contentType: 'application/json;charset=utf-8',
                        type: 'POST'
                    });
                    // Define request to update system cost centers for current section screen with wizard finish button
                    amplify.request.define('getUpdatedSystemCostCenters', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemSection',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to save section
                    amplify.request.define('updateItemSection', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemSection',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
            // get PTV Calculation
            getPTVCalculation = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPTVCalculation',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
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
            getBestPress = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBestPress',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: JSON.stringify(params)

                });
            },
            getUpdatedSystemCostCenters = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getUpdatedSystemCostCenters',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            updateItemSection = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'updateItemSection',
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
            getPTVCalculation: getPTVCalculation,
            getBestPress: getBestPress,
            getUpdatedSystemCostCenters: getUpdatedSystemCostCenters,
            updateItemSection: updateItemSection
        };
    })();

    return dataService;
});