define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    //Stock Cost And Price
    StockCostAndPrice = function (specifiedCostPriceId, specifiedCostPrice, specifiedPackCostPrice, specifiedFromDate, specifiedToDate, specifiedCostOrPriceIdentifier) {
        var
            self,
            //cost Price Id
            costPriceId = ko.observable(specifiedCostPriceId),
            //Cost Price
            costPrice = ko.observable(specifiedCostPrice).extend({ required: true, number: true }),
            //Pack Cost Price
            packCostPrice = ko.observable(specifiedPackCostPrice),
             //To Date 
            toDate = ko.observable(specifiedToDate === null ? moment().toDate() : moment(specifiedToDate, ist.utcFormat).toDate()).extend({ required: true }),
            //From Date
            fromDate = ko.observable(specifiedFromDate === null ? moment().toDate() : moment(specifiedFromDate, ist.utcFormat).toDate()).extend({ required: true }),
                //Cost Or Price Identifier
            costOrPriceIdentifier = ko.observable(specifiedCostOrPriceIdentifier),
            // Formatted From Date
             formattedFromDate = ko.computed({
                 read: function () {
                     return fromDate() !== undefined ? moment(fromDate(), ist.datePattern).toDate() : undefined;
                 }
             }),
             // Formatted To Date
             formattedToDate = ko.computed({
                 read: function () {
                     return toDate() !== undefined ? moment(toDate()).format(ist.datePattern) : undefined;
                 }
             }),
              isInvalidPeriod = ko.computed(function () {
                  return toDate() < fromDate();
              }),
             // Errors
            errors = ko.validation.group({
                costPrice: costPrice,
                fromDate: fromDate,
                toDate: toDate,
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // True if the booking has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            convertToServerData = function () {
                return {
                    CostPriceId: costPriceId(),
                    CostPrice: costPrice(),
                    PackCostPrice: packCostPrice(),
                    FromDate: fromDate() === undefined || fromDate() === null ? null : moment(fromDate()).format(ist.utcFormat),
                    ToDate: toDate() === undefined || toDate() === null ? null : moment(toDate()).format(ist.utcFormat),
                    CostOrPriceIdentifier: costOrPriceIdentifier(),
                }
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            costPriceId: costPriceId,
            costPrice: costPrice,
            packCostPrice: packCostPrice,
            fromDate: fromDate,
            toDate: toDate,
            formattedFromDate: formattedFromDate,
            formattedToDate: formattedToDate,
            costOrPriceIdentifier: costOrPriceIdentifier,
            isValid: isValid,
            errors: errors,
            isInvalidPeriod: isInvalidPeriod,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    //Stock Cost And Price Item For Client Factory
    StockCostAndPrice.Create = function () {
        return new StockCostAndPrice(0, 0, 0, null, null, 0);
    };

    ExternalEvents = function (specifiedName) {
        var
              //cost Price Id
            name = ko.observable(specifiedName),
             self = {
                 name: name
             };
        return self;
    }
    return {
        ExternalEvents: ExternalEvents,
    };
});