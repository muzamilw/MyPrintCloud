/*
    Module with the model for the  Delivery Notes
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

        // #region __________________  Purchase List View   ______________________
        PurchaseListView = function (specifiedPurchaseId, specifiedCode, specifiedPurchaseDate, specifiedRefNo,
            specifiedCompanyName, specifiedFlagColor) {

            var self,
                id = ko.observable(specifiedPurchaseId),
                code = ko.observable(specifiedCode),
                purchaseOrderDate = ko.observable(specifiedPurchaseDate !== null ? moment(specifiedPurchaseDate).toDate() : undefined),
                companyName = ko.observable(specifiedCompanyName),
                refNo = ko.observable(specifiedRefNo),
                flagColor = ko.observable(specifiedFlagColor);

            return {
                id: id,
                code: code,
                purchaseOrderDate: purchaseOrderDate,
                flagColor: flagColor,
                companyName: companyName,
                refNo: refNo,
            };

        },
        // #endregion __________________  Purchase List View    ______________________

        // #region __________________  System User   ______________________
        SystemUser = function (specifiedId, specifiedName, specifiedFullName) {
            return {
                id: specifiedId,
                name: specifiedName,
                fullName: specifiedFullName
            };
        },
        // #endregion __________________  Section Flag  ______________________

        // #region __________________  Section Flag   ______________________
        // Section Flag Entity        
        SectionFlag = function (specifiedId, specifiedFlagName, specifiedFlagColor) {
            // ReSharper restore InconsistentNaming
            return {
                id: specifiedId,
                name: specifiedFlagName,
                color: specifiedFlagColor
            };
        },
        // #endregion __________________  Section Flag  ______________________

        // #region __________________  Purchase  ______________________
        Purchase = function (specifiedPurchaseId, specifiedcode, specifieddate_Purchase, spcSupplierId, spcContactId,
            specifiedRefNo, spcSupplierContactAddressID, spcStatus, specifiedflagId, spcComments, spcFootnote,
            spcCreatedBy, spcDiscount, spcdiscountType, spcTotalPrice, spcNetTotal, spcTotalTax, spcGrandTotal, spcisproduct) {

            var self,
                id = ko.observable(specifiedPurchaseId),
                code = ko.observable(specifiedcode),
                purchaseDate = ko.observable(specifieddate_Purchase !== null ? moment(specifieddate_Purchase).toDate() : undefined),
                flagId = ko.observable(specifiedflagId),
                reffNo = ko.observable(specifiedRefNo),
                footnote = ko.observable(spcFootnote),
                comments = ko.observable(spcComments),
                status = ko.observable(spcStatus),
                contactId = ko.observable(spcContactId),
                // Store Id
                storeId = ko.observable(undefined),
                addressId = ko.observable(spcSupplierContactAddressID),
                createdBy = ko.observable(spcCreatedBy),
                discountType = ko.observable(spcdiscountType),
                totalPrice = ko.observable(spcTotalPrice),
                netTotal = ko.observable(spcNetTotal),
                isproduct = ko.observable(spcisproduct),
                totalTax = ko.observable(spcTotalTax),
                grandTotal = ko.observable(spcGrandTotal),
                supplierId = ko.observable(spcSupplierId).extend({ required: true }),
                //supplierTelNo = ko.observable(spcSupplierTelNo),
                discount = ko.observable(spcDiscount),
                companyName = ko.observable(undefined),
                taxRate = ko.observable(0),
                purchaseDetails = ko.observableArray([]),
                // Set Validation Summary
                setValidationSummary = function (validationSummaryList) {
                    validationSummaryList.removeAll();
                    if (supplierId.error) {
                        validationSummaryList.push({ name: "Customer", element: supplierId.domElement });
                    }
                },
                // Errors
                errors = ko.validation.group({
                    supplierId: supplierId
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
                // True if the order has been changed
                dirtyFlag = new ko.dirtyFlag({
                    purchaseDate: purchaseDate,
                    flagId: flagId,
                    reffNo: reffNo,
                    footnote: footnote,
                    comments: comments,
                    status: status,
                    contactId: contactId,
                    isproduct: isproduct,
                    storeId: storeId,
                    addressId: addressId,
                    createdBy: createdBy,
                    discountType: discountType,
                    totalPrice: totalPrice,
                    netTotal: netTotal,
                    totalTax: totalTax,
                    grandTotal: grandTotal,
                    supplierId: supplierId,
                    discount: discount,
                }),
                // Has Changes
                hasChanges = ko.computed(function () {
                    return dirtyFlag.isDirty();
                }),
                // Reset
                reset = function () {
                    dirtyFlag.reset();
                },
                convertToServerData = function () {
                    return {
                        PurchaseId: id(),
                        date_Purchase: purchaseDate() ? moment(purchaseDate()).format(ist.utcFormat) + 'Z' : undefined,
                        FlagId: flagId(),
                        RefNo: reffNo(),
                        Footnote: footnote(),
                        Comments: comments(),
                        Status: status(),
                        ContactId: contactId(),
                        SupplierContactAddressID: addressId(),
                        CreatedBy: createdBy(),
                        DiscountType: discountType(),
                        TotalPrice: totalPrice(),
                        NetTotal: netTotal(),
                        TotalTax: totalTax(),
                        GrandTotal: grandTotal(),
                        SupplierId: supplierId(),
                        Discount: discount(),
                        isproduct: isproduct(),
                    };
                };

            self = {
                id: id,
                code: code,
                purchaseDate: purchaseDate,
                flagId: flagId,
                reffNo: reffNo,
                footnote: footnote,
                comments: comments,
                status: status,
                contactId: contactId,
                storeId: storeId,
                addressId: addressId,
                createdBy: createdBy,
                discountType: discountType,
                totalPrice: totalPrice,
                taxRate: taxRate,
                netTotal: netTotal,
                totalTax: totalTax,
                grandTotal: grandTotal,
                isproduct: isproduct,
                supplierId: supplierId,
                discount: discount,
                companyName: companyName,
                setValidationSummary: setValidationSummary,
                purchaseDetails: purchaseDetails,
                convertToServerData: convertToServerData,
                errors: errors,
                isValid: isValid,
                showAllErrors: showAllErrors,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                reset: reset,
            };
            return self;
        },
        // #endregion __________________  Purchase  ______________________

        // #region __________________  Purchase Detail ______________________
        PurchaseDetail = function (specifiedPurchaseDetailId, specifiedItemId, specifiedquantity, specifiedprice, specifiedpackqty, specifiedItemCode,
            specifiedServiceDetail, specifiedTotalPrice, specifiedDiscount, specifiedNetTax, specifiedfreeitems, specifiedRefItemId, specifiedProductType,
            specifiedTaxValue) {
            var self,
                id = ko.observable(specifiedPurchaseDetailId),
                itemId = ko.observable(specifiedItemId),
                 quantity = ko.observable(specifiedquantity || 1),
                 price = ko.observable(specifiedprice || 0),
                 packqty = ko.observable(specifiedpackqty || 0),
                 itemCode = ko.observable(specifiedItemCode),
                 serviceDetail = ko.observable(specifiedServiceDetail),
                 taxValue = ko.observable(specifiedTaxValue || 0),
                 totalPrice = ko.computed(function () {
                     return quantity() * price();
                 }).extend({ numberInput: ist.numberFormat }),
                 discount = ko.observable(specifiedDiscount || 0).extend({ numberInput: ist.numberFormat }),
                 netTax = ko.computed(function () {
                     return (taxValue() / 100) * totalPrice();
                 }).extend({ numberInput: ist.numberFormat }),
                 freeitems = ko.observable(specifiedfreeitems || 0).extend({ number: true }),
                 refItemId = ko.observable(specifiedRefItemId),
                 productType = ko.observable(specifiedProductType),


                convertToServerData = function (source) {
                    return {
                        PurchaseDetailId: source.id(),
                        ItemId: source.itemId(),
                        quantity: source.quantity(),
                        price: source.price(),
                        packqty: source.packqty(),
                        pacItemCodekqty: source.itemCode(),
                        ServiceDetail: source.serviceDetail(),
                        TotalPrice: source.totalPrice(),
                        Discount: source.discount(),
                        NetTax: source.netTax(),
                        freeitems: source.freeitems(),
                        RefItemId: source.refItemId(),
                        ProductType: source.productType(),
                        TaxValue: source.taxValue(),
                    };
                };

            self = {
                id: id,
                itemId: itemId,
                quantity: quantity,
                price: price,
                packqty: packqty,
                itemCode: itemCode,
                taxValue: taxValue,
                serviceDetail: serviceDetail,
                totalPrice: totalPrice,
                discount: discount,
                netTax: netTax,
                freeitems: freeitems,
                refItemId: refItemId,
                productType: productType,
                convertToServerData: convertToServerData
            };
            return self;
        },

    // #endregion __________________  Purchase Detail  ______________________

        // #region __________________  Address ______________________
    // Address Entity
    Address = function (specifiedId, specifiedName, specifiedAddress1, specifiedAddress2, specifiedTelephone1, specifiedIsDefault) {
        // ReSharper restore InconsistentNaming
        return {
            id: specifiedId,
            name: specifiedName,
            address1: specifiedAddress1 || "",
            address2: specifiedAddress2 || "",
            telephone1: specifiedTelephone1 || "",
            isDefault: specifiedIsDefault
        };
    },
    // #endregion __________________  Address  ______________________

        // #region __________________  Company Contact ______________________
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
    // #endregion __________________  Compan yContact  ______________________

    // Purchase Factory
    Purchase.Create = function (source) {
        var deliveryNote = new Purchase(source.PurchaseId, source.Code, source.date_Purchase, source.SupplierId, source.ContactId, source.RefNo
            , source.SupplierContactAddressID, source.Status, source.FlagID, source.Comments, source.Footnote, source.CreatedBy, source.Discount,
            source.discountType, source.TotalPrice, source.NetTotal, source.TotalTax, source.GrandTotal, source.isproduct);

        _.each(source.PurchaseDetails, function (dNoteDetail) {
            deliveryNote.purchaseDetails.push(DeliveryNoteDetail.Create(dNoteDetail));
        });
        return deliveryNote;
    };


    // Purchase Detail factory
    PurchaseDetail.Create = function (source) {
        return new PurchaseDetail(source.PurchaseDetailId, source.ItemId, source.quantity, source.price, source.packqty, source.ItemCode, source.ServiceDetail, source.TotalPrice,
            source.Discount, source.NetTax, source.freeitems, source.RefItemId, source.ProductType, source.TaxValue);
    };

    // Purchase List View Factory
    PurchaseListView.Create = function (source) {
        return new PurchaseListView(source.PurchaseId, source.Code, source.DatePurchase, source.RefNo,
             source.SupplierName, source.FlagColor);
    };



    // Address Factory
    Address.Create = function (source) {
        return new Address(source.AddressId, source.AddressName, source.Address1, source.Address2, source.Tel1, source.IsDefaultAddress);
    };

    // Section Flag Factory
    SectionFlag.Create = function (source) {
        return new SectionFlag(source.SectionFlagId, source.FlagName, source.FlagColor);
    };

    // System User Factory
    SystemUser.Create = function (source) {
        return new SystemUser(source.SystemUserId, source.UserName, source.FullName);
    };

    // Company Contact Factory
    CompanyContact.Create = function (source) {
        return new CompanyContact(source.ContactId, source.Name, source.Email, source.IsDefaultContact);
    };

    return {
        PurchaseListView: PurchaseListView,
        Address: Address,
        CompanyContact: CompanyContact,
        Purchase: Purchase,
        SectionFlag: SectionFlag,
        SystemUser: SystemUser,
        PurchaseDetail: PurchaseDetail
    };
});