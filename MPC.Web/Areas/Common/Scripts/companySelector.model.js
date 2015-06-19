define(["moment"], function () {
    var

    // Company Entity
// ReSharper disable InconsistentNaming
    Company = function (specifiedId, specifiedName, specifiedURL, specifiedCreationDate, specifiedStoreId, specifiedIsCustomer, specifiedTaxRate) {
        // ReSharper restore InconsistentNaming
        
        return {
            id: specifiedId,
            name: specifiedName,
            url: specifiedURL,
            creationDate: specifiedCreationDate ? moment(specifiedCreationDate).toDate() : undefined,
            storeId: specifiedStoreId,
            isCustomer: specifiedIsCustomer,
            taxRate: specifiedTaxRate
        };
    },
    
    // Company Contact Entity
// ReSharper disable InconsistentNaming
    CompanyContact = function (specifiedId, specifiedName, specifiedCompanyId, specifiedCompanyName, specifiedStoreId, specifiedStoreName, specifiedIsCustomer,
    specifiedAddressId) {
        // ReSharper restore InconsistentNaming

        return {
            id: specifiedId,
            name: specifiedName,
            companyId: specifiedCompanyId,
            companyName: specifiedCompanyName,
            storeId: specifiedStoreId,
            storeName: specifiedStoreName,
            isCustomer: specifiedIsCustomer,
            addressId: specifiedAddressId
        };
    };

    // Company Factory
    Company.Create = function (source) {
        return new Company(source.CompanyId, source.Name, source.URL, source.CreationDate, source.StoreId, source.IsCustomer, source.TaxRate);
    };

    // Company Contact Factory
    CompanyContact.Create = function (source) {
        return new CompanyContact(source.ContactId, source.Name, source.CompanyId, source.CompanyName, source.StoreId, source.StoreName, source.IsCustomer,
        source.AddressId);
    };

    return {
        // Company Constructor
        Company: Company,
        // Company Contact Constructor
        CompanyContact: CompanyContact
    };
});