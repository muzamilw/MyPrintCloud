
// Contact View 
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var
        // #region _______________ORDER/ESTIMATE ___________________
        // Estimate Entity
        // ReSharper disable InconsistentNaming
        // Estimate Entity
    // ReSharper disable InconsistentNaming
    Estimate = function (specifiedId, specifiedCode, specifiedName, specifiedCompanyId, specifiedCompanyName, specifiedNumberOfItems, specifiedCreationDate,
        specifiedFlagColor, specifiedSectionFlagId, specifiedOrderCode, specifiedIsEstimate, specifiedContactId, specifiedAddressId, specifiedIsDirectSale,
        specifiedIsOfficialOrder, specifiedIsCreditApproved, specifiedOrderDate, specifiedStartDeliveryDate, specifiedFinishDeliveryDate,
        specifiedHeadNotes, specifiedArtworkByDate, specifiedDataByDate, specifiedPaperByDate, specifiedTargetBindDate, specifiedXeroAccessCode,
        specifiedTargetPrintDate, specifiedOrderCreationDateTime, specifiedOrderManagerId, specifiedSalesPersonId, specifiedSourceId,
        specifiedCreditLimitForJob, specifiedCreditLimitSetBy, specifiedCreditLimitSetOnDateTime, specifiedIsJobAllowedWOCreditCheck,
        specifiedAllowJobWOCreditCheckSetOnDateTime, specifiedAllowJobWOCreditCheckSetBy, specifiedCustomerPo, specifiedOfficialOrderSetBy,
        specifiedOfficialOrderSetOnDateTime, specifiedFootNotes, specifiedStatus, specifiedEstimateTotal) {
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
            estimateTotal = ko.observable(specifiedEstimateTotal || undefined).extend({ numberInput: ist.numberFormat }),
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
            // Status
            status = ko.observable(specifiedStatus || undefined),
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
                statusId: statusId,
                estimateTotal: estimateTotal
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
            isFromEstimate: isFromEstimate,
            noOfItemsUi: noOfItemsUi,
            creationDate: creationDate,
            flagColor: flagColor,
            orderCode: orderCode,
            isEstimate: isEstimate,
            companyId: companyId,
            companyName: companyName,
            estimateTotal: estimateTotal,
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
            statusId: statusId,
            status: status
        };
    },
          // #region ______________  CUSTOMER LIST VIEW MODEL   _________________
     customerViewListModel = function (companytId, custName, custCraetionDate, custStatus, cusStatusClass, custEmail, cusStoreImageFileBinary,cusStoreName) {
         var
             self,
             id = ko.observable(companytId),
             name = ko.observable(custName),
             creationdate = ko.observable(custCraetionDate),
             status = ko.observable(custStatus),
             customerTYpe = ko.observable(undefined),
             statusClass = ko.observable(cusStatusClass),
             storeImageFileBinary = ko.observable(cusStoreImageFileBinary),
             email = ko.observable(custEmail),
             defaultContact = ko.observable(undefined),
             defaultContactEmail = ko.observable(undefined),
             storeName = ko.observable(cusStoreName),
             // Errors
             errors = ko.validation.group({
             }),
             // Is Valid 
             isValid = ko.computed(function () {
                 return errors().length === 0 ? true : false;
             }),
             // ReSharper disable InconsistentNaming
             dirtyFlag = new ko.dirtyFlag({
             }),
             // Has Changes
             hasChanges = ko.computed(function () {
                 return dirtyFlag.isDirty();
             }),
             //Convert To Server
             convertToServerData = function (source) {
             },
             // Reset
             reset = function () {
                 dirtyFlag.reset();
             };
         self = {
             id: id,
             name: name,
             creationdate: creationdate,
             status: status,
             statusClass: statusClass,
             email: email,
             customerTYpe: customerTYpe,
             defaultContact: defaultContact,
             defaultContactEmail: defaultContactEmail,
             storeImageFileBinary: storeImageFileBinary,
             storeName: storeName,
             isValid: isValid,
             errors: errors,
             dirtyFlag: dirtyFlag,
             hasChanges: hasChanges,
             convertToServerData: convertToServerData,
             reset: reset
         };
         return self;
     };
    customerViewListModel.Create = function (source) {
        var statusClass = null;
        if (source.Status == "Inactive")
            statusClass = 'label label-danger';
        if (source.Status == "Active")
            statusClass = 'label label-success';
        if (source.Status == "Banned")
            statusClass = 'label label-default';
        if (source.Status == "Pending")
            statusClass = 'label label-warning';
        var customerType = null;
        var customer = new customerViewListModel(
            source.CompnayId,
            source.CustomerName,
            source.DateCreted,
            source.Status,
            statusClass,
            source.Email,
            source.StoreImagePath
        );
        customer.defaultContact(source.DefaultContactName);
        customer.defaultContactEmail(source.DefaultContactEmail);
        customer.customerTYpe(source.CustomerType);
        customer.storeName(source.StoreName);
        return customer;
    };

    // #endregion;


    TotalEarnings = function (specifiedMonth, specifiedOrders, specifiedTotal, specifiedmonthname, specifiedyear, specifiedstore) {
        //var //
        //    month :specifiedMonth;
        //    orders = ko.observable(specifiedOrders),
        //    total = ko.observable(specifiedTotal),
        //    monthname = ko.observable(specifiedmonthname),
        //    year = ko.observable(specifiedyear),
        //    store = ko.observable(specifiedstore);

        return {
            month: specifiedMonth || 0,
            orders: specifiedOrders || 0,
            total: specifiedTotal || 0,
            monthname: specifiedmonthname || 0,
            year: specifiedyear || 0,
            store: specifiedstore || "",
            flag: 1
        };
    };

    TotalEarnings.Create = function(source) {
        return new TotalEarnings(source.Month, source.Orders, source.Total, source.monthname, source.year, source.store);
    };
    
    //Registered Users Model
    RegisteredUser = function (specifiedMonth, specifiedTotalStore1, specifiedTotalStore2, specifiedTotalStore3,specifiedTotalStore4,specifiedTotalStore5, specifiedmonthname, specifiedyear, specifiedstore) {
        
            
        return {
            month: specifiedMonth || 0,
            totalStore1: specifiedTotalStore1 || 0,
            totalStore2: specifiedTotalStore2 || 0,
            totalStore3: specifiedTotalStore3 || 0,
            totalStore4: specifiedTotalStore4 || 0,
            totalStore5: specifiedTotalStore5 || 0,
            monthname: specifiedmonthname || "",
            year: specifiedyear || 0,
            store: specifiedstore || ""
        };
    };
    
    RegisteredUser.Create = function (source, valOrder) {
        return new RegisteredUser(source.Month, source.TotalContacts, valOrder, source.MonthName, source.Year, source.Name);
    };


    // Estimate Factory
    // Estimate Factory
    Estimate.Create = function (source) {
        var estimate = new Estimate(source.EstimateId, source.EstimateCode, source.EstimateName, source.CompanyId, source.CompanyName, source.ItemsCount,
        source.CreationDate, source.FlagColor, source.SectionFlagId, source.OrderCode, source.IsEstimate, source.ContactId, source.AddressId, source.IsDirectSale,
        source.IsOfficialOrder, source.IsCreditApproved, source.OrderDate, source.StartDeliveryDate, source.FinishDeliveryDate, source.HeadNotes,
        source.ArtworkByDate, source.DataByDate, source.PaperByDate, source.TargetBindDate, source.XeroAccessCode, source.TargetPrintDate,
        source.OrderCreationDateTime, source.SalesAndOrderManagerId, source.SalesPersonId, source.SourceId, source.CreditLimitForJob, source.CreditLimitSetBy,
        source.CreditLimitSetOnDateTime, source.IsJobAllowedWOCreditCheck, source.AllowJobWOCreditCheckSetOnDateTime, source.AllowJobWOCreditCheckSetBy,
        source.CustomerPo, source.OfficialOrderSetBy, source.OfficialOrderSetOnDateTime, source.FootNotes, source.Status, source.EstimateTotal);
        estimate.statusId(source.StatusId);
        estimate.status(source.Status || undefined);
        estimate.flagColor(source.SectionFlagColor || undefined);
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
    // #endregion 


    return {
        Estimate: Estimate,
        TotalEarnings: TotalEarnings,
        RegisteredUser:RegisteredUser,
        customerViewListModel: customerViewListModel
    };
});
