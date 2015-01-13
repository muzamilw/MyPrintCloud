define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    //Section view Entity
    Section = function (specifiedSectionId, specifiedName
                           ) {
        var
            self,
            //Unique ID
            sectionId = ko.observable(specifiedSectionId),
            //Name
            name = ko.observable(specifiedName);

        self = {
            sectionId: sectionId,
            name: name,
        };
        return self;
    };


    return {
        Section: Section
    };
});