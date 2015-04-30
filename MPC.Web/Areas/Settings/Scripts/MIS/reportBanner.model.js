define(["ko", "underscore", "underscore-ko"], function(ko) {
    var ReportNote = function (specifiedId, specifiedHeadNote, specifiedFootNote, AdvertitorialNote, specifiedCategoryId, specifiedReportBanner, specifiedTitle, specifiedEstimateBannerBytes,specifiedOrderBannerBytes, specifiedInvoiceBannerBytes, specifiedPurchaseBannerBytes, specifiedeliveryBannerBytes) {

        var
            self,
            id = ko.observable(specifiedId || 0),
            headNote = ko.observable(specifiedHeadNote),
            footnote = ko.observable(specifiedFootNote),
            advertitorialNote = ko.observable(AdvertitorialNote),
            categoryId = ko.observable(specifiedCategoryId),
            reportBanner = ko.observable(specifiedReportBanner),
            title = ko.observable(specifiedTitle),
            estimateBannerBytes = ko.observable(specifiedEstimateBannerBytes),
            orderBannerBytes = ko.observable(specifiedOrderBannerBytes),
            invoiceBannerBytes = ko.observable(specifiedInvoiceBannerBytes),
            purchaeBannerBytes = ko.observable(specifiedPurchaseBannerBytes),
            deliveryBannerBytes = ko.observable(specifiedeliveryBannerBytes),

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
                HeadNotes: headNote(),
                AdvertitorialNotes: advertitorialNote(),
                ReportCategoryId: categoryId(),
                ReportBanner: reportBanner(),
                ReportTitle: title(),
                FootNote: footnote(),
                EstimateBannerBytes: estimateBannerBytes(),
                orderBannerBytes : orderBannerBytes(),
                invoiceBannerBytes : invoiceBannerBytes(),
                purchaeBannerBytes : purchaeBannerBytes(),
                deliveryBannerBytes: deliveryBannerBytes()
            };
        };
        self = {
            id: id,
            headNote: headNote,
            footnote: footnote,
            advertitorialNote: advertitorialNote,
            categoryId: categoryId,
            reportBanner: reportBanner,
            title: title,
            estimateBannerBytes: estimateBannerBytes,
            orderBannerBytes : orderBannerBytes,
            invoiceBannerBytes : invoiceBannerBytes,
            purchaeBannerBytes : purchaeBannerBytes,
            deliveryBannerBytes: deliveryBannerBytes,
            dirtyFlag: dirtyFlag,
            errors: errors,
            isValid: isValid,
            hasChanges: hasChanges,
            reset: reset
    };
        return self;
    };

    ReportNote.Create = function (source) {
        var reprotNote = new ReportNote(source.Id, source.HeadNotes, source.FootNotes, source.AdvertitorialNotes, source.ReportCategoryId, source.ReportBanner, source.ReportTitle, source.EstimateBannerBytes, source.OrderBannerBytes, source.InvoiceBannerBytes, source.PurchaseBannerBytes, source.DeliveryBannerBytes);
            
        

        return reprotNote;
    };
    
    return {
        ReportNote: ReportNote
       
    };
});