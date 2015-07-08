/*
    Module with the model for the  Delivery Notes
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

        // #region __________________  Purchase List View   ______________________
        PurchaseListView = function (specifiedPurchaseId, specifiedCode, specifiedPurchaseDate, specifiedRefNo,
            specifiedCompanyName, specifiedFlagColor, specifiedStatus) {

            var self,
                id = ko.observable(specifiedPurchaseId),
                code = ko.observable(specifiedCode),
                purchaseOrderDate = ko.observable(specifiedPurchaseDate !== null ? moment(specifiedPurchaseDate).toDate() : undefined),
                companyName = ko.observable(specifiedCompanyName),
                refNo = ko.observable(specifiedRefNo),
                flagColor = ko.observable(specifiedFlagColor),
                status = ko.observable(specifiedStatus),
                statusName = ko.computed(function () {
                    if (status() === 31) {
                        return "Open";
                    }
                    else if (status() === 32) {
                        return "Posted";
                    }
                    else if (status() === 33) {
                        return "Cancelled";
                    }
                });

            return {
                id: id,
                code: code,
                purchaseOrderDate: purchaseOrderDate,
                flagColor: flagColor,
                companyName: companyName,
                refNo: refNo,
                status: status,
                statusName: statusName,
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
            spcCreatedBy, spcDiscount, spcdiscountType, spcTotalPrice, spcNetTotal, spcTotalTax, spcGrandTotal, spcisproduct, spcSupplierContactCompany) {

            var self,
                id = ko.observable(specifiedPurchaseId),
                code = ko.observable(specifiedcode),
                purchaseDate = ko.observable(specifieddate_Purchase !== null ? moment(specifieddate_Purchase).toDate() : undefined),
                flagId = ko.observable(specifiedflagId).extend({ required: true }),
                reffNo = ko.observable(specifiedRefNo),
                footnote = ko.observable(spcFootnote),
                comments = ko.observable(spcComments),
                status = ko.observable(spcStatus),
                 statusName = ko.computed(function () {
                     if (status() === 31) {
                         return "Open";
                     }
                     else if (status() === 32) {
                         return "Posted";
                     }
                     else if (status() === 33) {
                         return "Cancelled";
                     }
                 }),
                contactId = ko.observable(spcContactId).extend({ required: true }),
                supplierContactCompany = ko.observable(spcSupplierContactCompany),
                // Store Id
                storeId = ko.observable(undefined),
                addressId = ko.observable(spcSupplierContactAddressID).extend({ required: true }),
                createdBy = ko.observable(spcCreatedBy),
                discountType = ko.observable(spcdiscountType),
                totalPrice = ko.observable(spcTotalPrice || 0).extend({ numberInput: ist.numberFormat }),
                netTotal = ko.observable(spcNetTotal || 0).extend({ numberInput: ist.numberFormat }),
                isproduct = ko.observable(spcisproduct),
                totalTax = ko.observable(spcTotalTax || 0).extend({ numberInput: ist.numberFormat }),
                grandTotal = ko.observable(spcGrandTotal || 0).extend({ numberInput: ist.numberFormat }),
                supplierId = ko.observable(spcSupplierId).extend({ required: true }),
                //supplierTelNo = ko.observable(spcSupplierTelNo),
                discount = ko.observable(0, 0),
                companyName = ko.observable(undefined),
                taxRate = ko.observable(0),
                purchaseDetails = ko.observableArray([]),
                // Set Validation Summary
                setValidationSummary = function (validationSummaryList) {
                    validationSummaryList.removeAll();
                    if (supplierId.error) {
                        validationSummaryList.push({ name: "Supplier", element: supplierId.domElement });
                    }
                    if (contactId.error) {
                        validationSummaryList.push({ name: "Contact", element: contactId.domElement });
                    }
                    if (addressId.error) {
                        validationSummaryList.push({ name: "Address", element: addressId.domElement });
                    }
                    if (flagId.error) {
                        validationSummaryList.push({ name: "Flag", element: flagId.domElement });
                    }
                },
                // Errors
                errors = ko.validation.group({
                    supplierId: supplierId,
                    flagId: flagId,
                    addressId: addressId,
                    contactId:contactId
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
                        SupplierContactCompany: supplierContactCompany(),
                        PurchaseDetails: []
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
                statusName:statusName,
                supplierContactCompany: supplierContactCompany,
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


    // #region __________________  Goods Received Note  ______________________
    GoodsReceivedNote = function (specifiedGoodsReceivedId, specifiedPurchaseId, specifiedcode, specifieddate_Received, spcSupplierId, spcContactId,
            specifiedRefNo, spcStatus, specifiedflagId, spcComments, spcFootnote,
            spcCreatedBy, spcDiscount, spcdiscountType, spcTotalPrice, spcNetTotal, spcTotalTax, spcGrandTotal, spcisproduct, spcTel1, spcDeliveryDate, spcReference1,
            spcReference2, spcCarrierId) {

        var self,
            id = ko.observable(specifiedGoodsReceivedId),
            purchaseId = ko.observable(specifiedPurchaseId),
            code = ko.observable(specifiedcode),
            receiveDate = ko.observable(specifieddate_Received !== null ? moment(specifieddate_Received).toDate() : undefined),
            flagId = ko.observable(specifiedflagId).extend({ required: true }),
            reffNo = ko.observable(specifiedRefNo),
            footnote = ko.observable(spcFootnote),
            comments = ko.observable(spcComments),
            status = ko.observable(spcStatus),
            contactId = ko.observable(spcContactId).extend({ required: true }),
            // Store Id
            storeId = ko.observable(undefined),
            createdBy = ko.observable(spcCreatedBy),
            discountType = ko.observable(spcdiscountType),
            totalPrice = ko.observable(spcTotalPrice || 0).extend({ numberInput: ist.numberFormat }),
            netTotal = ko.observable(spcNetTotal || 0).extend({ numberInput: ist.numberFormat }),
            isproduct = ko.observable(spcisproduct),
            totalTax = ko.observable(spcTotalTax || 0).extend({ numberInput: ist.numberFormat }),
            grandTotal = ko.observable(spcGrandTotal || 0).extend({ numberInput: ist.numberFormat }),
            supplierId = ko.observable(spcSupplierId).extend({ required: true }),
            //supplierTelNo = ko.observable(spcSupplierTelNo),
            discount = ko.observable(spcDiscount || 0),
            companyName = ko.observable(undefined),
            taxRate = ko.observable(0),
            tel1 = ko.observable(spcTel1),
             deliveryDate = ko.observable(spcDeliveryDate),
             reference1 = ko.observable(spcReference1),
             reference2 = ko.observable(spcReference2),
             carrierId = ko.observable(spcCarrierId),

            goodsReceivedNoteDetails = ko.observableArray([]),
            // Set Validation Summary
            setValidationSummary = function (validationSummaryList) {
                validationSummaryList.removeAll();
                if (supplierId.error) {
                    validationSummaryList.push({ name: "Supplier", element: supplierId.domElement });
                }
                if (contactId.error) {
                    validationSummaryList.push({ name: "Contact", element: contactId.domElement });
                }
                if (flagId.error) {
                    validationSummaryList.push({ name: "Flag", element: flagId.domElement });
                }

            },
            // Errors
            errors = ko.validation.group({
                supplierId: supplierId,
                flagId: flagId,
                contactId: contactId,
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
                receiveDate: receiveDate,
                flagId: flagId,
                reffNo: reffNo,
                footnote: footnote,
                comments: comments,
                status: status,
                contactId: contactId,
                isproduct: isproduct,
                storeId: storeId,
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
                    GoodsReceivedId: id(),
                    PurchaseId: purchaseId(),
                    date_Received: receiveDate() ? moment(receiveDate()).format(ist.utcFormat) + 'Z' : null,
                    FlagId: flagId(),
                    RefNo: reffNo(),
                    UserNotes: footnote(),
                    Comments: comments(),
                    Status: status(),
                    ContactId: contactId(),
                    CreatedBy: createdBy(),
                    discountType: discountType(),
                    TotalPrice: totalPrice(),
                    NetTotal: netTotal(),
                    TotalTax: totalTax(),
                    grandTotal: grandTotal(),
                    SupplierId: supplierId(),
                    Discount: discount(),
                    isproduct: isproduct(),
                    Tel1: tel1(),
                    DeliveryDate: deliveryDate() ? moment(deliveryDate()).format(ist.utcFormat) + 'Z' : null,
                    Reference1: reference1(),
                    Reference2: reference2(),
                    CarrierId: carrierId(),
                    GoodsReceivedNoteDetails: []
                };
            };

        self = {
            id: id,
            purchaseId: purchaseId,
            code: code,
            receiveDate: receiveDate,
            flagId: flagId,
            reffNo: reffNo,
            footnote: footnote,
            comments: comments,
            status: status,
            contactId: contactId,
            storeId: storeId,
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
            tel1: tel1,
            deliveryDate: deliveryDate,
            reference1: reference1,
            reference2: reference2,
            carrierId: carrierId,
            setValidationSummary: setValidationSummary,
            goodsReceivedNoteDetails: goodsReceivedNoteDetails,
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
             quantity = ko.observable(specifiedquantity || 1).extend({ number: true }),
             price = ko.observable(specifiedprice || 0).extend({ number: true }),
             packqty = ko.observable(specifiedpackqty || 0).extend({ number: true }),
             itemCode = ko.observable(specifiedItemCode),
             serviceDetail = ko.observable(specifiedServiceDetail),
             taxValue = ko.observable(0),
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

               // Errors
                errors = ko.validation.group({
                    quantity: quantity,
                    price: price,
                    packqty: packqty,
                    freeitems: freeitems,
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
            convertToServerData = function (source) {
                return {
                    PurchaseDetailId: source.id() < 0 ? 0 : source.id(),
                    quantity: source.quantity(),
                    price: source.price(),
                    packqty: source.packqty(),
                    ItemCode: source.itemCode(),
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
            errors: errors,
            isValid: isValid,
            showAllErrors: showAllErrors,
            freeitems: freeitems,
            refItemId: refItemId,
            productType: productType,
            convertToServerData: convertToServerData
        };
        return self;
    },

    // #endregion __________________  Purchase Detail  ______________________

    // #region __________________  Goods Received Note Detail ______________________
    GoodsReceivedNoteDetail = function (specifiedGoodsReceivedDetailId, specifiedquantity, specifiedprice, specifiedpackqty, specifiedItemCode,
        specifiedServiceDetail, specifiedTotalPrice, specifiedDiscount, specifiedNetTax, specifiedfreeitems, specifiedRefItemId, specifiedProductType,
        specifiedTaxValue, specifiedQtyReceived) {
        var self,
            id = ko.observable(specifiedGoodsReceivedDetailId),
             quantity = ko.observable(specifiedquantity || 1).extend({ number: true }),
             price = ko.observable(specifiedprice || 0).extend({ number: true }),
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
             qtyReceived = ko.observable(specifiedQtyReceived || 1).extend({ number: true }),
               // Errors
                errors = ko.validation.group({
                    quantity: quantity,
                    price: price,
                    freeitems: freeitems,
                    qtyReceived: qtyReceived,
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

            convertToServerData = function (source) {
                return {
                    GoodsReceivedDetailId: source.id() < 0 ? 0 : source.id(),
                    TotalOrderedqty: source.quantity(),
                    Price: source.price(),
                    PackQty: source.packqty(),
                    ItemCode: source.itemCode(),
                    Details: source.serviceDetail(),
                    TotalPrice: source.totalPrice(),
                    Discount: source.discount(),
                    NetTax: source.netTax(),
                    FreeItems: source.freeitems(),
                    RefItemId: source.refItemId(),
                    ProductType: source.productType(),
                    TaxValue: source.taxValue(),
                    QtyReceived: source.qtyReceived(),
                };
            };

        self = {
            id: id,
            quantity: quantity,
            price: price,
            packqty: packqty,
            itemCode: itemCode,
            taxValue: taxValue,
            serviceDetail: serviceDetail,
            totalPrice: totalPrice,
            discount: discount,
            qtyReceived: qtyReceived,
            netTax: netTax,
            freeitems: freeitems,
            refItemId: refItemId,
            productType: productType,
            errors: errors,
            isValid: isValid,
            showAllErrors: showAllErrors,
            convertToServerData: convertToServerData
        };
        return self;
    },

    // #endregion __________________ Goods Received Note Detail  ______________________

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
        var purchase = new Purchase(source.PurchaseId, source.Code, source.date_Purchase, source.SupplierId, source.ContactId, source.RefNo
            , source.SupplierContactAddressID, source.Status, source.FlagID, source.Comments, source.FootNote, source.CreatedBy, source.Discount,
            source.discountType, source.TotalPrice, source.NetTotal, source.TotalTax, source.GrandTotal, source.isproduct, source.SupplierContactCompany);

        _.each(source.PurchaseDetails, function (purchaseDet) {
            purchase.purchaseDetails.push(PurchaseDetail.Create(purchaseDet));
        });
        return purchase;
    };


    // Purchase Detail factory
    PurchaseDetail.Create = function (source) {
        return new PurchaseDetail(source.PurchaseDetailId, source.ItemId, source.quantity, source.price, source.packqty, source.ItemCode, source.ServiceDetail, source.TotalPrice,
            source.Discount, source.NetTax, source.freeitems, source.RefItemId, source.ProductType, source.TaxValue);
    };

    // Purchase List View Factory
    PurchaseListView.Create = function (source) {
        return new PurchaseListView(source.PurchaseId, source.Code, source.DatePurchase, source.RefNo,
             source.SupplierName, source.FlagColor, source.Status);
    };
    // Goods Received Note Factory
    GoodsReceivedNote.Create = function (source) {
        var grn = new GoodsReceivedNote(source.GoodsReceivedId, source.PurchaseId, source.code, source.date_Received, source.SupplierId, source.ContactId, source.RefNo, source.Status,
             source.FlagId, source.Comments, source.UserNotes, source.CreatedBy, source.Discount, source.discountType, source.TotalPrice, source.NetTotal, source.TotalTax,
             source.grandTotal, source.isProduct, source.Tel1, source.DeliveryDate, source.Reference1, source.Reference2, source.CarrierId);
        _.each(source.GoodsReceivedNoteDetails, function (grnDet) {
            grn.goodsReceivedNoteDetails.push(GoodsReceivedNoteDetail.Create(grnDet));
        });
        return grn;
    }

    // Goods Received Note Detail factory
    GoodsReceivedNoteDetail.Create = function (source) {
        return new GoodsReceivedNoteDetail(source.GoodsReceivedDetailId, source.TotalOrderedqty, source.Price, source.PackQty, source.ItemCode, source.Details, source.TotalPrice,
            source.Discount, source.NetTax, source.FreeItems, source.RefItemId, source.ProductType, source.TaxValue, source.QtyReceived);
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
        PurchaseDetail: PurchaseDetail,
        GoodsReceivedNote: GoodsReceivedNote,
        GoodsReceivedNoteDetail: GoodsReceivedNoteDetail
    };
});