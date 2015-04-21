define(["ko", "underscore", "underscore-ko"], function () {
    var ReportCategory = function (source) {
        var self
        if (source != undefined) {
            CategoryId = source.CategoryId,
            CategoryName = source.CategoryName,
            Description = ko.observable(source.Description)
        } else {
            CategoryId = null,
            CategoryName = ko.observable(),
            Description = ko.observable()
        }


        errors = ko.validation.group({
        }),
        // Is Valid
       isValid = ko.computed(function () {
           return errors().length === 0;
       }),
       dirtyFlag = new ko.dirtyFlag({
           CategoryName: CategoryName,
           Description: Description

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
            CategoryId:CategoryId,
            CategoryName: CategoryName,
            Description: Description,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,

        };
        return self;

    }


    return {
        ReportCategory: ReportCategory
    }
    
});