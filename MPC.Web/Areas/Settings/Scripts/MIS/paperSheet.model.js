define(["ko", "underscore", "underscore-ko"], function (ko) {
    var
// ReSharper disable once InconsistentNaming
        PaperSheet = function (specifiedPaperSizeId, specifiedName, specifiedHeight, specifiedWidth, specifiedSizeMeasure, specifiedArea,
                                specifiedIsFixed, specifiedRegion, specifiedIsArchived,specifiedIsImperical) {
            var
                self,
                paperSizeId = ko.observable(specifiedPaperSizeId),
                name = ko.observable(specifiedName).extend({ required: true }),
                height = ko.observable(specifiedHeight).extend({ required: true, number: true }),
                width = ko.observable(specifiedWidth).extend({ required: true, number: true }),
                sizeMeasure = ko.observable(specifiedSizeMeasure),
                area = ko.observable(specifiedArea),
                isFixed = ko.observable(specifiedIsFixed),
                region = ko.observable(specifiedRegion),
                isArchived = ko.observable(specifiedIsArchived),
                isImperical = ko.observable(specifiedIsImperical),
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
                 isArchived: isArchived,
                 isImperical: isImperical
             }),
             // Has Changes
             hasChanges = ko.computed(function () {
                 return dirtyFlag.isDirty();
             }),
             convertToServerData = function(culture) {
                 return {
                     PaperSizeId: paperSizeId(),
                     Name: name(),
                     Height: height(),
                     Width: width(),
                     SizeMeasure: sizeMeasure(),
                     Area: area(),
                     IsFixed: isFixed(),
                     Region: culture != null ? culture : region(),
                     IsArchived: isArchived(),
                     isImperical: isImperical()
                 };
             },
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
                isImperical: isImperical,
                isValid: isValid,
                errors: errors,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                convertToServerData: convertToServerData,
                reset: reset
            };
            return self;
        };
    //function to attain cancel button functionality 
    PaperSheet.CreateFromClientModel = function (source) {
         return new PaperSheet(source.paperSizeId, source.name, source.height, source.width, source.sizeMeasure, source.area,
                               source.isFixed, source.region, source.isArchived,source.isImperical);
     };
    // server to client mapper
     var paperSheetServertoClientMapper = function (source) {
         return PaperSheet.Create(source);
     };

    // Area Factory
     PaperSheet.Create = function (source) {
         return new PaperSheet(source.PaperSizeId, source.Name, source.Height, source.Width, source.SizeMeasure, source.Area,
                               source.IsFixed, source.Region, source.IsArchived,source.isImperical);
     };
    return {
        PaperSheet: PaperSheet,
        paperSheetServertoClientMapper: paperSheetServertoClientMapper
    };
});