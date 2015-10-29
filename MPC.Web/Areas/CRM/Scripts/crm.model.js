
// Customer View 
define(["ko", "underscore", "underscore-ko"], function (ko) {

    // #region ______________  CUSTOMER LIST VIEW MODEL   _________________
    var customerViewListModel = function (companytId, custName, custCraetionDate, custStatus, cusStatusClass, custEmail, cusStoreImageFileBinary,custstoreName) {
        var
            self,
            id = ko.observable(companytId),
            name = ko.observable(custName),
            creationdate = ko.observable(custCraetionDate),
            status = ko.observable(custStatus),
            customerTYpe = ko.observable(undefined),
            statusClass = ko.observable(cusStatusClass),
            storeImageFileBinary = ko.observable(cusStoreImageFileBinary),
            email = ko.observable(custEmail),
            storeName = ko.observable(custstoreName),
            defaultContact = ko.observable(undefined),
            defaultContactEmail = ko.observable(undefined),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function (source) {
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            id: id,
            name: name,
            creationdate: creationdate,
            status: status,
            statusClass: statusClass,
            email: email,
            storeName: storeName,
            customerTYpe:customerTYpe,
            defaultContact: defaultContact,
            defaultContactEmail:defaultContactEmail,
            storeImageFileBinary: storeImageFileBinary,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    customerViewListModel.Create = function (source) {
        var statusClass = null;
        if (source.Status == "Inactive")
            statusClass = 'label label-danger';
        if (source.Status == "Active")
            statusClass = 'label label-success';
        if (source.Status == "Banned")
            statusClass = 'label label-default';
        if (source.Status == "Pending")
            statusClass = 'label label-warning';
        var customerType=null;
        var customer = new customerViewListModel(
            source.CompnayId,
            source.CustomerName,
            source.DateCreted,
            source.Status,
            statusClass,
            source.Email,
            source.StoreImagePath,
            source.StoreName
        );
        customer.defaultContact(source.DefaultContactName);
        customer.defaultContactEmail(source.DefaultContactEmail);
        customer.customerTYpe(source.CustomerType);
        return customer;
    };
    // #endregion

    // #region ______________  CRM SUPPLIER LIST VIEW MODEL   _____________
    // ReSharper disable once InconsistentNaming
    var CrmSupplierListViewModel =
    function (specifiedCompanyId, specifiedName, specifiedStatus, specifiedImage, specifiedUrl, specifiedIsCustomer, specifiedStoreImageFileBinary, specifiedEmail, specifiedCreatedDate) {
        var self,
           companyId = ko.observable(specifiedCompanyId).extend({ required: true }),
           name = ko.observable(specifiedName),
           status = ko.observable(specifiedStatus),
           image = ko.observable(specifiedImage),
           url = ko.observable(specifiedUrl),
           isCustomer = ko.observable(specifiedIsCustomer),
           storeImageFileBinary = ko.observable(specifiedStoreImageFileBinary),
            defaultContact = ko.observable(undefined),
            defaultContactEmail = ko.observable(undefined),
           type = ko.observable(),
            email = ko.observable(specifiedEmail),
           createdDate = ko.observable(specifiedCreatedDate),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                companyId: companyId,
                name: name,
                status: status,
                image: image,
                url: url,
                type: type,
                isCustomer: isCustomer,
                email: email,
                createdDate: createdDate
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                var result = {};
                result.CompanyId = source.companyId();
                result.Name = source.name();
                result.Status = source.status();
                result.Image = source.image();
                result.URL = source.url();
                result.IsCustomer = source.isCustomer();
                result.CreatedDate = source.createdDate();
                return result;
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            companyId: companyId,
            name: name,
            status: status,
            image: image,
            url: url,
            type: type,
            isCustomer: isCustomer,
            storeImageFileBinary: storeImageFileBinary,
            email: email,
            defaultContact: defaultContact,
            defaultContactEmail: defaultContactEmail,
            createdDate: createdDate,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    CrmSupplierListViewModel.CreateFromClientModel = function (source) {
        return new CrmSupplierListViewModel(
            source.companyId,
            source.name,
            source.status,
            source.image,
            source.url,
            source.isCustomer,
            source.email,
            source.createdDate
        );
    };
    CrmSupplierListViewModel.Create = function (source) {

        var crmSupplierListViewModel = new CrmSupplierListViewModel(
           source.CompanyId,
           source.Name,
           source.Status,
           source.Image,
           source.URL,
           source.IsCustomer,
           source.StoreImagePath,
           source.Email,
           source.CreatedDate != null ? moment(source.CreatedDate).format('YYYY/MM/DD') : ''
       );
        crmSupplierListViewModel.defaultContact(source.DefaultContactName);
        crmSupplierListViewModel.defaultContactEmail(source.DefaultContactEmail);

        //if (source.IsCustomer == 0) {
        //    store.type("Supplier");
        //}
        if (source.IsCustomer == 0) {
            crmSupplierListViewModel.type("Prospect");
        }
        if (source.IsCustomer == 1) {
            crmSupplierListViewModel.type("Customer");
        }
        if (source.IsCustomer == 2) {
            crmSupplierListViewModel.type("Supplier");
        }
        if (source.IsCustomer == 3) {
            crmSupplierListViewModel.type(" Corporate Store");
        }
            //else if (source.IsCustomer == 2) {
            //    store.type("Prospect");
            //}
        else if (source.IsCustomer == 4) {
            crmSupplierListViewModel.type("Retail Store");
        }

        return crmSupplierListViewModel;
    };
    // #endregion

    // #region ______________  A D D R E S S   _________________

    // ReSharper disable once InconsistentNaming
    var Address = function (specifiedAddressId, specifiedCompanyId, specifiedAddressName, specifiedAddress1, specifiedAddress2, specifiedAddress3, specifiedCity, specifiedState, specifiedCountry, specifiedStateName, specifiedCountryName, specifiedPostCode, specifiedFax,
        specifiedEmail, specifiedURL, specifiedTel1, specifiedTel2, specifiedExtension1, specifiedExtension2, specifiedReference, specifiedFAO, specifiedIsDefaultAddress, specifiedIsDefaultShippingAddress,
        specifiedisArchived, specifiedTerritoryId, specifiedTerritoryName, specifiedGeoLatitude, specifiedGeoLongitude, specifiedisPrivate,
        specifiedisDefaultTerrorityBilling, specifiedisDefaultTerrorityShipping, specifiedOrganisationId, specifiedStateCode) {
        var
            self,
            addressId = ko.observable(specifiedAddressId),
            companyId = ko.observable(specifiedCompanyId),
            addressName = ko.observable(specifiedAddressName).extend({ required: true }),
            address1 = ko.observable(specifiedAddress1).extend({ required: true }),
            address2 = ko.observable(specifiedAddress2),
            address3 = ko.observable(specifiedAddress3),
            city = ko.observable(specifiedCity).extend({ required: true }),
            state = ko.observable(specifiedState),
            country = ko.observable(specifiedCountry),
            stateName = ko.observable(specifiedStateName),
            stateCode = ko.observable(specifiedStateCode),
            stateNamenCode = ko.computed(function () {
                return stateName() + "( " + stateCode() + " )";
            }),
            countryName = ko.observable(specifiedCountryName),
            postCode = ko.observable(specifiedPostCode),
            fax = ko.observable(specifiedFax),
            email = ko.observable(specifiedEmail),
            uRL = ko.observable(specifiedURL),
            tel1 = ko.observable(specifiedTel1),
            tel2 = ko.observable(specifiedTel2),
            extension1 = ko.observable(specifiedExtension1),
            extension2 = ko.observable(specifiedExtension2),
            reference = ko.observable(specifiedReference),
            fAO = ko.observable(specifiedFAO),
            isDefaultAddress = ko.observable(specifiedIsDefaultAddress),
            isDefaultShippingAddress = ko.observable(specifiedIsDefaultShippingAddress),
            isArchived = ko.observable(specifiedisArchived),
            territoryId = ko.observable(specifiedTerritoryId).extend({ required: true }),
            territoryName = ko.observable(specifiedTerritoryName),
            geoLatitude = ko.observable(specifiedGeoLatitude),
            geoLongitude = ko.observable(specifiedGeoLongitude),
            isPrivate = ko.observable(specifiedisPrivate),
            isDefaultTerrorityBilling = ko.observable(specifiedisDefaultTerrorityBilling),
            isDefaultTerrorityShipping = ko.observable(specifiedisDefaultTerrorityShipping),
            organisationId = ko.observable(specifiedOrganisationId),
            territory = ko.observable(),
            scopeVariables = ko.observableArray([]),
            // Errors
            errors = ko.validation.group({
                addressName: addressName,
                territoryId: territoryId,
                address1: address1,
                city: city
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                addressId: addressId,
                companyId: companyId,
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
                uRL: uRL,
                tel1: tel1,
                tel2: tel2,
                extension1: extension1,
                extension2: extension2,
                reference: reference,
                fAO: fAO,
                isDefaultAddress: isDefaultAddress,
                isDefaultShippingAddress: isDefaultShippingAddress,
                isArchived: isArchived,
                territoryId: territoryId,
                geoLatitude: geoLatitude,
                geoLongitude: geoLongitude,
                isPrivate: isPrivate,
                isDefaultTerrorityBilling: isDefaultTerrorityBilling,
                isDefaultTerrorityShipping: isDefaultTerrorityShipping,
                organisationId: organisationId,
                scopeVariables: scopeVariables
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function (source) {
                return {
                    AddressId: addressId(),
                    CompanyId: companyId(),
                    AddressName: addressName(),
                    Address1: address1(),
                    Address2: address2(),
                    Address3: address3(),
                    City: city(),
                    StateId: state(),
                    CountryId: country(),
                    PostCode: postCode(),
                    Fax: fax(),
                    URL: uRL(),
                    Tel1: tel1(),
                    Tel2: tel2(),
                    Extension1: extension1(),
                    Extension2: extension2(),
                    Reference: reference(),
                    FAO: fAO(),
                    IsDefaultAddress: isDefaultAddress(),
                    IsDefaultShippingAddress: isDefaultShippingAddress(),
                    isArchived: isArchived(),
                    TerritoryId: territoryId(),
                    GeoLatitude: geoLatitude(),
                    GeoLongitude: geoLongitude(),
                    isPrivate: isPrivate(),
                    isDefaultTerrorityBilling: isDefaultTerrorityBilling(),
                    isDefaultTerrorityShipping: isDefaultTerrorityShipping(),
                    Email: email(),
                    OrganisationId: organisationId(),
                    ScopeVariables: []
                    //Territory: territory().convertToServerData(),
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            addressId: addressId,
            companyId: companyId,
            addressName: addressName,
            address1: address1,
            address2: address2,
            address3: address3,
            city: city,
            state: state,
            country: country,
            stateName: stateName,
            stateCode: stateCode,
            stateNamenCode: stateNamenCode,
            countryName: countryName,
            postCode: postCode,
            fax: fax,
            email: email,
            uRL: uRL,
            tel1: tel1,
            tel2: tel2,
            extension1: extension1,
            extension2: extension2,
            reference: reference,
            fAO: fAO,
            isDefaultAddress: isDefaultAddress,
            isDefaultShippingAddress: isDefaultShippingAddress,
            isArchived: isArchived,
            territoryId: territoryId,
            geoLatitude: geoLatitude,
            geoLongitude: geoLongitude,
            isPrivate: isPrivate,
            isDefaultTerrorityBilling: isDefaultTerrorityBilling,
            isDefaultTerrorityShipping: isDefaultTerrorityShipping,
            organisationId: organisationId,
            territory: territory,
            territoryName: territoryName,
            scopeVariables: scopeVariables,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    Address.CreateFromClientModel = function (source) {
        return new Address(
            source.addressId,
            source.companyId,
            source.addressName,
            source.address1,
            source.address2,
            source.address3,
            source.city,
            source.state,
            source.country,
            source.stateName,
            source.countryName,
            source.postCode,
            source.fax,
            source.email,
            source.uRL,
            source.tel1,
            source.tel2,
            source.extension1,
            source.extension2,
            source.reference,
            source.fAO,
            source.isDefaultAddress,
            source.isDefaultShippingAddress,
            source.isArchived,
            source.territoryId,
            source.territoryName,
            source.geoLatitude,
            source.geoLongitude,
            source.isPrivate,
            source.isDefaultTerrorityBilling,
            source.isDefaultTerrorityShipping,
            source.organisationId
        );
    };
    Address.Create = function (source) {
        var address = new Address(
            source.AddressId,
            source.CompanyId,
            source.AddressName,
            source.Address1,
            source.Address2,
            source.Address3,
            source.City,
            source.StateId,
            source.CountryId,
            source.StateName,
            source.CountryName,
            source.PostCode,
            source.Fax,
            source.Email,
            source.URL,
            source.Tel1,
            source.Tel2,
            source.Extension1,
            source.Extension2,
            source.Reference,
            source.FAO,
            source.IsDefaultAddress,
            source.IsDefaultShippingAddress,
            source.isArchived,
            source.TerritoryId,
            source.TerritoryName,
            source.GeoLatitude,
            source.GeoLongitude,
            source.isPrivate,
            source.isDefaultTerrorityBilling,
            source.isDefaultTerrorityShipping,
            source.OrganisationId,
            source.StateCode
        );
        return address;
    };
    // #endregion ______________  A D D R E S S   _________________

    // #region ________________C O M P A N Y   C O N T A C T ___________________

    // ReSharper disable once InconsistentNaming
    // ReSharper restore InconsistentNaming
    var CompanyContact = function (specifiedContactId, specifiedAddressId, specifiedCompanyId, specifiedFirstName, specifiedMiddleName, specifiedLastName, specifiedTitle,
        specifiedHomeTel1, specifiedHomeTel2, specifiedHomeExtension1, specifiedHomeExtension2, specifiedMobile, specifiedEmail, specifiedFAX, specifiedJobTitle, specifiedDOB,
        specifiedNotes, specifiedIsDefaultContact, specifiedHomeAddress1, specifiedHomeAddress2, specifiedHomeCity, specifiedHomeState, specifiedHomePostCode, specifiedHomeCountry,
        specifiedSecretQuestion, specifiedSecretAnswer, specifiedPassword, specifiedURL, specifiedIsEmailSubscription, specifiedIsNewsLetterSubscription, specifiedimage,
        specifiedquickFullName, specifiedquickTitle, specifiedquickCompanyName, specifiedquickAddress1, specifiedquickAddress2, specifiedquickAddress3, specifiedquickPhone,
        specifiedquickFax, specifiedquickEmail, specifiedquickWebsite, specifiedquickCompMessage, specifiedQuestionId, specifiedIsApprover, specifiedisWebAccess, specifiedisPlaceOrder,
        specifiedCreditLimit, specifiedisArchived, specifiedContactRoleId, specifiedTerritoryId, specifiedClaimIdentifer, specifiedAuthentifiedBy, specifiedIsPayByPersonalCreditCard,
        specifiedIsPricingshown, specifiedSkypeId, specifiedLinkedinURL, specifiedFacebookURL, specifiedTwitterURL, specifiedauthenticationToken, specifiedtwitterScreenName,
        specifiedShippingAddressId, specifiedisUserLoginFirstTime, specifiedquickMobileNumber, specifiedquickTwitterId, specifiedquickFacebookId, specifiedquickLinkedInId,
        specifiedquickOtherId, specifiedPOBoxAddress, specifiedCorporateUnit, specifiedOfficeTradingName, specifiedContractorName, specifiedBPayCRN, specifiedABN, specifiedACN,
        specifiedAdditionalField1, specifiedAdditionalField2, specifiedAdditionalField3, specifiedAdditionalField4, specifiedAdditionalField5, specifiedcanUserPlaceOrderWithoutApproval,
        specifiedCanUserEditProfile, specifiedcanPlaceDirectOrder, specifiedOrganisationId, specifiedSecondaryEmail, specifiedBussinessAddressId, specifiedRoleName, specifiedStoreName) {
        var self,
            contactId = ko.observable(specifiedContactId),
            addressId = ko.observable(specifiedAddressId),
            companyId = ko.observable(specifiedCompanyId),
            companyName = ko.observable(undefined),
            firstName = ko.observable(specifiedFirstName).extend({ required: { params: true, message: 'This field is required!' } }),
            middleName = ko.observable(specifiedMiddleName),
            lastName = ko.observable(specifiedLastName),
            title = ko.observable(specifiedTitle),
            homeTel1 = ko.observable(specifiedHomeTel1),
            homeTel2 = ko.observable(specifiedHomeTel2),
            homeExtension1 = ko.observable(specifiedHomeExtension1),
            homeExtension2 = ko.observable(specifiedHomeExtension2),
            mobile = ko.observable(specifiedMobile),
            email = ko.observable(specifiedEmail).extend({ required: { params: true, message: 'Please enter Valid Email Address!' }, email: { params: true, message: 'Please enter Valid Email Address!' } }),
            fAX = ko.observable(specifiedFAX),
            jobTitle = ko.observable(specifiedJobTitle),
            dOB = ko.observable(specifiedDOB),
            notes = ko.observable(specifiedNotes),
            isDefaultContact = ko.observable(specifiedIsDefaultContact),
            homeAddress1 = ko.observable(specifiedHomeAddress1),
            homeAddress2 = ko.observable(specifiedHomeAddress2),
            homeCity = ko.observable(specifiedHomeCity),
            homeState = ko.observable(specifiedHomeState),
            homePostCode = ko.observable(specifiedHomePostCode),
            homeCountry = ko.observable(specifiedHomeCountry),
            secretQuestion = ko.observable(specifiedSecretQuestion),
            secretAnswer = ko.observable(specifiedSecretAnswer),
            password = ko.observable(specifiedPassword).extend({ required: { params: true, message: 'This field is required with minimum 6 characters!' }, minLength: 6 }),
            uRL = ko.observable(specifiedURL),
            isEmailSubscription = ko.observable(specifiedIsEmailSubscription),
            isNewsLetterSubscription = ko.observable(specifiedIsNewsLetterSubscription),
            image = ko.observable(specifiedimage),
            quickFullName = ko.observable(specifiedquickFullName),
            quickTitle = ko.observable(specifiedquickTitle),
            quickCompanyName = ko.observable(specifiedquickCompanyName),
            quickAddress1 = ko.observable(specifiedquickAddress1),
            quickAddress2 = ko.observable(specifiedquickAddress2),
            quickAddress3 = ko.observable(specifiedquickAddress3),
            quickPhone = ko.observable(specifiedquickPhone),
            quickFax = ko.observable(specifiedquickFax),
            quickEmail = ko.observable(specifiedquickEmail),
            quickWebsite = ko.observable(specifiedquickWebsite),
            quickCompMessage = ko.observable(specifiedquickCompMessage),
            questionId = ko.observable(specifiedQuestionId),
            isApprover = ko.observable(specifiedIsApprover),
            isWebAccess = ko.observable(specifiedisWebAccess),
            isPlaceOrder = ko.observable(specifiedisPlaceOrder),
            creditLimit = ko.observable(specifiedCreditLimit),
            isArchived = ko.observable(specifiedisArchived),
            contactRoleId = ko.observable(specifiedContactRoleId),
            territoryId = ko.observable(specifiedTerritoryId),
            claimIdentifer = ko.observable(specifiedClaimIdentifer),
            authentifiedBy = ko.observable(specifiedAuthentifiedBy),
            isPayByPersonalCreditCard = ko.observable(specifiedIsPayByPersonalCreditCard),
            isPricingshown = ko.observable(specifiedIsPricingshown),
            skypeId = ko.observable(specifiedSkypeId),
            linkedinURL = ko.observable(specifiedLinkedinURL),
            facebookURL = ko.observable(specifiedFacebookURL),
            twitterURL = ko.observable(specifiedTwitterURL),
            authenticationToken = ko.observable(specifiedauthenticationToken),
            twitterScreenName = ko.observable(specifiedtwitterScreenName),
            shippingAddressId = ko.observable(specifiedShippingAddressId),
            isUserLoginFirstTime = ko.observable(specifiedisUserLoginFirstTime),
            quickMobileNumber = ko.observable(specifiedquickMobileNumber),
            quickTwitterId = ko.observable(specifiedquickTwitterId),
            quickFacebookId = ko.observable(specifiedquickFacebookId),
            quickLinkedInId = ko.observable(specifiedquickLinkedInId),
            quickOtherId = ko.observable(specifiedquickOtherId),
            pOBoxAddress = ko.observable(specifiedPOBoxAddress),
            corporateUnit = ko.observable(specifiedCorporateUnit),
            officeTradingName = ko.observable(specifiedOfficeTradingName),
            contractorName = ko.observable(specifiedContractorName),
            bPayCRN = ko.observable(specifiedBPayCRN),
            aBN = ko.observable(specifiedABN),
            aCN = ko.observable(specifiedACN),
            additionalField1 = ko.observable(specifiedAdditionalField1),
            additionalField2 = ko.observable(specifiedAdditionalField2),
            additionalField3 = ko.observable(specifiedAdditionalField3),
            additionalField4 = ko.observable(specifiedAdditionalField4),
            additionalField5 = ko.observable(specifiedAdditionalField5),
            canUserPlaceOrderWithoutApproval = ko.observable(specifiedcanUserPlaceOrderWithoutApproval),
            canUserEditProfile = ko.observable(specifiedCanUserEditProfile),
            canPlaceDirectOrder = ko.observable(specifiedcanPlaceDirectOrder),
            organisationId = ko.observable(specifiedOrganisationId),
            secondaryEmail = ko.observable(specifiedSecondaryEmail).extend({ email: true }),
            bussinessAddressId = ko.observable(specifiedBussinessAddressId).extend({ required: true }),
            roleName = ko.observable(specifiedRoleName),
            fileName = ko.observable(),
            bussinessAddress = ko.observable(),
            shippingAddress = ko.observable(),
            stateName = ko.observable(),
            StoreName = ko.observable(specifiedStoreName),
            companyContactVariables = ko.observableArray([]),
            confirmPassword = ko.observable(specifiedPassword).extend({ compareWith: password }),


            // Errors
            errors = ko.validation.group({
                firstName: firstName,
                email: email,
                bussinessAddressId: bussinessAddressId,
                password: password,
                confirmPassword: confirmPassword,
                secondaryEmail: secondaryEmail
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                contactId: contactId,
                addressId: addressId,
                companyId: companyId,
                firstName: firstName,
                middleName: middleName,
                lastName: lastName,
                title: title,
                homeTel1: homeTel1,
                homeTel2: homeTel2,
                homeExtension1: homeExtension1,
                homeExtension2: homeExtension2,
                mobile: mobile,
                email: email,
                fAX: fAX,
                jobTitle: jobTitle,
                dOB: dOB,
                notes: notes,
                isDefaultContact: isDefaultContact,
                homeAddress1: homeAddress1,
                homeAddress2: homeAddress2,
                homeCity: homeCity,
                homeState: homeState,
                homePostCode: homePostCode,
                homeCountry: homeCountry,
                secretQuestion: secretQuestion,
                secretAnswer: secretAnswer,
                password: password,
                uRL: uRL,
                isEmailSubscription: isEmailSubscription,
                isNewsLetterSubscription: isNewsLetterSubscription,
                image: image,
                quickFullName: quickFullName,
                quickTitle: quickTitle,
                quickCompanyName: quickCompanyName,
                quickAddress1: quickAddress1,
                quickAddress2: quickAddress2,
                quickAddress3: quickAddress3,
                quickPhone: quickPhone,
                quickFax: quickFax,
                quickEmail: quickEmail,
                quickWebsite: quickWebsite,
                quickCompMessage: quickCompMessage,
                questionId: questionId,
                isApprover: isApprover,
                isWebAccess: isWebAccess,
                isPlaceOrder: isPlaceOrder,
                creditLimit: creditLimit,
                isArchived: isArchived,
                contactRoleId: contactRoleId,
                territoryId: territoryId,
                claimIdentifer: claimIdentifer,
                authentifiedBy: authentifiedBy,
                isPayByPersonalCreditCard: isPayByPersonalCreditCard,
                isPricingshown: isPricingshown,
                skypeId: skypeId,
                linkedinURL: linkedinURL,
                facebookURL: facebookURL,
                twitterURL: twitterURL,
                authenticationToken: authenticationToken,
                twitterScreenName: twitterScreenName,
                shippingAddressId: shippingAddressId,
                isUserLoginFirstTime: isUserLoginFirstTime,
                quickMobileNumber: quickMobileNumber,
                quickTwitterId: quickTwitterId,
                quickFacebookId: quickFacebookId,
                quickLinkedInId: quickLinkedInId,
                quickOtherId: quickOtherId,
                pOBoxAddress: pOBoxAddress,
                corporateUnit: corporateUnit,
                officeTradingName: officeTradingName,
                contractorName: contractorName,
                bPayCRN: bPayCRN,
                aBN: aBN,
                aCN: aCN,
                additionalField1: additionalField1,
                additionalField2: additionalField2,
                additionalField3: additionalField3,
                additionalField4: additionalField4,
                additionalField5: additionalField5,
                canUserPlaceOrderWithoutApproval: canUserPlaceOrderWithoutApproval,
                canUserEditProfile: canUserEditProfile,
                canPlaceDirectOrder: canPlaceDirectOrder,
                organisationId: organisationId,
                bussinessAddressId: bussinessAddressId,
                StoreName: StoreName,
                fileName: fileName,
                secondaryEmail: secondaryEmail,
                companyName: companyName
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    ContactId: contactId(),
                    AddressId: addressId(),
                    CompanyId: companyId(),
                    FirstName: firstName(),
                    MiddleName: middleName(),
                    LastName: lastName(),
                    Title: title(),
                    HomeTel1: homeTel1(),
                    HomeTel2: homeTel2(),
                    HomeExtension1: homeExtension1(),
                    HomeExtension2: homeExtension2(),
                    Mobile: mobile(),
                    Email: email(),
                    FAX: fAX(),
                    JobTitle: jobTitle(),
                    DOB: dOB(),
                    Notes: notes(),
                    IsDefaultContact: isDefaultContact() == true ? 1 : 0,
                    HomeAddress1: homeAddress1(),
                    HomeAddress2: homeAddress2(),
                    HomeCity: homeCity(),
                    HomeState: homeState(),
                    HomePostCode: homePostCode(),
                    HomeCountry: homeCountry(),
                    SecretQuestion: secretQuestion(),
                    SecretAnswer: secretAnswer(),
                    Password: password(),
                    URL: uRL(),
                    IsEmailSubscription: isEmailSubscription(),
                    IsNewsLetterSubscription: isNewsLetterSubscription(),
                    ImageBytes: image(),
                    quickFullName: quickFullName(),
                    quickTitle: quickTitle(),
                    quickCompanyName: quickCompanyName(),
                    quickAddress1: quickAddress1(),
                    quickAddress2: quickAddress2(),
                    quickAddress3: quickAddress3(),
                    quickPhone: quickPhone(),
                    quickFax: quickFax(),
                    quickEmail: quickEmail(),
                    quickWebsite: quickWebsite(),
                    quickCompMessage: quickCompMessage(),
                    QuestionId: questionId(),
                    IsApprover: isApprover(),
                    isWebAccess: isWebAccess(),
                    isPlaceOrder: isPlaceOrder(),
                    CreditLimit: creditLimit(),
                    isArchived: isArchived(),
                    ContactRoleId: contactRoleId(),
                    TerritoryId: territoryId(),
                    ClaimIdentifer: claimIdentifer(),
                    AuthentifiedBy: authentifiedBy(),
                    IsPayByPersonalCreditCard: isPayByPersonalCreditCard(),
                    IsPricingshown: isPricingshown(),
                    SkypeId: skypeId(),
                    LinkedinURL: linkedinURL(),
                    FacebookURL: facebookURL(),
                    TwitterURL: twitterURL(),
                    authenticationToken: authenticationToken(),
                    twitterScreenName: twitterScreenName(),
                    ShippingAddressId: shippingAddressId(),
                    isUserLoginFirstTime: isUserLoginFirstTime(),
                    quickMobileNumber: quickMobileNumber(),
                    quickTwitterId: quickTwitterId(),
                    quickFacebookId: quickFacebookId(),
                    quickLinkedInId: quickLinkedInId(),
                    quickOtherId: quickOtherId(),
                    POBoxAddress: pOBoxAddress(),
                    CorporateUnit: corporateUnit(),
                    OfficeTradingName: officeTradingName(),
                    ContractorName: contractorName(),
                    BPayCRN: bPayCRN(),
                    ABN: aBN(),
                    ACN: aCN(),
                    AdditionalField1: additionalField1(),
                    AdditionalField2: additionalField2(),
                    AdditionalField3: additionalField3(),
                    AdditionalField4: additionalField4(),
                    AdditionalField5: additionalField5(),
                    canUserPlaceOrderWithoutApproval: canUserPlaceOrderWithoutApproval(),
                    CanUserEditProfile: canUserEditProfile(),
                    canPlaceDirectOrder: !isPayByPersonalCreditCard() ? false : canPlaceDirectOrder(),
                    OrganisationId: organisationId(),
                    BussinessAddressId: bussinessAddressId(),
                    FileName: fileName(),
                    StoreName: StoreName(),
                    SecondaryEmail: secondaryEmail(),
                    ScopVariables: []
                    //BussinessAddress: bussinessAddress() != undefined ? bussinessAddress().convertToServerData(): null,
                    //ShippingAddress: shippingAddress() != undefined ? shippingAddress().convertToServerData() : null,
                };
            },
             update = function (source) {
                 contactId(source.contactId());
                 contactId(source.contactId());
                 addressId(source.addressId());
                 companyId(source.companyId());
                 firstName(source.firstName());
                 middleName(source.middleName());
                 lastName(source.lastName());
                 title(source.title());
                 homeTel1(source.homeTel1());
                 homeTel2(source.homeTel2());
                 homeExtension1(source.homeExtension1());
                 homeExtension2(source.homeExtension2());
                 mobile(source.mobile());
                 email(source.email());
                 fAX(source.fAX());
                 jobTitle(source.jobTitle());
                 dOB(source.dOB());
                 notes(source.notes());
                 isDefaultContact(source.isDefaultContact());
                 homeAddress1(source.homeAddress1());
                 homeAddress2(source.homeAddress2());
                 homeCity(source.homeCity());
                 homeState(source.homeState());
                 homePostCode(source.homePostCode());
                 homeCountry(source.homeCountry());
                 secretQuestion(source.secretQuestion());
                 secretAnswer(source.secretAnswer());
                 password(source.password());
                 uRL(source.uRL());
                 isEmailSubscription(source.isEmailSubscription());
                 isNewsLetterSubscription(source.isNewsLetterSubscription());
                 image(source.image());
                 quickFullName(source.quickFullName());
                 quickTitle(source.quickTitle());
                 quickCompanyName(source.quickCompanyName());
                 quickAddress1(source.quickAddress1());
                 quickAddress2(source.quickAddress2());
                 quickAddress3(source.quickAddress3());
                 quickPhone(source.quickPhone());
                 quickFax(source.quickFax());
                 quickEmail(source.quickEmail());
                 quickWebsite(source.quickWebsite());
                 quickCompMessage(source.quickCompMessage());
                 questionId(source.questionId());
                 isApprover(source.isApprover());
                 isWebAccess(source.isWebAccess());
                 isPlaceOrder(source.isPlaceOrder());
                 creditLimit(source.creditLimit());
                 isArchived(source.isArchived());
                 contactRoleId(source.contactRoleId());
                 territoryId(source.territoryId());
                 claimIdentifer(source.claimIdentifer());
                 authentifiedBy(source.authentifiedBy());
                 isPayByPersonalCreditCard(source.isPayByPersonalCreditCard());
                 isPricingshown(source.isPricingshown());
                 skypeId(source.skypeId());
                 linkedinURL(source.linkedinURL());
                 facebookURL(source.facebookURL());
                 twitterURL(source.twitterURL());
                 authenticationToken(source.authenticationToken());
                 twitterScreenName(source.twitterScreenName());
                 shippingAddressId(source.shippingAddressId());
                 isUserLoginFirstTime(source.isUserLoginFirstTime());
                 quickMobileNumber(source.quickMobileNumber());
                 quickTwitterId(source.quickTwitterId());
                 quickFacebookId(source.quickFacebookId());
                 quickLinkedInId(source.quickLinkedInId());
                 quickOtherId(source.quickOtherId());
                 pOBoxAddress(source.pOBoxAddress());
                 corporateUnit(source.corporateUnit());
                 officeTradingName(source.officeTradingName());
                 contractorName(source.contractorName());
                 bPayCRN(source.bPayCRN());
                 aBN(source.aBN());
                 aCN(source.aCN());
                 additionalField1(source.additionalField1());
                 additionalField2(source.additionalField2());
                 additionalField3(source.additionalField3());
                 additionalField4(source.additionalField4());
                 additionalField5(source.additionalField5());
                 canUserPlaceOrderWithoutApproval(source.canUserPlaceOrderWithoutApproval());
                 canUserEditProfile(source.canUserEditProfile());
                 canPlaceDirectOrder(source.canPlaceDirectOrder());
                 organisationId(source.organisationId());
                 bussinessAddressId(source.bussinessAddressId());
                 confirmPassword(source.confirmPassword());
                 roleName(source.roleName());
                 fileName(source.fileName());
                 bussinessAddress(source.bussinessAddress());
                 shippingAddress(source.shippingAddress());
                 stateName(source.stateName());
                 companyContactVariables(source.companyContactVariables());
                 StoreName(source.StoreName());
                 secondaryEmail(source.secondaryEmail());
             },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            contactId: contactId,
            addressId: addressId,
            companyId: companyId,
            firstName: firstName,
            middleName: middleName,
            lastName: lastName,
            title: title,
            homeTel1: homeTel1,
            homeTel2: homeTel2,
            homeExtension1: homeExtension1,
            homeExtension2: homeExtension2,
            mobile: mobile,
            email: email,
            fAX: fAX,
            jobTitle: jobTitle,
            dOB: dOB,
            notes: notes,
            isDefaultContact: isDefaultContact,
            homeAddress1: homeAddress1,
            homeAddress2: homeAddress2,
            homeCity: homeCity,
            homeState: homeState,
            homePostCode: homePostCode,
            homeCountry: homeCountry,
            secretQuestion: secretQuestion,
            secretAnswer: secretAnswer,
            password: password,
            uRL: uRL,
            isEmailSubscription: isEmailSubscription,
            isNewsLetterSubscription: isNewsLetterSubscription,
            image: image,
            quickFullName: quickFullName,
            companyName:companyName,
            quickTitle: quickTitle,
            quickCompanyName: quickCompanyName,
            quickAddress1: quickAddress1,
            quickAddress2: quickAddress2,
            quickAddress3: quickAddress3,
            quickPhone: quickPhone,
            quickFax: quickFax,
            quickEmail: quickEmail,
            quickWebsite: quickWebsite,
            quickCompMessage: quickCompMessage,
            questionId: questionId,
            isApprover: isApprover,
            isWebAccess: isWebAccess,
            isPlaceOrder: isPlaceOrder,
            creditLimit: creditLimit,
            isArchived: isArchived,
            contactRoleId: contactRoleId,
            territoryId: territoryId,
            claimIdentifer: claimIdentifer,
            authentifiedBy: authentifiedBy,
            isPayByPersonalCreditCard: isPayByPersonalCreditCard,
            isPricingshown: isPricingshown,
            skypeId: skypeId,
            linkedinURL: linkedinURL,
            facebookURL: facebookURL,
            twitterURL: twitterURL,
            authenticationToken: authenticationToken,
            twitterScreenName: twitterScreenName,
            shippingAddressId: shippingAddressId,
            isUserLoginFirstTime: isUserLoginFirstTime,
            quickMobileNumber: quickMobileNumber,
            quickTwitterId: quickTwitterId,
            quickFacebookId: quickFacebookId,
            quickLinkedInId: quickLinkedInId,
            quickOtherId: quickOtherId,
            pOBoxAddress: pOBoxAddress,
            corporateUnit: corporateUnit,
            officeTradingName: officeTradingName,
            contractorName: contractorName,
            bPayCRN: bPayCRN,
            aBN: aBN,
            aCN: aCN,
            additionalField1: additionalField1,
            additionalField2: additionalField2,
            additionalField3: additionalField3,
            additionalField4: additionalField4,
            additionalField5: additionalField5,
            canUserPlaceOrderWithoutApproval: canUserPlaceOrderWithoutApproval,
            canUserEditProfile: canUserEditProfile,
            canPlaceDirectOrder: canPlaceDirectOrder,
            organisationId: organisationId,
            bussinessAddressId: bussinessAddressId,
            confirmPassword: confirmPassword,
            roleName: roleName,
            fileName: fileName,
            bussinessAddress: bussinessAddress,
            shippingAddress: shippingAddress,
            stateName: stateName,
            StoreName: StoreName,
            secondaryEmail: secondaryEmail,
            companyContactVariables: companyContactVariables,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            update: update,
            reset: reset
        };
        return self;
    };
    CompanyContact.CreateFromClientModel = function (source) {
        return new CompanyContact(
            source.contactId,
            source.addressId,
            source.companyId,
            source.firstName,
            source.middleName,
            source.lastName,
            source.title,
            source.homeTel1,
            source.homeTel2,
            source.homeExtension1,
            source.homeExtension2,
            source.mobile,
            source.email,
            source.fAX,
            source.jobTitle,
            source.dOB,
            source.notes,
            source.isDefaultContact,
            source.homeAddress1,
            source.homeAddress2,
            source.homeCity,
            source.homeState,
            source.homePostCode,
            source.homeCountry,
            source.secretQuestion,
            source.secretAnswer,
            source.password,
            source.uRL,
            source.isEmailSubscription,
            source.isNewsLetterSubscription,
            source.image,
            source.quickFullName,
            source.quickTitle,
            source.quickCompanyName,
            source.quickAddress1,
            source.quickAddress2,
            source.quickAddress3,
            source.quickPhone,
            source.quickFax,
            source.quickEmail,
            source.quickWebsite,
            source.quickCompMessage,
            source.questionId,
            source.isApprover,
            source.isWebAccess,
            source.isPlaceOrder,
            source.creditLimit,
            source.isArchived,
            source.contactRoleId,
            source.territoryId,
            source.claimIdentifer,
            source.authentifiedBy,
            source.isPayByPersonalCreditCard,
            source.isPricingshown,
            source.skypeId,
            source.linkedinURL,
            source.facebookURL,
            source.twitterURL,
            source.authenticationToken,
            source.twitterScreenName,
            source.shippingAddressId,
            source.isUserLoginFirstTime,
            source.quickMobileNumber,
            source.quickTwitterId,
            source.quickFacebookId,
            source.quickLinkedInId,
            source.quickOtherId,
            source.pOBoxAddress,
            source.corporateUnit,
            source.officeTradingName,
            source.contractorName,
            source.bPayCRN,
            source.aBN,
            source.aCN,
            source.additionalField1,
            source.additionalField2,
            source.additionalField3,
            source.additionalField4,
            source.additionalField5,
            source.canUserPlaceOrderWithoutApproval,
            source.canUserEditProfile,
            source.canPlaceDirectOrder,
            source.organisationId,
            source.secondaryEmail,
            source.addressId,
            source.FileName,
            source.StoreName
        );
    };
    CompanyContact.Create = function (source) {
        var companyContact = new CompanyContact(
            source.ContactId,
            source.AddressId,
            source.CompanyId,
            source.FirstName,
            source.MiddleName,
            source.LastName,
            source.Title,
            source.HomeTel1,
            source.HomeTel2,
            source.HomeExtension1,
            source.HomeExtension2,
            source.Mobile,
            source.Email,
            source.FAX,
            source.JobTitle,
            source.DOB,
            source.Notes,
            source.IsDefaultContact,
            source.HomeAddress1,
            source.HomeAddress2,
            source.HomeCity,
            source.HomeState,
            source.HomePostCode,
            source.HomeCountry,
            source.SecretQuestion,
            source.SecretAnswer,
            source.Password,
            source.URL,
            source.IsEmailSubscription,
            source.IsNewsLetterSubscription,
            source.ImageSource,
            source.quickFullName,
            source.quickTitle,
            source.quickCompanyName,
            source.quickAddress1,
            source.quickAddress2,
            source.quickAddress3,
            source.quickPhone,
            source.quickFax,
            source.quickEmail,
            source.quickWebsite,
            source.quickCompMessage,
            source.QuestionId,
            source.IsApprover,
            source.isWebAccess,
            source.isPlaceOrder,
            source.CreditLimit,
            source.isArchived,
            source.ContactRoleId,
            source.TerritoryId,
            source.ClaimIdentifer,
            source.AuthentifiedBy,
            source.IsPayByPersonalCreditCard,
            source.IsPricingshown,
            source.SkypeId,
            source.LinkedinURL,
            source.FacebookURL,
            source.TwitterURL,
            source.authenticationToken,
            source.twitterScreenName,
            source.ShippingAddressId,
            source.isUserLoginFirstTime,
            source.quickMobileNumber,
            source.quickTwitterId,
            source.quickFacebookId,
            source.quickLinkedInId,
            source.quickOtherId,
            source.POBoxAddress,
            source.CorporateUnit,
            source.OfficeTradingName,
            source.ContractorName,
            source.BPayCRN,
            source.ABN,
            source.ACN,
            source.AdditionalField1,
            source.AdditionalField2,
            source.AdditionalField3,
            source.AdditionalField4,
            source.AdditionalField5,
            source.canUserPlaceOrderWithoutApproval,
            source.CanUserEditProfile,
            source.canPlaceDirectOrder,
            source.OrganisationId,
            source.SecondaryEmail,
            //source.BussinessAddressId,
            source.AddressId,
            source.RoleName,
            source.FileName,
            source.StoreName
        );
        companyContact.companyName(source.CompanyName);
        companyContact.StoreName(source.StoreName);
        return companyContact;
    };

    // #endregion ________________COMPANY CONTACT ___________________

    // #region ______________  SYSTEM USER   ______________________________
    // ReSharper disable once InconsistentNaming
    var SystemUser = function (specifiedSystemUserId, specifiedUserName, specifiedFullName) {
        var self,
            systemUserId = ko.observable(specifiedSystemUserId),
            userName = ko.observable(specifiedUserName),
            fullName = ko.observable(specifiedFullName),
            // Errors
            errors = ko.validation.group({

            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                // ReSharper restore InconsistentNaming
                systemUserId: systemUserId,
                userName: userName
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.SystemUserId = source.systemUserId();
                result.UserName = source.userName();
                return result;
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            systemUserId: systemUserId,
            userName: userName,
            fullName:fullName,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    SystemUser.CreateFromClientModel = function (source) {
        return new SystemUser(
            source.systemUserId,
            source.userName
        );
    };
    SystemUser.Create = function (source) {
        var systemUser = new SystemUser(
            source.SystemUserId,
            source.UserName,
            source.FullName
        );
        return systemUser;
    };
    // #endregion______________  S Y S T E M     U S E R   _________________

    // #region ______________ STORE _______________________________________

    //WebMasterTag WebAnalyticCode
    // ReSharper disable once InconsistentNaming
    var Store = function (specifiedStoreId, specifiedCompanyId, specifiedName, specifiedStatus, specifiedImage, specifiedUrl, specifiedAccountOpenDate, specifiedAccountManagerId, specifiedAvatRegNumber,
        specifiedAvatRegReference,specifiedCreditLimit, specifiedPhoneNo, specifiedIsCustomer, specifiedNotes, specifiedWebMasterTag, specifiedWebAnalyticCode, specifiedWebAccessCode, specifiedTwitterUrl,
        specifiedFacebookUrl, specifiedLinkedinUrl, specifiedFacebookAppId, specifiedFacebookAppKey, specifiedTwitterAppId, specifiedTwitterAppKey,
        specifiedSalesAndOrderManagerId1, specifiedSalesAndOrderManagerId2, specifiedProductionManagerId1, specifiedProductionManagerId2,
        specifiedStockNotificationManagerId1, specifiedStockNotificationManagerId2, specifiedisDisplayBanners, specifiedisStoreModePrivate, specifiedisTextWatermark,
        specifiedWatermarkText, specifiedisBrokerPaymentRequired, specifiedisBrokerCanAcceptPaymentOnline, specifiedcanUserPlaceOrderWithoutApproval,
        specifiedisIncludeVAT, specifiedincludeEmailBrokerArtworkOrderReport, specifiedincludeEmailBrokerArtworkOrderXML, specifiedincludeEmailBrokerArtworkOrderJobCard
        , specifiedIsDeliveryTaxAble, specifiedPickupAddressId,
        specifiedmakeEmailBrokerArtworkOrderProductionReady, specifiedStoreImageFileBinary, specifiedStoreBackgroudImageSource, specifiedIsShowGoogleMap,
        specifiedDefaultSpriteImageSource, specifiedUserDefinedSpriteImageSource, specifiedUserDefinedSpriteFileName, specifiedCustomCSS, specifiedStoreBackgroundImage, specifiedStoreImagePath
    ) {
        var self,
            //storeId is used for select store dropdown on crm prospect/customer screen
            storeId = ko.observable(specifiedStoreId).extend({ required: true }),
            companyId = ko.observable(specifiedCompanyId), //.extend({ required: true }),
            name = ko.observable(specifiedName).extend({ required: true }),
            status = ko.observable(specifiedStatus),
            image = ko.observable(specifiedImage),
            url = ko.observable(specifiedUrl),
            accountOpenDate = ko.observable(specifiedAccountOpenDate ? moment(specifiedAccountOpenDate).toDate() : undefined),
            accountManagerId = ko.observable(specifiedAccountManagerId),
            avatRegNumber = ko.observable(specifiedAvatRegNumber),
            avatRegReference = ko.observable(specifiedAvatRegReference),
            creditLimit = ko.observable(specifiedCreditLimit),
            phoneNo = ko.observable(specifiedPhoneNo),
            isCustomer = ko.observable(specifiedIsCustomer),
            notes = ko.observable(specifiedNotes),
            webMasterTag = ko.observable(specifiedWebMasterTag),
            webAnalyticCode = ko.observable(specifiedWebAnalyticCode),
            type = ko.observable(),
            storeImagePath = ko.observable(specifiedStoreImagePath),
            //webAccessCode = ko.observable(specifiedWebAccessCode).extend({
            //    required: {
            //        onlyIf: function () {
            //            return type() == 3;
            //        }
            //    }
            //}),
            webAccessCode = ko.observable(specifiedWebAccessCode),//.extend({ required: true })
            twitterUrl = ko.observable(specifiedTwitterUrl),
            facebookUrl = ko.observable(specifiedFacebookUrl),
            linkedinUrl = ko.observable(specifiedLinkedinUrl),
            facebookAppId = ko.observable(specifiedFacebookAppId),
            facebookAppKey = ko.observable(specifiedFacebookAppKey),
            twitterAppId = ko.observable(specifiedTwitterAppId),
            twitterAppKey = ko.observable(specifiedTwitterAppKey),
            salesAndOrderManagerId1 = ko.observable(specifiedSalesAndOrderManagerId1),
            salesAndOrderManagerId2 = ko.observable(specifiedSalesAndOrderManagerId2),
            productionManagerId1 = ko.observable(specifiedProductionManagerId1),
            productionManagerId2 = ko.observable(specifiedProductionManagerId2),
            stockNotificationManagerId1 = ko.observable(specifiedStockNotificationManagerId1),
            stockNotificationManagerId2 = ko.observable(specifiedStockNotificationManagerId2), /////
            isStoreModePrivate = ko.observable(specifiedisStoreModePrivate),
            isTextWatermark = ko.observable(specifiedisTextWatermark),
            watermarkText = ko.observable(specifiedWatermarkText),
            isBrokerPaymentRequired = ko.observable(specifiedisBrokerPaymentRequired),
            isBrokerCanAcceptPaymentOnline = ko.observable(specifiedisBrokerCanAcceptPaymentOnline),
            canUserPlaceOrderWithoutApproval = ko.observable(specifiedcanUserPlaceOrderWithoutApproval),
            isIncludeVAT = ko.observable(specifiedisIncludeVAT),
            includeEmailBrokerArtworkOrderReport = ko.observable(specifiedincludeEmailBrokerArtworkOrderReport),
            includeEmailBrokerArtworkOrderXML = ko.observable(specifiedincludeEmailBrokerArtworkOrderXML),
            includeEmailBrokerArtworkOrderJobCard = ko.observable(specifiedincludeEmailBrokerArtworkOrderJobCard),
            makeEmailBrokerArtworkOrderProductionReady = ko.observable(specifiedmakeEmailBrokerArtworkOrderProductionReady),
            // isDeliveryTaxAble = ko.observable(specifiedIsDeliveryTaxAble),
            // is Delivery TaxAble
            isDeliveryTaxAble = ko.observable(!specifiedIsDeliveryTaxAble ? 2 : 1),
            // is Delivery TaxAble ui
            isDeliveryTaxAbleUi = ko.computed({
                read: function () {
                    return '' + isDeliveryTaxAble();
                },
                write: function (value) {
                    var deliveryTaxAble = parseInt(value);
                    if (deliveryTaxAble === isDeliveryTaxAble()) {
                        return;
                    }

                    isDeliveryTaxAble(deliveryTaxAble);
                }
            }),
            pickupAddressId = ko.observable(specifiedPickupAddressId),
            //store Image
            storeImageFileBinary = ko.observable(specifiedStoreImageFileBinary),
            storeImageName = ko.observable(),
            //company type
            companyType = ko.observable(),
            //type = ko.observable(),
            isDisplayBanners = ko.observable(specifiedisDisplayBanners),
            raveReviews = ko.observableArray([]),
            companyTerritories = ko.observableArray([]),
            addresses = ko.observableArray([]),
            users = ko.observableArray([]),
            //secondary Pages List
            secondaryPages = ko.observableArray([]),
            // ReSharper disable InconsistentNaming
            companyCMYKColors = ko.observableArray([]),
            //Color Palette
            //colorPalette = ko.observable(new ColorPalette()),
            colorPalette = ko.observable(),
            //Company Banner Set List
            companyBannerSets = ko.observableArray([]),
            //Payment Gateways
            paymentGateway = ko.observableArray([]),
            //Payment Methods
            paymentMethod = ko.observableArray([]),
            // ReSharper restore InconsistentNaming
            //Product Categories
            productCategories = ko.observableArray([]),
            //Company Domains
            companyDomains = ko.observableArray([]),
            //Products
            products = ko.observableArray([]),
            //Media Libraries
            mediaLibraries = ko.observableArray([]),
            //Company Cost Center
            companyCostCenters = ko.observableArray([]),
            //store Backgroud Image Image Source
            storeBackgroudImageImageSource = ko.observable(specifiedStoreBackgroudImageSource),
            //store Backgroud Image Path
            storeBackgroudImagePath = ko.observable(specifiedStoreBackgroundImage),
            //store Backgroud Image File Name
            storeBackgroudImageFileName = ko.observable(),
            defaultSpriteImageSource = ko.observable(specifiedDefaultSpriteImageSource),
            defaultSpriteImageFileName = ko.observable(),
            userDefinedSpriteImageSource = ko.observable(specifiedUserDefinedSpriteImageSource),
            userDefinedSpriteImageFileName = ko.observable(specifiedUserDefinedSpriteFileName),
            //Is Show Google Map
            isShowGoogleMap = ko.observable(specifiedIsShowGoogleMap != undefined ? specifiedIsShowGoogleMap.toString() : "1"),
            customCSS = ko.observable(specifiedCustomCSS),
            // Errors
            errors = ko.validation.group({
                companyId: companyId,
                name: name,
                storeId:storeId,
                //webAccessCode: webAccessCode,
                url: url,
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                // ReSharper restore InconsistentNaming
                storeId: storeId,
                companyId: companyId,
                name: name,
                status: status,
                image: image,
                url: url,
                accountOpenDate: accountOpenDate,
                accountManagerId: accountManagerId,
                avatRegNumber: avatRegNumber,
                avatRegReference: avatRegReference,
                creditLimit:creditLimit,
                phoneNo: phoneNo,
                isCustomer: isCustomer,
                notes: notes,
                webAccessCode: webAccessCode,
                twitterUrl: twitterUrl,
                mediaLibraries: mediaLibraries,
                facebookUrl: facebookUrl,
                linkedinUrl: linkedinUrl,
                facebookAppId: facebookAppId,
                facebookAppKey: facebookAppKey,
                twitterAppId: twitterAppId,
                twitterAppKey: twitterAppKey,
                companyType: companyType,
                type: type,
                raveReviews: raveReviews,
                companyTerritories: companyTerritories,
                addresses: addresses,
                users: users,
                secondaryPages: secondaryPages,
                companyCMYKColors: companyCMYKColors,
                webMasterTag: webMasterTag,
                webAnalyticCode: webAnalyticCode,
                salesAndOrderManagerId1: salesAndOrderManagerId1,
                salesAndOrderManagerId2: salesAndOrderManagerId2,
                productionManagerId1: productionManagerId1,
                productionManagerId2: productionManagerId2,
                stockNotificationManagerId1: stockNotificationManagerId1,
                stockNotificationManagerId2: stockNotificationManagerId2,
                isStoreModePrivate: isStoreModePrivate,
                isTextWatermark: isTextWatermark,
                watermarkText: watermarkText,
                isBrokerPaymentRequired: isBrokerPaymentRequired,
                isBrokerCanAcceptPaymentOnline: isBrokerCanAcceptPaymentOnline,
                canUserPlaceOrderWithoutApproval: canUserPlaceOrderWithoutApproval,
                isIncludeVAT: isIncludeVAT,
                includeEmailBrokerArtworkOrderReport: includeEmailBrokerArtworkOrderReport,
                includeEmailBrokerArtworkOrderXML: includeEmailBrokerArtworkOrderXML,
                includeEmailBrokerArtworkOrderJobCard: includeEmailBrokerArtworkOrderJobCard,
                makeEmailBrokerArtworkOrderProductionReady: makeEmailBrokerArtworkOrderProductionReady,
                storeImageFileBinary: storeImageFileBinary,
                storeImageName: storeImageName,
                isDisplayBanners: isDisplayBanners,
                storeBackgroudImageImageSource: storeBackgroudImageImageSource,
                storeBackgroudImageFileName: storeBackgroudImageFileName,
                isShowGoogleMap: isShowGoogleMap,
                customCSS: customCSS,
                companyDomains: companyDomains,
                isDeliveryTaxAble: isDeliveryTaxAble,
                pickupAddressId: pickupAddressId
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.StoreId = source.storeId();
                result.CompanyId = source.companyId();
                result.Name = source.name();
                result.Status = source.status();
                //result.ImageBytes = source.image();
                result.URL = source.url();
                result.AccountOpenDate = source.accountOpenDate() ? moment(source.accountOpenDate()).format(ist.utcFormat) + 'Z' : undefined;
                result.AccountManagerId = source.accountManagerId();
                result.VATRegNumber = source.avatRegNumber();
                result.VATRegReference = source.avatRegReference();
                result.CreditLimit = source.creditLimit();
                result.PhoneNo = source.phoneNo();
                result.IsCustomer = source.isCustomer();
                //result.IsCustomer = source.type();
                result.Notes = source.notes();
                result.WebMasterTag = source.webMasterTag();
                result.WebAnalyticCode = source.webAnalyticCode();
                result.WebAccessCode = source.webAccessCode();
                result.TwitterUrl = source.twitterUrl();
                result.FacebookUrl = source.facebookUrl();
                result.LinkedinUrl = source.linkedinUrl();
                result.FacebookAppId = source.facebookAppId();
                result.FacebookAppKey = source.facebookAppKey();
                result.TwitterAppId = source.twitterAppId();
                result.TwitterAppKey = source.twitterAppKey();
                result.StoreImagePath = source.storeImagePath();
                result.SalesAndOrderManagerId1 = source.salesAndOrderManagerId1();
                result.SalesAndOrderManagerId2 = source.salesAndOrderManagerId2();
                result.ProductionManagerId1 = source.productionManagerId1();
                result.ProductionManagerId2 = source.productionManagerId2();
                result.StockNotificationManagerId1 = source.stockNotificationManagerId1();
                result.StockNotificationManagerId2 = source.stockNotificationManagerId2();
                result.isStoreModePrivate = source.isStoreModePrivate();
                result.isTextWatermark = source.isTextWatermark();
                result.isShowGoogleMap = source.isShowGoogleMap();
                result.WatermarkText = source.watermarkText();
                result.isBrokerPaymentRequired = source.isBrokerPaymentRequired();
                result.isBrokerCanAcceptPaymentOnline = source.isBrokerCanAcceptPaymentOnline();
                result.canUserPlaceOrderWithoutApproval = source.canUserPlaceOrderWithoutApproval();
                result.isIncludeVAT = source.isIncludeVAT();
                // result.StoreBackgroundImage = source.storeBackgroudImagePath();
                result.includeEmailBrokerArtworkOrderReport = source.includeEmailBrokerArtworkOrderReport();
                result.includeEmailBrokerArtworkOrderXML = source.includeEmailBrokerArtworkOrderXML();
                result.includeEmailBrokerArtworkOrderJobCard = source.includeEmailBrokerArtworkOrderJobCard();
                result.makeEmailBrokerArtworkOrderProductionReady = source.makeEmailBrokerArtworkOrderProductionReady();
                result.isDisplayBanners = source.isDisplayBanners();
                result.IsDeliveryTaxAble = source.isDeliveryTaxAble() === 2 ? false : true;
                result.PickupAddressId = source.pickupAddressId();
                result.CompanyType = source.companyType() != undefined ? CompanyType().convertToServerData(source.companyType()) : null;
                result.CustomCSS = source.customCSS();
                result.RaveReviews = [];
                result.PaymentGateways = [];
                result.CompanyContacts = [];
                _.each(source.raveReviews(), function (item) {
                    result.RaveReviews.push(item.convertToServerData());
                });
                result.CompanyTerritories = [];
                _.each(source.companyTerritories(), function (item) {
                    result.CompanyTerritories.push(item.convertToServerData());
                });

                result.CompanyDomains = [];
                _.each(source.companyDomains(), function (item) {
                    result.CompanyDomains.push(item.convertToServerData());
                });
                //result.CompanyCostCenters = [];
                //_.each(source.companyCostCenters(), function (item) {
                //    result.CompanyCostCenters.push(item.convertToServerData());
                //});
                //_.each(source.users(), function (item) {
                //    result.CompanyContacts.push(item.convertToServerData());
                //});
                //#region Arrays
                result.NewAddedCompanyTerritories = [];
                result.EdittedCompanyTerritories = [];
                result.DeletedCompanyTerritories = [];
                result.NewAddedCompanyContacts = [];
                result.EdittedCompanyContacts = [];
                result.DeletedCompanyContacts = [];
                result.NewAddedAddresses = [];
                result.CompanyBannerSets = [];
                result.EdittedAddresses = [];
                result.DeletedAddresses = [];
                result.NewAddedCmsPages = [];
                result.EditCmsPages = [];
                result.DeletedCmsPages = [];
                result.PageCategories = [];
                result.CmsPageWithWidgetList = [];
                result.EdittedProductCategories = [];
                result.DeletedProductCategories = [];
                result.NewProductCategories = [];
                result.NewAddedProducts = [];
                result.EdittedProducts = [];
                result.Deletedproducts = [];
                result.Campaigns = [];
                result.CompanyCostCentres = [];
                _.each(source.paymentGateway(), function (item) {
                    result.PaymentGateways.push(item.convertToServerData());
                });

                result.ColorPalletes = [];
                result.StoreBackgroundFile = source.storeBackgroudImageImageSource();
                result.StoreBackgroudImageFileName = source.storeBackgroudImageFileName();
                //#endregion
                result.ImageName = source.storeImageName() === undefined ? null : source.storeImageName();
                result.ImageBytes = source.storeImageFileBinary() === undefined ? null : source.storeImageFileBinary();
                result.DefaultSpriteSource = source.defaultSpriteImageSource() === undefined ? null : source.defaultSpriteImageSource();
                result.UserDefinedSpriteSource = source.userDefinedSpriteImageSource() === undefined ? null : source.userDefinedSpriteImageSource();
                result.UserDefinedSpriteFileName = source.userDefinedSpriteImageFileName() === undefined ? null : source.userDefinedSpriteImageFileName();
                result.CmsOffers = [];
                result.MediaLibraries = [];
                return result;
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            //#region SELF
            storeId: storeId,
            companyId: companyId,
            name: name,
            status: status,
            image: image,
            url: url,
            accountOpenDate: accountOpenDate,
            accountManagerId: accountManagerId,
            avatRegNumber: avatRegNumber,
            avatRegReference: avatRegReference,
            creditLimit:creditLimit,
            phoneNo: phoneNo,
            isCustomer: isCustomer,
            notes: notes,
            webMasterTag: webMasterTag,
            webAnalyticCode: webAnalyticCode,
            webAccessCode: webAccessCode,
            twitterUrl: twitterUrl,
            facebookUrl: facebookUrl,
            linkedinUrl: linkedinUrl,
            facebookAppId: facebookAppId,
            facebookAppKey: facebookAppKey,
            twitterAppId: twitterAppId,
            salesAndOrderManagerId1: salesAndOrderManagerId1,
            salesAndOrderManagerId2: salesAndOrderManagerId2,
            productionManagerId1: productionManagerId1,
            productionManagerId2: productionManagerId2,
            stockNotificationManagerId1: stockNotificationManagerId1,
            stockNotificationManagerId2: stockNotificationManagerId2,
            twitterAppKey: twitterAppKey,
            companyType: companyType,
            storeImagePath: storeImagePath,
            isStoreModePrivate: isStoreModePrivate,
            isTextWatermark: isTextWatermark,
            watermarkText: watermarkText,
            isBrokerPaymentRequired: isBrokerPaymentRequired,
            isBrokerCanAcceptPaymentOnline: isBrokerCanAcceptPaymentOnline,
            canUserPlaceOrderWithoutApproval: canUserPlaceOrderWithoutApproval,
            isIncludeVAT: isIncludeVAT,
            includeEmailBrokerArtworkOrderReport: includeEmailBrokerArtworkOrderReport,
            includeEmailBrokerArtworkOrderXML: includeEmailBrokerArtworkOrderXML,
            includeEmailBrokerArtworkOrderJobCard: includeEmailBrokerArtworkOrderJobCard,
            makeEmailBrokerArtworkOrderProductionReady: makeEmailBrokerArtworkOrderProductionReady,
            isDisplayBanners: isDisplayBanners,
            storeImageFileBinary: storeImageFileBinary,
            storeImageName: storeImageName,
            type: type,
            raveReviews: raveReviews,
            companyTerritories: companyTerritories,
            addresses: addresses,
            users: users,
            companyCMYKColors: companyCMYKColors,
            colorPalette: colorPalette,
            companyBannerSets: companyBannerSets,
            secondaryPages: secondaryPages,
            paymentGateway: paymentGateway,
            paymentMethod: paymentMethod,
            productCategories: productCategories,
            products: products,
            isDeliveryTaxAble: isDeliveryTaxAble,
            storeBackgroudImageImageSource: storeBackgroudImageImageSource,
            storeBackgroudImageFileName: storeBackgroudImageFileName,
            storeBackgroudImagePath: storeBackgroudImagePath,
            isShowGoogleMap: isShowGoogleMap,
            defaultSpriteImageSource: defaultSpriteImageSource,
            defaultSpriteImageFileName: defaultSpriteImageFileName,
            userDefinedSpriteImageSource: userDefinedSpriteImageSource,
            userDefinedSpriteImageFileName: userDefinedSpriteImageFileName,
            isDeliveryTaxAbleUi: isDeliveryTaxAbleUi,
            pickupAddressId: pickupAddressId,
            customCSS: customCSS,
            companyDomains: companyDomains,
            mediaLibraries: mediaLibraries,
            companyCostCenters: companyCostCenters,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
            //#endregion
        };
        return self;
    };
    Store.CreateFromClientModel = function (source) {
        var result = new Store(
            source.storeId,
            source.companyId,
            source.name,
            source.status,
            source.image,
            source.url,
            source.accountOpenDate,
            source.accountManagerId,
            source.avatRegNumber,
            source.avatRegReference,
            source.creditLimit,
            source.phoneNo,
            source.isCustomer,
            source.notes,
            source.webMasterTag,
            source.webAnalyticCode,
            source.webAccessCode,
            source.twitterUrl,
            source.facebookUrl,
            source.linkedinUrl,
            source.facebookAppId,
            source.facebookAppKey,
            source.twitterAppId,
            source.twitterAppKey,
            source.salesAndOrderManagerId1,
            source.salesAndOrderManagerId2,
            source.productionManagerId1,
            source.productionManagerId2,
            source.stockNotificationManagerId1,
            source.stockNotificationManagerId2,
            source.isStoreModePrivate,
            source.isTextWatermark,
            source.watermarkText,
            source.isBrokerPaymentRequired,
            source.isBrokerCanAcceptPaymentOnline,
            source.canUserPlaceOrderWithoutApproval,
            source.isIncludeVAT,
            source.includeEmailBrokerArtworkOrderReport,
            source.includeEmailBrokerArtworkOrderXML,
            source.includeEmailBrokerArtworkOrderJobCard,
            source.isDeliveryTaxAble,
            source.pickupAddressId,
            source.makeEmailBrokerArtworkOrderProductionReady
        );
        _.each(source.addresses, function (item) {
            result.addresses.push(Address.CreateFromClientModel(item));
        });
        _.each(source.users, function (item) {
            result.users.push(CompanyContact.CreateFromClientModel(item));
        });
        return result;
    };
    Store.Create = function (source) {
        var store = new Store(
            source.StoreId,
            source.CompanyId,
            source.Name,
            source.Status,
            source.Image,
            source.URL,
            source.AccountOpenDate,
            source.AccountManagerId,
            source.VATRegNumber,
            source.VATRegReference,
            source.PhoneNo,
            source.IsCustomer,
            source.Notes,
            source.WebMasterTag,
            source.WebAnalyticCode,
            source.WebAccessCode,
            source.TwitterUrl,
            source.FacebookUrl,
            source.LinkedinUrl,
            source.FacebookAppId,
            source.FacebookAppKey,
            source.TwitterAppId,
            source.TwitterAppKey,
            source.SalesAndOrderManagerId1,
            source.SalesAndOrderManagerId2,
            source.ProductionManagerId1,
            source.ProductionManagerId2,
            source.StockNotificationManagerId1,
            source.StockNotificationManagerId2,
            source.isDisplayBanners,
            source.isStoreModePrivate,
            source.isTextWatermark,
            source.WatermarkText,
            source.isBrokerPaymentRequired,
            source.isBrokerCanAcceptPaymentOnline,
            source.canUserPlaceOrderWithoutApproval,
            source.isIncludeVAT,
            source.includeEmailBrokerArtworkOrderReport,
            source.includeEmailBrokerArtworkOrderXML,
            source.includeEmailBrokerArtworkOrderJobCard,
            source.IsDeliveryTaxAble,
            source.PickupAddressId,
            source.makeEmailBrokerArtworkOrderProductionReady,
            source.ImageSource,
            source.StoreBackgroudImageSource,
            source.isShowGoogleMap,
            source.DefaultSpriteImageSource,
            source.UserDefinedSpriteImageSource,
            source.UserDefinedSpriteFileName,
            source.CustomCSS,
            source.StoreBackgroundImage,
            source.StoreImagePath
        );

        if (source.IsCustomer == 0) {
            store.type("0");
        }

        else if (source.IsCustomer == 1) {
            store.type("1");
        }
        _.each(source.Addresses, function (item) {
            store.addresses.push(Address.Create(item));
        });
        _.each(source.CompanyContacts, function (item) {
            store.users.push(CompanyContact.Create(item));
        });

        return store;
    };
    // #endregion _____________________ S T O R E ______________________________

    // #region ______________ Media Library _______________________________

    // ReSharper disable once InconsistentNaming
    var MediaLibrary = function (specifiedMediaId, specifiedFilePath, specifiedFileName, specifiedFileType, specifiedCompanyId, specifiedImageSource) {
        var self,
            id = ko.observable(specifiedMediaId),
            fakeId = ko.observable(),
            filePath = ko.observable(specifiedFilePath),
            fileName = ko.observable(specifiedFileName),
            fileType = ko.observable(specifiedFileType),
            companyId = ko.observable(specifiedCompanyId),
            fileSource = ko.observable(specifiedImageSource),
            isSelected = ko.observable(false),

        //Convert To Server
        convertToServerData = function () {
            return {
                MediaId: id() === undefined ? 0 : id(),
                FilePath: filePath(),
                FileName: fileName(),
                FileType: fileType(),
                CompanyId: companyId(),
                FileSource: fileSource(),
                FakeId: fakeId(),
            };
        };
        self = {
            id: id,
            fakeId: fakeId,
            filePath: filePath,
            fileName: fileName,
            fileType: fileType,
            companyId: companyId,
            fileSource: fileSource,
            isSelected: isSelected,
            convertToServerData: convertToServerData,
        };
        return self;
    };
    MediaLibrary.Create = function (source) {
        return new MediaLibrary(
            source.MediaId,
            source.FilePath,
            source.FileName,
            source.FileType,
            source.CompanyId,
        source.ImageSource);
    };

    // #endregion ______________ MediaLibrary _________________

    // #region ______________  C O M P A N Y    T E R R I T O R Y    ______

    // ReSharper disable once InconsistentNaming
    var CompanyTerritory = function (specifiedTerritoryId, specifiedTerritoryName, specifiedCompanyId, specifiedTerritoryCode, specifiedisDefault) {

        var self,
            territoryId = ko.observable(specifiedTerritoryId),
            territoryName = ko.observable(specifiedTerritoryName).extend({ required: true }),
            companyId = ko.observable(specifiedCompanyId),
            territoryCode = ko.observable(specifiedTerritoryCode).extend({ required: true }),
            isDefault = ko.observable(specifiedisDefault),
            // Errors
            errors = ko.validation.group({
                territoryName: territoryName,
                territoryCode: territoryCode
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                territoryId: territoryId,
                territoryName: territoryName,
                companyId: companyId,
                territoryCode: territoryCode,
                isDefault: isDefault
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    TerritoryId: territoryId(),
                    TerritoryName: territoryName(),
                    CompanyId: companyId(),
                    TerritoryCode: territoryCode(),
                    isDefault: isDefault()
                }
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            territoryId: territoryId,
            territoryName: territoryName,
            companyId: companyId,
            territoryCode: territoryCode,
            isDefault: isDefault,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    CompanyTerritory.CreateFromClientModel = function (source) {
        return new CompanyTerritory(
            source.territoryId,
            source.territoryName,
            source.companyId,
            source.territoryCode,
            source.isDefault
        );
    };
    CompanyTerritory.Create = function (source) {
        var companyTerritory = new CompanyTerritory(
            source.TerritoryId,
            source.TerritoryName,
            source.CompanyId,
            source.TerritoryCode,
            source.isDefault
        );
        return companyTerritory;
    };

    // #endregion ______________  C O M P A N Y    T E R R I T O R Y    _________________

    // #region __________________  R O L E   ______________________

    // ReSharper disable once InconsistentNaming
    var Role = function (specifiedRoleId, specifiedRoleName) {

        var self,
            roleId = ko.observable(specifiedRoleId),
            roleName = ko.observable(specifiedRoleName),            // Errors
            errors = ko.validation.group({

            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                roleId: roleId,
                roleName: roleName,

            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    RoleId: roleId(),
                    RoleName: roleName()
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            roleId: roleId,
            roleName: roleName,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    Role.CreateFromClientModel = function (source) {
        return new Role(
            source.roleId,
            source.rolesName
            );
    };
    Role.Create = function (source) {
        var role = new Role(
            source.ContactRoleId,
            source.ContactRoleName
            );
        return role;
    };
    // #endregion __________________  R O L E   ______________________

    // #region __________________  R E G I S  T R A T I O N   Q U E S T I O N  ______________________

    // ReSharper disable once InconsistentNaming
    var RegistrationQuestion = function (specifiedQuestionId, specifiedQuestion) {

        var self,
            questionId = ko.observable(specifiedQuestionId),
            question = ko.observable(specifiedQuestion),            // Errors
            errors = ko.validation.group({

            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                questionId: questionId,
                question: question,

            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    QuestionId: questionId(),
                    Question: question()
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            questionId: questionId,
            question: question,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    RegistrationQuestion.CreateFromClientModel = function (source) {
        return new RegistrationQuestion(
            source.questionId,
            source.question
            );
    };
    RegistrationQuestion.Create = function (source) {
        var registrationQuestion = new RegistrationQuestion(
            source.QuestionId,
            source.Question
            );
        return registrationQuestion;
    };
    // #endregion __________________  R E G I S  T R A T I O N   Q U E S T I O N  ______________________

    // #region __________________  O R D E R ______________________

    // ReSharper disable once UnusedLocals
    var Estimate = function (specifiedId, specifiedCode, specifiedName, specifiedCompanyName, specifiedCreationDate,
        specifiedFlagColor, specifiedOrderCode, specifiedStstud, specifiedestimateTotal, specifiedisDirectOrder,specifiedItemsCount) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId || 0),
             // Order Code
            status = ko.observable(specifiedStstud || undefined),
            estimateTotal = ko.observable(specifiedestimateTotal || 0),
            isDirectOrder = ko.observable(specifiedisDirectOrder || undefined),
            // Name
            name = ko.observable(specifiedName || undefined).extend({ required: true }),
            // Code
            code = ko.observable(specifiedCode || undefined),
            // Company Name
            companyName = ko.observable(specifiedCompanyName || undefined),
            creationDate = ko.observable(specifiedCreationDate || undefined),
            flagColor = ko.observable(specifiedFlagColor || undefined),
            orderCode = ko.observable(specifiedOrderCode || undefined),
            itemscount = ko.observable(specifiedItemsCount),
            isDirectSaleUi = ko.computed(function () {
                return isDirectOrder() ? "Direct Order" : "Online Order";
            }),
                        // Errors
            errors = ko.validation.group({
                name: name,
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // Show All Error Messages
            showAllErrors = function () {
                // Show Item Errors
                errors.showAllMessages();
            },
            // Set Validation Summary
            setValidationSummary = function (validationSummaryList) {
                validationSummaryList.removeAll();
            },
            // True if the order has been changed
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

        return {
            id: id,
            status: status,
            estimateTotal: estimateTotal,
            isDirectOrder: isDirectOrder,
            companyName: companyName,
            name: name,
            code: code,
            creationDate: creationDate,
            flagColor: flagColor,
            orderCode: orderCode,
            isDirectSaleUi: isDirectSaleUi,
            errors: errors,
            isValid: isValid,
            showAllErrors: showAllErrors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            itemscount:  itemscount,
            setValidationSummary: setValidationSummary,
        };
    };
    // Estimate Factory
    Estimate.Create = function (source) {
        var estimate = new Estimate(source.EstimateId, source.OrderCode, source.EstimateName, source.CompanyName,
            source.CreationDate, source.SectionFlagId, source.OrderCode, source.Status, source.EstimateTotal, source.IsDirectOrder,source.ItemsCount);
        return estimate;
    };

    // #endregion __________________  O R D E R ______________________

    // #region __________________  I N V O I C E ______________________

    // ReSharper disable once InconsistentNaming
    var Invoice = function (specifiedId, specifiedCmpName, specifiedinvoiceCode, specifiedinvoiceDate, specifiedinvoiceName,
        specifiedisArchive, sepecifiedTotal, specifiedCOntact, specifiedStatus, specifiedFlagId) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId || 0),
            // Name
            companyName = ko.observable(specifiedCmpName),
            // Code
            invoiceCode = ko.observable(specifiedinvoiceCode || undefined),
            invoiceDate = ko.observable(specifiedinvoiceDate || undefined),
            invoiceName = ko.observable(specifiedinvoiceName || undefined),
            isArchive = ko.observable(specifiedisArchive || undefined),
            invoiceTotal = ko.observable(sepecifiedTotal || 0),
            contact = ko.observable(specifiedCOntact || undefined),
            status = ko.observable(specifiedStatus || undefined),
            flagColor = ko.observable('#C0C0C0'),
            flagId = ko.observable(specifiedFlagId),
                  // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // Show All Error Messages
            showAllErrors = function () {
                // Show Item Errors
                errors.showAllMessages();
            },
            // Set Validation Summary
            setValidationSummary = function (validationSummaryList) {
                validationSummaryList.removeAll();
            },
            // True if the order has been changed
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

        return {
            id: id,
            companyName: companyName,
            invoiceCode: invoiceCode,
            invoiceDate: invoiceDate,
            invoiceName: invoiceName,
            isArchive: isArchive,
            invoiceTotal: invoiceTotal,
            contact: contact,
            flagId: flagId,
            status: status,
            flagColor: flagColor,
            errors: errors,
            isValid: isValid,
            showAllErrors: showAllErrors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            itemscount:  itemscount,
            setValidationSummary: setValidationSummary,
        };
    };
    // Estimate Factory
    Invoice.Create = function (source) {
        var invoice = new Invoice(source.InvoiceId, source.CompanyName, source.InvoiceCode, source.InvoiceDate,
            source.InvoiceName, source.IsArchive, source.InvoiceTotal + "$", source.ContactName, source.Status, source.FlagId);
        return invoice;
    };

    // #endregion __________________  O R D E R ______________________

    // #region ______________  PURCHASE LIST VIEW MODEL   _____________
    // ReSharper disable once InconsistentNaming
    var PurchaseListViewModel =
    function (specifiedPurchaseId, specifiedCode, specifiedDatePurchase, specifiedSupplierName, specifiedTotalPrice) {
        var self,
           purchaseId = ko.observable(specifiedPurchaseId),
           code = ko.observable(specifiedCode),
           datePurchase = ko.observable(specifiedDatePurchase),
           supplierName = ko.observable(specifiedSupplierName),
           totalPrice = ko.observable(specifiedTotalPrice),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                purchaseId: purchaseId,
                code: code,
                datePurchase: datePurchase,
                supplierName: supplierName,
                totalPrice: totalPrice
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),

            //Convert To Server
            convertToServerData = function () {
                var result = {};
                return result;
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            purchaseId: purchaseId,
            code: code,
            datePurchase: datePurchase,
            supplierName: supplierName,
            totalPrice: totalPrice,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    PurchaseListViewModel.CreateFromClientModel = function (source) {
        return new PurchaseListViewModel(
            source.purchaseId,
            source.code,
            source.supplierName,
            source.totalPrice
        );
    };
    PurchaseListViewModel.Create = function (source) {
        var purchaseListViewModel = new PurchaseListViewModel(
           source.PurchaseId,
           source.Code,
           source.DatePurchase != null ? moment(source.DatePurchase).format('YYYY/MM/DD') : '',
           source.SupplierName,
           source.TotalPrice
       );


        return purchaseListViewModel;
    };
    // #endregion

    // #region ______________  GOODS RECIEVED NOTE LIST VIEW MODEL   _____________
    // ReSharper disable once InconsistentNaming
    var GoodsReceivedNoteListViewModel =
    function (specifiedGoodsReceivedId, specifiedCode, specifiedDateReceived, specifiedSupplierName, specifiedTotalPrice) {
        var self,
           goodsReceivedId = ko.observable(specifiedGoodsReceivedId),
           code = ko.observable(specifiedCode),
           dateReceived = ko.observable(specifiedDateReceived),
           supplierName = ko.observable(specifiedSupplierName),
           totalPrice = ko.observable(specifiedTotalPrice),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                goodsReceivedId: goodsReceivedId,
                code: code,
                dateReceived: dateReceived,
                supplierName: supplierName,
                totalPrice: totalPrice
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),

            //Convert To Server
            convertToServerData = function () {
                var result = {};
                return result;
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            goodsReceivedId: goodsReceivedId,
            code: code,
            dateReceived: dateReceived,
            supplierName: supplierName,
            totalPrice: totalPrice,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    GoodsReceivedNoteListViewModel.CreateFromClientModel = function (source) {
        return new GoodsReceivedNoteListViewModel(
            source.goodsReceivedId,
            source.code,
            source.dateReceived,
            source.supplierName,
            source.totalPrice
            
        );
    };
    GoodsReceivedNoteListViewModel.Create = function (source) {
        var goodsReceivedNoteListViewModel = new GoodsReceivedNoteListViewModel(
           source.GoodsReceivedId,
           source.Code,
           source.DateReceived != null ? moment(source.DateReceived).format('YYYY/MM/DD') : '',
           source.SupplierName,
           source.TotalPrice
       );


        return goodsReceivedNoteListViewModel;
    };
    // #endregion

    return {
        customerViewListModel: customerViewListModel,
        CrmSupplierListViewModel: CrmSupplierListViewModel,
        Address: Address,
        CompanyContact: CompanyContact,
        SystemUser: SystemUser,
        Store: Store,
        MediaLibrary: MediaLibrary,
        CompanyTerritory: CompanyTerritory,
        Role: Role,
        RegistrationQuestion: RegistrationQuestion,
        Estimate: Estimate,
        Invoice: Invoice,
        PurchaseListViewModel: PurchaseListViewModel,
        GoodsReceivedNoteListViewModel: GoodsReceivedNoteListViewModel
    };
});
