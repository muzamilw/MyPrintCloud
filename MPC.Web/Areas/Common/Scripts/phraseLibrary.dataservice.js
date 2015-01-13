/*
    Data service module with ajax calls to the server
*/
define("common/phraseLibrary.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Get Sections List 
                    amplify.request.define('getSections', 'ajax', {
                        url: ist.siteUrl + '/Api/SectionsForPhraseLibrary',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Phrases By Section Id
                    amplify.request.define('getPhrasesBySectionId', 'ajax', {
                        url: ist.siteUrl + '/Api/SectionsForPhraseLibrary',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
            // get Sections
            getSections = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getSections',
                    success: callbacks.success,
                    error: callbacks.error,
                });

            },
            // get Phrases By Section Id
            getPhrasesBySectionId = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPhrasesBySectionId',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };
        return {
            getSections: getSections,
            getPhrasesBySectionId: getPhrasesBySectionId,
        };
    })();

    return dataService;
});