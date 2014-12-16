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
            //Account Number
            accountNumber = ko.observable(specifiedAccountNumber),
            //Url
            url = ko.observable(specifiedURL),
            //Credit Reference
            creditReference = ko.observable(specifiedCreditReference),
            //Credit Limit 
            creditLimit = ko.observable(specifiedCreditLimit).extend({ number: true }),
            //Terms 
             terms = ko.observable(specifiedTerms),
             //Type Id
             typeId = ko.observable(specifiedTypeId),
             //Default Nominal Code 
            defaultNominalCode = ko.observable(specifiedDefaultNominalCode),
             defaultMarkUpId = ko.observable(specifiedDefaultMarkUpId),
             //Account Open Date 
             accountOpenDate = ko.observable(specifiedAccountOpenDate),
             //Account Manager Id
             accountManagerId = ko.observable(specifiedAccountManagerId),
             //status 
             status = ko.observable(specifiedStatus),
             //Notes
             notes = ko.observable(specifiedNotes),
             //Account Status Id 
             accountStatusId = ko.observable(specifiedAccountStatusId),
             //Tax Registration NImber
             vATRegNumber = ko.observable(specifiedVATRegNumber),
             vATRegReference = ko.observable(specifiedVATRegReference),
             //Flag Id 
             flagId = ko.observable(specifiedFlagId),
             //Price Flag Id 
             priceFlagId = ko.observable(specifiedPriceFlagId),
             //Address In Supplier
             addressInSupplier = ko.observable(Address.Create()),
             //Company Contact
             companyContact = ko.observable(CompanyContact.Create()),
             //CompanyContactsList
             companyContacts = ko.observableArray([]),
             //Addresses
             addresses = ko.observableArray([]),
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
                    Name: name() === undefined ? null : name(),
                    AccountNumber: accountNumber() === undefined ? null : accountNumber(),
                    URL: url() === undefined ? null : url(),
                    CreditReference: creditReference() === undefined ? null : creditReference(),
                    CreditLimit: creditLimit() === undefined ? null : creditLimit(),
                    Terms: terms() === undefined ? null : terms(),
                    TypeId: typeId() === undefined ? null : typeId(),
                    DefaultNominalCode: defaultNominalCode() === undefined ? null : defaultNominalCode(),
                    DefaultMarkUpId: defaultMarkUpId() === undefined ? null : defaultMarkUpId(),
                    AccountOpenDate: accountOpenDate() === undefined || accountOpenDate() === null ? undefined : moment(accountOpenDate()).format(ist.utcFormat),
                    AccountManagerId: accountManagerId() === undefined ? null : accountManagerId(),
                    Status: 0,
                    Notes: notes() === undefined ? null : notes(),
                    AccountStatusId: accountStatusId() === undefined ? null : accountStatusId(),
                    VATRegNumber: vATRegNumber() === undefined ? null : vATRegNumber(),
                    VATRegReference: vATRegReference() === undefined ? null : vATRegReference(),
                    FlagId: flagId() === undefined ? null : flagId(),
                    PriceFlagId: priceFlagId() === undefined ? null : priceFlagId(),
                    Addresses: addresses(),
                    CompanyContacts: companyContacts(),
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
            companyContacts: companyContacts,
            addresses: addresses,
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
            //Address Name
            addressName = ko.observable(specifiedAddressName).extend({ required: true }),
            address1 = ko.observable(specifiedAddress1).extend({ required: true }),
            address2 = ko.observable(specifiedAddress2),
            address3 = ko.observable(specifiedAddress3),
            city = ko.observable(specifiedCity).extend({ required: true }),
            //Satate
            state = ko.observable(specifiedState),
            //Country
            country = ko.observable(specifiedCountry),
            //Posy Code
            postCode = ko.observable(specifiedPostCode),
           //fax
            fax = ko.observable(specifiedFax),
            //Email
            email = ko.observable(specifiedEmail).extend({ email: true }),
            //URL
            url = ko.observable(specifiedURL),
            //Telephone 1
            tel1 = ko.observable(specifiedTel1),
            tel2 = ko.observable(specifiedTel2),
            extension1 = ko.observable(specifiedExtension1),
            extension2 = ko.observable(specifiedExtension2),
            //Reference
            reference = ko.observable(specifiedReference),
            //FAO
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
                    AddressName: addressName() === undefined ? null : addressName(),
                    Address1: address1() === undefined ? null : address1(),
                    Address2: address2() === undefined ? null : address2(),
                    Address3: address3() === undefined ? null : address3(),
                    City: city() === undefined ? null : city(),
                    State: state() === undefined ? null : state(),
                    Country: country() === undefined ? null : country(),
                    PostCode: postCode() === undefined ? null : postCode(),
                    Fax: fax() === undefined ? null : fax(),
                    Email: email() === undefined ? null : email(),
                    URL: url() === undefined ? null : url(),
                    Tel1: tel1() === undefined ? null : tel1(),
                    Tel2: tel2() === undefined ? null : tel2(),
                    Extension1: extension1() === undefined ? null : extension1(),
                    Extension2: extension2() === undefined ? null : extension2(),
                    Reference: reference() === undefined ? null : reference(),
                    FAO: fao() === undefined ? null : fao(),
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
            //First NAme
             firstName = ko.observable(specifiedFirstName).extend({ required: true }),
             //Last NAme
             lastName = ko.observable(specifiedLastName),
             //Telephone 1
             homeTel1 = ko.observable(specifiedHomeTel1),
             homeTel2 = ko.observable(specifiedHomeTel2),
             //Home Extension 1 
             homeExtension1 = ko.observable(specifiedHomeExtension1),
             homeExtension2 = ko.observable(specifiedHomeExtension2),
             //Mobile
             mobile = ko.observable(specifiedMobile),
             //Email
             email = ko.observable(specifiedEmail).extend({ required: true, email: true }),
             //Notes
             contactNotes = ko.observable(specifiedNotes),
             //Home Address1
             homeAddress1 = ko.observable(specifiedHomeAddress1),
             //City
             homeCity = ko.observable(specifiedHomeCity),
             //State
             homeState = ko.observable(specifiedHomeState),
             secretQuestion = ko.observable(specifiedSecretQuestion),
             //Secret Answer 
             secretAnswer = ko.observable(specifiedSecretAnswer),
             //Password
             password = ko.observable(specifiedPassword).extend({ required: true }),
             //Confirmed Password
             password1 = ko.observable(undefined).extend({ compareWith: password }),
             //Is Email Subscription 
             isEmailSubscription = ko.observable(specifiedIsEmailSubscription),
             //Is News Letter Subscription
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
                    FirstName: firstName() === undefined ? null : firstName(),
                    LastName: lastName() === undefined ? null : lastName(),
                    HomeTel1: homeTel1() === undefined ? null : homeTel1(),
                    HomeTel2: homeTel2() === undefined ? null : homeTel2(),
                    HomeExtension1: homeExtension1() === undefined ? null : homeExtension1(),
                    HomeExtension2: homeExtension2() === undefined ? null : homeExtension2(),
                    Mobile: mobile() === undefined ? null : mobile(),
                    Email: email() === undefined ? null : email(),
                    Notes: contactNotes() === undefined ? null : contactNotes(),
                    HomeAddress1: homeAddress1() === undefined ? null : homeAddress1(),
                    HomeCity: homeCity() === undefined ? null : homeCity(),
                    HomeState: homeState() === undefined ? null : homeState(),
                    SecretQuestion: secretQuestion() === undefined ? null : secretQuestion(),
                    SecretAnswer: secretAnswer() === undefined ? null : secretAnswer(),
                    Password: password() === undefined ? null : password(),
                    IsEmailSubscription: isEmailSubscription() === undefined ? false : isEmailSubscription(),
                    IsNewsLetterSubscription: isNewsLetterSubscription() === undefined ? false : isNewsLetterSubscription(),
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
            contactNotes: contactNotes,
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