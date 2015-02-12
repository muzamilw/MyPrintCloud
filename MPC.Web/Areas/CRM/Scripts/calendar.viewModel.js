/*
    Module with the view model for the Calendar.
*/
define("calendar/calendar.viewModel",
    ["jquery", "amplify", "ko", "calendar/calendar.dataservice", "calendar/calendar.model", "common/pagination"],
    function ($, amplify, ko, dataservice, model, pagination) {
        var ist = window.ist || {};
        ist.calendar = {
            viewModel: (function () {
                var
                    //View
                    view,
                    //Active Calender Activity
                    selectedActivity = ko.observable(),
                    selectedActivityForRemove = ko.observable(),
                    selectedCompany = ko.observable(),
                    companySearchFilter = ko.observable(),
                    loggedInUserId = ko.observable(),
                    pager = ko.observable(),
                   sectionFlags = ko.observableArray([]),
                companyContacts = ko.observableArray([]),
                pipeLineProducts = ko.observableArray([]),
                pipeLineSources = ko.observableArray([]),
                systemUsers = ko.observableArray([]),
                companies = ko.observableArray([]),
                activityTypes = ko.observableArray([]),
                items = ko.observableArray([]),

                fullCalendar = {
                    // Defines a view model class you can use to populate a calendar
                    viewModel: function (configuration) {
                        this.events = configuration.events;
                        this.header = configuration.header;
                        this.editable = configuration.editable;
                        this.viewDate = configuration.viewDate || ko.observable(new Date());
                        //this.droppable = configuration.droppable;
                        //this.dropAccept = configuration.dropAccept;
                    }
                };

                //Call on Drop or resize Activity
                eventDropOrResize = function (calenderActivity) {
                    var activity = model.Activity();
                    activity.id(calenderActivity.id);
                    activity.startDateTime(calenderActivity.start);
                    activity.endDateTime(calenderActivity.end);
                    selectedActivity(activity);
                    saveActivityOnDropOrResize();
                },
                 saveActivityOnDropOrResize = function () {
                     dataservice.saveActivityDropOrResize(selectedActivity().convertToServerData(), {
                         success: function (data) {
                             toastr.success("Successfully save.");
                         },
                         error: function (exceptionMessage, exceptionType) {

                             if (exceptionType === ist.exceptionType.CaresGeneralException) {

                                 toastr.error(exceptionMessage);

                             } else {

                                 toastr.error("Failed to save.");

                             }

                         }
                     });
                 }

                //Call click on activity for edit
                eventClick = function (activity) {
                    selectedActivityForRemove(activity);
                    getActivityDetail(activity);
                    selectedActivity(model.Activity());
                    view.showCalendarActivityDialog();
                },
                //Add new Activity
                newEventAdd = function (addNewActivityEvent) {
                    var newAddActivity = model.Activity();
                    newAddActivity.startDateTime(addNewActivityEvent);
                    newAddActivity.isCustomerType("1");
                    //newAddActivity.systemUserId(loggedInUserId());
                    newAddActivity.systemUserId("7e20d462-c881-4d05-9e91-4c619385333b");
                    selectedActivity(newAddActivity);
                    view.showCalendarActivityDialog();
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

                            _.each(data.Activities, function (item) {
                                items.push({
                                    id: item.ActivityId,
                                    title: item.ActivityRef,
                                    start: item.ActivityStartTime,
                                    end: item.ActivityEndTime,
                                });
                            });

                        },
                        error: function () {
                            toastr.error("Failed to load base data.");
                        }
                    });
                },

                // viewDate = ko.observable(Date.now()),
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
                }),
                //Show
                showCompanyDialog = function () {
                    companies.removeAll();
                    view.showCompanyDialog();
                    getCompanies();
                }

                getIsCustomerType = function () {
                    if (selectedActivity().isCustomerType() === "1") {
                        return 1;
                    }
                    else if (selectedActivity().isCustomerType() === "2") {
                        return 2;
                    }
                    else if (selectedActivity().isCustomerType() === "0") {
                        return 0;
                    }
                },
                //Hide
                hideCompanyDialog = function () {
                    view.hideCompanyDialog();
                }
                //Get Companies
                getCompanies = function () {
                    dataservice.getCompanyByCustomerType({
                        IsCustomerType: getIsCustomerType(),
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
                            toastr.error("Failed to load Detail . Error: ");
                        }
                    });
                },
                //Search Company
                searchCompany = function () {
                    getCompanies();
                },
                //Reset Company Dialog
                resetCompany = function () {
                    companySearchFilter(undefined);
                    getCompanies();
                },
                //On Select Company
                selectCompany = function (company) {
                    selectedActivity().companyName(company.name());
                    selectedActivity().contactCompanyId(company.id());
                    selectedCompany(company);
                    hideCompanyDialog();
                    companyContacts.removeAll();
                    getCompanyContactByCompanyId(company);
                },
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
                            if (data !== null && loggedInUserId() === selectedActivity().systemUserId()) {
                                if (selectedActivity().id() === undefined) {
                                    selectedActivity().id(data);
                                    var activity = selectedActivity();
                                    items.push({
                                        id: activity.id(),
                                        title: activity.subject(),
                                        start: activity.startDateTime(),
                                        end: activity.endDateTime(),
                                    });
                                }
                            }
                            view.hideCalendarActivityDialog();
                            toastr.success("Successfully save.");
                        },
                        error: function (exceptionMessage, exceptionType) {

                            if (exceptionType === ist.exceptionType.CaresGeneralException) {

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
                        companyId: company.id(),
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

                //Initialize
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    pager(pagination.Pagination({ PageSize: 10 }, companies, getCompanies));
                    getBase();
                };

                return {
                    selectedActivity: selectedActivity,
                    companySearchFilter: companySearchFilter,
                    selectCompany: selectCompany,
                    selectedCompany: selectedCompany,
                    loggedInUserId: loggedInUserId,
                    pager: pager,
                    items: items,
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
                    showCompanyDialog: showCompanyDialog,
                    hideCompanyDialog: hideCompanyDialog,
                    searchCompany: searchCompany,
                    resetCompany: resetCompany,
                    onSaveActivity: onSaveActivity,
                    onDeleteActivity: onDeleteActivity,
                };
            })()
        };
        return ist.calendar.viewModel;
    });

