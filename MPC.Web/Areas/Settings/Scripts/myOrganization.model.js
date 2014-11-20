/*
    Module with the model for the My Organization
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var
    //Company Sites Entity
    // ReSharper disable InconsistentNaming
     CompanySites = function () {
         // ReSharper restore InconsistentNaming
         var // Reference to this object
             self,
             // Unique key
             id = ko.observable(),
             //Name
             name = ko.observable().extend({ required: true }),
             //Address 1
             address1 = ko.observable(),
             //Address 2
             address2 = ko.observable(),
             //City
             city = ko.observable(),
             //State
             state = ko.observable(),
             //Country
             country = ko.observable(),
             //Zip Code
             zipCode = ko.observable(),
             //Tel
             tel = ko.observable(),
             //Fax
             fax = ko.observable(),
             //Email
             email = ko.observable().extend({ email: true }),
             //Mobile
             mobile = ko.observable(),
             //Url
             url = ko.observable(),
             //MisLogo
             misLogo = ko.observable(),
             //Currency Symbol Id
             currencyId = ko.observable(),
             //Language ID
             languageId = ko.observable(),
             //System Length Unit ID
            lengthUnitId = ko.observable(),
            //System Weight Unit ID
            weightUnitId = ko.observable(),
            //Tax Registration No
            taxRegistrationNo = ko.observable(),
            //Markup ID
            markupId = ko.observable(),
            //markups In My Organization
            markupsInMyOrganization = ko.observableArray([]),
            //Tax Rates In MyOrganization
            taxRatesInMyOrganization = ko.observableArray([]),
            //Chart Of Accounts In My Organization
            chartOfAccountsInMyOrganization = ko.observableArray([]),
             // Errors
             errors = ko.validation.group({
                 email: email
             }),
             // Is Valid
             isValid = ko.computed(function () {
                 return errors().length === 0;
             }),

             // True if the booking has been changed
             // ReSharper disable InconsistentNaming
             dirtyFlag = new ko.dirtyFlag({
                 name: name,
                 address1: address1,
                 address2: address2,
                 city: city,
                 state: state,
                 country: country,
                 zipCode: zipCode,
                 tel: tel,
                 fax: fax,
                 email: email,
                 mobile: mobile,
                 url: url,
                 currencyId: currencyId,
                 languageId: languageId,
                 lengthUnitId: lengthUnitId,
                 weightUnitIdrl: weightUnitId,
                 taxRegistrationNo: taxRegistrationNo,
                 markupId: markupId
             }),
             // Has Changes
             hasChanges = ko.computed(function () {
                 return dirtyFlag.isDirty();
             }),
             // Reset
             reset = function () {
                 dirtyFlag.reset();
             };

         self = {
             id: id,
             name: name,
             address1: address1,
             address2: address2,
             city: city,
             state: state,
             country: country,
             zipCode: zipCode,
             tel: tel,
             fax: fax,
             email: email,
             mobile: mobile,
             url: url,
             misLogo: misLogo,
             currencyId: currencyId,
             languageId: languageId,
             lengthUnitId: lengthUnitId,
             weightUnitId: weightUnitId,
             taxRegistrationNo: taxRegistrationNo,
             markupId: markupId,
             markupsInMyOrganization: markupsInMyOrganization,
             taxRatesInMyOrganization: taxRatesInMyOrganization,
             chartOfAccountsInMyOrganization: chartOfAccountsInMyOrganization,
             errors: errors,
             isValid: isValid,
             dirtyFlag: dirtyFlag,
             hasChanges: hasChanges,
             reset: reset,
         };
         return self;
     };
    //Chart Of Account Entity
    // ReSharper disable once AssignToImplicitGlobalInFunctionScope
    ChartOfAccount = function () {
        // ReSharper restore InconsistentNaming
        var // Reference to this object
            self,
            // Unique key
            id = ko.observable(),
            //AccountNo
            accountNo = ko.observable().extend({ required: true, number: true }),
            //Name
            name = ko.observable().extend({ required: true }),
               // Errors
            errors = ko.validation.group({
                name: name,
                accountNo: accountNo
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
           // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };

        self = {
            id: id,
            name: name,
            accountNo: accountNo,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
        };
        return self;
    };
    //Markup Entity
    // ReSharper disable once AssignToImplicitGlobalInFunctionScope
    Markup = function () {
        // ReSharper restore InconsistentNaming
        var // Reference to this object
            self,
            // Unique key
            id = ko.observable(),
            //Name
            name = ko.observable().extend({ required: true }),
            //Rate
            rate = ko.observable().extend({ required: true, number: true }),
               // Errors
            errors = ko.validation.group({
                name: name,
                rate: rate
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
              // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };

        self = {
            id: id,
            name: name,
            rate: rate,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
        };
        return self;
    };
    //Tax Rate Entity
    // ReSharper disable once AssignToImplicitGlobalInFunctionScope
    TaxRate = function () {
        // ReSharper restore InconsistentNaming
        var // Reference to this object
            self,
            // Unique key
            id = ko.observable(),
             //Name
            name = ko.observable().extend({ required: true }),
            //Tax1
            tax1 = ko.observable().extend({ required: true }),
               // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };

        self = {
            id: id,
            name: name,
            tax1: tax1,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
        };
        return self;
    };
    //Convert Server To Client
    var CompanySitesClientMapper = function (source) {
        var companySites = new CompanySites();
        companySites.id(source.OrganisationId === null ? undefined : source.OrganisationId);
        companySites.name(source.OrganisationName === null ? undefined : source.OrganisationName);
        companySites.address1(source.Address1 === null ? undefined : source.Address1);
        companySites.address2(source.Address2 === null ? undefined : source.Address2);
        companySites.city(source.City === null ? undefined : source.City);
        companySites.state(source.State === null ? undefined : source.State);
        companySites.country(source.Country === null ? undefined : source.Country);
        companySites.zipCode(source.ZipCode === null ? undefined : source.ZipCode);
        companySites.tel(source.Tel === null ? undefined : source.Tel);
        companySites.fax(source.Fax === null ? undefined : source.Fax);
        companySites.email(source.Email === null ? undefined : source.Email);
        companySites.mobile(source.Mobile === null ? undefined : source.Mobile);
        companySites.url(source.Url === null ? undefined : source.Url);
        companySites.currencyId(source.CurrencyId === null ? undefined : source.CurrencyId);
        companySites.languageId(source.LanguageId === null ? undefined : source.LanguageId);
        companySites.lengthUnitId(source.SystemLengthUnit === null ? undefined : source.SystemLengthUnit);
        companySites.weightUnitId(source.SystemWeightUnit === null ? undefined : source.SystemWeightUnit);
        companySites.taxRegistrationNo(source.TaxRegistrationNo === null ? undefined : source.TaxRegistrationNo);
        companySites.markupId(source.MarkupId === null ? undefined : source.MarkupId);
        return companySites;
    };
    //Convert Server To Client
    var ChartOfAccountClientMapper = function (source) {
        var chartOfAccount = new ChartOfAccount();
        chartOfAccount.id(source.Id === null ? undefined : source.Id);
        chartOfAccount.name(source.Name === null ? undefined : source.Name);
        chartOfAccount.accountNo(source.AccountNo === null ? undefined : source.AccountNo);
        return chartOfAccount;
    };
    //Convert Server To Client
    var MarkupClientMapper = function (source) {
        var markup = new Markup();
        markup.id(source.MarkUpId === null ? undefined : source.MarkUpId);
        markup.name(source.MarkUpName === null ? undefined : source.MarkUpName);
        markup.rate(source.MarkUpRate === null ? undefined : source.MarkUpRate);
        return markup;
    };
    //Convert Server To Client
    var TaxRateClientMapper = function (source) {
        var taxRate = new TaxRate();
        taxRate.id(source.TaxId === null ? undefined : source.TaxId);
        taxRate.name(source.TaxName === null ? undefined : source.TaxName);
        taxRate.tax1(source.Tax1 === null ? undefined : source.Tax1);
        return taxRate;
    };
    //Convert Client To Server
    var CompanySitesServerMapper = function (source) {
        var result = {};
        result.OrganisationId = source.id() === undefined ? 0 : source.id();
        result.OrganisationName = source.name() === undefined ? null : source.name();
        result.Address1 = source.address1() === undefined ? null : source.address1();
        result.Address2 = source.address2() === undefined ? null : source.address2();
        result.City = source.city() === undefined ? null : source.city();
        result.State = source.state() === undefined ? null : source.state();
        result.Country = source.country() === undefined ? null : source.country();
        result.ZipCode = source.zipCode() === undefined ? null : source.zipCode();
        result.Tel = source.tel() === undefined ? null : source.tel();
        result.Fax = source.fax() === undefined ? null : source.fax();
        result.Email = source.email() === undefined ? null : source.email();
        result.Mobile = source.mobile() === undefined ? null : source.mobile();
        result.Url = source.url() === undefined ? null : source.url();
        result.CurrencyId = source.currencyId() === undefined ? null : source.currencyId();
        result.LanguageId = source.languageId() === undefined ? null : source.languageId();
        result.SystemLengthUnit = source.lengthUnitId() === undefined ? null : source.lengthUnitId();
        result.SystemWeightUnit = source.weightUnitId() === undefined ? null : source.weightUnitId();
        result.TaxRegistrationNo = source.taxRegistrationNo() === undefined ? null : source.taxRegistrationNo();
        result.MarkupId = source.markupId() === undefined ? null : source.markupId();
        //Tax Rates
        result.TaxRates = [];
        _.each(source.taxRatesInMyOrganization(), function (item) {
            result.TaxRates.push(TaxRateServerMapper(item));
        });
        //Markup
        result.Markups = [];
        _.each(source.markupsInMyOrganization(), function (item) {
            result.Markups.push(MarkupServerMapper(item));
        });
        //
        //Chart Of Accounts
        result.ChartOfAccounts = [];
        _.each(source.chartOfAccountsInMyOrganization(), function (item) {
            result.ChartOfAccounts.push(ChartOfAccountServerMapper(item));
        });

        return result;
    };
    //Convert Client To Server
    var ChartOfAccountServerMapper = function (source) {
        var result = {};
        result.Id = source.id() === undefined ? 0 : source.id();
        result.Name = source.name() === undefined ? null : source.name();
        result.AccountNo = source.accountNo() === undefined ? null : source.accountNo();
        return result;
    };
    //Convert Client To Server
    var MarkupServerMapper = function (source) {
        var result = {};
        result.MarkUpId = source.id() === undefined ? 0 : source.id();
        result.MarkUpName = source.name() === undefined ? null : source.name();
        result.MarkUpRate = source.rate() === undefined ? null : source.rate();
        return result;
    };
    //Convert Client To Server
    var TaxRateServerMapper = function (source) {
        var result = {};
        result.TaxId = source.id() === undefined ? 0 : source.id();
        result.TaxName = source.name() === undefined ? null : source.name();
        result.Tax1 = source.tax1() === undefined ? null : source.tax1();
        return result;
    };
    // Convert Client to server
    var OrganizationServerMapperForId = function (source) {
        var result = {};
        result.organisationId = source.id() === undefined ? 0 : source.id();
        return result;
    };

    return {
        CompanySites: CompanySites,
        ChartOfAccount: ChartOfAccount,
        Markup: Markup,
        TaxRate: TaxRate,
        CompanySitesClientMapper: CompanySitesClientMapper,
        ChartOfAccountClientMapper: ChartOfAccountClientMapper,
        MarkupClientMapper: MarkupClientMapper,
        TaxRateClientMapper: TaxRateClientMapper,
        CompanySitesServerMapper: CompanySitesServerMapper,
        ChartOfAccountServerMapper: ChartOfAccountServerMapper,
        MarkupServerMapper: MarkupServerMapper,
        TaxRateServerMapper: TaxRateServerMapper,
        OrganizationServerMapperForId: OrganizationServerMapperForId,
    };
});