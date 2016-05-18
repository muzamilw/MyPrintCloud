﻿/*
    Data service module with ajax calls to the server
*/
define("common/systemUser.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {
                    // Define request to get Get User Signature 
                    amplify.request.define('getSystemUserSignature', 'ajax', {
                        url: ist.siteUrl + '/Api/SystemUserForMis',
                        dataType: 'json',
                        type: 'GET'
                    });
                    //Get system users list
                    amplify.request.define('getSystemUsers', 'ajax', {
                        url: ist.siteUrl + '/Api/SystemUserList',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to save Signature
                    amplify.request.define('saveSystemUserSignature', 'ajax', {
                        url: ist.siteUrl + '/Api/SystemUserForMis',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    amplify.request.define('saveSystemUser', 'ajax', {
                        url: ist.siteUrl + '/Api/SystemUserList',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    
                    isInitialized = true;
                }
            },
            
            getSystemUserSignature = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getSystemUserSignature',
                    success: callbacks.success,
                    error: callbacks.error
                    
                });

            },
            getSystemUsers = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getSystemUsers',
                    success: callbacks.success,
                    error: callbacks.error

                });
            },
             // save Store
            saveSystemUserSignature = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveSystemUserSignature',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            saveSystemUser = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveSystemUser',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };
        return {
            getSystemUserSignature: getSystemUserSignature,
            saveSystemUserSignature: saveSystemUserSignature,
            getSystemUsers: getSystemUsers,
            saveSystemUser: saveSystemUser
        };
    })();

    return dataService;
});