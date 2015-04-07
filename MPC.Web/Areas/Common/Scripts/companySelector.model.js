define(["moment"], function () {
    var

    // Company Entity
// ReSharper disable InconsistentNaming
    Company = function (specifiedId, specifiedName, specifiedURL, specifiedCreationDate, specifiedStoreId) {
        // ReSharper restore InconsistentNaming
        
        return {
            id: specifiedId,
            name: specifiedName,
            url: specifiedURL,
            creationDate: specifiedCreationDate ? moment(specifiedCreationDate).format(ist.datePattern) : undefined,
            storeId: specifiedStoreId
        };
    };

    // Company Factory
    Company.Create = function (source) {
        return new Company(source.CompanyId, source.Name, source.URL, source.CreationDate, source.StoreId);
    };


    return {
        // Company Constructor
        Company: Company
    };
});