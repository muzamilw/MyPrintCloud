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
                    // Define request to Get Base Data For Estimate
                    amplify.request.define('getBaseDataForEstimate', 'ajax', {
                        url: ist.siteUrl + '/Api/EstimateBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                   
                    // Define request to get Inquiries
                    amplify.request.define('getInquiries', 'ajax', {
                        url: ist.siteUrl + '/Api/Inquiry',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get Estimates
                    amplify.request.define('getEstimates', 'ajax', {
                        url: ist.siteUrl + '/Api/Estimate',
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
                    // Define request to estimate Deep Copy
                    amplify.request.define('copyEstimate', 'ajax', {
                        url: ist.siteUrl + '/Api/CopyEstimate',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'Get'
                    });
                    // Define request to Order Deep Copy
                    amplify.request.define('copyOrder', 'ajax', {
                        url: ist.siteUrl + '/Api/CopyOrder',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'Get'
                    });
                    // Define request to get inquiry by id
                    amplify.request.define('getInquiry', 'ajax', {
                        url: ist.siteUrl + '/Api/Inquiry',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });
                    // Define request to progress Inquiry To Estimate
                    amplify.request.define('progressInquiryToEstimate', 'ajax', {
                        url: ist.siteUrl + '/Api/InquiryProgress',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });
                    // Define update estimate and order on progress to order
                    amplify.request.define('progressEstimateToOrder', 'ajax', {
                        url: ist.siteUrl + '/Api/ProgressEstimate',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });

                    // Define request to save Order
                    amplify.request.define('saveOrder', 'ajax', {
                        url: ist.siteUrl + '/Api/Order',
                        dataType: 'json',
                        dataMap: JSON.stringify,
                        contentType: "application/json; charset=utf-8",
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });

                    // Define request to save Inquiry
                    amplify.request.define('saveInquiry', 'ajax', {
                        url: ist.siteUrl + '/Api/Inquiry',
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

                    // Define request to Progress Estimate To order
                    amplify.request.define('progressOrderToEstimate', 'ajax', {
                        url: ist.siteUrl + '/Api/ProgressEstimate',
                        dataType: 'json',
                        dataMap: JSON.stringify,
                        contentType: "application/json; charset=utf-8",
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to get order by id
                    amplify.request.define('getBaseDataForCompany', 'ajax', {
                        url: ist.siteUrl + '/Api/OrderBaseForCompany',
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
                    
                    // Define request to Download Artwork of the order
                    amplify.request.define('downloadOrderArtwork', 'ajax', {
                        url: ist.siteUrl + '/Api/DownloadArtwork',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    // Define request to get base data for inquiry
                    amplify.request.define('getBaseDataForInquiry', 'ajax', {
                        url: ist.siteUrl + '/Api/InquiryBase',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });
                    // Define request to Get Inquiry Items
                    amplify.request.define('getInquiryItems', 'ajax', {
                        url: ist.siteUrl + '/Api/InquiryItem',
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
            // Get base data For Estimate
            getBaseDataForEstimate = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseDataForEstimate',
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
            // copy Estimate
            copyEstimate = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'copyEstimate',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // copy Order
            copyOrder= function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'copyOrder',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Get Get Inquiry Items
            getInquiryItems = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getInquiryItems',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Get Inquiry by id 
            getInquiry = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getInquiry',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            //progress Inquiry To Estimate
            progressInquiryToEstimate = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'progressInquiryToEstimate',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            //progress Estimate to order
            progressEstimateToOrder = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'progressEstimateToOrder',
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
            // Get Inquiries
            getInquiries = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getInquiries',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error
                });
            },
            //get Estimates
            getEstimates = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getEstimates',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error
                });
            },
            // Delete Orders
            deleteOrder = function(params, callbacks) {
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
            //Save Inquiry
            saveInquiry = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveInquiry',
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
            //Progress Order To Estimate
            progressOrderToEstimate = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'progressOrderToEstimate',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // get Stock items
            getInventoriesList = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getInventoriesList',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            downloadOrderArtwork = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'downloadOrderArtwork',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            
             // get Base Data For Inquiry
            getBaseDataForInquiry = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseDataForInquiry',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // get Cost centres for company
            getCostCenters = function(params, callbacks) {
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
            getInquiry: getInquiry,
            getOrders: getOrders,
            copyEstimate: copyEstimate,
            getInquiries: getInquiries,
            getEstimates: getEstimates,
            saveOrder: saveOrder,
            saveInquiry: saveInquiry,
            archiveOrder: archiveOrder,
            getBaseData: getBaseData,
            cloneOrder: cloneOrder,
            progressOrderToEstimate: progressOrderToEstimate,
            getBaseDataForCompany: getBaseDataForCompany,
            getCostCenters: getCostCenters,
            getInventoriesList: getInventoriesList,
            deleteOrder: deleteOrder,
            progressInquiryToEstimate: progressInquiryToEstimate,
            progressEstimateToOrder: progressEstimateToOrder,
            getInquiryItems: getInquiryItems,
            getBaseDataForInquiry: getBaseDataForInquiry,
            getBaseDataForEstimate: getBaseDataForEstimate,
            downloadOrderArtwork: downloadOrderArtwork,
            copyOrder:copyOrder


        };
    })();

    return dataService;
});