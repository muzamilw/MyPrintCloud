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
                        type: 'POST'
                    });
                    amplify.request.define('saveQuestionVariable', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenterTree',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to save Prefixes
                    amplify.request.define('saveCostCenter', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenter',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    amplify.request.define('getCostCentreById', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenter',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get CostCenter
                    amplify.request.define('getCostCentreAnswerList', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenterTree',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Get Base Date for Collections
                    amplify.request.define('getBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenterBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Get Tree Data
                    amplify.request.define('GetTreeListById', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenterTree',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('deleteQuestionVariable', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenterTree',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    
                    
                    amplify.request.define('deleteAnswerVariable', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenterTree',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    amplify.request.define('saveNewQuestionVariable', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenterTree',
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'Put'
                    });
                    amplify.request.define('saveVariable', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCentreMatrix',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    amplify.request.define('DeleteMatrixVariable', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCentreMatrix',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    // Define request to Delete cost centre Permanently
                    amplify.request.define('deleteCostCentre', 'ajax', {
                        url: ist.siteUrl + '/Api/CostCenter',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'DELETE'
                    });
                    isInitialized = true;
                }
            },
            saveNewQuestionVariable = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveNewQuestionVariable',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: JSON.stringify(param)
                });
            };
             // Get base data
            getBaseData = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseData',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            GetTreeListById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'GetTreeListById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };
        getCostCentreAnswerList = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getCostCentreAnswerList',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        };
        DeleteMatrixVariable = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'DeleteMatrixVariable',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        },
        deleteQuestionVariable = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'deleteQuestionVariable',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        },
        deleteAnswerVariable = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'deleteAnswerVariable',
                success: callbacks.success,
                error: callbacks.error,
                data: params
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
        saveQuestionVariable = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'saveQuestionVariable',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        },
         saveVariable = function (param, callbacks) {
             initialize();
             return amplify.request({
                 resourceId: 'saveVariable',
                 success: callbacks.success,
                 error: callbacks.error,
                 data: param // JSON.stringify(param)
             });
         },
         deleteCostCentre = function (param, callbacks) {
             initialize();
             return amplify.request({
                 resourceId: 'deleteCostCentre',
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
            getCostCentersList: getCostCentersList,
            getCostCentreById: getCostCentreById,
            deleteCostCenter: deleteCostCenter,
            saveNewCostCenter: saveNewCostCenter,
            saveCostCenter: saveCostCenter,
            getBaseData: getBaseData,
            GetTreeListById: GetTreeListById,
            getCostCentreAnswerList: getCostCentreAnswerList,
            saveQuestionVariable: saveQuestionVariable,
            deleteQuestionVariable: deleteQuestionVariable,
            deleteAnswerVariable: deleteAnswerVariable,
            saveNewQuestionVariable: saveNewQuestionVariable,
            saveVariable: saveVariable,
            DeleteMatrixVariable: DeleteMatrixVariable,
            deleteCostCentre: deleteCostCentre
        };
    })();

    return dataService;
});