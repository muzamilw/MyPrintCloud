define("inventorySubCategory/inventorySubCategory.model", ["ko", "underscore", "underscore-ko"], function (ko) {
    var
// ReSharper disable once InconsistentNaming
        InventorySubCategory = function (specifiedSubCategoryId, specifiedCode, specifiedName, specifiedDescription, specifiedFixed, specifiedCategoryId) {
            var
                self,
                subCategoryId = ko.observable(specifiedSubCategoryId),
                code = ko.observable(specifiedCode).extend({ required: true }),
                name = ko.observable(specifiedName).extend({ required: true }),
                description = ko.observable(specifiedDescription),
                fixed = ko.observable(specifiedFixed),
                categoryId = ko.observable(specifiedCategoryId),
             // Errors
             errors = ko.validation.group({
                 code: code,
                 name: name
             }),
                // Is Valid 
             isValid = ko.computed(function () {
                 return errors().length === 0 ? true : false;
             }),

             // True if the booking has been changed
             // ReSharper disable InconsistentNaming
             dirtyFlag = new ko.dirtyFlag({
                 subCategoryId: subCategoryId,
                 code: code,
                 name: name,
                 description: description,
                 fixed: fixed,
                 categoryId: categoryId
             }),
             // Has Changes
             hasChanges = ko.computed(function () {
                 return dirtyFlag.isDirty();
             }),
             convertToServerData = function () {
                 return {
                     SubCategoryId: subCategoryId(),
                     Code: code(),
                     Name: name(),
                     Description: description(),
                     Fixed: fixed(),
                     CategoryId: categoryId()
                 }
             },
            // Reset
             reset = function () {
                 dirtyFlag.reset();
             };
            self = {

                subCategoryId: subCategoryId,
                code: code,
                name: name,
                description: description,
                fixed: fixed,
                categoryId: categoryId,

                isValid: isValid,
                errors: errors,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                convertToServerData: convertToServerData,
                reset: reset
            };
            return self;
        };
    //function to attain cancel button functionality 
    InventorySubCategory.CreateFromClientModel = function (source) {
        return new InventorySubCategory(source.subCategoryId, source.code, source.name, source.description, source.fixed, source.categoryId);
    };
    // server to client mapper
    var inventorySubCategoryServertoClientMapper = function (source) {
        return InventorySubCategory.Create(source);
    };

    // Area Factory
    InventorySubCategory.Create = function (source) {
        return new InventorySubCategory(source.SubCategoryId, source.Code, source.Name, source.Description, source.Fixed, source.CategoryId);
    };
    return {
        InventorySubCategory: InventorySubCategory,
        inventorySubCategoryServertoClientMapper: inventorySubCategoryServertoClientMapper
    };
});