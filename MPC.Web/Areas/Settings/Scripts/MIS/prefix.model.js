define(["ko", "underscore", "underscore-ko"], function(ko) {
    var prefix = function () {  

        var
            self,
            estimatePrefix = ko.observable().extend({ required: true }),
            estimateStart = ko.observable(),
            estimateNext = ko.observable().extend({ required: true }),
            invoicePrefix = ko.observable().extend({ required: true }),
            invoiceStart = ko.observable(),
            invoiceNext = ko.observable().extend({ required: true }),
            jobPrefix = ko.observable().extend({ required: true }),
            jobStart = ko.observable(),
            jobNext = ko.observable().extend({ required: true }),
            poPrefix = ko.observable().extend({ required: true }),
            poStart = ko.observable(),
            poNext = ko.observable().extend({ required: true }),
            gofPrefix = ko.observable(),
            gofStart = ko.observable(),
            gofNext = ko.observable(),
            itemPrefix = ko.observable(),
            itemStart = ko.observable(),
            itemNext = ko.observable(),
            deliveryNPrefix = ko.observable().extend({ required: true }),
            deliveryNStart = ko.observable(),
            deliveryNNext = ko.observable().extend({ required: true }),
            jobCardPrefix = ko.observable(),
            jobCardStart = ko.observable(),
            jobCardNext = ko.observable(),
            grnPrefix = ko.observable().extend({ required: true }),
            grnStart = ko.observable(),
            grnNext = ko.observable().extend({ required: true }),
            productPrefix = ko.observable().extend({ required: true }),
            productStart = ko.observable(),
            productNext = ko.observable().extend({ required: true }),
            finishedGoodsPrefix = ko.observable(),
            finishedGoodsStart = ko.observable(),
            finishedGoodsNext = ko.observable(),
            orderPrefix = ko.observable().extend({ required: true }),
            orderStart = ko.observable(),
            orderNext = ko.observable().extend({ required: true }),
            enquiryPrefix = ko.observable().extend({ required: true }),
            enquiryStart = ko.observable(),
            enquiryNext = ko.observable().extend({ required: true }),
            stockItemPrefix = ko.observable().extend({ required: true }),
            stockItemStart = ko.observable(),
            stockItemNext = ko.observable().extend({ required: true }),
            systemSiteId = ko.observable(),
            prefixId = ko.observable(),
            organisationId = ko.observable(),
            departmentId = ko.observable(),
            errors = ko.validation.group({
                enquiryPrefix: enquiryPrefix,
                enquiryNext: enquiryNext,
                estimatePrefix: estimatePrefix,
                estimateNext: estimateNext,
                orderPrefix: orderPrefix,
                orderNext: orderNext,
                jobPrefix: jobPrefix,
                jobNext: jobNext,
                invoicePrefix: invoicePrefix,
                invoiceNext: invoiceNext,
                stockItemPrefix: stockItemPrefix,
                stockItemNext: stockItemNext,
                productPrefix: productPrefix,
                productNext: productNext,
                deliveryNPrefix: deliveryNPrefix,
                deliveryNNext: deliveryNNext,
                grnPrefix: grnPrefix,
                grnNext: grnNext,
                poPrefix: poPrefix,
                poNext: poNext
            }),
            isValid = ko.computed(function() {
                return errors().length === 0 ? true : false;;
            }),
            dirtyFlag = new ko.dirtyFlag({
                enquiryPrefix: enquiryPrefix,
                enquiryNext: enquiryNext,
                estimatePrefix: estimatePrefix,
                estimateNext: estimateNext,
                orderPrefix: orderPrefix,
                orderNext: orderNext,
                jobPrefix: jobPrefix,
                jobNext: jobNext,
                invoicePrefix: invoicePrefix,
                invoiceNext: invoiceNext,
                stockItemPrefix: stockItemPrefix,
                stockItemNext: stockItemNext,
                productPrefix: productPrefix,
                productNext: productNext,
                deliveryNPrefix: deliveryNPrefix,
                deliveryNNext: deliveryNNext,
                grnPrefix: grnPrefix,
                grnNext: grnNext,
                poPrefix: poPrefix,
                poNext: poNext
            }),
            hasChanges = ko.computed(function() {
                return dirtyFlag.isDirty();
            }),
            
            reset = function() {
                dirtyFlag.reset();
            };
        self = {
            estimatePrefix: estimatePrefix,
            estimateStartes: estimateStart,
            estimateNext: estimateNext,
            invoicePrefix: invoicePrefix,
            invoiceStart: invoiceStart,
            invoiceNext: invoiceNext,
            jobPrefix: jobPrefix,
            jobStart: jobStart,
            jobNext: jobNext,
            poPrefix: poPrefix,
            poStart: poStart,
            poNext: poNext,
            gofPrefix: gofPrefix,
            gofStart: gofStart,
            gofNext: gofNext,
            itemPrefix: itemPrefix,
            itemStart: itemStart,
            itemNext: itemNext,
            deliveryNPrefix: deliveryNPrefix,
            deliveryNStart: deliveryNStart,
            deliveryNNext: deliveryNNext,
            jobCardPrefix: jobCardPrefix,
            jobCardStart: jobCardStart,
            jobCardNext: jobCardNext,
            grnPrefix: grnPrefix,
            grnStart: grnStart,
            grnNext: grnNext,
            productPrefix: productPrefix,
            productStart: productStart,
            productNext: productNext,
            finishedGoodsPrefix: finishedGoodsPrefix,
            finishedGoodsStart: finishedGoodsStart,
            finishedGoodsNext: finishedGoodsNext,
            orderPrefix: orderPrefix,
            orderStart: orderStart,
            orderNext: orderNext,
            enquiryPrefix: enquiryPrefix,
            enquiryStart: enquiryStart,
            enquiryNext: enquiryNext,
            stockItemPrefix: stockItemPrefix,
            stockItemStart: stockItemStart,
            stockItemNext: stockItemNext,
            systemSiteId: systemSiteId,
            prefixId: prefixId,
            departmentId: departmentId,
            organisationId:organisationId,
            dirtyFlag: dirtyFlag,
            errors: errors,
            isValid: isValid,
            hasChanges: hasChanges,
            reset: reset
    };
        return self;
    };

    var prefixClientMapper = function(source) {
        var oPrefix = new prefix();
        oPrefix.prefixId(source.PrefixId);
        oPrefix.estimatePrefix(source.EstimatePrefix);
        oPrefix.estimateNext(source.EstimateNext);
        oPrefix.estimateStartes(source.EstimateStart);
        oPrefix.invoicePrefix(source.InvoicePrefix);
        oPrefix.invoiceStart(source.InvoiceStart);
        oPrefix.invoiceNext(source.InvoiceNext);
        oPrefix.jobPrefix(source.JobPrefix);
        oPrefix.jobStart(source.JobStart);
        oPrefix.jobNext(source.JobNext);
        oPrefix.poPrefix(source.PoPrefix);
        oPrefix.poStart(source.PoStart);
        oPrefix.poNext(source.PoNext);
        oPrefix.grnPrefix(source.GrnPrefix);
        oPrefix.grnStart(source.GrnStart);
        oPrefix.grnNext(source.GrnNext);
        oPrefix.gofPrefix(source.GofPrefix);
        oPrefix.gofStart(source.GofStart);
        oPrefix.gofNext(source.GofNext);
        oPrefix.itemPrefix(source.ItemPrefix);
        oPrefix.itemStart(source.ItemStart);
        oPrefix.itemNext(source.ItemNext);
        oPrefix.deliveryNNext(source.DeliveryNNext);
        oPrefix.deliveryNStart(source.DeliveryNStart);
        oPrefix.deliveryNPrefix(source.DeliveryNPrefix);
        oPrefix.jobCardPrefix(source.JobCardPrefix);
        oPrefix.jobCardStart(source.JobCardStart);
        oPrefix.jobCardNext(source.JobCardNext);
        oPrefix.productPrefix(source.ProductPrefix);
        oPrefix.productStart(source.ProductStart);
        oPrefix.productNext(source.ProductNext);
        oPrefix.finishedGoodsNext(source.FinishedGoodsNext);
        oPrefix.finishedGoodsPrefix(source.FinishedGoodsPrefix);
        oPrefix.finishedGoodsStart(source.FinishedGoodsStart);
        oPrefix.orderPrefix(source.OrderPrefix);
        oPrefix.orderStart(source.OrderStart);
        oPrefix.orderNext(source.OrderNext);
        oPrefix.enquiryNext(source.EnquiryNext);
        oPrefix.enquiryPrefix(source.EnquiryPrefix);
        oPrefix.enquiryStart(source.EnquiryStart);
        oPrefix.stockItemNext(source.StockItemNext);
        oPrefix.stockItemPrefix(source.StockItemPrefix);
        oPrefix.stockItemStart(source.StockItemStart);
        oPrefix.departmentId(source.DepartmentId);
        oPrefix.systemSiteId(source.SystemSiteId);
        oPrefix.organisationId(source.OrganisationId);
        return oPrefix;

    };
    var prefixServerMapper = function (source) {
        var result = {};
        result.PrefixId = source.prefixId();
        result.EstimatePrefix = source.estimatePrefix();
        result.EstimateStart = source.estimateStartes();
        result.EstimateNext = source.estimateNext();
        result.InvoicePrefix = source.invoicePrefix();
        result.InvoiceStart = source.invoiceStart();
        result.InvoiceNext = source.invoiceNext();
        result.JobNext = source.jobNext();
        result.JobStart = source.jobStart();
        result.JobPrefix = source.jobPrefix();
        result.PoPrefix = source.poPrefix();
        result.PoStart = source.poStart();
        result.PoNext = source.poNext();
        result.GofNext = source.gofNext();
        result.GofStart = source.gofStart();
        result.GofPrefix = source.gofPrefix();
        result.ItemPrefix = source.itemPrefix();
        result.ItemStart = source.itemStart();
        result.ItemNext = source.itemNext();
        result.DeliveryNNext = source.deliveryNNext();
        result.DeliveryNPrefix = source.deliveryNPrefix();
        result.DeliveryNStart = source.deliveryNStart();
        result.JobCardPrefix = source.jobCardPrefix();
        result.JobCardNext = source.jobCardNext();
        result.JobCardStart = source.jobCardStart();
        result.GrnPrefix = source.grnPrefix();
        result.GrnNext = source.grnNext();
        result.GrnStart = source.grnStart();
        result.ProductNext = source.productNext();
        result.ProductStart = source.productStart();
        result.ProductPrefix = source.productPrefix();
        result.FinishedGoodsStart = source.finishedGoodsStart();
        result.FinishedGoodsNext = source.finishedGoodsNext();
        result.FinishedGoodsPrefix = source.finishedGoodsPrefix();
        result.OrderStart = source.orderStart();
        result.OrderPrefix = source.orderPrefix();
        result.OrderNext = source.orderNext();
        result.EnquiryPrefix = source.enquiryPrefix();
        result.EnquiryStart = source.enquiryStart();
        result.EnquiryNext = source.enquiryNext();
        result.StockItemNext = source.stockItemNext();
        result.StockItemPrefix = source.stockItemPrefix();
        result.StockItemStart = source.stockItemStart();
        result.SystemSiteId = source.systemSiteId();
        result.OrganisationId = source.organisationId();
        return result;
    };
    
    return {
        prefix: prefix,
        prefixClientMapper: prefixClientMapper,
        prefixServerMapper: prefixServerMapper
    };
});