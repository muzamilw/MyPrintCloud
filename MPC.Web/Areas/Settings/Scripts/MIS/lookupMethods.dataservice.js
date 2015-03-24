﻿/*
    Data service module with ajax calls to the server
*/
define("lookupMethods/lookupMethods.dataservice", function () {

    var dataService = (function () {
        var
             isInitialized = false,
             initialize = function () {
                 if (!isInitialized) {

                     amplify.request.define('GetLookup', 'ajax', {
                         url: ist.siteUrl + '/Api/LookupMethod',
                         datatype: 'json',
                         type: 'Get'
                     });

                     amplify.request.define('saveLookup', 'ajax', {
                         url: ist.siteUrl + '/Api/LookupMethod',
                         dataType: 'json',
                         contentType: 'application/json; charset=utf-8',
                         decoder: amplify.request.decoders.istStatusDecoder,
                         type: 'POST'
                     });
                     isInitialized = true;
                 }
             },
              saveLookup = function (param, callbacks) {
                  initialize();
                  return amplify.request({
                      resourceId: 'saveLookup',
                      success: callbacks.success,
                      error: callbacks.error,
                      data: JSON.stringify(param)
                  });
              },
             GetLookup = function (params, callbacks) {
                 initialize();
                 return amplify.request({
                     resourceId: 'GetLookup',
                     success: callbacks.success,
                     error: callbacks.error,
                     data: params
                 });
             };
        return {
            GetLookup: GetLookup,
            saveLookup: saveLookup
        }

    })();

    return dataService;
});