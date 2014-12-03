/*
    Module with the model for the Product
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var
     // Item Entity
    // ReSharper disable InconsistentNaming
    Item = function (specifiedId, specifiedName, specifiedCode, specifiedProductName, specifiedProductCode, specifiedThumbnail) {
         // ReSharper restore InconsistentNaming
         var // Unique key
             id = ko.observable(specifiedId || 0),
             // Name
             name = ko.observable(specifiedName || undefined).extend({ required: true }),
             // Code
             code = ko.observable(specifiedCode || undefined),
             // Product Name
             productName = ko.observable(specifiedProductName || undefined),
             // Product Code
             productCode = ko.observable(specifiedProductCode || undefined),
             // thumbnail
             thumbnail = ko.observable(specifiedThumbnail || undefined),
             // Errors
             errors = ko.validation.group({
                 name: name
             }),
             // Is Valid
             isValid = ko.computed(function () {
                 return errors().length === 0;
             }),
             // True if the product has been changed
             // ReSharper disable InconsistentNaming
             dirtyFlag = new ko.dirtyFlag({
                 name: name,
                 code: code
             }),
             // Has Changes
             hasChanges = ko.computed(function () {
                 return dirtyFlag.isDirty();
             }),
             // Reset
             reset = function () {
                 dirtyFlag.reset();
             };

         return {
             id: id,
             name: name,
             code: code,
             productName: productName,
             productCode: productCode,
             thumbnail: thumbnail,
             errors: errors,
             isValid: isValid,
             dirtyFlag: dirtyFlag,
             hasChanges: hasChanges,
             reset: reset
         };
     };

    // Item Factory
    Item.Create = function(source) {
        return new Item(source.ItemId, source.ItemName, source.ProductName, source.ProductCode, source.ThumbnailPath);
    }

    return {
        Item: Item
    };
});