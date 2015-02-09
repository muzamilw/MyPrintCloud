

define("crm/crm.supplier.model", ["ko", "underscore", "underscore-ko"], function (ko) {
    var

    // #region ______________  CRM SUPPLIER LIST VIEW MODEL   _________________
    // ReSharper disable once InconsistentNaming
     CrmSupplierListViewModel =
     function (specifiedCompanyId, specifiedName, specifiedStatus, specifiedImage, specifiedUrl, specifiedIsCustomer, specifiedStoreImageFileBinary, specifiedEmail, specifiedCreatedDate) {
         var self,
            companyId = ko.observable(specifiedCompanyId).extend({ required: true }),
            name = ko.observable(specifiedName),
            status = ko.observable(specifiedStatus),
            image = ko.observable(specifiedImage),
            url = ko.observable(specifiedUrl),
            isCustomer = ko.observable(specifiedIsCustomer),
            storeImageFileBinary = ko.observable(specifiedStoreImageFileBinary),
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
           source.ImageSource,
           source.Email,
           source.CreatedDate != null ?moment(source.CreatedDate).format('YYYY/MM/DD'): ''
       );

        //if (source.IsCustomer == 0) {
        //    store.type("Supplier");
        //}
        if (source.IsCustomer == 1) {
            crmSupplierListViewModel.type("Retail Customer");
        }
            //else if (source.IsCustomer == 2) {
            //    store.type("Prospect");
            //}
        else if (source.IsCustomer == 3) {
            crmSupplierListViewModel.type("Corporate");
        }

        return crmSupplierListViewModel;
    };
    // #endregion

    //#region ______________ R E T U R N ______________
    return {
        CrmSupplierListViewModel: CrmSupplierListViewModel
    };
    // #endregion 
});
