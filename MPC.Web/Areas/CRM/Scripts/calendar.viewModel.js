/*
    Module with the view model for the Calendar.
*/
define("calendar/calendar.viewModel",
    ["jquery", "amplify", "ko", "calendar/calendar.dataservice", "calendar/calendar.model"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.calendar = {
            viewModel: (function () {
                var
                    //View
                    view,
                    //Active Calendar
                    selectedCalendar = ko.observable(),
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
                var date = new Date();
                var d = date.getDate();
                var m = date.getMonth();
                var y = date.getFullYear();
                oDropped = function (date1, allDay) {

                },
                    eventDropOrResize = function (fcEvent) {
                        var a = fcEvent;
                        //this.collection.get(fcEvent.id).save({ start: fcEvent.start, end: fcEvent.end });
                    },
                 eventClick = function (fcEvent) {
                     var a = fcEvent;
                 },
                newEventAdd = function (fcEvent) {
                    var a = fcEvent;
                },

                    items = ko.observableArray([
                        {
                            title: 'All Day Event',
                            start: new Date(y, m, 1)
                        },
                        {
                            title: 'Long Event',
                            start: new Date(y, m, d - 5),
                            end: new Date(y, m, d - 2)
                        },
                        {
                            id: 999,
                            title: 'Repeating Event',
                            start: new Date(y, m, d - 3, 16, 0),
                            allDay: false
                        },
                        {
                            id: 999,
                            title: 'Repeating Event',
                            start: new Date(y, m, d + 4, 16, 0),
                            allDay: false
                        },
                        {
                            title: 'Meeting',
                            start: new Date(y, m, d, 10, 30),
                            allDay: false
                        },
                        {
                            title: 'Lunch',
                            start: new Date(y, m, d, 12, 0),
                            end: new Date(y, m, d, 14, 0),
                            allDay: false
                        },
                        {
                            title: 'Birthday Party',
                            start: new Date(y, m, d + 1, 19, 0),
                            end: new Date(y, m, d + 1, 22, 30),
                            allDay: false
                        },
                        {
                            title: 'Click for Google',
                            start: new Date(y, m, 28),
                            end: new Date(y, m, 29),
                            url: 'http://google.com/'
                        }
                    ]),
                    viewDate = ko.observable(Date.now()),
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
                   });

                //Initialize
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                };

                return {
                    items: items,
                    //Utility Functiions
                    initialize: initialize,
                    mycCalender: mycCalender,
                };
            })()
        };
        return ist.calendar.viewModel;
    });

