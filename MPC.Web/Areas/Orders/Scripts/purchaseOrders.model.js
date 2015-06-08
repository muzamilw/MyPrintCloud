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
                flagId: flagId,
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
            spcCreatedBy, spcDiscount, spcdiscountType, spcTotalPrice, spcNetTotal, spcTotalTax, spcGrandTotal) {

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
                totalTax = ko.observable(spcTotalTax),
                grandTotal = ko.observable(spcGrandTotal),
                supplierId = ko.observable(spcSupplierId).extend({ required: true }),
                //supplierTelNo = ko.observable(spcSupplierTelNo),
                discount = ko.observable(spcDiscount),
                companyName = ko.observable(undefined),
                deliveryNoteDetails = ko.observableArray([]),
                // Set Validation Summary
                setValidationSummary = function (validationSummaryList) {
                    validationSummaryList.removeAll();
                    if (companyId.error) {
                        validationSummaryList.push({ name: "Customer", element: companyId.domElement });
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
                        purchaseDate: purchaseDate(),
                        flagId: flagId(),
                        reffNo: reffNo(),
                        footnote: footnote(),
                        comments: comments(),
                        status: status(),
                        contactId: contactId(),
                        addressId: addressId(),
                        createdBy: createdBy(),
                        discountType: discountType(),
                        totalPrice: totalPrice(),
                        netTotal: netTotal(),
                        totalTax: totalTax(),
                        grandTotal: grandTotal(),
                        supplierId: supplierId(),
                        discount: discount(),
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
                netTotal: netTotal,
                totalTax: totalTax,
                grandTotal: grandTotal,
                supplierId: supplierId,
                discount: discount,
                companyName: companyName,
                setValidationSummary: setValidationSummary,
                deliveryNoteDetails: deliveryNoteDetails,
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
        // #endregion __________________  Delivery Note  ______________________

        // #region __________________  Delivery Note Detail ______________________
        DeliveryNoteDetail = function (specifiedId, specifiedDescription) {
            var self,
                id = ko.observable(specifiedId),
                description = ko.observable(specifiedDescription),

                convertToServerData = function (source) {
                    return {
                        DeliveryDetailid: source.id(),
                        Description: source.description(),
                    };
                };

            self = {
                id: id,
                description: description,
                convertToServerData: convertToServerData
            };
            return self;
        },

    // #endregion __________________  Delivery Note Detail  ______________________

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
            source.discountType, source.TotalPrice, source.NetTotal, source.TotalTax, source.GrandTotal);

        _.each(source.DeliveryNoteDetails, function (dNoteDetail) {
            deliveryNote.deliveryNoteDetails.push(DeliveryNoteDetail.Create(dNoteDetail));
        });
        return deliveryNote;
    };


    // Delivery Notes Detail factory
    DeliveryNoteDetail.Create = function (source) {
        return new DeliveryNoteDetail(source.DeliveryDetailid, source.Description);
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
        DeliveryNoteDetail: DeliveryNoteDetail
    };
});