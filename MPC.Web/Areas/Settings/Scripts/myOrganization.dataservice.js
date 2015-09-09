/*
    Data service module with ajax calls to the server
*/
define("myOrganization/myOrganization.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get My Organization base 
                    amplify.request.define('getMyOrganizationBase', 'ajax', {
                        url: ist.siteUrl + '/Api/MyOrganizationBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get My Organization detail
                    amplify.request.define('getMyOrganizationDetail', 'ajax', {
                        url: ist.siteUrl + '/Api/MyOrganization',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to save My Organization
                    amplify.request.define('saveMyOrganization', 'ajax', {
                        url: ist.siteUrl + '/Api/MyOrganization',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to get Resouce File Contents By Language Id 
                    amplify.request.define('getResourceFileByLanguageId', 'ajax', {
                        url: ist.siteUrl + '/Api/LanguageEditor',
                        dataType: 'json',
                        type: 'GET'
                    });

         

                    //amplify.request.define('getMarkups', 'ajax', {
                    //    url: ist.siteUrl + '/Api/MyOrganization',
                    //    dataType: 'json',
                    //    type: 'GET'
                    //});

                    //amplify.request.define('getRegionalSettings', 'ajax', {
                    //    url: ist.siteUrl + '/Api/MyOrganizationBase',
                    //    dataType: 'json',
                    //    type: 'GET'
                    //});

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
            getMyOrganizationDetail = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getMyOrganizationDetail',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
             // get Resouce File Contents By Language Id 
            getResourceFileByLanguageId = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getResourceFileByLanguageId',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // // Get Markups
            //getMarkups = function (params, callbacks) {
            //    initialize();
            //    return amplify.request({
            //        resourceId: 'getMarkups',
            //        success: callbacks.success,
            //        error: callbacks.error,
            //        data: params
            //    });
            //},
            //// Get Regional Settings
            //getRegionalSettings = function (params, callbacks) {
            //    initialize();
            //    return amplify.request({
            //        resourceId: 'getRegionalSettings',
            //        success: callbacks.success,
            //        error: callbacks.error,
            //        data: params
            //    });
            //},
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
            getResourceFileByLanguageId: getResourceFileByLanguageId
            //getMarkups: getMarkups,
            //getRegionalSettings: getRegionalSettings
        };
    })();

    return dataService;
});