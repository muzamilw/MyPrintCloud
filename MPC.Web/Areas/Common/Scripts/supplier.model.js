define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    //Supplier List view Entity
    SupplierListView = function (specifiedSupplierId, specifiedName, specifiedUrl
                           ) {
        var
            self,
            //Unique ID
            supplierId = ko.observable(specifiedSupplierId),
            //Name
            name = ko.observable(specifiedName),
            //URL
            url = ko.observable(specifiedUrl),
            //is Selected
            isSelected = ko.observable(false);

        self = {
            supplierId: supplierId,
            name: name,
            url: url,
            isSelected: isSelected,
        };
        return self;
    };
    //Supplier
    var Supplier = function (specifiedId, specifiedName, specifiedAccountNumber, specifiedURL, specifiedCreditReference, specifiedCreditLimit, specifiedTerms
        , specifiedTypeId, specifiedDefaultNominalCode, specifiedDefaultMarkUpId, specifiedAccountOpenDate, specifiedAccountManagerId, specifiedStatus, specifiedNotes
        , specifiedAccountStatusId, specifiedVATRegNumber, specifiedVATRegReference, specifiedFlagId, specifiedPriceFlagId) {
        var
            self,
            //cost Price Id
            id = ko.observable(specifiedId),
            //Company Name
            name = ko.observable(specifiedName).extend({ required: true }),
            accountNumber = ko.observable(specifiedAccountNumber),
            url = ko.observable(specifiedURL),
            creditReference = ko.observable(specifiedCreditReference),
            creditLimit = ko.observable(specifiedCreditLimit).extend({ number: true }),
             terms = ko.observable(specifiedTerms),
             typeId = ko.observable(specifiedTypeId),
             defaultNominalCode = ko.observable(specifiedDefaultNominalCode),
            defaultMarkUpId = ko.observable(specifiedDefaultMarkUpId),
             accountOpenDate = ko.observable(specifiedAccountOpenDate),
             accountManagerId = ko.observable(specifiedAccountManagerId),
            status = ko.observable(specifiedStatus),
             notes = ko.observable(specifiedNotes),
             accountStatusId = ko.observable(specifiedAccountStatusId),
             vATRegNumber = ko.observable(specifiedVATRegNumber),
             vATRegReference = ko.observable(specifiedVATRegReference),
             flagId = ko.observable(specifiedFlagId),
             priceFlagId = ko.observable(specifiedPriceFlagId),
             //Address In Supplier
             addressInSupplier = ko.observable(Address.Create()),
             //Company Contact
             companyContact = ko.observable(CompanyContact.Create()),
                // Errors
            errors = ko.validation.group({
                name: name,
                creditLimit: creditLimit
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
                    CompanyId: id(),
                    Name: name(),
                    AccountNumber: accountNumber(),
                    URL: url(),
                    CreditReference: creditReference(),
                    CreditLimit: creditLimit(),
                    Terms: terms(),
                    TypeId: typeId(),
                    DefaultNominalCode: defaultNominalCode(),
                    DefaultMarkUpId: defaultMarkUpId(),
                    AccountOpenDate: accountOpenDate() === undefined || accountOpenDate() === null ? undefined : moment(accountOpenDate()).format(ist.utcFormat),
                    AccountManagerId: accountManagerId(),
                    Status: status(),
                    Notes: notes(),
                    AccountStatusId: accountStatusId(),
                    VATRegNumber: vATRegNumber(),
                    VATRegReference: vATRegReference(),
                    FlagId: flagId(),
                    PriceFlagId: priceFlagId(),
                }
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            id: id,
            name: name,
            accountNumber: accountNumber,
            url: url,
            creditReference: creditReference,
            creditLimit: creditLimit,
            terms: terms,
            typeId: typeId,
            defaultNominalCode: defaultNominalCode,
            defaultMarkUpId: defaultMarkUpId,
            accountOpenDate: accountOpenDate,
            accountManagerId: accountManagerId,
            status: status,
            notes: notes,
            accountStatusId: accountStatusId,
            vATRegNumber: vATRegNumber,
            vATRegReference: vATRegReference,
            flagId: flagId,
            priceFlagId: priceFlagId,
            addressInSupplier: addressInSupplier,
            companyContact: companyContact,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    //Address
    var Address = function (specifiedAddressName, specifiedAddress1, specifiedAddress2, specifiedAddress3, specifiedCity, specifiedState, specifiedCountry,
        specifiedPostCode, specifiedFax, specifiedEmail, specifiedURL, specifiedTel1, specifiedTel2, specifiedExtension1, specifiedExtension2, specifiedReference
        , specifiedFAO) {
        var
            self,
            addressName = ko.observable(specifiedAddressName).extend({ required: true }),
            address1 = ko.observable(specifiedAddress1).extend({ required: true }),
            address2 = ko.observable(specifiedAddress2),
            address3 = ko.observable(specifiedAddress3),
            city = ko.observable(specifiedCity).extend({ required: true }),
            state = ko.observable(specifiedState),
            country = ko.observable(specifiedCountry),
            postCode = ko.observable(specifiedPostCode),
            fax = ko.observable(specifiedFax),
            email = ko.observable(specifiedEmail).extend({ email: true }),
            url = ko.observable(specifiedURL),
            tel1 = ko.observable(specifiedTel1),
            tel2 = ko.observable(specifiedTel2),
            extension1 = ko.observable(specifiedExtension1),
            extension2 = ko.observable(specifiedExtension2),
            reference = ko.observable(specifiedReference),
            fao = ko.observable(specifiedFAO),
            // Errors
            errors = ko.validation.group({
                addressName: addressName,
                address1: address1,
                email: email,
                city: city,
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
                    Address1: address1(),
                    Address2: address2(),
                    Address3: address3(),
                    City: city(),
                    State: state(),
                    Country: country(),
                    PostCode: postCode(),
                    Fax: fax(),
                    Email: email(),
                    URL: url(),
                    Tel1: tel1(),
                    Tel2: tel2(),
                    Extension1: extension1(),
                    Extension2: extension2(),
                    Reference: reference(),
                    FAO: fao(),
                }
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            addressName: addressName,
            address1: address1,
            address2: address2,
            address3: address3,
            city: city,
            state: state,
            country: country,
            postCode: postCode,
            fax: fax,
            email: email,
            url: url,
            tel1: tel1,
            tel2: tel2,
            extension1: extension1,
            extension2: extension2,
            reference: reference,
            fao: fao,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    //Company Contact
    var CompanyContact = function (specifiedFirstName, specifiedLastName, specifiedHomeTel1, specifiedHomeTel2, specifiedHomeExtension1, specifiedHomeExtension2,
        specifiedMobile, specifiedEmail, specifiedNotes, specifiedHomeAddress1, specifiedHomeCity, specifiedHomeState, specifiedSecretQuestion, specifiedSecretAnswer
        , specifiedPassword, specifiedIsEmailSubscription, specifiedIsNewsLetterSubscription) {
        var
            self,
             firstName = ko.observable(specifiedFirstName).extend({ required: true }),
             lastName = ko.observable(specifiedLastName),
             homeTel1 = ko.observable(specifiedHomeTel1),
             homeTel2 = ko.observable(specifiedHomeTel2),
             homeExtension1 = ko.observable(specifiedHomeExtension1),
             homeExtension2 = ko.observable(specifiedHomeExtension2),
             mobile = ko.observable(specifiedMobile),
             email = ko.observable(specifiedEmail).extend({ required: true, email: true }),
             notes = ko.observable(specifiedNotes),
             homeAddress1 = ko.observable(specifiedHomeAddress1),
             homeCity = ko.observable(specifiedHomeCity),
             homeState = ko.observable(specifiedHomeState),
             secretQuestion = ko.observable(specifiedSecretQuestion),
             secretAnswer = ko.observable(specifiedSecretAnswer),
             password = ko.observable(specifiedPassword).extend({ required: true }),
             password1 = ko.observable(undefined).extend({ compareWith: password }),
             isEmailSubscription = ko.observable(specifiedIsEmailSubscription),
             isNewsLetterSubscription = ko.observable(specifiedIsNewsLetterSubscription),
             // Errors
            errors = ko.validation.group({
                email: email,
                password1: password1,
                password: password,
                firstName: firstName
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
                    Address1: address1(),
                    FirstName: firstName,
                    LastName: lastName,
                    HomeTel1: homeTel1,
                    HomeTel2: homeTel2,
                    HomeExtension1: homeExtension1,
                    HomeExtension2: homeExtension2,
                    Mobile: mobile,
                    Email: email,
                    Notes: notes,
                    HomeAddress1: homeAddress1,
                    HomeCity: homeCity,
                    HomeState: homeState,
                    SecretQuestion: secretQuestion,
                    SecretAnswer: secretAnswer,
                    Password: password,
                    IsEmailSubscription: isEmailSubscription,
                    IsNewsLetterSubscription: isNewsLetterSubscription,
                }
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            firstName: firstName,
            lastName: lastName,
            homeTel1: homeTel1,
            homeTel2: homeTel2,
            homeExtension1: homeExtension1,
            homeExtension2: homeExtension2,
            mobile: mobile,
            email: email,
            notes: notes,
            homeAddress1: homeAddress1,
            homeCity: homeCity,
            homeState: homeState,
            secretQuestion: secretQuestion,
            secretAnswer: secretAnswer,
            password: password,
            password1: password1,
            isEmailSubscription: isEmailSubscription,
            isNewsLetterSubscription: isNewsLetterSubscription,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    //Create Factory 
    SupplierListView.Create = function (source) {
        return new SupplierListView(source.SupplierId, source.Name, source.URL);
    };

    Supplier.Create = function () {
        return new Supplier(0, undefined, "Supplier", undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, "Supplier", undefined
            , undefined, undefined, undefined, undefined, undefined);
    };
    Address.Create = function () {
        return new Address(undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined
            , undefined, undefined, undefined);
    };

    CompanyContact.Create = function () {
        return new CompanyContact(undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined
            , undefined, undefined, undefined);
    };
    return {
        SupplierListView: SupplierListView,
        Supplier: Supplier,
        Address: Address,
        CompanyContact: CompanyContact,
    };
});