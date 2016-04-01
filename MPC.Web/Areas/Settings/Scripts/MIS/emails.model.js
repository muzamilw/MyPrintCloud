define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    //Section view Entity
    Campaign = function (specifiedCampaignId, specifiedCampaignName, specifiedDescription, specifiedCampaignType, specifiedIsEnabled, specifiedStartDateTime
        , specifiedIncludeCustomers, specifiedIncludeSuppliers, specifiedIncludeProspects, specifiedIncludeNewsLetterSubscribers, specifiedIncludeFlag, specifiedFlagIDs,
        specifiedCustomerTypeIDs, specifiedGroupIDs, specifiedSubjectA, specifiedHTMLMessageA, specifiedFromName, specifiedFromAddress, specifiedReturnPathAddress,
        specifiedReplyToAddress, specifiedEmailLogFileAddress2, specifiedEmailEvent, specifiedEventName, specifiedSendEmailAfterDays, specifiedIncludeType,
        specifiedIncludeCorporateCustomers, specifiedEnableLogFiles, specifiedEmailLogFileAddress3) {
        var self,
            id = ko.observable(specifiedCampaignId),
            campaignName = ko.observable(specifiedCampaignName).extend({ required: true }),
            description = ko.observable(specifiedDescription),
            campaignType = ko.observable(specifiedCampaignType),
            isEnabled = ko.observable(specifiedIsEnabled),
            startDateTime = ko.observable((specifiedStartDateTime !== undefined && specifiedStartDateTime !== null) ? moment(specifiedStartDateTime, ist.utcFormat).toDate() : undefined).extend({
                required: {
                    message: "Start Date is required",
                    onlyIf: function () {
                        return campaignType() === 3;
                    }
                }
            }),
        includeCustomers = ko.observable(specifiedIncludeCustomers),
        includeSuppliers = ko.observable(specifiedIncludeSuppliers),
        includeProspects = ko.observable(specifiedIncludeProspects),
        includeNewsLetterSubscribers = ko.observable(specifiedIncludeNewsLetterSubscribers),
        includeFlag = ko.observable(specifiedIncludeFlag),
        flagIDs = ko.observable(specifiedFlagIDs),
        customerTypeIDs = ko.observable(specifiedCustomerTypeIDs),
        groupIDs = ko.observable(specifiedGroupIDs),
        subjectA = ko.observable(specifiedSubjectA).extend({ required: true }),
        hTMLMessageA = ko.observable(specifiedHTMLMessageA),
        fromName = ko.observable(specifiedFromName),
        fromAddress = ko.observable(specifiedFromAddress),
        returnPathAddress = ko.observable(specifiedReturnPathAddress),
        replyToAddress = ko.observable(specifiedReplyToAddress),
        emailLogFileAddress2 = ko.observable(specifiedEmailLogFileAddress2).extend({ email: true }),
        emailEventId = ko.observable(specifiedEmailEvent),
        eventName = ko.observable(specifiedEventName),
        sendEmailAfterDays = ko.observable(specifiedSendEmailAfterDays).extend({ number: true }),
        campaignImages = ko.observableArray([]),
        includeType = ko.observable(specifiedIncludeType),
        includeCorporateCustomers = ko.observable(specifiedIncludeCorporateCustomers),
        enableLogFiles = ko.observable(specifiedEnableLogFiles),
        isEditorDirty = ko.observable(),
        emailLogFileAddress3 = ko.observable(specifiedEmailLogFileAddress3).extend({ email: true }),
        // Errors
        errors = ko.validation.group({
            campaignName: campaignName,
            subjectA: subjectA
        }),
        // Is Valid 
        isValid = ko.computed(function () {
            return errors().length === 0 ? true : false;
        }),

        // ReSharper disable InconsistentNaming
        dirtyFlag = new ko.dirtyFlag({
            campaignName: campaignName,
            description: description,
            campaignType: campaignType,
            isEnabled: isEnabled,
            subjectA: subjectA,
            hTMLMessageA: hTMLMessageA,
            fromName: fromName,
            fromAddress: fromAddress,
            returnPathAddress: returnPathAddress,
            replyToAddress: replyToAddress,
            emailLogFileAddress2: emailLogFileAddress2,
            emailEventId: emailEventId,
            campaignImages: campaignImages,
            isEditorDirty: isEditorDirty
        }),
        // Has Changes
        hasChanges = ko.computed(function () {
            return dirtyFlag.isDirty();
        }),
        //Convert To Server
        convertToServerData = function (source) {
            var result = {};
            result.CampaignId = source.id() === undefined ? 0 : source.id();
            result.CampaignName = source.campaignName() === undefined ? null : source.campaignName();
            result.Description = source.description() === undefined ? null : source.description();
            result.CampaignType = source.campaignType() === undefined ? null : source.campaignType();
            result.IsEnabled = source.isEnabled() === undefined ? false : source.isEnabled();
            result.StartDateTime = (startDateTime() === undefined || startDateTime() === null) ? null : moment(startDateTime()).format(ist.utcFormat);
            result.IncludeCustomers = (source.includeCustomers() === undefined || source.includeCustomers() === null) ? false : source.includeCustomers();
            result.IncludeSuppliers = source.includeSuppliers() === undefined ? false : source.includeSuppliers();
            result.IncludeProspects = source.includeProspects() === undefined ? false : source.includeProspects();
            result.IncludeNewsLetterSubscribers = source.includeNewsLetterSubscribers() === undefined ? false : source.includeNewsLetterSubscribers();
            result.IncludeFlag = source.includeFlag() === undefined ? null : source.includeFlag();
            result.FlagIDs = source.flagIDs() === undefined ? null : source.flagIDs();
            result.CustomerTypeIDs = source.customerTypeIDs() === undefined ? null : source.customerTypeIDs();
            result.GroupIDs = source.groupIDs() === undefined ? null : source.groupIDs();
            result.SubjectA = source.subjectA() === undefined ? null : source.subjectA();
            result.HTMLMessageA = source.hTMLMessageA() === undefined ? null : source.hTMLMessageA();
            result.FromName = source.fromName() === undefined ? null : source.fromName();
            result.FromAddress = source.fromAddress() === undefined ? null : source.fromAddress();
            result.ReturnPathAddress = source.returnPathAddress() === undefined ? null : source.returnPathAddress();
            result.ReplyToAddress = source.replyToAddress() === undefined ? null : source.replyToAddress();
            result.EmailLogFileAddress2 = source.emailLogFileAddress2() === undefined ? null : source.emailLogFileAddress2();
            result.EmailEvent = source.emailEventId() === undefined ? null : source.emailEventId();
            result.SendEmailAfterDays = source.sendEmailAfterDays() === undefined ? null : source.sendEmailAfterDays();
            result.IncludeType = source.includeType() === undefined ? false : source.includeType();
            result.IncludeCorporateCustomers = source.includeCorporateCustomers() === undefined ? false : source.includeCorporateCustomers();
            result.EnableLogFiles = source.enableLogFiles() === undefined ? false : source.enableLogFiles();
            result.EmailLogFileAddress3 = source.emailLogFileAddress3() === undefined ? null : source.emailLogFileAddress3();
            result.CampaignImages = [];
            return result;
        },
        // Reset
        reset = function () {
            dirtyFlag.reset();
        };
        self = {
            id: id,
            campaignName: campaignName,
            description: description,
            campaignType: campaignType,
            isEnabled: isEnabled,
            startDateTime: startDateTime,
            includeCustomers: includeCustomers,
            includeSuppliers: includeSuppliers,
            includeProspects: includeProspects,
            includeNewsLetterSubscribers: includeNewsLetterSubscribers,
            includeFlag: includeFlag,
            flagIDs: flagIDs,
            customerTypeIDs: customerTypeIDs,
            groupIDs: groupIDs,
            subjectA: subjectA,
            hTMLMessageA: hTMLMessageA,
            fromName: fromName,
            fromAddress: fromAddress,
            returnPathAddress: returnPathAddress,
            replyToAddress: replyToAddress,
            emailLogFileAddress2: emailLogFileAddress2,
            emailEventId: emailEventId,
            eventName: eventName,
            sendEmailAfterDays: sendEmailAfterDays,
            campaignImages: campaignImages,
            includeType: includeType,
            includeCorporateCustomers: includeCorporateCustomers,
            enableLogFiles: enableLogFiles,
            emailLogFileAddress3: emailLogFileAddress3,
            isEditorDirty: isEditorDirty,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
   
    Campaign.Create = function (source) {
        return new Campaign(
            source.CampaignId,
            source.CampaignName,
            source.Description,
            source.CampaignType,
            source.IsEnabled,
            source.StartDateTime,
            source.IncludeCustomers,
            source.IncludeSuppliers,
            source.IncludeProspects,
            source.IncludeNewsLetterSubscribers,
            source.IncludeFlag,
            source.FlagIDs,
            source.CustomerTypeIDs,
            source.GroupIDs,
            source.SubjectA,
            source.HTMLMessageA,
            source.FromName,
            source.FromAddress,
            source.ReturnPathAddress,
            source.ReplyToAddress,
            source.EmailLogFileAddress2,
            source.EmailEvent,
            source.EventName,
            source.SendEmailAfterDays,
            source.IncludeType,
            source.IncludeCorporateCustomers,
            source.EnableLogFiles,
            source.EmailLogFileAddress3
      );
    };
    // #region ______________ Campaign Section _________________
    var CampaignSection = function (specifiedSectionId, specifiedSectionName) {
        var self,
            id = ko.observable(specifiedSectionId),
            sectionName = ko.observable(specifiedSectionName),
            //Is Expanded
            isExpanded = ko.observable(false),
            campaignEmailVariables = ko.observableArray([]);

        self = {
            id: id,
            sectionName: sectionName,
            isExpanded: isExpanded,
            campaignEmailVariables: campaignEmailVariables,
        };
        return self;
    };
    CampaignSection.Create = function (source) {
        return new CampaignSection(source.SectionId, source.SectionName);
    };

    var CampaignEmailVariable = function (specifiedVariableId, specifiedVariableName, specifiedVariableTag) {
        var self,
            id = ko.observable(specifiedVariableId),
            variableName = ko.observable(specifiedVariableName),
           variableTag = ko.observable(specifiedVariableTag);

        self = {
            id: id,
            variableName: variableName,
            variableTag: variableTag,
        };
        return self;
    };
    CampaignEmailVariable.Create = function (source) {
        return new CampaignEmailVariable(source.VariableId, source.VariableName, source.VariableTag);
    };

    var CampaignImage = function (specifiedCampaignImageId, specifiedCampaignId, specifiedImagePath, specifiedImageName, specifiedImageSource) {
        var self,
            id = ko.observable(specifiedCampaignImageId),
            campaignId = ko.observable(specifiedCampaignId),
            imagePath = ko.observable(specifiedImagePath),
            imageName = ko.observable(specifiedImageName),
            imageSource = ko.observable(specifiedImageSource);
        //Convert To Server
        convertToServerData = function (source) {
            var result = {};
            result.CampaignImageId = source.id() === undefined ? 0 : source.id();
            result.CampaignId = source.campaignId() === undefined ? 0 : source.campaignId();
            result.ImagePath = source.imagePath() === undefined ? 0 : source.imagePath();
            result.ImageName = source.imageName() === undefined ? 0 : source.imageName();
            result.ImageByteSource = source.imageSource() === undefined ? 0 : source.imageSource();
            return result;
        },
        self = {
            id: id,
            campaignId: campaignId,
            imagePath: imagePath,
            imageName: imageName,
            imageSource: imageSource,
            convertToServerData: convertToServerData,
        };
        return self;
    };
    CampaignImage.Create = function (source) {
        return new CampaignImage(source.CampaignImageId, source.CampaignId, source.ImagePath, source.ImageName, source.ImageSource);
    };
    

    return {
        Campaign: Campaign,
        CampaignSection: CampaignSection,
        CampaignImage: CampaignImage,
        CampaignEmailVariable: CampaignEmailVariable
        
    };
});