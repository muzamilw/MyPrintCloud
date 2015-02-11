define("crm/crm.dataservice", function () {
    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {
                    amplify.request.define('getCompanies', 'ajax', {
                        url: ist.siteUrl + '/Api/Customer',
                        dataType: 'json',
                        type: 'GET'
                    });
                };
            },
            // get Customer list of list view
            getCustomersForListView = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCompanies',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };
        return {
            getCustomersForListView: getCustomersForListView
        };
    })();

    return dataService;
});