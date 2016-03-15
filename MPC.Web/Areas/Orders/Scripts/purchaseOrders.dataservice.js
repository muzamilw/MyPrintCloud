/*
    Data service module with ajax calls to the server
*/
define("purchaseOrders/purchaseOrders.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {

                    // Define request to get Purchase Orders
                    amplify.request.define('getPurchaseOrders', 'ajax', {
                        url: ist.siteUrl + '/Api/PurchaseOrder',
                        dataType: 'json',
                        type: 'GET'
                    }),
                        // Define request to get order by id
                    amplify.request.define('getBaseDataForCompany', 'ajax', {
                        url: ist.siteUrl + '/Api/OrderBaseForCompany',
                        dataType: 'json',
                        type: 'GET'
                    }),
                        // Define request to get Purchase Detail By ID
                    amplify.request.define('getPurchaseOrderById', 'ajax', {
                        url: ist.siteUrl + '/Api/PurchaseOrder',
                        dataType: 'json',
                        type: 'GET'
                    }),
                        // Define request to get GRN Detail By ID
                    amplify.request.define('getGRNById', 'ajax', {
                        url: ist.siteUrl + '/Api/GoodsReceivedNote',
                        dataType: 'json',
                        type: 'GET'
                    }),
                        // Define request to get Items
                    amplify.request.define('downloadArtwork', 'ajax', {
                        url: ist.siteUrl + '/Production/Home/Test',
                        dataType: 'json',
                        type: 'Post'
                    }),
                        // Define request to get 
                    amplify.request.define('getBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/PurchaseBase',
                        dataType: 'json',
                        type: 'GET'
                    }),
                        // Define request to Delete Purchase Order
                    amplify.request.define('deletePurchaseOrder', 'ajax', {
                        url: ist.siteUrl + '/Api/PurchaseOrder',
                        dataType: 'json',
                        type: 'Delete'
                    }),
                        // Define request to Save GRN
                    amplify.request.define('saveGRN', 'ajax', {
                        url: ist.siteUrl + '/Api/GoodsReceivedNote',
                        dataType: 'json',
                        type: 'Post'
                    }),
                        // Define request to Save Purchase
                    amplify.request.define('savePurchase', 'ajax', {
                        url: ist.siteUrl + '/Api/PurchaseOrder',
                        dataType: 'json',
                        dataMap: JSON.stringify,
                        contentType: "application/json; charset=utf-8",
                        type: 'Post'
                    }),
                        // Define request to Export Purchase Order
                    amplify.request.define('exportPurchaseOrder', 'ajax', {
                        url: ist.siteUrl + '/Api/ExportPurchaseOrder',
                        dataType: 'json',
                        type: 'GET'
                    });

                    isInitialized = true;
                }
            },
            // Save Purchase
            savePurchase = function(param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'savePurchase',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // Save GRN
            saveGRN = function(param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveGRN',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // Delete Purchase Order
            deletePurchaseOrder = function(param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deletePurchaseOrder',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // Get Base For Company
            getBaseData = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseData',
                    data: params,
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
            getPurchaseOrderById = function(param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPurchaseOrderById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            getGRNById = function(param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getGRNById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            getPurchaseOrders = function(param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPurchaseOrders',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            exportPurchaseOrder = function(param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'exportPurchaseOrder',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };


        return {
            getPurchaseOrders: getPurchaseOrders,
            getPurchaseOrderById: getPurchaseOrderById,
            savePurchase: savePurchase,
            getBaseData: getBaseData,
            getBaseDataForCompany: getBaseDataForCompany,
            deletePurchaseOrder: deletePurchaseOrder,
            saveGRN: saveGRN,
            getGRNById: getGRNById,
            exportPurchaseOrder: exportPurchaseOrder
        };
    })();

    return dataService;
});