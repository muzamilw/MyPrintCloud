define(["ko", "underscore", "underscore-ko"], function (ko) {
    // ReSharper disable InconsistentNaming
    var Section = function (spcId, spcName) {
        // ReSharper restore InconsistentNaming

        var
           self,
           id = ko.observable(spcId || 0),
           name = ko.observable(spcName || 0),
           sectionflags = ko.observableArray([]),
           errors = ko.validation.group({


           }),
           isValid = ko.computed(function () {
               return errors().length === 0 ? true : false;;
           }),
           dirtyFlag = new ko.dirtyFlag({
               sectionflags: sectionflags
           }),
           hasChanges = ko.computed(function () {
               return dirtyFlag.isDirty();
           }),

           reset = function () {
               dirtyFlag.reset();
           },
       convertToServerData = function () {
           return {

           };
       };
        self = {
            id: id,
            name: name,
            sectionflags: sectionflags,
            dirtyFlag: dirtyFlag,
            errors: errors,
            isValid: isValid,
            hasChanges: hasChanges,
            reset: reset
        };
        return self;
    },
// ReSharper disable InconsistentNaming
        SectionFlag = function (spcId, spcName, spcDescription, desColor, spcSectionId) {
            // ReSharper restore InconsistentNaming
            // ReSharper restore InconsistentNaming

            var
               self,
               id = ko.observable(spcId || 0),
               name = ko.observable(spcName || undefined).extend({ required: true }),
               description = ko.observable(spcDescription || undefined),
               color = ko.observable(desColor || '#E0E0E0'),
               sectionId = ko.observable(spcSectionId || undefined),
               errors = ko.validation.group({


               }),
               isValid = ko.computed(function () {
                   return errors().length === 0 ? true : false;;
               }),
               dirtyFlag = new ko.dirtyFlag({
                   id: id,
                   name: name,
                   description: description,
                   color: color
               }),
               hasChanges = ko.computed(function () {
                   return dirtyFlag.isDirty();
               }),

               reset = function () {
                   dirtyFlag.reset();
               },
           convertToServerData = function () {
               return {
                   SectionFlagId: id(),
                   FlagName: name(),
                   FlagDescription: description(),
                   FlagColor: color(),
                   SectionId: sectionId()
               };
           };
            self = {
                id: id,
                name: name,
                description: description,
                color: color,
                sectionId:sectionId,
                convertToServerData: convertToServerData,
                dirtyFlag: dirtyFlag,
                errors: errors,
                isValid: isValid,
                hasChanges: hasChanges,
                reset: reset
            };
            return self;
        };

    SectionFlag.Create = function (source) {

        var sectionFlag = new SectionFlag(source.SectionFlagId, source.FlagName, source.FlagDescription, source.FlagColor, source.SectionId);
        return sectionFlag;
    };

    Section.Create = function (source) {
        var section = new Section(source.SectionId, source.SectionName);
        return section;
    };
    SectionFlagUpdateModel = function() {
        return {
            SectionFlags: []
        };
    };
    return {
        Section: Section,
        SectionFlag: SectionFlag,
        SectionFlagUpdateModel:SectionFlagUpdateModel
    };
});