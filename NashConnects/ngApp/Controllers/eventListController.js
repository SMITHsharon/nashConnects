app.controller("eventListController", ["$routeParams", "$scope", "$http", "$location", function ($routeParams, $scope, $http, $location) {

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

    console.log("in eventListController");

    let vm = this;

    vm.message = "Scheduled Events";

    $scope.nonprofit;
    $scope.events;
    $scope.eventGroups;
    var nonprofitId = $routeParams.nonprofitid;
    console.log("routeParams :: ", $routeParams)
    console.log("nonprofitId :: ", nonprofitId);

    listOfEvents = [];

    var getEventList = () => {
        if (nonprofitId != null) { // get Event for This NonProfit
            $http.get(`/api/NonProfits/${nonprofitId}/events/list`)
                .then((eventListResult) => {
                    //console.log("eventListResult, listing all Events :: ", eventListResult);
                    var eventListDataResult = eventListResult.data;
                    //console.log("Events List eventListDataResult.data :: ", eventListDataResult);
                    //console.log("Events List eventListDataResult.Events :: ", eventListDataResult.Events);
                    $scope.nonprofit = eventListDataResult.nonProfitName;
                    var nonProfitId = eventListDataResult.Id;
                    var listOfEvents = eventListDataResult.Events;

                    $scope.events = listOfEvents;
                    console.log("$scope.events :: ", $scope.events);
                    console.log("listOfEvents[0].EventId :: ", listOfEvents[0].EventId);
                }).catch((eventsError) => {
                    console.log("error, listing NonProfit Events :: ", eventsError);
                });
        }
        else // get Events for All NonProfits
        {
            $http.get("api/NonProfits/events/list")
                .then((eventListResult) => {
                    console.log("eventListResult :: ", eventListResult);
                    var eventListDataResult = eventListResult.data;
                    console.log("Events List eventListDataResult.data :: ", eventListDataResult); // this is an array of objects

                    if (eventListDataResult.length > 0) {
                        Object.keys(eventListDataResult).forEach((key) => {
                            eventListDataResult[key].id = key;
                            listOfEvents.push(eventListDataResult[key]);
                        });
                    }

                    $scope.eventGroups = listOfEvents;
                    console.log("$scope.events :: ", $scope.events);

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