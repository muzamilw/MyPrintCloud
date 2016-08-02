﻿define(["ko", "underscore", "underscore-ko"], function (ko) {
    


    var CostCenter = function () {

        var
            self,
            costCentreId = ko.observable(),
            name = ko.observable().extend({ required: true }),
            description = ko.observable(),
            type = ko.observable().extend({ required: true }),
            typeName = ko.observable(),
            createdBy = ko.observable(),
            lockedBy = ko.observable(),
            lastModifiedBy = ko.observable(),
            minimumCost = ko.observable().extend({ required: true }),
            setupCost = ko.observable().extend({ required: true }),
            setupTime = ko.observable(),
            defaultVa = ko.observable(),
            defaultVaId = ko.observable(),
            overHeadRate = ko.observable(),
            hourlyCharge = ko.observable(),
            costPerThousand = ko.observable(),
            creationDate = ko.observable(),
            lastModifiedDate = ko.observable(),
            preferredSupplierId = ko.observable(),
            codeFileName = ko.observable(),
            nominalCode = ko.observable(),
            completionTime = ko.observable(),
            headerCode = ko.observable(),
            middleCode = ko.observable(),
            footerCode = ko.observable(),
            strCostPlantParsed = ko.observable(),
            strCostPlantUnParsed = ko.observable(),
            strCostLabourParsed = ko.observable(),
            strCostLabourUnParsed = ko.observable(),
            strCostMaterialParsed = ko.observable(),
            strCostMaterialUnParsed = ko.observable(),
            strPricePlantParsed = ko.observable(),
            strPricePlantUnParsed = ko.observable(),
            strPriceLabourParsed = ko.observable(),
            strPriceLabourUnParsed = ko.observable(),
            strPriceMaterialParsed = ko.observable(),
            strPriceMaterialUnParsed = ko.observable(),
            strActualCostPlantParsed = ko.observable(),
            strActualCostPlantUnParsed = ko.observable(),
            strActualCostLabourParsed = ko.observable(),
            strActualCostLabourUnParsed = ko.observable(),
            strActualCostMaterialParsed = ko.observable(),
            strActualCostMaterialUnParsed = ko.observable(),
            strTimeParsed = ko.observable(),
            strTimeUnParsed = ko.observable(),
            isDisabled = ko.observable(),
            isDirectCost = ko.observable(),
            setupSpoilage = ko.observable(),
            runningSpoilage = ko.observable(),
            calculationMethodType = ko.observable().extend({ required: true }),
            noOfHours = ko.observable(),
            perHourCost = ko.observable(),
            perHourPrice = ko.observable(),
            unitQuantity = ko.observable(),
            quantitySourceType = ko.observable(),
            quantityVariableId = ko.observable(),
            quantityQuestionString = ko.observable(),
            quantityQuestionDefaultValue = ko.observable(),
            quantityCalculationString = ko.observable(),
            costPerUnitQuantity = ko.observable(),
            pricePerUnitQuantity = ko.observable().extend({ required: true }),
            timePerUnitQuantity = ko.observable(),
            timeRunSpeed = ko.observable(),
            timeNoOfPasses = ko.observable(1),
            timeSourceType = ko.observable(),
            timeVariableId = ko.observable(),
            timeQuestionString = ko.observable(),
            timeQuestionDefaultValue = ko.observable(),
            timeCalculationString = ko.observable(),
            priority = ko.observable(),
            costQuestionString = ko.observable(),
            costDefaultValue = ko.observable(),
            priceQuestionString = ko.observable(),
            priceDefaultValue = ko.observable(),
            estimatedTimeQuestionString = ko.observable(),
            estimatedTimeDefaultValue = ko.observable(),
            sequence = ko.observable(),
            completeCode = ko.observable(),
            itemDescription = ko.observable(),
            companyId = ko.observable(),
            systemTypeId = ko.observable(),
            flagId = ko.observable(),
            isScheduleable = ko.observable(),
            isPrintOnJobCard = ko.observable(),
            webStoreDesc = ko.observable(),
            isPublished = ko.observable(),
            estimateProductionTime = ko.observable(),
            mainImageUrl = ko.observable(),
            thumbnailImageUrl = ko.observable(),
            deliveryCharges = ko.observable(),
            xeroAccessCode = ko.observable(),
            organisationId = ko.observable(),
            costCenterResource = ko.observableArray([]),
            costCenterInstructions = ko.observableArray([]),
            serviceTypesList = ko.observableArray([]),
            deliveryServiceType = ko.observable(),
            costcentreImageFileBinary = ko.observable(),
            costcentreImageName = ko.observable(),
            carrierId = ko.observable(),
             isEditPlantCost = ko.observable(false),
             isEditPlantQuote = ko.observable(false),
             isEditPlantActualCost = ko.observable(false),
             isEditStockCost = ko.observable(false),
             isEditStockQuote = ko.observable(false),
             isEditStockActualCost = ko.observable(false),
             isEditLabourCost = ko.observable(false),
             isEditLabourQuote = ko.observable(false),
             isEditLabourActualCost = ko.observable(false),
             isEditTime = ko.observable(false),
             isTimeVariable = ko.observable(),
             //isTimePrompt = ko.observable(false)
             isQtyVariable = ko.observable(),
             isCalculationMethodEnable = ko.observable(costCentreId === 0 || costCentreId === undefined ? true : false),
        //isQtyPrompt = ko.observable(false),
        errors = ko.validation.group({
            name: name,
            type: type,
            setupCost: setupCost
            // pricePerUnitQuantity: pricePerUnitQuantity,
            //  minimumCost: minimumCost,
            // perHourPrice: perHourPrice,
            // timeQuestionString: timeQuestionString,
            //quantiQuestionString
        }),
        isValid = ko.computed(function () {
            return errors().length === 0 ? true : false;;
        }),
         showAllErrors = function () {
             // Show Item Errors
             errors.showAllMessages();
         },
        dirtyFlag = new ko.dirtyFlag({
            name: name,
            description: description,
            webStoreDesc: webStoreDesc,
            calculationMethodType: calculationMethodType,
            priority: priority,
            type: type,
            costQuestionString: costQuestionString,
            costDefaultValue: costDefaultValue,
            priceQuestionString: priceQuestionString,
            priceDefaultValue: priceDefaultValue,
            timeQuestionString:timeQuestionString,
            estimatedTimeQuestionString: estimatedTimeQuestionString,
            estimatedTimeDefaultValue: estimatedTimeDefaultValue,
            minimumCost: minimumCost,
            setupCost: setupCost,
            costPerThousand: costPerThousand,
            hourlyCharge: hourlyCharge,
            overHeadRate: overHeadRate,
            completionTime: completionTime,
            setupTime: setupTime,
            setupSpoilage: setupSpoilage,
            runningSpoilage: runningSpoilage,
            isDirectCost: isDirectCost,
            isScheduleable: isScheduleable,
            isPrintOnJobCard: isPrintOnJobCard,
            perHourPrice: perHourPrice,
            costPerUnitQuantity: costPerUnitQuantity,
            pricePerUnitQuantity: pricePerUnitQuantity,
            timePerUnitQuantity: timePerUnitQuantity,
            quantityVariableId: quantityVariableId,
            quantityQuestionString: quantityQuestionString,
            quantityQuestionDefaultValue: quantityQuestionDefaultValue,
            strCostPlantUnParsed: strCostPlantUnParsed,
            strPricePlantUnParsed: strPricePlantUnParsed,
            strActualCostPlantUnParsed: strActualCostPlantUnParsed,
            strCostMaterialUnParsed: strCostMaterialUnParsed,
            strPriceMaterialUnParsed: strPriceMaterialUnParsed,
            strActualCostMaterialUnParsed: strActualCostMaterialUnParsed,
            strCostLabourUnParsed: strCostLabourUnParsed,
            strPriceLabourUnParsed: strPriceLabourUnParsed,
            strActualCostLabourUnParsed: strActualCostLabourUnParsed,
            strTimeUnParsed: strTimeUnParsed,
            carrierId: carrierId,
            costCenterInstructions: costCenterInstructions,
            deliveryCharges: deliveryCharges,
            isPublished: isPublished,
            deliveryServiceType: deliveryServiceType,
            estimateProductionTime: estimateProductionTime,
            costcentreImageName: costcentreImageName,
            isTimeVariable: isTimeVariable,
            isDisabled: isDisabled,
            timeQuestionDefaultValue: timeQuestionDefaultValue,
            timeVariableId : timeVariableId,
            //isTimePrompt: isTimePrompt
            isQtyVariable: isQtyVariable,
            defaultVaId: defaultVaId
            //isQtyPrompt: isQtyPrompt
           
        }),
        hasChanges = ko.computed(function () {
            return dirtyFlag.isDirty();
        }),

        reset = function () {
            dirtyFlag.reset();
        };
        self = {
            costCentreId: costCentreId,
            name: name,
            description: description,
            type: type,
            typeName: typeName,
            createdBy: createdBy,
            lockedBy: lockedBy,
            lastModifiedBy: lastModifiedBy,
            minimumCost: minimumCost,
            setupCost: setupCost,
            setupTime: setupTime,
            defaultVa: defaultVa,
            defaultVaId: defaultVaId,
            overHeadRate: overHeadRate,
            hourlyCharge: hourlyCharge,
            costPerThousand: costPerThousand,
            creationDate: creationDate,
            lastModifiedDate: lastModifiedDate,
            preferredSupplierId: preferredSupplierId,
            codeFileName: codeFileName,
            nominalCode: nominalCode,
            completionTime: completionTime,
            headerCode: headerCode,
            middleCode: middleCode,
            footerCode: footerCode,
            strCostPlantParsed: strCostPlantParsed,
            strCostPlantUnParsed: strCostPlantUnParsed,
            strCostLabourParsed: strCostLabourParsed,
            strCostLabourUnParsed: strCostLabourUnParsed,
            strCostMaterialParsed: strCostMaterialParsed,
            strCostMaterialUnParsed: strCostMaterialUnParsed,
            strPricePlantParsed: strPricePlantParsed,
            strPricePlantUnParsed: strPricePlantUnParsed,
            strPriceLabourParsed: strPriceLabourParsed,
            strPriceLabourUnParsed: strPriceLabourUnParsed,
            strPriceMaterialParsed: strPriceMaterialParsed,
            strPriceMaterialUnParsed: strPriceMaterialUnParsed,
            strActualCostPlantParsed: strActualCostPlantParsed,
            strActualCostPlantUnParsed: strActualCostPlantUnParsed,
            strActualCostLabourParsed: strActualCostLabourParsed,
            strActualCostLabourUnParsed: strActualCostLabourUnParsed,
            strActualCostMaterialParsed: strActualCostMaterialParsed,
            strActualCostMaterialUnParsed: strActualCostMaterialUnParsed,
            strTimeParsed: strTimeParsed,
            strTimeUnParsed: strTimeUnParsed,
            isDisabled: isDisabled,
            isDirectCost: isDirectCost,
            setupSpoilage: setupSpoilage,
            runningSpoilage: runningSpoilage,
            calculationMethodType: calculationMethodType,
            noOfHours: noOfHours,
            perHourCost: perHourCost,
            perHourPrice: perHourPrice,
            unitQuantity: unitQuantity,
            quantitySourceType: quantitySourceType,
            quantityVariableId: quantityVariableId,
            quantityQuestionString: quantityQuestionString,
            quantityQuestionDefaultValue: quantityQuestionDefaultValue,
            quantityCalculationString: quantityCalculationString,
            costPerUnitQuantity: costPerUnitQuantity,
            pricePerUnitQuantity: pricePerUnitQuantity,
            timePerUnitQuantity: timePerUnitQuantity,
            timeRunSpeed: timeRunSpeed,
            timeNoOfPasses: timeNoOfPasses,
            timeSourceType: timeSourceType,
            timeVariableId: timeVariableId,
            timeQuestionString: timeQuestionString,
            timeQuestionDefaultValue: timeQuestionDefaultValue,
            timeCalculationString: timeCalculationString,
            priority: priority,
            costQuestionString: costQuestionString,
            costDefaultValue: costDefaultValue,
            priceQuestionString: priceQuestionString,
            priceDefaultValue: priceDefaultValue,
            estimatedTimeQuestionString: estimatedTimeQuestionString,
            estimatedTimeDefaultValue: estimatedTimeDefaultValue,
            sequence: sequence,
            completeCode: completeCode,
            itemDescription: itemDescription,
            companyId: companyId,
            systemTypeId: systemTypeId,
            flagId: flagId,
            isScheduleable: isScheduleable,
            isPrintOnJobCard: isPrintOnJobCard,
            webStoreDesc: webStoreDesc,
            isPublished: isPublished,
            estimateProductionTime: estimateProductionTime,
            mainImageUrl: mainImageUrl,
            thumbnailImageUrl: thumbnailImageUrl,
            deliveryCharges: deliveryCharges,
            xeroAccessCode: xeroAccessCode,
            organisationId: organisationId,
            costCenterResource: costCenterResource,
            costCenterInstructions: costCenterInstructions,
            isEditPlantCost: isEditPlantCost,
            isEditPlantQuote: isEditPlantQuote,
            isEditPlantActualCost: isEditPlantActualCost,
            isEditStockCost: isEditStockCost,
            isEditStockQuote: isEditStockQuote,
            isEditStockActualCost: isEditStockActualCost,
            isEditLabourCost: isEditLabourCost,
            isEditLabourQuote: isEditLabourQuote,
            isEditLabourActualCost: isEditLabourActualCost,
            isCalculationMethodEnable:isCalculationMethodEnable,
            isEditTime: isEditTime,
            dirtyFlag: dirtyFlag,
            errors: errors,
            isValid: isValid,
            hasChanges: hasChanges,
            reset: reset,
            serviceTypesList: serviceTypesList,
            deliveryServiceType: deliveryServiceType,
            carrierId: carrierId,
            costcentreImageFileBinary: costcentreImageFileBinary,
            costcentreImageName: costcentreImageName,
            isTimeVariable: isTimeVariable,
            //isTimePrompt: isTimePrompt,
            isQtyVariable: isQtyVariable,
            showAllErrors: showAllErrors
            //isQtyPrompt: isQtyPrompt
        };
        return self;
    };

    costCenterListView = function (specifiedCostCentreId, specifiedName, specifiedDescription, specifiedType, specifiedCalType, specifiedStatus
                            ) {
        var
            self,
            //Unique ID
            costCenterId = ko.observable(specifiedCostCentreId),
            //Name
            name = ko.observable(specifiedName),
            //Description
            description = ko.observable(specifiedDescription),
            //Type
            type = ko.observable(specifiedType),
            calculationMethodType = ko.observable(specifiedCalType),
            isDisabled = ko.observable(specifiedStatus),
        convertToServerData = function () {
            return {
                CostCentreId: costCenterId(),
            };
        };
        self = {
            costCenterId: costCenterId,
            name: name,
            description: description,
            type: type,
            isDisabled: isDisabled,
            calculationMethodType: calculationMethodType,
            convertToServerData: convertToServerData,
        };
        return self;
    };

    costCenterListView.Create = function (source) {
        return new costCenterListView(source.CostCentreId, source.Name, source.Description, source.Type, source.CalculationMethodType, source.IsDisabled);
    };
    //Cost Center Instructions for Client
    costCenterInstruction = function (specifiedInstructionId, specifiedInstruction, specifiedcostCenterOption, specifiedCostCentreId) {
        var
             self,
             instructionId = ko.observable(specifiedInstructionId),
             instruction = ko.observable(specifiedInstruction).extend({ required: true }),
             costCenterOption = ko.observable(specifiedcostCenterOption),
             costCenterId = ko.observable(specifiedCostCentreId),
             workInstructionChoices = ko.observableArray([]),
              dirtyFlag = new ko.dirtyFlag({
                  instruction: instruction,
                  workInstructionChoices: workInstructionChoices,
              }),
                    errors = ko.validation.group({
                        instruction: instruction
                    }),
        isValid = ko.computed(function () {
            return errors().length === 0 ? true : false;;
        }),
             // Has Changes
             hasChanges = ko.computed(function () {
                 return dirtyFlag.isDirty();
             }),
             convertToServerData = function () {
                 return {
                     Instruction: instruction(),
                     InstructionId: instructionId(),
                     CostCenterOption: costCenterOption()
                 }
             };
        self = {
            instructionId: instructionId,
            instruction: instruction,
            costCenterOption: costCenterOption,
            costCenterId: costCenterId,
            workInstructionChoices: workInstructionChoices,
            convertToServerData: convertToServerData,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            isValid: isValid,
            errors: errors
        };
        return self;

    };

    costCenterInstruction.CreateFromClientModel = function (source) {
        var result = {};
        result.InstructionId = source.instructionId();
        result.Instruction = source.instruction();
        result.CostCenterOption = source.costCenterOption();
        result.CostcentreWorkInstructionsChoices = [];
        _.each(source.workInstructionChoices(), function (item) {
            result.CostcentreWorkInstructionsChoices.push(item.convertToServerData());
        });
        return result;
        //return new costCenterInstruction(
        //    source.InstructionId, source.Instruction, source.CostCenterOption
        //);
    };
    costCenterInstruction.Create = function (source) {
        var ccInstruction = new costCenterInstruction(
            source.InstructionId,
            source.Instruction,
            source.costCenterOption,
            source.CostCentreId
            );
        _.each(source.CostcentreWorkInstructionsChoices, function (item) {
            ccInstruction.workInstructionChoices.push(costCenterInstructionChoice.Create(item))
        });
        return ccInstruction;
    };
    NewCostCenterInstruction = function () {
        var cci = new costCenterInstruction(0);
        return cci;
    };
    NewInstructionChoice = function () {
        var cic = new costCenterInstructionChoice(0);
        return cic;
    };
    costCenterInstructionChoice = function (specifiedChoiceId, specifiedChoice, specifiedInstructionId) {
        var
             self,
             choiceId = ko.observable(specifiedChoiceId),
             choice = ko.observable(specifiedChoice).extend({ required: true }),
             instructionId = ko.observable(specifiedInstructionId),
              dirtyFlag = new ko.dirtyFlag({
                  choice: choice
              }),
                   errors = ko.validation.group({
                       choice: choice
                   }),
        isValid = ko.computed(function () {
            return errors().length === 0 ? true : false;;
        }),
             // Has Changes
             hasChanges = ko.computed(function () {
                 return dirtyFlag.isDirty();
             }),
             convertToServerData = function () {
                 return {
                     Id: choiceId(),
                     Choice: choice(),
                     InstructionId: instructionId()
                 }
             };
        self = {
            choiceId: choiceId,
            choice: choice,
            instructionId: instructionId,
            convertToServerData: convertToServerData,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            isValid: isValid,
            errors: errors

        };
        return self;

    };
    costCenterInstructionChoice.Create = function (source) {
        var wiChoice = new costCenterInstructionChoice(
            source.Id,
            source.Choice,
            source.InstructionId
            );
        return wiChoice;
    };
    costCenterInstructionChoice.CreateFromClientModel = function (source) {
        return new costCenterInstructionChoice(
            source.Id, source.Choice, source.InstructionId
        );
    };

    costCenterResource = function (specifiedCostCenterResourceId, specifiedCostCentreId, specifiedResourceId) {
        var
             self,
             costCenterResourceId = ko.observable(specifiedCostCenterResourceId),
             costCentreId = ko.observable(specifiedCostCentreId),
             resourceId = ko.observable(specifiedResourceId),
              dirtyFlag = new ko.dirtyFlag({
                  resourceId: resourceId
              }),
             // Has Changes
             hasChanges = ko.computed(function () {
                 return dirtyFlag.isDirty();
             }),
             convertToServerData = function () {
                 return {
                     CostCenterResourceId: costCenterResourceId(),
                     CostCentreId: costCentreId(),
                     ResourceId: resourceId()
                 }
             };
        self = {
            costCenterResourceId: costCenterResourceId,
            costCentreId: costCentreId,
            resourceId: resourceId,
            hasChanges: hasChanges,
            dirtyFlag: dirtyFlag,
            convertToServerData: convertToServerData
        };
        return self;

    };

    var costCenterClientMapper = function (source) {
        var oCostCenter = new CostCenter();
        oCostCenter.costCentreId(source.CostCentreId);
        oCostCenter.name(source.Name);
        oCostCenter.description(source.Description);
        oCostCenter.type(source.Type);
        oCostCenter.typeName(source.TypeName);
        oCostCenter.createdBy(source.createdBy);
        oCostCenter.lockedBy(source.LockedBy);
        oCostCenter.lastModifiedBy(source.LastModifiedBy);
        oCostCenter.minimumCost(source.MinimumCost);
        oCostCenter.setupCost(source.SetupCost);
        oCostCenter.setupTime(source.SetupTime);
        oCostCenter.defaultVa(source.DefaultVA);
        oCostCenter.defaultVaId(source.DefaultVAId);
        oCostCenter.overHeadRate(source.OverHeadRate);
        oCostCenter.hourlyCharge(source.HourlyCharge);
        oCostCenter.costPerThousand(source.CostPerThousand);
        oCostCenter.creationDate(source.CreationDate);
        oCostCenter.lastModifiedDate(source.LastModifiedDate);
        oCostCenter.preferredSupplierId(source.PreferredSupplierId);
        oCostCenter.codeFileName(source.CodeFileName);
        oCostCenter.nominalCode(source.nominalCode);
        oCostCenter.completionTime(source.CompletionTime);
        oCostCenter.headerCode(source.HeaderCode);
        oCostCenter.middleCode(source.MiddleCode);
        oCostCenter.footerCode(source.FooterCode);
        oCostCenter.strCostPlantParsed(source.strCostPlantParsed);
        oCostCenter.strCostPlantUnParsed(source.strCostPlantUnParsed === undefined || source.strCostPlantUnParsed === null ? 'EstimatedPlantCost = 0' : source.strCostPlantUnParsed);
        oCostCenter.strCostLabourParsed(source.strCostLabourParsed);
        oCostCenter.strCostLabourUnParsed(source.strCostLabourUnParsed === undefined || source.strCostLabourUnParsed === null ? 'EstimatedLabourCost = 0' : source.strCostLabourUnParsed);
        oCostCenter.strCostMaterialParsed(source.strCostMaterialParsed);
        oCostCenter.strCostMaterialUnParsed(source.strCostMaterialUnParsed === undefined || source.strCostMaterialUnParsed === null ? 'EstimatedMaterialCost = 0' : source.strCostMaterialUnParsed);
        oCostCenter.strPricePlantParsed(source.strPricePlantParsed);
        oCostCenter.strPricePlantUnParsed(source.strPricePlantUnParsed === undefined || source.strPricePlantUnParsed === null ? 'QuotedPlantPrice = 0' : source.strPricePlantUnParsed);
        oCostCenter.strPriceLabourParsed(source.strPriceLabourParsed);
        oCostCenter.strPriceLabourUnParsed(source.strPriceLabourUnParsed === undefined || source.strPriceLabourUnParsed === null ? 'QuotedLabourPrice = 0' : source.strPriceLabourUnParsed);
        oCostCenter.strPriceMaterialParsed(source.strPriceMaterialParsed);
        oCostCenter.strPriceMaterialUnParsed(source.strPriceMaterialUnParsed === undefined || source.strPriceMaterialUnParsed === null ? 'QuotedMaterialPrice = 0' : source.strPriceMaterialUnParsed);
        oCostCenter.strActualCostPlantParsed(source.strActualCostPlantParsed);
        oCostCenter.strActualCostPlantUnParsed(source.strActualCostPlantUnParsed === undefined || source.strActualCostPlantUnParsed === null ? 'ActualPlantCost = 0' : source.strActualCostPlantUnParsed);
        oCostCenter.strActualCostLabourParsed(source.strActualCostLabourParsed);
        oCostCenter.strActualCostLabourUnParsed(source.strActualCostLabourUnParsed === undefined || source.strActualCostLabourUnParsed === null ? 'ActualLabourCost = 0' : source.strActualCostLabourUnParsed);
        oCostCenter.strActualCostMaterialParsed(source.strActualCostMaterialParsed);
        oCostCenter.strActualCostMaterialUnParsed(source.strActualCostMaterialUnParsed === undefined || source.strActualCostMaterialUnParsed === null ? '' : source.strActualCostMaterialUnParsed);
        oCostCenter.strTimeParsed(source.strTimeParsed);
        oCostCenter.strTimeUnParsed(source.strTimeUnParsed === undefined || source.strTimeUnParsed === null ? 'EstimatedTime = 0' : source.strTimeUnParsed);
        if (source.IsDisabled) {
            oCostCenter.isDisabled(false);
        } else {
            oCostCenter.isDisabled(true);
        }
        
        oCostCenter.isDirectCost(source.IsDirectCost);
        oCostCenter.setupSpoilage(source.SetupSpoilage);
        oCostCenter.runningSpoilage(source.RunningSpoilage);
        oCostCenter.calculationMethodType(source.CalculationMethodType);
        oCostCenter.noOfHours(source.NoOfHours);
        oCostCenter.perHourCost(source.PerHourCost);
        oCostCenter.perHourPrice(source.PerHourPrice);
        oCostCenter.unitQuantity(source.UnitQuantity);
        oCostCenter.quantitySourceType(source.QuantitySourceType);
        oCostCenter.quantityVariableId(source.QuantityVariableId);
        oCostCenter.quantityQuestionString(source.QuantityQuestionString);
        oCostCenter.quantityQuestionDefaultValue(source.QuantityQuestionDefaultValue);
        oCostCenter.quantityCalculationString(source.QuantityCalculationString);
        oCostCenter.costPerUnitQuantity(source.CostPerUnitQuantity);
        oCostCenter.pricePerUnitQuantity(source.PricePerUnitQuantity);
        oCostCenter.timePerUnitQuantity(source.TimePerUnitQuantity);
        oCostCenter.timeRunSpeed(source.TimeRunSpeed);
        oCostCenter.timeNoOfPasses(source.TimeNoOfPasses);
        oCostCenter.timeSourceType(source.TimeSourceType);
        oCostCenter.timeVariableId(source.TimeVariableId);
        oCostCenter.timeQuestionString(source.TimeQuestionString);
        oCostCenter.timeQuestionDefaultValue(source.TimeQuestionDefaultValue);
        oCostCenter.timeCalculationString(source.TimeCalculationString);
        oCostCenter.priority(source.Priority);
        oCostCenter.costQuestionString(source.CostQuestionString);
        oCostCenter.costDefaultValue(source.CostDefaultValue);
        oCostCenter.priceQuestionString(source.PriceQuestionString);
        oCostCenter.priceDefaultValue(source.PriceDefaultValue);
        oCostCenter.estimatedTimeQuestionString(source.EstimatedTimeQuestionString);
        oCostCenter.estimatedTimeDefaultValue(source.EstimatedTimeDefaultValue);
        oCostCenter.sequence(source.Sequence);
        oCostCenter.completeCode(source.CompleteCode);
        oCostCenter.itemDescription(source.ItemDescription);
        oCostCenter.companyId(source.CompanyId);
        oCostCenter.systemTypeId(source.SystemTypeId);
        oCostCenter.flagId(source.FlagId);
        oCostCenter.isScheduleable(source.IsScheduleable);
        oCostCenter.isPrintOnJobCard(source.IsPrintOnJobCard);
        oCostCenter.webStoreDesc(source.WebStoreDesc);
        oCostCenter.isPublished(source.isPublished);
        oCostCenter.estimateProductionTime(source.EstimateProductionTime);
        oCostCenter.mainImageUrl(source.MainImageURL);
        oCostCenter.thumbnailImageUrl(source.ThumbnailImageURL);
        oCostCenter.deliveryCharges(source.DeliveryCharges);
        oCostCenter.xeroAccessCode(source.XeroAccessCode);
        oCostCenter.organisationId(source.OrganisationId);
        oCostCenter.deliveryServiceType(source.DeliveryServiceType);
        oCostCenter.carrierId(source.CarrierId);
        //if (source.TimeSourceType === 1) {
        //    oCostCenter.isTimeVariable(true);
        //};
        //if (source.TimeSourceType === 2) {
        //    oCostCenter.isTimePrompt(true);
        //};
        oCostCenter.isTimeVariable(source.TimeSourceType === 1 ? '1' : '2');
        //oCostCenter.isTimePrompt(source.TimeSourceType === 2 ? true : false);
        oCostCenter.isQtyVariable(source.QuantitySourceType === 1 ? '1' : '2');
        // oCostCenter.isQtyPrompt(source.QuantitySourceType === 2 ? true : false);

        // oCostCenter.serviceTypesList(ServiceTypesList());
        _.each(source.CostcentreInstructions, function (item) {
            oCostCenter.costCenterInstructions.push(costCenterInstruction.Create(item));
        });
        return oCostCenter;

    };
    var ServiceTypeModel = function (data) {
        var self = this;
        self.name = ko.observable(data.name);
    };


    var ServiceTypesList = function () {
        ServiceTypesList = [];
        ServiceTypesList.push(new ServiceTypeModel({ name: "EUROPE_FIRST_INTERNATIONAL_PRIORITY" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_1_DAY_FREIGHT" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_2_DAY" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_2_DAY_AM" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_2_DAY_FREIGHT" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_3_DAY_FREIGHT" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_DISTANCE_DEFERRED" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_EXPRESS_SAVER" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_FIRST_FREIGHT" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_FREIGHT_ECONOMY" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_FREIGHT_PRIORITY" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_GROUND" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_NEXT_DAY_AFTERNOON" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_NEXT_DAY_EARLY_MORNING" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_NEXT_DAY_END_OF_DAY" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_NEXT_DAY_FREIGHT" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FEDEX_NEXT_DAY_MID_MORNING" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "GROUND_HOME_DELIVERY" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "FIRST_OVERNIGHT" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "INTERNATIONAL_ECONOMY" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "INTERNATIONAL_ECONOMY_FREIGHT" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "INTERNATIONAL_FIRST" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "INTERNATIONAL_PRIORITY" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "INTERNATIONAL_PRIORITY_FREIGHT" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "PRIORITY_OVERNIGHT" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "SAME_DAY" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "SAME_DAY_CITY" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "SMART_POST" }));
        ServiceTypesList.push(new ServiceTypeModel({ name: "STANDARD_OVERNIGHT" }));
        return ServiceTypesList;
    };

    var costCenterServerMapper = function (source) {
        var result = {};
        result.CostCentreId = source.costCentreId();
        result.Name = source.name();
        result.Description = source.description();
        result.Type = source.type();
        result.createdBy = source.createdBy();
        result.LockedBy = source.lockedBy();
        result.LastModifiedBy = source.lastModifiedBy();
        result.MinimumCost = source.minimumCost();
        result.SetupCost = source.setupCost();
        result.SetupTime = source.setupTime();
        result.DefaultVA = source.defaultVa();
        result.DefaultVAId = source.defaultVaId();
        result.OverHeadRate = source.overHeadRate();
        result.HourlyCharge = source.hourlyCharge();
        result.CostPerThousand = source.costPerThousand();
        result.CreationDate = source.creationDate();
        result.LastModifiedDate = source.lastModifiedDate();
        result.PreferredSupplierId = source.preferredSupplierId();
        result.CodeFileName = source.codeFileName();
        result.nominalCode = source.nominalCode();
        result.CompletionTime = source.completionTime();
        result.HeaderCode = source.headerCode();
        result.MiddleCode = source.middleCode();
        result.FooterCode = source.footerCode();
        result.strCostPlantParsed = source.strCostPlantParsed();
        result.strCostPlantUnParsed = source.strCostPlantUnParsed();
        result.strCostLabourParsed = source.strCostLabourParsed();
        result.strCostLabourUnParsed = source.strCostLabourUnParsed();
        result.strCostMaterialParsed = source.strCostMaterialParsed();
        result.strCostMaterialUnParsed = source.strCostMaterialUnParsed();
        result.strPricePlantParsed = source.strPricePlantParsed();
        result.strPricePlantUnParsed = source.strPricePlantUnParsed();
        result.strPriceLabourParsed = source.strPriceLabourParsed();
        result.strPriceLabourUnParsed = source.strPriceLabourUnParsed();
        result.strPriceMaterialParsed = source.strPriceMaterialParsed();
        result.strPriceMaterialUnParsed = source.strPriceMaterialUnParsed();
        result.strActualCostPlantParsed = source.strActualCostPlantParsed();
        result.strActualCostPlantUnParsed = source.strActualCostPlantUnParsed();
        result.strActualCostLabourParsed = source.strActualCostLabourParsed();
        result.strActualCostLabourUnParsed = source.strActualCostLabourUnParsed();
        result.strActualCostMaterialParsed = source.strActualCostMaterialParsed();
        result.strActualCostMaterialUnParsed = source.strActualCostMaterialUnParsed();
        result.strTimeParsed = source.strTimeParsed();
        result.strTimeUnParsed = source.strTimeUnParsed();
        if (source.isDisabled()) {
            result.IsDisabled= false
        } else {
            result.IsDisabled = true;
        }
        //result.IsDisabled = source.isDisabled();
        result.IsDirectCost = source.isDirectCost();
        result.SetupSpoilage = source.setupSpoilage();
        result.RunningSpoilage = source.runningSpoilage();
        result.CalculationMethodType = source.calculationMethodType();
        result.NoOfHours = source.noOfHours();
        result.PerHourCost = source.perHourCost();
        result.PerHourPrice = source.perHourPrice();
        result.UnitQuantity = source.unitQuantity();
        result.QuantityVariableId = source.quantityVariableId();
        result.QuantityQuestionString = source.quantityQuestionString();
        result.QuantityQuestionDefaultValue = source.quantityQuestionDefaultValue();
        result.QuantityCalculationString = source.quantityCalculationString();
        result.CostPerUnitQuantity = source.costPerUnitQuantity();
        result.PricePerUnitQuantity = source.pricePerUnitQuantity();
        result.TimePerUnitQuantity = source.timePerUnitQuantity();
        result.TimeRunSpeed = source.timeRunSpeed();
        result.TimeNoOfPasses = 1;// source.timeNoOfPasses();
        result.TimeVariableId = source.timeVariableId();
        result.TimeQuestionString = source.timeQuestionString();
        result.TimeQuestionDefaultValue = source.timeQuestionDefaultValue();
        result.TimeCalculationString = source.timeCalculationString();
        result.Priority = source.priority();
        result.CostQuestionString = source.costQuestionString();
        result.CostDefaultValue = source.costDefaultValue();
        result.PriceQuestionString = source.priceQuestionString();
        result.PriceDefaultValue = source.priceDefaultValue();
        result.EstimatedTimeQuestionString = source.estimatedTimeQuestionString();
        result.EstimatedTimeDefaultValue = source.estimatedTimeDefaultValue();
        result.Sequence = source.sequence();
        result.CompleteCode = source.completeCode();
        result.ItemDescription = source.itemDescription();
        result.CompanyId = source.companyId();
        result.SystemTypeId = source.systemTypeId();
        result.FlagId = source.flagId();
        result.IsScheduleable = source.isScheduleable();
        result.IsPrintOnJobCard = source.isPrintOnJobCard() === true ? 1 : 0;
        result.WebStoreDesc = source.webStoreDesc();
        result.isPublished = source.isPublished();
        result.EstimateProductionTime = source.estimateProductionTime();
        result.MainImageURL = source.mainImageUrl();
        result.ThumbnailImageURL = source.thumbnailImageUrl();
        result.DeliveryCharges = source.deliveryCharges();
        result.XeroAccessCode = source.xeroAccessCode();
        result.OrganisationId = source.organisationId();
        result.CarrierId = source.carrierId();
        result.DeliveryServiceType = source.deliveryServiceType();
        result.ImageBytes = source.costcentreImageFileBinary() === undefined ? null : source.costcentreImageFileBinary();

        result.TimeSourceType = source.isTimeVariable() === '1' ? 1 : 2;
        result.QuantitySourceType = source.isQtyVariable() === '1' ? 1 : 2;

        result.CostcentreInstructions = [];
        _.each(source.costCenterInstructions(), function (item) {
            result.CostcentreInstructions.push(costCenterInstruction.CreateFromClientModel(item));
        });
        return result;
    };

    var MCQsAnswer = function (source) {
        var self
        if (source != undefined) {
            Id = ko.observable(source.Id),
            QuestionId = ko.observable(source.QuestionId),
            AnswerString = ko.observable(source.AnswerString)


        } else {
            Id = ko.observable(),
            QuestionId = ko.observable(),
            AnswerString = ko.observable()
        }

        errors = ko.validation.group({
        }),
        // Is Valid
       isValid = ko.computed(function () {
           return errors().length === 0;
       }),
       dirtyFlag = new ko.dirtyFlag({
           Id: Id,
           QuestionId: QuestionId,
           AnswerString: AnswerString

       }),
        // Has Changes
       hasChanges = ko.computed(function () {
           return dirtyFlag.isDirty();
       }),
        // Reset
       reset = function () {
           dirtyFlag.reset();
       };

        self = {
            Id: Id,
            QuestionId: QuestionId,
            AnswerString: AnswerString,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,

        };
        return self;

    }
    var QuestionVariable = function (source, sourceMCQsAnswer) {
        var self
        if (source != undefined) {
            Id = ko.observable(source.Id),
            QuestionString = ko.observable(source.QuestionString),
            Type = ko.observable(source.Type),
            DefaultAnswer = ko.observable(source.DefaultAnswer),
            CompanyId = ko.observable(source.CompanyId),
            SystemSiteId = ko.observable(source.SystemSiteId),
            VariableString = ko.computed(function () {
                return "{question, ID=&quot;" + Id() + "&quot;,caption=&quot;" + QuestionString() + "&quot;}";
                
            }, this),
            QuestionVariableMCQsAnswer = ko.observableArray([])
            if (sourceMCQsAnswer != null) {
                _.each(sourceMCQsAnswer, function (item) {
                    QuestionVariableMCQsAnswer.push(MCQsAnswer(item));
                });

            }


        } else {
            Id = ko.observable(),
            QuestionString = ko.observable(),
            Type = ko.observable(),
            DefaultAnswer = ko.observable(),
            CompanyId = ko.observable(),
            SystemSiteId = ko.observable(),
            VariableString = ko.computed(function () {
                return "{question, ID=&quot;" + Id() + "&quot;,caption=&quot;" + QuestionString() + "&quot;}";

            }, this),
            QuestionVariableMCQsAnswer = ko.observableArray([])
        }

        errors = ko.validation.group({
        }),
        // Is Valid
       isValid = ko.computed(function () {
           return errors().length === 0;
       }),
       dirtyFlag = new ko.dirtyFlag({
           Id :Id,
           QuestionString :QuestionString,
           Type :Type,
           DefaultAnswer :DefaultAnswer,
           CompanyId :CompanyId,
           SystemSiteId :SystemSiteId,
           VariableString :VariableString,
           QuestionVariableMCQsAnswer:QuestionVariableMCQsAnswer

       }),
        // Has Changes
       hasChanges = ko.computed(function () {
           return dirtyFlag.isDirty();
       }),
        // Reset
       reset = function () {
           dirtyFlag.reset();
       };

        self = {
            Id :Id,
            QuestionString :QuestionString,
            Type :Type,
            DefaultAnswer :DefaultAnswer,
            CompanyId :CompanyId,
            SystemSiteId :SystemSiteId,
            VariableString :VariableString,
            QuestionVariableMCQsAnswer:QuestionVariableMCQsAnswer,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
           

        };
        return self;

    }
    var MCQsAnswerServerMapper = function (source) {
        var MCQsAnswer= {}
        MCQsAnswer.Id = source.Id();
        MCQsAnswer.QuestionId = source.QuestionId();
        MCQsAnswer.AnswerString = source.AnswerString();
       
        return MCQsAnswer;

    }
    var MCQsAnswerClientMapper = function (source) {
        var oMCQsAnswer = new MCQsAnswer()
        oMCQsAnswer.Id(source.Id);
        oMCQsAnswer.QuestionId(source.QuestionId);
        oMCQsAnswer.AnswerString(source.AnswerString);

        return oMCQsAnswer;

    }
    var QuestionVariableServerMapper = function (source) {
        var QuestionVariable = {};
        if (source != undefined) {
            QuestionVariable.Id = source.Id();
            QuestionVariable.QuestionString = source.QuestionString();
            QuestionVariable.Type = source.Type();
            QuestionVariable.DefaultAnswer = source.DefaultAnswer();
            QuestionVariable.CompanyId = source.CompanyId();
            QuestionVariable.SystemSiteId = source.SystemSiteId();
            QuestionVariable.VariableString = source.VariableString();
        }
        var QuestionVariableMCQs = [];
        if (source.QuestionVariableMCQsAnswer() != undefined) {
            _.each(source.QuestionVariableMCQsAnswer(), function (item) {
                var MCQsAnswer = MCQsAnswerServerMapper(item);
                QuestionVariableMCQs.push(MCQsAnswer);
            });


        }
        return {
            Question: QuestionVariable,
            Answer: QuestionVariableMCQs
        }
      

    }
    var QuestionVariableClientMapper = function (source, oQuestionVariableMCQsAnswer) {
        var oQuestionVariable = new QuestionVariable();
        if (source != undefined) {
            oQuestionVariable.Id(source.Id());
            oQuestionVariable.QuestionString(source.QuestionString());
            oQuestionVariable.Type(source.Type());
            oQuestionVariable.DefaultAnswer(source.DefaultAnswer());
            oQuestionVariable.CompanyId(source.CompanyId());
            oQuestionVariable.SystemSiteId(source.SystemSiteId());
        
        }
       
        oQuestionVariable.QuestionVariableMCQsAnswer.removeAll();
        if (oQuestionVariableMCQsAnswer != undefined) {
            _.each(oQuestionVariableMCQsAnswer, function (item) {
                var MCQsAnswer = MCQsAnswerClientMapper(item);
                oQuestionVariable.QuestionVariableMCQsAnswer.push(MCQsAnswer);
            });


        }
        return oQuestionVariable;


    }
    var matrixVariable = function (source, sourceMatrixDetail) {
        var self
        if (source != undefined) {
            MatrixId = ko.observable(source.MatrixId),
            Name = ko.observable(source.Name),
            Description = ko.observable(source.Description),
            RowsCount = ko.observable(source.RowsCount),
            ColumnsCount = ko.observable(source.ColumnsCount),
            OrganisationId = ko.observable(source.OrganisationId),
            SystemSiteId = ko.observable(source.SystemSiteId),
            VariableString = ko.computed(function () {
                return "{matrix, ID=&quot;" + MatrixId() + "&quot;,Name=&quot;" + Name() + "&quot;}";
            }, this),
            row = ko.observableArray([]),
            MatrixDetailVariableList = ko.observableArray([])
            if (sourceMatrixDetail != null) {
                _.each(sourceMatrixDetail, function (item) {
                    MatrixDetailVariableList.push(MatrixDetail(item)); // write MatrixDetail
                });

            }


        } else {
            MatrixId = ko.observable(),
            Name = ko.observable(),
            Description = ko.observable(),
            RowsCount = ko.observable(),
            ColumnsCount = ko.observable(),
            OrganisationId = ko.observable(),
            SystemSiteId = ko.observable(),
            
            VariableString = ko.computed(function () {
                return "{matrix, ID=&quot;" + MatrixId() + "&quot;,Name=&quot;" + Name() + "&quot;}";
            }, this),
            row = ko.observableArray([]),
            MatrixDetailVariableList = ko.observableArray([])
        }

        errors = ko.validation.group({
        }),
        // Is Valid
       isValid = ko.computed(function () {
           return errors().length === 0;
       }),
       dirtyFlag = new ko.dirtyFlag({
           MatrixId : MatrixId,
           Name : Name,
           Description : Description,
           RowsCount : RowsCount,
           ColumnsCount : ColumnsCount,
           OrganisationId : OrganisationId,
           SystemSiteId : SystemSiteId,
           VariableString: VariableString,
           MatrixDetailVariableList: MatrixDetailVariableList

       }),
        // Has Changes
       hasChanges = ko.computed(function () {
           return dirtyFlag.isDirty();
       }),
        // Reset
       reset = function () {
           dirtyFlag.reset();
       };

        self = {
            MatrixId : MatrixId,
            Name : Name,
            Description : Description,
            RowsCount : RowsCount,
            ColumnsCount : ColumnsCount,
            OrganisationId : OrganisationId,
            SystemSiteId : SystemSiteId,
            VariableString: VariableString,
            row:row,
            MatrixDetailVariableList:MatrixDetailVariableList,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,


        };
        return self;

    }
    var MatrixDetail = function (source) {
        var self
        if (source != undefined) {
            Id = ko.observable(source.Id),
            MatrixId = ko.observable(source.MatrixId),
            Value = ko.observable(source.Value)
        } else {
            Id = ko.observable(),
            MatrixId = ko.observable(),
            Value = ko.observable()
        }

        errors = ko.validation.group({
        }),
        // Is Valid
       isValid = ko.computed(function () {
           return errors().length === 0;
       }),
       dirtyFlag = new ko.dirtyFlag({
           Id : Id,
           MatrixId: MatrixId,
           Value: Value

       }),
        // Has Changes
       hasChanges = ko.computed(function () {
           return dirtyFlag.isDirty();
       }),
        // Reset
       reset = function () {
           dirtyFlag.reset();
       };

        self = {
            Id : Id,
            MatrixId: MatrixId,
            Value: Value,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,

        };
        return self;

    }
    var MatrixDetailClientMapper = function (source) {
        var oMatrixDetail = new MatrixDetail()
        if (source != null) {
            oMatrixDetail.Id(source.Id);
            oMatrixDetail.MatrixId(source.MatrixId);
            oMatrixDetail.Value(source.Value);
        }
        
        return oMatrixDetail;
    }
    var MatrixVariableClientMapper = function (source, sourceMatrixDetail) {
        var omatrixVariable = new matrixVariable();
        if (source != undefined) {
            omatrixVariable.MatrixId(source.MatrixId());
            omatrixVariable.Name(source.Name());
            omatrixVariable.Description(source.Description());
            omatrixVariable.RowsCount(source.RowsCount());
            omatrixVariable.ColumnsCount(source.ColumnsCount());
            omatrixVariable.OrganisationId(source.OrganisationId());
            omatrixVariable.SystemSiteId(source.SystemSiteId());
        }

        omatrixVariable.MatrixDetailVariableList.removeAll();
        var MatrixDetailList = [];
        if (sourceMatrixDetail != undefined) {
            _.each(sourceMatrixDetail, function (item) {
                MatrixDetailList.push(MatrixDetailClientMapper(item));
            });
            var k = 0 ;
            omatrixVariable.row.removeAll();
            for (var i = 0; i < source.RowsCount() ; i++) {
                var rowsTem = ko.observableArray([]);
                for (var j = 0; j < source.ColumnsCount() ; j++) {
                    if (i == 0 && j == 0) {
                        var row = MatrixDetailClientMapper();
                        row.Id(-1);
                        rowsTem.push(row);
                    } else {
                        rowsTem.push(MatrixDetailList[k]);
                        k++;
                    }
                   
                }
                omatrixVariable.MatrixDetailVariableList.push(rowsTem);
                
            }

        }
        return omatrixVariable;


    }
    var MatrixDetailServerMapper = function (source) {
        var oMatrixDetail = {}
        if (source != undefined) {
            oMatrixDetail.Id = source.Id();
            oMatrixDetail.MatrixId = source.MatrixId();
            oMatrixDetail.Value = source.Value();
        }
       

        return oMatrixDetail;

    }
    var MatrixVariableServerMapper = function (source) {
        var CostCentreMatrix = {};
        if (source != undefined) {
            MatrixDetailVariableList: MatrixDetailVariableList


            CostCentreMatrix.MatrixId = source.MatrixId();
            CostCentreMatrix.Name = source.Name();
            CostCentreMatrix.Description = source.Description();
            CostCentreMatrix.RowsCount = source.RowsCount();
            CostCentreMatrix.ColumnsCount = source.ColumnsCount();
            CostCentreMatrix.SystemSiteId = source.SystemSiteId();
            CostCentreMatrix.VariableString = source.VariableString();
        }
        var MatrixDetailList = [];
        if (source.MatrixDetailVariableList() != undefined) {
            _.each(source.MatrixDetailVariableList(), function (item) {
                _.each(item(), function (itemDetails) {
                    if (itemDetails.Id() != -1) {
                        var oItemDetails = MatrixDetailServerMapper(itemDetails);
                        MatrixDetailList.push(oItemDetails);
                    }
                    
                });
                
                
            });


        }
        return {
            CostCentreMatrix: CostCentreMatrix,
            CostCentreMatrixDetail: MatrixDetailList
        }


    }

    var StockItemVariable = function (item) {
        var self
        if (item != null && item != undefined) {
            Id = ko.observable(item.id),
            StockName = ko.observable(item.name)
        } else {
            Id = ko.observable(),
            StockName = ko.observable()
        }
           
        CostType = ko.observable(),
        quantityValue = ko.observable(),
        QuantityType = ko.observable(),
        variableValue = ko.observable(),
        questionValue = ko.observable(),
        // SystemVariable = ko.observable(),
        // QuantityQuestion = ko.observable(),
      Value = ko.computed(function () {
          if (QuantityType() == "qty") {
              return quantityValue();
          }else if (QuantityType() == "variable") {
              return variableValue();
          }else if (QuantityType() == "question") {
              return questionValue();
          } else {
              return 0;
          }
               
      }, this),
        VariableString = ko.computed(function () {
            return "{stock, ID=&quot;" + Id() + "&quot;,name=&quot;" + StockName() + "&quot;,type=&quot;" + CostType() + "&quot;,qtytype=&quot;" + QuantityType() + "&quot;,value=&quot;" + Value() + "&quot;}";
        }, this),
           
   
    errors = ko.validation.group({
    }),
        // Is Valid
   isValid = ko.computed(function () {
       return errors().length === 0;
   }),
   dirtyFlag = new ko.dirtyFlag({
         

   }),
        // Has Changes
   hasChanges = ko.computed(function () {
       return dirtyFlag.isDirty();
   }),
        // Reset
   reset = function () {
       dirtyFlag.reset();
   };

        self = {
            Id: Id,
            StockName: StockName,
            CostType: CostType,
            quantityValue: quantityValue,
            QuantityType: QuantityType,
            variableValue: variableValue,
            questionValue: questionValue,
            Value: Value,
            VariableString:VariableString,
            // SystemVariable: SystemVariable,
            //QuantityQuestion:QuantityQuestion,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,


        };
        return self;

    },
    MachineClickChargeZone = function (source) {
        var self
        if (source != undefined) {
            Id = ko.observable(source != undefined ? source.Id : undefined),
            MethodId = ko.observable(source.MethodId),
            From1 = ko.observable(source.From1),
            To1 = ko.observable(source.To1),
            Sheets1 = ko.observable(source.Sheets1),
            SheetCost1 = ko.observable(source.SheetCost1),
            SheetPrice1 = ko.observable(source.SheetPrice1),
            From2 = ko.observable(source.From2),
            To2 = ko.observable(source.To2).extend({ number: true }),
            Sheets2 = ko.observable(source.Sheets2),
            SheetCost2 = ko.observable(source.SheetCost2),
            SheetPrice2 = ko.observable(source.SheetPrice2),
            From3 = ko.observable(source.From3),
            To3 = ko.observable(source.To3),
            Sheets3 = ko.observable(source.Sheets3),
            SheetCost3 = ko.observable(source.SheetCost3),
            SheetPrice3 = ko.observable(source.SheetPrice3),
            From4 = ko.observable(source.From4),
            To4 = ko.observable(source.To4),
            Sheets4 = ko.observable(source.Sheets4),
            SheetCost4 = ko.observable(source.SheetCost4),
            SheetPrice4 = ko.observable(source.SheetPrice4),
            From5 = ko.observable(source.From5),
            To5 = ko.observable(source.To5),
            Sheets5 = ko.observable(source.Sheets5),
            SheetCost5 = ko.observable(source.SheetCost5),
            SheetPrice5 = ko.observable(source.SheetPrice5),
            From6 = ko.observable(source.From6),
            To6 = ko.observable(source.To6),
            Sheets6 = ko.observable(source.Sheets6),
            SheetCost6 = ko.observable(source.SheetCost6),
            SheetPrice6 = ko.observable(source.SheetPrice6),
            From7 = ko.observable(source.From7),
            To7 = ko.observable(source.To7),
            Sheets7 = ko.observable(source.Sheets7),
            SheetCost7 = ko.observable(source.SheetCost7),
            SheetPrice7 = ko.observable(source.SheetPrice7),
            From8 = ko.observable(source.From8),
            To8 = ko.observable(source.To8),
            Sheets8 = ko.observable(source.Sheets8),
            SheetCost8 = ko.observable(source.SheetCost8),
            SheetPrice8 = ko.observable(source.SheetPrice8),
            From9 = ko.observable(source.From9),
            To9 = ko.observable(source.To9),
            Sheets9 = ko.observable(source.Sheets9),
            SheetCost9 = ko.observable(source.SheetCost9),
            SheetPrice9 = ko.observable(source.SheetPrice9),
            From10 = ko.observable(source.From10),
            To10 = ko.observable(source.To10),
            Sheets10 = ko.observable(source.Sheets10),
            SheetCost10 = ko.observable(source.SheetCost10),
            SheetPrice10 = ko.observable(source.SheetPrice10),
            From11 = ko.observable(source.From11),
            To11 = ko.observable(source.To11),
            Sheets11 = ko.observable(source.Sheets11),
            SheetCost11 = ko.observable(source.SheetCost11),
            SheetPrice11 = ko.observable(source.SheetPrice11),
            From12 = ko.observable(source.From12),
            To12 = ko.observable(source.To12),
            Sheets12 = ko.observable(source.Sheets12),
            SheetCost12 = ko.observable(source.SheetCost12),
            SheetPrice12 = ko.observable(source.SheetPrice12),
            From13 = ko.observable(source.From13),
            To13 = ko.observable(source.To13),
            Sheets13 = ko.observable(source.Sheets13),
            SheetCost13 = ko.observable(source.SheetCost13),
            SheetPrice13 = ko.observable(source.SheetPrice13),
            From14 = ko.observable(source.From14),
            To14 = ko.observable(source.To14),
            Sheets14 = ko.observable(source.Sheets14),
            SheetCost14 = ko.observable(source.SheetCost14),
            SheetPrice14 = ko.observable(source.SheetPrice14),
            From15 = ko.observable(source.From15),
            To15 = ko.observable(source.To15),
            Sheets15 = ko.observable(source.Sheets15),
            SheetCost15 = ko.observable(source.SheetCost15),
            SheetPrice15 = ko.observable(source.SheetPrice15),
            isaccumulativecharge = ko.observable(source.isaccumulativecharge),
            IsRoundUp = ko.observable(source.IsRoundUp),
            TimePerHour = ko.observable(source.TimePerHour),
            ZoneName = ko.observable(source.ZoneName).extend({ required: true, message: 'Zone title is requird' }),
            VariableString = ko.computed(function() {
                return "{clickchargezone, ID=&quot;" + Id() + "&quot;,caption=&quot;" + ZoneName() + "&quot;}";

            }, this)
        } else {
            Id = ko.observable(),
            MethodId = ko.observable(),
            From1 = ko.observable(0),
            To1 = ko.observable(5000),
            Sheets1 = ko.observable(1000),
            SheetCost1 = ko.observable(22),
            SheetPrice1 = ko.observable(22),
            From2 = ko.observable(5001),
            To2 = ko.observable(10000),
            Sheets2 = ko.observable(1000),
            SheetCost2 = ko.observable(21),
            SheetPrice2 = ko.observable(21),
            From3 = ko.observable(10001),
            To3 = ko.observable(20000),
            Sheets3 = ko.observable(1000),
            SheetCost3 = ko.observable(20),
            SheetPrice3 = ko.observable(20),
            From4 = ko.observable(20001),
            To4 = ko.observable(30000),
            Sheets4 = ko.observable(1000),
            SheetCost4 = ko.observable(19),
            SheetPrice4 = ko.observable(19),
            From5 = ko.observable(30001),
            To5 = ko.observable(40000),
            Sheets5 = ko.observable(1000),
            SheetCost5 = ko.observable(18),
            SheetPrice5 = ko.observable(18),
            From6 = ko.observable(40001),
            To6 = ko.observable(50000),
            Sheets6 = ko.observable(1000),
            SheetCost6 = ko.observable(17),
            SheetPrice6 = ko.observable(17),
            From7 = ko.observable(50001),
            To7 = ko.observable(60000),
            Sheets7 = ko.observable(1000),
            SheetCost7 = ko.observable(16),
            SheetPrice7 = ko.observable(16),
            From8 = ko.observable(60001),
            To8 = ko.observable(70000),
            Sheets8 = ko.observable(1000),
            SheetCost8 = ko.observable(15),
            SheetPrice8 = ko.observable(15),
            From9 = ko.observable(70001),
            To9 = ko.observable(80000),
            Sheets9 = ko.observable(1000),
            SheetCost9 = ko.observable(14),
            SheetPrice9 = ko.observable(14),
            From10 = ko.observable(80001),
            To10 = ko.observable(90000),
            Sheets10 = ko.observable(1000),
            SheetCost10 = ko.observable(13),
            SheetPrice10 = ko.observable(13),
            From11 = ko.observable(90001),
            To11 = ko.observable(100000),
            Sheets11 = ko.observable(1000),
            SheetCost11 = ko.observable(12),
            SheetPrice11 = ko.observable(12),
            From12 = ko.observable(100001),
            To12 = ko.observable(110000),
            Sheets12 = ko.observable(1000),
            SheetCost12 = ko.observable(11),
            SheetPrice12 = ko.observable(11),
            From13 = ko.observable(110001),
            To13 = ko.observable(120000),
            Sheets13 = ko.observable(1000),
            SheetCost13 = ko.observable(10),
            SheetPrice13 = ko.observable(10),
            From14 = ko.observable(120001),
            To14 = ko.observable(130000),
            Sheets14 = ko.observable(1000),
            SheetCost14 = ko.observable(9),
            SheetPrice14 = ko.observable(9),
            From15 = ko.observable(130001),
            To15 = ko.observable(140000),
            Sheets15 = ko.observable(1000),
            SheetCost15 = ko.observable(8),
            SheetPrice15 = ko.observable(8),
            isaccumulativecharge = ko.observable(),
            IsRoundUp = ko.observable(),
            TimePerHour = ko.observable(),
            ZoneName = ko.observable('Default Zone').extend({ required: true, message: 'Zone title is requird' }),
            VariableString = ko.computed(function () {
                return "{clickchargezone,caption=&quot;" + ZoneName() + "&quot;}";

            }, this)
        }

        errors = ko.validation.group({
            ZoneName: ZoneName
        }),
            // Is Valid
        isValid = ko.computed(function() {
            return errors().length === 0;
        }),
        dirtyFlag = new ko.dirtyFlag({
            Id: Id,
            MethodId: MethodId,
            From1: From1,
            To1: To1,
            Sheets1: Sheets1,
            SheetCost1: SheetCost1,
            SheetPrice1: SheetPrice1,
            From2: From2,
            To2: To2,
            Sheets2: Sheets2,
            SheetCost2: SheetCost2,
            SheetPrice2: SheetPrice2,
            From3: From3,
            To3: To3,
            Sheets3: Sheets3,
            SheetCost3: SheetCost3,
            SheetPrice3: SheetPrice3,
            From4: From4,
            To4: To4,
            Sheets4: Sheets4,
            SheetCost4: SheetCost4,
            SheetPrice4: SheetPrice4,
            From5: From5,
            To5: To5,
            Sheets5: Sheets5,
            SheetCost5: SheetCost5,
            SheetPrice5: SheetPrice5,
            From6: From6,
            To6: To6,
            Sheets6: Sheets6,
            SheetCost6: SheetCost6,
            SheetPrice6: SheetPrice6,
            From7: From7,
            To7: To7,
            Sheets7: Sheets7,
            SheetCost7: SheetCost7,
            SheetPrice7: SheetPrice7,
            From8: From8,
            To8: To8,
            Sheets8: Sheets8,
            SheetCost8: SheetCost8,
            SheetPrice8: SheetPrice8,
            From9: From9,
            To9: To9,
            Sheets9: Sheets9,
            SheetCost9: SheetCost9,
            SheetPrice9: SheetPrice9,
            From10: From10,
            To10: To10,
            Sheets10: Sheets10,
            SheetCost10: SheetCost10,
            SheetPrice10: SheetPrice10,
            From11: From11,
            To11: To11,
            Sheets11: Sheets11,
            SheetCost11: SheetCost11,
            SheetPrice11: SheetPrice11,
            From12: From12,
            To12: To12,
            Sheets12: Sheets12,
            SheetCost12: SheetCost12,
            SheetPrice12: SheetPrice12,
            From13: From13,
            To13: To13,
            Sheets13: Sheets13,
            SheetCost13: SheetCost13,
            SheetPrice13: SheetPrice13,
            From14: From14,
            To14: To14,
            Sheets14: Sheets14,
            SheetCost14: SheetCost14,
            SheetPrice14: SheetPrice14,
            From15: From15,
            To15: To15,
            Sheets15: Sheets15,
            SheetCost15: SheetCost15,
            SheetPrice15: SheetPrice15,
            isaccumulativecharge: isaccumulativecharge,
            IsRoundUp: IsRoundUp,
            TimePerHour: TimePerHour,
            ZoneName: ZoneName
        }),
            // Has Changes
        hasZoneChanges = ko.computed(function() {
            return dirtyFlag.isDirty();
        }),
            // Reset
        reset = function() {
            dirtyFlag.reset();
        };
       

        self = {
            Id: Id,
            MethodId: MethodId,
            From1: From1,
            To1: To1,
            Sheets1: Sheets1,
            SheetCost1: SheetCost1,
            SheetPrice1: SheetPrice1,
            From2: From2,
            To2: To2,
            Sheets2: Sheets2,
            SheetCost2: SheetCost2,
            SheetPrice2: SheetPrice2,
            From3: From3,
            To3: To3,
            Sheets3: Sheets3,
            SheetCost3: SheetCost3,
            SheetPrice3: SheetPrice3,
            From4: From4,
            To4: To4,
            Sheets4: Sheets4,
            SheetCost4: SheetCost4,
            SheetPrice4: SheetPrice4,
            From5: From5,
            To5: To5,
            Sheets5: Sheets5,
            SheetCost5: SheetCost5,
            SheetPrice5: SheetPrice5,
            From6: From6,
            To6: To6,
            Sheets6: Sheets6,
            SheetCost6: SheetCost6,
            SheetPrice6: SheetPrice6,
            From7: From7,
            To7: To7,
            Sheets7: Sheets7,
            SheetCost7: SheetCost7,
            SheetPrice7: SheetPrice7,
            From8: From8,
            To8: To8,
            Sheets8: Sheets8,
            SheetCost8: SheetCost8,
            SheetPrice8: SheetPrice8,
            From9: From9,
            To9: To9,
            Sheets9: Sheets9,
            SheetCost9: SheetCost9,
            SheetPrice9: SheetPrice9,
            From10: From10,
            To10: To10,
            Sheets10: Sheets10,
            SheetCost10: SheetCost10,
            SheetPrice10: SheetPrice10,
            From11: From11,
            To11: To11,
            Sheets11: Sheets11,
            SheetCost11: SheetCost11,
            SheetPrice11: SheetPrice11,
            From12: From12,
            To12: To12,
            Sheets12: Sheets12,
            SheetCost12: SheetCost12,
            SheetPrice12: SheetPrice12,
            From13: From13,
            To13: To13,
            Sheets13: Sheets13,
            SheetCost13: SheetCost13,
            SheetPrice13: SheetPrice13,
            From14: From14,
            To14: To14,
            Sheets14: Sheets14,
            SheetCost14: SheetCost14,
            SheetPrice14: SheetPrice14,
            From15: From15,
            To15: To15,
            Sheets15: Sheets15,
            SheetCost15: SheetCost15,
            SheetPrice15: SheetPrice15,
            isaccumulativecharge: isaccumulativecharge,
            IsRoundUp: IsRoundUp,
            TimePerHour: TimePerHour,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasZoneChanges: hasZoneChanges,
            reset: reset,
            ZoneName: ZoneName,
            VariableString: VariableString
           
        };
        return self;
    },
        ClickChargeZoneServerMapper = function (source) {
            return {
                Id: source.Id(),
                MethodId: source.MethodId(),
                From1: source.From1(),
                To1: source.To1(),
                Sheets1: source.Sheets1(),
                SheetCost1: source.SheetCost1(),
                SheetPrice1: source.SheetPrice1(),
                From2: source.From2(),
                To2: source.To2(),
                Sheets2: source.Sheets2(),
                SheetCost2: source.SheetCost2(),
                SheetPrice2: source.SheetPrice2(),
                From3: source.From3(),
                To3: source.To3(),
                Sheets3: source.Sheets3(),
                SheetCost3: source.SheetCost3(),
                SheetPrice3: source.SheetPrice3(),
                From4: source.From4(),
                To4: source.To4(),
                Sheets4: source.Sheets4(),
                SheetCost4: source.SheetCost4(),
                SheetPrice4: source.SheetPrice4(),
                From5: source.From5(),
                To5: source.To5(),
                Sheets5: source.Sheets5(),
                SheetCost5: source.SheetCost5(),
                SheetPrice5: source.SheetPrice5(),
                From6: source.From6(),
                To6: source.To6(),
                Sheets6: source.Sheets6(),
                SheetCost6: source.SheetCost6(),
                SheetPrice6: source.SheetPrice6(),
                From7: source.From7(),
                To7: source.To7(),
                Sheets7: source.Sheets7(),
                SheetCost7: source.SheetCost7(),
                SheetPrice7: source.SheetPrice7(),
                From8: source.From8(),
                To8: source.To8(),
                Sheets8: source.Sheets8(),
                SheetCost8: source.SheetCost8(),
                SheetPrice8: source.SheetPrice8(),
                From9: source.From9(),
                To9: source.To9(),
                Sheets9: source.Sheets9(),
                SheetCost9: source.SheetCost9(),
                SheetPrice9: source.SheetPrice9(),
                From10: source.From10(),
                To10: source.To10(),
                Sheets10: source.Sheets10(),
                SheetCost10: source.SheetCost10(),
                SheetPrice10: source.SheetPrice10(),
                From11: source.From11(),
                To11: source.To11(),
                Sheets11: source.Sheets11(),
                SheetCost11: source.SheetCost11(),
                SheetPrice11: source.SheetPrice11(),
                From12: source.From12(),
                To12: source.To12(),
                Sheets12: source.Sheets12(),
                SheetCost12: source.SheetCost12(),
                SheetPrice12: source.SheetPrice12(),
                From13: source.From13(),
                To13: source.To13(),
                Sheets13: source.Sheets13(),
                SheetCost13: source.SheetCost13(),
                SheetPrice13: source.SheetPrice13(),
                From14: source.From14(),
                To14: source.To14(),
                Sheets14: source.Sheets14(),
                SheetCost14: source.SheetCost14(),
                SheetPrice14: source.SheetPrice14(),
                From15: source.From15(),
                To15: source.To15(),
                Sheets15: source.Sheets15(),
                SheetCost15: source.SheetCost15(),
                SheetPrice15: source.SheetPrice15(),
                isaccumulativecharge: source.isaccumulativecharge(),
                IsRoundUp: source.IsRoundUp(),
                TimePerHour: source.TimePerHour(),
                ZoneName: source.ZoneName()
            };
        };

    return {
        CostCenter: CostCenter,
        costCenterClientMapper: costCenterClientMapper,
        costCenterServerMapper: costCenterServerMapper,
        costCenterListView: costCenterListView,
        NewCostCenterInstruction: NewCostCenterInstruction,
        NewInstructionChoice: NewInstructionChoice,
        MCQsAnswer: MCQsAnswer,
        QuestionVariable: QuestionVariable,
        QuestionVariableServerMapper: QuestionVariableServerMapper,
        QuestionVariableClientMapper: QuestionVariableClientMapper,
        matrixVariable: matrixVariable,
        MatrixDetail: MatrixDetail,
        MatrixVariableClientMapper: MatrixVariableClientMapper,
        MatrixDetailClientMapper: MatrixDetailClientMapper,
        MatrixVariableServerMapper: MatrixVariableServerMapper,
        StockItemVariable: StockItemVariable,
        MachineClickChargeZone: MachineClickChargeZone,
        ClickChargeZoneServerMapper: ClickChargeZoneServerMapper
    };
});