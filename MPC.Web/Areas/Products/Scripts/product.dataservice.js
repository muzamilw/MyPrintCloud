﻿/*
    Data service module with ajax calls to the server
*/
define("product/product.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {


                    // Define request to get items
                    amplify.request.define('getItems', 'ajax', {
                        url: ist.siteUrl + '/Api/Item',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get item by id
                    amplify.request.define('getItem', 'ajax', {
                        url: ist.siteUrl + '/Api/Item',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });

                    // Define request to save Item
                    amplify.request.define('saveItem', 'ajax', {
                        url: ist.siteUrl + '/Api/Item',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });

                    // Define request to archive Item
                    amplify.request.define('archiveItem', 'ajax', {
                        url: ist.siteUrl + '/Api/Item',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'DELETE'
                    });

                    // Define request to get stock items
                    amplify.request.define('getStockItems', 'ajax', {
                        url: ist.siteUrl + '/Api/StockItems',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get base data
                    amplify.request.define('getBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemBase',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get item price matrices for Item by FlagId
                    amplify.request.define('getItemPriceMatricesForItemByFlagId', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemPriceMatrix',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get product category childs
                    amplify.request.define('getProductCategoryChilds', 'ajax', {
                        url: ist.siteUrl + '/Api/ProductCategory',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    // Define request to get base data
                    amplify.request.define('getBaseDataForDesignerCategory', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemDesignerTemplateBase',
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
            // Get Base Data for Designer Category
            getBaseDataForDesignerCategory = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseDataForDesignerCategory',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Get Stock Items
            getStockItems = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getStockItems',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Get Item by id 
            getItem = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getItem',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Get Items
            getItems = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getItems',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Archive Item
            archiveItem = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'archiveItem',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // Save Item
            saveItem = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveItem',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // Get ItemPriceMatrices For Item By FlagId
            getItemPriceMatricesForItemByFlagId = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getItemPriceMatricesForItemByFlagId',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // get ProductCategory Childs
            getProductCategoryChilds = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getProductCategoryChilds',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };

        return {
            getItem: getItem,
            getItems: getItems,
            saveItem: saveItem,
            archiveItem: archiveItem,
            getStockItems: getStockItems,
            getBaseData: getBaseData,
            getItemPriceMatricesForItemByFlagId: getItemPriceMatricesForItemByFlagId,
            getProductCategoryChilds: getProductCategoryChilds,
            getBaseDataForDesignerCategory: getBaseDataForDesignerCategory
        };
    })();

    return dataService;
});