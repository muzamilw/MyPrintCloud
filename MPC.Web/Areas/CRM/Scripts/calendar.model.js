﻿define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    //Activity
    Activity = function (specifiedActivityId,specifiedSystemUserId, specifiedActivityRef, specifiedActivityTypeId, specifiedContactCompanyId, specifiedContactId,
        specifiedProductTypeId, specifiedSourceId, specifiedFlagId, specifiedStartDateTime, specifiedEndDateTime, specifiedIsCustomerActivity, specifiedIsPrivate,
        specifiedCompanyName, specifiedActivityNotes) {
        var
        self,
        //Activity Id
        id = ko.observable(specifiedActivityId),
        systemUserId = ko.observable(specifiedSystemUserId),
        //Subject
        title = ko.observable(specifiedActivityRef).extend({ required: true }),
        activityTypeId = ko.observable(specifiedActivityTypeId),
        contactCompanyId = ko.observable(specifiedContactCompanyId),
        contactId = ko.observable(specifiedContactId),
        productTypeId = ko.observable(specifiedProductTypeId),
        sourceId = ko.observable(specifiedSourceId),
        flagId = ko.observable(specifiedFlagId),
         //Start Date Time
        startDateTime = ko.observable((specifiedStartDateTime === null || specifiedStartDateTime === undefined) ? new Date() : moment(specifiedStartDateTime, ist.utcFormat)),
        //End Date Time
        endDateTime = ko.observable((specifiedEndDateTime === null || specifiedEndDateTime === undefined) ? null : moment(specifiedEndDateTime, ist.utcFormat)),

       isCustomerActivity = ko.observable(specifiedIsCustomerActivity),
        isPrivate = ko.observable(specifiedIsPrivate),
        companyName = ko.observable(specifiedCompanyName),
        activityNotes = ko.observable(specifiedActivityNotes),
                 isInvalidPeriod = ko.computed(function () {
                     return endDateTime() < startDateTime();
                 }),
        // Errors
        errors = ko.validation.group({
            title: title
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
                ActivityId: id(),
                SystemUserId: systemUserId(),
                ActivityRef: title(),
                ActivityTypeId: activityTypeId(),
                ContactCompanyId: packCostPrice(),
                ContactId: packCostPrice(),
                ProductTypeId: packCostPrice(),
                SourceId: packCostPrice(),
                FlagId: packCostPrice(),
                ActivityStartTime: fromDate() === undefined || fromDate() === null ? null : moment(fromDate()).format(ist.utcFormat),
                ActivityEndTime: toDate() === undefined || toDate() === null ? null : moment(toDate()).format(ist.utcFormat),
                IsCustomerActivity: costOrPriceIdentifier(),
                IsPrivate: costOrPriceIdentifier(),
                ActivityNotes: activityNotes(),
            }
        },
        // Reset
        reset = function () {
            dirtyFlag.reset();
        };
        self = {
            id: id,
            systemUserId: systemUserId,
            title: title,
            activityTypeId: activityTypeId,
            contactCompanyId: contactCompanyId,
            contactId: contactId,
            productTypeId: productTypeId,
            sourceId: sourceId,
            flagId: flagId,
            startDateTime: startDateTime,
            endDateTime: endDateTime,
            isCustomerActivity: isCustomerActivity,
            isPrivate: isPrivate,
            companyName: companyName,
            activityNotes: activityNotes,
            isInvalidPeriod: isInvalidPeriod,
            dirtyFlag: dirtyFlag,
            isValid: isValid,
            errors: errors,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    //Activity Create 
    Activity.Create = function (source) {
        return new Activity(source.ActivityId,source.SystemUserId, source.ActivityRef, source.ActivityTypeId, source.ContactCompanyId, source.ContactId, source.ProductTypeId, source.SourceId,
            source.FlagId, source.ActivityStartTime, source.ActivityEndTime, source.IsCustomerActivity, source.IsPrivate, source.CompanyName, source.ActivityNotes);
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