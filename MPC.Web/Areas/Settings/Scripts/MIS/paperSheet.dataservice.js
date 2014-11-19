/*
    Data service module with ajax calls to the server
*/
define("paperSheet/paperSheet.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get My Organization base 
                    amplify.request.define('getPaperSheets', 'ajax', {
                        url: ist.siteUrl + '/Api/Get',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get My Organization detail
                    amplify.request.define('getPaperSheetDetail', 'ajax', {
                        url: ist.siteUrl + '/Api/Get',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to save My Organization
                    amplify.request.define('saveMyOrganization', 'ajax', {
                        url: ist.siteUrl + '/Api/Add',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    isInitialized = true;
                }
            },
            // Get My Organization Base
            getMyOrganizationBase = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getMyOrganizationBase',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // Get My Organization by id 
            getMyOrganizationDetail = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPaperSheetDetail',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // Save My Organization
            saveMyOrganization = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveMyOrganization',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };

        return {
            getMyOrganizationBase: getMyOrganizationBase,
            getMyOrganizationDetail: getMyOrganizationDetail,
            saveMyOrganization: saveMyOrganization,
        };
    })();

    return dataService;
});