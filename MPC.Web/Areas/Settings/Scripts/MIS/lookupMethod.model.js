define(["ko", "underscore", "underscore-ko"], function (ko) {

    var lookupMethod = function () {
        var self,
        MethodId = ko.observable(),
        Name = ko.observable(),
       Type = ko.observable(),
       self = {
           MethodId: MethodId,
           Name: Name,
           Type: Type

       };
        return self;

    };


    return {
        
    };
});