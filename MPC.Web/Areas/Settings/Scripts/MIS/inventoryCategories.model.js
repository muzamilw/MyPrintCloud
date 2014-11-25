define(["ko", "underscore", "underscore-ko", "inventorySubCategory/inventorySubCategory.model"], function (ko, stockSubCategoriesModel) {
    var
        // ReSharper disable once InconsistentNaming
        InventoryCategory = function (specifiedCategoryId, specifiedCode, specifiedName, specifiedDescription, specifiedFixed, specifiedItemWeight,
                                      specifiedItemColour, specifiedItemSizeCustom, specifiedItemPaperSize, specifiedItemCoatedType, specifiedItemCoated,
                                      specifiedItemExposure, specifiedItemCharge, specifiedRecLock, specifiedTaxId, specifiedFlag1, specifiedFlag2,
                                      specifiedFlag3, specifiedFlag4, specifiedCompanyId, specifiedStockSubCategories) {
            var
                self,
                categoryId = ko.observable(specifiedCategoryId),
                code = ko.observable(specifiedCode).extend({ required: true }),
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
                isValid = ko.computed(function() {
                    return errors().length === 0 ? true : false;
                }),

                // True if the booking has been changed
                // ReSharper disable InconsistentNaming
                dirtyFlag = new ko.dirtyFlag({
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
                    taxId: taxId ,
                    flag1 : flag1 ,
                    flag2 : flag2 ,
                    flag3 : flag3 ,
                    flag4 : flag4 ,
                    companyId : companyId ,
                    stockSubCategories: stockSubCategories
             }),
             // Has Changes
             hasChanges = ko.computed(function () {
                 return dirtyFlag.isDirty();
             }),
             convertToServerData = function () {
                 return {
                     CategoryId: categoryId(),
                     Code: code(),
                     Name: name(),
                     Description: description(),
                     Fixed: fixed(),
                     ItemWeight: itemWeight(),
                     ItemColour: itemColour(),
                     ItemSizeCustom: itemSizeCustom(),
                     ItemPaperSize: itemPaperSize(),
                     ItemCoatedType: itemCoatedType(),
                     ItemCoated: itemCoated(),
                     ItemExposure: itemExposure(),
                     ItemCharge: itemCharge(),
                     RecLock: recLock(),
                     TaxId: taxId(),
                     Flag1: flag1(),
                     Flag2: flag2(),
                     Flag3: flag3(),
                     Flag4: flag4(),
                     CompanyId: companyId(),
                     StockSubCategories: stockSubCategories()
                 }
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
                taxId: taxId ,
                flag1 : flag1 ,
                flag2 : flag2 ,
                flag3 : flag3 ,
                flag4 : flag4 ,
                companyId : companyId ,
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
        return new InventoryCategory(source.categoryId, source.code, source.name, source.description, source.fixed, source.itemWeight,
                                     source.itemColour, source.itemSizeCustom, source.itemPaperSize, source.itemCoatedType, source.itemCoated,
                                     source.itemExposure, source.itemCharge, source.recLock, source.taxId, source.flag1, source.flag2,
                                     source.flag3, source.flag4, source.companyId, source.stockSubCategories);
    };
    // server to client mapper
    var InventoryCategoryServertoClientMapper = function (source) {
        return InventoryCategory.Create(source);
    };

    // InventoryCategory Factory
    InventoryCategory.Create = function (source) {
        var newInventoryCategory =  new InventoryCategory(source.CategoryId, source.Code, source.Name, source.Description, source.Fixed, source.ItemWeight, source.ItemColour,
                                     source.ItemSizeCustom, source.ItemPaperSize, source.ItemCoatedType, source.ItemCoated,
                                     source.ItemExposure, source.ItemCharge, source.RecLock, source.TaxId, source.Flag1, source.Flag2,
                                     source.Flag3, source.Flag4, source.CompanyId);
            _.each(source.StockSubCategories, function (item) {
                newInventoryCategory.stockSubCategories.push(stockSubCategoriesModel.inventorySubCategoryServertoClientMapper(item));
            });
        return newInventoryCategory;
    };
    return {
        InventoryCategory: InventoryCategory,
        InventoryCategoryServertoClientMapper: InventoryCategoryServertoClientMapper
    };
});