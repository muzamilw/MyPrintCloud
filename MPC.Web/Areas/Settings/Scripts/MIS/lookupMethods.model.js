define(["ko", "underscore", "underscore-ko"], function (ko) {

    var lookupMethodList = function () {
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
    var lookupupListClientMapper = function (source) {
        var lookupMethodList = new lookupMethodList();
        lookupMethodList.MethodId(source.MethodId);
        lookupMethodList.Name(source.Name);
        lookupMethodList.Type(source.Type);
    }

    return {
        lookupMethodList: lookupMethodList,
        lookupupListClientMapper: lookupupListClientMapper
    };
});