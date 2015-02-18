﻿define(["ko", "underscore", "underscore-ko"], function(ko) {
    var CostCenter = function() {

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
            minimumCost = ko.observable(),
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
            pricePerUnitQuantity = ko.observable(),
            timePerUnitQuantity = ko.observable(),
            timeRunSpeed = ko.observable(),
            timeNoOfPasses = ko.observable(),
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
            errors = ko.validation.group({
                name: name,
                type: type,
                setupCost: setupCost
            }),
            isValid = ko.computed(function() {
                return errors().length === 0 ? true : false;;
            }),
            dirtyFlag = new ko.dirtyFlag({
                name: name,
                description: description,
                webStoreDesc: webStoreDesc,
                calculationMethodType: calculationMethodType,
                priority:priority,
                type: type,
                costQuestionString: costQuestionString,
                costDefaultValue: costDefaultValue,
                priceQuestionString: priceQuestionString,
                priceDefaultValue: priceDefaultValue,
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
                strTimeUnParsed: strTimeUnParsed

            }),
            hasChanges = ko.computed(function() {
                return dirtyFlag.isDirty();
            }),

            reset = function() {
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
            dirtyFlag: dirtyFlag,
            errors: errors,
            isValid: isValid,
            hasChanges: hasChanges,
            reset: reset,
            serviceTypesList: serviceTypesList,
            deliveryServiceType: deliveryServiceType
        };
        return self;
    };

    costCenterListView = function (specifiedCostCentreId, specifiedName, specifiedDescription, specifiedType, specifiedCalType
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

            convertToServerData = function () {
                return {
                    CostCentreId: costCenterId(),
                }
            };
        self = {
            costCenterId: costCenterId,
            name: name,
            description: description,
            type: type,
            calculationMethodType:calculationMethodType,
            convertToServerData: convertToServerData,
        };
        return self;
    };
  
    costCenterListView.Create = function (source) {
        return new costCenterListView(source.CostCentreId, source.Name, source.Description, source.Type, source.CalculationMethodType);
    };
    //Cost Center Instructions for Client
    costCenterInstruction = function (specifiedInstructionId, specifiedInstruction, specifiedcostCenterOption, specifiedCostCentreId) {
       var
            self,
            instructionId = ko.observable(specifiedInstructionId),
            instruction = ko.observable(specifiedInstruction),
            costCenterOption = ko.observable(specifiedcostCenterOption),
            costCenterId = ko.observable(specifiedCostCentreId),
             dirtyFlag = new ko.dirtyFlag({
                 instruction: instruction
             }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            convertToServerData = function () {
                return {
                    Instruction: instruction(),
                    InstructionId : instructionId(),
                    CostCenterOption: costCenterOption()
                }
            };
        self = {
            instructionId: instructionId,
            instruction: instruction,
            costCenterOption: costCenterOption,
            costCenterId:costCenterId,
            convertToServerData: convertToServerData
        };
        return self;

    };

    costCenterInstruction.CreateFromClientModel = function (source) {
        return new costCenterInstruction(
            source.InstructionId, source.Instruction, source.CostCenterOption
        );
    };
    costCenterInstruction.Create = function (source) {
        var ccInstruction = new costCenterInstruction(
            source.InstructionId,
            source.Instruction,
            source.costCenterOption,
            source.CostCentreId
            );
        return ccInstruction;
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
            dirtyFlag:dirtyFlag,
            convertToServerData: convertToServerData
        };
        return self;

    };

    var costCenterClientMapper = function(source) {
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
        oCostCenter.strCostPlantUnParsed(source.strCostPlantUnParsed);
        oCostCenter.strCostLabourParsed(source.strCostLabourParsed);
        oCostCenter.strCostLabourUnParsed(source.strCostLabourUnParsed);
        oCostCenter.strCostMaterialParsed(source.strCostMaterialParsed);
        oCostCenter.strCostMaterialUnParsed(source.strCostMaterialUnParsed);
        oCostCenter.strPricePlantParsed(source.strPricePlantParsed);
        oCostCenter.strPricePlantUnParsed(source.strPricePlantUnParsed);
        oCostCenter.strPriceLabourParsed(source.strPriceLabourParsed);
        oCostCenter.strPriceLabourUnParsed(source.strPriceLabourUnParsed);
        oCostCenter.strPriceMaterialParsed(source.strPriceMaterialParsed);
        oCostCenter.strPriceMaterialUnParsed(source.strPriceMaterialUnParsed);
        oCostCenter.strActualCostPlantParsed(source.strActualCostPlantParsed);
        oCostCenter.strActualCostPlantUnParsed(source.strActualCostPlantUnParsed);
        oCostCenter.strActualCostLabourParsed(source.strActualCostLabourParsed);
        oCostCenter.strActualCostLabourUnParsed(source.strActualCostLabourUnParsed);
        oCostCenter.strActualCostMaterialParsed(source.strActualCostMaterialParsed);
        oCostCenter.strActualCostMaterialUnParsed(source.strActualCostMaterialUnParsed);
        oCostCenter.strTimeParsed(source.strTimeParsed);
        oCostCenter.strTimeUnParsed(source.strTimeUnParsed);
        oCostCenter.isDisabled(source.IsDisabled);
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
        result.IsDisabled = source.isDisabled();
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
        result.TimeNoOfPasses = source.timeNoOfPasses();
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
        result.IsPrintOnJobCard = source.isPrintOnJobCard();
        result.WebStoreDesc = source.webStoreDesc();
        result.isPublished = source.isPublished();
        result.EstimateProductionTime = source.estimateProductionTime();
        result.MainImageURL = source.mainImageUrl();
        result.ThumbnailImageURL = source.thumbnailImageUrl();
        result.DeliveryCharges = source.deliveryCharges();
        result.XeroAccessCode = source.xeroAccessCode();
        result.OrganisationId = source.organisationId();
        result.DeliveryServiceType = source.deliveryServiceType();
        
        return result;
    };
    
    return {
        CostCenter: CostCenter,
        costCenterClientMapper: costCenterClientMapper,
        costCenterServerMapper: costCenterServerMapper,
        costCenterListView: costCenterListView
    };
});