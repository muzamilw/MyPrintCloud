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

                    // Define request to get SectionFlags
                    amplify.request.define('getSectionLibray', 'ajax', {
                        url: ist.siteUrl + '/Api/Section',
                        dataType: 'json',
                        type: 'GET'
                    });

                    isInitialized = true;
                }
            },
            // Get Sections by Section Id 
              getSectionFlags = function (callbacks) {
                  initialize();
                  return amplify.request({
                      resourceId:'getSectionLibray',
                      success: callbacks.success,
                      error: callbacks.error,
                  });
              };
            // Save Prefixes
           

        return {
            getSectionFlags: getSectionFlags
            
        };
    })();

    return dataService;
});