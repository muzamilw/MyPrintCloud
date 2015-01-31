/*
    Data service module with ajax calls to the server
*/
define("costcenter/costcenter.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get CostCenter
                    amplify.request.define('getCostCentersList', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenter',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    // Define request to delete CostCenter
                    amplify.request.define('deleteCostCenter', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenter',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    // Define request to save New CostCenter
                    amplify.request.define('saveNewCostCenter', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenter',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'Put'
                    });
                    // Define request to save Prefixes
                    amplify.request.define('saveCostCenter', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenter',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to get CostCenter
                    amplify.request.define('getCostCentreById', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenter',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Get Base Date for Collections
                    amplify.request.define('getBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenterBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
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
            // Get Cost Centers List
            getCostCentersList = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCostCentersList',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // Get Cost Centers by Id 
            getCostCentreById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCostCentreById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            deleteCostCenter = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteCostCenter',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // Save New paper Sheet
            saveNewCostCenter = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveNewCostCenter',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // Save Cost Center
            saveCostCenter = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveCostCenter',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };

        return {
            getCostCentersList:getCostCentersList,
            getCostCentreById: getCostCentreById,
            deleteCostCenter: deleteCostCenter,
            saveNewCostCenter:saveNewCostCenter,
            saveCostCenter: saveCostCenter,
            getBaseData: getBaseData
        };
    })();

    return dataService;
});