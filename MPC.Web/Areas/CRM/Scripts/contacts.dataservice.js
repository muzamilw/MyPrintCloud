define("crm/contacts.dataservice", function () {
    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {
                    amplify.request.define('getbaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContactBaseForCrm',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getContactsDetail', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContactForCrm',
                        dataType: 'json',
                        type: 'GET'
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
                };
            },

            // get contact list of list view
            getContactsForListView = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getContacts',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // get Contacts Detail
            getContactsDetail = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getContactsDetail',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             // get base Data
            getbaseData = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getbaseData',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
              // get contact list of list view
            deleteContact = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteContact',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };
        return {
            getbaseData:getbaseData,
            getContactsForListView: getContactsForListView,
            deleteContact: deleteContact,
            getContactsDetail: getContactsDetail
        };
    })();

    return dataService;
});