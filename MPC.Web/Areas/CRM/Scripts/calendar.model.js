define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    //Activity
    Activity = function (specifiedActivityId, specifiedActivityRef, specifiedStartDateTime, specifiedEndDateTime) {
        var
            self,
            //Activity Id
            id = ko.observable(specifiedActivityId),
            //Subject
            title = ko.observable(specifiedActivityRef).extend({ required: true }),
            //Start Date Time
            startDateTime = ko.observable((specifiedStartDateTime === null || specifiedStartDateTime === undefined) ? new Date() : moment(specifiedStartDateTime, ist.utcFormat)),
            //End Date Time
            endDateTime = ko.observable((specifiedEndDateTime === null || specifiedEndDateTime === undefined) ? null : moment(specifiedEndDateTime, ist.utcFormat)),
           // className = ko.observable("label-primary"),

            isInvalidPeriod = ko.computed(function () {
                return endDateTime() < startDateTime();
            }),
             // Errors
            errors = ko.validation.group({
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
            id: id,
            title: title,
            startDateTime: startDateTime,
            endDateTime: endDateTime,
            isInvalidPeriod: isInvalidPeriod,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    //Activity Create 
    Activity.Create = function (source) {
        return new Activity();
    };

    Company = function (specifiedCompanyId, specifiedName, specifiedURL, specifiedCreationDate) {
        var self,
            id = ko.observable(specifiedCompanyId),
            name = ko.observable(specifiedName),
        url = ko.observable(specifiedURL),
        creationDate = ko.observable(specifiedCreationDate);
        self = {
            id: id,
            name: name,
            url: url,
            creationDate: creationDate,
        };
        return self;
    };
    //Stock Cost And Price Item For Client Factory
    Company.Create = function (source) {
        return new Company(source.CompanyId, source.Name, source.URL, source.CreationDate);
    };
    return {
        Activity: Activity,
        Company: Company,
    };
});