define("crm/contacts.dataservice", function () {
    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {
                    amplify.request.define('getbaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContactBaseForCrm',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getContactsDetail', 'ajax', {
                        //url: ist.siteUrl + '/Api/CompanyContactForCrm',
                        url: ist.siteUrl + '/Api/CrmContact',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Delete Company Contact
                    amplify.request.define('deleteCompanyContact', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContact',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    amplify.request.define('getContacts', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContactForCrm',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('deleteContact', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContactForCrm',
                        dataType: 'json',
                        type: 'DELETE'
                    });

                    // Define request to export Company Contact Csv
                    amplify.request.define('exportCompanyContacts', 'ajax', {

                        url: ist.siteUrl + '/Api/ExportCRMContacts',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('saveCompanyContact', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContact',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                };
            },

            // get contact list of list view
            getContactsForListView = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getContacts',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // get Contacts Detail
            getContactsDetail = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getContactsDetail',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
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
            // get base Data
            getbaseData = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getbaseData',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
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
            // get contact list of list view
            deleteContact = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteContact',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // Save Company Contact
            saveCompanyContact = function(param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveCompanyContact',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };
        return {
            getbaseData:getbaseData,
            getContactsForListView: getContactsForListView,
            deleteContact: deleteContact,
            getContactsDetail: getContactsDetail,
            saveCompanyContact: saveCompanyContact,
            deleteCompanyContact: deleteCompanyContact,
            exportCompanyContacts: exportCompanyContacts
        };
    })();

    return dataService;
});