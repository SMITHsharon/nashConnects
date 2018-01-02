app.controller("eventListScheduledController", ["$routeParams", "$scope", "$http", "$location", function ($routeParams, $scope, $http, $location) {

    //.when("/nonprofit/{:id}/events/list`",
    //{
          //for user to list Events for selected NonProfit
    //    templateUrl: "/ngApp/Views/EventsListSingleNonProfit.html",
    //    controller:  "eventListController",
    //    controllerAs: 'vm'
    //})
    //.when("/events/list",
    //{
    //    //for user to list all Scheduled Events
    //    templateUrl: "/ngApp/Views/EventsListAllNonProfits.html",
    //    controller: "eventListController",
    //    controllerAs: 'vm'
    //})

    let vm = this;

    vm.message = "Scheduled Events";

    $scope.nonProfitId;
    $scope.nonProfitName;
    $scope.events;
    $scope.eventGroups;
    var nonprofitId = $routeParams.nonprofitid;

    listOfEvents = [];

    var getEventList = () => {
        if (nonprofitId !== undefined) { // get Event for This NonProfit
            $http.get(`/api/NonProfits/${nonprofitId}/events/list`)
                .then((eventListResult) => {
                    var eventListDataResult = eventListResult.data;
                    listOfEvents = eventListDataResult.Events;
                    if (listOfEvents.length > 0)
                    {
                        $scope.nonprofitId = eventListDataResult.nonProfitId;
                        $scope.nonProfitName = eventListDataResult.nonProfitName;
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
                    console.log("$scope.eventGroups :: ", $scope.eventGroups);
                    
                }).catch((errorListResult) => {
                    console.error("error, listing All Events :: ", errorListResult);
                });
        }
    };
    getEventList();


    $scope.register = (eventId) => {
        console.log("passing nonprofitId, eventId :: ", nonprofitId, eventId);
        $location.url(`/nonprofit/${nonprofitId}/event/${eventId}/register`);
    }

    $scope.edit = (eventId) => {
        console.log("passing nonprofitId, eventId :: ", nonprofitId, eventId);
        $location.url(`/nonprofit/${nonprofitId}/event/${eventId}/edit`);
    }


}]);