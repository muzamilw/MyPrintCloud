/*
    Data service module with ajax calls to the server
*/
define("invoice/invoice.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Invoice by id
                    amplify.request.define('getBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/InvoiceBase',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get invoices
                    amplify.request.define('getInvoices', 'ajax', {
                        url: ist.siteUrl + '/Api/InvoiceList',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get Invoice by id
                    amplify.request.define('getInvoice', 'ajax', {
                        url: ist.siteUrl + '/Api/Invoice',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });
                    // Define request to get order by id
                    amplify.request.define('getBaseDataForCompany', 'ajax', {
                        url: ist.siteUrl + '/Api/OrderBaseForCompany',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to save invoice
                    amplify.request.define('saveInvoice', 'ajax', {
                        url: ist.siteUrl + '/Api/Invoice',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
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
            // Get Invoice by id 
            getInvoice = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getInvoice',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Get Invoices
            getInvoices = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getInvoices',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error
                });
            },
            getBaseDataForCompany = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseDataForCompany',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Save Invoice
            saveInvoice = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveInvoice',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },

            // Archive Invoice
            archiveInvoice = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'archiveInvoice',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };
         
        

        return {
            getInvoices: getInvoices,
            getBaseData: getBaseData,
            getInvoice: getInvoice,
            getBaseDataForCompany: getBaseDataForCompany,
            saveInvoice: saveInvoice
        };
    })();

    return dataService;
});