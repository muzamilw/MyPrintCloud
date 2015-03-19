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

    var ClickChargeLookup = function (source) {
        var self
        Id = source.Id,
        MethodId = source.MethodId,
        SheetCost = ko.observable(source.SheetCost),
        Sheets = ko.observable(source.Sheets),
        SheetPrice = ko.observable(source.SheetPrice),
        TimePerHour = ko.observable(source.TimePerHour),

         errors = ko.validation.group({
         }),
        // Is Valid
        isValid = ko.computed(function () {
            return errors().length === 0;
        }),
        dirtyFlag = new ko.dirtyFlag({
            SheetCost: SheetCost,
            Sheets: Sheets,
            SheetPrice: SheetPrice,
            TimePerHour: TimePerHour
        }),
        // Has Changes
        hasChanges = ko.computed(function () {
            return dirtyFlag.isDirty();
        }),
        // Reset
        reset = function () {
            dirtyFlag.reset();
        };

        self = {
            Id: Id,
            MethodId: MethodId,
            Sheets: Sheets,
            SheetCost: SheetCost,
            SheetPrice: SheetPrice,
            TimePerHour: TimePerHour,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset
        };
        return self;

    }
    var lookupupListClientMapper = function (source) {
        var olookupMethodList = new lookupMethodList();
        olookupMethodList.MethodId(source.MethodId);
        olookupMethodList.Name(source.Name);
        olookupMethodList.Type(source.Type);
        return olookupMethodList;
    }

    return {
        lookupMethodList: lookupMethodList,
        lookupupListClientMapper: lookupupListClientMapper
    };
});