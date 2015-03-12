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
                    
                    amplify.request.define('getStockItemsListforProduct', 'ajax', {
                        url: ist.siteUrl + '/Api/StockItems',
                        dataType: 'json',
                        type: 'GET'
                    });
                   
                    //  Define request to archive MachineList
                    amplify.request.define('deleteMachine', 'ajax', {
                        url: ist.siteUrl + '/Api/Machine',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                  
                  
                    //// Define request to save Machine
                  
                    amplify.request.define('saveMachine', 'ajax', {
                        url: ist.siteUrl + '/Api/Machine',
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });

                    // Define request to get Machine
                    amplify.request.define('getMachineById', 'ajax', {
                        url: ist.siteUrl + '/Api/Machine',
                        dataType: 'json',
                        type: 'GET'
                    });

                    amplify.request.define('saveNewMachine', 'ajax', {
                        url: ist.siteUrl + '/Api/Machine',
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'Put'
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
        deleteMachine = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'deleteMachine',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        },
            //// Save Machine 
        saveMachine = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'saveMachine',
                success: callbacks.success,
                error: callbacks.error,
                data: JSON.stringify(param)
            });
        },
          saveNewMachine = function (param, callbacks) {
              initialize();
              return amplify.request({
                  resourceId: 'saveNewMachine',
                  success: callbacks.success,
                  error: callbacks.error,
                  data: JSON.stringify(param)
              });
          };

        
        return {
            GetMachineList: GetMachineList,
            getMachineById: getMachineById,
            getStockItemsList: getStockItemsList,
            deleteMachine: deleteMachine,
            saveMachine: saveMachine,
            saveNewMachine: saveNewMachine
            
        };
    })();

    return dataService;
});