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
                 companySearchFilter = ko.observable(),
                  pager = ko.observable(),
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
                eventDropOrResize = function (fcEvent) {
                    var a = fcEvent;
                    //this.collection.get(fcEvent.id).save({ start: fcEvent.start, end: fcEvent.end });
                },
                eventClick = function (fcEvent) {
                    var a = fcEvent;
                },
                newEventAdd = function (fcEvent) {
                    var a = fcEvent;
                    view.showCalendarActivityDialog();
                },
                sectionFlags = ko.observableArray([]),
                companyContacts = ko.observableArray([]),
                pipeLineProducts = ko.observableArray([]),
                pipeLineSources = ko.observableArray([]),
                systemUsers = ko.observableArray([]),
                companies = ko.observableArray([]),

                // Get Base
                getBase = function () {
                    dataservice.getCalendarBase({
                        success: function (data) {
                            //Section Flags
                            sectionFlags.removeAll();
                            ko.utils.arrayPushAll(sectionFlags(), data.SectionFlags);
                            sectionFlags.valueHasMutated();
                            //Company Contacts
                            companyContacts.removeAll();
                            ko.utils.arrayPushAll(companyContacts(), data.CompanyContacts);
                            companyContacts.valueHasMutated();
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
                        },
                        error: function () {
                            toastr.error("Failed to load base data.");
                        }
                    });
                },
                items = ko.observableArray([]);
                var newActivity = model.Activity();
                newActivity.title("test");
                //newActivity.startDateTime(new Date());
                var copiedEventObject = $.extend({}, newActivity);
                // assign it the date that was reported
                copiedEventObject.start = newActivity.startDateTime();
                copiedEventObject.allDay = false;
                copiedEventObject.title = newActivity.title();
                items.push(copiedEventObject);

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
                showCompanyDialog = function () {
                    view.showCompanyDialog();
                    getCompanies();
                }
                hideCompanyDialog = function () {
                    view.hideCompanyDialog();
                }

                getCompanies = function () {
                    dataservice.getCompanyByCustomerType({
                        IsCustomerType: 1,
                        SearchString: companySearchFilter(),
                        PageSize: pager().pageSize(),
                        PageNo: pager().currentPage(),
                    }, {
                        success: function (data) {
                            if (data != null) {
                                companies.removeAll();
                                _.each(data, function (item) {
                                    companies.push(model.Company.Create(item));
                                });
                            }
                        },
                        error: function (response) {
                            toastr.error("Failed to load Detail . Error: ");
                        }
                    });
                },

                searchCompany = function () {
                    getCompanies();
                }
                resetCompany = function () {
                    companySearchFilter(undefined);
                    getCompanies();
                }
                //Initialize
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    pager(pagination.Pagination({ PageSize: 10 }, companies, getCompanies));
                    //getBase();
                };

                return {
                    selectedActivity: selectedActivity,
                    companySearchFilter: companySearchFilter,
                    pager: pager,
                    items: items,
                    sectionFlags: sectionFlags,
                    companyContacts: companyContacts,
                    pipeLineProducts: pipeLineProducts,
                    pipeLineSources: pipeLineSources,
                    systemUsers: systemUsers,
                    companies: companies,
                    //Utility Functiions
                    initialize: initialize,
                    mycCalender: mycCalender,
                    showCompanyDialog: showCompanyDialog,
                    hideCompanyDialog: hideCompanyDialog,
                    searchCompany: searchCompany,
                    resetCompany: resetCompany,
                };
            })()
        };
        return ist.calendar.viewModel;
    });

