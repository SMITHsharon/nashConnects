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
        if (nonprofitId !== undefined) { // get Event for This NonProfit
            $http.get(`/api/NonProfits/${nonprofitId}/events/list`)
                .then((eventListResult) => {
                    var eventListDataResult = eventListResult.data;
                    $scope.nonprofit = eventListDataResult.nonProfitName;
                    var nonProfitId = eventListDataResult.Id;
                    var listOfEvents = eventListDataResult.Events;

                    $scope.eventGroups = listOfEvents;
                }).catch((eventsError) => {
                    console.log("error, listing NonProfit Events :: ", eventsError);
                });
        }
        else // get Events for All NonProfits
        {
            $http.get("api/NonProfits/events/list")
                .then((eventListResult) => {
                    var eventListDataResult = eventListResult.data;
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