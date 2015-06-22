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
               sectionflags: sectionflags

           }),
           isValid = ko.computed(function () {
              return  sectionflags.filter(function(flag) {
                   return !flag.isValid();
               }).length === 0
               && errors().length === 0 ? true : false;
               
           }),
           showAllErrors = function () {
                            // Show Item Errors
            errors.showAllMessages();
               // Show Item Stock Option Errors
            var sectionFlagsError = sectionflags.filter(function (flag) {
                return !flag.isValid();
            });
            if (sectionFlagsError.length > 0) {
                _.each(sectionFlagsError, function (flag) {
                    flag.errors.showAllMessages();
                });
            }
           },
              // Set Validation Summary
            setValidationSummary = function (validationSummaryList) {
                // Show Item Stock Option Errors
                var sectionFlagsInvalid = sectionflags.find(function (flag) {
                    return !flag.isValid();
                });
                if (sectionFlagsInvalid) {
                    if (sectionFlagsInvalid.errors) {
                        var labelElement = sectionFlagsInvalid.name.domElement;
                        validationSummaryList.push({ name: 'Section Flag Name', element: labelElement });
                    }
                }
            },
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
            setValidationSummary:setValidationSummary,
            dirtyFlag: dirtyFlag,
            errors: errors,
            showAllErrors:showAllErrors,
            isValid: isValid,
            hasChanges: hasChanges,
            reset: reset
        };
        return self;
    },
// ReSharper disable InconsistentNaming
        SectionFlag = function (spcId, spcName, spcDescription, desColor, spcSectionId, spcIsDefault) {
            // ReSharper restore InconsistentNaming
            // ReSharper restore InconsistentNaming

            var
               self,
               id = ko.observable(spcId || 0),
               name = ko.observable(spcName || undefined).extend({ required: true }),
               description = ko.observable(spcDescription || undefined),
               color = ko.observable(desColor || '#E0E0E0'),
               sectionId = ko.observable(spcSectionId || undefined),
               isDefault = ko.observable(spcIsDefault || undefined),
               errors = ko.validation.group({
                   name: name

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
                sectionId: sectionId,
                isDefault:isDefault,
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

        var sectionFlag = new SectionFlag(source.SectionFlagId, source.FlagName, source.FlagDescription, source.FlagColor, source.SectionId, source.IsDefault);
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