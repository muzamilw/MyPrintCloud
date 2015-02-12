define("crm/contacts.dataservice", function () {
    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {
                    amplify.request.define('getContacts', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContactForCrm',
                        dataType: 'json',
                        type: 'GET'
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
            };
        return {
            getContactsForListView: getContactsForListView
        };
    })();

    return dataService;
});