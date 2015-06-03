define("dashboard.dataservice", function () {
    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {
                    amplify.request.define('getOrderStauses', 'ajax', {
                        url: ist.siteUrl + '/Api/GetDashboardData',
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
            // get base Data
            getOrderStauses = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getOrderStauses',
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
            getOrderStauses: getOrderStauses,
            saveCompanyContact: saveCompanyContact
        };
    })();

    return dataService;
});