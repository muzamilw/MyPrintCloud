
// Contact View 
define(["ko", "underscore", "underscore-ko"], function (ko) {

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
        specifiedCanUserEditProfile, specifiedcanPlaceDirectOrder, specifiedOrganisationId, specifiedBussinessAddressId, specifiedCompanyName,specifiedStoreName) {
        var self,
            contactId = ko.observable(specifiedContactId),
            addressId = ko.observable(specifiedAddressId),
            companyId = ko.observable(specifiedCompanyId),
            companyName = ko.observable(specifiedCompanyName),
            firstName = ko.observable(specifiedFirstName).extend({ required: true }),
            middleName = ko.observable(specifiedMiddleName),
            lastName = ko.observable(specifiedLastName),
            title = ko.observable(specifiedTitle),
            homeTel1 = ko.observable(specifiedHomeTel1),
            homeTel2 = ko.observable(specifiedHomeTel2),
            homeExtension1 = ko.observable(specifiedHomeExtension1),
            homeExtension2 = ko.observable(specifiedHomeExtension2),
            mobile = ko.observable(specifiedMobile),
            email = ko.observable(specifiedEmail).extend({ required: true }),
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
            bussinessAddressId = ko.observable(specifiedBussinessAddressId).extend({ required: true }),
            fileName = ko.observable(),
            bussinessAddress = ko.observable(),
            shippingAddress = ko.observable(),
            StoreName = ko.observable(specifiedStoreName),

            // Errors
            errors = ko.validation.group({
                firstName: firstName,
                email: email,
                bussinessAddressId: bussinessAddressId
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
                HomeCountry: homeCountry,
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
                fileName: fileName,
                StoreName: StoreName,
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
                    canPlaceDirectOrder: canPlaceDirectOrder(),
                    OrganisationId: organisationId(),
                    BussinessAddressId: bussinessAddressId(),
                    FileName: fileName(),
                    StoreName: StoreName(),
                    //BussinessAddress: bussinessAddress() != undefined ? bussinessAddress().convertToServerData(): null,
                    //ShippingAddress: shippingAddress() != undefined ? shippingAddress().convertToServerData() : null,
                };
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
            HomeCountry: homeCountry,
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
            fileName: fileName,
           
            bussinessAddress: bussinessAddress,
            shippingAddress: shippingAddress,
            StoreName: StoreName,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset,
            companyName: companyName
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
            source.BussinessAddressId,
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
            source.BussinessAddressId,
            source.FileName,
            source.CompanyName,
            source.StoreName
        );
        return companyContact;
    };
    // #endregion ________________COMPANY CONTACT ___________________

    // #region ______________  Q U E S T I O N    _________________

    // ReSharper disable once InconsistentNaming
    var Question = function (specifiedRoleId, specifiedQuestion) {
        var
            self,
            questionId = ko.observable(specifiedRoleId),
            question = ko.observable(specifiedQuestion).extend({ required: true }),
          
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
            convertToServerData = function () {
                return {
                    QuestionId: questionId(),
                    Question: question(),
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
    Question.CreateFromClientModel = function (source) {
        return new Question(
            source.questionId,
            source.question
        );
    };
    Question.Create = function (source) {
        var question = new Question(
            source.QuestionId,
            source.Question
        );
        return question;
    };
    // #endregion ______________  A D D R E S S   _________________

    // #region ______________  R O L E   _________________

    // ReSharper disable once InconsistentNaming
    var Role = function (specifiedRoleId, specifiedRoleName) {
        var
            self,
            roleId = ko.observable(specifiedRoleId),
            roleName = ko.observable(specifiedRoleName).extend({ required: true }),
          
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
            convertToServerData = function () {
                return {
                    ContactRoleId: roleId(),
                    ContactRoleName: roleName(),
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
            source.roleId(),
            source.roleName()
        );
    };
    Role.Create = function (source) {
        var role = new Role(
            source.ContactRoleId,
            source.ContactRoleName
        );
        return role;
    };
    // #endregion ______________  A D D R E S S   _________________

    // #region ______________  A D D R E S S   _________________

    // ReSharper disable once InconsistentNaming
    var Address = function (specifiedAddressId, specifiedCompanyId, specifiedAddressName, specifiedAddress1, specifiedAddress2, specifiedAddress3, specifiedCity, specifiedState, specifiedCountry, specifiedStateName, specifiedCountryName, specifiedPostCode, specifiedFax,
        specifiedEmail, specifiedURL, specifiedTel1, specifiedTel2, specifiedExtension1, specifiedExtension2, specifiedReference, specifiedFAO, specifiedIsDefaultAddress, specifiedIsDefaultShippingAddress,
        specifiedisArchived, specifiedTerritoryId, specifiedTerritoryName, specifiedGeoLatitude, specifiedGeoLongitude, specifiedisPrivate,
        specifiedisDefaultTerrorityBilling, specifiedisDefaultTerrorityShipping, specifiedOrganisationId, specifiedStateId) {
        var
            self,
            addressId = ko.observable(specifiedAddressId),
            companyId = ko.observable(specifiedCompanyId),
            addressName = ko.observable(specifiedAddressName).extend({ required: true }),
            address1 = ko.observable(specifiedAddress1),
            address2 = ko.observable(specifiedAddress2),
            address3 = ko.observable(specifiedAddress3),
            city = ko.observable(specifiedCity),
            stateId = ko.observable(specifiedStateId),
            state = ko.observable(specifiedState),
            country = ko.observable(specifiedCountry),
            stateName = ko.observable(specifiedStateName),
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
            // Errors
            errors = ko.validation.group({
                addressName: addressName,
                territoryId: territoryId
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
                organisationId: organisationId
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
                    State: state(),
                    StateId:stateId(),
                    Country: country(),
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
                    OrganisationId: organisationId(),
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
            stateId:stateId,
            country: country,
            stateName: stateName,
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
            source.organisationId,
            source.stateId
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
            source.State,
            source.Country,
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
            source.StateId

        );
        return address;
    };
    // #endregion ______________  A D D R E S S   _________________

    // #region ______________  C O M P A N Y    T E R R I T O R Y    _________________

    // ReSharper disable once InconsistentNaming
    var CompanyTerritory = function (specifiedTerritoryId, specifiedTerritoryName) {

        var self,
            territoryId = ko.observable(specifiedTerritoryId),
            territoryName = ko.observable(specifiedTerritoryName).extend({ required: true }),
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
            convertToServerData = function () {
                return {
                    TerritoryId: territoryId(),
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            territoryId: territoryId,
            territoryName: territoryName,
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
            source.territoryName
        );
    };
    CompanyTerritory.Create = function (source) {
        var companyTerritory = new CompanyTerritory(
            source.TerritoryId,
            source.TerritoryName
        );
        return companyTerritory;
    };

    // #endregion ______________  C O M P A N Y    T E R R I T O R Y    _________________

    return {
        CompanyContact: CompanyContact,
        Address:Address,
        CompanyTerritory:CompanyTerritory,
        Role:Role,
        Question:Question
    };
});
