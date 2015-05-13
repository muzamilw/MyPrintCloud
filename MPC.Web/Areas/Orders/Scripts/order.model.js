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
            specifiedOfficialOrderSetOnDateTime, specifiedFootNotes) {
            // ReSharper restore InconsistentNaming
            var // Unique key
                id = ko.observable(specifiedId || 0),
                // Name
                name = ko.observable(specifiedName || undefined).extend({ required: true }),
                // Code
                code = ko.observable(specifiedCode || undefined),
                // Is From Estimate
                isFromEstimate = ko.computed(function() {
                    return code() !== null && code() !== undefined && code() !== "";
                }),
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
                isDirectSaleUi = ko.computed(function() {
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
                // Status Id
                statusId = ko.observable(undefined),
                // Status
                status = ko.observable(undefined),
                // Order signed by
                orderReportSignedBy = ko.observable(undefined),
                // Store Id
                storeId = ko.observable(undefined),
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


                    // Show Item  Errors
                    var itemInvalid = items.find(function (item) {
                        return !item.isValid() && item.itemType() !== 2;
                    });

                    if (itemInvalid) {
                        var nameElement = items.domElement;
                        validationSummaryList.push({ name: itemInvalid.productName() + " has invalid data.", element: nameElement });
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
                    taxRate: taxRate,
                    sectionFlagId: sectionFlagId,
                    statusId: statusId
                    //estimateTotal: estimateTotal
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
                        TaxRate: taxRate(),
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
                status: status,
                storeId: storeId
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
            specifiedDeliveryDate, specifiedEstimateId) {
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
                

        // Company Contact Entity
        CompanyContact = function (specifiedId, specifiedName, specifiedEmail) {
            // ReSharper restore InconsistentNaming
            return {
                id: specifiedId,
                name: specifiedName,
                email: specifiedEmail || ""
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
        estimate.status(source.Status);
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
        return new SystemUser(source.SystemUserId, source.UserName, source.FullName);
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
        return new ShippingInformation(source.ShippingId, source.ItemId, source.AddressId, source.Quantity, source.Price, source.DeliveryNoteRaised, source.DeliveryDate, source.EstimateId);
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
        // Status Enum
        Status: Status,
        // Pre Payment Constructor
        PrePayment: PrePayment,
        // Inventory
        Inventory: Inventory,
        // Shipping Information Constructor
        ShippingInformation: ShippingInformation
    };
});