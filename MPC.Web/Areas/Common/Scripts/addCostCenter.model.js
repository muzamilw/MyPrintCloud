define(["moment"], function () {
    var

    CostCentre = function (specifiedId, specifiedname,
          specifiedDes, specifiedSetupcost, specifiedPpq, specifiedCompanyTax, specifiedDeliveryCharges, specifiedquantity1, specifiedquantity2, specifiedquantity3,
        specifiedCostCentreType, specifiedCostCentreQuantitySourceType, specifiedCostCentreTimeSourceType, specifiedCalculationMethodType) {

        var self,
            id = ko.observable(specifiedId),
            name = ko.observable(specifiedname),
            quantity1 = ko.observable(specifiedquantity1),
            quantity2 = ko.observable(specifiedquantity2),
            quantity3 = ko.observable(specifiedquantity3),
            description = ko.observable(specifiedDes),
            deliveryCharges = ko.observable(specifiedDeliveryCharges),
            setupCost = ko.observable(specifiedSetupcost).extend({ numberInput: ist.numberFormat }),
            setupCost2 = ko.observable(specifiedSetupcost).extend({ numberInput: ist.numberFormat }),
            setupCost3 = ko.observable(specifiedSetupcost).extend({ numberInput: ist.numberFormat }),
            sectionId = ko.observable(),
            callMode = ko.observable(),
            setupCostWithTax = ko.computed(function () {
                if (specifiedCompanyTax != undefined && specifiedCompanyTax != null) {
                    return setupCost() + (setupCost() * (specifiedCompanyTax / 100));
                }
                return setupCost();
            }),
            deliveryChargesWithTax = ko.computed(function () {
                if (specifiedCompanyTax != undefined && specifiedCompanyTax != null) {
                    return deliveryCharges() + (deliveryCharges() * (specifiedCompanyTax / 100));
                }
                return deliveryCharges();
            }),
            pricePerUnitQuantity = ko.observable(specifiedPpq).extend({ numberInput: ist.numberFormat }),
            // Is Selected - To be used for cost center execution selector
            isSelected = ko.observable(false),
            // Cost Centre Type
            costCentreTypeId = ko.observable(specifiedCostCentreType || undefined),
            // Cost Centre Quantity Source Type
            costCentreQuantitySourceType = ko.observable(specifiedCostCentreQuantitySourceType || undefined),
            // Cost Centre Time Source Type
            costCentreTimeSourceType = ko.observable(specifiedCostCentreTimeSourceType || undefined),
            // Calculation Method Type
            calculationMethodType = ko.observable(specifiedCalculationMethodType || undefined),
            stockQueue = ko.observable(),
            inputQueue = ko.observable(),
            questionQueue = ko.observable(),
            costCenterQueue = ko.observable(),
            errors = ko.validation.group({

            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                id: id,
                name: name,
                quantity1: quantity1,
                quantity2: quantity2,
                quantity3: quantity3,
                description: description,
                setupCost: setupCost,
                pricePerUnitQuantity: pricePerUnitQuantity
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    CostCentreId: id(),
                    Name: name(),
                    Description: description(),
                    SetupCost: setupCost(),
                    PricePerUnitQuantity: pricePerUnitQuantity(),
                    DeliveryCharges: deliveryCharges(),
                    CalculationMethodType: calculationMethodType()
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            id: id,
            name: name,
            quantity1: quantity1,
            quantity2: quantity2,
            quantity3: quantity3,
            description: description,
            setupCost: setupCost,
            setupCost2: setupCost2,
            setupCost3: setupCost3,
            pricePerUnitQuantity: pricePerUnitQuantity,
            setupCostWithTax: setupCostWithTax,
            deliveryChargesWithTax: deliveryChargesWithTax,
            deliveryCharges: deliveryCharges,
            isSelected: isSelected,
            costCentreTypeId: costCentreTypeId,
            costCentreQuantitySourceType: costCentreQuantitySourceType,
            costCentreTimeSourceType: costCentreTimeSourceType,
            calculationMethodType: calculationMethodType,
            sectionId: sectionId,
            callMode: callMode,
            stockQueue: stockQueue,
            costCenterQueue: costCenterQueue,
            inputQueue: inputQueue,
            questionQueue : questionQueue,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };

    CostCentre.Create = function (source) {
        var cost = new CostCentre(
            source.CostCentreId,
            source.Name,
            source.Description,
            source.SetupCost,
            source.PricePerUnitQuantity,
            source.CompanyTaxRate,
            source.DeliveryCharges,
            0,
            0,
            0,
            source.Type,
            source.QuantitySourceType,
            source.TimeSourceType,
            source.CalculationMethodType
            );
        return cost;
    };

  

    return {
        // Cost Centre Constructor
        CostCentre: CostCentre
    };
});