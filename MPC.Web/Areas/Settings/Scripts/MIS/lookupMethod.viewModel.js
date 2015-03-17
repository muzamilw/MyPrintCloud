define("lookupMethod/lookupMethod.viewModel", ["jquery", "amplify", "ko", "lookupMethod/lookupMethod.dataservice", "lookupMethod/lookupMethod.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.lookupMethod = {
            
            viewModel: (function () {
                var
                    view,
                    errorList = ko.observableArray([]),
                    lookupupList = ko.observableArray([]),
                    GetLookupList = function () {
                          
                          dataservice.GetLookupList({
                              
                          }, {
                              success: function (data) {
                                  lookupupList.removeAll();
                                  if (data != null) {
                                     
                                      _.each(data, function (item) {
                                          var module = model.lookupupListClientMapper(item);
                                          lookupupList.push(module);
                                      });
                                  }
                                  
                              },
                              error: function (response) {
                                  
                                  toastr.error("Error: Failed to Load Lookup List Data." + response);
                              }
                          });
                      }

                return {
                    errorList: errorList,
                    GetLookupList:GetLookupList
                }
            })()
        };
        return  ist.lookupMethod.viewModel;
        
    });
    