define(["ko", "underscore", "underscore-ko"], function (ko) {
    var
        // ReSharper disable once InconsistentNaming
        InventoryCategory = function (specifiedCategoryId, specifiedCode, specifiedName, specifiedDescription, specifiedFixed, specifiedItemWeight,
                                      specifiedItemColour, specifiedItemSizeCustom, specifiedItemPaperSize, specifiedItemCoatedType, specifiedItemCoated,
                                      specifiedItemExposure, specifiedItemCharge, specifiedRecLock, specifiedTaxId, specifiedFlag1, specifiedFlag2,
                                      specifiedFlag3, specifiedFlag4, specifiedCompanyId) {
            var
                self,
                categoryId = ko.observable(specifiedCategoryId),
                code = ko.observable(specifiedCode).extend({
                    required: true,
                    //minLength: { params: 0, message: "My Number is too short."},
                    maxLength: { params: 5, message: "Code must be between 0-5 characters " }
                }),
                name = ko.observable(specifiedName).extend({ required: true }),
                description = ko.observable(specifiedDescription),
                fixed = ko.observable(specifiedFixed),
                itemWeight = ko.observable(specifiedItemWeight),
                itemColour = ko.observable(specifiedItemColour),
                itemSizeCustom = ko.observable(specifiedItemSizeCustom),
                itemPaperSize = ko.observable(specifiedItemPaperSize),
                itemCoatedType = ko.observable(specifiedItemCoatedType),
                itemCoated = ko.observable(specifiedItemCoated),
                itemExposure = ko.observable(specifiedItemExposure),
                itemCharge = ko.observable(specifiedItemCharge),
                recLock = ko.observable(specifiedRecLock),
                taxId = ko.observable(specifiedTaxId),
                flag1 = ko.observable(specifiedFlag1),
                flag2 = ko.observable(specifiedFlag2),
                flag3 = ko.observable(specifiedFlag3),
                flag4 = ko.observable(specifiedFlag4),
                companyId = ko.observable(specifiedCompanyId),
                //stockSubCategories = ko.observable(specifiedStockSubCategories),
                stockSubCategories = ko.observableArray([]),
                // Errors
                errors = ko.validation.group({
                    code: code,
                    name: name,
                }),
                // Is Valid 
                isValid = ko.computed(function () {
                    return errors().length === 0 ? true : false;
                }),


                // ReSharper disable InconsistentNaming
                dirtyFlag = new ko.dirtyFlag({
                    // ReSharper restore InconsistentNaming
                    categoryId: categoryId,
                    code: code,
                    name: name,
                    description: description,
                    fixed: fixed,
                    itemWeight: itemWeight,
                    itemColour: itemColour,
                    itemSizeCustom: itemSizeCustom,
                    itemPaperSize: itemPaperSize,
                    itemCoatedType: itemCoatedType,
                    itemCoated: itemCoated,
                    itemExposure: itemExposure,
                    itemCharge: itemCharge,
                    recLock: recLock,
                    taxId: taxId,
                    flag1: flag1,
                    flag2: flag2,
                    flag3: flag3,
                    flag4: flag4,
                    companyId: companyId,
                    stockSubCategories: stockSubCategories
                }),
             // Has Changes
             hasChanges = ko.computed(function () {
                 return dirtyFlag.isDirty();
             }),
             //Convert To Server
             convertToServerData = function (source) {
                 var result = {};
                 result.CategoryId = source.categoryId();
                 result.Code = source.code();
                 result.Name = source.name();
                 result.Description = source.description();
                 result.Fixed = source.fixed() == false ? 0 : 1;
                 result.ItemWeight = source.itemWeight() == false ? 0 : 1;
                 result.ItemColour = source.itemColour() == false ? 0 : 1;
                 result.ItemSizeCustom = source.itemSizeCustom() == false ? 0 : 1;
                 result.ItemPaperSize = source.itemPaperSize() == false ? 0 : 1;
                 result.ItemCoatedType = source.itemCoatedType() == false ? 0 : 1;
                 result.ItemCoated = source.itemCoated() == false ? 0 : 1;
                 result.ItemExposure = source.itemExposure() == false ? 0 : 1;
                 result.ItemCharge = source.itemCharge() == false ? 0 : 1;
                 result.RecLock = source.recLock() == false ? 0 : 1;
                 result.TaxId = source.taxId();
                 result.Flag1 = source.flag1() == false ? 0 : 1;
                 result.Flag2 = source.flag2() == false ? 0 : 1;
                 result.Flag3 = source.flag3() == false ? 0 : 1;
                 result.Flag4 = source.flag4() == false ? 0 : 1;
                 result.CompanyId = source.companyId();
                 //SubCategories
                 result.StockSubCategories = [];
                 _.each(source.stockSubCategories(), function (item) {
                     result.StockSubCategories.push(item.convertToServerData());
                 });
                 return result;
             },
            // Reset
             reset = function () {
                 dirtyFlag.reset();
             };
            self = {
                categoryId: categoryId,
                code: code,
                name: name,
                description: description,
                fixed: fixed,
                itemWeight: itemWeight,
                itemColour: itemColour,
                itemSizeCustom: itemSizeCustom,
                itemPaperSize: itemPaperSize,
                itemCoatedType: itemCoatedType,
                itemCoated: itemCoated,
                itemExposure: itemExposure,
                itemCharge: itemCharge,
                recLock: recLock,
                taxId: taxId,
                flag1: flag1,
                flag2: flag2,
                flag3: flag3,
                flag4: flag4,
                companyId: companyId,
                stockSubCategories: stockSubCategories,
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
    InventoryCategory.CreateFromClientModel = function (source) {
        var newInventoryCategory = new InventoryCategory(
            source.categoryId,
            source.code,
            source.name,
            source.description,
            source.fixed,
            source.itemWeight,
            source.itemColour,
            source.itemSizeCustom,
            source.itemPaperSize,
            source.itemCoatedType,
            source.itemCoated,
            source.itemExposure,
            source.itemCharge,
            source.recLock,
            source.taxId,
            source.flag1,
            source.flag2,
            source.flag3,
            source.flag4,
            source.companyId);
        _.each(source.stockSubCategories, function (item) {
            newInventoryCategory.stockSubCategories.push(InventorySubCategory.CreateFromClientModel(item));
        });
        return newInventoryCategory;
    };


    // InventoryCategory Factory
    InventoryCategory.Create = function (source) {
        var newInventoryCategory = new InventoryCategory(
            source.CategoryId,
            source.Code,
            source.Name,
            source.Description,
            source.Fixed,
            source.ItemWeight,
            source.ItemColour,
            source.ItemSizeCustom,
            source.ItemPaperSize,
            source.ItemCoatedType,
            source.ItemCoated,
            source.ItemExposure,
            source.ItemCharge,
            source.RecLock,
            source.TaxId,
            source.Flag1,
            source.Flag2,
            source.Flag3,
            source.Flag4,
            source.CompanyId
            );
        _.each(source.StockSubCategories, function (item) {
            newInventoryCategory.stockSubCategories.push(InventorySubCategory.Create(item));
        });
        return newInventoryCategory;
    };


    // *** INVENTORY SUB CATEGORY***

    // ReSharper disable once AssignToImplicitGlobalInFunctionScope
    InventorySubCategory = function (specifiedSubCategoryId, specifiedCode, specifiedName, specifiedDescription, specifiedFixed, specifiedCategoryId) {
        var
            self,
            subCategoryId = ko.observable(specifiedSubCategoryId),
            code = ko.observable(specifiedCode),//.extend({ required: true })
            name = ko.observable(specifiedName).extend({ required: true }),
            description = ko.observable(specifiedDescription),
            fixed = ko.observable(specifiedFixed),
            categoryId = ko.observable(specifiedCategoryId),
         // Errors
         errors = ko.validation.group({
             // code: code,
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
                 Fixed: fixed() == false ? 0 : 1,
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


    InventorySubCategory.Create = function (source) {
        return new InventorySubCategory(source.SubCategoryId, source.Code, source.Name, source.Description, source.Fixed, source.CategoryId);
    };

    return {
        InventoryCategory: InventoryCategory,
        InventorySubCategory: InventorySubCategory,
    };
});