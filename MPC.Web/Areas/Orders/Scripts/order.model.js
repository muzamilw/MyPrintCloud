/*
    Module with the model for the Order
*/
define(["ko", "common/itemDetail.model", "underscore", "underscore-ko"], function (ko, itemModel) {
    var // Status Enums
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
            specifiedOfficialOrderSetOnDateTime, specifiedFootNotes, specifiedEnquiryId, specifiedRefEstimateId, specifiedOrderReportSignedBy, specifiedReportSignedBy,
            specifiedInvoiceStatus,specifiedStoreName) {
            // ReSharper restore InconsistentNaming
            var // Unique key
                id = ko.observable(specifiedId || 0),
                // Name
                name = ko.observable(specifiedName || undefined).extend({ required: true }),
                // Code
                code = ko.observable(specifiedCode || undefined),
                // Is From Estimate
                isFromEstimate = ko.computed(function () {
                    return code() !== null && code() !== undefined && code() !== "";
                }),
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
                // Estimate Total
                estimateTotal = ko.observable(0).extend({ numberInput: ist.numberFormat }),
                // Flag Id
                sectionFlagId = ko.observable(specifiedSectionFlagId || undefined).extend({ required: true }),
                // Order Code
                orderCode = ko.observable(specifiedOrderCode || undefined),
                // Is Estimate
                isEstimate = ko.observable(specifiedIsEstimate || false),
                // Contact Id
                contactId = ko.observable(specifiedContactId || undefined),
                // Address Id
                addressId = ko.observable(specifiedAddressId || undefined),
                // Is Direct Sale
                isDirectSale = ko.observable(((specifiedIsDirectSale !== null && specifiedIsDirectSale !== undefined &&
                    specifiedIsDirectSale === true) || !id()) ? true : false),
                // Is Direct Sale Ui
                isDirectSaleUi = ko.computed(function () {
                    return isDirectSale() ? "Direct Order" : "Online Order";
                }),
                // Is Official Order
                isOfficialOrder = ko.observable(specifiedIsOfficialOrder || false),
                // Is Credit Approved
                isCreditApproved = ko.observable(specifiedIsCreditApproved || false),
                // Order Date
                orderDate = ko.observable(specifiedOrderDate ? moment(specifiedOrderDate).toDate() : moment().toDate()),
                // Start Delivery Date
                startDeliveryDate = ko.observable(specifiedStartDeliveryDate ? moment(specifiedStartDeliveryDate).toDate() : undefined),
                // Finish Delivery Date
                finishDeliveryDate = ko.observable(specifiedFinishDeliveryDate ? moment(specifiedFinishDeliveryDate).toDate() : moment().toDate()),
                // Head Notes
                headNotes = ko.observable(specifiedHeadNotes || undefined),
                // Artwork By Date
                artworkByDate = ko.observable(specifiedArtworkByDate ? moment(specifiedArtworkByDate).toDate() : moment().toDate()),
                // Data By Date
                dataByDate = ko.observable(specifiedDataByDate ? moment(specifiedDataByDate).toDate() : moment().toDate()),
                // Paper By Date
                paperByDate = ko.observable(specifiedPaperByDate ? moment(specifiedPaperByDate).toDate() : moment().toDate()),
                // Target Bind Date
                targetBindDate = ko.observable(specifiedTargetBindDate ? moment(specifiedTargetBindDate).toDate() : moment().toDate()),
                // Xero Access Code
                xeroAccessCode = ko.observable(specifiedXeroAccessCode || undefined),
                // Target Print Date
                targetPrintDate = ko.observable(specifiedTargetPrintDate ? moment(specifiedTargetPrintDate).toDate() : moment().toDate()),
                // Order Creation Date Time
                orderCreationDateTime = ko.observable(specifiedOrderCreationDateTime ? moment(specifiedOrderCreationDateTime).toDate() : undefined),
                // Order Manager Id
                orderManagerId = ko.observable(specifiedOrderManagerId || undefined),
                // Sales Person Id
                salesPersonId = ko.observable(specifiedSalesPersonId || undefined),
                // Source Id
                sourceId = ko.observable(specifiedSourceId || undefined),
                storeName = ko.observable(specifiedStoreName || undefined),
                // Credit Limit For Job
                creditLimitForJob = ko.observable(specifiedCreditLimitForJob || undefined),
                // System Users
                systemUsers = ko.observableArray([]),
                // Credit Limit Set By
                creditLimitSetBy = ko.observable(specifiedCreditLimitSetBy || undefined),
                isExtraOrder = ko.observable(),
                // Get User by Id
                getUserById = function (userId) {
                    return systemUsers.find(function (user) {
                        return user.id === userId;
                    });
                },
                // Set Credit Limit Set By
                setCreditiLimitSetBy = function (userId) {
                    if (!userId) {
                        return;
                    }
                    var user = getUserById(userId);
                    if (user) {
                        creditLimitSetByUser(user);
                    }
                },
                // Credit Limit Set By For User
                creditLimitSetByUser = ko.computed({
                    read: function () {
                        if (!creditLimitSetBy()) {
                            return SystemUser.Create({});
                        }
                        return getUserById(creditLimitSetBy());
                    },
                    write: function (value) {
                        if (!value) {
                            creditLimitSetBy(undefined);
                            return;
                        }
                        var userId = value.id;
                        if (userId === creditLimitSetBy()) {
                            return;
                        }
                        creditLimitSetBy(userId);
                    }
                }),
                // Credit Limit Set on Date Time
                creditLimitSetOnDateTime = ko.observable(specifiedCreditLimitSetOnDateTime ? moment(specifiedCreditLimitSetOnDateTime).toDate() : moment().toDate()),
                // Is JobAllowedWOCreditCheck
                isJobAllowedWoCreditCheck = ko.observable(specifiedIsJobAllowedWOCreditCheck || undefined),
                // Allow Job WOCreditCheckSetOnDateTime
                allowJobWoCreditCheckSetOnDateTime = ko.observable(specifiedAllowJobWOCreditCheckSetOnDateTime ?
                    moment(specifiedAllowJobWOCreditCheckSetOnDateTime).toDate() : moment().toDate()),
                // Allow JobWOCreditCheckSetBy
                allowJobWoCreditCheckSetBy = ko.observable(specifiedAllowJobWOCreditCheckSetBy || undefined),
                // Set Allow JobWOCreditCheckSetBy
                setAllowJobWoCreditCheckSetBy = function (userId) {
                    if (!userId) {
                        return;
                    }
                    var user = getUserById(userId);
                    if (user) {
                        allowJobWoCreditCheckSetByUser(user);
                    }
                },
                // Allow JobWOCreditCheckSetBy For User
                allowJobWoCreditCheckSetByUser = ko.computed({
                    read: function () {
                        if (!allowJobWoCreditCheckSetBy()) {
                            return SystemUser.Create({});
                        }
                        return getUserById(allowJobWoCreditCheckSetBy());
                    },
                    write: function (value) {
                        if (!value) {
                            allowJobWoCreditCheckSetBy(undefined);
                            return;
                        }
                        var userId = value.id;
                        if (userId === allowJobWoCreditCheckSetBy()) {
                            return;
                        }
                        allowJobWoCreditCheckSetBy(userId);
                    }
                }),
                // Customer Po
                customerPo = ko.observable(specifiedCustomerPo || undefined),
                // Official Order Set By
                officialOrderSetBy = ko.observable(specifiedOfficialOrderSetBy || undefined),
                // Official Order Set By For User
                officialOrderSetByUser = ko.computed({
                    read: function () {
                        if (!officialOrderSetBy()) {
                            return SystemUser.Create({});
                        }
                        return getUserById(officialOrderSetBy());
                    },
                    write: function (value) {
                        if (!value) {
                            officialOrderSetBy(undefined);
                            return;
                        }
                        var userId = value.id;
                        if (userId === officialOrderSetBy()) {
                            return;
                        }
                        officialOrderSetBy(userId);
                    }
                }),
                // Set Official Order Set By 
                setOfficialOrderSetBy = function (userId) {
                    if (!userId) {
                        return;
                    }
                    var user = getUserById(userId);
                    if (user) {
                        officialOrderSetByUser(user);
                    }
                },
                // Official Order Set on Date Time
                officialOrderSetOnDateTime = ko.observable(specifiedOfficialOrderSetOnDateTime ? moment(specifiedOfficialOrderSetOnDateTime).toDate() : moment().toDate()),
                // Foot Notes
                footNotes = ko.observable(specifiedFootNotes || undefined),
                //Enqiry Id
                enquiryId = ko.observable(specifiedEnquiryId || undefined),
                //Reference Estimate Id
                refEstimateId = ko.observable(specifiedRefEstimateId || undefined),
                //Tax Rate
                taxRate = ko.observable(undefined),
                // Items
                items = ko.observableArray([]),
                // Delivery Items
                deliveryItems = ko.computed(function () {
                    if (items().length === 0) {
                        return [];
                    }

                    return items.filter(function (item) {
                        return item.itemType() === 2;
                    });
                }),
                // Non Delivery Items
                nonDeliveryItems = ko.computed(function () {
                    if (items().length === 0) {
                        return [];
                    }

                    return items.filter(function (item) {
                        return item.itemType() !== 2;
                    });
                }),
                // Pre Payments
                prePayments = ko.observableArray([]),
                // Deliver Schedule
                deliverySchedules = ko.observableArray([]),
                //Inquiry Items
                inquiryItems = ko.observableArray([]),
                //Is Inquiry Item Loaded Flag
                isInquiryItemLoaded = ko.observable(false),
                // original Status Id
                originalStatusId = ko.observable(undefined),
                // Status Id
                statusId = ko.observable(undefined),
                // Status
                status = ko.observable(undefined),
                // Estimate signed by
                reportSignedBy = ko.observable(specifiedReportSignedBy || undefined),
                // Order Report Signed By
                orderReportSignedBy = ko.observable(specifiedOrderReportSignedBy || undefined),
                // Set Credit Limit Set By
                setOrderReportSignedBy = function (userId) {
                    if (!userId) {
                        return;
                    }
                    var user = getUserById(userId);
                    if (user) {
                        orderReportSignedByUser(user);
                    }
                },
                // Order Report Set By For User
                orderReportSignedByUser = ko.computed({
                    read: function () {
                        if (!orderReportSignedBy()) {
                            return SystemUser.Create({});
                        }
                        return getUserById(orderReportSignedBy());
                    },
                    write: function (value) {
                        if (!value) {
                            orderReportSignedBy(undefined);
                            return;
                        }
                        var userId = value.id;
                        if (userId === orderReportSignedBy()) {
                            return;
                        }
                        orderReportSignedBy(userId);
                    }
                }),
                // Store Id
                storeId = ko.observable(undefined),
                // invoice Status
                invoiceStatus = ko.observable(specifiedInvoiceStatus),
                // Has Deleted Items
                hasDeletedItems = ko.observable(false),
                // Has Deleted PrePayments
                hasDeletedPrepayments = ko.observable(false),
                // Has Deleted Delivery Schedules
                hasDeletedDeliverySchedules = ko.observable(false),
                // Errors
                errors = ko.validation.group({
                    name: name,
                    companyId: companyId,
                    sectionFlagId: sectionFlagId
                }),
                // Is Valid
                isValid = ko.computed(function () {
                    return errors().length === 0 &&
                        items.filter(function (item) {
                            return !item.isValid() && item.itemType() !== 2;
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

                    if (name.error) {
                        validationSummaryList.push({ name: "Order Title", element: name.domElement });
                    }
                    if (companyId.error) {
                        validationSummaryList.push({ name: "Customer", element: companyId.domElement });
                    }
                    if (sectionFlagId.error) {
                        validationSummaryList.push({ name: "Order Flag ", element: sectionFlagId.domElement });
                    }
                    
                    //if (items().length === 0) {
                    //    validationSummaryList.push({ name: "Please add item to print. " });
                    //}
                    // Show Item  Errors
                    var itemInvalid = items.find(function (item) {
                        return !item.isValid() && item.itemType() !== 2;
                    });

                    if (itemInvalid) {
                        var nameElement = items.domElement;
                        // Show Item Section Errors
                        var itemSectionInvalid = itemInvalid.itemSections.find(function (itemSection) {
                            return !itemSection.isValid();
                        });
                        var invalidSectionName = '';
                        if (itemSectionInvalid) {
                            invalidSectionName = itemSectionInvalid.name();
                        }
                        validationSummaryList.push({
                            name: itemInvalid.productName() + " has invalid data in Section named " + invalidSectionName,
                            element: nameElement
                        });
                    }
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
                    orderReportSignedBy: orderReportSignedBy,
                    reportSignedBy: reportSignedBy,
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
                    invoiceStatus:invoiceStatus,
                    statusId: statusId
                }),
                // Item Has Changes
                itemHasChanges = function () {
                    var itemChanges = items.find(function (item) {
                        return item.hasChanges();
                    });
                    return (itemChanges !== null && itemChanges !== undefined) || hasDeletedItems();
                },
                // Pre payment Has Changes
                prepaymentHasChanges = function () {
                    var prepaymentChanges = prePayments.find(function (item) {
                        return item.hasChanges();
                    });
                    return (prepaymentChanges !== null && prepaymentChanges !== undefined) || hasDeletedPrepayments();
                },
                // Delivery Schedule Has Changes
                deliveryScheduleHasChanges = function () {
                    var deliveryScheduleChange = deliverySchedules.find(function (item) {
                        return item.hasChanges();
                    });
                    return (deliveryScheduleChange !== null && deliveryScheduleChange !== undefined) || hasDeletedDeliverySchedules();
                },
                // Has Changes
                hasChanges = ko.computed(function () {
                    return dirtyFlag.isDirty() || itemHasChanges() || deliveryScheduleHasChanges() || prepaymentHasChanges();
                }),
                // Reset
                reset = function () {
                    items.each(function (item) {
                        return item.reset();
                    });
                    prePayments.each(function (item) {
                        return item.reset();
                    });
                    deliverySchedules.each(function (item) {
                        return item.reset();
                    });
                    hasDeletedItems(false);
                    hasDeletedDeliverySchedules(false);
                    hasDeletedPrepayments(false);
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
                        EstimateTotal: estimateTotal(),
                        SectionFlagId: sectionFlagId(),
                        IsDirectSale: isDirectSale(),
                        IsOfficialOrder: isOfficialOrder(),
                        IsCreditApproved: isCreditApproved(),
                        OrderDate: orderDate() ? moment(orderDate()).format(ist.utcFormat) + 'Z' : undefined,
                        StartDeliveryDate: startDeliveryDate() ? moment(startDeliveryDate()).format(ist.utcFormat) + 'Z' : undefined,
                        FinishDeliveryDate: finishDeliveryDate() ? moment(finishDeliveryDate()).format(ist.utcFormat) + 'Z' : undefined,
                        HeadNotes: headNotes(),
                        FootNotes: footNotes(),
                        EnquiryId: enquiryId(),
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
                        OrderReportSignedBy: orderReportSignedBy(),
                        ReportSignedBy: reportSignedBy(),
                        IsEstimate: isEstimate(),
                        PrePayments: [],
                        ShippingInformations: [],
                        Items: []
                    };
                };

            return {
                id: id,
                name: name,
                code: code,
                isFromEstimate: isFromEstimate,
                noOfItemsUi: noOfItemsUi,
                creationDate: creationDate,
                flagColor: flagColor,
                orderCode: orderCode,
                isEstimate: isEstimate,
                companyId: companyId,
                companyName: companyName,
                estimateTotal: estimateTotal,
                orderReportSignedBy: orderReportSignedBy,
                reportSignedBy: reportSignedBy,
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
                enquiryId: enquiryId,
                taxRate: taxRate,
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
                storeName:storeName,
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
                numberOfItems: numberOfItems,
                deliveryItems: deliveryItems,
                nonDeliveryItems: nonDeliveryItems,
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
                statusId: statusId,
                originalStatusId: originalStatusId,
                refEstimateId: refEstimateId,
                status: status,
                isExtraOrder:isExtraOrder,
                storeId: storeId,
                setCreditiLimitSetBy: setCreditiLimitSetBy,
                setAllowJobWoCreditCheckSetBy: setAllowJobWoCreditCheckSetBy,
                setOfficialOrderSetBy: setOfficialOrderSetBy,
                creditLimitSetByUser: creditLimitSetByUser,
                allowJobWoCreditCheckSetByUser: allowJobWoCreditCheckSetByUser,
                officialOrderSetByUser: officialOrderSetByUser,
                setOrderReportSignedBy: setOrderReportSignedBy,
                orderReportSignedByUser: orderReportSignedByUser,
                inquiryItems: inquiryItems,
                isInquiryItemLoaded: isInquiryItemLoaded,
                systemUsers: systemUsers,
                invoiceStatus: invoiceStatus,
                hasDeletedItems: hasDeletedItems,
                hasDeletedPrepayments: hasDeletedPrepayments,
                hasDeletedDeliverySchedules: hasDeletedDeliverySchedules
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
                amount = ko.observable(specifiedAmount).extend({ numberInput: ist.numberFormat }),

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
            specifiedDeliveryDate, specifiedEstimateId, specifiedAddressName, specifiedItemName) {
            var // Unique key
                shippingId = ko.observable(specifiedShippingId),
                // Item ID
                itemId = ko.observable(specifiedItemId).extend({ required: true }),
                // Address ID
                addressId = ko.observable(specifiedAddressId),
                // Quantity
                quantity = ko.observable(specifiedQuantity).extend({ number: true, required: true }),
                // Price
                price = ko.observable(specifiedPrice).extend({ numberInput: ist.numberFormat }),
                //Deliver Not Raised Flag
                deliveryNoteRaised = ko.observable(specifiedDeliveryNoteRaised !== undefined ? specifiedDeliveryNoteRaised : false),
                // Deliver Date
                deliveryDate = ko.observable((specifiedDeliveryDate === undefined || specifiedDeliveryDate === null) ?
                    (!specifiedEstimateId ? moment().add('days', 2).toDate() : moment().toDate()) :
                    moment(specifiedDeliveryDate).toDate()),
                // Formatted Delivery Date
                formattedDeliveryDate = ko.computed({
                    read: function () {
                        return deliveryDate() !== undefined ? moment(deliveryDate(), ist.datePattern).toDate() : new Date();
                    }
                }),
                // Item Name
                itemName = ko.observable(specifiedItemName || ''),
                // Address Name
                addressName = ko.observable(specifiedAddressName || ''),
                //
                isSelected = ko.observable(false),
                // Estimate ID
                estimateId = ko.observable(specifiedEstimateId || 0),
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
                        EstimateId: estimateId()
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
                estimateId: estimateId,
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
        SystemUser = function (specifiedId, specifiedName, specifiedFullName) {
            return {
                id: specifiedId,
                name: specifiedName,
                fullName: specifiedFullName
            };
        },
        // Pipeline Source Entity        
        PipeLineSource = function (specifiedId, specifiedDescription) {
            return {
                id: specifiedId,
                name: specifiedDescription
            };
        },
         // Pipeline Products Entity        
        PipeLineProduct = function (specifiedId, specifiedDescription) {
            return {
                id: specifiedId,
                name: specifiedDescription
            };
        },
        // Address Entity
        Address = function (specifiedId, specifiedName, specifiedAddress1, specifiedAddress2, specifiedTelephone1, specifiedIsDefault) {
            return {
                id: specifiedId,
                name: specifiedName,
                address1: specifiedAddress1 || "",
                address2: specifiedAddress2 || "",
                telephone1: specifiedTelephone1 || "",
                isDefault: specifiedIsDefault
            };
        },

        // Company Contact Entity
        CompanyContact = function (specifiedId, specifiedName, specifiedEmail, specifiedIsDefault) {
            // ReSharper restore InconsistentNaming
            return {
                id: specifiedId,
                name: specifiedName,
                email: specifiedEmail || "",
                isDefault: specifiedIsDefault
            };
        };

    // Estimate Factory
    Estimate.Create = function (source, constructorParams) {
        var estimate = new Estimate(source.EstimateId, source.EstimateCode, source.EstimateName, source.CompanyId, source.CompanyName, source.ItemsCount,
        source.CreationDate, source.FlagColor, source.SectionFlagId, source.OrderCode, source.IsEstimate, source.ContactId, source.AddressId, source.IsDirectSale,
        source.IsOfficialOrder, source.IsCreditApproved, source.OrderDate, source.StartDeliveryDate, source.FinishDeliveryDate, source.HeadNotes,
        source.ArtworkByDate, source.DataByDate, source.PaperByDate, source.TargetBindDate, source.XeroAccessCode, source.TargetPrintDate,
        source.OrderCreationDateTime, source.OrderManagerId, source.SalesPersonId, source.SourceId, source.CreditLimitForJob, source.CreditLimitSetBy,
        source.CreditLimitSetOnDateTime, source.IsJobAllowedWOCreditCheck, source.AllowJobWOCreditCheckSetOnDateTime, source.AllowJobWOCreditCheckSetBy,
        source.CustomerPo, source.OfficialOrderSetBy, source.OfficialOrderSetOnDateTime, source.FootNotes, source.EnquiryId, source.RefEstimateId,
        source.OrderReportSignedBy, source.ReportSignedBy, source.InvoiceStatus,source.StoreName);

        estimate.statusId(source.StatusId);
        estimate.originalStatusId(source.StatusId);
        estimate.status(source.Status);
        estimate.isExtraOrder(source.IsExtraOrder);
        
        estimate.systemUsers(constructorParams.SystemUsers);
        var total = (parseFloat((source.EstimateTotal === undefined || source.EstimateTotal === null) ? 0 : source.EstimateTotal)).toFixed(2);
        estimate.estimateTotal(total);
        // Map Items if any
        if (source.Items && source.Items.length > 0) {
            var items = [];

            _.each(source.Items, function (item) {
                items.push(itemModel.Item.Create(item));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(estimate.items(), items);
            estimate.items.valueHasMutated();
        }

        // Map Pre Payments if any
        if (source.PrePayments && source.PrePayments.length > 0) {
            var prePayments = [];

            _.each(source.PrePayments, function (item) {
                prePayments.push(PrePayment.Create(item));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(estimate.prePayments(), prePayments);
            estimate.prePayments.valueHasMutated();
        }

        // Map Delivery Schedules if any
        if (source.ShippingInformations && source.ShippingInformations.length > 0) {
            var deliverySchedules = [];

            _.each(source.ShippingInformations, function (item) {
                deliverySchedules.push(ShippingInformation.Create(item));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(estimate.deliverySchedules(), deliverySchedules);
            estimate.deliverySchedules.valueHasMutated();
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



    // #endregion __________________  COST CENTRE   ______________________

    // #region __________________  I N V E N T O R Y   ______________________

    // ReSharper disable once InconsistentNaming
    var Inventory = function (specifiedId, specifiedname,
        specifiedWeight, specifiedPackageQty, specifiedPerQtyQty, specifiedPrice) {

        var self,
            id = ko.observable(specifiedId),
            name = ko.observable(specifiedname),
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
                id: id,
                name: name,
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
                    StockItemId: id(),
                    ItemName: name(),
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
            id: id,
            name: name,
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
            source.perQtyQty || 0,
            source.PackCostPrice === -9999 ? 0 : source.PackCostPrice
            );
        return inventory;
    };
    // #endregion __________________   I N V E N T O R Y    ______________________

    //#region INQUIRIES

    var Inquiry = function (
        specifiedInquiryId, specifiedTitle, specifiedContactId, specifiedCreatedDate, specifiedSourceId, specifiedCompanyId, specifiedCompanyName, specifiedRequireByDate,
        specifiedSystemUserId, specifiedStatus, specifiedIsDirectInquiry, specifiedFlagId, specifiedInquiryCode, specifiedCreatedBy, specifiedOrganisationId, specifiedFlagColor, specifiedEstimateId, specifiedInquiryItemsCount
    ) {
        var self,
        inquiryId = ko.observable(specifiedInquiryId),
        title = ko.observable(specifiedTitle),
        contactId = ko.observable(specifiedContactId),
        createdDate = ko.observable(specifiedCreatedDate),
        sourceId = ko.observable(specifiedSourceId),
        companyId = ko.observable(specifiedCompanyId).extend({ required: true }),
        companyName = ko.observable(specifiedCompanyName),
        requireByDate = ko.observable(specifiedRequireByDate ? moment(specifiedRequireByDate).toDate() : moment().toDate()),
        systemUserId = ko.observable(specifiedSystemUserId),
        status = ko.observable(specifiedStatus),
        isDirectInquiry = ko.observable(specifiedIsDirectInquiry),
        flagId = ko.observable(specifiedFlagId).extend({ required: true }),
        inquiryCode = ko.observable(specifiedInquiryCode),
        createdBy = ko.observable(specifiedCreatedBy),
        organisationId = ko.observable(specifiedOrganisationId),
        flagColor = ko.observable(specifiedFlagColor),
        estimateId = ko.observable(specifiedEstimateId),
        inquiryItemsCount = ko.observable(specifiedInquiryItemsCount),
        inquiryAttachments = ko.observableArray([]),
        inquiryItems = ko.observableArray([]),
        // System Users
        systemUsers = ko.observableArray([]),
        //Pipe Line Sources
        pipelineSources = ko.observableArray([]),
        // Get User by Id
        getUserById = function (userId) {
            return systemUsers.find(function (user) {
                return user.id === userId;
            });
        },
        // System User Id Set By For User
        systemUserIdByUser = ko.computed({
            read: function () {
                if (!systemUserId()) {
                    return SystemUser.Create({});
                }
                return getUserById(systemUserId());
            },
            write: function (value) {
                if (!value) {
                    systemUserId(undefined);
                    return;
                }
                var userId = value.id;
                if (userId === systemUserId()) {
                    return;
                }
                systemUserId(userId);
            }
        }),
        // Number of Inquiry Items
        noOfInquiryItems = ko.computed(function () {
            return "( " + inquiryItemsCount() + " ) Items";
        }),
        // Get Pipe Line by Id
        getpipeLineById = function (userId) {
            return pipelineSources.find(function (source) {
                return source.id === userId;
            });
        },
        // System Pipe Line Id Set By For User
        systemPipeLineByUser = ko.computed({
            read: function () {
                if (!sourceId()) {
                    return SystemUser.Create({});
                }
                return getpipeLineById(sourceId());
            },
            write: function (value) {
                if (!value) {
                    sourceId(undefined);
                    return;
                }
                var userId = value.id;
                if (userId === sourceId()) {
                    return;
                }
                sourceId(userId);
            }
        }),
        errors = ko.validation.group({
            companyId: companyId,
            flagId: flagId,
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

            if (companyId.error) {
                validationSummaryList.push({ name: "Customer", element: companyId.domElement });
            }
            if (flagId.error) {
                validationSummaryList.push({ name: "Inquiry Flag ", element: flagId.domElement });
            }

        },
        // ReSharper disable once InconsistentNaming
        dirtyFlag = new ko.dirtyFlag({
            inquiryId: inquiryId,
            title: title,
            contactId: contactId,
            createdDate: createdDate,
            sourceId: sourceId,
            companyId: companyId,
            requireByDate: requireByDate,
            systemUserId: systemUserId,
            status: status,
            isDirectInquiry: isDirectInquiry,
            flagId: flagId,
            inquiryCode: inquiryCode,
            createdBy: createdBy,
            organisationId: organisationId,
            inquiryItems: inquiryItems,
            inquiryAttachments: inquiryAttachments
        }),
        // Has Changes
        hasChanges = ko.computed(function () {
            return dirtyFlag.isDirty();
        }),
        //Convert To Server
        convertToServerData = function () {
            return {
                InquiryId: inquiryId(),
                Title: title(),
                ContactId: contactId(),
                CreatedDate: createdDate() ? moment(createdDate()).format(ist.utcFormat) + 'Z' : undefined,
                SourceId: sourceId(),
                CompanyId: companyId(),
                RequireByDate: requireByDate() ? moment(requireByDate()).format(ist.utcFormat) + 'Z' : undefined,
                SystemUserId: systemUserId(),
                Status: status(),
                IsDirectInquiry: isDirectInquiry(),
                FlagId: flagId(),
                InquiryCode: inquiryCode(),
                CreatedBy: createdBy(),
                OrganisationId: organisationId(),
                InquiryAttachments: [],
                InquiryItems: []
            };
        },
        // Reset
        reset = function () {
            dirtyFlag.reset();
        };

        self = {
            inquiryId: inquiryId,
            title: title,
            contactId: contactId,
            createdDate: createdDate,
            sourceId: sourceId,
            companyId: companyId,
            companyName: companyName,
            requireByDate: requireByDate,
            systemUserId: systemUserId,
            status: status,
            isDirectInquiry: isDirectInquiry,
            flagId: flagId,
            inquiryItemsCount: inquiryItemsCount,
            inquiryCode: inquiryCode,
            createdBy: createdBy,
            organisationId: organisationId,
            flagColor: flagColor,
            inquiryAttachments: inquiryAttachments,
            inquiryItems: inquiryItems,
            systemUserIdByUser: systemUserIdByUser,
            systemPipeLineByUser: systemPipeLineByUser,
            systemUsers: systemUsers,
            pipelineSources: pipelineSources,
            estimateId: estimateId,
            noOfInquiryItems: noOfInquiryItems,
            isValid: isValid,
            errors: errors,
            showAllErrors: showAllErrors,
            setValidationSummary: setValidationSummary,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };

    Inquiry.Create = function (source, constructorParams) {
        var inquiry = new Inquiry(
              source.InquiryId,
              source.Title,
              source.ContactId,
              source.CreatedDate,
              source.SourceId,
              source.CompanyId,
              source.CompanyName,
              source.RequireByDate,
              source.SystemUserId,
              source.Status,
              source.IsDirectInquiry,
              source.FlagId,
              source.InquiryCode,
              source.CreatedBy,
              source.OrganisationId,
            source.FlagColor,
            source.EstimateId,
            source.InquiryItemsCount
            );
        // Map Items if any
        if (source.InquiryAttachments && source.InquiryAttachments.length > 0) {
            var items = [];
            _.each(source.InquiryAttachments, function (item) {
                items.push(InquiryAttachment.Create(item));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(inquiry.inquiryAttachments(), items);
            inquiry.inquiryAttachments.valueHasMutated();
        }
        if (source.InquiryItems && source.InquiryItems.length > 0) {
            items = [];
            _.each(source.InquiryItems, function (item) {
                items.push(InquiryItem.Create(item));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(inquiry.inquiryItems(), items);
            inquiry.inquiryItems.valueHasMutated();
        }
        if (constructorParams) {
            inquiry.systemUsers(constructorParams.SystemUsers);
            inquiry.pipelineSources(constructorParams.PipelineSources);
        }

        return inquiry;
    };
    //#endregion

    //#region INQUIRY ATTACHMENT

    var InquiryAttachment = function (
        specifiedAttachmentId, specifiedOrignalFileName, specifiedAttachmentPath, specifiedInquiryId, specifiedExtension
    ) {
        var self,
        attachmentId = ko.observable(specifiedAttachmentId),
        orignalFileName = ko.observable(specifiedOrignalFileName),
        attachmentPath = ko.observable(specifiedAttachmentPath),
        inquiryId = ko.observable(specifiedInquiryId),
        extension = ko.observable(specifiedExtension),
        errors = ko.validation.group({

        }),
        // Is Valid 
        isValid = ko.computed(function () {
            return errors().length === 0 ? true : false;
        }),

        // ReSharper disable once InconsistentNaming
        dirtyFlag = new ko.dirtyFlag({
            attachmentId: attachmentId,
            orignalFileName: orignalFileName,
            attachmentPath: attachmentPath,
            inquiryId: inquiryId,
            extension: extension
        }),
        // Has Changes
        hasChanges = ko.computed(function () {
            return dirtyFlag.isDirty();
        }),
        //Convert To Server
            convertToServerData = function () {
                return {
                    AttachmentId: attachmentId(),
                    OrignalFileName: orignalFileName(),
                    AttachmentPath: attachmentPath(),
                    InquiryId: inquiryId(),
                    Extension: extension()
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        //inquiryId title contactId createdDate sourceId companyId requireByDate systemUserId status
        //isDirectInquiry flagId inquiryCode createdBy organisationId
        self = {
            attachmentId: attachmentId,
            orignalFileName: orignalFileName,
            attachmentPath: attachmentPath,
            inquiryId: inquiryId,
            extension: extension,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };

    InquiryAttachment.Create = function (source) {
        var inquiryAttachment = new InquiryAttachment(
              source.AttachmentId,
              source.OrignalFileName,
              source.AttachmentPath,
              source.InquiryId,
              source.Extension
            );
        return inquiryAttachment;
    };

    //#endregion 

    //#region INQUIRY ITEMS

    var InquiryItem = function (
        specifiedInquiryItemId, specifiedTitle, specifiedNotes, specifiedDeliveryDate, specifiedInquiryId, specifiedProductId, specifiedMarketingSource
    ) {
        var self,
        inquiryItemId = ko.observable(specifiedInquiryItemId),
        title = ko.observable(specifiedTitle).extend({ required: true }),
        notes = ko.observable(specifiedNotes),
        deliveryDate = ko.observable(specifiedDeliveryDate ? moment(specifiedDeliveryDate).toDate() : moment().toDate()),
        inquiryId = ko.observable(specifiedInquiryId),
        productId = ko.observable(specifiedProductId),
        marketingSource = ko.observable(specifiedMarketingSource),
        errors = ko.validation.group({
            title: title
        }),
        // Show All Error Messages
        showAllErrors = function () {
            // Show Item Errors
            errors.showAllMessages();
        },
        // Is Valid 
        isValid = ko.computed(function () {
            return errors().length === 0 ? true : false;
        }),

        // ReSharper disable once InconsistentNaming
        dirtyFlag = new ko.dirtyFlag({
            inquiryItemId: inquiryItemId,
            title: title,
            notes: notes,
            deliveryDate: deliveryDate,
            inquiryId: inquiryId,
            productId: productId
        }),
        // Has Changes
        hasChanges = ko.computed(function () {
            return dirtyFlag.isDirty();
        }),
        //Convert To Server
            convertToServerData = function () {
                return {
                    InquiryItemId: inquiryItemId(),
                    Title: title(),
                    Notes: notes(),
                    DeliveryDate: deliveryDate() ? moment(deliveryDate()).format(ist.utcFormat) + 'Z' : undefined,
                    InquiryId: inquiryId(),
                    ProductId: productId()
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        //inquiryId title contactId createdDate sourceId companyId requireByDate systemUserId status
        //isDirectInquiry flagId inquiryCode createdBy organisationId
        self = {
            inquiryItemId: inquiryItemId,
            title: title,
            notes: notes,
            deliveryDate: deliveryDate,
            inquiryId: inquiryId,
            productId: productId,
            marketingSource: marketingSource,
            isValid: isValid,
            errors: errors,
            showAllErrors: showAllErrors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    InquiryItem.CreateFromClientModel = function (source) {
        return new InquiryItem(
            source.inquiryItemId,
              source.title,
              source.notes,
              source.deliveryDate,
              source.inquiryId,
              source.productId,
            source.MarketingSource
        );
    };
    InquiryItem.Create = function (source) {
        var inquiryItem = new InquiryItem(
              source.InquiryItemId,
              source.Title,
              source.Notes,
              source.DeliveryDate,
              source.InquiryId,
              source.ProductId,
            source.MarketingSource
            );
        return inquiryItem;
    };


    //#endregion 

    // Section Flag Factory
    SectionFlag.Create = function (source) {
        return new SectionFlag(source.SectionFlagId, source.FlagName, source.FlagColor);
    };



    // Address Factory
    Address.Create = function (source) {
        return new Address(source.AddressId, source.AddressName, source.Address1, source.Address2, source.Tel1, source.IsDefaultAddress);
    };

    // Company Contact Factory
    CompanyContact.Create = function (source) {
        return new CompanyContact(source.ContactId, source.Name, source.Email, source.IsDefaultContact);
    };

    // System User Factory
    SystemUser.Create = function (source) {
        return new SystemUser(source.SystemUserId, source.UserName, source.FullName);
    };

    // Pipeline Source Factory
    PipeLineSource.Create = function (source) {
        return new PipeLineSource(source.SourceId, source.Description);
    };

    // Pipeline Product Factory
    PipeLineProduct.Create = function (source) {
        return new PipeLineProduct(source.ProductId, source.Description);
    };

    // Pre Payment Factory
    PrePayment.Create = function (source) {
        return new PrePayment(source.PrePaymentId, source.CustomerId, source.OrderId, source.Amount, source.PaymentDate, source.PaymentMethodId,
            source.PaymentMethodName, source.ReferenceCode, source.PaymentDescription);
    };

    ShippingInformation.Create = function (source) {
        return new ShippingInformation(source.ShippingId, source.ItemId, source.AddressId, source.Quantity, source.Price, source.DeliveryNoteRaised,
            source.DeliveryDate, source.EstimateId, source.AddressName, source.ItemName);
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
        // PipeLine Product Constructor
        PipeLineProduct: PipeLineProduct,
        // Status Enum
        Status: Status,
        // Pre Payment Constructor
        PrePayment: PrePayment,
        // Inventory
        Inventory: Inventory,
        // Shipping Information Constructor
        ShippingInformation: ShippingInformation,
        //Inquiry
        Inquiry: Inquiry,
        //Inquiry Attachment
        InquiryAttachment: InquiryAttachment,
        //Inquiry Item
        InquiryItem: InquiryItem
    };
});