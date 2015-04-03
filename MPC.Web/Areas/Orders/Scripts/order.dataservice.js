/*
    Data service module with ajax calls to the server
*/
define("order/order.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {

                    // Define request to get order by id
                    amplify.request.define('getBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/OrderBase',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get orders
                    amplify.request.define('getOrders', 'ajax', {
                        url: ist.siteUrl + '/Api/Order',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to delete Order
                    amplify.request.define('deleteOrder', 'ajax', {
                        url: ist.siteUrl + '/Api/Order',
                        dataType: 'json',
                        type: 'DELETE'
                    });

                    // Define request to get order by id
                    amplify.request.define('getOrder', 'ajax', {
                        url: ist.siteUrl + '/Api/Order',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });

                    // Define request to save Order
                    amplify.request.define('saveOrder', 'ajax', {
                        url: ist.siteUrl + '/Api/Order',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });

                    // Define request to archive Order
                    amplify.request.define('archiveOrder', 'ajax', {
                        url: ist.siteUrl + '/Api/Order',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'DELETE'
                    });

                    // Define request to clone Order
                    amplify.request.define('cloneOrder', 'ajax', {
                        url: ist.siteUrl + '/Api/OrderClone',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });

                    // Define request to get order by id
                    amplify.request.define('getBaseDataForCompany', 'ajax', {
                        url: ist.siteUrl + '/Api/OrderBaseForCompany',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Items By CompanyId
                    amplify.request.define('getItemsByCompanyId', 'ajax', {
                        url: ist.siteUrl + '/Api/OrderRetailStore',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Items Details By Item Id
                    amplify.request.define('getItemsDetailsByItemId', 'ajax', {
                        url: ist.siteUrl + '/Api/OrderRetailStoreDetail',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get cost centers By CompanyId
                    amplify.request.define('getCostCentersByCompanyId', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyCostCenters',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Inventory Stock Items
                    amplify.request.define('getInventoriesList', 'ajax', {
                        url: ist.siteUrl + '/Api/Inventory',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Print Plan for section screen
                    amplify.request.define('getPTV', 'ajax', {
                        url: ist.siteUrl + '/Api/DrawPtv',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Best Press list for section screen with run wizard button
                    amplify.request.define('getBestPress', 'ajax', {
                        url: ist.siteUrl + '/Api/BestPress',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
            // Get base data
            getBaseData = function(callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseData',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Get Base For Company
            getBaseDataForCompany = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseDataForCompany',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Get Order by id 
            getOrder = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getOrder',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Get Orders
            getOrders = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getOrders',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error
                });
            },
             // Delete Orders
           deleteOrder = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteOrder',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error
                });
            },
            // Archive Order
            archiveOrder = function(param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'archiveOrder',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // Save Order
            saveOrder = function(param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveOrder',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // Clone Order
            cloneOrder = function(param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'cloneOrder',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // get Store by id
            getItemsByCompanyId = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getItemsByCompanyId',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // get Items Detail by id
            getItemsDetailsByItemId = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getItemsDetailsByItemId',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
                // get Stock items
            getInventoriesList = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getInventoriesList',
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
                    data: params
                });
            },
        // get Cost centres for company
        getCostCenters = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getCostCentersByCompanyId',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        };

        return {
            getOrder: getOrder,
            getOrders: getOrders,
            saveOrder: saveOrder,
            archiveOrder: archiveOrder,
            getBaseData: getBaseData,
            cloneOrder: cloneOrder,
            getBaseDataForCompany: getBaseDataForCompany,
            getItemsByCompanyId: getItemsByCompanyId,
            getCostCenters: getCostCenters,
            getInventoriesList: getInventoriesList,          
            getItemsDetailsByItemId: getItemsDetailsByItemId,
            deleteOrder: deleteOrder,
            getPTV: getPTV,
            getBestPress: getBestPress
        };
    })();

    return dataService;
});