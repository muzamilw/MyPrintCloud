define(["ko", "underscore", "underscore-ko"], function () {
    var ReportCategory = function (source) {
        var self
        if (source != undefined) {
            CategoryId = ko.observable(source.CategoryId),
            CategoryName = ko.observable(source.CategoryName),
            Description = ko.observable(source.Description),
            reports = ko.observableArray([])
            if (source.Reports != undefined && source.Reports != null) {
                _.each(source.Reports, function(item){
                    reports.push(Report(item));
                });
            }


        } else {
            CategoryId = null,
            CategoryName = ko.observable(),
            Description = ko.observable(),
            reports = ko.observableArray([])
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
            reports: reports
        };
        return self;

    }
    var requestReportList = function () {

    }
    var Report = function (source) {
        var self
        if (source != undefined) {
            ReportId = ko.observable(source.ReportId),
            Name = ko.observable(source.Name)
           
        } else {
            ReportId = ko.observable(),
            Name = ko.observable()
            
        }


        errors = ko.validation.group({
        }),
        // Is Valid
       isValid = ko.computed(function () {
           return errors().length === 0;
       }),
       dirtyFlag = new ko.dirtyFlag({
          

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
            ReportId: ReportId,
            Name: Name,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            reports: reports
        };
        return self;

    }
    return {
        ReportCategory: ReportCategory
    }
    
});