/*
    Module with the view model for the To Do List.
*/
define("toDoList/toDoList.viewModel",
    ["jquery", "amplify", "ko", "calendar/calendar.dataservice", "calendar/calendar.model", "common/pagination", "common/companySelector.viewModel", "crm/contacts.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, companySelector, contactVM) {
        var ist = window.ist || {};
        ist.toDoList = {
            viewModel: (function () {
                var
                    //View
                    view,
                    //Active Calender Activity
                    selectedActivity = ko.observable(),
                    selectedActivityCopy = ko.observable(),
                    selectedSystemUser = ko.observable(),
                    isCustomerType = ko.observable("1"),
                    selectedActivityForRemove = ko.observable(),
                    companySearchFilter = ko.observable(),
                    searchContactFilter = ko.observable(),
                    loggedInUserId = ko.observable(),
                    isBaseDataLoaded = ko.observable(false),
                    pager = ko.observable(),
                    createdByUserName = ko.observable(),
                    sectionFlags = ko.observableArray([]),
                    companyContacts = ko.observableArray([]),
                    pipeLineProducts = ko.observableArray([]),
                    pipeLineSources = ko.observableArray([]),
                    systemUsers = ko.observableArray([]),
                    companies = ko.observableArray([]),
                    activityTypes = ko.observableArray([]),
                    items = ko.observableArray([]),
                    activities = [],
                dateDisplay = ko.observable(),
                date = new Date(),
                day = date.getDate(),
                year = date.getFullYear(),
                 month = date.getMonth(),
                montharray = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");

                //Call click on activity for edit
                onEditActivity = function (activity) {
                    searchContactFilter(undefined);
                    selectedActivity(undefined);
                    isCustomerType("1");
                    selectedActivityForRemove(activity);
                    getActivityDetail(activity);
                    selectedActivity(model.Activity());
                    //createdBy
                    //var Createdbysystemuser = systemUsers.filter(function (itemsys) { return itemsys.SystemUserId == selectedActivity().createdBy });
                    //if (Createdbysystemuser.length > 0) {
                    //    createdByUserName(Createdbysystemuser[0].FullName);
                    //}

                    view.showCalendarActivityDialog();

                },
                //Add new Activity
                addActivity = function () {
                    searchContactFilter(undefined);
                    selectedActivity(undefined);
                    isCustomerType("1");
                    var newAddActivity = model.Activity();
                    newAddActivity.startDateTime(date);
                    newAddActivity.endDateTime(date);
                    newAddActivity.isCustomerType("1");
                    newAddActivity.systemUserId(loggedInUserId());
                    newAddActivity.createdBy(loggedInUserId());
                    //newAddActivity.systemUserId("7e20d462-c881-4d05-9e91-4c619385333b");
                    selectedActivity(newAddActivity);
                    selectedSystemUser(loggedInUserId());
                    view.showCalendarActivityDialog();
                },
                // Filter Calendar Activities change on user
                onChangeSystemUser = function () {
                    var start = new Date(year, month, 1);
                    var end = new Date(year + (month == 12 ? 1 : 0), month + 1, 0);
                    getCalendarActivities(moment(start).format(ist.utcFormat), moment(end).format(ist.utcFormat));
                },
                //delete Activity
                onDeleteActivity = function (activity) {
                    dataservice.deleteActivity(selectedActivity().convertToServerData(), {
                        success: function () {
                            items.remove(selectedActivityForRemove());
                            view.hideCalendarActivityDialog();
                            toastr.success("Successfully remove.");
                        },
                        error: function () {
                            toastr.error("Failed to remove.");
                        }
                    });
                },
                // Get Base
                getBase = function () {
                    dataservice.getCalendarBase({
                        success: function (data) {

                            //Section Flags
                            sectionFlags.removeAll();
                            ko.utils.arrayPushAll(sectionFlags(), data.SectionFlags);
                            sectionFlags.valueHasMutated();
                            //Pipe Line Products
                            pipeLineProducts.removeAll();
                            ko.utils.arrayPushAll(pipeLineProducts(), data.PipeLineProducts);
                            pipeLineProducts.valueHasMutated();
                            //Pipe Line Sources
                            pipeLineSources.removeAll();
                            ko.utils.arrayPushAll(pipeLineSources(), data.PipeLineSources);
                            pipeLineSources.valueHasMutated();
                            //System Users
                            systemUsers.removeAll();
                            ko.utils.arrayPushAll(systemUsers(), data.SystemUsers);
                            systemUsers.valueHasMutated();
                            //Activity Types
                            activityTypes.removeAll();
                            ko.utils.arrayPushAll(activityTypes(), data.ActivityTypes);
                            activityTypes.valueHasMutated();

                            loggedInUserId(data.LoggedInUserId);

                            selectedSystemUser(data.LoggedInUserId);
                            getActivities();
                        },
                        error: function () {
                            toastr.error("Failed to load base data.");
                        }
                    });
                },
                //Get Activities
                getCalendarActivities = function (startDate, EndDate) {
                    dataservice.getActivies({
                        StartDateTime: startDate,
                        EndDateTime: EndDate,
                        UserId: selectedSystemUser()
                    }, {
                        success: function (data) {
                            items.removeAll();
                            if (data != null) {
                                _.each(data, function (item) {
                                    var sectionFlag = _.find(sectionFlags(), function (sFlag) {
                                        return sFlag.SectionFlagId == item.FlagId;
                                    });
                                    var newitem = model.ActivityList.Create(item);
                                    newitem.actionby(systemUsers.filter(function (itemsys) { return itemsys.SystemUserId == item.SystemUserId })[0].FullName);
                                    //systemUsers
                                    items.push(newitem);

                                });
                            }
                            // viewDate();
                        },
                        error: function (response) {

                            toastr.error("Failed to load Detail . Error: ");
                        }
                    });
                }
                //On Save Acivity
                onSaveActivity = function (activity) {
                    if (dobeforesave()) {
                        saveActivity();
                    }
                },
                //Save Acivity
                saveActivity = function () {
                    dataservice.saveActivity(selectedActivity().convertToServerData(), {
                        success: function (data) {
                            if (data !== null && selectedSystemUser() != undefined && selectedSystemUser().toLowerCase() != undefined && selectedSystemUser().toLowerCase() === selectedActivity().systemUserId().toLowerCase()) {

                                if (selectedActivity().id() === undefined) {
                                    selectedActivity().id(data);
                                    var activity = model.ActivityList();
                                    activity.id(selectedActivity().id());
                                    activity.activityNotes(selectedActivity().activityNotes());
                                    activity.startDateTime(selectedActivity().startDateTime() !== undefined ? moment(selectedActivity().startDateTime()).format(ist.dateTimePattern) : undefined);
                                    activity.endDateTime(selectedActivity().endDateTime() !== undefined ? moment(selectedActivity().endDateTime()).format(ist.dateTimePattern) : undefined);
                                    activity.actionby(systemUsers.filter(function (itemsys) { return itemsys.SystemUserId == selectedActivity().systemUserId() })[0].FullName);
                                    items.splice(0, 0, activity);
                                } else {
                                    selectedActivityForRemove().activityNotes(selectedActivity().activityNotes());
                                    selectedActivityForRemove().startDateTime(selectedActivity().startDateTime() !== undefined ? moment(selectedActivity().startDateTime()).format(ist.dateTimePattern) : undefined);
                                    selectedActivityForRemove().endDateTime(selectedActivity().endDateTime() !== undefined ? moment(selectedActivity().endDateTime()).format(ist.dateTimePattern) : undefined);
                                }
                            }
                            view.hideCalendarActivityDialog();
                            toastr.success("Successfully save.");
                        },
                        error: function (exceptionMessage, exceptionType) {

                            if (exceptionType === ist.exceptionType.MPCGeneralException) {

                                toastr.error(exceptionMessage);

                            } else {

                                toastr.error("Failed to save.");

                            }

                        }
                    });
                },
                //Do Before Save Logic
                dobeforesave = function () {
                    var flag = true;
                    if (!selectedActivity().isValid()) {
                        selectedActivity().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                //Get Activity Detail
                getActivityDetail = function (activity) {
                    dataservice.getActivityDetailById({
                        activityId: activity.id,
                    }, {
                        success: function (data) {
                            if (data != null) {
                                createdByUserName(undefined);
                                selectedActivity(model.Activity.Create(data));
                                var Createdbysystemuser = systemUsers.filter(function (itemsys) { return itemsys.SystemUserId == selectedActivity().createdBy() });
                                if (Createdbysystemuser.length > 0) {
                                    createdByUserName(Createdbysystemuser[0].FullName);
                                }
                            }
                        },
                        error: function (response) {
                            toastr.error("Failed to load Detail . Error: ");
                        }
                    });
                },
                //Get Contact List
                getContactList = function () {
                    if (selectedActivity() !== undefined && selectedActivity().isCustomerActivity()) {
                        // toastr.success("sss");
                        getCompanyContactByName();
                    }

                },
                //Get Company Contact By Name and customer Type
                getCompanyContactByName = function () {
                    dataservice.getCompanyContactByName({
                        CustomerType: isCustomerType(),
                        SearchFilter: searchContactFilter(),
                        PageSize: pager().pageSize(),
                        PageNo: pager().currentPage(),
                    }, {
                        success: function (data) {
                            if (data != null) {
                                //Company Contacts
                                companyContacts.removeAll();
                                _.each(data.CompanyContacts, function (item) {
                                    companyContacts.push(model.CompanyContact.Create(item));
                                });
                                pager().totalCount(data.RowCount);
                                view.showContactSelectorDialog();

                            }
                        },
                        error: function (response) {
                            toastr.error("Failed to load Detail . Error: ");
                        }
                    });
                },
                // On Select Contact
                onSelectContact = function (contact) {
                    view.hideContactSelectorDialog();
                    selectedActivity().companyName(contact.name() + " , " + contact.companyName());
                    selectedActivity().contactId(contact.id());
                },
                isCustomerType.subscribe(function (value) {
                    if (selectedActivity() !== undefined) {
                        getCompanyContactByName();
                    }

                }),
                closeContactDialog = function () {
                    view.hideContactSelectorDialog();
                },
                nextMonth = function () {
                    var nextM = parseInt(month) + 1;
                    date = new Date(year, nextM, day);
                    getActivities();
                },
                 previousMonth = function () {
                     var preM = parseInt(month) - 1;
                     date = new Date(year, preM, day);
                     getActivities();

                 },
                todayActivities = function () {
                    date = new Date();
                    getActivities();

                }
                getActivities = function () {
                    year = date.getFullYear();
                    month = date.getMonth();
                    dateDisplay(montharray[month] + " " + year);
                    var start = new Date(year, month, 1);
                    var end = new Date(year + (month == 12 ? 1 : 0), month + 1, 0);
                    getCalendarActivities(moment(start).format(ist.utcFormat), moment(end).format(ist.utcFormat));
                },
                    formatSelection = function (state) {
                        return "<span style=\"height:20px;width:25px;float:left;margin-right:10px;margin-top:5px;background-color:" + $(state.element).data("color") + "\"></span><span>" + state.text + "</span>";
                    },
                    formatResult = function (state) {
                        return "<div style=\"height:20px;margin-right:10px;width:25px;float:left;background-color:" + $(state.element).data("color") + "\"></div><div>" + state.text + "</div>";
                    },
                    selected = ko.observable(),
                   selectedCompany = ko.observable(),
                // Add Contact
                addContact = function () {
                    openCompanyDialog();
                },
                // Open Company Dialog
                 openCompanyDialog = function () {
                     companySelector.show(onSelectCompany, [0, 1, 2]);
                 },
                 onSaveContact = function (contact) {
                     selectedActivity().companyName(contact.firstName() + " , " + selectedCompany().name);
                     selectedActivity().contactId(contact.contactId());
                 },
                // On Select Company
                onSelectCompany = function (company) {
                    if (!company) {
                        return;
                    }
                    //selectedActivity().contactCompanyId(company.id);
                    selectedCompany(company);
                    contactVM.addContact(onSaveContact, company.id);
                },
                //Hide
                hideCompanyDialog = function () {
                    view.hideCompanyDialog();
                },
                //Initialize
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    getBase();
                    dateDisplay(montharray[month] + " " + year);
                    pager(pagination.Pagination({ PageSize: 10 }, companyContacts, getCompanyContactByName));
                };

                return {
                    selectedActivity: selectedActivity,
                    loggedInUserId: loggedInUserId,
                    pager: pager,
                    sectionFlags: sectionFlags,
                    companyContacts: companyContacts,
                    pipeLineProducts: pipeLineProducts,
                    pipeLineSources: pipeLineSources,
                    systemUsers: systemUsers,
                    companies: companies,
                    activityTypes: activityTypes,
                    //Utility Functiions
                    initialize: initialize,
                    onSaveActivity: onSaveActivity,
                    onDeleteActivity: onDeleteActivity,
                    activities: activities,
                    selectedSystemUser: selectedSystemUser,
                    onChangeSystemUser: onChangeSystemUser,
                    searchContactFilter: searchContactFilter,
                    getContactList: getContactList,
                    onSelectContact: onSelectContact,
                    isCustomerType: isCustomerType,
                    closeContactDialog: closeContactDialog,
                    items: items,
                    dateDisplay: dateDisplay,
                    nextMonth: nextMonth,
                    previousMonth: previousMonth,
                    todayActivities: todayActivities,
                    onEditActivity: onEditActivity,
                    formatResult: formatResult,
                    addActivity: addActivity,
                    formatSelection: formatSelection,
                    selected: selected,
                    addContact: addContact,
                    createdByUserName: createdByUserName
                };
            })()
        };
        return ist.toDoList.viewModel;
    });

