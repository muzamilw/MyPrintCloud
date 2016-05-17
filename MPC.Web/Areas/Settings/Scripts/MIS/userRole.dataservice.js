/*
    Data service module with ajax calls to the server
*/
define("userRole/userRole.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {
                    // Define request to get Get User Signature 
                    amplify.request.define('getUserRoles', 'ajax', {
                        url: ist.siteUrl + '/Api/UserRole',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    // Define request to save Signature
                    amplify.request.define('saveUserRole', 'ajax', {
                        url: ist.siteUrl + '/Api/UserRole',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    
                    isInitialized = true;
                }
            },
            
            getUserRoles = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getUserRoles',
                    success: callbacks.success,
                    error: callbacks.error
                });

            },
            
             // save Store
            saveUserRole = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveUserRole',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };
        return {
            getUserRoles: getUserRoles,
            saveUserRole: saveUserRole
        };
    })();

    return dataService;
});