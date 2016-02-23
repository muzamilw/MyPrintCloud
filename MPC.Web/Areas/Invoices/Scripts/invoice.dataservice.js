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
                        dataMap: JSON.stringify,
                        contentType: "application/json; charset=utf-8",
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    
                    // Define request to archive invoice Permanently
                    amplify.request.define('archiveInvoicePermanently', 'ajax', {
                        url: ist.siteUrl + '/Api/Invoice',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'DELETE'
                    });

                    // Define request to Download Artwork of the order
                    amplify.request.define('exportInvoice', 'ajax', {
                        url: ist.siteUrl + '/Api/ExportInvoice',
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

           archiveInvoicePermanently = function (param, callbacks) {
               initialize();
               return amplify.request({
                   resourceId: 'archiveInvoicePermanently',
                   success: callbacks.success,
                   error: callbacks.error,
                   data: param
               });
           },
             // exportInvoice
        exportInvoice = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'exportInvoice',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        }

            

        return {
            getInvoices: getInvoices,
            getBaseData: getBaseData,
            getInvoice: getInvoice,
            getBaseDataForCompany: getBaseDataForCompany,
            saveInvoice: saveInvoice,
            archiveInvoicePermanently: archiveInvoicePermanently,
            exportInvoice: exportInvoice
        };
    })();

    return dataService;
});