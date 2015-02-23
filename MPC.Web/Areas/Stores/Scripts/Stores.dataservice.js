define("stores/stores.dataservice", function () {
    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {
                    // Define request to get Store
                    amplify.request.define('getStores', 'ajax', {
                        url: ist.siteUrl + '/Api/Company',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Campaign Base Data
                    amplify.request.define('getCampaignBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/CampaignBase',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get Items For Widgets
                    amplify.request.define('getItemsForWidgets', 'ajax', {
                        url: ist.siteUrl + '/Api/GetItemsForWidgets',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get product category childs
                    amplify.request.define('getProductCategoryChilds', 'ajax', {
                        url: ist.siteUrl + '/Api/ProductCategory',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Product Category By Id 
                    amplify.request.define('getProductCategoryById', 'ajax', {
                        url: ist.siteUrl + '/Api/ProductCategory',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Company Territory
                    amplify.request.define('searchCompanyTerritory', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyTerritory',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Get Field Variable Detail By Id
                    amplify.request.define('getFieldVariableDetailById', 'ajax', {
                        url: ist.siteUrl + '/Api/FieldVariableDetail',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Get Cmpany Contact Varibable ByContact  Id
                    amplify.request.define('getCmpanyContactVaribableByContactId', 'ajax', {
                        url: ist.siteUrl + '/Api/GetCompanyContactVariable',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Field Variables By CompanyId
                    amplify.request.define('getFieldVariablesByCompanyId', 'ajax', {
                        url: ist.siteUrl + '/Api/FieldVariable',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    // Define request to get Address
                    amplify.request.define('searchAddress', 'ajax', {
                        url: ist.siteUrl + '/Api/Address',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Secondary Pages
                    amplify.request.define('getSecondaryPages', 'ajax', {
                        url: ist.siteUrl + '/Api/SecondaryPage',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get CompanyContact
                    amplify.request.define('searchCompanyContact', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContact',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Store By StoreId
                    amplify.request.define('getStoreById', 'ajax', {
                        url: ist.siteUrl + '/Api/Company',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Page Layout widgets
                    amplify.request.define('getCmsPageLayoutWidget', 'ajax', {
                        url: ist.siteUrl + '/Api/CmsPageLayoutDetail',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Widget Detail
                    amplify.request.define('getWidgetDetail', 'ajax', {
                        url: ist.siteUrl + '/Api/GetWidgetDetail',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get Store
                    amplify.request.define('getBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/StoreBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Store
                    amplify.request.define('getBaseDataForNewCompany', 'ajax', {
                        url: ist.siteUrl + '/Api/StoreBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to delete Store
                    amplify.request.define('deleteStore', 'ajax', {
                        url: ist.siteUrl + '/Api/Company',
                        dataType: 'json',
                        type: 'Delete'
                    });
                    // Define request to save Store
                    amplify.request.define('saveStore', 'ajax', {
                        url: ist.siteUrl + '/Api/Company',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to save Field Variable
                    amplify.request.define('saveFieldVariable', 'ajax', {
                        url: ist.siteUrl + '/Api/FieldVariable',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });

                    // Define request to Get Secondry Page By Id
                    amplify.request.define('getSecondryPageById', 'ajax', {
                        url: ist.siteUrl + '/Api/SecondaryPage',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Get Company Territory Validation check
                    amplify.request.define('validateCompanyToDelete', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyTerritory',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Save Company Territory
                    amplify.request.define('saveCompanyTerritory', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyTerritory',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to Delete Company Territory
                    amplify.request.define('deleteCompanyTerritory', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyTerritory',
                        dataType: 'json',
                        type: 'DELETE'
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
                    // Define request to Delete Company Address
                    amplify.request.define('deleteCompanyAddress', 'ajax', {
                        url: ist.siteUrl + '/Api/Address',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    // Define request to Delete Company Contact
                    amplify.request.define('deleteCompanyContact', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContact',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    // Define request to Get Address Validation check
                    amplify.request.define('validateAddressToDelete', 'ajax', {
                        url: ist.siteUrl + '/Api/Address',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Save Product Category
                    amplify.request.define('saveProductCategory', 'ajax', {
                        url: ist.siteUrl + '/Api/ProductCategory',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    isInitialized = true;
                }
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
            },
            // get Product Category By Id
            getProductCategoryById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getProductCategoryById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // get Items For Widgets
            getItemsForWidgets = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getItemsForWidgets',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
             // get Store
            getStores = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getStores',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            //Get Cmpany Contact Varibable By Contact Id
            getCmpanyContactVaribableByContactId = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCmpanyContactVaribableByContactId',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            //Get Field Variable Detail By Id
            getFieldVariableDetailById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getFieldVariableDetailById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             // get Campaign Base Data
            getCampaignBaseData = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCampaignBaseData',
                    success: callbacks.success,
                    error: callbacks.error,
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
            // Get Field Variables By CompanyId
            getFieldVariablesByCompanyId = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getFieldVariablesByCompanyId',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            
            // search Address
            searchAddress = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'searchAddress',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             //Get Secondary Pages
            getSecondaryPages = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getSecondaryPages',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // search Company Contact
            searchCompanyContact = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'searchCompanyContact',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // get Store by id
            getStoreById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getStoreById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             // get CMS Page Layout Widget
            getCmsPageLayoutWidget = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCmsPageLayoutWidget',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
              // get Widget Detail 
            getWidgetDetail = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getWidgetDetail',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },

            // get Base Data By Store Id
            getBaseData = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseData',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // get Base Data for new company
            getBaseDataForNewCompany = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseData',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // delete Stores
            deleteStore = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteStore',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
              // Get Secondry Page By Id
            getSecondryPageById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getSecondryPageById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
        // validate Company To Delete
        validateCompanyToDelete = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'validateCompanyToDelete',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        },
        // validate Address To Delete
        validateAddressToDelete = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'validateAddressToDelete',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        },
        // Save Product Category
        saveProductCategory = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'saveProductCategory',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        },
         // Save Company Territory
            saveCompanyTerritory = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveCompanyTerritory',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
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
            // Delete Company Territory
            deleteCompanyTerritory = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteCompanyTerritory',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
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


        // save Field Variable
        saveFieldVariable = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'saveFieldVariable',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        },
        // save Store
        saveStore = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'saveStore',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        };

        return {
            getStores: getStores,
            getProductCategoryChilds: getProductCategoryChilds,
            getStoreById: getStoreById,
            getBaseData: getBaseData,
            getBaseDataForNewCompany: getBaseDataForNewCompany,
            deleteStore: deleteStore,
            saveStore: saveStore,
            searchCompanyTerritory: searchCompanyTerritory,
            getFieldVariablesByCompanyId: getFieldVariablesByCompanyId,
            searchAddress: searchAddress,
            searchCompanyContact: searchCompanyContact,
            getSecondaryPages: getSecondaryPages,
            getSecondryPageById: getSecondryPageById,
            getCmsPageLayoutWidget: getCmsPageLayoutWidget,
            getWidgetDetail: getWidgetDetail,
            getProductCategoryById: getProductCategoryById,
            getItemsForWidgets: getItemsForWidgets,
            saveProductCategory: saveProductCategory,
            getCampaignBaseData: getCampaignBaseData,
            validateCompanyToDelete: validateCompanyToDelete,
            validateAddressToDelete: validateAddressToDelete,
            saveCompanyTerritory: saveCompanyTerritory,
            saveAddress: saveAddress,
            saveCompanyContact: saveCompanyContact,
            deleteCompanyTerritory: deleteCompanyTerritory,
            deleteCompanyAddress: deleteCompanyAddress,
            deleteCompanyContact: deleteCompanyContact,
            saveFieldVariable: saveFieldVariable,
            getFieldVariableDetailById: getFieldVariableDetailById,
            getCmpanyContactVaribableByContactId: getCmpanyContactVaribableByContactId,
        };
    })();

    return dataService;
});