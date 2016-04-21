/*
    Data service module with ajax calls to the server
*/
define("emails/emails.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {
                    // Define request to get Campaign Base Data
                    amplify.request.define('getCampaignBaseData', 'ajax', {
                        url: ist.siteUrl + '/Api/CampaignBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Get Campaign Detail By Id
                    amplify.request.define('getCampaignDetailById', 'ajax', {
                        url: ist.siteUrl + '/Api/GetCampaignDetail',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Get Campaign Detail By Id
                    amplify.request.define('getCampaigns', 'ajax', {
                        url: ist.siteUrl + '/Api/GetCampaignDetail',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    // Define request to save Campaign
                    amplify.request.define('saveEmailCampaign', 'ajax', {
                        url: ist.siteUrl + '/Api/GetCampaignDetail',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    
                    isInitialized = true;
                }
            },
            
            getCampaignBaseData = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCampaignBaseData',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            // get Campaigns
            getCampaigns = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCampaigns',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            //Get Campaign Detail By Id
            getCampaignDetailById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCampaignDetailById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            
             // save Store
            saveEmailCampaign = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveEmailCampaign',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };
        return {
            getCampaignBaseData: getCampaignBaseData,
            getCampaigns : getCampaigns,
            getCampaignDetailById : getCampaignDetailById,
            saveEmailCampaign: saveEmailCampaign
        };
    })();

    return dataService;
});