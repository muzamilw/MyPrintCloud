define(["ko", "underscore", "underscore-ko"], function () {
    var ReportCategory = function (source) {
        var self
        if (source != undefined) {
            CategoryId = ko.observable(source.CategoryId),
            CategoryName = ko.observable(source.CategoryName),
            Description = ko.observable(source.Description),
            reports = ko.observableArray([])
            if (source.Reports != undefined && source.Reports != null) {
                _.each(source.Reports, function (item) {
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
            CategoryId: CategoryId,
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

    EmailFields = function () {
        // ReSharper restore InconsistentNaming
        var // Reference to this object
            self,
            // Unique key
            emailTo = ko.observable(),
            //ko.observable().extend({ required: true }),
            emailCC = ko.observable(),

            emailSubject = ko.observable(),

            emailAttachment = ko.observable(),

            emailAttachmentPath = ko.observable(),

            emailSignature = ko.observable(),


            // Errors
            errors = ko.validation.group({
                emailTo: emailTo,

            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),

            // True if the booking has been changed
            // ReSharper disable InconsistentNaming
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

            emailTo: emailTo,
            emailCC: emailCC,
            emailSubject: emailSubject,
            emailAttachment: emailAttachment,
            emailAttachmentPath: emailAttachmentPath,
            emailSignature: emailSignature,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset
        };
        return self;
    };



    var reportParamsMapper = function (source) {
        var self
        if (source != undefined) {
            ControlType = ko.observable(source.ControlType),
            ParmName = ko.observable(source.ParmName),
            Caption1 = ko.observable(source.Caption1)


        } else {
            ControlType = ko.observable(),
            ParmName = ko.observable(),
            Caption1 = ko.observable()
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
            ControlType: ControlType,
            ParmName: ParmName,
            Caption1: Caption1,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            reports: reports
        };
        return self;

    }


    var ComboMapper = function (source) {
        var self
        if (source != undefined) {
            ComboId = ko.observable(source.ComboId),
            ComboText = ko.observable(source.ComboText)



        } else {
            ComboId = ko.observable(),
            ComboText = ko.observable()

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
            ComboId: ComboId,
            ComboText: ComboText,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            reports: reports
        };
        return self;

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


    var ReportParam = function (source) {
        var self
        if (source != undefined) {
            ParmId = ko.observable(source.ParmId),
            ParmName = ko.observable(source.ParmName),
            Caption1 = ko.observable(source.Caption1),
            ReportId = ko.observable(source.ReportId),
            ControlType = ko.observable(source.ControlType),
            Caption2 = ko.observable(source.Caption2)
        } else {

            ParmId = ko.observable(),
           ParmName = ko.observable(),
           Caption1 = ko.observable(),
           ReportId = ko.observable(),
           ControlType = ko.observable(),
           Caption2 = ko.observable()
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
            ParmId: ParmId(),
            ParmName: ParmName(),
            Caption1: Caption1(),
            ReportId: ReportId(),
            ControlType: ControlType(),
            Caption2: Caption2(),
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,

        };
        return self;

    }


    return {
        ReportCategory: ReportCategory,
        reportParamsMapper: reportParamsMapper,
        ComboMapper: ComboMapper,
        EmailFields: EmailFields,
        ReportParam: ReportParam
    }

});