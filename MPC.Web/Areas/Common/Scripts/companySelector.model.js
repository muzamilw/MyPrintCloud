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
    };

    // Company Factory
    Company.Create = function (source) {
        return new Company(source.CompanyId, source.Name, source.URL, source.CreationDate, source.StoreId, source.IsCustomer, source.TaxRate);
    };


    return {
        // Company Constructor
        Company: Company
    };
});