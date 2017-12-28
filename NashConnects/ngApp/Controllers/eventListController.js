﻿app.controller("eventListController", ["$routeParams", "$scope", "$http", "$location", function ($routeParams, $scope, $http, $location) {

    //.when("/nonprofit/{:id}/events/list`",
    //{
          //for user to list Events
    //    templateUrl: "/ngApp/Views/EventsListSingleNonProfit.html",
    //    controller:  "eventListController",
    //    controllerAs: 'vm'
    //})
    //.when("/events/list",
    //{
    //    //for user to list Events
    //    templateUrl: "/ngApp/Views/EventsListAllNonProfits.html",
    //    controller: "eventListController",
    //    controllerAs: 'vm'
    //})

    let vm = this;

    vm.message = "Scheduled Events";

    $scope.nonprofit;
    $scope.events;
    $scope.eventGroups;
    var nonprofitId = $routeParams.nonprofitid;

    var eventListDataResult;
    listOfEvents = [];

    var getEventList = () => {
        if (nonprofitId !== undefined) { // get Event for This NonProfit
            $http.get(`/api/NonProfits/${nonprofitId}/events/list`)
                .then((eventListResult) => {
                    eventListDataResult = eventListResult.data;
                    listOfEvents = eventListDataResult.Events;
                    if (listOfEvents.length > 0)
                    {
                        $scope.nonprofit = eventListDataResult.nonProfitName;
                        $scope.events = listOfEvents;
                    }
                    else // this NonProfit has no Events scheduled
                    {
                        alert(`No events are scheduled for ${eventListDataResult.nonProfitName}`);
                        $location.url(`/nonprofits/list`)
                    }

                }).catch((eventsError) => {
                    console.error("error, listing NonProfit Events :: ", eventsError);
                });
        }
        else // get Events for All NonProfits
        {
            $http.get("api/NonProfits/events/list")
                .then((eventListResult) => {
                    eventListDataResult = eventListResult.data;

                    for (var i = 0; i < eventListDataResult.length; i++)
                    {
                        var key = Object.keys(eventListDataResult[i])[0];
                        eventListDataResult[i] = eventListDataResult[i][key];
                    }

                    $scope.eventGroups = eventListDataResult;
                    
                }).catch((errorListResult) => {
                    console.error("error, listing All Events :: ", errorListResult);
                });
        }
    };
    getEventList();


    $scope.register = (eventId) => {
        console.log("passing nonprofitId, eventId :: ", nonprofitId, eventId);
        $location.url(`/nonprofit/${nonprofitId}/event/${eventId}/register`)
    }


}]);