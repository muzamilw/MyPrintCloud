define(["moment"], function () {
    var

    // Company Entity
// ReSharper disable InconsistentNaming
    Company = function (specifiedId, specifiedName, specifiedURL, specifiedCreationDate) {
        // ReSharper restore InconsistentNaming
        
        return {
            id: specifiedId,
            name: specifiedName,
            url: specifiedURL,
            creationDate: specifiedCreationDate ? moment(specifiedCreationDate).format(ist.datePattern) : undefined
        };
    };

    // Company Factory
    Company.Create = function (source) {
        return new Company(source.CompanyId, source.Name, source.URL, source.CreationDate);
    };


    return {
        // Company Constructor
        Company: Company
    };
});