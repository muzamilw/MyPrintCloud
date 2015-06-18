/*
    Module with the view model for the Calendar.
*/
define("calendar/calendar.viewModel",
    ["jquery", "amplify", "ko", "calendar/calendar.dataservice", "calendar/calendar.model", "common/companySelector.viewModel", "common/pagination", "crm/contacts.viewModel"],
    function ($, amplify, ko, dataservice, model, companySelector, pagination, contactVM) {
        var ist = window.ist || {};
        ist.calendar = {
            viewModel: (function () {
                var
                    //View
                    view,
                    //Active Calender Activity
                    selectedActivity = ko.observable(),
                    selectedSystemUser = ko.observable(),
                    isCustomerType = ko.observable("1"),
                    selectedActivityForRemove = ko.observable(),
                    selectedCompany = ko.observable(),
                    companySearchFilter = ko.observable(),
                    searchContactFilter = ko.observable(),
                    loggedInUserId = ko.observable(),
                    isBaseDataLoaded = ko.observable(false),
                    isBaseLoaded = ko.observable(false),
                    pager = ko.observable(),
                    sectionFlags = ko.observableArray([]),
                    companyContacts = ko.observableArray([]),
                    pipeLineProducts = ko.observableArray([]),
                    pipeLineSources = ko.observableArray([]),
                    systemUsers = ko.observableArray([]),
                    companies = ko.observableArray([]),
                    activityTypes = ko.observableArray([]),
                    items = ko.observableArray([]),
                    activities = [],

                fullCalendar = {
                    // Defines a view model class you can use to populate a calendar
                    viewModel: function (configuration) {
                        this.events = configuration.events;
                        this.header = configuration.header;
                        this.eventDropOrResize = configuration.eventDropOrResize;
                        this.editable = configuration.editable;
                        this.viewDate = configuration.viewDate || ko.observable(new Date());
                        this.defaultView = configuration.defaultView || ko.observable();
                        //this.droppable = configuration.droppable;
                        //this.dropAccept = configuration.dropAccept;
                    }
                },

                //Call on Drop or resize Activity
                eventDropOrResize = function (calenderActivity) {
                    var activity = model.Activity();
                    activity.id(calenderActivity.id);
                    activity.startDateTime(calenderActivity.start);
                    if (calenderActivity.end === null) {
                        activity.endDateTime(calenderActivity.start);
                    } else {
                        activity.endDateTime(calenderActivity.end);
                    }
                   
                    selectedActivity(activity);
                    saveActivityOnDropOrResize();
                },
                //Save For Drop Or resize Activity
                saveActivityOnDropOrResize = function () {
                    dataservice.saveActivityDropOrResize(selectedActivity().convertToServerData(), {
                        success: function (data) {
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
                }

                //Call click on activity for edit
                eventClick = function (activity) {
                    searchContactFilter(undefined);
                    selectedActivity(undefined);
                    isCustomerType("1");
                    selectedActivityForRemove(activity);
                    getActivityDetail(activity);
                    selectedActivity(model.Activity());
                    view.showCalendarActivityDialog();
                },
                //loadPage = ko.observable(false);
                loadPage = true;
                var start;
                var end;
                var lastView = ko.observable('month');
                viewDate = ko.observable(new Date()),
               viewEventClick = function (viewClick) {
                   if (start !== moment(viewClick.start) && end !== moment(viewClick.end) && (loadPage || lastView() !== viewClick.name)) {
                       start = moment(viewClick.start);
                       end = moment(viewClick.end);
                       lastView(viewClick.name);
                       if (!isBaseDataLoaded()) {
                           isBaseDataLoaded(true);
                           getBase(moment(viewClick.start).format(ist.utcFormat), moment(viewClick.end).format(ist.utcFormat));

                       } else {
                           getCalendarActivities(moment(viewClick.start).format(ist.utcFormat), moment(viewClick.end).format(ist.utcFormat));
                       }

                   }
                   loadPage = false;
               },
                //Get Activities For Next,Pre,Today click on
                getActivitiesForNextPreTodayClick = function (currentView) {
                    viewDate(currentView.start);
                    getCalendarActivities(moment(currentView.start).format(ist.utcFormat), moment(currentView.end).format(ist.utcFormat));
                },
                //Add new Activity
                newEventAdd = function (addNewActivityEvent) {
                    searchContactFilter(undefined);
                    selectedActivity(undefined);
                    isCustomerType("1");
                    var newAddActivity = model.Activity();
                    newAddActivity.startDateTime(addNewActivityEvent);
                    newAddActivity.endDateTime(addNewActivityEvent);
                    newAddActivity.isCustomerType("1");
                    newAddActivity.systemUserId(loggedInUserId());
                    //newAddActivity.systemUserId("7e20d462-c881-4d05-9e91-4c619385333b");
                    selectedActivity(newAddActivity);
                    view.showCalendarActivityDialog();
                },
                // Filter Calendar Activities change on user
                onChangeSystemUser = function () {
                    if (isBaseLoaded()) {
                        getCalendarActivities(moment(start).format(ist.utcFormat), moment(end).format(ist.utcFormat));
                    }
                   
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
                getBase = function (startDate, endDate) {
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
                            isBaseLoaded(true);
                            getCalendarActivities(startDate, endDate);
                        },
                        error: function () {
                            toastr.error("Failed to load base data.");
                        }
                    });
                },
                //
                mycCalender = new fullCalendar.viewModel({
                    events: items,
                    header: {
                        left: 'prev,next today',
                        center: 'title',
                        right: 'month,agendaWeek,agendaDay'
                    },
                    editable: true,
                    selectable: true,
                    selectHelper: true,
                    defaultView: lastView,
                    viewDate: viewDate,
                    eventDropOrResize: eventDropOrResize,
                }),

                //Set IS Customer Type
                getIsCustomerType = function () {
                    if (selectedActivity().isCustomerType() === "1") {
                        return [1];
                    }
                    else if (selectedActivity().isCustomerType() === "2") {
                        return [2];
                    }
                    else if (selectedActivity().isCustomerType() === "0") {
                        return [0];
                    }
                    return [1];
                },
                //Get Companies
                getCompanies = function () {
                    dataservice.getCompanyByCustomerType({
                        CustomerTypes: getIsCustomerType(),
                        SearchString: companySearchFilter(),
                        PageSize: pager().pageSize(),
                        PageNo: pager().currentPage(),
                    }, {
                        success: function (data) {
                            if (data != null) {
                                companies.removeAll();
                                _.each(data.Companies, function (item) {
                                    companies.push(model.Company.Create(item));
                                });
                                pager().totalCount(data.TotalCount);
                            }
                        },
                        error: function (response) {
                            toastr.error("Failed to load Contacts. Error: ");
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
                                    items.push({
                                        id: item.ActivityId,
                                        title: item.ActivityRef,
                                        start: item.ActivityStartTime,
                                        backgroundColor: sectionFlag != undefined ? sectionFlag.FlagColor + " !important" : null,
                                        end: item.ActivityEndTime,
                                        allDay: true,
                                        textColor:'black'
                                    });
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
                            if (data !== null && selectedSystemUser().toLowerCase() === selectedActivity().systemUserId().toLowerCase()) {
                                var activity = selectedActivity();

                                var sectionFlag = sectionFlags.find(function (sFlag) {
                                    return sFlag.SectionFlagId == activity.flagId();
                                });
                                if (selectedActivity().id() === undefined) {
                                    activity.id(data);

                                    items.push({
                                        id: activity.id(),
                                        title: activity.activityNotes(),
                                        backgroundColor: sectionFlag != undefined ? sectionFlag.FlagColor + " !important" : null,
                                        start: activity.startDateTime(),
                                        end: activity.endDateTime(),
                                        allDay: true,
                                        textColor: 'black'
                                    });
                                } else {
                                    items.remove(selectedActivityForRemove());
                                    items.push({
                                        id: activity.id(),
                                        title: activity.activityNotes(),
                                        backgroundColor: sectionFlag != undefined ? sectionFlag.FlagColor : null,
                                        start: activity.startDateTime(),
                                        end: activity.endDateTime(),
                                        allDay: true,
                                        textColor: 'black'
                                    });
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
                }
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
                                selectedActivity(model.Activity.Create(data));
                            }
                        },
                        error: function (response) {
                            toastr.error("Failed to load Detail . Error: ");
                        }
                    });
                },
                //Get Company Contact By CompanyId
                getCompanyContactByCompanyId = function (company) {
                    dataservice.getCompanyContactByCompanyId({
                        companyId: company.id,
                    }, {
                        success: function (data) {
                            if (data != null) {
                                //Company Contacts
                                companyContacts.removeAll();
                                ko.utils.arrayPushAll(companyContacts(), data);
                                companyContacts.valueHasMutated();

                            }
                        },
                        error: function (response) {
                            toastr.error("Failed to load Detail . Error: ");
                        }
                    });
                },
                 formatSelection = function (state) {
                     return "<span style=\"height:20px;width:25px;float:left;margin-right:10px;margin-top:5px;background-color:" + $(state.element).data("color") + "\"></span><span>" + state.text + "</span>";
                 },

                formatResult = function (state) {
                    return "<div style=\"height:20px;margin-right:10px;width:25px;float:left;background-color:" + $(state.element).data("color") + "\"></div><div>" + state.text + "</div>";
                },
                selected = ko.observable(),

                //Get Contact List
                getContactList = function () {
                    if (selectedActivity() !== undefined && selectedActivity().isCustomerActivity()) {
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
                //Change  Customer Type in Contact Dialog
                onClickCustomerTypeRadio = ko.computed(function () {
                    //if (selectedActivity() != undefined) {
                    //    // isCustomerType(radio.isCustomerType());
                    //   // getCompanyContactByName();
                    //}
                    // toastr.success("sss");
                }),
                // Work n Turn
                        isCustomerType.subscribe(function (value) {
                            if (selectedActivity() !== undefined) {
                                getCompanyContactByName();
                            }

                        }),
                closeContactDialog = function () {
                    view.hideContactSelectorDialog();
                },

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
                    pager(pagination.Pagination({ PageSize: 10 }, companyContacts, getCompanyContactByName));

                };

                return {
                    selectedActivity: selectedActivity,
                    selected: selected,
                    loggedInUserId: loggedInUserId,
                    pager: pager,
                    //items: items,
                    sectionFlags: sectionFlags,
                    companyContacts: companyContacts,
                    pipeLineProducts: pipeLineProducts,
                    pipeLineSources: pipeLineSources,
                    systemUsers: systemUsers,
                    companies: companies,
                    activityTypes: activityTypes,
                    //Utility Functiions
                    initialize: initialize,
                    mycCalender: mycCalender,
                    hideCompanyDialog: hideCompanyDialog,
                    onSaveActivity: onSaveActivity,
                    onDeleteActivity: onDeleteActivity,
                    activities: activities,
                    getActivitiesForNextPreTodayClick: getActivitiesForNextPreTodayClick,
                    formatSelection: formatSelection,
                    formatResult: formatResult,
                    selectedSystemUser: selectedSystemUser,
                    onChangeSystemUser: onChangeSystemUser,
                    searchContactFilter: searchContactFilter,
                    getContactList: getContactList,
                    onSelectContact: onSelectContact,
                    isCustomerType: isCustomerType,
                    onClickCustomerTypeRadio: onClickCustomerTypeRadio,
                    closeContactDialog: closeContactDialog,
                    addContact: addContact,
                    eventDropOrResize: eventDropOrResize
                };
            })()
        };
        return ist.calendar.viewModel;
    });

