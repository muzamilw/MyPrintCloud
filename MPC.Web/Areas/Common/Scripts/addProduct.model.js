define(["moment"], function () {
    // Item Entity
    var Item = function(specifiedId, specifiedName, specifiedCode, specifiedProductName, specifiedProductCode,
        specifiedJobDescriptionTitle1, specifiedJobDescription1, specifiedJobDescriptionTitle2, specifiedJobDescription2, specifiedJobDescriptionTitle3,
        specifiedJobDescription3, specifiedJobDescriptionTitle4, specifiedJobDescription4, specifiedJobDescriptionTitle5,
        specifiedJobDescription5, specifiedJobDescriptionTitle6, specifiedJobDescription6, specifiedJobDescriptionTitle7, specifiedJobDescription7,
        specifiedIsQtyRanged, specifiedDefaultItemTax, specifiedStatusId, specifiedStatusName, specifiedQty1, specifiedQty1NetTotal, specifiedItemNotes,
        specifiedProductCategories, specifiedJobCode, specifiedJobCreationDateTime, specifiedJobManagerId, specifiedJobActualStartDateTime,
        specifiedJobActualCompletionDateTime, specifiedJobProgressedBy, specifiedJobSignedBy, specifiedNominalCodeId, specifiedJobStatusId,
        specifiedInvoiceDescription, specifiedQty1MarkUpId1, specifiedQty2MarkUpId2, specifiedQty3MarkUpId3, specifiedQty2NetTotal, specifiedQty3NetTotal,
        specifiedQty1Tax1Value, specifiedQty2Tax1Value, specifiedQty3Tax1Value, specifiedQty1GrossTotal, specifiedQty2GrossTotal, specifiedQty3GrossTotal,
        specifiedTax1, specifiedItemType, specifiedEstimateId) {
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
            // Qty1 NetTotal Computed 
            qty1NetTotalComputed = ko.computed({
                read: function() {
                    if (qty1NetTotal()) {
                        var val = parseFloat(qty1NetTotal());
                        if (!isNaN(val)) {
                            var calc = (val.toFixed(2));
                            qty1NetTotal(calc);
                            return calc;
                        } else {
                            qty1NetTotal(0.00);
                            return qty1NetTotal();
                        }
                    } else {
                        return 0.00;
                    }
                },
                write: function(value) {
                    qty1NetTotal(value);
                }
            }),
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
            productCategoriesUi = ko.computed(function() {
                if (!productCategories || productCategories().length === 0) {
                    return "";
                }

                var categories = "";
                productCategories.each(function(category, index) {
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
            // Qty2 NetTotal Computed 
            qty2NetTotalComputed = ko.computed({
                read: function() {
                    if (qty2NetTotal()) {
                        var val = parseFloat(qty2NetTotal());
                        if (!isNaN(val)) {
                            var calc = (val.toFixed(2));
                            qty2NetTotal(calc);
                            return calc;
                        } else {
                            qty2NetTotal(0.00);
                            return qty2NetTotal();
                        }
                    } else {
                        return 0.00;
                    }
                },
                write: function(value) {
                    qty2NetTotal(value);
                }
            }),
            qty3NetTotal = ko.observable(specifiedQty3NetTotal || 0),
            // Qty3 NetTotal Computed 
            qty3NetTotalComputed = ko.computed({
                read: function() {
                    if (qty3NetTotal()) {
                        var val = parseFloat(qty3NetTotal());
                        if (!isNaN(val)) {
                            var calc = (val.toFixed(2));
                            qty3NetTotal(calc);
                            return calc;
                        } else {
                            qty3NetTotal(0.00);
                            return qty3NetTotal();
                        }
                    } else {
                        return 0.00;
                    }
                },
                write: function(value) {
                    qty3NetTotal(value);
                }
            }),
            qty1Tax1Value = ko.observable(specifiedQty1Tax1Value || 0),
            qty2Tax1Value = ko.observable(specifiedQty2Tax1Value || 0),
            qty3Tax1Value = ko.observable(specifiedQty3Tax1Value || 0),
            qty1GrossTotal = ko.observable(specifiedQty1GrossTotal || 0),
            qty2GrossTotal = ko.observable(specifiedQty2GrossTotal || 0),
            qty3GrossTotal = ko.observable(specifiedQty3GrossTotal || 0),
            tax1 = ko.observable(specifiedTax1 || undefined),
            taxRateIsDisabled = ko.observable(false),
            // Item Type
            itemType = ko.observable(specifiedItemType || undefined),
            // Estimate Id
            estimateId = ko.observable(specifiedEstimateId || 0),
            // Job Estimated Start Date Time
            jobEstimatedStartDateTime = ko.observable(),
            // Job Estimated Completion Date Time
            jobEstimatedCompletionDateTime = ko.observable(),
            //Item Attachments
            itemAttachments = ko.observableArray([]),
            // Errors
            errors = ko.validation.group({
                productCode: productCode,
                productName: productName
            }),
            // Is Valid
            isValid = ko.computed(function() {
                return errors().length === 0 &&
                    itemSections.filter(function(itemSection) {
                        return !itemSection.isValid();
                    }).length === 0;
            }),
            // Show All Error Messages
            showAllErrors = function() {
                // Show Item Errors
                errors.showAllMessages();
                // Show Item Section Errors
                var itemSectionErrors = itemSections.filter(function(itemSection) {
                    return !itemSection.isValid();
                });
                if (itemSectionErrors.length > 0) {
                    _.each(itemSectionErrors, function(itemSection) {
                        itemSection.errors.showAllMessages();
                    });
                }
            },
            // Set Validation Summary
            setValidationSummary = function() {

            },
            // True if the product has been changed
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
                jobEstimatedStartDateTime: jobEstimatedStartDateTime,
                jobEstimatedCompletionDateTime: jobEstimatedCompletionDateTime,
                jobManagerId: jobManagerId,
                itemSections: itemSections
            }),
            // Item Section Changes
            itemSectionHasChanges = ko.computed(function() {
                return itemSections.find(function(itemSection) {
                    return itemSection.hasChanges();
                }) != null;
            }),
            // Item Attachment Changes
            itemAttachmentHasChanges = ko.computed(function() {
                return itemAttachments.find(function(itemAttachment) {
                    return itemAttachment.hasChanges();
                }) != null;
            }),
            // Has Changes
            hasChanges = ko.computed(function() {
                return dirtyFlag.isDirty() || itemSectionHasChanges() || itemAttachmentHasChanges();
            }),
            // Reset
            reset = function() {
                itemSections.each(function(itemSection) {
                    return itemSection.reset();
                });
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function() {
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
                    JobCreationDateTime: jobCreationDateTime() ?
                        moment(jobCreationDateTime()).format(ist.utcFormat) + "Z" : undefined,
                    JobEstimatedStartDateTime: jobEstimatedStartDateTime() ?
                        moment(jobEstimatedStartDateTime()).format(ist.utcFormat) + "Z" : undefined,
                    JobEstimatedCompletionDateTime: jobEstimatedCompletionDateTime() ?
                        moment(jobEstimatedCompletionDateTime()).format(ist.utcFormat) + "Z" : undefined,
                    JobManagerId: jobManagerId(),
                    JobStatusId: jobStatusId(),
                    ItemSections: itemSections.map(function(itemSection, index) {
                        var section = itemSection.convertToServerData(id() <= 0);
                        section.SectionNo = index + 1;
                        if (id() <= 0) {
                            section.ItemSectionId = 0;
                            section.ItemId = 0;
                        }
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
            qty1NetTotalComputed: qty1NetTotalComputed,
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
            jobEstimatedStartDateTime: jobEstimatedStartDateTime,
            jobEstimatedCompletionDateTime: jobEstimatedCompletionDateTime,
            jobStatusId: jobStatusId,
            nominalCodeId: nominalCodeId,
            invoiceDescription: invoiceDescription,
            itemSections: itemSections,
            qty1MarkUpId1: qty1MarkUpId1,
            qty2MarkUpId2: qty2MarkUpId2,
            qty3MarkUpId3: qty3MarkUpId3,
            qty2NetTotal: qty2NetTotal,
            qty2NetTotalComputed: qty2NetTotalComputed,
            qty3NetTotalComputed: qty3NetTotalComputed,
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
        ItemSection = function(specifiedId, specifiedSectionNo, specifiedSectionName, specifiedSectionSizeId, specifiedItemSizeId, specifiedIsSectionSizeCustom,
            specifiedSectionSizeHeight, specifiedSectionSizeWidth, specifiedIsItemSizeCustom, specifiedItemSizeHeight, specifiedItemSizeWidth,
            specifiedPressId, specifiedStockItemId, specifiedStockItemName, specifiedPressName, specifiedGuillotineId, specifiedQty1, specifiedQty2,
            specifiedQty3, specifiedQty1Profit, specifiedQty2Profit, specifiedQty3Profit, specifiedBaseCharge1, specifiedBaseCharge2, specifiedBaseCharge3,
            specifiedIncludeGutter, specifiedFilmId, specifiedIsPaperSupplied, specifiedSide1PlateQty, specifiedSide2PlateQty, specifiedIsPlateSupplied,
            specifiedItemId, specifiedIsDoubleSided, specifiedIsWorknTurn, specifiedPrintViewLayoutPortrait, specifiedPrintViewLayoutLandscape, specifiedPlateInkId,
            specifiedSimilarSections, specifiedSide1Inks, specifiedSide2Inks, specifiedIsPortrait, specifiedFirstTrim, specifiedSecondTrim, specifiedQty1MarkUpID,
            specifiedQty2MarkUpID, specifiedQty3MarkUpID, specifiedProductType, specifiedPressIdSide2, specifiedImpressionCoverageSide1, specifiedImpressionCoverageSide2,
            specifiedPassesSide1, specifiedPassesSide2, specifiedPrintingType, specifiedPressSide1ColourHeads, specifiedPressSide1IsSpotColor,
            specifiedPressSide2ColourHeads, specifiedPressSide2IsSpotColor, specifiedStockItemPackageQty) {
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
                baseCharge1 = ko.observable(specifiedBaseCharge1 != null ? specifiedBaseCharge1.toFixed(2) : 0),
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
                printViewLayout = ko.observable(),
                // PrintViewLayoutPortrait
                printViewLayoutPortrait = ko.observable(specifiedPrintViewLayoutPortrait || 0),
                // PrintViewLayoutLandscape
                printViewLayoutLandscape = ko.observable(specifiedPrintViewLayoutLandscape || 0),
                // Number Up
                numberUp = ko.computed(function() {
                    if (printViewLayoutPortrait() >= printViewLayoutLandscape()) {
                        printViewLayout(0);
                        return printViewLayoutPortrait();
                    } else if (printViewLayoutPortrait() <= printViewLayoutLandscape()) {
                        printViewLayout(1);
                        return printViewLayoutLandscape();
                    }

                    return 0;
                }),
                side1Inks = ko.observable(specifiedSide1Inks),
                side2Inks = ko.observable(specifiedSide2Inks),
                // Plate Ink Id
                plateInkId = ko.observable(specifiedPlateInkId || undefined),
                // SimilarSections
                similarSections = ko.observable(specifiedSimilarSections || 1),
                // Section Cost Centres
                sectionCostCentres = ko.observableArray([]),
                // Section Ink Coverage List
                sectionInkCoverageList = ko.observableArray([]),
                // Select Stock Item
                selectStock = function(stockItem) {
                    if (!stockItem || stockItemId() === stockItem.id) {
                        return;
                    }

                    stockItemId(stockItem.id);
                    stockItemName(stockItem.name);
                },
                // Select Press
                selectPress = function(press) {
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
                swapItemHeightWidth = function() {
                    var itemHeight = itemSizeHeight();
                    itemSizeHeight(itemSizeWidth());
                    itemSizeWidth(itemHeight);
                },
                isPortrait = ko.observable(specifiedIsPortrait),
                isFirstTrim = ko.observable(specifiedFirstTrim || false),
                isSecondTrim = ko.observable(specifiedSecondTrim || false),
                qty1MarkUpId = ko.observable(specifiedQty1MarkUpID || undefined),
                qty2MarkUpId = ko.observable(specifiedQty2MarkUpID || undefined),
                qty3MarkUpId = ko.observable(specifiedQty3MarkUpID || undefined),
                // Impression Coverage Side 1
                impressionCoverageSide1 = ko.observable(specifiedImpressionCoverageSide1 || undefined),
                // Impression Coverage Side 2
                impressionCoverageSide2 = ko.observable(specifiedImpressionCoverageSide2 || undefined),
                // Passes Side 1
                passesSide1 = ko.observable(specifiedPassesSide1 || 0).extend({ number: true, min: 0, max: 9 }),
                // Passes Side 2
                passesSide2 = ko.observable(specifiedPassesSide2 || 0).extend({ number: true, min: 0, max: 9 }),
                // Press Id Side 1 Colour Heads
                pressIdSide1ColourHeads = ko.observable(specifiedPressSide1ColourHeads || 0),
                // Press Id Side 2 Colour Heads
                pressIdSide2ColourHeads = ko.observable(specifiedPressSide2ColourHeads || 0),
                // Press Id Side 1 Is Spot Color
                pressIdSide1IsSpotColor = ko.observable(specifiedPressSide1IsSpotColor || false),
                // Press Id Side 2 Is Spot Color
                pressIdSide2IsSpotColor = ko.observable(specifiedPressSide2IsSpotColor || false),
                // Stock Item Package Qty
                stockItemPackageQty = ko.observable(specifiedStockItemPackageQty || undefined),
                //Product Type
                productType = ko.observable(specifiedProductType),
                // Press Id Side 2
                pressIdSide2 = ko.observable(specifiedPressIdSide2 || undefined),
                // Printing Type
                printingType = ko.observable(specifiedPrintingType || 1),
                // Errors
                errors = ko.validation.group({
                    name: name,
                    stockItemId: stockItemId,
                    plateInkId: plateInkId,
                    numberUp: numberUp
                }),
                // Is Valid
                isValid = ko.computed(function() {
                    return errors().length === 0;
                }),
                // True if the Item Section has been changed
                // ReSharper disable InconsistentNaming
                dirtyFlag = new ko.dirtyFlag({
                    
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
                        SectionName: name(),
                        SectionNo: sectionNo(),
                        StockItemId1: stockItemId(),
                        StockItem1Name: stockItemName(),
                        PressId: pressId(),
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
                        PrintViewLayout: printViewLayout(),
                        PrintViewLayoutPortrait: printViewLayoutPortrait(),
                        PrintViewLayoutLandscape: printViewLayoutLandscape(),
                        IsPortrait: isPortrait(),
                        ImpressionCoverageSide1: impressionCoverageSide1(),
                        ImpressionCoverageSide2: impressionCoverageSide2(),
                        PressSide1ColourHeads: pressIdSide1ColourHeads(),
                        PressSide1IsSpotColor: pressIdSide1IsSpotColor(),
                        PressSide2IsSpotColor: pressIdSide2IsSpotColor(),
                        StockItemPackageQty: stockItemPackageQty(),
                        PressSide2ColourHeads: pressIdSide2ColourHeads(),
                        PrintingType: printingType(),
                        PressIdSide2: pressIdSide2(),
                        SectionInkCoverages: sectionInkCoverageList.map(function (sic) {
                            var inkCoverage = sic.convertToServerData();
                            inkCoverage.SectionId = 0;
                            return inkCoverage;
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
                printViewLayout: printViewLayout,
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
                isPortrait: isPortrait,
                isFirstTrim: isFirstTrim,
                isSecondTrim: isSecondTrim,
                qty1MarkUpId: qty1MarkUpId,
                qty2MarkUpId: qty2MarkUpId,
                qty3MarkUpId: qty3MarkUpId,
                impressionCoverageSide1: impressionCoverageSide1,
                impressionCoverageSide2: impressionCoverageSide2,
                passesSide1: passesSide1,
                pressIdSide1ColourHeads: pressIdSide1ColourHeads,
                pressIdSide1IsSpotColor: pressIdSide1IsSpotColor,
                pressIdSide2IsSpotColor: pressIdSide2IsSpotColor,
                stockItemPackageQty: stockItemPackageQty,
                passesSide2: passesSide2,
                pressIdSide2ColourHeads: pressIdSide2ColourHeads,
                printingType: printingType,
                errors: errors,
                isValid: isValid,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                reset: reset,
                convertToServerData: convertToServerData
            };
        },
        //#region Section Ink Coverage Entity
        // ReSharper disable once AssignToImplicitGlobalInFunctionScope
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
        //#endregion
        // Item Price Matrix Entity
        ItemPriceMatrix = function(specifiedId, specifiedQuantity, specifiedQtyRangedFrom, specifiedQtyRangedTo, specifiedPricePaperType1, specifiedPricePaperType2,
            specifiedPricePaperType3, specifiedPriceStockType4, specifiedPriceStockType5, specifiedPriceStockType6, specifiedPriceStockType7, specifiedPriceStockType8,
            specifiedPriceStockType9, specifiedPriceStockType10, specifiedPriceStockType11, specifiedFlagId, specifiedSupplierId, specifiedSupplierSequence, specifiedItemId, specifiedProductItemTax, specifiedCompanyTaxRate) {
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
                pricePaperType1 = ko.observable(specifiedPricePaperType1 || 0).extend({ numberInput: ist.numberFormat }),
                // Price Paper Type1 Ui
                pricePaperType1Ui = ko.computed(function() {
                    if (!pricePaperType1()) {
                        return '$ 0.00';
                    }

                    return '$ ' + pricePaperType1();
                }),
                // Price Paper Type2
                pricePaperType2 = ko.observable(specifiedPricePaperType2 || 0).extend({ numberInput: ist.numberFormat }),
                // Price Paper Type2 Ui
                pricePaperType2Ui = ko.computed(function() {
                    if (!pricePaperType2()) {
                        return '$ 0.00';
                    }

                    return '$ ' + pricePaperType2();
                }),
                // Price Paper Type3
                pricePaperType3 = ko.observable(specifiedPricePaperType3 || 0).extend({ numberInput: ist.numberFormat }),
                // Price Paper Type3 Ui
                pricePaperType3Ui = ko.computed(function() {
                    if (!pricePaperType3()) {
                        return '$ 0.00';
                    }

                    return '$ ' + pricePaperType3();
                }),
                // Price Stock Type4
                priceStockType4 = ko.observable(specifiedPriceStockType4 || 0).extend({ numberInput: ist.numberFormat }),
                // Price Stock Type4 Ui
                priceStockType4Ui = ko.computed(function() {
                    if (!priceStockType4()) {
                        return '$ 0.00';
                    }

                    return '$ ' + priceStockType4();
                }),
                // Price Stock Type5
                priceStockType5 = ko.observable(specifiedPriceStockType5 || 0).extend({ numberInput: ist.numberFormat }),
                // Price Stock Type5 Ui
                priceStockType5Ui = ko.computed(function() {
                    if (!priceStockType5()) {
                        return '$ 0.00';
                    }

                    return '$ ' + priceStockType5();
                }),
                // Price Stock Type6
                priceStockType6 = ko.observable(specifiedPriceStockType6 || 0).extend({ numberInput: ist.numberFormat }),
                // Price Stock Type6 Ui
                priceStockType6Ui = ko.computed(function() {
                    if (!priceStockType6()) {
                        return '$ 0.00';
                    }

                    return '$ ' + priceStockType6();
                }),
                // Price Stock Type7
                priceStockType7 = ko.observable(specifiedPriceStockType7 || 0).extend({ numberInput: ist.numberFormat }),
                // Price Stock Type7 Ui
                priceStockType7Ui = ko.computed(function() {
                    if (!priceStockType7()) {
                        return '$ 0.00';
                    }

                    return '$ ' + priceStockType7();
                }),
                // Price Stock Type8
                priceStockType8 = ko.observable(specifiedPriceStockType8 || 0).extend({ numberInput: ist.numberFormat }),
                // Price Stock Type8 Ui
                priceStockType8Ui = ko.computed(function() {
                    if (!priceStockType8()) {
                        return '$ 0.00';
                    }

                    return '$ ' + priceStockType8();
                }),
                // Price Stock Type9
                priceStockType9 = ko.observable(specifiedPriceStockType9 || 0).extend({ numberInput: ist.numberFormat }),
                // Price Stock Type9 Ui
                priceStockType9Ui = ko.computed(function() {
                    if (!priceStockType4()) {
                        return '$ 0.00';
                    }

                    return '$ ' + priceStockType9();
                }),
                // Price Stock Type10
                priceStockType10 = ko.observable(specifiedPriceStockType10 || 0).extend({ numberInput: ist.numberFormat }),
                // Price Stock Type10 Ui
                priceStockType10Ui = ko.computed(function() {
                    if (!priceStockType10()) {
                        return '$ 0.00';
                    }

                    return '$ ' + priceStockType10();
                }),
                // Price Stock Type11
                priceStockType11 = ko.observable(specifiedPriceStockType11 || 0).extend({ numberInput: ist.numberFormat }),
                // Price Stock Type11 Ui
                priceStockType11Ui = ko.computed(function() {
                    if (!priceStockType11()) {
                        return '$ 0.00';
                    }

                    return '$ ' + priceStockType11();
                }),
                // Item Id
                itemId = ko.observable(specifiedItemId || 0),
                productItemTax = ko.observable(specifiedProductItemTax),
                companyTaxRate = ko.observable(specifiedCompanyTaxRate),
                pricePaperType1WithTax = ko.computed(function() {
                    if (productItemTax() != undefined && productItemTax() != null) {
                        return pricePaperType1() + (pricePaperType1() * (productItemTax() / 100));
                    }
                    else if (companyTaxRate() != undefined && companyTaxRate() != null) {
                        return pricePaperType1() + (pricePaperType1() * (companyTaxRate() / 100));
                    }
                    return pricePaperType1();
                }),
                pricePaperType2WithTax = ko.computed(function () {
                    if (productItemTax() != undefined && productItemTax() != null) {
                        return pricePaperType2() + (pricePaperType2() * (productItemTax() / 100));
                    }
                    else if (companyTaxRate() != undefined && companyTaxRate() != null) {
                        return pricePaperType2() + (pricePaperType2() * (companyTaxRate() / 100));
                    }
                    return pricePaperType2();
                }),
                pricePaperType3WithTax = ko.computed(function () {
                    if (productItemTax() != undefined && productItemTax() != null) {
                        return pricePaperType3() + (pricePaperType3() * (productItemTax() / 100));
                    }
                    else if (companyTaxRate() != undefined && companyTaxRate() != null) {
                        return pricePaperType3() + (pricePaperType3() * (companyTaxRate() / 100));
                    }
                    return pricePaperType3();
                }),
                priceStockType4WithTax = ko.computed(function () {
                    if (productItemTax() != undefined && productItemTax() != null) {
                        return priceStockType4() + (priceStockType4() * (productItemTax() / 100));
                    }
                    else if (companyTaxRate() != undefined && companyTaxRate() != null) {
                        return priceStockType4() + (priceStockType4() * (companyTaxRate() / 100));
                    }
                    return priceStockType4();
                }),
                priceStockType5WithTax = ko.computed(function () {
                    if (productItemTax() != undefined && productItemTax() != null) {
                        return priceStockType5() + (priceStockType5() * (productItemTax() / 100));
                    }
                    else if (companyTaxRate() != undefined && companyTaxRate() != null) {
                        return priceStockType5() + (priceStockType5() * (companyTaxRate() / 100));
                    }
                    return priceStockType5();
                }),
                priceStockType6WithTax = ko.computed(function () {
                    if (productItemTax() != undefined && productItemTax() != null) {
                        return priceStockType6() + (priceStockType6() * (productItemTax() / 100));
                    }
                    else if (companyTaxRate() != undefined && companyTaxRate() != null) {
                        return priceStockType6() + (priceStockType6() * (companyTaxRate() / 100));
                    }
                    return priceStockType6();
                }),
                priceStockType7WithTax = ko.computed(function () {
                    if (productItemTax() != undefined && productItemTax() != null) {
                        return priceStockType7() + (priceStockType7() * (productItemTax() / 100));
                    }
                    else if (companyTaxRate() != undefined && companyTaxRate() != null) {
                        return priceStockType7() + (priceStockType7() * (companyTaxRate() / 100));
                    }
                    return priceStockType7();
                }),
                priceStockType8WithTax = ko.computed(function () {
                    if (productItemTax() != undefined && productItemTax() != null) {
                        return priceStockType8() + (priceStockType8() * (productItemTax() / 100));
                    }
                    else if (companyTaxRate() != undefined && companyTaxRate() != null) {
                        return priceStockType8() + (priceStockType8() * (companyTaxRate() / 100));
                    }
                    return priceStockType8();
                }),
                priceStockType9WithTax = ko.computed(function () {
                    if (productItemTax() != undefined && productItemTax() != null) {
                        return priceStockType9() + (priceStockType9() * (productItemTax() / 100));
                    }
                    else if (companyTaxRate() != undefined && companyTaxRate() != null) {
                        return priceStockType9() + (priceStockType9() * (companyTaxRate() / 100));
                    }
                    return priceStockType9();
                }),
                priceStockType10WithTax = ko.computed(function () {
                    if (productItemTax() != undefined && productItemTax() != null) {
                        return priceStockType10() + (priceStockType10() * (productItemTax() / 100));
                    }
                    else if (companyTaxRate() != undefined && companyTaxRate() != null) {
                        return priceStockType10() + (priceStockType10() * (companyTaxRate() / 100));
                    }
                    return priceStockType10();
                }),
                priceStockType11WithTax = ko.computed(function () {
                    if (productItemTax() != undefined && productItemTax() != null) {
                        return priceStockType11() + (priceStockType11() * (productItemTax() / 100));
                    }
                    else if (companyTaxRate() != undefined && companyTaxRate() != null) {
                        return priceStockType11() + (priceStockType11() * (companyTaxRate() / 100));
                    }
                    return priceStockType11();
                }),
                // Errors
                errors = ko.validation.group({                    
                    
                }),
                // Is Valid
                isValid = ko.computed(function() {
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
                pricePaperType1WithTax: pricePaperType1WithTax,
                pricePaperType2WithTax: pricePaperType2WithTax,
                pricePaperType3WithTax: pricePaperType3WithTax,
                priceStockType4WithTax: priceStockType4WithTax,
                priceStockType5WithTax: priceStockType5WithTax,
                priceStockType6WithTax: priceStockType6WithTax,
                priceStockType7WithTax: priceStockType7WithTax,
                priceStockType8WithTax: priceStockType8WithTax,
                priceStockType9WithTax: priceStockType9WithTax,
                priceStockType10WithTax: priceStockType10WithTax,
                priceStockType11WithTax: priceStockType11WithTax,
                errors: errors,
                isValid: isValid,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                reset: reset,
                convertToServerData: convertToServerData
            };
        },
        // Item Stock Option Entity
        ItemStockOption = function(specifiedId, specifiedStockLabel, specifiedStockId, specifiedStockItemName, specifiedStockItemDescription, specifiedImage,
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
                onAddItemAddonCostCentre = function() {
                    activeItemAddonCostCentre(ItemAddonCostCentre.Create({ ProductAddOnId: 0, ItemStockOptionId: id() }, callbacks));
                },
                onEditItemAddonCostCentre = function(itemAddonCostCentre) {
                    activeItemAddonCostCentre(itemAddonCostCentre);
                },
                // Save ItemAddonCostCentre
                saveItemAddonCostCentre = function() {
                    if (activeItemAddonCostCentre().id() === 0) { // Add
                        activeItemAddonCostCentre().id(itemAddonCostCentreCounter);
                        addItemAddonCostCentre(activeItemAddonCostCentre());
                        itemAddonCostCentreCounter -= 1;
                        return;
                    }
                },
                // Add Item Addon Cost Center
                addItemAddonCostCentre = function(itemAddonCostCentre) {
                    itemAddonCostCentres.splice(0, 0, itemAddonCostCentre);
                },
                // Remove ItemAddon Cost Centre
                removeItemAddonCostCentre = function(itemAddonCostCentre) {
                    itemAddonCostCentres.remove(itemAddonCostCentre);
                },
                // Select Stock Item
                selectStock = function(stockItem) {
                    if (!stockItem || stockItemId() === stockItem.id) {
                        return;
                    }

                    stockItemId(stockItem.id);
                    stockItemName(stockItem.name);
                    stockItemDescription(stockItem.description);
                    label(stockItem.name);
                },
                // On Select File
                onSelectImage = function(file, data) {
                    image(data);
                    fileSource(data);
                    fileName(file.name);
                },
                // Errors
                errors = ko.validation.group({
                    label: label
                }),
                // Is Valid
                isValid = ko.computed(function() {
                    return errors().length === 0 && itemAddonCostCentres.filter(function(itemAddonCostCentre) {
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
                hasChanges = ko.computed(function() {
                    return dirtyFlag.isDirty() || itemAddonCostCentres.find(function(itemAddonCostCentre) {
                        return itemAddonCostCentre.hasChanges();
                    }) != null;
                }),
                // Reset
                reset = function() {
                    // Reset Item Addon Cost Centres State to Un-Modified
                    itemAddonCostCentres.each(function(itemAddonCostCentre) {
                        return itemAddonCostCentre.reset();
                    });
                    dirtyFlag.reset();
                },
                // Convert To Server Data
                convertToServerData = function() {
                    return {
                        ItemStockOptionId: id(),
                        StockLabel: label(),
                        StockId: stockItemId(),
                        ItemId: itemId(),
                        FileSource: fileSource(),
                        FileName: fileName(),
                        OptionSequence: optionSequence(),
                        ItemAddOnCostCentres: itemAddonCostCentres.map(function(itemAddonCostCentre) {
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
        // Item Addon Cost Centre Entity
        ItemAddonCostCentre = function(specifiedId, specifiedIsMandatory, specifiedItemStockOptionId, specifiedCostCentreId, specifiedCostCentreName,
            specifiedCostCentreType, specifiedTotalPrice, callbacks, specifiedProductItemTax, specifiedCompanyTaxRate) {
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
                totalPrice = ko.observable(specifiedTotalPrice || undefined).extend({ numberInput: ist.numberFormat }),
                 // Total Price With tax
                totalPriceWithTax = ko.computed(function () {
                    if (specifiedProductItemTax != undefined && specifiedProductItemTax != null) {
                        return totalPrice() + (totalPrice() * (specifiedProductItemTax / 100));
                    }
                    else if (specifiedCompanyTaxRate != undefined && specifiedCompanyTaxRate  != null) {
                        return totalPrice() + (totalPrice() * (specifiedCompanyTaxRate / 100));
                    }
                    return totalPrice();
                }),
                // Cost Centre Id - On Change
                costCentreId = ko.computed({
                    read: function() {
                        return internalCostCentreId();
                    },
                    write: function(value) {
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
                isValid = ko.computed(function() {
                    return errors().length === 0;
                }),
                // True if the Item Vdp Price has been changed
                // ReSharper disable InconsistentNaming
                dirtyFlag = new ko.dirtyFlag({
                    isMandatory: isMandatory,
                    costCentreId: costCentreId
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
                totalPriceWithTax: totalPriceWithTax,
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
        // ReSharper disable InconsistentNaming
        SectionCostCentre = function(specifiedId, specifiedName, specifiedCostCentreId, specifiedCostCentreType, specifiedOrder, specifiedIsDirectCost,
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
                isValid = ko.computed(function() {
                    return errors().length === 0;
                }),
                // True if the Item Section has been changed
                // ReSharper disable InconsistentNaming
                dirtyFlag = new ko.dirtyFlag({
                    
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
                convertToServerData = function (isNewSectionCostCenter) {
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
                        Qty3MarkUpValue: qty3MarkUpValue(),
                        SectionCostCentreDetails: sectionCostCentreDetails.map(function (scc) {
                            var sectionCc = scc.convertToServerData();
                            if (isNewSectionCostCenter) {
                                sectionCc.SectionCostCentreId = 0;
                            }
                            return sectionCc;
                        })
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
        SectionCostCenterDetail = function (
            specifiedSectionCostCentreDetailId, specifiedSectionCostCentreId, specifiedStockId, specifiedSupplierId, specifiedQty1, specifiedQty2,
            specifiedQty3, specifiedCostPrice, specifiedActualQtyUsed, specifiedStockName, specifiedSupplier
        ) {
            var
            sectionCostCentreDetailId = ko.observable(specifiedSectionCostCentreDetailId),
            sectionCostCentreId = ko.observable(specifiedSectionCostCentreId),
            stockId = ko.observable(specifiedStockId),
            supplierId = ko.observable(specifiedSupplierId),
            qty1 = ko.observable(specifiedQty1),
            qty2 = ko.observable(specifiedQty2),
            qty3 = ko.observable(specifiedQty3),
            costPrice = ko.observable(specifiedCostPrice),
            actualQtyUsed = ko.observable(specifiedActualQtyUsed),
            stockName = ko.observable(specifiedStockName),
            supplier = ko.observable(specifiedSupplier),
           // Errors
                errors = ko.validation.group({

                }),
                // Is Valid
                isValid = ko.computed(function () {
                    return errors().length === 0;
                }),
                dirtyFlag = new ko.dirtyFlag({
                    sectionCostCentreDetailId: sectionCostCentreDetailId,
                    sectionCostCentreId: sectionCostCentreId,
                    stockId: stockId,
                    supplierId: supplierId,
                    qty1: qty1,
                    qty2: qty2,
                    qty3: qty3,
                    costPrice: costPrice,
                    stockName: stockName,
                    supplier: supplier
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
                        //            
                        SectionCostCentreDetailId: sectionCostCentreDetailId(),
                        SectionCostCentreId: sectionCostCentreId(),
                        StockId: stockId(),
                        SupplierId: supplierId(),
                        Qty1: qty1(),
                        Qty2: qty2(),
                        Qty3: qty3(),
                        CostPrice: costPrice(),
                        ActualQtyUsed: actualQtyUsed(),
                        StockName: stockName(),
                        Supplier: supplier(),
                    };
                };

            return {
                sectionCostCentreDetailId: sectionCostCentreDetailId,
                sectionCostCentreId: sectionCostCentreId,
                stockId: stockId,
                supplierId: supplierId,
                qty1: qty1,
                qty2: qty2,
                qty3: qty3,
                costPrice: costPrice,
                stockName: stockName,
                supplier: supplier,
                errors: errors,
                isValid: isValid,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                reset: reset,
                convertToServerData: convertToServerData
            };
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
    // Item Stock Option Factory
   ItemStockOption.Create = function (source, callbacks) {
       var itemStockOption = new ItemStockOption(source.ItemStockOptionId, source.StockLabel, source.StockId, source.StockItemName, source.StockItemDescription,
           source.ImageUrlSource, source.OptionSequence, source.ItemId, callbacks, source.ProductItemTax, source.CompanyTaxRate);

       // If Item Addon CostCentres exists then add
       if (source.ItemAddOnCostCentres) {
           var itemAddonCostCentres = [];

           _.each(source.ItemAddOnCostCentres, function (itemAddonCostCenter) {
               itemAddonCostCenter.ProductItemTax = source.ProductItemTax;
               itemAddonCostCenter.CompanyTaxRate = source.CompanyTaxRate;
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
    

    // Item Section Factory
   ItemSection.Create = function (source) {
       var itemSection = new ItemSection(source.ItemSectionId, source.SectionNo, source.SectionName, source.SectionSizeId, source.ItemSizeId,
           source.IsSectionSizeCustom, source.SectionSizeHeight, source.SectionSizeWidth, source.IsItemSizeCustom, source.ItemSizeHeight,
           source.ItemSizeWidth, source.PressId, source.StockItemId1, source.StockItem1Name, source.PressName, source.GuillotineId, source.Qty1,
           source.Qty2, source.Qty3, source.Qty1Profit, source.Qty2Profit, source.Qty3Profit, source.BaseCharge1, source.BaseCharge2,
           source.Basecharge3, source.IncludeGutter, source.FilmId, source.IsPaperSupplied, source.Side1PlateQty, source.Side2PlateQty, source.IsPlateSupplied,
           source.ItemId, source.IsDoubleSided, source.IsWorknTurn, source.PrintViewLayoutPortrait, source.PrintViewLayoutLandscape, source.PlateInkId,
           source.SimilarSections, source.Side1Inks, source.Side2Inks,
            source.IsPortrait, source.IsFirstTrim, source.IsSecondTrim, source.Qty1MarkUpID, source.Qty2MarkUpID, source.Qty3MarkUpID, source.ProductType,
            source.PressIdSide2, source.ImpressionCoverageSide1, source.ImpressionCoverageSide2, source.PassesSide1, source.PassesSide2, source.PrintingType,
            source.PressSide1ColourHeads, source.PressSide1IsSpotColor, source.PressSide2ColourHeads, source.PressSide2IsSpotColor, source.StockItemPackageQty);

       // Map Section Ink Coverage if Any
       if (source.SectionInkCoverages && source.SectionInkCoverages.length > 0) {
           var sectioninkcoverages = [];

           _.each(source.SectionInkCoverages, function (sectionink) {
               sectioninkcoverages.push(SectionInkCoverage.Create(sectionink));
           });

           // Push to Original Item
           ko.utils.arrayPushAll(itemSection.sectionInkCoverageList(), sectioninkcoverages);
           itemSection.sectionInkCoverageList.valueHasMutated();
       }

       return itemSection;
   };

    // Item Price Matrix Factory
   ItemPriceMatrix.Create = function (source) {
       return new ItemPriceMatrix(source.PriceMatrixId, source.Quantity, source.QtyRangeFrom, source.QtyRangeTo, source.PricePaperType1, source.PricePaperType2,
           source.PricePaperType3, source.PriceStockType4, source.PriceStockType5, source.PriceStockType6, source.PriceStockType7, source.PriceStockType8,
           source.PriceStockType9, source.PriceStockType10, source.PriceStockType11, source.FlagId, source.SupplierId, source.SupplierSequence, source.ItemId, source.ProductItemTax, source.CompanyTaxRate);
   };
    // Section Cost Centre Factory
    SectionCostCentre.Create = function(source) {
        var sectionCostCentre = new SectionCostCentre(source.SectionCostcentreId, source.Name, source.CostCentreId, source.CostCenterType, source.Order,
            source.IsDirectCost, source.IsOptionalExtra, source.IsPurchaseOrderRaised, source.Status, source.Qty1Charge, source.Qty2Charge, source.Qty3Charge,
            source.Qty1MarkUpID, source.Qty2MarkUpID, source.Qty3MarkUpID, source.Qty1MarkUpValue, source.Qty2MarkUpValue, source.Qty3MarkUpValue,
            source.Qty1NetTotal, source.Qty2NetTotal, source.Qty3NetTotal, source.Qty1, source.Qty2, source.Qty3, source.CostCentreName,
            source.ItemSectionId, source.Qty1WorkInstructions, source.Qty2WorkInstructions, source.Qty3WorkInstructions,
            source.Qty1EstimatedStockCost, source.Qty2EstimatedStockCost, source.Qty3EstimatedStockCost);

        // Map Section Cost Centre Details if Any
        if (source.SectionCostCentreDetails && source.SectionCostCentreDetails.length > 0) {
            var sectionCostcentresDetails = [];

            _.each(source.SectionCostcentres, function (sectionCostCentreDetail) {
                sectionCostcentresDetails.push(SectionCostCenterDetail.Create(sectionCostCentreDetail));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(sectionCostCentre.sectionCostCentreDetails(), sectionCostcentresDetails);
            sectionCostCentre.sectionCostCentreDetails.valueHasMutated();
        }

        // Map Section Cost Resources if Any
        if (source.SectionCostCentreResources && source.SectionCostCentreResources.length > 0) {

        }

        return sectionCostCentre;
    };
    // Item Addon Cost Centre Factory
   ItemAddonCostCentre.Create = function (source, callbacks) {
       return new ItemAddonCostCentre(source.ProductAddOnId, source.IsMandatory, source.ItemStockOptionId, source.CostCentreId, source.CostCentreName,
           source.CostCentreTypeName, source.TotalPrice, callbacks, source.ProductItemTax, source.CompanyTaxRate);
   };
    // Section Cost Centre Factory
   SectionCostCenterDetail.Create = function (source) {

       var sectionCostCenterDetail = new SectionCostCenterDetail(source.SectionCostCentreDetailId, source.SectionCostCentreId, source.StockId, source.SupplierId, source.Qty1,
           source.Qty2, source.Qty3, source.CostPrice, source.StockName, source.Supplier);

       return sectionCostCenterDetail;
   };

    //#region Section Ink Coverage Factory
   SectionInkCoverage.Create = function (source) {
       return new SectionInkCoverage(source.Id, source.SectionId, source.InkOrder, source.InkId, source.CoverageGroupId, source.Side);
   };
    //#endregion
    return {
        // Cost Centre Constructor
        Item: Item,
        ItemStockOption: ItemStockOption,
        ItemSection: ItemSection,
        ItemPriceMatrix: ItemPriceMatrix,
        ItemAddonCostCentre: ItemAddonCostCentre,
        SectionCostCentre: SectionCostCentre,
        SectionCostCenterDetail: SectionCostCenterDetail,
        SectionInkCoverage: SectionInkCoverage
    };
});