/*
    Data service module with ajax calls to the server
*/
define("sectionflags/sectionflags.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Sections
                    amplify.request.define('getSectionLibray', 'ajax', {
                        url: ist.siteUrl + '/Api/Section',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to get Section Flags
                    amplify.request.define('getSectionFlags', 'ajax', {
                        url: ist.siteUrl + '/Api/SectionFlags',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    // Define request to save Section Flags
                    amplify.request.define('saveSectionFlags', 'ajax', {
                        url: ist.siteUrl + '/Api/SectionFlags',
                        dataType: 'json',
                        //contentType: 'application/json;charset=utf-8',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    isInitialized = true;
                }
            },
            // Get Sections by Section Id 
              getSections = function (callbacks) {
                  initialize();
                  return amplify.request({
                      resourceId:'getSectionLibray',
                      success: callbacks.success,
                      error: callbacks.error,
                  });
              },
        // Get Sections flags
        getSectionFlags = function (params,callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getSectionFlags',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        },
            // Save Sections flags
        saveSectionFlags = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'saveSectionFlags',
                success: callbacks.success,
                error: callbacks.error,
                data: params//JSON.stringify(params)
            });
        };

        return {
            getSections: getSections,
            getSectionFlags: getSectionFlags,
            saveSectionFlags: saveSectionFlags
            
        };
    })();

    return dataService;
});