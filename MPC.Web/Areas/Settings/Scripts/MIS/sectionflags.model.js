define(["ko", "underscore", "underscore-ko"], function (ko) {
    var sectionflags = function () {

        var
            self,
            sectionId = ko.observable(),
            sectionName = ko.observable(),
            secOrder = ko.observable(),
            parentId = ko.observable(),
            href = ko.observable(),
            sectionImage = ko.observable(),
            independent = ko.observable()
            
            

        self =
        {
            sectionId:sectionId,
            sectionName: sectionName,
            secOrder: estimateStart,
            parentId: estimateNext,
            href: invoicePrefix,
            sectionImage: invoiceStart,
            independent: invoiceNext
          
        };
        return self;
    };

    function sectionflags(initialsection)
    {
        var self = this;
        self.section = ko.observable(initialMeal);
    }

  var sectionFlagsClientMapper = function (source) {
        var osectionflags = new sectionflags();
        osectionflags.sectionId(source.SectionId);
        osectionflags.sectionName(source.SectionName);
        osectionflags.secOrder(source.SecOrder);
        osectionflags.parentId(source.ParentId);
        osectionflags.href(source.href);
        osectionflags.sectionImage(source.SectionImage);
        osectionflags.independent(source.Independent);
        
        return osectionflags;

    };
    var sectionFlagsServerMapper = function (source) {
        var result = {};
        result.SectionId = source.sectionId();
        result.SectionName = source.sectionName();
        result.SecOrder = source.secOrder();
        result.ParentId = source.parentId();
        result.href = source.href();
        result.SectionImage = source.sectionImage();
        result.Independent = source.independent();
        
        return result;
    };

    return {
        prefix: sectionflags,
        sectionFlagsClientMapper: sectionFlagsClientMapper,
        sectionFlagsServerMapper: sectionFlagsServerMapper
    };
});