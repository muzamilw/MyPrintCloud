define("common/reportManager.dataservice", function () {

    // Data service for reportManager 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get report
                    amplify.request.define('getreports', 'ajax', {
                        url: ist.siteUrl + '/Api/ReportManager',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getreportparamsbyId', 'ajax', {
                        url: ist.siteUrl + '/Api/ReportManager',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getreportcategories', 'ajax', {
                        url: ist.siteUrl + '/Api/ReportManager',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    amplify.request.define('getReportEmailData', 'ajax', {
                        url: ist.siteUrl + '/Api/ReportEmail',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
            getreportcategories = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getreportcategories',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },


            // Get 
            getreports = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getreports',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            };

        // Get 
        getReportEmailData = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getReportEmailData',
                data: params,
                success: callbacks.success,
                error: callbacks.error,
            });
        };

        getreportparamsbyId = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getreportparamsbyId',
                data: params,
                success: callbacks.success,
                error: callbacks.error,
            });
        };

        return {
            getreports: getreports,
            getreportcategories: getreportcategories,
            getreportparamsbyId: getreportparamsbyId,
            getReportEmailData: getReportEmailData
           
        };
    })();

    return dataService;
});