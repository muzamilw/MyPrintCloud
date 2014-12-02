

define("stores/stores.model",["ko", "underscore", "underscore-ko"], function (ko) {
    var
        // ReSharper disable once InconsistentNaming
        Store = function (specifiedCompanyId, specifiedName, specifiedUrl, specifiedAccountOpenDate, specifiedAccountManagerId, specifiedAvatRegNumber,
            specifiedAvatRegReference, flagId, specifiedPhoneNo, specifiedIsCustomer, specifiedNotes, specifiedWebAccessCode, specifiedTwitterUrl,
            specifiedFacebookUrl, specifiedLinkedinUrl, specifiedFacebookAppId, specifiedFacebookAppKey, specifiedTwitterAppId, specifiedTwitterAppKey
        ) {
            var
                self,
                companyId = ko.observable(specifiedCompanyId),
                name = ko.observable(specifiedName),
                url = ko.observable(specifiedUrl),
                accountOpenDate = ko.observable(specifiedAccountOpenDate),
                accountManagerId = ko.observable(specifiedAccountManagerId),
                avatRegNumber = ko.observable(specifiedAvatRegNumber),
                avatRegReference = ko.observable(specifiedAvatRegReference),
                phoneNo = ko.observable(specifiedPhoneNo),
                isCustomer = ko.observable(specifiedIsCustomer),
                notes = ko.observable(specifiedNotes),
                webAccessCode = ko.observable(specifiedWebAccessCode),
                twitterUrl = ko.observable(specifiedTwitterUrl),
                facebookUrl = ko.observable(specifiedFacebookUrl),
                linkedinUrl = ko.observable(specifiedLinkedinUrl),
                facebookAppId = ko.observable(specifiedFacebookAppId),
                facebookAppKey = ko.observable(specifiedFacebookAppKey),
                twitterAppId = ko.observable(specifiedTwitterAppId),
                twitterAppKey = ko.observable(specifiedTwitterAppKey),
                
                // Errors
                errors = ko.validation.group({
                    name: name,
                    url: url,
                }),
                // Is Valid 
                isValid = ko.computed(function () {
                    return errors().length === 0 ? true : false;
                }),


                // ReSharper disable InconsistentNaming
                dirtyFlag = new ko.dirtyFlag({
                    // ReSharper restore InconsistentNaming
                    companyId: companyId,
                    name: name,
                    url: url,
                    accountOpenDate: accountOpenDate,
                    accountManagerId: accountManagerId,
                    avatRegNumber: avatRegNumber,
                    avatRegReference: avatRegReference,
                    phoneNo: phoneNo,
                    isCustomer: isCustomer,
                    notes: notes,
                    webAccessCode: webAccessCode,
                    twitterUrl: twitterUrl,
                    facebookUrl: facebookUrl,
                    linkedinUrl: linkedinUrl,
                    facebookAppId: facebookAppId,
                    facebookAppKey: facebookAppKey,
                    twitterAppId: twitterAppId,
                    twitterAppKey: twitterAppKey
                }),
                // Has Changes
                hasChanges = ko.computed(function () {
                    return dirtyFlag.isDirty();
                }),
                //Convert To Server
                convertToServerData = function (source) {
                    var result = {};
                    result.CompanyId = source.companyId();
                    result.Name = source.name();
                    result.URL = source.url();
                    result.AccountOpenDate = source.accountOpenDate();
                    result.AccountManagerId = source.accountManagerId();
                    result.AvatRegNumber = source.avatRegNumber();
                    result.PvatRegReference = source.avatRegReference();
                    result.PhoneNo = source.phoneNo();
                    result.IsCustomer = source.isCustomer();
                    result.Notes = source.notes();
                    result.WebAccessCode = source.webAccessCode();
                    result.TwitterUrl = source.twitterUrl();
                    result.FacebookUrl = source.facebookUrl();
                    result.LinkedinUrl = source.linkedinUrl();
                    result.FacebookAppId = source.facebookAppId();
                    result.FacebookAppKey = source.facebookAppKey();
                    result.TwitterAppId = source.twitterAppId();
                    result.TwitterAppKey = source.twitterAppKey();
                    return result;
                },
                // Reset
                reset = function () {
                    dirtyFlag.reset();
                };
            self = {
                companyId: companyId,
                name: name,
                url: url,
                accountOpenDate: accountOpenDate,
                accountManagerId: accountManagerId,
                avatRegNumber: avatRegNumber,
                avatRegReference: avatRegReference,
                phoneNo: phoneNo,
                isCustomer: isCustomer,
                notes: notes,
                webAccessCode: webAccessCode,
                twitterUrl: twitterUrl,
                facebookUrl: facebookUrl,
                linkedinUrl: linkedinUrl,
                facebookAppId: facebookAppId,
                facebookAppKey: facebookAppKey,
                twitterAppId: twitterAppId,
                twitterAppKey: twitterAppKey,
                isValid: isValid,
                errors: errors,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                convertToServerData: convertToServerData,
                reset: reset
            };
            return self;
        };
    Store.CreateFromClientModel = function (source) {
        return new Store(     
            source.companyId,
            source.name,
            source.url,
            source.accountOpenDate,
            source.accountManagerId,
            source.avatRegNumber,
            source.avatRegReference,
            source.phoneNo,
            source.isCustomer,
            source.notes,
            source.webAccessCode,
            source.twitterUrl,
            source.facebookUrl,
            source.linkedinUrl,
            source.facebookAppId,
            source.facebookAppKey,
            source.twitterAppId,
            source.twitterAppKey,
            source.companyId
            );
    };
    Store.Create = function (source) {
        var store = new Store(
            source.CompanyId,
            source.Name,
            source.URL,
            source.AccountOpenDate,
            source.AccountManagerId,
            source.AvatRegNumber,
            source.AvatRegReference,
            source.PhoneNo,
            source.IsCustomer,
            source.Notes,
            source.WebAccessCode,
            source.TwitterUrl,
            source.FacebookUrl,
            source.LinkedinUrl,
            source.FacebookAppId,
            source.FacebookAppKey,
            source.TwitterAppId,
            source.TwitterAppKey,
            source.CompanyId
            );
        return store;
    };
    return {
        Store: Store,
    };
});



