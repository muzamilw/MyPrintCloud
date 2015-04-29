﻿/*
    Data service module with ajax calls to the server
*/
define("common/addCostCenter.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get cost centers for product dialog
                    amplify.request.define('getCostCentersForProduct', 'ajax', {
                        url: ist.siteUrl + '/Api/ProductCostCenter',
                        dataType: 'json',
                        type: 'GET'
                    });

                    isInitialized = true;
                }
            },
             // get Cost centres for company
        getCostCentersForProduct = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getCostCentersForProduct',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        };

        return {
            getCostCentersForProduct: getCostCentersForProduct
        };
    })();

    return dataService;
});