/*
    Module with the view model for the Compnay Contacts
*/
define("crm/contacts.viewModel",
    ["jquery", "amplify", "ko", "crm/contacts.dataservice", "crm/contacts.model", "common/confirmation.viewModel", "common/pagination", "common/sharedNavigation.viewModel"],
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

                // Selected Company
                selectedCompanyContact = ko.observable(),
                // Selected Role Id
                contactRoleId = ko.observable(true),
                states = ko.observableArray(),
                // Gets customers for list view
                getCompanyContacts = function() {
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
                            pager().totalCount(data.RowCount);
                            _.each(data.CompanyContacts, function (customer) {
                                var contactModel = new model.CompanyContact.Create(customer);
                                companyContactsForListView.push(contactModel);
                            });
                        }
                    },
                    error: function() {
                            toastr.error("Error: Failed To load Customers!");
                        }
                    });
                },
                // Search button handler
                searchButtonHandler = function () {
                    getCompanyContacts();
                },
                //  Reset button handler
                resetButtonHandler=function() {
                    searchFilter(null);
                    getCompanyContacts();
                },
                deleteContactbuttonHandler = function(contact) {
                    // Ask for confirmation
                    confirmation.afterProceed(function() {
                        dataservice.deleteContact({
                            CompanyContactId: contact.contactId(),
                        },
                        {
                            success: function(data) {
                                toastr.success("Contact successfuly deleted!");
                            },
                            error: function() {
                                toastr.error("Error: Failed To delete Contact!");
                            }
                        });
                    });
                    confirmation.show();
                },
                editContactbuttonHandler = function (contact) {
                    selectedBussinessAddressId(contact.addressId());
                    selectedShippingAddressId(contact.shippingAddressId());
                    selectedBussinessAddress("");
                    selectedShippingAddress("");
                    getContactDetail(contact);
                    view.showCompanyContactDetailDialog();
                },
                 // Gets Base Data
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
                                    var question = new model.Question.Create(quest);
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
                getContactDetail = function (contact) {
                    dataservice.getContactsDetail({ companyId: contact.companyId() },
                    {
                        success: function (data) {
                            if (data != null) {
                                // Address
                                bussinessAddresses.removeAll();
                                shippingAddresses.removeAll();
                                _.each(data.Addresses, function (item) {
                                    var address = new model.Address.Create(item);
                                    shippingAddresses.push(address);
                                    bussinessAddresses.push(address);
                                    if (item.AddressId == contact.addressId()) {
                                        selectedBussinessAddress(address);
                                        selectedShippingAddress(address);
                                    }
                                });
                                // State Setting for address
                                _.each(states(), function (state) {
                                    if (state.StateId == selectedBussinessAddress().stateId())
                                        selectedBussinessAddress().state(state.StateName);
                                });

                                // State Setting for shipping address
                                _.each(states(), function (state) {
                                    if (state.StateId == selectedShippingAddress().stateId())
                                        selectedShippingAddress().state(state.StateName);
                                });
                                // Territories
                                contactCompanyTerritoriesFilter.removeAll();
                                _.each(data.CompanyTerritories, function (terror) {
                                    var territory = new model.CompanyTerritory.Create(terror);
                                    contactCompanyTerritoriesFilter.push(territory);
                                });
                                selectedCompanyContact(contact);
                            }
                        },
                        error: function() {
                            toastr.error("Error: Failed To load Base data!");
                        }
                    });
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
                    pager:pager,
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
                    states: states
                };
            })()
        };
        return ist.contacts.viewModel;
    });
