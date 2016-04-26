/*
    Module with the model for the My Organization
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var
    //Oraganisation Entity
    // ReSharper disable InconsistentNaming
     CompanySites = function () {
         // ReSharper restore InconsistentNaming
         var // Reference to this object
             self,
             // Unique key
             id = ko.observable(),
             //Name
             name = ko.observable().extend({ required: true }),
             // logo Image
             orgnizationImage = ko.observable(),
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
            // isImperical
            isImperical = ko.observable(),
            //Tax Registration No
            taxRegistrationNo = ko.observable(),
             agileApiUrl = ko.observable(),
             agileApiKey = ko.observable(),
             isAgileApiActive = ko.observable(),
             unleashedApiId = ko.observable(),
             unleashedApiKey = ko.observable(),
             isUnleashedApiActive = ko.observable(),
             isZapierActive = ko.observable(),
            //Markup ID
            markupId = ko.observable().extend({ required: true }),
            // defaultPO
            defaultPOTax = ko.observable(),
             // BleedAreaSize
            bleedAreaSize = ko.observable(),
             // Show Bleed Area
            showBleedArea = ko.observable(),

            //markups In My Organization
            markupsInMyOrganization = ko.observableArray([]),
           //Chart Of Accounts In My Organization
            chartOfAccountsInMyOrganization = ko.observableArray([]),
            //Flag for change 
            flagForChanges = ko.observable(),

             mailchimpAPIKey = ko.observable(),
             mailchimpAPIId = ko.observable(),
             ismailChimpActive = ko.observable(),
             mailchimpListName = ko.observable(),
             
             orderHeadNote = ko.observable(),
             orderFootNote = ko.observable(),
             invoiceHeadNote = ko.observable(),
             invoiceFootNote = ko.observable(),
             deliveryHeadNote = ko.observable(),
             deliveryFootNote = ko.observable(),
             purchaseHeadNote = ko.observable(),
             purchaseFootNote = ko.observable(),
             xeroConsumerKey = ko.observable(),
             xeroConsumerSecret = ko.observable(),
             isXeroActive = ko.observable(),
            //Language Editor List
            languageEditors = ko.observableArray([]),
             // Errors
             errors = ko.validation.group({
                 email: email,
                 markupId: markupId
             }),
             // Is Valid
             isValid = ko.computed(function () {
                 return errors().length === 0;
             }),

             // True if the booking has been changed
             // ReSharper disable InconsistentNaming
             dirtyFlag = new ko.dirtyFlag({
                 name: name,
                 orgnizationImage:orgnizationImage,
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
                 markupId: markupId,
                 flagForChanges: flagForChanges,
                 languageEditors: languageEditors,
                 isImperical: isImperical,
                 agileApiUrl: agileApiUrl,
                 defaultPOTax: defaultPOTax,
                 bleedAreaSize: bleedAreaSize,
                 showBleedArea: showBleedArea,
                 agileApiKey: agileApiKey,
                 mailchimpAPIKey: mailchimpAPIKey,
                 mailchimpAPIId: mailchimpAPIId ,
                 ismailChimpActive: ismailChimpActive,
                 mailchimpListName: mailchimpListName,
                 isAgileApiActive: isAgileApiActive,
                 unleashedApiId: unleashedApiId,
                 unleashedApiKey: unleashedApiKey,
                 isUnleashedApiActive: isUnleashedApiActive,
                 isZapierActive: isZapierActive,
                 orderHeadNote: orderHeadNote,
                 orderFootNote: orderFootNote,
                 invoiceHeadNote: invoiceHeadNote,
                 invoiceFootNote: invoiceFootNote,
                 deliveryHeadNote: deliveryHeadNote,
                 deliveryFootNote: deliveryFootNote,
                 purchaseHeadNote: purchaseHeadNote,
                 purchaseFootNote: purchaseFootNote,
                 xeroConsumerKey: xeroConsumerKey,
                 xeroConsumerSecret: xeroConsumerSecret,
                 isXeroActive: isXeroActive
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
             orgnizationImage:orgnizationImage,
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
             chartOfAccountsInMyOrganization: chartOfAccountsInMyOrganization,
             flagForChanges: flagForChanges,
             languageEditors: languageEditors,
             isImperical: isImperical,
             agileApiUrl: agileApiUrl,
             defaultPOTax: defaultPOTax,
             bleedAreaSize: bleedAreaSize,
             showBleedArea: showBleedArea,
             agileApiKey: agileApiKey,
             mailchimpAPIKey: mailchimpAPIKey,
             mailchimpAPIId: mailchimpAPIId,
             ismailChimpActive: ismailChimpActive,
             mailchimpListName: mailchimpListName,
             isAgileApiActive: isAgileApiActive,
             unleashedApiId: unleashedApiId,
             unleashedApiKey: unleashedApiKey,
             isUnleashedApiActive: isUnleashedApiActive,
             isZapierActive: isZapierActive,
             orderHeadNote: orderHeadNote,
             orderFootNote: orderFootNote,
             invoiceHeadNote: invoiceHeadNote,
             invoiceFootNote: invoiceFootNote,
             deliveryHeadNote: deliveryHeadNote,
             deliveryFootNote: deliveryFootNote,
             purchaseHeadNote: purchaseHeadNote,
             purchaseFootNote: purchaseFootNote,
             errors: errors,
             isValid: isValid,
             dirtyFlag: dirtyFlag,
             hasChanges: hasChanges,
             reset: reset,
             xeroConsumerKey: xeroConsumerKey,
             xeroConsumerSecret: xeroConsumerSecret,
             isXeroActive: isXeroActive
         };
         return self;
     };
    //Language Editor
    LanguageEditor = function (specifiedKey, specifiedValue) {
        var // Reference to this object
         self,
         //Key
         key = ko.observable(specifiedKey),
         //Value
         value = ko.observable(specifiedValue),

         //Convert To Server
         convertToServerData = function () {
             return {
                 Key: key(),
                 Value: value(),
             };
         },
            // Errors
         errors = ko.validation.group({

         }),
         // Is Valid
         isValid = ko.computed(function () {
             return errors().length === 0;
         }),
        // ReSharper disable InconsistentNaming
         dirtyFlag = new ko.dirtyFlag({
             key: key,
             value: value,
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
            key: key,
            value: value,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset,
        };
        return self;
    };
    LanguageEditor.Create = function (source) {
        return LanguageEditor(source.Key, source.Value);
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
            // Rate Ui
            rateUi = ko.computed({
                read: function() {
                    return rate();
                },
                write: function(value) {
                    if (value < 0 || value === rate()) {
                        if (value < 0) {
                            rate(value);
                            rate(0);
                        }
                        return;
                    }
                    rate(value);
                }
            }),
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
            rateUi: rateUi,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset
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
        companySites.state(source.StateId === null ? undefined : source.StateId);
        companySites.country(source.CountryId === null ? undefined : source.CountryId);
        // companySites.languageEditor(source.LanguageEditor === null ? undefined : LanguageEditor.Create(source.LanguageEditor));
        companySites.orgnizationImage(source.ImageSource);
        companySites.isImperical(source.IsImperical);
        companySites.agileApiKey(source.AgileApiKey);
        companySites.agileApiUrl(source.AgileApiUrl);
        companySites.defaultPOTax(source.DefaultPOTax);
        companySites.bleedAreaSize(source.BleedAreaSize);
        companySites.showBleedArea(source.ShowBleedArea);
        companySites.isAgileApiActive(source.isAgileActive);
        companySites.unleashedApiId(source.XeroApiId);
        companySites.unleashedApiKey(source.XeroApiKey);
        companySites.isUnleashedApiActive(source.isXeroIntegrationRequired);
        companySites.isZapierActive(source.IsZapierEnable);
        companySites.mailchimpAPIKey(source.MailChimpApikey);
        companySites.mailchimpAPIId(source.MailChimpApiId);
        companySites.ismailChimpActive(source.isMailChimpActive);
        companySites.mailchimpListName(source.MailChimpListName);
        
        companySites.orderHeadNote(source.OrderHeadNote);
        companySites.orderFootNote(source.OrderFootNote);
        companySites.invoiceHeadNote(source.InvoiceHeadNote);
        companySites.invoiceFootNote(source.InvoiceFootNote);
        companySites.deliveryHeadNote(source.DeliveryHeadNote);
        companySites.deliveryFootNote(source.DeliveryFootNote);
        companySites.purchaseHeadNote(source.PurchaseHeadNote);
        companySites.purchaseFootNote(source.PurchaseFootNote);
        companySites.xeroConsumerKey(source.XeroConsumerKey);
        companySites.xeroConsumerSecret(source.XeroConsumerSecret);
        companySites.isXeroActive(source.IsXeroActive);
        
        
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
    //Convert Client To Server
    var CompanySitesServerMapper = function (source) {
        var result = {};
        result.OrganisationId = source.id() === undefined ? 0 : source.id();
        result.OrganisationName = source.name() === undefined ? null : source.name();
        result.Address1 = source.address1() === undefined ? null : source.address1();
        result.Address2 = source.address2() === undefined ? null : source.address2();
        result.City = source.city() === undefined ? null : source.city();
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
        result.CountryId = source.country() === undefined ? null : source.country();
        result.StateId = source.state() === undefined ? null : source.state();
        result.IsImperical = source.isImperical() === undefined ? null : source.isImperical();
        result.AgileApiUrl = source.agileApiUrl() === undefined ? null : source.agileApiUrl();
        result.AgileApiKey = source.agileApiKey() === undefined ? null : source.agileApiKey();
        result.isAgileActive = source.isAgileApiActive() === undefined ? null : source.isAgileApiActive();
        
        result.XeroApiId = source.unleashedApiId() === undefined ? null : source.unleashedApiId();
        result.XeroApiKey = source.unleashedApiKey() === undefined ? null : source.unleashedApiKey();
        result.isXeroIntegrationRequired = source.isUnleashedApiActive() === undefined ? null : source.isUnleashedApiActive();
        result.IsZapierEnable = source.isZapierActive() === undefined ? null : source.isZapierActive();
        result.DefaultPOTax = source.defaultPOTax() === undefined ? null : source.defaultPOTax();
        result.BleedAreaSize = source.bleedAreaSize() === undefined ? null : source.bleedAreaSize();
        result.ShowBleedArea = source.showBleedArea() === undefined ? null : source.showBleedArea();
        result.MailChimpApikey = source.mailchimpAPIKey() === undefined ? null : source.mailchimpAPIKey();
        result.MailChimpApiId = source.mailchimpAPIId() === undefined ? null : source.mailchimpAPIId();
        result.isMailChimpActive = source.ismailChimpActive() === undefined ? null : source.ismailChimpActive();
        result.MailChimpListName = source.mailchimpListName() === undefined ? null : source.mailchimpListName();
        
        result.OrderHeadNote = source.orderHeadNote() === undefined ? null : source.orderHeadNote();
        result.OrderFootNote = source.orderFootNote() === undefined ? null : source.orderFootNote();
        result.InvoiceHeadNote = source.invoiceHeadNote() === undefined ? null : source.invoiceHeadNote();
        result.InvoiceFootNote = source.invoiceFootNote() === undefined ? null : source.invoiceFootNote();
        result.DeliveryHeadNote = source.deliveryHeadNote() === undefined ? null : source.deliveryHeadNote();
        result.DeliveryFootNote = source.deliveryFootNote() === undefined ? null : source.deliveryFootNote();
        result.PurchaseHeadNote = source.purchaseHeadNote() === undefined ? null : source.purchaseHeadNote();
        result.PurchaseFootNote = source.purchaseFootNote() === undefined ? null : source.purchaseFootNote();
        result.XeroConsumerKey = source.xeroConsumerKey() === undefined ? null : source.xeroConsumerKey();
        result.XeroConsumerSecret = source.xeroConsumerSecret() === undefined ? null : source.xeroConsumerSecret();
        result.IsXeroActive = source.isXeroActive() === undefined ? null : source.isXeroActive();
        
        
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
        //Chart Of Accounts
        result.LanguageEditors = [];
        _.each(source.languageEditors(), function (item) {
            result.LanguageEditors.push(item.convertToServerData());
        });
        result.MisLogo = source.orgnizationImage();
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
        CompanySitesClientMapper: CompanySitesClientMapper,
        ChartOfAccountClientMapper: ChartOfAccountClientMapper,
        MarkupClientMapper: MarkupClientMapper,
        CompanySitesServerMapper: CompanySitesServerMapper,
        ChartOfAccountServerMapper: ChartOfAccountServerMapper,
        MarkupServerMapper: MarkupServerMapper,
        OrganizationServerMapperForId: OrganizationServerMapperForId,
        LanguageEditor: LanguageEditor,
    };
});