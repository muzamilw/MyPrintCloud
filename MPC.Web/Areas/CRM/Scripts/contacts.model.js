
// Contact View 
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var companyContactViewListModel = function (specifiedContactId, specifiedCompanyId, contactname, contactemail,
        isWebAccess, canPlaceDirectOrder, specifiedIsApprovalRequired, canSeePrices, specifiedCreditLimit, contactrole,
        phonenumber, specifiedCompanyName) {
        var
            self,
            contactId = ko.observable(specifiedContactId),
            cmpanyId = ko.observable(specifiedCompanyId),
            companyName = ko.observable(specifiedCompanyName),
            name = ko.observable(contactname),
            email = ko.observable(contactemail),
            hasWebaccess = ko.observable(isWebAccess),
            canPlaceOrder = ko.observable(canPlaceDirectOrder),
            isApprovalRequired = ko.observable(specifiedIsApprovalRequired),
            canSeePrice = ko.observable(canSeePrices),
            creditLimit = ko.observable(specifiedCreditLimit),
            role = ko.observable(contactrole),
            phone = ko.observable(phonenumber),
            // Errors

            errors = ko.validation.group({
            }),
            // Is Valid 
            isValid = ko.computed(function() {
                return errors().length === 0 ? true : false;
            }),
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
            }),
            // Has Changes
            hasChanges = ko.computed(function() {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function(source) {
            },
            // Reset
            reset = function() {
                dirtyFlag.reset();
            };
        self = {
            contactId:contactId,
            cmpanyId: cmpanyId,
            name: name,
            email: email,
            hasWebaccess: hasWebaccess,
            canPlaceOrder: canPlaceOrder,
            isApprovalRequired: isApprovalRequired,
            canSeePrice: canSeePrice,
            creditLimit: creditLimit,
            role: role,
            companyName:companyName,
            phone:phone,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    companyContactViewListModel.Create = function (source) {

        var customer = new companyContactViewListModel(source.ContactId, source.CompanyId, source.FirstName ,
            source.Email, source.isWebAccess, source.isPlaceOrder, source.IsApprover, source.IsPricingshown,
            source.CreditLimit, source.quickPhone, source.CompanyName);
        return customer;
    };
    return {
        companyContactViewListModel: companyContactViewListModel
    };
});
