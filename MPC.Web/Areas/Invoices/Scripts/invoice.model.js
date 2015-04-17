﻿/*
    Module with the model for the Invoice
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var // Status Enums
        // ReSharper disable InconsistentNaming
        Status = {
            // ReSharper restore InconsistentNaming
            ShoppingCart: 3,
            NotProgressedToJob: 17
        },
        // Invoice Entity
    // ReSharper disable InconsistentNaming
        Invoice = function (specifiedId, specifiedCode, specifiedType, specifiedName, specifiedCompanyId, specifiedCompanyName, specifiedNumberOfItems, specifiedContactId, specifiedOrderNo,
            specifiedStatus, specifiedTotal, specifiedInvoiceDate, specifiedAccountNo, specifiedTerms, specifiedAddressId, specifiedIsArchive,
            specifiedTaxValue, specifiedGrandTotal, specifiedFlagId,specifiedFlagColor, specifiedNotes, specifiedEstimateId,
            specifiedIsProforma, specifiedIsPrinted, specifiedSignedBy, specifiedHeadNotes, specifiedFootNotes, specifiedPostingDate) {
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
                invoiceDate = ko.observable(specifiedInvoiceDate ? moment(specifiedInvoiceDate).toDate() : undefined),
                // Flag Color
                flagColor = ko.observable(specifiedFlagColor || undefined),
                // Estimate Total
                invoiceTotal = ko.observable(specifiedTotal),
                // Flag Id
                sectionFlagId = ko.observable(specifiedFlagId || undefined),
                // Contact Id
                contactId = ko.observable(specifiedContactId || undefined),
                // Address Id
                addressId = ko.observable(specifiedAddressId || undefined),                
                orderNo = ko.observable(specifiedOrderNo),                
                invoiceStatus = ko.observable(specifiedStatus),               
                accountNo = ko.observable(specifiedAccountNo),               
                terms = ko.observable(specifiedTerms || undefined),                
                invoicePostingDate = ko.observable(specifiedPostingDate || undefined),
                invoicePostedBy = ko.observable(),
                // Is Archived
                isArchived = ko.observable(specifiedIsArchive),
                // Tax Value
                taxValue = ko.observable(specifiedTaxValue),
                // Grand Total
                grandTotal = ko.observable(specifiedGrandTotal),
                // User Notes
                userNotes = ko.observable(specifiedNotes),
                // Notes Update Date
                notesUpdateDateTime = ko.observable(),
                //
                invoiceDetailItems = ko.observableArray([]),
                isProformaInvoice = ko.observable(),
                invoiceReportSignedBy = ko.observable(undefined),
                estimateId = ko.observable(),
                headNotes = ko.observable(),
                footNotes = ko.observable(),
                xeroAccessCode = ko.observable(),
                // Errors
                errors = ko.validation.group({
                    name: name,
                    companyId: companyId
                }),
                // Is Valid
                isValid = ko.computed(function () {
                    return errors().length === 0 ? true : false;
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
                    invoiceDate: invoiceDate,
                    sectionFlagId: sectionFlagId,
                    invoiceTotal: invoiceTotal,
                    orderNo: orderNo,
                    invoiceStatus: invoiceStatus,
                    terms: terms,
                    invoicePostingDate: invoicePostingDate,
                    taxValue: taxValue,
                    grandTotal: grandTotal,
                    userNotes: userNotes,
                    invoiceReportSignedBy: invoiceReportSignedBy
                   

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
                        InvoiceId: id(),
                        InvoiceStatus: invoiceStatus(),
                        InvoiceCode: code(),
                        InvoiceName: name(),
                        CompanyId: companyId(),
                        ContactId: contactId(),
                        AddressId: addressId(),
                        FlagID: sectionFlagId(),
                        OrderNo: orderNo(),
                        InvoiceTotal: invoiceTotal(),
                        AccountNumber: accountNo(),
                        InvoiceDate: invoiceDate() ? moment(invoiceDate()).format(ist.utcFormat) + 'Z' : undefined,
                        Terms: terms(),
                        InvoicePostingDate: invoicePostingDate() ? moment(invoicePostingDate()).format(ist.utcFormat) + 'Z' : undefined,
                        InvoicePostedBy: invoicePostedBy(),
                        IsArchive: isArchived(),
                        TaxValue: taxValue(),
                        GrandTotal: grandTotal(),
                        UserNotes: userNotes(),
                        NotesUpdateDateTime: notesUpdateDateTime() ? moment(notesUpdateDateTime()).format(ist.utcFormat) + 'Z' : undefined,
                        EstimateId: estimateId(),
                        IsProformaInvoice: isProformaInvoice(),
                        ReportSignedBy: invoiceReportSignedBy(),
                        HeadNotes: headNotes(),
                        FootNotes: footNotes(),
                        XeroAccessCode: xeroAccessCode(),
                        InvoiceDetails: []
                    };
                };

            return {
                id: id,
                invoiceStatus: invoiceStatus,
                code: code,
                name: name,
                companyId: companyId,
                contactId: contactId,
                addressId: addressId,
                sectionFlagId: sectionFlagId,
                orderNo: orderNo,
                invoiceTotal: invoiceTotal,
                companyName: companyName,
                accountNo: accountNo,
                invoiceDate: invoiceDate,
                terms: terms,
                invoicePostingDate: invoicePostingDate,
                invoicePostedBy: invoicePostedBy,
                isArchived: isArchived,
                taxValue: taxValue,
                grandTotal: grandTotal,
                userNotes: userNotes,
                notesUpdateDateTime: notesUpdateDateTime,
                estimateId: estimateId,
                isProformaInvoice: isProformaInvoice,
                invoiceReportSignedBy: invoiceReportSignedBy,
                headNotes: headNotes,
                footNotes: footNotes,
                xeroAccessCode: xeroAccessCode

            };
        },
        // Item Entity
        InvoiecDetail = function (specifiedId, specifieInvoiceId, specifiedDetailType, specifiedItemId, specifiedInvoiceTitle,
            specifiedItemCharge, specifiedQty, specifiedItemTaxvalue, specifiedFlagId, specifiedDescription,
            specifiedItemType, specifiedTaxId) {
            // ReSharper restore InconsistentNaming
            var // Unique key
                id = ko.observable(specifiedId || 0),
                // Name
                invoiceId = ko.observable(specifieInvoiceId || undefined),
                // Code
                detailType = ko.observable(specifiedDetailType || undefined),
                // Product Name
                itemId = ko.observable(specifiedItemId || undefined),
                // Product Code
                invoiceTitle = ko.observable(specifiedInvoiceTitle || undefined),
                // job description title1
                itemCharge = ko.observable(specifiedItemCharge || undefined),
                // job description title2
                quantity = ko.observable(specifiedQty || undefined),
                // job description title3
                itemTaxValue = ko.observable(specifiedItemTaxvalue || undefined),
                // job description title4
                flagId = ko.observable(specifiedFlagId || undefined),
                // job description title5
                description = ko.observable(specifiedDescription || undefined),
                // job description title6
                itemType = ko.observable(specifiedItemType || undefined),
                // job description title7
                taxId = ko.observable(specifiedTaxId || undefined),
                // Errors
                errors = ko.validation.group({
                    itemCharge: itemCharge,
                    quantity: quantity
                }),
                // Is Valid
                isValid = ko.computed(function () {
                    return errors().length === 0 ? true : false;
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
               
            // ReSharper disable InconsistentNaming
                dirtyFlag = new ko.dirtyFlag({
                    name: name,
                    code: code,
                    productName: productName,
                    productCode: productCode,
                    itemAttachments: itemAttachments,
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
                        ItemType: itemType(),
                        EstimateId: estimateId(),
                        JobCreationDateTime: jobCreationDateTime() ? moment(jobCreationDateTime()).format(ist.utcFormat) + "Z" : undefined,
                        ItemSections: itemSections.map(function (itemSection, index) {
                            var section = itemSection.convertToServerData();
                            section.SectionNo = index + 1;
                            return section;
                        }),
                        ItemAttachment: itemAttachments()
                    };
                };

            return {
                id: id,
                name: name,
                code: code,
                productName: productName,
                productCode: productCode,
                itemAttachments: itemAttachments,
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
                itemType: itemType,
                estimateId: estimateId,
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
            specifiedSimilarSections, specifiedSide1Inks, specifiedSide2Inks) {
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
                    read: function () {
                        return '' + doubleOrWorknTurn();
                    },
                    write: function (value) {
                        if (!value || value === doubleOrWorknTurn()) {
                            return;
                        }

                        // Single Side
                        if (value === "1") {
                            isDoubleSided(false);
                            isWorknTurn(false);
                        } else if (value === "2") { // Double Sided
                            isDoubleSided(true);
                            isWorknTurn(false);
                        } else if (value === "3") { // Work n Turn
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
                numberUp = ko.computed(function () {
                    if (printViewLayoutPortrait() >= printViewLayoutLandscape()) {
                        return printViewLayoutPortrait();
                    } else if (printViewLayoutPortrait() <= printViewLayoutLandscape()) {
                        return printViewLayoutLandscape();
                    }

                    return 0;
                }),
                side1Inks = ko.observable(specifiedSide1Inks),
                side2Inks = ko.observable(specifiedSide2Inks),
                // Plate Ink Id
                plateInkId = ko.observable(specifiedPlateInkId || undefined),
                // SimilarSections
                similarSections = ko.observable(specifiedSimilarSections || 0),
                // Section Cost Centres
                sectionCostCentres = ko.observableArray([]),
                // Section Ink Coverage List
                sectionInkCoverageList = ko.observableArray([]),
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
                swapSectionHeightWidth = function () {
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
                    stockItemId: stockItemId,
                    plateInkId: plateInkId,
                    numberUp: numberUp,
                    stockItemName: stockItemName
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
                    similarSections: similarSections,
                    qty1: qty1,
                    qty2: qty2,
                    includeGutter: includeGutter(),
                    isPaperSupplied: isPaperSupplied(),
                    qty3: qty3
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
                        ItemSizeWidth: itemSizeWidth(),
                        IsDoubleSided: isDoubleSided(),
                        IsWorknTurn: isWorknTurn(),
                        PrintViewLayoutPortrait: printViewLayoutPortrait(),
                        PrintViewLayoutLandscape: printViewLayoutLandscape(),
                        PlateInkId: plateInkId(),
                        SimilarSections: similarSections(),
                        IncludeGutter: includeGutter(),
                        IsPaperSupplied: isPaperSupplied(),
                        BaseCharge1: baseCharge1(),
                        BaseCharge2: baseCharge2(),
                        BaseCharge3: baseCharge3(),
                        Qty1Profit: qty1Profit(),
                        Qty2Profit: qty2Profit(),
                        Qty3Profit: qty3Profit(),
                        Qty1: qty1(),
                        Qty2: qty2(),
                        Qty3: qty3(),
                        Side1Inks: side1Inks(),
                        Side2Inks: side2Inks(),
                        SectionCostcentres: sectionCostCentres.map(function (scc) {
                            return scc.convertToServerData();
                        }),
                        SectionInkCoverages: sectionInkCoverageList.map(function (sic) {
                            return sic.convertToServerData();

                        })
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
                sectionInkCoverageList: sectionInkCoverageList,
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
                side1Inks: side1Inks,
                side2Inks: side2Inks,
                errors: errors,
                isValid: isValid,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                reset: reset,
                convertToServerData: convertToServerData
            };
        },
        // Section Cost Centre Entity
    // ReSharper disable InconsistentNaming
        SectionCostCentre = function (specifiedId, specifiedName, specifiedCostCentreId, specifiedCostCentreType, specifiedOrder, specifiedIsDirectCost,
            specifiedIsOptionalExtra, specifiedIsPurchaseOrderRaised, specifiedStatus, specifiedQty1Charge, specifiedQty2Charge, specifiedQty3Charge,
            specifiedQty1MarkUpID, specifiedQty2MarkUpID, specifiedQty3MarkUpID, specifiedQty1MarkUpValue, specifiedQty2MarkUpValue, specifiedQty3MarkUpValue,
            specifiedQty1NetTotal, specifiedQty2NetTotal, specifiedQty3NetTotal, specifiedQty1, specifiedQty2, specifiedQty3, specifiedCostCentreName,
            specifiedItemSectionId, specifiedQty1WorkInstructions, specifiedQty2WorkInstructions, specifiedQty3WorkInstructions,
            specifiedQty1EstimatedStockCost, specifiedQty2EstimatedStockCost, specifiedQty3EstimatedStockCost) {
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
                //Qty 1 Work Instructions
                qty1WorkInstructions = ko.observable(specifiedQty1WorkInstructions || undefined),
                //Qty 2 Work Instructions
                qty2WorkInstructions = ko.observable(specifiedQty2WorkInstructions || undefined),
                //Qty 3 Wor kInstructions
                qty3WorkInstructions = ko.observable(specifiedQty3WorkInstructions || undefined),
                //Qty 1 Estimated Stock Cost
                qty1EstimatedStockCost = ko.observable(specifiedQty1EstimatedStockCost || undefined),
                //Qty 2 Estimated Stock Cost
                qty2EstimatedStockCost = ko.observable(specifiedQty2EstimatedStockCost || undefined),
                //Qty 3 Estimated Stock Cost
                qty3EstimatedStockCost = ko.observable(specifiedQty3EstimatedStockCost || undefined),
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
                        ItemSectionId: itemSectionId(),
                        SectionCostcentreId: id(),
                        Name: name(),
                        CostCentreId: costCentreId(),
                        Qty1: qty1(),
                        Qty2: qty2(),
                        Qty3: qty3(),
                        Qty1Charge: qty1Charge(),
                        Qty2Charge: qty2Charge(),
                        Qty3Charge: qty3Charge(),
                        Qty1NetTotal: qty1NetTotal(),
                        Qty2NetTotal: qty2NetTotal(),
                        Qty3NetTotal: qty3NetTotal(),
                        Qty1MarkUpId: qty1MarkUpId(),
                        Qty2MarkUpId: qty2MarkUpId(),
                        Qty3MarkUpId: qty3MarkUpId(),
                        Qty1MarkUpValue: qty1MarkUpValue(),
                        Qty2MarkUpValue: qty2MarkUpValue(),
                        Qty3MarkUpValue: qty3MarkUpValue()
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
                qty1WorkInstructions: qty1WorkInstructions,
                qty2WorkInstructions: qty2WorkInstructions,
                qty3WorkInstructions: qty3WorkInstructions,
                qty1EstimatedStockCost: qty1EstimatedStockCost,
                qty2EstimatedStockCost: qty2EstimatedStockCost,
                qty3EstimatedStockCost: qty3EstimatedStockCost,
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
                referenceCode = ko.observable(specifiedReferenceCode).extend({ required: true }),
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
                    amount: amount,
                    referenceCode: referenceCode
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
            var // self reference
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
                //Is Selected 
                isSelected = ko.observable(false),

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
                isSelected: isSelected,
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
        // Section Ink Coverage
        SectionInkCoverage = function (specifiedId, specifiedSectionId, specifiedInkOrder, specifiedInkId, specifiedCoverageGroupId, specifiedSide) {
            // ReSharper restore InconsistentNaming
            var // Unique key
                id = ko.observable(specifiedId),
                // section Id
                sectionId = ko.observable(specifiedSectionId),
                // ink Order
                inkOrder = ko.observable(specifiedInkOrder),
                //Ink Id
                inkId = ko.observable(specifiedInkId),
                // Coverage Group Id
                coverageGroupId = ko.observable(specifiedCoverageGroupId),
                //Side
                side = ko.observable(specifiedSide),
                // Errors
                errors = ko.validation.group({

                }),
                // Is Valid
                isValid = ko.computed(function () {
                    return errors().length === 0;
                    // ReSharper disable InconsistentNaming
                }),
                dirtyFlag = new ko.dirtyFlag({
                    // ReSharper restore InconsistentNaming
                    id: id,
                    sectionId: sectionId,
                    inkOrder: inkOrder,
                    inkId: inkId,
                    coverageGroupId: coverageGroupId,
                    side: side
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
                        Id: id(),
                        SectionId: sectionId(),
                        InkOrder: inkOrder(),
                        InkId: inkId(),
                        CoverageGroupId: coverageGroupId(),
                        Side: side()
                    };
                };

            return {
                id: id,
                sectionId: sectionId,
                inkOrder: inkOrder,
                inkId: inkId,
                coverageGroupId: coverageGroupId,
                side: side,
                errors: errors,
                isValid: isValid,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                reset: reset,
                convertToServerData: convertToServerData
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
        },
        // Item Attachment 
        // ReSharper disable once InconsistentNaming
        ItemAttachment = function (specifiedId, specifiedfileTitle, specifiedcompanyId, specifiedfileName, specifiedfolderPath) {
            // ReSharper restore InconsistentNaming
            var // Unique key
                id = ko.observable(specifiedId),
                //File Title
                fileTitle = ko.observable(specifiedfileTitle),
                //Company Id
                companyId = ko.observable(specifiedcompanyId),
                //File Name
                fileName = ko.observable(specifiedfileName),
                //Folder Path
                folderPath = ko.observable(specifiedfolderPath),
                // File path when new file is loaded 
                fileSourcePath = ko.observable(undefined),
                // Errors
                errors = ko.validation.group({

                }),
                // Is Valid
                isValid = ko.computed(function () {
                    return errors().length === 0;
                    // ReSharper disable InconsistentNaming
                }),
                dirtyFlag = new ko.dirtyFlag({
                    // ReSharper restore InconsistentNaming
                    id: id,
                    fileTitle: fileTitle,
                    companyId: companyId,
                    fileName: fileName,
                    fileSourcePath: fileSourcePath,
                    folderPath: folderPath
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
                        ItemAttachmentId: id(),
                        FileTitle: fileTitle(),
                        CompanyId: companyId(),
                        FileName: fileName(),
                        FolderPath: fileSourcePath()
                    };
                };

            return {
                id: id,
                fileTitle: fileTitle,
                companyId: companyId,
                fileName: fileName,
                folderPath: folderPath,
                fileSourcePath: fileSourcePath,
                errors: errors,
                isValid: isValid,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                reset: reset,
                convertToServerData: convertToServerData
            };
        };
    ItemAttachment.Create = function (source) {
        return new ItemAttachment(source.ItemAttachmentId, source.FileTitle, source.CompanyId, source.FileName, source.FolderPath);
    };

    // Section Cost Centre Factory
    SectionCostCentre.Create = function (source) {
        var sectionCostCentre = new SectionCostCentre(source.SectionCostcentreId, source.Name, source.CostCentreId, source.CostCenterType, source.Order,
            source.IsDirectCost, source.IsOptionalExtra, source.IsPurchaseOrderRaised, source.Status, source.Qty1Charge, source.Qty2Charge, source.Qty3Charge,
            source.Qty1MarkUpID, source.Qty2MarkUpID, source.Qty3MarkUpID, source.Qty1MarkUpValue, source.Qty2MarkUpValue, source.Qty3MarkUpValue,
            source.Qty1NetTotal, source.Qty2NetTotal, source.Qty3NetTotal, source.Qty1, source.Qty2, source.Qty3, source.CostCentreName,
            source.ItemSectionId, source.Qty1WorkInstructions, source.Qty2WorkInstructions, source.Qty3WorkInstructions,
            source.Qty1EstimatedStockCost, source.Qty2EstimatedStockCost, source.Qty3EstimatedStockCost);

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
            source.ItemId, source.IsDoubleSided, source.IsWorknTurn, source.PrintViewLayoutPortrait, source.PrintViewLayoutLandscape, source.PlateInkId, source.SimilarSections, source.Side1Inks, source.Side2Inks);

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
            source.JobActualCompletionDateTime, source.JobProgressedBy, source.JobCardPrintedBy, source.NominalCodeId, source.JobStatusId, source.InvoiceDescription,
            source.Qty1MarkUpId1, source.Qty2MarkUpId2, source.Qty3MarkUpId3, source.Qty2NetTotal, source.Qty3NetTotal, source.Qty1Tax1Value, source.Qty2Tax1Value,
            source.Qty3Tax1Value, source.Qty1GrossTotal, source.Qty2GrossTotal, source.Qty3GrossTotal, source.Tax1, source.ItemType, source.EstimateId);

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

        if (source.ItemAttachments && source.ItemAttachments.length > 0) {
            item.itemAttachments.removeAll();
            _.each(source.ItemAttachments, function (attachment) {
                item.itemAttachments.push(ItemAttachment.Create(attachment));
            });
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
        estimate.estimateTotal(source.EstimateTotal || undefined);
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

    // #region __________________  BEST PRESS   ______________________
    // Best Press Entity        
    BestPress = function (specifiedMachineID, specifiedMachineName, specifiedQty1Cost, specifiedQty1RunTime, specifiedQty2Cost, specifiedQty2RunTime,
        specifiedQty3Cost, specifiedQty3RunTime, specifiedisSelected) {
        return {
            id: specifiedMachineID,
            machineName: specifiedMachineName,
            qty1Cost: specifiedQty1Cost,
            qty1RunTime: specifiedQty1RunTime,
            qty2Cost: specifiedQty2Cost,
            qty2RunTime: specifiedQty2RunTime,
            qty3Cost: specifiedQty3Cost,
            qty3RunTime: specifiedQty3RunTime,
            isSelected: specifiedisSelected,
        };
    },
    // #endregion __________________   BEST PRESS    ______________________

    // #region __________________  User Cost Center For Run Wizard  ______________________

    // User Cost Center Entity        
    UserCostCenter = function (specifiedCostCentreId, specifiedName) {
        var // Unique key
        id = ko.observable(specifiedCostCentreId),
            name = ko.observable(specifiedName),
            isSelected = ko.observable(false),
             // Convert To Server Data
                convertToServerData = function () {
                    return {
                        SectionCostcentreId: id(),
                    };
                };
        return {
            id: id,
            name: name,
            isSelected: isSelected,
            convertToServerData: convertToServerData
        };
    },
    // #endregion __________________   User Cost Center    ______________________

    // User Cost Center Factory
    UserCostCenter.Create = function (source) {
        return new UserCostCenter(source.CostCentreId, source.Name);
    };
    // Best Press Factory
    BestPress.Create = function (source) {
        return new BestPress(source.MachineID, source.MachineName, source.Qty1Cost, source.Qty1RunTime, source.Qty2Cost, source.Qty2RunTime, source.Qty3Cost,
            source.Qty3RunTime, source.isSelected);
    };
    // Section Flag Factory
    SectionFlag.Create = function (source) {
        return new SectionFlag(source.SectionFlagId, source.FlagName, source.FlagColor);
    };

    // Section Ink Coverage Factory
    SectionInkCoverage.Create = function (source) {
        return new SectionInkCoverage(source.Id, source.SectionId, source.InkOrder, source.InkId, source.CoverageGroupId, source.Side);
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
            source.CostCentreTypeName, source.TotalPrice, callbacks);
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
        InkPlateSide: InkPlateSide,
        //Section Ink Coverage
        SectionInkCoverage: SectionInkCoverage,
        // Best Press
        BestPress: BestPress,
        // User Cost Center
        UserCostCenter: UserCostCenter,
        // Item Attachment
        ItemAttachment: ItemAttachment
    };
});