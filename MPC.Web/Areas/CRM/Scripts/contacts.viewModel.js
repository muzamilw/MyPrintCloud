/*
    Module with the view model for the Compnay Contacts
*/
define("crm/contacts.viewModel",
    ["jquery", "amplify", "ko", "crm/contacts.dataservice", "crm/crm.model", "common/confirmation.viewModel", "common/pagination", "common/sharedNavigation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, sharedNavigationVm) {
        var ist = window.ist || {};
        ist.contacts = {
            viewModel: (function () {
                var //View
                    view,
                    // Search filter 
                    searchFilter = ko.observable(),
                    // Customers array for list view
                    companyContactsForListView = ko.observableArray(),
                    // address list
                    bussinessAddresses = ko.observableArray(),
                    // address list
                    shippingAddresses = ko.observableArray(),
                    // company Territories array for list view
                    contactCompanyTerritoriesFilter = ko.observableArray(),
                    // Roles list
                    roles = ko.observableArray(),
                    // Registration questions
                    registrationQuestions = ko.observableArray(),
                    // Pager for pagging
                    pager = ko.observable(),
                    // Sort On
                    sortOn = ko.observable(1),
                    // selected Bussiness Address
                    selectedBussinessAddressId = ko.observable(),
                    // selected Shipping Address
                    selectedShippingAddressId = ko.observable(),
                    // Sort In Ascending
                    sortIsAsc = ko.observable(true),
                    selectedBussinessAddress = ko.observable(),
                    selectedShippingAddress = ko.observable(),
                    //Addresses to be used in store users shipping and billing address
                    allCompanyAddressesList = ko.observableArray([]),
                    // Selected Company
                    //selectedCompanyContact = ko.observable(),
                    companyContactEditorViewModel = new ist.ViewModel(model.CompanyContact),
                    selectedCompanyContact = companyContactEditorViewModel.itemForEditing,
                    // Selected Role Id
                    contactRoleId = ko.observable(true),
                    // list of state
                    states = ko.observableArray(),
                    // Gets customers for list view
                    getCompanyContacts = function () {
                        dataservice.getContactsForListView({
                            SearchFilter: searchFilter(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            SortBy: sortOn(),
                            IsAsc: sortIsAsc()
                        },
                        {
                            success: function (data) {
                                if (data != null) {
                                    companyContactsForListView.removeAll();
                                    _.each(data.CompanyContacts, function (customer) {
                                        var contactModel = new model.CompanyContact.Create(customer);
                                        companyContactsForListView.push(contactModel);
                                    });
                                    pager().totalCount(data.RowCount);
                                }
                            },
                            error: function () {
                                toastr.error("Error: Failed To load Customers!");
                            }
                        });
                    },
                    afterSaveForCalendarActivity = null,
                addContact = function (afterSaveCallback, companyId) {
                    afterSaveForCalendarActivity = afterSaveCallback;
                    selectedCompanyContact(model.CompanyContact());
                    selectedCompanyContact().companyId(companyId);
                    getContactDetail(selectedCompanyContact());
                    view.showCompanyContactDetailDialog();
                },
                // Search button handler
                searchButtonHandler = function (callback) {
                    pager().reset();
                    getCompanyContacts();
                },
                // Reset button handler
                resetButtonHandler = function () {
                    searchFilter(null);
                    getCompanyContacts();
                },
                // Delete Contact button Handler 
                deleteContactbuttonHandler = function (contact) {
                    // Ask for confirmation
                    confirmation.afterProceed(function () {
                        dataservice.deleteContact({
                            CompanyContactId: contact.contactId(),
                        },
                        {
                            success: function () {
                                companyContactsForListView.remove(contact);
                                //pager().totalCount(pager().totalCount() - 1);
                                toastr.success("Contact successfuly deleted!");
                            },
                            error: function () {
                                toastr.error("Error: Failed To delete Contact!");
                            }
                        });
                    });
                    confirmation.show();
                },
                // Edit Contact button Handler
                editContactbuttonHandler = function (contact) {
                    selectedBussinessAddressId(contact.addressId());
                    selectedShippingAddressId(contact.shippingAddressId());
                    //selectedBussinessAddress("");
                    //selectedShippingAddress("");
                    getContactDetail(contact);
                    view.showCompanyContactDetailDialog();
                },
                 // Get Base Data
                getBaseData = function () {
                    dataservice.getbaseData({},
                    {
                        success: function (data) {
                            if (data != null) {
                                // Roles
                                roles.removeAll();
                                _.each(data.CompanyContactRoles, function (compRole) {
                                    var role = new model.Role.Create(compRole);
                                    roles.push(role);
                                });
                                // Questions
                                registrationQuestions.removeAll();
                                _.each(data.RegistrationQuestions, function (quest) {
                                    var question = new model.RegistrationQuestion.Create(quest);
                                    registrationQuestions.push(question);
                                });
                                // States
                                states.removeAll();
                                ko.utils.arrayPushAll(states(), data.StateDropDowns);
                                states.valueHasMutated();
                            }
                        },
                        error: function () {
                            toastr.error("Error: Failed To load Base data!");
                        }
                    });
                },
                // Delete CompanyContact
                onDeleteCompanyContact = function (companyContact) { //CompanyContact
                    if (companyContact.isDefaultContact()) {
                        toastr.error("Default Contact Cannot be deleted", "", ist.toastrOptions);
                        return;
                    }
                    // Ask for confirmation
                    confirmation.afterProceed(function () {
                        //#region Db Saved Record Id > 0
                        if (companyContact.contactId() > 0) {

                            if (companyContact.companyId() > 0 && companyContact.contactId() > 0) {
                                dataservice.deleteCompanyContact({
                                    CompanyContactId: companyContact.contactId()
                                }, {
                                    success: function (data) {
                                        if (data) {
                                            var contact = companyContactsForListView.find(function (cnt) {
                                                return cnt.contactId() === companyContact.contactId();
                                            });
                                            if (contact) {
                                                companyContactsForListView.remove(contact);
                                                toastr.success("Deleted Successfully");
                                            }
                                           
                                        } else {
                                            toastr.error("Contact can not be deleted", "", ist.toastrOptions);
                                        }
                                    },
                                    error: function (response) {
                                        toastr.error("Error: Failed To Delete Company Contact " + response, "", ist.toastrOptions);
                                    }
                                });
                            }
                        }
                            //#endregion
                        else {
                            if (companyContact.contactId() < 0 || companyContact.contactId() == undefined) {

                                _.each(newCompanyContacts(), function (item) {
                                    if (item.contactId() == companyContact.contactId()) {
                                        newCompanyContacts.remove(companyContact);
                                    }
                                });
                                selectedStore().users.remove(companyContact);
                            }
                        }
                        view.hideCompanyContactDialog();

                    });
                    confirmation.show();

                },
                getContactDetail = function (contact) {
                    dataservice.getContactsDetail({ companyId: contact.companyId() },
                        {
                            success: function (data) {
                                if (data != null) {
                                    // Address
                                    bussinessAddresses.removeAll();
                                    shippingAddresses.removeAll();
                                    allCompanyAddressesList.removeAll();
                                    _.each(data.Addresses, function (item) {
                                        var address = new model.Address.Create(item);
                                        shippingAddresses.push(address);
                                        bussinessAddresses.push(address);
                                        allCompanyAddressesList.push(address);
                                        if (item.AddressId === contact.addressId()) {
                                            selectedBussinessAddress(address);
                                            selectedShippingAddress(address);
                                        }
                                    });
                                    //if (selectedBussinessAddress() != undefined && selectedBussinessAddress() !=="") {
                                    //    // State Setting for address
                                    //    _.each(states(), function(state) {
                                    //        if (state.StateId === selectedBussinessAddress().stateId())
                                    //            selectedBussinessAddress().state(state.StateName);
                                    //    });
                                    //}
                                    //if (selectedShippingAddress() != undefined && selectedShippingAddress() !=="") {
                                    //    // State Setting for shipping address
                                    //    _.each(states(), function(state) {
                                    //        if (state.StateId === selectedShippingAddress().stateId())
                                    //            selectedShippingAddress().state(state.StateName);
                                    //    });
                                    //}
                                    // Territories
                                    contactCompanyTerritoriesFilter.removeAll();
                                    _.each(data.CompanyTerritories, function (terror) {
                                        var territory = new model.CompanyTerritory.Create(terror);
                                        contactCompanyTerritoriesFilter.push(territory);
                                    });
                                    //selectedCompanyContact(contact);
                                    companyContactEditorViewModel.selectItem(contact);
                                    selectedCompanyContact().reset();
                                }
                            },
                            error: function () {
                                toastr.error("Error: Failed To load Base data!");
                            }
                        });
                },
                populateAddressesList = ko.computed(function () {
                    if (selectedCompanyContact() != undefined && selectedCompanyContact().territoryId() != undefined) {
                        shippingAddresses.removeAll();
                        bussinessAddresses.removeAll();
                        _.each(allCompanyAddressesList(), function (item) {

                            if (item.territoryId() == selectedCompanyContact().territoryId()) {
                                shippingAddresses.push(item);
                                bussinessAddresses.push(item);
                            }
                        });
                    }
                }),
                selectBussinessAddress = ko.computed(function () {
                    if (selectedCompanyContact() != undefined && selectedCompanyContact().addressId() != undefined) {
                    }
                    //if (selectedBussinessAddressId() != undefined) {
                    if (selectedCompanyContact() != undefined && selectedCompanyContact().bussinessAddressId() != undefined) {
                        _.each(allCompanyAddressesList(), function (item) {
                            if (item.addressId() == selectedCompanyContact().bussinessAddressId()) {
                                selectedBussinessAddress(item);
                                if (item.city() == null) {
                                    selectedBussinessAddress().city(undefined);
                                }
                                if (item.state() == null) {
                                    selectedBussinessAddress().state(undefined);
                                }
                                if (selectedCompanyContact() != undefined) {
                                    selectedCompanyContact().bussinessAddressId(item.addressId());
                                    selectedCompanyContact().addressId(item.addressId());
                                    selectedBussinessAddress().stateName(item.stateName());
                                }
                            }
                        });
                    }
                    if (selectedCompanyContact() != undefined && selectedCompanyContact().bussinessAddressId() == undefined) {
                        selectedBussinessAddress(undefined);
                    }
                }),
                selectShippingAddress = ko.computed(function () {
                    //if (selectedShippingAddressId() != undefined) {
                    if (selectedCompanyContact() != undefined && selectedCompanyContact().shippingAddressId() != undefined) {
                        _.each(allCompanyAddressesList(), function (item) {
                            if (item.addressId() == selectedCompanyContact().shippingAddressId()) {
                                selectedShippingAddress(item);
                                if (item.city() == null) {
                                    selectedShippingAddress().city(undefined);
                                }
                                if (item.state() == null) {
                                    selectedShippingAddress().state(undefined);
                                }
                                if (selectedCompanyContact() != undefined) {
                                    selectedCompanyContact().shippingAddressId(item.addressId());
                                    selectedShippingAddress().stateName(item.stateName());
                                }
                            }
                        });
                    }
                    if (selectedCompanyContact() != undefined && selectedCompanyContact().shippingAddressId() == undefined) {
                        selectedShippingAddress(undefined);
                    }
                }),
                // State Setting for address
                stateSettingForBusinessAddress = function () {
                    if (!selectedBussinessAddress()) {
                        return;
                    }
                    _.each(states(), function (state) {
                        if (state.StateId === selectedBussinessAddress().stateId())
                            selectedBussinessAddress().state(state.StateName);
                    });
                },
                // State Setting for Shipping address
                stateSettingForShippingAddress = function () {
                    if (!selectedShippingAddress()) {
                        return;
                    }
                    _.each(states(), function (state) {
                        if (state.StateId === selectedShippingAddress().stateId())
                            selectedShippingAddress().state(state.StateName);
                    });
                },
                // Contact save buttoin handler
                 onSaveCompanyContact = function () {
                     if (doBeforeSaveCompanyContact()) {
                         dataservice.saveCompanyContact(
                             selectedCompanyContact().convertToServerData(),
                             {
                                 success: function (data) {
                                     if (data) {
                                         toastr.success("Saved Successfully");
                                         selectedCompanyContact().contactId(data.ContactId);
                                         var savedCompanyContact = model.CompanyContact.Create(data);
                                         var count = 0;
                                         _.each(companyContactsForListView(), function (user) {
                                             if (user.contactId() == savedCompanyContact.contactId()) {
                                                 user.firstName(savedCompanyContact.firstName());
                                                 user.email(savedCompanyContact.email());
                                                 user.image(savedCompanyContact.image());
                                             }
                                             count = count + 1;
                                         });
                                         if (afterSaveForCalendarActivity && typeof afterSaveForCalendarActivity === "function") {
                                             afterSaveForCalendarActivity(selectedCompanyContact());
                                             view.hideCompanyContactDialog();
                                         } else {
                                             onCloseCompanyContact();
                                         }

                                     }
                                 },
                                 error: function (response) {
                                     toastr.error("Error: Failed To Save Contact " + response);
                                     onCloseCompanyContact();
                                 }
                             });
                     }
                 },
                // ReSharper disable once InconsistentNaming
                 UserProfileImageFileLoadedCallback = function (file, data) {
                     selectedCompanyContact().image(data);
                     selectedCompanyContact().fileName(file.name);
                 },
                // Close contact button handerl
                 onCloseCompanyContact = function () {
                     selectedCompanyContact(undefined);
                     selectedBussinessAddressId(undefined);
                     view.hideCompanyContactDialog();
                 },
                // Do Before Save CompanyContact
                doBeforeSaveCompanyContact = function () {
                    var flag = true;
                    if (!selectedCompanyContact().isValid()) {
                        selectedCompanyContact().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                //Initialize
               initializeForCalendar = function (specifiedView) {
                   view = specifiedView;
                   ko.applyBindings(view.viewModel, view.contactProfileBindingRoot);
                   getBaseData();
               },
                //Initialize
               initialize = function (specifiedView) {
                   view = specifiedView;
                   ko.applyBindings(view.viewModel, view.bindingRoot);
                   pager(new pagination.Pagination({ PageSize: 5 }, companyContactsForListView, getCompanyContacts));
                   getBaseData();
                   getCompanyContacts();
               };
                return {
                    initialize: initialize,
                    onSaveCompanyContact: onSaveCompanyContact,
                    onCloseCompanyContact: onCloseCompanyContact,
                    pager: pager,
                    searchFilter: searchFilter,
                    companyContactsForListView: companyContactsForListView,
                    searchButtonHandler: searchButtonHandler,
                    resetButtonHandler: resetButtonHandler,
                    sharedNavigationVm: sharedNavigationVm,
                    deleteContactbuttonHandler: deleteContactbuttonHandler,
                    editContactbuttonHandler: editContactbuttonHandler,
                    bussinessAddresses: bussinessAddresses,
                    contactCompanyTerritoriesFilter: contactCompanyTerritoriesFilter,
                    roles: roles,
                    registrationQuestions: registrationQuestions,
                    contactRoleId: contactRoleId,
                    selectedCompanyContact: selectedCompanyContact,
                    selectedBussinessAddressId: selectedBussinessAddressId,
                    selectedShippingAddressId: selectedShippingAddressId,
                    selectedBussinessAddress: selectedBussinessAddress,
                    selectedShippingAddress: selectedShippingAddress,
                    shippingAddresses: shippingAddresses,
                    states: states,
                    UserProfileImageFileLoadedCallback: UserProfileImageFileLoadedCallback,
                    allCompanyAddressesList: allCompanyAddressesList,
                    onDeleteCompanyContact: onDeleteCompanyContact,
                    addContact: addContact,
                    initializeForCalendar: initializeForCalendar
                };
            })()
        };
        return ist.contacts.viewModel;
    });
