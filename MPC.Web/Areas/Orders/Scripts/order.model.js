/*
    Module with the model for the Order
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    // Status Enums
    // ReSharper disable InconsistentNaming
    Status = {
        // ReSharper restore InconsistentNaming
        ShoppingCart: 3,
        NotProgressedToJob: 17
    },

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
            noOfItemsUi = ko.computed(function () {
                return "( " + numberOfItems() + " ) Items";
            }),
            // Creation Date
            creationDate = ko.observable(specifiedCreationDate ? moment(specifiedCreationDate).toDate() : undefined),
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
            isDirectSale = ko.observable(!specifiedIsDirectSale ? false : true),
            // Is Direct Sale Ui
            isDirectSaleUi = ko.computed(function () {
                return isDirectSale() ? "Direct Order" : "Online Order";
            }),
            // Is Official Order
            isOfficialOrder = ko.observable(specifiedIsOfficialOrder || false),
            // Is Credit Approved
            isCreditApproved = ko.observable(specifiedIsCreditApproved || false),
            // Order Date
            orderDate = ko.observable(specifiedOrderDate ? moment(specifiedOrderDate).toDate() : undefined),
            // Start Delivery Date
            startDeliveryDate = ko.observable(specifiedStartDeliveryDate ? moment(specifiedStartDeliveryDate).toDate() : undefined),
            // Finish Delivery Date
            finishDeliveryDate = ko.observable(specifiedFinishDeliveryDate ? moment(specifiedFinishDeliveryDate).toDate() : undefined),
            // Head Notes
            headNotes = ko.observable(specifiedHeadNotes || undefined),
            // Artwork By Date
            artworkByDate = ko.observable(specifiedArtworkByDate ? moment(specifiedArtworkByDate).toDate() : undefined),
            // Data By Date
            dataByDate = ko.observable(specifiedDataByDate ? moment(specifiedDataByDate).toDate() : undefined),
            // Paper By Date
            paperByDate = ko.observable(specifiedPaperByDate ? moment(specifiedPaperByDate).toDate() : undefined),
            // Target Bind Date
            targetBindDate = ko.observable(specifiedTargetBindDate ? moment(specifiedTargetBindDate).toDate() : undefined),
            // Xero Access Code
            xeroAccessCode = ko.observable(specifiedXeroAccessCode || undefined),
            // Target Print Date
            targetPrintDate = ko.observable(specifiedTargetPrintDate ? moment(specifiedTargetPrintDate).toDate() : undefined),
            // Order Creation Date Time
            orderCreationDateTime = ko.observable(specifiedOrderCreationDateTime ? moment(specifiedOrderCreationDateTime).toDate() : undefined),
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
            creditLimitSetOnDateTime = ko.observable(specifiedCreditLimitSetOnDateTime ? moment(specifiedCreditLimitSetOnDateTime).toDate() : undefined),
            // Is JobAllowedWOCreditCheck
            isJobAllowedWoCreditCheck = ko.observable(specifiedIsJobAllowedWOCreditCheck || undefined),
            // Allow Job WOCreditCheckSetOnDateTime
            allowJobWoCreditCheckSetOnDateTime = ko.observable(specifiedAllowJobWOCreditCheckSetOnDateTime ?
                moment(specifiedAllowJobWOCreditCheckSetOnDateTime).toDate() : undefined),
            // Allow JobWOCreditCheckSetBy
            allowJobWoCreditCheckSetBy = ko.observable(specifiedAllowJobWOCreditCheckSetBy || undefined),
            // Customer Po
            customerPo = ko.observable(specifiedCustomerPo || undefined),
            // Official Order Set By
            officialOrderSetBy = ko.observable(specifiedOfficialOrderSetBy || undefined),
            // Official Order Set on Date Time
            officialOrderSetOnDateTime = ko.observable(specifiedOfficialOrderSetOnDateTime ? moment(specifiedOfficialOrderSetOnDateTime).toDate() : undefined),
            // Foot Notes
            footNotes = ko.observable(specifiedFootNotes || undefined),
            // Items
            items = ko.observableArray([]),
            // Pre Payments
            prePayments = ko.observableArray([]),
            // Deliver Schedule
            deliverySchedules = ko.observableArray([]),
            // Status Id
            statusId = ko.observable(undefined),
            // Errors
            errors = ko.validation.group({
                name: name,
                companyId: companyId
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0 &&
                    items.filter(function (item) {
                        return !item.isValid();
                    }).length === 0;
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
                sectionFlagId: sectionFlagId,
                statusId: statusId
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
                    EstimateId: id(),
                    StatusId: statusId(),
                    EstimateCode: code(),
                    EstimateName: name(),
                    CompanyId: companyId(),
                    ContactId: contactId(),
                    AddressId: addressId(),
                    SectionFlagId: sectionFlagId(),
                    IsDirectSale: isDirectSale(),
                    IsOfficialOrder: isOfficialOrder(),
                    IsCreditApproved: isCreditApproved(),
                    OrderDate: orderDate() ? moment(orderDate()).format(ist.utcFormat) + 'Z' : undefined,
                    StartDeliveryDate: startDeliveryDate() ? moment(startDeliveryDate()).format(ist.utcFormat) + 'Z' : undefined,
                    FinishDeliveryDate: finishDeliveryDate() ? moment(finishDeliveryDate()).format(ist.utcFormat) + 'Z' : undefined,
                    HeadNotes: headNotes(),
                    FootNotes: footNotes(),
                    ArtworkByDate: artworkByDate() ? moment(artworkByDate()).format(ist.utcFormat) + 'Z' : undefined,
                    DataByDate: dataByDate() ? moment(dataByDate()).format(ist.utcFormat) + 'Z' : undefined,
                    PaperByDate: paperByDate() ? moment(paperByDate()).format(ist.utcFormat) + 'Z' : undefined,
                    TargetBindDate: targetBindDate() ? moment(targetBindDate()).format(ist.utcFormat) + 'Z' : undefined,
                    XeroAccessCode: xeroAccessCode(),
                    TargetPrintDate: targetPrintDate() ? moment(targetPrintDate()).format(ist.utcFormat) + 'Z' : undefined,
                    OrderCreationDateTime: orderCreationDateTime() ? moment(orderCreationDateTime()).format(ist.utcFormat) + 'Z' : undefined,
                    OrderManagerId: orderManagerId(),
                    SalesPersonId: salesPersonId(),
                    SourceId: sourceId(),
                    CreditLimitForJob: creditLimitForJob(),
                    CreditLimitSetBy: creditLimitSetBy(),
                    CreditLimitSetOnDateTime: creditLimitSetOnDateTime() ? moment(creditLimitSetOnDateTime()).format(ist.utcFormat) + 'Z' : undefined,
                    IsJobAllowedWoCreditCheck: isJobAllowedWoCreditCheck(),
                    AllowJobWoCreditCheckSetOnDateTime: allowJobWoCreditCheckSetOnDateTime() ?
                        moment(allowJobWoCreditCheckSetOnDateTime()).format(ist.utcFormat) + 'Z' : undefined,
                    AllowJobWoCreditCheckSetBy: allowJobWoCreditCheckSetBy(),
                    CustomerPo: customerPo(),
                    OfficialOrderSetBy: officialOrderSetBy(),
                    OfficialOrderSetOnDateTime: officialOrderSetOnDateTime() ? moment(officialOrderSetOnDateTime()).format(ist.utcFormat) + 'Z' : undefined,
                    PrePayments: [],
                    ShippingInformations: [],
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
            prePayments: prePayments,
            deliverySchedules: deliverySchedules,
            errors: errors,
            isValid: isValid,
            showAllErrors: showAllErrors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            setValidationSummary: setValidationSummary,
            convertToServerData: convertToServerData,
            statusId: statusId
        };
    },

    // Item Entity
    Item = function (specifiedId, specifiedName, specifiedCode, specifiedProductName, specifiedProductCode,
        specifiedJobDescriptionTitle1, specifiedJobDescription1, specifiedJobDescriptionTitle2, specifiedJobDescription2, specifiedJobDescriptionTitle3,
        specifiedJobDescription3, specifiedJobDescriptionTitle4, specifiedJobDescription4, specifiedJobDescriptionTitle5,
        specifiedJobDescription5, specifiedJobDescriptionTitle6, specifiedJobDescription6, specifiedJobDescriptionTitle7, specifiedJobDescription7,
        specifiedIsQtyRanged, specifiedDefaultItemTax, specifiedStatusId, specifiedStatusName, specifiedQty1, specifiedQty1NetTotal, specifiedItemNotes,
        specifiedProductCategories, specifiedJobCode, specifiedJobCreationDateTime, specifiedJobManagerId, specifiedJobActualStartDateTime,
        specifiedJobActualCompletionDateTime, specifiedJobProgressedBy, specifiedJobSignedBy, specifiedNominalCodeId, specifiedJobStatusId,
        specifiedInvoiceDescription, specifiedQty1MarkUpId1, specifiedQty2MarkUpId2, specifiedQty3MarkUpId3, specifiedQty2NetTotal, specifiedQty3NetTotal,
        specifiedQty1Tax1Value, specifiedQty2Tax1Value, specifiedQty3Tax1Value, specifiedQty1GrossTotal, specifiedQty2GrossTotal, specifiedQty3GrossTotal,
         specifiedTax1) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId || 0),
            // Name
            name = ko.observable(specifiedName || undefined),
             // Code
            code = ko.observable(specifiedCode || undefined),
            // Product Name
            productName = ko.observable(specifiedProductName || undefined).extend({ required: true }),
            // Product Code
            productCode = ko.observable(specifiedProductCode || undefined).extend({ required: true }),
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
            // Is Qty Ranged
            isQtyRanged = ko.observable(!specifiedIsQtyRanged ? 2 : 1),
            // Default Item Tax
            defaultItemTax = ko.observable(specifiedDefaultItemTax || undefined),
            // Status Id
            statusId = ko.observable(specifiedStatusId || undefined),
            // Status Name
            statusName = ko.observable(specifiedStatusName || undefined),
            // Qty 1
            qty1 = ko.observable(specifiedQty1 || 0),
            // Qty 1 Net Total
            qty1NetTotal = ko.observable(specifiedQty1NetTotal || 0),
            // Item Notes
            itemNotes = ko.observable(specifiedItemNotes || undefined),
            // Job Code
            jobCode = ko.observable(specifiedJobCode || undefined),
            // Creation Date Time
            jobCreationDateTime = ko.observable(specifiedJobCreationDateTime ? moment(specifiedJobCreationDateTime).toDate() : undefined),
            // Job Status Id
            jobStatusId = ko.observable(specifiedJobStatusId || undefined),
            // Job Manager Id
            jobManagerId = ko.observable(specifiedJobManagerId || undefined),
            // Job Progressed By
            jobProgressedBy = ko.observable(specifiedJobProgressedBy || undefined),
            // Job Progressed By
            jobSignedBy = ko.observable(specifiedJobSignedBy || undefined),
            // Job Actual Start DateTime
            jobActualStartDateTime = ko.observable(specifiedJobActualStartDateTime ? moment(specifiedJobActualStartDateTime).toDate() : undefined),
            // Job ActualCompletion DateTime
            jobActualCompletionDateTime = ko.observable(specifiedJobActualCompletionDateTime ? moment(specifiedJobActualCompletionDateTime).toDate() : undefined),
            // NominalCode Id
            nominalCodeId = ko.observable(specifiedNominalCodeId || undefined),
            // Invoice Description
            invoiceDescription = ko.observable(specifiedInvoiceDescription || undefined),
            // Product Categories
            productCategories = ko.observableArray(specifiedProductCategories || []),
            // Product Categories Ui
            productCategoriesUi = ko.computed(function () {
                if (!productCategories || productCategories().length === 0) {
                    return "";
                }

                var categories = "";
                productCategories.each(function (category, index) {
                    var pcname = category;
                    if (index < productCategoryItems().length - 1) {
                        pcname = pcname + " || ";
                    }
                    categories += pcname;
                });

                return categories;
            }),
            // Item Stock options
            itemStockOptions = ko.observableArray([]),
            // Item Sections
            itemSections = ko.observableArray([]),
            //ItemPriceMatrix
            itemPriceMatrices = ko.observableArray([]),
            qty1MarkUpId1 = ko.observable(specifiedQty1MarkUpId1 || undefined),
            qty2MarkUpId2 = ko.observable(specifiedQty2MarkUpId2 || undefined),
            qty3MarkUpId3 = ko.observable(specifiedQty3MarkUpId3 || undefined),
            qty2NetTotal = ko.observable(specifiedQty2NetTotal || 0),
            qty3NetTotal = ko.observable(specifiedQty3NetTotal || 0),
            qty1Tax1Value = ko.observable(specifiedQty1Tax1Value || 0),
            qty2Tax1Value = ko.observable(specifiedQty2Tax1Value || 0),
            qty3Tax1Value = ko.observable(specifiedQty3Tax1Value || 0),
            qty1GrossTotal = ko.observable(specifiedQty1GrossTotal || 0),
            qty2GrossTotal = ko.observable(specifiedQty2GrossTotal || 0),
            qty3GrossTotal = ko.observable(specifiedQty3GrossTotal || 0),
            tax1 = ko.observable(specifiedTax1 || undefined),
            taxRateIsDisabled = ko.observable(false),
           
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
                jobDescription1: jobDescription1,
                jobDescription2: jobDescription2,
                jobDescription3: jobDescription3,
                jobDescription4: jobDescription4,
                jobDescription5: jobDescription5,
                jobDescription6: jobDescription6,
                jobDescription7: jobDescription7,
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
                    JobDescription1: jobDescription1(),
                    JobDescription2: jobDescription2(),
                    JobDescription3: jobDescription3(),
                    JobDescription4: jobDescription4(),
                    JobDescription5: jobDescription5(),
                    JobDescription6: jobDescription6(),
                    JobDescription7: jobDescription7(),
                    IsQtyRanged: isQtyRanged() === 2 ? false : true,
                    DefaultItemTax: defaultItemTax(),
                    ItemNotes: itemNotes(),
                    JobCreationDateTime: jobCreationDateTime() ? moment(jobCreationDateTime()).format(ist.utcFormat) + "Z" : undefined,
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
            productCode: productCode,
            jobDescriptionTitle1: jobDescriptionTitle1,
            jobDescriptionTitle2: jobDescriptionTitle2,
            jobDescriptionTitle3: jobDescriptionTitle3,
            jobDescriptionTitle4: jobDescriptionTitle4,
            jobDescriptionTitle5: jobDescriptionTitle5,
            jobDescriptionTitle6: jobDescriptionTitle6,
            jobDescriptionTitle7: jobDescriptionTitle7,
            jobDescription1: jobDescription1,
            jobDescription2: jobDescription2,
            jobDescription3: jobDescription3,
            jobDescription4: jobDescription4,
            jobDescription5: jobDescription5,
            jobDescription6: jobDescription6,
            jobDescription7: jobDescription7,
            isQtyRanged: isQtyRanged,
            defaultItemTax: defaultItemTax,
            statusId: statusId,
            statusName: statusName,
            qty1NetTotal: qty1NetTotal,
            qty1: qty1,
            productCategoriesUi: productCategoriesUi,
            jobCode: jobCode,
            itemNotes: itemNotes,
            jobCreationDateTime: jobCreationDateTime,
            jobManagerId: jobManagerId,
            jobProgressedBy: jobProgressedBy,
            jobSignedBy: jobSignedBy,
            jobActualStartDateTime: jobActualStartDateTime,
            jobActualCompletionDateTime: jobActualCompletionDateTime,
            jobStatusId: jobStatusId,
            nominalCodeId: nominalCodeId,
            invoiceDescription: invoiceDescription,
            itemSections: itemSections,
            qty1MarkUpId1: qty1MarkUpId1,
            qty2MarkUpId2: qty2MarkUpId2,
            qty3MarkUpId3: qty3MarkUpId3,
            qty2NetTotal: qty2NetTotal,
            qty3NetTotal: qty3NetTotal,
            qty1Tax1Value: qty1Tax1Value,
            qty2Tax1Value: qty2Tax1Value,
            qty3Tax1Value: qty3Tax1Value,
            qty1GrossTotal: qty1GrossTotal,
            qty2GrossTotal: qty2GrossTotal,
            qty3GrossTotal: qty3GrossTotal,
            tax1: tax1,
            taxRateIsDisabled: taxRateIsDisabled,
            itemStockOptions: itemStockOptions,
            itemPriceMatrices: itemPriceMatrices,
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

    // Item Section Entity
    ItemSection = function (specifiedId, specifiedSectionNo, specifiedSectionName, specifiedSectionSizeId, specifiedItemSizeId, specifiedIsSectionSizeCustom,
        specifiedSectionSizeHeight, specifiedSectionSizeWidth, specifiedIsItemSizeCustom, specifiedItemSizeHeight, specifiedItemSizeWidth,
        specifiedPressId, specifiedStockItemId, specifiedStockItemName, specifiedPressName, specifiedGuillotineId, specifiedQty1, specifiedQty2,
        specifiedQty3, specifiedQty1Profit, specifiedQty2Profit, specifiedQty3Profit, specifiedBaseCharge1, specifiedBaseCharge2, specifiedBaseCharge3,
        specifiedIncludeGutter, specifiedFilmId, specifiedIsPaperSupplied, specifiedSide1PlateQty, specifiedSide2PlateQty, specifiedIsPlateSupplied,
        specifiedItemId, specifiedIsDoubleSided, specifiedIsWorknTurn, specifiedPrintViewLayoutPortrait, specifiedPrintViewLayoutLandscape, specifiedPlateInkId,
        specifiedSimilarSections) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // name
            name = ko.observable(specifiedSectionName || undefined).extend({ required: true }),
            // Stock Item Id
            stockItemId = ko.observable(specifiedStockItemId || undefined).extend({ required: true }),
            // Stock Item Name
            stockItemName = ko.observable(specifiedStockItemName || undefined),
            // Press Id
            pressId = ko.observable(specifiedPressId || undefined).extend({ required: true }),
            // Press Name
            pressName = ko.observable(specifiedPressName || undefined),
            // section size id
            sectionSizeId = ko.observable(specifiedSectionSizeId || undefined),
            // Item size id
            itemSizeId = ko.observable(specifiedItemSizeId || undefined),
            // Section No
            sectionNo = ko.observable(specifiedSectionNo || undefined),
            // Is Section Size Custom
            isSectionSizeCustom = ko.observable(specifiedIsSectionSizeCustom || undefined),
            // Is Item Size Custom
            isItemSizeCustom = ko.observable(specifiedIsItemSizeCustom || undefined),
            // Section Size Height
            sectionSizeHeight = ko.observable(specifiedSectionSizeHeight || undefined),
            // Section Size Width
            sectionSizeWidth = ko.observable(specifiedSectionSizeWidth || undefined),
            // Item Size Height
            itemSizeHeight = ko.observable(specifiedItemSizeHeight || undefined),
            // Item Size Width
            itemSizeWidth = ko.observable(specifiedItemSizeWidth || undefined),
            // Guillotine Id
            guillotineId = ko.observable(specifiedGuillotineId || undefined),
            // Qty1
            qty1 = ko.observable(specifiedQty1 || 0),
            // Qty2
            qty2 = ko.observable(specifiedQty2 || 0),
            // Qty3
            qty3 = ko.observable(specifiedQty3 || 0),
            // Qty1 Profit
            qty1Profit = ko.observable(specifiedQty1Profit || 0),
            // Qty2Profit Width
            qty2Profit = ko.observable(specifiedQty2Profit || 0),
            // Qty3Profit Width
            qty3Profit = ko.observable(specifiedQty3Profit || 0),
            // Base Charge1
            baseCharge1 = ko.observable(specifiedBaseCharge1 || 0),
            // Base Charge2
            baseCharge2 = ko.observable(specifiedBaseCharge2 || 0),
            // Base Charge3
            baseCharge3 = ko.observable(specifiedBaseCharge3 || 0),
            // Include Gutter
            includeGutter = ko.observable(specifiedIncludeGutter || undefined),
            // FilmId
            filmId = ko.observable(specifiedFilmId || undefined),
            // IsPaperSupplied
            isPaperSupplied = ko.observable(specifiedIsPaperSupplied || undefined),
            // Side1 Plate Qty
            side1PlateQty = ko.observable(specifiedSide1PlateQty || undefined),
            // Side2 Plate Qty
            side2PlateQty = ko.observable(specifiedSide2PlateQty || undefined),
            // Is Plate Supplied
            isPlateSupplied = ko.observable(specifiedIsPlateSupplied || undefined),
            // Item Id
            itemId = ko.observable(specifiedItemId || undefined),
            // Is Double Sided
            isDoubleSided = ko.observable(specifiedIsDoubleSided || false),
            // Is Work N Turn
            isWorknTurn = ko.observable(specifiedIsWorknTurn || false),
            // DoubleOrWorknTurn
            doubleOrWorknTurn = ko.observable(specifiedIsDoubleSided !== null || specifiedIsDoubleSided !== undefined ? 
                (!specifiedIsDoubleSided ? 1 : ((specifiedIsWorknTurn !== null || specifiedIsWorknTurn !== undefined) && specifiedIsWorknTurn ? 3 : 2)) :
                (specifiedIsWorknTurn !== null || specifiedIsWorknTurn !== undefined ? (!specifiedIsWorknTurn ? 1 : 3) : 1)),
            // Double Or Work n Turn
            doubleWorknTurn = ko.computed({
                read: function() {
                    return '' + doubleOrWorknTurn();
                },
                write: function(value) {
                    if (!value || value === doubleOrWorknTurn()) {
                        return;
                    }

                    // Single Side
                    if (value === "1") {
                        isDoubleSided(false);
                        isWorknTurn(false);
                    }
                    else if (value === "2") { // Double Sided
                        isDoubleSided(true);
                        isWorknTurn(false);
                    }
                    else if (value === "3") { // Work n Turn
                        isDoubleSided(true);
                        isWorknTurn(true);
                    }

                    doubleOrWorknTurn(value);
                }
            }),
            // PrintViewLayoutPortrait
            printViewLayoutPortrait = ko.observable(specifiedPrintViewLayoutPortrait || 0),
            // PrintViewLayoutLandscape
            printViewLayoutLandscape = ko.observable(specifiedPrintViewLayoutLandscape || 0),
            // Number Up
            numberUp = ko.computed(function() {
                if (printViewLayoutPortrait() >= printViewLayoutLandscape()) {
                    return printViewLayoutPortrait();
                }
                else if (printViewLayoutPortrait() <= printViewLayoutLandscape()) {
                    return printViewLayoutLandscape();
                }

                return 0;
            }),
            // Plate Ink Id
            plateInkId = ko.observable(specifiedPlateInkId || undefined),
            // SimilarSections
            similarSections = ko.observable(specifiedSimilarSections || 0),
            // Section Cost Centres
            sectionCostCentres = ko.observableArray([]),
            // Select Stock Item
            selectStock = function (stockItem) {
                if (!stockItem || stockItemId() === stockItem.id) {
                    return;
                }

                stockItemId(stockItem.id);
                stockItemName(stockItem.name);
            },
            // Select Press
            selectPress = function (press) {
                if (!press || pressId() === press.id) {
                    return;
                }

                pressId(press.id);
                pressName(press.name);
            },
            // Swap Section Height and Width
            swapSectionHeightWidth = function() {
                var sectionHeight = sectionSizeHeight();
                sectionSizeHeight(sectionSizeWidth());
                sectionSizeWidth(sectionHeight);
            },
            // Swap Item Size Height and Width
            swapItemHeightWidth = function () {
                var itemHeight = itemSizeHeight();
                itemSizeHeight(itemSizeWidth());
                itemSizeWidth(itemHeight);
            },
            // Errors
            errors = ko.validation.group({
                name: name,
                pressId: pressId,
                stockItemId: stockItemId
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // True if the Item Section has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                stockItemId: stockItemId,
                pressId: pressId,
                name: name,
                sectionSizeId: sectionSizeId,
                itemSizeId: itemSizeId,
                isSectionSizeCustom: isSectionSizeCustom,
                isItemSizeCustom: isItemSizeCustom,
                sectionSizeHeight: sectionSizeHeight,
                sectionSizeWidth: sectionSizeWidth,
                itemSizeHeight: itemSizeHeight,
                itemSizeWidth: itemSizeWidth,
                isDoubleSided: isDoubleSided,
                isWorknTurn: isWorknTurn,
                printViewLayoutPortrait: printViewLayoutPortrait,
                printViewLayoutLandscape: printViewLayoutLandscape,
                plateInkId: plateInkId,
                similarSections: similarSections
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
                    ItemSectionId: id(),
                    SectionName: name(),
                    SectionNo: sectionNo(),
                    StockItemID1: stockItemId(),
                    PressId: pressId(),
                    ItemId: itemId(),
                    SectionSizeId: sectionSizeId(),
                    ItemSizeId: itemSizeId(),
                    IsSectionSizeCustom: isSectionSizeCustom(),
                    IsItemSizeCustom: isItemSizeCustom(),
                    SectionSizeHeight: sectionSizeHeight(),
                    SectionSizeWidth: sectionSizeWidth(),
                    ItemSizeHeight: itemSizeHeight(),
                    ItemSizeWidth: itemSizeWidth()
                };
            };

        return {
            id: id,
            stockItemId: stockItemId,
            pressId: pressId,
            stockItemName: stockItemName,
            pressName: pressName,
            name: name,
            itemId: itemId,
            sectionNo: sectionNo,
            sectionSizeId: sectionSizeId,
            itemSizeId: itemSizeId,
            isSectionSizeCustom: isSectionSizeCustom,
            isItemSizeCustom: isItemSizeCustom,
            sectionSizeHeight: sectionSizeHeight,
            sectionSizeWidth: sectionSizeWidth,
            itemSizeHeight: itemSizeHeight,
            itemSizeWidth: itemSizeWidth,
            guillotineId: guillotineId,
            qty1: qty1,
            qty2: qty2,
            qty3: qty3,
            qty1Profit: qty1Profit,
            qty2Profit: qty2Profit,
            qty3Profit: qty3Profit,
            baseCharge1: baseCharge1,
            baseCharge2: baseCharge2,
            baseCharge3: baseCharge3,
            includeGutter: includeGutter,
            filmId: filmId,
            isPaperSupplied: isPaperSupplied,
            side1PlateQty: side1PlateQty,
            side2PlateQty: side2PlateQty,
            isPlateSupplied: isPlateSupplied,
            isDoubleSided: isDoubleSided,
            isWorknTurn: isWorknTurn,
            doubleWorknTurn: doubleWorknTurn,
            printViewLayoutPortrait: printViewLayoutPortrait,
            printViewLayoutLandscape: printViewLayoutLandscape,
            numberUp: numberUp,
            plateInkId: plateInkId,
            similarSections: similarSections,
            sectionCostCentres: sectionCostCentres,
            selectStock: selectStock,
            selectPress: selectPress,
            swapSectionHeightWidth: swapSectionHeightWidth,
            swapItemHeightWidth: swapItemHeightWidth,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Section Cost Centre Entity
    SectionCostCentre = function (specifiedId, specifiedName, specifiedCostCentreId, specifiedCostCentreType, specifiedOrder, specifiedIsDirectCost,
        specifiedIsOptionalExtra, specifiedIsPurchaseOrderRaised, specifiedStatus, specifiedQty1Charge, specifiedQty2Charge, specifiedQty3Charge,
        specifiedQty1MarkUpID, specifiedQty2MarkUpID, specifiedQty3MarkUpID, specifiedQty1MarkUpValue, specifiedQty2MarkUpValue, specifiedQty3MarkUpValue,
        specifiedQty1NetTotal, specifiedQty2NetTotal, specifiedQty3NetTotal, specifiedQty1, specifiedQty2, specifiedQty3, specifiedCostCentreName,
        specifiedItemSectionId) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // name
            name = ko.observable(specifiedName || undefined),
            // Cost Centre Id
            costCentreId = ko.observable(specifiedCostCentreId || undefined),
            // Cost Centre Name
            costCentreName = ko.observable(specifiedCostCentreName || undefined),
            // Cost Centre Type
            costCentreType = ko.observable(specifiedCostCentreType || undefined),
            // Order
            order = ko.observable(specifiedOrder || undefined),
            // Is Direct Cost
            isDirectCost = ko.observable(specifiedIsDirectCost || undefined),
            // Is Optional Extra
            isOptionalExtra = ko.observable(specifiedIsOptionalExtra || undefined),
            // Is PurchaseOrder Raised
            isPurchaseOrderRaised = ko.observable(specifiedIsPurchaseOrderRaised || undefined),
            // Status
            status = ko.observable(specifiedStatus || undefined),
            // Qty1Charge
            qty1Charge = ko.observable(specifiedQty1Charge || undefined),
            // Qty2Charge
            qty2Charge = ko.observable(specifiedQty2Charge || undefined),
            // Qty3Charge
            qty3Charge = ko.observable(specifiedQty3Charge || undefined),
            // qty1MarkUpId
            qty1MarkUpId = ko.observable(specifiedQty1MarkUpID || undefined),
            // qty2MarkUpId
            qty2MarkUpId = ko.observable(specifiedQty2MarkUpID || undefined),
            // qty3MarkUpId
            qty3MarkUpId = ko.observable(specifiedQty3MarkUpID || undefined),
            // Qty1 Markup Value
            qty1MarkUpValue = ko.observable(specifiedQty1MarkUpValue || undefined),
            // qty2MarkUpValue
            qty2MarkUpValue = ko.observable(specifiedQty2MarkUpValue || undefined),
            // qty3MarkUpValue
            qty3MarkUpValue = ko.observable(specifiedQty3MarkUpValue || undefined),
            // Qty1
            qty1 = ko.observable(specifiedQty1 || undefined),
            // Qty2
            qty2 = ko.observable(specifiedQty2 || undefined),
            // Qty3
            qty3 = ko.observable(specifiedQty3 || undefined),
            // Qty1 Profit
            qty1NetTotal = ko.observable(specifiedQty1NetTotal || undefined),
            // Qty2NetTotal
            qty2NetTotal = ko.observable(specifiedQty2NetTotal || undefined),
            // Qty3NetTotal
            qty3NetTotal = ko.observable(specifiedQty3NetTotal || undefined),
            // Item Section Id
            itemSectionId = ko.observable(specifiedItemSectionId || undefined),
            // Section Cost Centre Details
            sectionCostCentreDetails = ko.observableArray([]),
            // Section Cost Centre Resources
            sectionCostCentreResources = ko.observableArray([]),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // True if the Item Section has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
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
                    ItemSectionId: id()
                };
            };

        return {
            id: id,
            name: name,
            costCentreId: costCentreId,
            costCentreName: costCentreName,
            costCentreType: costCentreType,
            order: order,
            isDirectCost: isDirectCost,
            isOptionalExtra: isOptionalExtra,
            isPurchaseOrderRaised: isPurchaseOrderRaised,
            status: status,
            qty1Charge: qty1Charge,
            qty2Charge: qty2Charge,
            qty3Charge: qty3Charge,
            qty1: qty1,
            qty2: qty2,
            qty3: qty3,
            qty1MarkUpId: qty1MarkUpId,
            qty2MarkUpId: qty2MarkUpId,
            qty3MarkUpId: qty3MarkUpId,
            qty1MarkUpValue: qty1MarkUpValue,
            qty2MarkUpValue: qty2MarkUpValue,
            qty3MarkUpValue: qty3MarkUpValue,
            qty1NetTotal: qty1NetTotal,
            qty2NetTotal: qty2NetTotal,
            qty3NetTotal: qty3NetTotal,
            itemSectionId: itemSectionId,
            sectionCostCentreDetails: sectionCostCentreDetails,
            sectionCostCentreResources: sectionCostCentreResources,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Pre Payment
    PrePayment = function (specifiedPrePaymentId, specifiedCustomerId, specifiedOrderId, specifiedAmount, specifiedPaymentDate, specifiedPaymentMethodId,
        specifiedPaymentMethodName, specifiedReferenceCode, specifiedPaymentDescription) {
        var // Unique key
             prePaymentId = ko.observable(specifiedPrePaymentId),
             // Customer Id
              customerId = ko.observable(specifiedCustomerId),
              // Order Id
              orderId = ko.observable(specifiedOrderId),
              //Amount
              amount = ko.observable(specifiedAmount).extend({ number: true }),
              //Payment Date
              paymentDate = ko.observable(specifiedPaymentDate ? moment(specifiedPaymentDate).toDate() : undefined),
              // Payment Method Id
             paymentMethodId = ko.observable(specifiedPaymentMethodId),
             //Payment Method Name
             paymentMethodName = ko.observable(specifiedPaymentMethodName),
             // Reference Code
             referenceCode = ko.observable(specifiedReferenceCode),
             // Payment Description
             paymentDescription = ko.observable(specifiedPaymentDescription),
// ReSharper disable UnusedLocals
             customerAddress = ko.observable(),
// ReSharper restore UnusedLocals
              // Formatted Payment Date
             formattedPaymentDate = ko.computed({
                 read: function () {
                     return paymentDate() !== undefined ? moment(paymentDate(), ist.datePattern).toDate() : undefined;
                 }
             }),

               // Errors
            errors = ko.validation.group({
                amount: amount
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
              dirtyFlag = new ko.dirtyFlag({
                  prePaymentId: prePaymentId,
                  customerId: customerId,
                  orderId: orderId,
                  amount: amount,
                  paymentDate: paymentDate,
                  paymentMethodId: paymentMethodId,
                  paymentMethodName: paymentMethodName,
                  referenceCode: referenceCode,
                  paymentDescription: paymentDescription
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
                    PrePaymentId: prePaymentId(),
                    CustomerId: customerId(),
                    OrderId: orderId(),
                    Amount: amount(),
                    PaymentDate: paymentDate() ? moment(paymentDate()).format(ist.utcFormat) : null,
                    PaymentMethodId: paymentMethodId(),
                    ReferenceCode: referenceCode(),
                    PaymentDescription: paymentDescription(),
                };
            };

        return {
            prePaymentId: prePaymentId,
            customerId: customerId,
            orderId: orderId,
            amount: amount,
            paymentDate: paymentDate,
            paymentMethodId: paymentMethodId,
            paymentMethodName: paymentMethodName,
            referenceCode: referenceCode,
            paymentDescription: paymentDescription,
            formattedPaymentDate: formattedPaymentDate,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Shipping Information
     ShippingInformation = function (specifiedShippingId, specifiedItemId, specifiedAddressId, specifiedQuantity, specifiedPrice, specifiedDeliveryNoteRaised,
        specifiedDeliveryDate) {
         var // Unique key
             shippingId = ko.observable(specifiedShippingId),
             // Item ID
             itemId = ko.observable(specifiedItemId).extend({ required: true }),
             // Address ID
             addressId = ko.observable(specifiedAddressId),
             // Quantity
             quantity = ko.observable(specifiedQuantity).extend({ number: true, required: true }),
             // Price
             price = ko.observable(specifiedPrice),
             //Deliver Not Raised Flag
             deliveryNoteRaised = ko.observable(specifiedDeliveryNoteRaised !== undefined ? specifiedDeliveryNoteRaised : false),
             // Deliver Date
             deliveryDate = ko.observable((specifiedDeliveryDate === undefined || specifiedDeliveryDate === null) ? moment().toDate() : moment(specifiedDeliveryDate, ist.utcFormat).toDate()),
              // Formatted Delivery Date
              formattedDeliveryDate = ko.computed({
                  read: function () {
                      return deliveryDate() !== undefined ? moment(deliveryDate(), ist.datePattern).toDate() : new Date();
                  }
              }),
              // Item Name
              itemName = ko.observable(),
              // Address Name
              addressName = ko.observable(),
              //
              isSelected = ko.observable(false),
             // Errors
             errors = ko.validation.group({
                 quantity: quantity,
                 itemId: itemId
             }),
             // Is Valid
             isValid = ko.computed(function () {
                 return errors().length === 0;
             }),
             dirtyFlag = new ko.dirtyFlag({
                 shippingId: shippingId,
                 itemId: itemId,
                 addressId: addressId,
                 quantity: quantity,
                 price: price,
                 deliveryNoteRaised: deliveryNoteRaised,
                 deliveryDate: deliveryDate
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
                     ShippingId: shippingId(),
                     ItemId: itemId(),
                     AddressId: addressId(),
                     Quantity: quantity(),
                     DeliveryDate: deliveryDate() ? moment(deliveryDate()).format(ist.utcFormat) : null,
                     Price: price(),
                     DeliveryNoteRaised: deliveryNoteRaised(),
                 };
             };

         return {
             shippingId: shippingId,
             itemId: itemId,
             addressId: addressId,
             quantity: quantity,
             price: price,
             deliveryNoteRaised: deliveryNoteRaised,
             deliveryDate: deliveryDate,
             formattedDeliveryDate: formattedDeliveryDate,
             itemName: itemName,
             addressName: addressName,
             isSelected: isSelected,
             errors: errors,
             isValid: isValid,
             dirtyFlag: dirtyFlag,
             hasChanges: hasChanges,
             reset: reset,
             convertToServerData: convertToServerData
         };
     },

    // Section Flag Entity        
    SectionFlag = function (specifiedId, specifiedFlagName, specifiedFlagColor) {
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
    Address = function (specifiedId, specifiedName, specifiedAddress1, specifiedAddress2, specifiedTelephone1) {
        return {
            id: specifiedId,
            name: specifiedName,
            address1: specifiedAddress1 || "",
            address2: specifiedAddress2 || "",
            telephone1: specifiedTelephone1 || ""
        };
    },

    // Item Stock Option Entity
    ItemStockOption = function (specifiedId, specifiedStockLabel, specifiedStockId, specifiedStockItemName, specifiedStockItemDescription, specifiedImage,
        specifiedOptionSequence, specifiedItemId, callbacks) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // Label
            label = ko.observable(specifiedStockLabel || undefined).extend({ required: true }),
            // Stock Item Id
            stockItemId = ko.observable(specifiedStockId || undefined),
            // Stock Item Name
            stockItemName = ko.observable(specifiedStockItemName || undefined),
            // Stock Item Description
            stockItemDescription = ko.observable(specifiedStockItemDescription || undefined),
            // image
            image = ko.observable(specifiedImage || undefined),
            // file source
            fileSource = ko.observable(),
            // file name
            fileName = ko.observable(),
            // Option Sequence
            optionSequence = ko.observable(specifiedOptionSequence || undefined),
            // Item Id
            itemId = ko.observable(specifiedItemId || undefined),
            // Item Addon Cost Centers
            itemAddonCostCentres = ko.observableArray([]),
            // Active Video Item
            activeItemAddonCostCentre = ko.observable(ItemAddonCostCentre.Create({}, callbacks)),
            // Added ItemAddonCostCentre Counter
            itemAddonCostCentreCounter = -1,
            // On Add ItemAddonCostCentre
            onAddItemAddonCostCentre = function () {
                activeItemAddonCostCentre(ItemAddonCostCentre.Create({ ProductAddOnId: 0, ItemStockOptionId: id() }, callbacks));
            },
            onEditItemAddonCostCentre = function (itemAddonCostCentre) {
                activeItemAddonCostCentre(itemAddonCostCentre);
            },
            // Save ItemAddonCostCentre
            saveItemAddonCostCentre = function () {
                if (activeItemAddonCostCentre().id() === 0) { // Add
                    activeItemAddonCostCentre().id(itemAddonCostCentreCounter);
                    addItemAddonCostCentre(activeItemAddonCostCentre());
                    itemAddonCostCentreCounter -= 1;
                    return;
                }
            },
            // Add Item Addon Cost Center
            addItemAddonCostCentre = function (itemAddonCostCentre) {
                itemAddonCostCentres.splice(0, 0, itemAddonCostCentre);
            },
            // Remove ItemAddon Cost Centre
            removeItemAddonCostCentre = function (itemAddonCostCentre) {
                itemAddonCostCentres.remove(itemAddonCostCentre);
            },
            // Select Stock Item
            selectStock = function (stockItem) {
                if (!stockItem || stockItemId() === stockItem.id) {
                    return;
                }

                stockItemId(stockItem.id);
                stockItemName(stockItem.name);
                stockItemDescription(stockItem.description);
                label(stockItem.name);
            },
            // On Select File
            onSelectImage = function (file, data) {
                image(data);
                fileSource(data);
                fileName(file.name);
            },
            
            // Errors
            errors = ko.validation.group({
                label: label
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0 && itemAddonCostCentres.filter(function (itemAddonCostCentre) {
                    return !itemAddonCostCentre.isValid();
                }).length === 0;
            }),
            // True if the Item Vdp Price has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                stockItemId: stockItemId,
                label: label,
                image: image,
                itemAddonCostCentres: itemAddonCostCentres
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty() || itemAddonCostCentres.find(function (itemAddonCostCentre) {
                    return itemAddonCostCentre.hasChanges();
                }) != null;
            }),
            // Reset
            reset = function () {
                // Reset Item Addon Cost Centres State to Un-Modified
                itemAddonCostCentres.each(function (itemAddonCostCentre) {
                    return itemAddonCostCentre.reset();
                });
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    ItemStockOptionId: id(),
                    StockLabel: label(),
                    StockId: stockItemId(),
                    ItemId: itemId(),
                    FileSource: fileSource(),
                    FileName: fileName(),
                    OptionSequence: optionSequence(),
                    ItemAddOnCostCentres: itemAddonCostCentres.map(function (itemAddonCostCentre) {
                        return itemAddonCostCentre.convertToServerData();
                    })
                };
            };

        return {
            id: id,
            stockItemId: stockItemId,
            label: label,
            stockItemName: stockItemName,
            stockItemDescription: stockItemDescription,
            itemId: itemId,
            image: image,
            fileSource: fileSource,
            fileName: fileName,
            optionSequence: optionSequence,
            activeItemAddonCostCentre: activeItemAddonCostCentre,
            itemAddonCostCentres: itemAddonCostCentres,
            addItemAddonCostCentre: addItemAddonCostCentre,
            removeItemAddonCostCentre: removeItemAddonCostCentre,
            onAddItemAddonCostCentre: onAddItemAddonCostCentre,
            onEditItemAddonCostCentre: onEditItemAddonCostCentre,
            saveItemAddonCostCentre: saveItemAddonCostCentre,
            selectStock: selectStock,
            onSelectImage: onSelectImage,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Item Price Matrix Entity
    ItemPriceMatrix = function (specifiedId, specifiedQuantity, specifiedQtyRangedFrom, specifiedQtyRangedTo, specifiedPricePaperType1, specifiedPricePaperType2,
        specifiedPricePaperType3, specifiedPriceStockType4, specifiedPriceStockType5, specifiedPriceStockType6, specifiedPriceStockType7, specifiedPriceStockType8,
        specifiedPriceStockType9, specifiedPriceStockType10, specifiedPriceStockType11, specifiedFlagId, specifiedSupplierId, specifiedSupplierSequence, specifiedItemId) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId || 0),
            // Quantity
            quantity = ko.observable(specifiedQuantity || 0),
            // Qty Ranged From
            qtyRangedFrom = ko.observable(specifiedQtyRangedFrom || 0),
            // Qty Ranged To
            qtyRangedTo = ko.observable(specifiedQtyRangedTo || 0),
            // Flag Id
            flagId = ko.observable(specifiedFlagId || undefined),
            // Supplier Id
            supplierId = ko.observable(specifiedSupplierId || undefined),
            // Supplier Sequence
            supplierSequence = ko.observable(specifiedSupplierSequence || undefined),
            // Price Paper Type1
            pricePaperType1 = ko.observable(specifiedPricePaperType1 || 0),
            // Price Paper Type1 Ui
            pricePaperType1Ui = ko.computed(function () {
                if (!pricePaperType1()) {
                    return '$ 0.00';
                }

                return '$ ' + pricePaperType1();
            }),
            // Price Paper Type2
            pricePaperType2 = ko.observable(specifiedPricePaperType2 || 0),
            // Price Paper Type2 Ui
            pricePaperType2Ui = ko.computed(function () {
                if (!pricePaperType2()) {
                    return '$ 0.00';
                }

                return '$ ' + pricePaperType2();
            }),
            // Price Paper Type3
            pricePaperType3 = ko.observable(specifiedPricePaperType3 || 0),
            // Price Paper Type3 Ui
            pricePaperType3Ui = ko.computed(function () {
                if (!pricePaperType3()) {
                    return '$ 0.00';
                }

                return '$ ' + pricePaperType3();
            }),
            // Price Stock Type4
            priceStockType4 = ko.observable(specifiedPriceStockType4 || 0),
            // Price Stock Type4 Ui
            priceStockType4Ui = ko.computed(function () {
                if (!priceStockType4()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType4();
            }),
            // Price Stock Type5
            priceStockType5 = ko.observable(specifiedPriceStockType5 || 0),
            // Price Stock Type5 Ui
            priceStockType5Ui = ko.computed(function () {
                if (!priceStockType5()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType5();
            }),
            // Price Stock Type6
            priceStockType6 = ko.observable(specifiedPriceStockType6 || 0),
            // Price Stock Type6 Ui
            priceStockType6Ui = ko.computed(function () {
                if (!priceStockType6()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType6();
            }),
            // Price Stock Type7
            priceStockType7 = ko.observable(specifiedPriceStockType7 || 0),
            // Price Stock Type7 Ui
            priceStockType7Ui = ko.computed(function () {
                if (!priceStockType7()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType7();
            }),
            // Price Stock Type8
            priceStockType8 = ko.observable(specifiedPriceStockType8 || 0),
            // Price Stock Type8 Ui
            priceStockType8Ui = ko.computed(function () {
                if (!priceStockType8()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType8();
            }),
            // Price Stock Type9
            priceStockType9 = ko.observable(specifiedPriceStockType9 || 0),
            // Price Stock Type9 Ui
            priceStockType9Ui = ko.computed(function () {
                if (!priceStockType4()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType9();
            }),
            // Price Stock Type10
            priceStockType10 = ko.observable(specifiedPriceStockType10 || 0),
            // Price Stock Type10 Ui
            priceStockType10Ui = ko.computed(function () {
                if (!priceStockType10()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType10();
            }),
            // Price Stock Type11
            priceStockType11 = ko.observable(specifiedPriceStockType11 || 0),
            // Price Stock Type11 Ui
            priceStockType11Ui = ko.computed(function () {
                if (!priceStockType11()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType11();
            }),
            // Item Id
            itemId = ko.observable(specifiedItemId || 0),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // True if the Item Vdp Price has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                quantity: quantity,
                qtyRangedFrom: qtyRangedFrom,
                qtyRangedTo: qtyRangedTo,
                flagId: flagId,
                supplierId: supplierId,
                supplierSequence: supplierSequence,
                pricePaperType1: pricePaperType1,
                pricePaperType2: pricePaperType2,
                pricePaperType3: pricePaperType3,
                priceStockType4: priceStockType4,
                priceStockType5: priceStockType5,
                priceStockType6: priceStockType6,
                priceStockType7: priceStockType7,
                priceStockType8: priceStockType8,
                priceStockType9: priceStockType9,
                priceStockType10: priceStockType10,
                priceStockType11: priceStockType11
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
                    PriceMatrixId: id(),
                    ItemId: itemId(),
                    Quantity: quantity(),
                    QtyRangeFrom: qtyRangedFrom(),
                    QtyRangeTo: qtyRangedTo(),
                    FlagId: flagId(),
                    SupplierId: supplierId(),
                    SupplierSequence: supplierSequence(),
                    PricePaperType1: pricePaperType1(),
                    PricePaperType2: pricePaperType2(),
                    PricePaperType3: pricePaperType3(),
                    PriceStockType4: priceStockType4(),
                    PriceStockType5: priceStockType5(),
                    PriceStockType6: priceStockType6(),
                    PriceStockType7: priceStockType7(),
                    PriceStockType8: priceStockType8(),
                    PriceStockType9: priceStockType9(),
                    PriceStockType10: priceStockType10(),
                    PriceStockType11: priceStockType11()
                };
            };

        return {
            id: id,
            itemId: itemId,
            quantity: quantity,
            qtyRangedFrom: qtyRangedFrom,
            qtyRangedTo: qtyRangedTo,
            flagId: flagId,
            supplierId: supplierId,
            supplierSequence: supplierSequence,
            pricePaperType1: pricePaperType1,
            pricePaperType2: pricePaperType2,
            pricePaperType3: pricePaperType3,
            priceStockType4: priceStockType4,
            priceStockType5: priceStockType5,
            priceStockType6: priceStockType6,
            priceStockType7: priceStockType7,
            priceStockType8: priceStockType8,
            priceStockType9: priceStockType9,
            priceStockType10: priceStockType10,
            priceStockType11: priceStockType11,
            pricePaperType1Ui: pricePaperType1Ui,
            pricePaperType2Ui: pricePaperType2Ui,
            pricePaperType3Ui: pricePaperType3Ui,
            priceStockType4Ui: priceStockType4Ui,
            priceStockType5Ui: priceStockType5Ui,
            priceStockType6Ui: priceStockType6Ui,
            priceStockType7Ui: priceStockType7Ui,
            priceStockType8Ui: priceStockType8Ui,
            priceStockType9Ui: priceStockType9Ui,
            priceStockType10Ui: priceStockType10Ui,
            priceStockType11Ui: priceStockType11Ui,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Item Addon Cost Centre Entity
    ItemAddonCostCentre = function (specifiedId, specifiedIsMandatory, specifiedItemStockOptionId, specifiedCostCentreId, specifiedCostCentreName,
        specifiedCostCentreType, specifiedTotalPrice, callbacks) {
        // ReSharper restore InconsistentNaming
        var
            // self reference
            self,
            // Unique key
            id = ko.observable(specifiedId),
            // is Mandatory
            isMandatory = ko.observable(specifiedIsMandatory || undefined),
            // Cost Centre Id
            internalCostCentreId = ko.observable(specifiedCostCentreId || undefined),
            // Cost Centre Name
            costCentreName = ko.observable(specifiedCostCentreName || undefined),
            // Cost Centre Type
            costCentreType = ko.observable(specifiedCostCentreType || undefined),
            // Total Price
            totalPrice = ko.observable(specifiedTotalPrice || undefined),
            // Cost Centre Id - On Change
            costCentreId = ko.computed({
                read: function () {
                    return internalCostCentreId();
                },
                write: function (value) {
                    if (!value || value === internalCostCentreId()) {
                        return;
                    }

                    internalCostCentreId(value);
                    if (callbacks && callbacks.onCostCentreChange && typeof callbacks.onCostCentreChange === "function") {
                        callbacks.onCostCentreChange(value, self);
                    }
                }
            }),
            // Item Stock Option Id
            itemStockOptionId = ko.observable(specifiedItemStockOptionId || 0),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // True if the Item Vdp Price has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                isMandatory: isMandatory,
                costCentreId: costCentreId
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
                    ProductAddOnId: id(),
                    IsMandatory: isMandatory(),
                    CostCentreId: costCentreId(),
                    ItemStockOptionId: itemStockOptionId()
                };
            };

        self = {
            id: id,
            itemStockOptionId: itemStockOptionId,
            costCentreId: costCentreId,
            costCentreName: costCentreName,
            costCentreType: costCentreType,
            totalPrice: totalPrice,
            isMandatory: isMandatory,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };

        return self;
    },

    // Company Contact Entity
    CompanyContact = function (specifiedId, specifiedName, specifiedEmail) {
// ReSharper restore InconsistentNaming
        return {
            id: specifiedId,
            name: specifiedName,
            email: specifiedEmail || ""
        };
    },
    
    // Paper Size Entity
// ReSharper disable InconsistentNaming
    PaperSize = function (specifiedId, specifiedName, specifiedHeight, specifiedWidth) {
// ReSharper restore InconsistentNaming
        return {
            id: specifiedId,
            name: specifiedName,
            height: specifiedHeight,
            width: specifiedWidth
        };
    },
    
    // Ink Plate Side Entity
// ReSharper disable InconsistentNaming
    InkPlateSide = function (specifiedId, specifiedName, specifiedIsDoubleSided, specifiedPlateInkSide1, specifiedPlateInkSide2) {
// ReSharper restore InconsistentNaming
        return {
            id: specifiedId,
            name: specifiedName,
            isDoubleSided: specifiedIsDoubleSided,
            plateInkSide1: specifiedPlateInkSide1,
            plateInkSide2: specifiedPlateInkSide2
        };
    };

    // Section Cost Centre Factory
    SectionCostCentre.Create = function (source) {
        var sectionCostCentre = new SectionCostCentre(source.SectionCostcentreId, source.Name, source.CostCentreId, source.CostCenterType, source.Order,
            source.IsDirectCost, source.IsOptionalExtra, source.IsPurchaseOrderRaised, source.Status, source.Qty1Charge, source.Qty2Charge, source.Qty3Charge,
            source.Qty1MarkUpID, source.Qty2MarkUpID, source.Qty3MarkUpID, source.Qty1MarkUpValue, source.Qty2MarkUpValue, source.Qty3MarkUpValue,
            source.Qty1NetTotal, source.Qty2NetTotal, source.Qty3NetTotal, source.Qty1, source.Qty2, source.Qty3, source.CostCentreName,
            source.ItemSectionId);

        // Map Section Cost Centre Details if Any
        if (source.SectionCostCentreDetails && source.SectionCostCentreDetails.length > 0) {

        }

        // Map Section Cost Resources if Any
        if (source.SectionCostCentreResources && source.SectionCostCentreResources.length > 0) {

        }

        return sectionCostCentre;
    };

    // Item Section Factory
    ItemSection.Create = function (source) {
        var itemSection = new ItemSection(source.ItemSectionId, source.SectionNo, source.SectionName, source.SectionSizeId, source.ItemSizeId,
            source.IsSectionSizeCustom, source.SectionSizeHeight, source.SectionSizeWidth, source.IsItemSizeCustom, source.ItemSizeHeight,
            source.ItemSizeWidth, source.PressId, source.StockItemId1, source.StockItem1Name, source.PressName, source.GuillotineId, source.Qty1,
            source.Qty2, source.Qty3, source.Qty1Profit, source.Qty2Profit, source.Qty3Profit, source.BaseCharge1, source.BaseCharge2,
            source.BaseCharge3, source.IncludeGutter, source.FilmId, source.IsPaperSupplied, source.Side1PlateQty, source.Side2PlateQty, source.IsPlateSupplied,
            source.ItemId);

        // Map Section Cost Centres if Any
        if (source.SectionCostcentres && source.SectionCostcentres.length > 0) {
            var sectionCostcentres = [];

            _.each(source.SectionCostcentres, function (sectionCostCentre) {
                sectionCostcentres.push(SectionCostCentre.Create(sectionCostCentre));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(itemSection.sectionCostCentres(), sectionCostcentres);
            itemSection.sectionCostCentres.valueHasMutated();
        }

        return itemSection;
    };

    // Item Factory
    Item.Create = function (source) {
        var item = new Item(source.ItemId, source.ItemName, source.ItemCode, source.ProductName, source.ProductCode,
            source.JobDescriptionTitle1, source.JobDescription1, source.JobDescriptionTitle2,
            source.JobDescription2, source.JobDescriptionTitle3, source.JobDescription3, source.JobDescriptionTitle4, source.JobDescription4,
            source.JobDescriptionTitle5, source.JobDescription5, source.JobDescriptionTitle6, source.JobDescription6, source.JobDescriptionTitle7,
            source.JobDescription7, source.IsQtyRanged, source.DefaultItemTax, source.StatusId, source.Status, source.Qty1, source.Qty1NetTotal,
            source.ItemNotes, source.ProductCategories, source.JobCode, source.JobCreationDateTime, source.JobManagerId, source.JobActtualStartDateTime,
            source.JobActualCompletionDateTime, source.JobProgressedBy, source.JobCardPrintedBy, source.NominalCodeId,source.JobStatusId, source.InvoiceDescription,
            source.Qty1MarkUpId1, source.Qty2MarkUpId2, source.Qty3MarkUpId3, source.Qty2NetTotal, source.Qty3NetTotal, source.Qty1Tax1Value, source.Qty2Tax1Value,
            source.Qty3Tax1Value, source.Qty1GrossTotal, source.Qty2GrossTotal, source.Qty3GrossTotal, source.Tax1);

        // Map Item Sections if any
        if (source.ItemSections && source.ItemSections.length > 0) {
            var itemSections = [];

            _.each(source.ItemSections, function (itemSection) {
                itemSections.push(ItemSection.Create(itemSection));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(item.itemSections(), itemSections);
            item.itemSections.valueHasMutated();
        }

        // Return item with dirty state if New
        if (!item.id()) {
            return item;
        }

        // Reset State to Un-Modified
        item.reset();

        return item;
    };

    // Estimate Factory
    Estimate.Create = function (source) {
        var estimate = new Estimate(source.EstimateId, source.EstimateCode, source.EstimateName, source.CompanyId, source.CompanyName, source.ItemsCount,
        source.CreationDate, source.FlagColor, source.SectionFlagId, source.OrderCode, source.IsEstimate, source.ContactId, source.AddressId, source.IsDirectSale,
        source.IsOfficialOrder, source.IsCreditApproved, source.OrderDate, source.StartDeliveryDate, source.FinishDeliveryDate, source.HeadNotes,
        source.ArtworkByDate, source.DataByDate, source.PaperByDate, source.TargetBindDate, source.XeroAccessCode, source.TargetPrintDate,
        source.OrderCreationDateTime, source.SalesAndOrderManagerId, source.SalesPersonId, source.SourceId, source.CreditLimitForJob, source.CreditLimitSetBy,
        source.CreditLimitSetOnDateTime, source.IsJobAllowedWOCreditCheck, source.AllowJobWOCreditCheckSetOnDateTime, source.AllowJobWOCreditCheckSetBy,
        source.CustomerPo, source.OfficialOrderSetBy, source.OfficialOrderSetOnDateTime);
        estimate.statusId(source.StatusId);
        // Map Items if any
        if (source.Items && source.Items.length > 0) {
            var items = [];

            _.each(source.Items, function (item) {
                items.push(Item.Create(item));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(estimate.items(), items);
            estimate.items.valueHasMutated();
        }

        // Return item with dirty state if New
        if (!estimate.id()) {
            return estimate;
        }

        // Reset State to Un-Modified
        estimate.reset();

        return estimate;
    };
    // #region __________________  COST CENTRE   ______________________

    // ReSharper disable once InconsistentNaming
    var costCentre = function (specifiedId, specifiedname,
        specifiedDes, specifiedSetupcost, specifiedPpq, specifiedquantity1, specifiedquantity2, specifiedquantity3) {

        var self,
            id = ko.observable(specifiedId),
            name = ko.observable(specifiedname),
            quantity1 = ko.observable(specifiedquantity1),
            quantity2 = ko.observable(specifiedquantity2),
            quantity3 = ko.observable(specifiedquantity3),
            description = ko.observable(specifiedDes),
            setupCost = ko.observable(specifiedSetupcost),
            pricePerUnitQuantity = ko.observable(specifiedPpq),
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
                    PricePerUnitQuantity: pricePerUnitQuantity()
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
            pricePerUnitQuantity: pricePerUnitQuantity,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };

    costCentre.Create = function (source) {
        var cost = new costCentre(
            source.CostCentreId,
            source.Name,
            source.Description,
            source.SetupCost,
            source.PricePerUnitQuantity
            );
        return cost;
    };
    // #endregion __________________  COST CENTRE   ______________________

    // #region __________________  I N V E N T O R Y   ______________________

    // ReSharper disable once InconsistentNaming
    var Inventory = function (specifiedId, specifiedname,
        specifiedWeight, specifiedPackageQty, specifiedPerQtyQty, specifiedPrice) {

        var self,
            stockItemId = ko.observable(specifiedId),
            itemName = ko.observable(specifiedname),
            itemWeight = ko.observable(specifiedWeight),
            packageQty = ko.observable(specifiedPackageQty),
            perQtyQty = ko.observable(specifiedPerQtyQty),
            price = ko.observable(specifiedPrice),
            errors = ko.validation.group({

            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                stockItemId: stockItemId,
                itemName: itemName,
                itemWeight: itemWeight,
                packageQty: packageQty,
                perQtyQty: perQtyQty,
                price: price
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    StockItemId: stockItemId(),
                    ItemName: itemName(),
                    ItemWeight: itemWeight(),
                    PackageQty: packageQty(),
                    PerQtyQty: perQtyQty(),
                    Price: price()
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            stockItemId: stockItemId,
            itemName: itemName,
            itemWeight: itemWeight,
            packageQty: packageQty,
            perQtyQty: perQtyQty,
            price: price,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };

    Inventory.Create = function (source) {
        var inventory = new Inventory(
            source.StockItemId,
            source.ItemName,
            source.ItemWeight,
            source.PackageQty,
            source.perQtyQty
           // source.Price
            );
        return inventory;
    };
    // #endregion __________________   I N V E N T O R Y    ______________________

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

    // Pre Payment Factory
    PrePayment.Create = function (source) {
        return new PrePayment(source.PrePaymentId, source.CustomerId, source.OrderId, source.Amount, source.PaymentDate, source.PaymentMethodId,
            source.PaymentMethodName, source.ReferenceCode, source.PaymentDescription);
    };

    ShippingInformation.Create = function (source) {
        return new ShippingInformation(source.ShippingId, source.ItemId, source.AddressId, source.Quantity, source.Price, source.DeliveryNoteRaised, source.DeliveryDate);
    };

    // Item Stock Option Factory
    ItemStockOption.Create = function (source, callbacks) {
        var itemStockOption = new ItemStockOption(source.ItemStockOptionId, source.StockLabel, source.StockId, source.StockItemName, source.StockItemDescription,
            source.ImageUrlSource, source.OptionSequence, source.ItemId, callbacks);

        // If Item Addon CostCentres exists then add
        if (source.ItemAddOnCostCentres) {
            var itemAddonCostCentres = [];

            _.each(source.ItemAddOnCostCentres, function (itemAddonCostCenter) {
                itemAddonCostCentres.push(ItemAddonCostCentre.Create(itemAddonCostCenter, callbacks));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(itemStockOption.itemAddonCostCentres(), itemAddonCostCentres);
            itemStockOption.itemAddonCostCentres.valueHasMutated();
        }

        // Reset State to Un-Modified
        itemStockOption.reset();

        return itemStockOption;
    };

    // Item Price Matrix Factory
    ItemPriceMatrix.Create = function (source) {
        return new ItemPriceMatrix(source.PriceMatrixId, source.Quantity, source.QtyRangeFrom, source.QtyRangeTo, source.PricePaperType1, source.PricePaperType2,
            source.PricePaperType3, source.PriceStockType4, source.PriceStockType5, source.PriceStockType6, source.PriceStockType7, source.PriceStockType8,
            source.PriceStockType9, source.PriceStockType10, source.PriceStockType11, source.FlagId, source.SupplierId, source.SupplierSequence, source.ItemId);
    };

    // Item Addon Cost Centre Factory
    ItemAddonCostCentre.Create = function (source, callbacks) {
        return new ItemAddonCostCentre(source.ProductAddOnId, source.IsMandatory, source.ItemStockOptionId, source.CostCentreId, source.CostCentreName,
            source.CostCentreTypeName,source.TotalPrice, callbacks);
    };
    
    // Paper Size Factory
    PaperSize.Create = function (source) {
        return new PaperSize(source.PaperSizeId, source.Name, source.Height, source.Width);
    };
    
    // Ink Plate Side Factory
    InkPlateSide.Create = function (source) {
        return new InkPlateSide(source.PlateInkId, source.InkTitle, source.IsDoubleSided, source.PlateInkSide1, source.PlateInkSide2);
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
        Item: Item,
        // Item Section Constructor
        ItemSection: ItemSection,
        // Section Cost Centre Constructor
        SectionCostCentre: SectionCostCentre,
        // Status Enum
        Status: Status,
        // Cost Center
        costCentre: costCentre,
        // Pre Payment Constructor
        PrePayment: PrePayment,
        // Inventory
        Inventory: Inventory,
        // Shipping Information Constructor
        ShippingInformation: ShippingInformation,
        //Item Stock Option
        ItemStockOption: ItemStockOption,
        //Item Price Matrix
        ItemPriceMatrix: ItemPriceMatrix,
        //Item Add on Cost Centre
        ItemAddonCostCentre: ItemAddonCostCentre,
        // Paper Size Constructor
        PaperSize: PaperSize,
        // Ink Plate Side Constructor
        InkPlateSide: InkPlateSide
    };
});