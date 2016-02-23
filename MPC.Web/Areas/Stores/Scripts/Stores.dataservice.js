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
                        type: 'GET',
                        decoder: amplify.request.decoders.istStatusDecoder
                    });
                    // Define request to get CMS Tags For Default Load of CMS Page
                    amplify.request.define('getCmsTags', 'ajax', {
                        url: ist.siteUrl + '/Api/CmsTag',
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
                    // Define request to delete Product Category By Id 
                    amplify.request.define('deleteProductCategoryById', 'ajax', {
                        url: ist.siteUrl + '/Api/ProductCategory',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    // Define request to delete Media Library By Id 
                    amplify.request.define('deleteMediaLibraryItemById', 'ajax', {
                        url: ist.siteUrl + '/Api/MediaLibrary',
                        dataType: 'json',
                        type: 'DELETE',
                        decoder: amplify.request.decoders.istStatusDecoder
                    });
                    // Define request to delete variable icon id
                    amplify.request.define('deleteCompanyVariableIcon', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyVariableIcon',
                        dataType: 'json',
                        type: 'DELETE',
                        decoder: amplify.request.decoders.istStatusDecoder
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
                    // Define request to Get Campaign Detail By Id
                    amplify.request.define('getCampaignDetailById', 'ajax', {
                        url: ist.siteUrl + '/Api/GetCampaignDetail',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Get Smart Form Details By Smart Form Id
                    amplify.request.define('getSmartFormDetailBySmartFormId', 'ajax', {
                        url: ist.siteUrl + '/Api/GetSmartFormDetail',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to Get Scope Varibable By Contact  Id
                    amplify.request.define('getScopeVaribableByContactId', 'ajax', {
                        url: ist.siteUrl + '/Api/GetCompanyContactVariable',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Get Cmpany Contact Varibable By Company Id
                    amplify.request.define('getCmpanyContactVaribableByCompanyId', 'ajax', {
                        url: ist.siteUrl + '/Api/GetCompanyContactVariableByCompanyId',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get Field Variables By CompanyId
                    amplify.request.define('getFieldVariablesByCompanyId', 'ajax', {
                        url: ist.siteUrl + '/Api/FieldVariable',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get System variables
                    amplify.request.define('getSystemFieldVariables', 'ajax', {
                        url: ist.siteUrl + '/Api/SystemVariable',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get Smart Forms By Company Id
                    amplify.request.define('getSmartFormsByCompanyId', 'ajax', {
                        url: ist.siteUrl + '/Api/SmartForm',
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

                    // Define request to delete Secondary Page
                    amplify.request.define('deleteSecondaryPage', 'ajax', {
                        url: ist.siteUrl + '/Api/SecondaryPage',
                        dataType: 'json',
                        type: 'DELETE',
                        decoder: amplify.request.decoders.istStatusDecoder
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
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });
                    // Define request to Get Theme Detail By Full Zip Path
                    amplify.request.define('getThemeDetail', 'ajax', {
                        url: ist.siteUrl + '/Api/GetThemeDetail',
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
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });
                    // Define request to get Store
                    amplify.request.define('getBaseDataForNewCompany', 'ajax', {
                        url: ist.siteUrl + '/Api/StoreBase',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });
                    // Define request to delete Store
                    amplify.request.define('deleteStore', 'ajax', {
                        url: ist.siteUrl + '/Api/Company',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'Delete'
                    });
                    // Define request to delete Banner
                    amplify.request.define('deleteCompanyBanner', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyBanner',
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
                    // Define request to save Discount Voucher
                    amplify.request.define('saveDiscountVoucher', 'ajax', {
                        url: ist.siteUrl + '/Api/DiscountVaoucherDetail',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to get Discount Vaoucher Detail
                    amplify.request.define('getDiscountVaoucherById', 'ajax', {
                        url: ist.siteUrl + '/Api/DiscountVaoucherDetail',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Delete Field Variable
                    amplify.request.define('deleteFieldVariable', 'ajax', {
                        url: ist.siteUrl + '/Api/FieldVariable',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'DELETE'
                    });


                    // Define request to save Secondary Page
                    amplify.request.define('saveSecondaryPage', 'ajax', {
                        url: ist.siteUrl + '/Api/SecondaryPage',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });

                    // Define request to Create Store
                    amplify.request.define('createStore', 'ajax', {
                        url: ist.siteUrl + '/Api/ImportExportOrganisation/ImportStore?parameter1={parameter1}&parameter2={parameter2}&parameter3={parameter3}',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST',
                        data: { parameter3: location.host }
                    });
                    // Define request to save Smart Form
                    amplify.request.define('saveSmartForm', 'ajax', {
                        url: ist.siteUrl + '/Api/SmartForm',
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
                    

                    // Define request to Get Discount Vouchers
                    amplify.request.define('getDiscountVoucher', 'ajax', {
                        url: ist.siteUrl + '/Api/DiscountVoucher',
                        dataType: 'json',
                        type: 'GET'
                    });
                    


                    // Define request to Get Discount Vouchers
                    amplify.request.define('getRealEstateCampaign', 'ajax', {
                        url: ist.siteUrl + '/Api/RealEstateCompaign',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to Get company variable icon
                    amplify.request.define('getCompanyVariableIcon', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyVariableIcon',
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
                    // Define request to Import Company Contact Csv
                    amplify.request.define('importCompanyContact', 'ajax', {
                        url: ist.siteUrl + '/Api/Import',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to export Company Contact Csv
                    amplify.request.define('exportCompanyContacts', 'ajax', {
                     
                        url: ist.siteUrl + '/Api/Export',
                        dataType: 'json',
                        type: 'GET'
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
                    // Define request to Delete Company Contact
                    amplify.request.define('unarchiveCompanyContact', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContactForOrder',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
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
                    // Define request to Delete Company Permanently
                    amplify.request.define('deleteCompanyPermanent', 'ajax', {
                        url: ist.siteUrl + '/Api/DeleteCompany',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'DELETE'
                    });
                    // Define request to Delete Company Permanently
                    amplify.request.define('copyFullStore', 'ajax', {
                        url: ist.siteUrl + '/Api/StoreCopy',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to Get Paymetn Gateways
                    amplify.request.define('getPaymentGateways', 'ajax', {
                        url: ist.siteUrl + '/Api/PaymentGateway',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to validate Live Stores Count
                    amplify.request.define('validateLiveStoresCount', 'ajax', {
                        url: ist.siteUrl + '/Api/OrganisationLicensing',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getProductforDV', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyProductCategory',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to verify Store save
                    amplify.request.define('validateCanStoreSaveById', 'ajax', {
                        url: ist.siteUrl + '/Api/OrganisationLicensing',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Get Store CSS
                    amplify.request.define('getStoreCss', 'ajax', {
                        url: ist.siteUrl + '/Api/StoreCss',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Delete Company Permanently
                    amplify.request.define('updateStoreCss', 'ajax', {
                        url: ist.siteUrl + '/Api/StoreCss',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to save Store
                    amplify.request.define('saveCompanyVariableIcon', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyVariableIcon',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to Get Organisation Widgets
                    amplify.request.define('getOrganisationWidgets', 'ajax', {
                        url: ist.siteUrl + '/Api/Widget',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to save Custom Organisation Widget
                    amplify.request.define('saveMyCustomWidget', 'ajax', {
                        url: ist.siteUrl + '/Api/Widget',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to Delete Custom Widget
                    amplify.request.define('deleteCustomWidget', 'ajax', {
                        url: ist.siteUrl + '/Api/Widget',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    // Define request to delete Store
                    amplify.request.define('archiveSpotColor', 'ajax', {
                        url: ist.siteUrl + '/Api/SpotColor',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'Delete'
                    });
                    isInitialized = true;
                }
            },
               // get Payment Gateways
            getPaymentGateways = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPaymentGateways',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
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
            getDiscountVaoucherById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getDiscountVaoucherById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            }, 
             // get Product Category By Id
            deleteProductCategoryById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteProductCategoryById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
              // delete Media Library Item By Id
            deleteMediaLibraryItemById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteMediaLibraryItemById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },

               // delete company variable icon
            deleteCompanyVariableIcons = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteCompanyVariableIcon',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },

            // get Items For Widgets
            getItemsForWidgets = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getItemsForWidgets',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // get CMS Tags For Load default for CMS Page
            getCmsTags = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCmsTags',
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
            //Get Scope Varibable By Contact Id
            getScopeVaribableByContactId = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getScopeVaribableByContactId',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            //Get Cmpany Contact Varibable By Company Id
            getCmpanyContactVaribableByCompanyId = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCmpanyContactVaribableByCompanyId',
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
            //Get Campaign Detail By Id
            getCampaignDetailById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCampaignDetailById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },

             //Get Smart Form Detail By Smart Form Id
                getSmartFormDetailBySmartFormId = function (params, callbacks) {
                    initialize();
                    return amplify.request({
                        resourceId: 'getSmartFormDetailBySmartFormId',
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
            // Get System Variables
            getSystemFieldVariables = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getSystemFieldVariables',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },


            // Get Smart Forms By Company Id
            getSmartFormsByCompanyId = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getSmartFormsByCompanyId',
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
             //Get Theme Detail By full path
            getThemeDetail = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getThemeDetail',
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

              // delete Stores
            archiveSpotColor = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'archiveSpotColor',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             // delete Banner
            deleteCompanyBanner = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteCompanyBanner',
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
               // get Discount Vouchers
            getDiscountVouchers = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getDiscountVoucher',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },

           

            // get realEstate
            getRealEstateCampaigns = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getRealEstateCampaign',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },

            // get companyVariable Icons
            getCompanyVariableIcons = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCompanyVariableIcon',
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
         // import Company Contacts
            importCompanyContact = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'importCompanyContact',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            
        // export Company Contacts
        exportCompanyContacts = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'exportCompanyContacts',
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


            // unarchive Company Contact
            unarchiveCompanyContact = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'unarchiveCompanyContact',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // Delete Secondary Page
            deleteSecondaryPage = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteSecondaryPage',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },

            deleteCompanyPermanent = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteCompanyPermanent',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
             copyFullStore = function (param, callbacks) {
                 initialize();
                 return amplify.request({
                     resourceId: 'copyFullStore',
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
        saveDiscountVoucher = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'saveDiscountVoucher',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        },
        
        // Delete Field Variable
        deleteFieldVariable = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'deleteFieldVariable',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        },

          // save Secondary Page
        saveSecondaryPage = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'saveSecondaryPage',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        },
         // Create Store
        createStore = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'createStore',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        },
         // save Smart Form
        saveSmartForm = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'saveSmartForm',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        },
          validateLiveStoresCount = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'validateLiveStoresCount',
                    success: callbacks.success,
                    error: callbacks.error
                });
          },
            validateCanStoreSaveById = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'validateCanStoreSaveById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            // get Store by id
            getStoreCss = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getStoreCss',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             updateStoreCss = function (param, callbacks) {
                 initialize();
                 return amplify.request({
                     resourceId: 'updateStoreCss',
                     success: callbacks.success,
                     error: callbacks.error,
                     data: param
                 });
             },
            
            getProductforDV = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getProductforDV',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
       },
       
        // save company variable icon
            saveCompanyVariableIcon = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveCompanyVariableIcon',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            //Get Organisation Widgets
            getOrganisationWidgets = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getOrganisationWidgets',
                    success: callbacks.success,
                    error: callbacks.error
                });
            },
            saveCustomWidget = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveMyCustomWidget',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            deleteCustomWidget = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteCustomWidget',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },

        // save Store saveCustomWidget
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
            saveCompanyVariableIcon: saveCompanyVariableIcon,
            saveStore: saveStore,
            searchCompanyTerritory: searchCompanyTerritory,
            getFieldVariablesByCompanyId: getFieldVariablesByCompanyId,
            getSystemFieldVariables: getSystemFieldVariables,
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
            importCompanyContact: importCompanyContact,
            exportCompanyContacts: exportCompanyContacts,
            deleteCompanyTerritory: deleteCompanyTerritory,
            deleteCompanyAddress: deleteCompanyAddress,
            deleteCompanyContact: deleteCompanyContact,
            unarchiveCompanyContact: unarchiveCompanyContact,
            saveFieldVariable: saveFieldVariable,
            deleteFieldVariable: deleteFieldVariable,
            getFieldVariableDetailById: getFieldVariableDetailById,
            getSmartFormDetailBySmartFormId: getSmartFormDetailBySmartFormId,
            getScopeVaribableByContactId: getScopeVaribableByContactId,
            getCmpanyContactVaribableByCompanyId: getCmpanyContactVaribableByCompanyId,
            saveSmartForm: saveSmartForm,
            getSmartFormsByCompanyId: getSmartFormsByCompanyId,
            deleteCompanyBanner: deleteCompanyBanner,
            getThemeDetail: getThemeDetail,
            getCmsTags: getCmsTags,
            getCampaignDetailById: getCampaignDetailById,
            deleteProductCategoryById: deleteProductCategoryById,
            createStore: createStore,
            deleteCompanyPermanent: deleteCompanyPermanent,
            copyFullStore: copyFullStore,
            deleteMediaLibraryItemById: deleteMediaLibraryItemById,
            deleteCompanyVariableIcons: deleteCompanyVariableIcons,
            saveSecondaryPage: saveSecondaryPage,
            deleteSecondaryPage: deleteSecondaryPage,
            getPaymentGateways: getPaymentGateways,
            getDiscountVouchers: getDiscountVouchers,
            saveDiscountVoucher: saveDiscountVoucher,
            getDiscountVaoucherById: getDiscountVaoucherById,
            validateLiveStoresCount: validateLiveStoresCount,
            validateCanStoreSaveById: validateCanStoreSaveById,
            getStoreCss: getStoreCss,
            updateStoreCss: updateStoreCss,
            getRealEstateCampaigns: getRealEstateCampaigns,
            getCompanyVariableIcons: getCompanyVariableIcons,
            archiveSpotColor: archiveSpotColor,
            getOrganisationWidgets: getOrganisationWidgets,
            saveCustomWidget: saveCustomWidget,
            deleteCustomWidget: deleteCustomWidget
        };
    })();

    return dataService;
});