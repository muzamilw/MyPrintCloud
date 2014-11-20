define(["ko", "underscore", "underscore-ko"], function (ko) {
    var
// ReSharper disable once InconsistentNaming
        PaperSheet = function () {
            var
                self,
                paperSizeId = ko.observable(),
                name = ko.observable().extend({ required: true }),
                height = ko.observable().extend({ required: true }),
                width = ko.observable().extend({ required: true }),
                sizeMeasure = ko.observable(),
                area = ko.observable(),
                isFixed = ko.observable(),
                region = ko.observable(),
                isArchived = ko.observable(),
             // Errors
             errors = ko.validation.group({
                 name: name,
                 height: height,
                 width: width
             }),
                // Is Valid 
             isValid = ko.computed(function () {
                 return errors().length === 0 ? true : false;
             }),

             // True if the booking has been changed
             // ReSharper disable InconsistentNaming
             dirtyFlag = new ko.dirtyFlag({
                 paperSizeId: paperSizeId,
                 name: name,
                 height: height,
                 width: width,
                 sizeMeasure: sizeMeasure,
                 area: area,
                 isFixed: isFixed,
                 region: region,
                 isArchived: isArchived
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
                paperSizeId: paperSizeId,
                name: name,
                height: height,
                width: width,
                sizeMeasure: sizeMeasure,
                area: area,
                isFixed: isFixed,
                region: region,
                isArchived: isArchived,
                isValid: isValid,
                errors: errors,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                reset: reset
            };
            return self;
        };

    var paperSheetClientMapper = function (source) {
        var papersheet = new PaperSheet();
        papersheet.paperSizeId(source.PaperSizeId === null ? undefined : source.PaperSizeId);
        papersheet.name(source.Name === null ? undefined : source.Name);
        papersheet.height(source.Height === null ? undefined : source.Height);
        papersheet.width(source.Weight === null ? undefined : source.Width);
        papersheet.sizeMeasure(source.SizeMeasure === null ? undefined : source.SizeMeasure);
        papersheet.area(source.Area === null ? undefined : source.Area);
        papersheet.isFixed(source.IsFixed === null ? undefined : source.IsFixed);
        papersheet.region(source.Region === null ? undefined : source.Region);
        return papersheet;
    };
    var paperSheetServerMapper = function (source) {
        var result = {};
        result.PaperSizeId = source.paperSizeId() === null ? undefined : source.paperSizeId();
        result.Name = source.name() === null ? undefined : source.name();
        result.Height = source.height() === null ? undefined : source.height();
        result.Width = source.width() === null ? undefined : source.width();
        result.SizeMeasure = source.sizeMeasure() === null ? undefined : source.sizeMeasure();
        result.Area = source.area() === null ? undefined : source.area();
        result.IsFixed = source.isFixed() === null ? undefined : source.isFixed();
        result.Region = source.region() === null ? undefined : source.region();
        return result;
    };
    return {
        PaperSheet: PaperSheet,
        paperSheetClientMapper: paperSheetClientMapper,
        paperSheetServerMapper: paperSheetServerMapper
    };
});