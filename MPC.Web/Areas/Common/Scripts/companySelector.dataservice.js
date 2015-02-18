/*
    Data service module with ajax calls to the server
*/
define("common/companySelector.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {
                    
                    // Define request to Get Company By Customer Type
                    amplify.request.define('getCompaniesByType', 'ajax', {
                        url: ist.siteUrl + '/Api/GetCompaniesForCalendar',
                        dataType: 'json',
                        type: 'GET'
                    });

                    isInitialized = true;
                }
            },
            // Get Companies By Type
            getCompaniesByType = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCompaniesByType',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            };
        
        return {
            getCompaniesByType: getCompaniesByType
        };
    })();

    return dataService;
});