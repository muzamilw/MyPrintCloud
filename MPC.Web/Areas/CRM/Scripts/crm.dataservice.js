define("crm/crm.dataservice", function () {
    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {
                    amplify.request.define('getDataForOrderTab', 'ajax', {
                        url: ist.siteUrl + '/Api/OrdersForCrm',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getDataForInvoiceTab', 'ajax', {
                        url: ist.siteUrl + '/Api/Invoice',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getCompanies', 'ajax', {
                        url: ist.siteUrl + '/Api/Customer',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Store By StoreId
                    amplify.request.define('getStoreById', 'ajax', {
                        url: ist.siteUrl + '/Api/Customer',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Address
                    amplify.request.define('searchAddress', 'ajax', {
                        url: ist.siteUrl + '/Api/Address',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get CompanyContact
                    amplify.request.define('searchCompanyContact', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContact',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Save Company Contact
                    amplify.request.define('saveCompanyContact', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContact',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to Delete Company Contact
                    amplify.request.define('deleteCompanyContact', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContact',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    // Define request to get Store
                    amplify.request.define('getBaseDataFornewCompany', 'ajax', {
                        url: ist.siteUrl + '/Api/CrmBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to save Store
                    amplify.request.define('saveStore', 'ajax', {
                        url: ist.siteUrl + '/Api/Company',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to get Suppliers
                    amplify.request.define('getSuppliers', 'ajax', {
                        url: ist.siteUrl + '/Api/CrmSupplier',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Purchases
                    amplify.request.define('getPurchases', 'ajax', {
                        url: ist.siteUrl + '/Api/Purchase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Goods Received Notes
                    amplify.request.define('getGoodsReceivedNotes', 'ajax', {
                        url: ist.siteUrl + '/Api/GoodsReceivedNote',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Delete Company Address
                    amplify.request.define('deleteCompanyAddress', 'ajax', {
                        url: ist.siteUrl + '/Api/Address',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    // Define request to get Company Territory
                    amplify.request.define('searchCompanyTerritory', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyTerritory',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Save Address
                    amplify.request.define('saveAddress', 'ajax', {
                        url: ist.siteUrl + '/Api/Address',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to Save Company Contact
                    amplify.request.define('saveCompanyContact', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContact',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to get Store
                    amplify.request.define('getBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/StoreBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                };
            },
             // get order tab data
            getOrders = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getDataForOrderTab',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // Save Address
            saveAddress = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveAddress',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
              // get Invoice tab data
            getInvoices= function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getDataForInvoiceTab',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // get Customer list of list view
            getCustomersForListView = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCompanies',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // get Store by id
            getStoreById = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getStoreById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // search Address
            searchAddress = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'searchAddress',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // search Company Contact
            searchCompanyContact = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'searchCompanyContact',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // get Base Data By Store Id
            getBaseDataFornewCompany = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseDataFornewCompany',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // save Store
            saveStore = function(param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveStore',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // searchCompanyTerritory
            searchCompanyTerritory = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'searchCompanyTerritory',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // get Base Data By Store Id
            getBaseData = function(params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getBaseData',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        },
        // Save Company Contact
            saveCompanyContact = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveCompanyContact',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // get Suppliers
	        getSuppliers = function (params, callbacks) {
	            initialize();
	            return amplify.request({
	                resourceId: 'getSuppliers',
	                success: callbacks.success,
	                error: callbacks.error,
	                data: params
	            });
	        },
            // get Purchases
	        getPurchases = function (params, callbacks) {
	            initialize();
	            return amplify.request({
	                resourceId: 'getPurchases',
	                success: callbacks.success,
	                error: callbacks.error,
	                data: params
	            });
	        },
             // Delete Company Address
            deleteCompanyAddress = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteCompanyAddress',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
             // Delete Company Contact
            deleteCompanyContact = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteCompanyContact',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // get Goods Received Notes
	        getGoodsReceivedNotes = function (params, callbacks) {
	            initialize();
	            return amplify.request({
	                resourceId: 'getGoodsReceivedNotes',
	                success: callbacks.success,
	                error: callbacks.error,
	                data: params
	            });
	        };
        return {
            getCustomersForListView: getCustomersForListView,
            getStoreById: getStoreById,
            searchAddress: searchAddress,
            searchCompanyContact: searchCompanyContact,
            getBaseDataFornewCompany: getBaseDataFornewCompany,
            getBaseData: getBaseData,
            saveStore: saveStore,
            getSuppliers: getSuppliers,
            getOrdersData: getOrders,
            searchCompanyTerritory: searchCompanyTerritory,
            getInvoices: getInvoices,
            saveCompanyContact: saveCompanyContact,
            saveAddress: saveAddress,
            getPurchases: getPurchases,
            getGoodsReceivedNotes: getGoodsReceivedNotes,
            deleteCompanyAddress: deleteCompanyAddress,
            deleteCompanyContact: deleteCompanyContact
        };
    })();

    return dataService;
});