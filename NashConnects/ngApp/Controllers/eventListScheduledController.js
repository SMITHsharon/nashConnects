app.controller("eventListScheduledController", ["$routeParams", "$scope", "$http", "$location", function ($routeParams, $scope, $http, $location) {

    //.when("/nonprofit/{:id}/events/list`",
    //{
          //for user to list Events for selected NonProfit
    //    templateUrl: "/ngApp/Views/EventsListSingleNonProfit.html",
    //    controller:  "eventListScheduledController",
    //    controllerAs: 'vm'
    //})
    //.when("/events/list",
    //{
    //    //for user to list all Scheduled Events
    //    templateUrl: "/ngApp/Views/EventsListAllNonProfits.html",
    //    controller: "eventListScheduledController",
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
                        $scope.nonProfitId = eventListDataResult.nonProfitId;
                        $scope.nonProfitName = eventListDataResult.nonProfitName;
                        $scope.events = listOfEvents;
                    }
                    else // this NonProfit has no Events scheduled
                    {
                        alert(`No events are scheduled for ${eventListDataResult.nonProfitName}`);
                        $location.url(`/nonprofits/list`);
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
        $location.url(`/nonprofit/${nonprofitId}/event/${eventId}/register`);
    };


    $scope.postEvent = (nonprofitId) => {
        $location.url(`/event/add/${nonprofitId}`);
    };


    $scope.editEvent = (eventId) => {
        console.log("eventId :: ", eventId);
        console.log("nonprofitId :: ", nonprofitId);
        $location.url(`/nonprofit/${nonprofitId}/event/${eventId}/edit`);
    };


}]);