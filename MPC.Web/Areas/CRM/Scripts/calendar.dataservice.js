/*
    Data service module with ajax calls to the server
*/
define("calendar/calendar.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {


                    // Define request to get Calendar base 
                    amplify.request.define('getCalendarBase', 'ajax', {
                        url: ist.siteUrl + '/Api/CalendarBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to delete Inventory
                    amplify.request.define('deleteInventory', 'ajax', {
                        url: ist.siteUrl + '/Api/Inventory',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    // Define request to Get Company By Customer Type
                    amplify.request.define('getCompanyByCustomerType', 'ajax', {
                        url: ist.siteUrl + '/Api/Calendar',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to save Inventory
                    amplify.request.define('saveInventory', 'ajax', {
                        url: ist.siteUrl + '/Api/Inventory',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });

                    isInitialized = true;
                }
            },


             // Get Calendar Base
            getCalendarBase = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCalendarBase',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // delete Inventory
            deleteInventory = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteInventory',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             //Get Company By Customer Type
            getCompanyByCustomerType = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCompanyByCustomerType',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },

           // Save Inventory
            saveInventory = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveInventory',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };

        return {
            getCalendarBase: getCalendarBase,
            deleteInventory: deleteInventory,
            saveInventory: saveInventory,
            getCompanyByCustomerType: getCompanyByCustomerType,
        };
    })();

    return dataService;
});