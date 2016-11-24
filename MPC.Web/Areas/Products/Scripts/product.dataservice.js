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
                        decoder: amplify.request.decoders.istStatusDecoder,
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
                    // Define request to Download Artwork of the order
                    amplify.request.define('exportItems', 'ajax', {
                        url: ist.siteUrl + '/Api/ExportItem',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    // Define request to get base data
                    amplify.request.define('getBaseDataForProduct', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemBase',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });

                    // Define request to get item price matrices for Item by FlagId
                    amplify.request.define('getItemPriceMatricesForItemByFlagId', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemPriceMatrix',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get product category childs
                    amplify.request.define('getProductCategoryChildsForProduct', 'ajax', {
                        url: ist.siteUrl + '/Api/ProductChildCategory',//ProductCategory
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to delete product category childs
                    amplify.request.define('deleteProductCategoryChildsForProduct', 'ajax', {
                        url: ist.siteUrl + '/Api/ProductCategory',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    // Define request to get base data
                    amplify.request.define('getBaseDataForDesignerCategory', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemDesignerTemplateBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    // Define request to get machines
                    amplify.request.define('getMachines', 'ajax', {
                        url: ist.siteUrl + '/Api/ProductMachines',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to clone Item
                    amplify.request.define('cloneItem', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemClone',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    
                    // Define request to get product category childs
                    amplify.request.define('getProductCategories', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyProductCategory',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    // Define request to get product
                    amplify.request.define('getProducts', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemsForDiscountVoucher',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to delete Item
                    amplify.request.define('deleteItem', 'ajax', {
                        url: ist.siteUrl + '/Api/DeleteItem',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'DELETE'
                    });
                    
                    // Define request to get Print Plan for section screen
                    amplify.request.define('getPtv', 'ajax', {
                        url: ist.siteUrl + '/Api/DrawPtv',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get Print Plan for section screen
                    amplify.request.define('getPtvCalculation', 'ajax', {
                        url: ist.siteUrl + '/Api/PtvCalculation',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    // Define request to Import product Csv
                    amplify.request.define('importProducts', 'ajax', {
                        url: ist.siteUrl + '/Api/ExportItem',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to send email notification to users
                    amplify.request.define('notifyUsersTemplateCreation', 'ajax', {
                        url: ist.siteUrl + '/Api/ProductTemplateNotification',
                        dataType: 'json',
                        type: 'GET'
                    });

                    isInitialized = true;
                }
            },
            // Get base data
            getBaseDataForProduct = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseDataForProduct',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Get Base Data for Designer Category
            getBaseDataForDesignerCategory = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseDataForDesignerCategory',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // Get Product Machines
            getMachines = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getMachines',
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
            
        // export Item
        exportItems = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'exportItems',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        },
            // Clone Item
            cloneItem = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'cloneItem',
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
            // get ProductCategories
            getProductCategories = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getProductCategories',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             // get Products
            getProducts = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getProducts',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             // get ProductCategories
        
            // get ProductCategory Childs
            getProductCategoryChildsForProduct = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getProductCategoryChildsForProduct',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // Delete Item
            deleteItem = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteItem',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // get PTV Calculation
            getPtvCalculation = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPtvCalculation',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },

             // import Company Contacts
        importProducts = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'importProducts',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        },
        notifyUsersTemplateCreation = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'notifyUsersTemplateCreation',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
            
        },

            // get Ptv
            getPtv = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPtv',
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
            getBaseDataForProduct: getBaseDataForProduct,
            getItemPriceMatricesForItemByFlagId: getItemPriceMatricesForItemByFlagId,
            getProductCategoryChildsForProduct: getProductCategoryChildsForProduct,
            getBaseDataForDesignerCategory: getBaseDataForDesignerCategory,
            getMachines: getMachines,
            cloneItem: cloneItem,
            getProductCategories: getProductCategories,
            getProducts: getProducts,
            deleteItem: deleteItem,
            getPtvCalculation: getPtvCalculation,
            getPtv: getPtv,
            exportItems: exportItems,
            importProducts: importProducts,
            notifyUsersTemplateCreation: notifyUsersTemplateCreation
        };
    })();

    return dataService;
});