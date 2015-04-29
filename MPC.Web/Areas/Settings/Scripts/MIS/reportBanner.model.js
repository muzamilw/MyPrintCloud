define(["ko", "underscore", "underscore-ko"], function(ko) {
    var ReportNote = function (specifiedId, specifiedHeadNote, specifiedFootNote, AdvertitorialNote, specifiedCategoryId, specifiedReportBanner, specifiedTitle) {  

        var
            self,
            id = ko.observable(specifiedId || 0),
            headNote = ko.observable(specifiedHeadNote),
            footnote = ko.observable(specifiedFootNote),
            AdvertitorialNote = ko.observable(AdvertitorialNote),
            specifiedCategoryId = ko.observable(specifiedCategoryId),
            specifiedReportBanner = ko.observable(specifiedReportBanner),
            specifiedTitle = ko.observable(specifiedTitle),


            errors = ko.validation.group({
                
               
            }),
            isValid = ko.computed(function() {
                return errors().length === 0 ? true : false;;
            }),
            dirtyFlag = new ko.dirtyFlag({
                reportBannerUrl: reportBannerUrl
                
            }),
            hasChanges = ko.computed(function() {
                return dirtyFlag.isDirty();
            }),
            
            reset = function() {
                dirtyFlag.reset();
            };
        convertToServerData = function () {
            return {
                Id: id(),
                HeadNotes: specifiedFootNote(),
                AdvertitorialNotes: detailType(),
                ReportCategoryId: specifiedCategoryId(),
                ReportBanner: specifiedReportBanner(),
                ReportTitle: specifiedTitle(),
               
            };
        };
        self = {
            id: id,
            headNote: headNote,
            footnote: footnote,
            AdvertitorialNote: AdvertitorialNote,
            specifiedCategoryId: specifiedCategoryId,
            specifiedReportBanner: specifiedReportBanner,
            specifiedTitle: specifiedTitle,
            dirtyFlag: dirtyFlag,
            errors: errors,
            isValid: isValid,
            hasChanges: hasChanges,
            reset: reset
    };
        return self;
    };

    ReportNote.Create = function (source) {
        var reprotNote = new ReportNote(source.Id, source.HeadNotes, source.FootNotes, source.AdvertitorialNotes, source.ReportCategoryId, source.ReportBanner, source.ReportTitle);
            
        

        return reprotNote;
    };
    
    return {
        ReportNote: ReportNote
       
    };
});