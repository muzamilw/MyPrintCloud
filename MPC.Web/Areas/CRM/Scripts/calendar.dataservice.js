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
                    // Define request to delete Activity
                    amplify.request.define('deleteActivity', 'ajax', {
                        url: ist.siteUrl + '/Api/Calendar',
                        dataType: 'json',
                        type: 'DELETE'
                    });
                    // Define request to Get Activity Detail By Id
                    amplify.request.define('getActivityDetailById', 'ajax', {
                        url: ist.siteUrl + '/Api/ActivityDetail',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Get Company Contact By CompanyId
                    amplify.request.define('getCompanyContactByCompanyId', 'ajax', {
                        url: ist.siteUrl + '/Api/CalendarBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Get Company Contact By Name and Type
                    amplify.request.define('getCompanyContactByName', 'ajax', {
                        url: ist.siteUrl + '/Api/CompanyContactForCalendar',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to Get Company By Customer Type
                    amplify.request.define('getCompanyByCustomerType', 'ajax', {
                        url: ist.siteUrl + '/Api/GetCompaniesForCalendar',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Get Company By Customer Type
                    amplify.request.define('getActivies', 'ajax', {
                        url: ist.siteUrl + '/Api/Calendar',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to save Activity
                    amplify.request.define('saveActivity', 'ajax', {
                        url: ist.siteUrl + '/Api/Calendar',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to save Activity ON Drop Or resize
                    amplify.request.define('saveActivityDropOrResize', 'ajax', {
                        url: ist.siteUrl + '/Api/ActivityDetail',
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
            // delete Activity
            deleteActivity = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteActivity',
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
              //Get Activities
            getActivies = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getActivies',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },

             //Get Activity Detail By Id
            getActivityDetailById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getActivityDetailById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            //get Company Contact By Company Id
            getCompanyContactByCompanyId = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCompanyContactByCompanyId',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             //get Company Contact By Name and Customer Type
            getCompanyContactByName = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCompanyContactByName',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },

           // CalendarBase
        // Save Activity
        saveActivityDropOrResize = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'saveActivityDropOrResize',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        },
        // Save Activity
            saveActivity = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveActivity',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };

        return {
            getCalendarBase: getCalendarBase,
            getActivies: getActivies,
            deleteActivity: deleteActivity,
            saveActivity: saveActivity,
            getCompanyByCustomerType: getCompanyByCustomerType,
            getActivityDetailById: getActivityDetailById,
            saveActivityDropOrResize: saveActivityDropOrResize,
            getCompanyContactByCompanyId: getCompanyContactByCompanyId,
            getCompanyContactByName: getCompanyContactByName
        };
    })();

    return dataService;
});