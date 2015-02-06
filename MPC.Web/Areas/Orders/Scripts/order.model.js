/*
    Module with the model for the Order
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var
    
    // Estimate Entity
    // ReSharper disable InconsistentNaming
    Estimate = function (specifiedId, specifiedName, specifiedCode) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId || 0),
            // Name
            name = ko.observable(specifiedName || undefined),
            // Code
            code = ko.observable(specifiedCode || undefined),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // Show All Error Messages
            showAllErrors = function () {
                // Show Item Errors
                errors.showAllMessages();
            },
            // Set Validation Summary
            setValidationSummary = function (validationSummaryList) {
                validationSummaryList.removeAll();
            },
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
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    ItemId: id(),
                    ItemCode: code()
                };
            };

        return {
            id: id,
            name: name,
            code: code,
            errors: errors,
            isValid: isValid,
            showAllErrors: showAllErrors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            setValidationSummary: setValidationSummary,
            convertToServerData: convertToServerData
        };
    };

    
    // Estimate Factory
    Estimate.Create = function (source) {
        var estimate = new Estimate(source.ItemId, source.ItemName, source.ItemCode);

        // Return item with dirty state if New
        if (!estimate.id()) {
            return estimate;
        }

        // Reset State to Un-Modified
        estimate.reset();

        return estimate;
    };


    return {
        // Estimate Constructor
        Estimate: Estimate
    };
});