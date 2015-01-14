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
                    // Define request to get Phrases By Phrase Field Id
                    amplify.request.define('getPhrasesByPhraseFieldId', 'ajax', {
                        url: ist.siteUrl + '/Api/SectionsForPhraseLibrary',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Phase Library
                    amplify.request.define('savePhaseLibrary', 'ajax', {
                        url: ist.siteUrl + '/Api/SectionsForPhraseLibrary',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
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
            // get Phrases By Phrase Field Id
            getPhrasesByPhraseFieldId = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPhrasesByPhraseFieldId',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             // save Store
        savePhaseLibrary = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'savePhaseLibrary',
                success: callbacks.success,
                error: callbacks.error,
                data: param
            });
        };
        return {
            getSections: getSections,
            getPhrasesByPhraseFieldId: getPhrasesByPhraseFieldId,
            savePhaseLibrary: savePhaseLibrary,
        };
    })();

    return dataService;
});