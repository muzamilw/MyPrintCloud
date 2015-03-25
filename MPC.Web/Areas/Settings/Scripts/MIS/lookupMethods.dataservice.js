/*
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
                     amplify.request.define('saveNewLookup', 'ajax', {
                         url: ist.siteUrl + '/Api/LookupMethod',
                         dataType: 'json',
                         contentType: 'application/json; charset=utf-8',
                         decoder: amplify.request.decoders.istStatusDecoder,
                         type: 'PUT'
                     });
                     amplify.request.define('deleteLookup', 'ajax', {
                         url: ist.siteUrl + '/Api/LookupMethod',
                         dataType: 'json',
                         type: 'DELETE'
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
              saveNewLookup = function (param, callbacks) {
                  initialize();
                  return amplify.request({
                      resourceId: 'saveNewLookup',
                      success: callbacks.success,
                      error: callbacks.error,
                      data: JSON.stringify(param)
                  });
              },
              
        deleteLookup = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'deleteLookup',
                success: callbacks.success,
                error: callbacks.error,
                data: params
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
            saveLookup: saveLookup,
            saveNewLookup: saveNewLookup,
            deleteLookup: deleteLookup
        }

    })();

    return dataService;
});