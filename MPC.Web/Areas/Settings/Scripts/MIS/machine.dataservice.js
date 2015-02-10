/*
    Data service module with ajax calls to the server
*/
define("machine/machine.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get MachineList
                    amplify.request.define('GetMachineList', 'ajax', {
                        url: ist.siteUrl + '/Api/Machine',
                        dataType: 'json',
                        type: 'GET'
                    });
                    //amplify.request.define('GetLookupMethodList', 'ajax', {
                    //    url: ist.siteUrl + '/Api/Machine',
                    //    dataType: 'json',
                    //    type: 'GET'
                    //});
                    amplify.request.define('getStockItemsListforProduct', 'ajax', {
                        url: ist.siteUrl + '/Api/StockItems',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('GetAllStockItemList', 'ajax', {
                        url: ist.siteUrl + '/Api/StockItems',
                        dataType: 'json',
                        type: 'GET'
                    });
                    //  Define request to archive MachineList
                    amplify.request.define('archiveMachine', 'ajax', {
                        url: ist.siteUrl + '/Api/Machine',
                        dataType: 'json',
                        type: 'POST'
                    });
                    //// Define request to save New MachineList
                    //amplify.request.define('saveNewMachine', 'ajax', {
                    //    url: ist.siteUrl + '/Api/Machine',
                    //    dataType: 'json',
                    //    decoder: amplify.request.decoders.istStatusDecoder,
                    //    type: 'Put'
                    //});
                    //// Define request to save Machine
                    //amplify.request.define('saveMachine', 'ajax', {
                    //    url: ist.siteUrl + '/Api/Machine',
                    //    dataType: 'json',
                    //    decoder: amplify.request.decoders.istStatusDecoder,
                    //    type: 'POST'
                    //});
                    // Define request to get Machine
                    amplify.request.define('getMachineById', 'ajax', {
                        url: ist.siteUrl + '/Api/Machine',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
            GetMachineList = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'GetMachineList',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // Get Machine List
            //GetLookupMethodList = function (params, callbacks) {
            //    initialize();
            //    return amplify.request({
            //        resourceId: 'GetLookupMethodList',
            //        success: callbacks.success,
            //        error: callbacks.error,
            //        data: params
            //    });
            //},
          GetAllStockItemList = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'GetAllStockItemList',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            getStockItemsList = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getStockItemsListforProduct',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
             //Get Machine by Id 
            getMachineById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getMachineById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
        archiveMachine = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'archiveMachine',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        };
        //// Save New paper Sheet
        //saveNewMachine = function (param, callbacks) {
        //    initialize();
        //    return amplify.request({
        //        resourceId: 'saveNewMachine',
        //        success: callbacks.success,
        //        error: callbacks.error,
        //        data: param
        //    });
        //},
        //// Save Cost Center
        //saveMachine = function (param, callbacks) {
        //    initialize();
        //    return amplify.request({
        //        resourceId: 'saveMachine',
        //        success: callbacks.success,
        //        error: callbacks.error,
        //        data: param
        //    });
        //};

        return {
            GetMachineList: GetMachineList,
            getMachineById: getMachineById,
            //GetLookupMethodList: GetLookupMethodList,
            getStockItemsList: getStockItemsList,
            archiveMachine: archiveMachine,
            GetAllStockItemList: GetAllStockItemList
            
        };
    })();

    return dataService;
});