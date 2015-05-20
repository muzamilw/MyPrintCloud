﻿/*
    Module with the model for the Invoice
*/
define(["ko", "common/itemDetail.model", "underscore", "underscore-ko"], function (ko, itemModel) {
    var
      // Invoice Entity
      Invoice = function (specifiedId, specifiedCode, specifiedType, specifiedName, specifiedCompanyId, specifiedCompanyName, specifiedContactId, specifiedOrderNo,
            specifiedStatus, specifiedTotal, specifiedInvoiceDate, specifiedAccountNo, specifiedTerms, specifiedAddressId, specifiedIsArchive,
            specifiedTaxValue, specifiedGrandTotal, specifiedFlagId, specifiedNotes, specifiedEstimateId,
            specifiedIsProforma, specifiedIsPrinted, specifiedSignedBy, specifiedHeadNotes, specifiedFootNotes, specifiedPostingDate, specifiedXeroAccessCode,
            specifiedStatusName, specifiedInvoicePostedBy) {
          // ReSharper restore InconsistentNaming
          var // Unique key
              id = ko.observable(specifiedId || 0),
              // Name
              name = ko.observable(specifiedName || undefined).extend({ required: true }),
              // Code
              code = ko.observable(specifiedCode || undefined),
              type = ko.observable(specifiedType),
              // Company Id
              companyId = ko.observable(specifiedCompanyId || undefined).extend({ required: true }),
              // Company Name
              companyName = ko.observable(specifiedCompanyName),
              // store Id
              storeId = ko.observable(),
              // Number Of items
              numberOfItems = ko.observable(),
              statusName = ko.observable(specifiedStatusName),
              // Estimate Total
              estimateTotal = ko.observable(0).extend({ numberInput: ist.numberFormat }),
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
              orderNo = ko.observable(specifiedOrderNo || "N/A"),
              invoiceStatus = ko.observable(specifiedStatus),
              invoiceStatusText = ko.computed(function () {
                  return ((invoiceStatus === undefined || invoiceStatus() === null || invoiceStatus() === 19) ? "Awaiting" : "Posted");
              }),
              accountNo = ko.observable(specifiedAccountNo),
              terms = ko.observable(specifiedTerms || undefined),
              invoicePostingDate = ko.observable(specifiedPostingDate || undefined),
              invoicePostedBy = ko.observable(specifiedInvoicePostedBy),
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
              items = ko.observableArray([]),
              isProformaInvoice = ko.observable(specifiedIsProforma),
              invoiceReportSignedBy = ko.observable(specifiedSignedBy),
              estimateId = ko.observable(specifiedEstimateId),
              headNotes = ko.observable(specifiedHeadNotes),
              footNotes = ko.observable(specifiedFootNotes),
              xeroAccessCode = ko.observable(specifiedXeroAccessCode),
              isDirectSale = ko.observable(specifiedOrderNo == null ? true : false),
              isPostedInvoice = ko.observable(invoiceStatus === 20 ? true : false),
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
              // Is Direct Sale Ui
              isDirectSaleUi = ko.computed(function () {
                  return isDirectSale() ? "Direct Order" : "Online Order";
              }),
              // Errors
              errors = ko.validation.group({
                  name: name,
                  companyId: companyId,
                  // sectionFlagId: sectionFlagId
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
                      validationSummaryList.push({ name: "Invoice Title", element: name.domElement });
                  }
                  if (companyId.error) {
                      validationSummaryList.push({ name: "Customer", element: companyId.domElement });
                  }
                  if (sectionFlagId.error) {
                      validationSummaryList.push({ name: "Invoice Flag ", element: sectionFlagId.domElement });
                  }


                  // Show Item  Errors
                  var itemInvalid = items.find(function (item) {
                      return !item.isValid() && item.itemType() !== 2;
                  });

                  if (itemInvalid) {
                      var nameElement = items.domElement;
                      validationSummaryList.push({ name: itemInvalid.productName() + "has invalid data.", element: nameElement });
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
                  invoiceReportSignedBy: invoiceReportSignedBy,
                  type: type,
                  headNotes: headNotes,
                  footNotes: footNotes,
                  items: items
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
                      InvoiceType: type(),
                      XeroAccessCode: xeroAccessCode(),
                      InvoiceDetails: [],
                      Items: []
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
              nonDeliveryItems: nonDeliveryItems,
              taxValue: taxValue,
              grandTotal: grandTotal,
              userNotes: userNotes,
              notesUpdateDateTime: notesUpdateDateTime,
              invoiceStatusText: invoiceStatusText,
              estimateId: estimateId,
              isProformaInvoice: isProformaInvoice,
              invoiceReportSignedBy: invoiceReportSignedBy,
              headNotes: headNotes,
              footNotes: footNotes,
              type: type,
              xeroAccessCode: xeroAccessCode,
              isDirectSaleUi: isDirectSaleUi,
              isDirectSale: isDirectSale,
              invoiceDetailItems: invoiceDetailItems,
              deliveryItems: deliveryItems,
              statusName: statusName,
              storeId: storeId,
              estimateTotal: estimateTotal,
              errors: errors,
              isValid: isValid,
              showAllErrors: showAllErrors,
              dirtyFlag: dirtyFlag,
              hasChanges: hasChanges,
              reset: reset,
              setValidationSummary: setValidationSummary,
              convertToServerData: convertToServerData,
              isPostedInvoice: isPostedInvoice,
              items: items

          };
      };

    //  Invoice Detail Entity
    var InvoiceDetail = function (specifiedInvoiceDetailId, specifiedInvoiceTitle, specifiedItemCharge, specifiedQuantity, specifiedItemTaxValue,
    specifiedFlagId, specifiedDescription, specifiedDetailType, specifiedItemType, specifiedTaxValue, specifiedItemGrossTotal) {
        var self,
            id = ko.observable(specifiedInvoiceDetailId),
            // Invoice Title 
            productName = ko.observable(specifiedInvoiceTitle),
            itemCharge = ko.observable(specifiedItemCharge).extend({ required: true, numberInput: ist.numberFormat }),
            // Quantity
            qty1 = ko.observable(specifiedQuantity).extend({
                number: true,
                required: true,
            }),
            tax = ko.observable(specifiedTaxValue).extend({ number: true }),
            itemTaxValue = ko.observable(specifiedItemTaxValue).extend({ numberInput: ist.numberFormat }),
            flagId = ko.observable(specifiedFlagId),
            detailType = ko.observable(specifiedDetailType),
            itemType = ko.observable(specifiedItemType),
            description = ko.observable(specifiedDescription),
           // itemGrossTotal = ko.observable(specifiedItemGrossTotal),
            // For List View
            qty1GrossTotal = ko.observable(specifiedItemGrossTotal).extend({ numberInput: ist.numberFormat }),

        // Errors
    errors = ko.validation.group({
        itemCharge: itemCharge,
        qty1: qty1,
        tax: tax
    }),
        // Is Valid 
    isValid = ko.computed(function () {
        return errors().length === 0 ? true : false;
    }),

    dirtyFlag = new ko.dirtyFlag({
        productName: productName,
        itemCharge: itemCharge,
    }),
        // Has Changes
    hasChanges = ko.computed(function () {
        return dirtyFlag.isDirty();
    }),
        //Convert To Server
    convertToServerData = function (source) {
        if (source) {
            var result = {};
            result.InvoiceDetailId = source.id();
            result.InvoiceTitle = source.productName();
            result.ItemCharge = source.itemCharge() === null ? 0 : source.itemCharge();
            result.Quantity = source.qty1();
            result.ItemTaxValue = source.itemTaxValue();
            result.TaxValue = source.tax();
            result.FlagId = source.flagId();
            result.Description = source.description();
            result.ItemType = source.itemType();
            result.DetailType = source.detailType();
            result.ItemGrossTotal = source.qty1GrossTotal();
            return result;
        }

    },
        // Reset
    reset = function () {
        dirtyFlag.reset();
    };
        self = {
            id: id,
            productName: productName,
            itemCharge: itemCharge,
            qty1: qty1,
            itemTaxValue: itemTaxValue,
            flagId: flagId,
            description: description,
            qty1GrossTotal: qty1GrossTotal,
            detailType: detailType,
            itemType: itemType,
            //itemGrossTotal: itemGrossTotal,
            tax: tax,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    //  Invoice Detail Factory
    InvoiceDetail.Create = function (source) {
        return new InvoiceDetail(source.InvoiceDetailId, source.InvoiceTitle, source.ItemCharge, source.Quantity, source.ItemTaxValue,
            source.FlagId, source.Description, source.DetailType, source.ItemType, source.TaxValue, source.ItemGrossTotal);
    }

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
        };
    // Address Factory
    Address.Create = function (source) {
        return new Address(source.AddressId, source.AddressName, source.Address1, source.Address2, source.Tel1);
    };

    // Company Contact Factory
    CompanyContact.Create = function (source) {
        return new CompanyContact(source.ContactId, source.Name, source.Email);
    };
    // Item Section Factory
    Invoice.Create = function (source) {
        var invoice = new Invoice(source.InvoiceId, source.InvoiceCode, source.InvoiceType, source.InvoiceName, source.CompanyId, source.CompanyName, source.ContactId, source.OrderNo,
            source.InvoiceStatus, source.InvoiceTotal, source.InvoiceDate, source.AccountNumber,
            source.Terms, source.AddressId, source.IsArchive,
            source.TaxValue, source.GrandTotal, source.FlagId, source.UserNotes, source.EstimateId,
            source.IsProformaInvoice, source.IsPrinted, source.ReportSignedBy, source.HeadNotes, source.FootNotes,
            source.InvoicePostingDate, source.XeroAccessCode, source.Status, source.InvoicePostedBy);

        var items = [];
        // Map invoice items if Any
        if (source.InvoiceDetails && source.InvoiceDetails.length > 0) {
            _.each(source.InvoiceDetails, function (invdetail) {
                items.push(InvoiceDetail.Create(invdetail));
            });
        }

        // Map Items if any
        if (source.Items && source.Items.length > 0) {
            _.each(source.Items, function (item) {
                items.push(itemModel.Item.Create(item));
            });
        }
        // Push to Original Item
        ko.utils.arrayPushAll(invoice.items(), items);
        invoice.items.valueHasMutated();
        return invoice;
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
            invoiceTotal = ko.observable(specifiedInvoiceTotal).extend({ numberInput: ist.numberFormat }),
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
            id: id,
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
            source.CompanyName, source.InvoiceDate, source.ItemsCount, source.FlagColor, source.InvoiceTotal, source.OrderNo);
    };

    return {

        Invoice: Invoice,
        InvoicesListView: InvoicesListView,
        Address: Address,
        CompanyContact: CompanyContact,
        InvoiceDetail: InvoiceDetail
    };
});