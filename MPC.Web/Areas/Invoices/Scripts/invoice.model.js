/*
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
        Invoice = function (specifiedId, specifiedCode, specifiedType, specifiedName, specifiedCompanyId, specifiedCompanyName, specifiedContactId, specifiedOrderNo,
            specifiedStatus, specifiedTotal, specifiedInvoiceDate, specifiedAccountNo, specifiedTerms, specifiedAddressId, specifiedIsArchive,
            specifiedTaxValue, specifiedGrandTotal, specifiedFlagId, specifiedNotes, specifiedEstimateId,
            specifiedIsProforma, specifiedIsPrinted, specifiedSignedBy, specifiedHeadNotes, specifiedFootNotes, specifiedPostingDate, specifiedXeroAccessCode) {
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
                companyName = ko.observable(specifiedCompanyName),
                // Number Of items
                numberOfItems = ko.observable(),
                // Number of Items UI
                noOfItemsUi = ko.computed(function () {
                    return "( " + numberOfItems() + " ) Items";
                }),
                // Creation Date
                invoiceDate = ko.observable(specifiedInvoiceDate ? moment(specifiedInvoiceDate).toDate() : undefined),
                // Flag Color
                flagColor = ko.observable(),
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
                isProformaInvoice = ko.observable(specifiedIsProforma),
                invoiceReportSignedBy = ko.observable(specifiedSignedBy),
                estimateId = ko.observable(specifiedEstimateId),
                headNotes = ko.observable(specifiedHeadNotes),
                footNotes = ko.observable(specifiedFootNotes),
                xeroAccessCode = ko.observable(specifiedXeroAccessCode),
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
                xeroAccessCode: xeroAccessCode,
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
        // Invoice Detail Entity
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
                    invoiceTitle: invoiceTitle,
                    itemCharge: itemCharge,
                    quantity: quantity,
                    itemTaxValue: itemTaxValue,
                    description: description,
                    itemType: itemType,
                    taxId: taxId
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
                        InvoiceDetailId: id(),
                        InvoiceId: invoiceId(),
                        DetailType: detailType(),
                        ItemId: itemId(),
                        InvoiceTitle: invoiceTitle(),
                        ItemCharge: itemCharge(),
                        Quantity: quantity(),
                        ItemTaxValue: itemTaxValue(),
                        FlagId: flagId(),
                        Description: description(),
                        ItemType: itemType(),
                        TaxId: taxId()
                    };
                };

            return {
                id: id,
                invoiceId: invoiceId,
                detailType: detailType,
                itemId: itemId,
                invoiceTitle: invoiceTitle,
                itemCharge: itemCharge,
                quantity: quantity,
                itemTaxValue: itemTaxValue,
                flagId: flagId,
                description: description,
                itemType: itemType,
                taxId: taxId,
                errors: errors,
                isValid: isValid,
                showAllErrors: showAllErrors,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                reset: reset,
                setValidationSummary: setValidationSummary,
                convertToServerData: convertToServerData
            };
        };
        
     
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
    // Item Section Factory
    Invoice.Create = function (source) {
        var invoice = new Invoice(source.InvoiceId, source.InvoiceCode, source.InvoiceType, source.InvoiceName, source.CompanyId, source.CompanyName, source.ContactId, source.OrderNo,
            source.InvoiceStatus, source.InvoiceTotal, source.InvoiceDate, source.AccountNumber,
            source.Terms, source.AddressId, source.IsArchive,
            source.TaxValue, source.GrandTotal, source.FlagID, source.UserNotes, source.EstimateId,
            source.IsProformaInvoice, source.IsPrinted, source.ReportSignedBy, source.HeadNotes, source.FootNotes,
            source.InvoicePostingDate, source.XeroAccessCode);

        // Map Section Cost Centres if Any
        if (source.invoiceDetailItems && source.invoiceDetailItems.length > 0) {
            var invDetailItems = [];

            _.each(source.invoiceDetailItems, function (invdetail) {
                invDetailItems.push(InvoiecDetail.Create(invdetail));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(invoice.invoiceDetailItems(), invDetailItems);
            invoice.invoiceDetailItems.valueHasMutated();
        }

        return invoice;
    };

    // Item Factory
    InvoiecDetail.Create = function (source) {
        var invDetail = new InvoiecDetail(source.InvoiceDetailId, source.InvoiceId, source.DetailType, source.ItemId, source.InvoiceTitle,
            source.ItemCharge, source.Quantity, source.ItemTaxValue, source.FlagId, source.Description,
            source.ItemType, source.TaxId);

        
        return invDetail;
    };

    InvoicesListView = function (specifiedId, specifiedName, specifiedType, specifiedCode, specifiedCompanyName, specifiedInvoiceDate, specifiedItemsCount,
                            specifiedFlagColor, specifiedInvoiceTotal, specifiedOrderNo) {
        var
            self,
            //Unique ID
            id = ko.observable(specifiedId),
            //Name
            name = ko.observable(specifiedName),
            //Type
            type = ko.observable(specifiedType),
            code = ko.observable(specifiedCode),
            companyName = ko.observable(specifiedCompanyName),
            invoiceDate = ko.observable(specifiedInvoiceDate),
            itemsCount = ko.observable(specifiedItemsCount),
            flagColor = ko.observable(specifiedFlagColor),
            invoiceTotal = ko.observable(specifiedInvoiceTotal),
            isDirectSale = ko.observable(specifiedOrderNo == null ? true : false),            
                // Number of Items UI
                noOfItemsUi = ko.computed(function () {
                    return "( " + itemsCount() + " ) Items";
                }),
            convertToServerData = function () {
                return {
                    InvoiceId: id(),
                }
            };
        self = {
            id:id,
            name: name,
            type: type,
            code: code,
            companyName: companyName,
            invoiceDate: invoiceDate,
            itemsCount: itemsCount,
            flagColor: flagColor,
            invoiceTotal: invoiceTotal,
            convertToServerData: convertToServerData,
            isDirectSale: isDirectSale,
            noOfItemsUi: noOfItemsUi
        };
        return self;
    };

    InvoicesListView.Create = function (source) {
        return new InvoicesListView(source.InvoiceId, source.InvoiceName, source.InvoiceType, source.InvoiceCode,
            source.CompanyName, source.InvoiceDate, source.ItemsCount, source.FlagColor, source.GrandTotal, source.OrderNo);
    };

    return {
        
        Invoice: Invoice,        
        InvoicesListView: InvoicesListView,
        Address: Address,
        CompanyContact: CompanyContact
    };
});