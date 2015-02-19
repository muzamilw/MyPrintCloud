/*
    Module with the model for the Order
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    // Estimate Entity
    // ReSharper disable InconsistentNaming
    Estimate = function (specifiedId, specifiedCode, specifiedName, specifiedCompanyId, specifiedCompanyName, specifiedNumberOfItems, specifiedCreationDate,
        specifiedFlagColor, specifiedSectionFlagId, specifiedOrderCode, specifiedIsEstimate, specifiedContactId, specifiedAddressId, specifiedIsDirectSale,
        specifiedIsOfficialOrder, specifiedIsCreditApproved, specifiedOrderDate, specifiedStartDeliveryDate, specifiedFinishDeliveryDate,
        specifiedHeadNotes, specifiedArtworkByDate, specifiedDataByDate, specifiedPaperByDate, specifiedTargetBindDate, specifiedXeroAccessCode,
        specifiedTargetPrintDate, specifiedOrderCreationDateTime, specifiedOrderManagerId, specifiedSalesPersonId, specifiedSourceId, 
        specifiedCreditLimitForJob, specifiedCreditLimitSetBy, specifiedCreditLimitSetOnDateTime, specifiedIsJobAllowedWOCreditCheck,
        specifiedAllowJobWOCreditCheckSetOnDateTime, specifiedAllowJobWOCreditCheckSetBy, specifiedCustomerPo, specifiedOfficialOrderSetBy,
        specifiedOfficialOrderSetOnDateTime, specifiedFootNotes) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId || 0),
            // Name
            name = ko.observable(specifiedName || undefined).extend({ required: true }),
            // Code
            code = ko.observable(specifiedCode || undefined),
            // Company Id
            companyId = ko.observable(specifiedCompanyId || undefined).extend({ required: true }),
            // Company Name
            companyName = ko.observable(specifiedCompanyName || undefined),
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
            // Flag Id
            sectionFlagId = ko.observable(specifiedSectionFlagId || undefined),
            // Order Code
            orderCode = ko.observable(specifiedOrderCode || undefined),
            // Is Estimate
            isEstimate = ko.observable(specifiedIsEstimate || false),
            // Contact Id
            contactId = ko.observable(specifiedContactId || undefined),
            // Address Id
            addressId = ko.observable(specifiedAddressId || undefined),
            // Is Direct Sale
            isDirectSale = ko.observable(specifiedIsDirectSale || true),
            // Is Direct Sale Ui
            isDirectSaleUi = ko.computed(function() {
                return isDirectSale() ? "Direct Order" : "Online Order";
            }),
            // Is Official Order
            isOfficialOrder = ko.observable(specifiedIsOfficialOrder || false),
            // Is Credit Approved
            isCreditApproved = ko.observable(specifiedIsCreditApproved || false),
            // Order Date
            orderDate = ko.observable(specifiedOrderDate || undefined),
            // Start Delivery Date
            startDeliveryDate = ko.observable(specifiedStartDeliveryDate || undefined),
            // Finish Delivery Date
            finishDeliveryDate = ko.observable(specifiedFinishDeliveryDate || undefined),
            // Head Notes
            headNotes = ko.observable(specifiedHeadNotes || undefined),
            // Artwork By Date
            artworkByDate = ko.observable(specifiedArtworkByDate || undefined),
            // Data By Date
            dataByDate = ko.observable(specifiedDataByDate || undefined),
            // Paper By Date
            paperByDate = ko.observable(specifiedPaperByDate || undefined),
            // Target Bind Date
            targetBindDate = ko.observable(specifiedTargetBindDate || undefined),
            // Xero Access Code
            xeroAccessCode = ko.observable(specifiedXeroAccessCode || undefined),
            // Target Print Date
            targetPrintDate = ko.observable(specifiedTargetPrintDate || undefined),
            // Order Creation Date Time
            orderCreationDateTime = ko.observable(specifiedOrderCreationDateTime || undefined),
            // Order Manager Id
            orderManagerId = ko.observable(specifiedOrderManagerId || undefined),
            // Sales Person Id
            salesPersonId = ko.observable(specifiedSalesPersonId || undefined),
            // Source Id
            sourceId = ko.observable(specifiedSourceId || undefined),
            // Credit Limit For Job
            creditLimitForJob = ko.observable(specifiedCreditLimitForJob || undefined),
            // Credit Limit Set By
            creditLimitSetBy = ko.observable(specifiedCreditLimitSetBy || undefined),
            // Credit Limit Set on Date Time
            creditLimitSetOnDateTime = ko.observable(specifiedCreditLimitSetOnDateTime || undefined),
            // Is JobAllowedWOCreditCheck
            isJobAllowedWoCreditCheck = ko.observable(specifiedIsJobAllowedWOCreditCheck || undefined),
            // Allow Job WOCreditCheckSetOnDateTime
            allowJobWoCreditCheckSetOnDateTime = ko.observable(specifiedAllowJobWOCreditCheckSetOnDateTime || undefined),
            // Allow JobWOCreditCheckSetBy
            allowJobWoCreditCheckSetBy = ko.observable(specifiedAllowJobWOCreditCheckSetBy || undefined),
            // Customer Po
            customerPo = ko.observable(specifiedCustomerPo || undefined),
            // Official Order Set By
            officialOrderSetBy = ko.observable(specifiedOfficialOrderSetBy || undefined),
            // Official Order Set on Date Time
            officialOrderSetOnDateTime = ko.observable(specifiedOfficialOrderSetOnDateTime || undefined),
            // Foot Notes
            footNotes = ko.observable(specifiedFootNotes || undefined),
            // Items
            items = ko.observableArray([]),
            // Errors
            errors = ko.validation.group({
                name: name,
                companyId: companyId
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
            // True if the order has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                name: name,
                code: code,
                companyId: companyId,
                contactId: contactId,
                addressId: addressId,
                isDirectSale: isDirectSale,
                isOfficialOrder: isOfficialOrder,
                isCreditApproved: isCreditApproved,
                orderDate: orderDate,
                startDeliveryDate: startDeliveryDate,
                finishDeliveryDate: finishDeliveryDate,
                headNotes: headNotes,
                artworkByDate: artworkByDate,
                dataByDate: dataByDate,
                paperByDate: paperByDate,
                targetBindDate: targetBindDate,
                xeroAccessCode: xeroAccessCode,
                targetPrintDate: targetPrintDate,
                orderCreationDateTime: orderCreationDateTime,
                orderManagerId: orderManagerId,
                salesPersonId: salesPersonId,
                sourceId: sourceId,
                creditLimitForJob: creditLimitForJob,
                creditLimitSetBy: creditLimitSetBy,
                creditLimitSetOnDateTime: creditLimitSetOnDateTime,
                isJobAllowedWoCreditCheck: isJobAllowedWoCreditCheck,
                allowJobWoCreditCheckSetOnDateTime: allowJobWoCreditCheckSetOnDateTime,
                allowJobWoCreditCheckSetBy: allowJobWoCreditCheckSetBy,
                customerPo: customerPo,
                officialOrderSetBy: officialOrderSetBy,
                officialOrderSetOnDateTime: officialOrderSetOnDateTime,
                footNotes: footNotes,
                sectionFlagId: sectionFlagId
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
                    EstimateId: id(),
                    EstimateCode: code(),
                    EstimateName: name(),
                    CompanyId: companyId(),
                    ContactId: contactId(),
                    AddressId: addressId(),
                    SectionFlagId: sectionFlagId(),
                    IsDirectSale: isDirectSale(),
                    IsOfficialOrder: isOfficialOrder(),
                    IsCreditApproved: isCreditApproved(),
                    OrderDate: orderDate(),
                    StartDeliveryDate: startDeliveryDate(),
                    FinishDeliveryDate: finishDeliveryDate(),
                    HeadNotes: headNotes(),
                    FootNotes: footNotes(),
                    ArtworkByDate: artworkByDate(),
                    DataByDate: dataByDate(),
                    PaperByDate: paperByDate(),
                    TargetBindDate: targetBindDate(),
                    XeroAccessCode: xeroAccessCode(),
                    TargetPrintDate: targetPrintDate(),
                    OrderCreationDateTime: orderCreationDateTime(),
                    OrderManagerId: orderManagerId(),
                    SalesPersonId: salesPersonId(),
                    SourceId: sourceId(),
                    CreditLimitForJob: creditLimitForJob(),
                    CreditLimitSetBy: creditLimitSetBy(),
                    CreditLimitSetOnDateTime: creditLimitSetOnDateTime(),
                    IsJobAllowedWoCreditCheck: isJobAllowedWoCreditCheck(),
                    AllowJobWoCreditCheckSetOnDateTime: allowJobWoCreditCheckSetOnDateTime(),
                    AllowJobWoCreditCheckSetBy: allowJobWoCreditCheckSetBy(),
                    CustomerPo: customerPo(),
                    OfficialOrderSetBy: officialOrderSetBy(),
                    OfficialOrderSetOnDateTime: officialOrderSetOnDateTime()
                };
            };

        return {
            id: id,
            name: name,
            code: code,
            noOfItemsUi: noOfItemsUi,
            creationDate: creationDate,
            flagColor: flagColor,
            orderCode: orderCode,
            isEstimate: isEstimate,
            companyId: companyId,
            companyName: companyName,
            contactId: contactId,
            addressId: addressId,
            sectionFlagId: sectionFlagId,
            isDirectSale: isDirectSale,
            isDirectSaleUi: isDirectSaleUi,
            isOfficialOrder: isOfficialOrder,
            isCreditApproved: isCreditApproved,
            orderDate: orderDate,
            startDeliveryDate: startDeliveryDate,
            finishDeliveryDate: finishDeliveryDate,
            headNotes: headNotes,
            footNotes: footNotes,
            artworkByDate: artworkByDate,
            dataByDate: dataByDate,
            paperByDate: paperByDate,
            targetBindDate: targetBindDate,
            xeroAccessCode: xeroAccessCode,
            targetPrintDate: targetPrintDate,
            orderCreationDateTime: orderCreationDateTime,
            orderManagerId: orderManagerId,
            salesPersonId: salesPersonId,
            sourceId: sourceId,
            creditLimitForJob: creditLimitForJob,
            creditLimitSetBy: creditLimitSetBy,
            creditLimitSetOnDateTime: creditLimitSetOnDateTime,
            isJobAllowedWoCreditCheck: isJobAllowedWoCreditCheck,
            allowJobWoCreditCheckSetOnDateTime: allowJobWoCreditCheckSetOnDateTime,
            allowJobWoCreditCheckSetBy: allowJobWoCreditCheckSetBy,
            customerPo: customerPo,
            officialOrderSetBy: officialOrderSetBy,
            officialOrderSetOnDateTime: officialOrderSetOnDateTime,
            items: items,
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

    // Item Entity
    Item = function (specifiedId, specifiedName, specifiedCode, specifiedProductName, specifiedProductCode, specifiedProductCategoryName,
        specifiedJobDescriptionTitle1, specifiedJobDescription1, specifiedJobDescriptionTitle2, specifiedJobDescription2, specifiedJobDescriptionTitle3,
        specifiedJobDescription3, specifiedJobDescriptionTitle4, specifiedJobDescription4, specifiedJobDescriptionTitle5,
        specifiedJobDescription5, specifiedJobDescriptionTitle6, specifiedJobDescription6,
        specifiedJobDescriptionTitle7, specifiedJobDescription7, specifiedJobDescriptionTitle8, specifiedJobDescription8, specifiedJobDescriptionTitle9,
        specifiedJobDescription9, specifiedJobDescriptionTitle10, specifiedJobDescription10, specifiedIsQtyRanged,
        specifiedDefaultItemTax) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId || 0),
            // Name
            name = ko.observable(specifiedName || undefined),
            // Code
            code = ko.observable(specifiedCode || undefined),
            // Product Name
            productName = ko.observable(specifiedProductName || undefined).extend({ required: true }),
            // Product Name For Grid
            productNameForGrid = ko.computed(function () {
                if (!productName()) {
                    return "";
                }
                return productName().length > 30 ? productName().substring(0, 29) : productName();
            }),
            // Product Code
            productCode = ko.observable(specifiedProductCode || undefined).extend({ required: true }),
            // product Category Name
            productCategoryName = ko.observable(specifiedProductCategoryName || undefined),
            // job description title1
            jobDescriptionTitle1 = ko.observable(specifiedJobDescriptionTitle1 || undefined),
            // job description title2
            jobDescriptionTitle2 = ko.observable(specifiedJobDescriptionTitle2 || undefined),
            // job description title3
            jobDescriptionTitle3 = ko.observable(specifiedJobDescriptionTitle3 || undefined),
            // job description title4
            jobDescriptionTitle4 = ko.observable(specifiedJobDescriptionTitle4 || undefined),
            // job description title5
            jobDescriptionTitle5 = ko.observable(specifiedJobDescriptionTitle5 || undefined),
            // job description title6
            jobDescriptionTitle6 = ko.observable(specifiedJobDescriptionTitle6 || undefined),
            // job description title7
            jobDescriptionTitle7 = ko.observable(specifiedJobDescriptionTitle7 || undefined),
            // job description title8
            jobDescriptionTitle8 = ko.observable(specifiedJobDescriptionTitle8 || undefined),
            // job description title9
            jobDescriptionTitle9 = ko.observable(specifiedJobDescriptionTitle9 || undefined),
            // job description title10
            jobDescriptionTitle10 = ko.observable(specifiedJobDescriptionTitle10 || undefined),
            // job description 1
            jobDescription1 = ko.observable(specifiedJobDescription1 || undefined),
            // job description 2
            jobDescription2 = ko.observable(specifiedJobDescription2 || undefined),
            // job description 3
            jobDescription3 = ko.observable(specifiedJobDescription3 || undefined),
            // job description 4
            jobDescription4 = ko.observable(specifiedJobDescription4 || undefined),
            // job description 5
            jobDescription5 = ko.observable(specifiedJobDescription5 || undefined),
            // job description 6
            jobDescription6 = ko.observable(specifiedJobDescription6 || undefined),
            // job description 7
            jobDescription7 = ko.observable(specifiedJobDescription7 || undefined),
            // job description 8
            jobDescription8 = ko.observable(specifiedJobDescription8 || undefined),
            // job description 9
            jobDescription9 = ko.observable(specifiedJobDescription9 || undefined),
            // job description 10
            jobDescription10 = ko.observable(specifiedJobDescription10 || undefined),
            // Is Qty Ranged
            isQtyRanged = ko.observable(!specifiedIsQtyRanged ? 2 : 1),
            // Default Item Tax
            defaultItemTax = ko.observable(specifiedDefaultItemTax || undefined),
            // Item Stock options
            itemStockOptions = ko.observableArray([]),
            // Item Sections
            itemSections = ko.observableArray([]),
            // Errors
            errors = ko.validation.group({
                productCode: productCode,
                productName: productName
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0 && 
                itemSections.filter(function (itemSection) {
                    return !itemSection.isValid();
                }).length === 0;
            }),
            // Show All Error Messages
            showAllErrors = function () {
                // Show Item Errors
                errors.showAllMessages();
                // Show Item Section Errors
                var itemSectionErrors = itemSections.filter(function (itemSection) {
                    return !itemSection.isValid();
                });
                if (itemSectionErrors.length > 0) {
                    _.each(itemSectionErrors, function (itemSection) {
                        itemSection.errors.showAllMessages();
                    });
                }
            },
            // Set Validation Summary
            setValidationSummary = function (validationSummaryList) {
                validationSummaryList.removeAll();
                if (productName.error) {
                    validationSummaryList.push({ name: productName.domElement.name, element: productName.domElement });
                }
                if (productCode.error) {
                    validationSummaryList.push({ name: productCode.domElement.name, element: productCode.domElement });
                }
                // Show Item Stock Option Errors
                var itemStockOptionInvalid = itemStockOptions.find(function (itemStockOption) {
                    return !itemStockOption.isValid();
                });
                if (itemStockOptionInvalid) {
                    if (itemStockOptionInvalid.label.error) {
                        var labelElement = itemStockOptionInvalid.label.domElement;
                        validationSummaryList.push({ name: labelElement.name, element: labelElement });
                    }
                }
                // Show Item Section Errors
                var itemSectionInvalid = itemSections.find(function (itemSection) {
                    return !itemSection.isValid();
                });
                if (itemSectionInvalid) {
                    if (itemSectionInvalid.name.error || itemSectionInvalid.pressId.error || itemSectionInvalid.stockItemId.error) {
                        var nameElement = itemSectionInvalid.name.domElement;
                        var errorName = "";
                        if (itemSectionInvalid.name.error) {
                            errorName = "Section Name";
                        }
                        else if (itemSectionInvalid.pressId.error) {
                            errorName = "Section Press";
                        }
                        else if (itemSectionInvalid.stockItemId.error) {
                            errorName = "Section Stock Item";
                        }
                        validationSummaryList.push({ name: errorName, element: nameElement });
                    }
                }
            },
            // True if the product has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                name: name,
                code: code,
                productName: productName,
                productCode: productCode,
                jobDescriptionTitle1: jobDescriptionTitle1,
                jobDescriptionTitle2: jobDescriptionTitle2,
                jobDescriptionTitle3: jobDescriptionTitle3,
                jobDescriptionTitle4: jobDescriptionTitle4,
                jobDescriptionTitle5: jobDescriptionTitle5,
                jobDescriptionTitle6: jobDescriptionTitle6,
                jobDescriptionTitle7: jobDescriptionTitle7,
                jobDescriptionTitle8: jobDescriptionTitle8,
                jobDescriptionTitle9: jobDescriptionTitle9,
                jobDescriptionTitle10: jobDescriptionTitle10,
                jobDescription1: jobDescription1,
                jobDescription2: jobDescription2,
                jobDescription3: jobDescription3,
                jobDescription4: jobDescription4,
                jobDescription5: jobDescription5,
                jobDescription6: jobDescription6,
                jobDescription7: jobDescription7,
                jobDescription8: jobDescription8,
                jobDescription9: jobDescription9,
                jobDescription10: jobDescription10,
                isQtyRanged: isQtyRanged,
                defaultItemTax: defaultItemTax,
                itemSections: itemSections
            }),
            // Item Section Changes
            itemSectionHasChanges = ko.computed(function () {
                return itemSections.find(function (itemSection) {
                    return itemSection.hasChanges();
                }) != null;
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty() || itemSectionHasChanges();
            }),
            // Reset
            reset = function () {
                itemSections.each(function (itemSection) {
                    return itemSection.reset();
                });
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    ItemId: id(),
                    ItemCode: code(),
                    ProductCode: productCode(),
                    ProductName: productName(),
                    JobDescriptionTitle1: jobDescriptionTitle1(),
                    JobDescriptionTitle2: jobDescriptionTitle2(),
                    JobDescriptionTitle3: jobDescriptionTitle3(),
                    JobDescriptionTitle4: jobDescriptionTitle4(),
                    JobDescriptionTitle5: jobDescriptionTitle5(),
                    JobDescriptionTitle6: jobDescriptionTitle6(),
                    JobDescriptionTitle7: jobDescriptionTitle7(),
                    JobDescriptionTitle8: jobDescriptionTitle8(),
                    JobDescriptionTitle9: jobDescriptionTitle9(),
                    JobDescriptionTitle10: jobDescriptionTitle10(),
                    JobDescription1: jobDescription1(),
                    JobDescription2: jobDescription2(),
                    JobDescription3: jobDescription3(),
                    JobDescription4: jobDescription4(),
                    JobDescription5: jobDescription5(),
                    JobDescription6: jobDescription6(),
                    JobDescription7: jobDescription7(),
                    JobDescription8: jobDescription8(),
                    JobDescription9: jobDescription9(),
                    JobDescription10: jobDescription10(),
                    IsQtyRanged: isQtyRanged() === 2 ? false : true,
                    DefaultItemTax: defaultItemTax(),
                    ItemSections: itemSections.map(function (itemSection, index) {
                        var section = itemSection.convertToServerData();
                        section.SectionNo = index + 1;
                        return section;
                    }),
                };
            };

        return {
            id: id,
            name: name,
            code: code,
            productName: productName,
            productNameForGrid: productNameForGrid,
            productCode: productCode,
            productCategoryName: productCategoryName,
            jobDescriptionTitle1: jobDescriptionTitle1,
            jobDescriptionTitle2: jobDescriptionTitle2,
            jobDescriptionTitle3: jobDescriptionTitle3,
            jobDescriptionTitle4: jobDescriptionTitle4,
            jobDescriptionTitle5: jobDescriptionTitle5,
            jobDescriptionTitle6: jobDescriptionTitle6,
            jobDescriptionTitle7: jobDescriptionTitle7,
            jobDescriptionTitle8: jobDescriptionTitle8,
            jobDescriptionTitle9: jobDescriptionTitle9,
            jobDescriptionTitle10: jobDescriptionTitle10,
            jobDescription1: jobDescription1,
            jobDescription2: jobDescription2,
            jobDescription3: jobDescription3,
            jobDescription4: jobDescription4,
            jobDescription5: jobDescription5,
            jobDescription6: jobDescription6,
            jobDescription7: jobDescription7,
            jobDescription8: jobDescription8,
            jobDescription9: jobDescription9,
            jobDescription10: jobDescription10,
            isQtyRangedUi: isQtyRangedUi,
            isQtyRanged: isQtyRanged,
            defaultItemTax: defaultItemTax,
            itemSections: itemSections,
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
    },
   
    // System User Entity        
    SystemUser = function (specifiedId, specifiedName) {
        return {
            id: specifiedId,
            name: specifiedName
        };
    },
    
    // Pipeline Source Entity        
    PipeLineSource = function (specifiedId, specifiedDescription) {
        return {
            id: specifiedId,
            name: specifiedDescription
        };
    },

    // Address Entity
    Address = function(specifiedId, specifiedName, specifiedAddress1, specifiedAddress2, specifiedTelephone1) {
        return {            
            id: specifiedId,
            name: specifiedName,
            address1: specifiedAddress1 || "",
            address2: specifiedAddress2 || "",
            telephone1: specifiedTelephone1 || ""
        };
    },
    
    // Company Contact Entity
    CompanyContact = function (specifiedId, specifiedName, specifiedEmail) {
        return {            
            id: specifiedId,
            name: specifiedName,
            email: specifiedEmail || ""
        };
    };
    
    // Estimate Factory
    Estimate.Create = function(source) {
        var estimate = new Estimate(source.EstimateId, source.EstimateCode, source.EstimateName, source.CompanyId, source.CompanyName, source.ItemsCount,
        source.CreationDate, source.FlagColor, source.SectionFlagId, source.OrderCode, source.IsEstimate, source.ContactId, source.AddressId, source.IsDirectSale,
        source.IsOfficialOrder, source.IsCreditApproved, source.OrderDate, source.StartDeliveryDate, source.FinishDeliveryDate, source.HeadNotes,
        source.ArtworkByDate, source.DataByDate, source.PaperByDate, source.TargetBindDate, source.XeroAccessCode, source.TargetPrintDate,
        source.OrderCreationDateTime, source.SalesAndOrderManagerId, source.SalesPersonId, source.SourceId, source.CreditLimitForJob, source.CreditLimitSetBy,
        source.CreditLimitSetOnDateTime, source.IsJobAllowedWOCreditCheck, source.AllowJobWOCreditCheckSetOnDateTime, source.AllowJobWOCreditCheckSetBy,
        source.CustomerPo, source.OfficialOrderSetBy, source.OfficialOrderSetOnDateTime);

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

    // Address Factory
    Address.Create = function (source) {
        return new Address(source.AddressId, source.AddressName, source.Address1, source.Address2, source.Tel1);
    };
    
    // Company Contact Factory
    CompanyContact.Create = function (source) {
        return new CompanyContact(source.ContactId, source.Name, source.Email);
    };
    
    // System User Factory
    SystemUser.Create = function (source) {
        return new SystemUser(source.SystemUserId, source.UserName);
    };
    
    // Pipeline Source Factory
    PipeLineSource.Create = function (source) {
        return new PipeLineSource(source.SourceId, source.Description);
    };

    return {
        // Estimate Constructor
        Estimate: Estimate,
        // sectionflag constructor
        SectionFlag: SectionFlag,
        // Address Constructor
        Address: Address,
        // Company Contact Constructor
        CompanyContact: CompanyContact,
        // System User Constructor
        SystemUser: SystemUser,
        // PipeLine Source Constructor
        PipeLineSource: PipeLineSource,
        // Item Constructor
        Item: Item
    };
});