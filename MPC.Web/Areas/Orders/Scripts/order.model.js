/*
    Module with the model for the Order
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    // Estimate Entity
    // ReSharper disable InconsistentNaming
    Estimate = function (specifiedId, specifiedName, specifiedCode, specifiedEstimateName, specifiedNumberOfItems, specifiedCreationDate,
        specifiedFlagColor, specifiedOrderCode,specifiedIsEstimate) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId || 0),
            // Name
            name = ko.observable(specifiedName || undefined),
            // Code
            code = ko.observable(specifiedCode || undefined),
            // Estimate Name
            estimateName = ko.observable(specifiedEstimateName || undefined),
            // Number Of items
            numberOfItems = ko.observable(specifiedNumberOfItems || 0),
            // Number of Items UI
            noOfItemsUi = ko.computed(function() {
                return "( " + numberOfItems() + " ) Items";
            }),
            // Creation Date
            creationDate = ko.observable(specifiedCreationDate || undefined),
            // Flag Color
            flagColor = ko.observable(specifiedFlagColor || undefined),
            // Order Code
            orderCode = ko.observable(specifiedOrderCode || undefined),
                // Is Estimate
            isEstimate = ko.observable(specifiedIsEstimate || undefined),
            // Errors
            errors = ko.validation.group({
            
            }),
            // Is Valid
            isValid = ko.computed(function() {
                return errors().length === 0;
            }),
            // Show All Error Messages
            showAllErrors = function() {
                // Show Item Errors
                errors.showAllMessages();
            },
            // Set Validation Summary
            setValidationSummary = function(validationSummaryList) {
                validationSummaryList.removeAll();
            },
            // True if the product has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                name: name,
                code: code
            }),
            // Has Changes
            hasChanges = ko.computed(function() {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function() {
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function() {
                return {
                    ItemId: id(),
                    ItemCode: code()
                };
            };

        return {
            id: id,
            name: name,
            code: code,
            estimateName: estimateName,
            noOfItemsUi: noOfItemsUi,
            creationDate: creationDate,
            flagColor: flagColor,
            orderCode:orderCode,
            isEstimate:isEstimate,
            errors: errors,
            isValid: isValid,
            showAllErrors: showAllErrors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            setValidationSummary: setValidationSummary,
            convertToServerData: convertToServerData
        };
    },

    // Section Flag Entity        
    SectionFlag = function(specifiedId, specifiedFlagName, specifiedFlagColor) {
            return {
                id: specifiedId,
                name: specifiedFlagName,
                color: specifiedFlagColor
            };
        };
    
    // Estimate Factory
    Estimate.Create = function(source) {
        var estimate = new Estimate(source.CompanyId, source.CompanyName, source.EstimateCode, source.EstimateName, source.ItemsCount,
            source.CreationDate, source.FlagColor, source.OrderCode, source.IsEstimate);

        // Return item with dirty state if New
        if (!estimate.id()) {
            return estimate;
        }

        // Reset State to Un-Modified
        estimate.reset();

        return estimate;
    };
    
    // Section Flag Factory
    SectionFlag.Create = function (source) {
        return new SectionFlag(source.SectionFlagId, source.FlagName, source.FlagColor);
    };


    return {
        // Estimate Constructor
        Estimate: Estimate,
        // sectionflag constructor
        SectionFlag: SectionFlag
    };
});