/*
    Data service module with ajax calls to the server
*/
define("deliveryNotes/deliveryNotes.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Items
                    amplify.request.define('getdeliveryNotes', 'ajax', {
                        url: ist.siteUrl + '/Api/DeliveryNotes',
                        dataType: 'json',
                        type: 'GET'
                    }),
                    // Define request to get order by id
                    amplify.request.define('getBaseDataForCompany', 'ajax', {
                        url: ist.siteUrl + '/Api/OrderBaseForCompany',
                        dataType: 'json',
                        type: 'GET'
                    }),
                    // Define request to get Items
                    amplify.request.define('getDetaildeliveryNote', 'ajax', {
                        url: ist.siteUrl + '/Api/DeliveryNotes',
                        dataType: 'json',
                        type: 'GET'
                    }),
                    
                    // Define request to get Items
                    amplify.request.define('downloadArtwork', 'ajax', {
                        url: ist.siteUrl + '/Production/Home/Test',
                        dataType: 'json',
                        type: 'Post'
                    }),
                    // Define request to get order by id
                    amplify.request.define('getBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/DeliveryNoteBase',
                        dataType: 'json',
                        type: 'GET'
                    }),
                    // Define request to Save Item
                    amplify.request.define('saveItem', 'ajax', {
                        url: ist.siteUrl + '/Api/ItemJobStatus',
                        dataType: 'json',
                        type: 'Post'
                    });

                    isInitialized = true;
                }
            },

            // Save Item
            saveItem = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveItem',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
             // Get Base For Company
            getBaseData = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseData',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
             // Get Base For Company
            getBaseDataForCompany = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseDataForCompany',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
       getDetaildeliveryNote = function (param, callbacks) {
           initialize();
           return amplify.request({
               resourceId: 'getDetaildeliveryNote',
               success: callbacks.success,
               error: callbacks.error,
               data: param
           });
       },
            getdeliveryNotes = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getdeliveryNotes',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };


        return {
            getdeliveryNotes: getdeliveryNotes,
            getDetaildeliveryNote:getDetaildeliveryNote,
            saveItem: saveItem,
            getBaseData: getBaseData,
            getBaseDataForCompany: getBaseDataForCompany

        };
    })();

    return dataService;
});