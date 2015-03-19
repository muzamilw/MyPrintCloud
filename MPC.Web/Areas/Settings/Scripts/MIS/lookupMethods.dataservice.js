/*
    Data service module with ajax calls to the server
*/
define("lookupMethods/lookupMethods.dataservice", function () {

    var dataService = (function () {
        var
             isInitialized = false,
             initialize = function () {
                 if (!isInitialized) {

                     amplify.request.define('GetLookupList', 'ajax', {
                         url: ist.siteUrl + '/Api/LookupMethod',
                         datatype: 'json',
                         type: 'Get'
                     });

                     isInitialized = true;
                 }
             },
             GetLookupList = function (params, callbacks) {
                 initialize();
                 return amplify.request({
                     resourceId: 'GetLookupList',
                     success: callbacks.success,
                     error: callbacks.error,
                     data: params
                 });
             };
        return {
            GetLookupList: GetLookupList
        }

    })();

    return dataService;
});