app.controller("eventListController", ["$routeParams", "$scope", "$http", "$location", function ($routeParams, $scope, $http, $location) {

    //.when("/nonprofit/{:id}/events/list`",
    //{
          //for user to list Events
    //    templateUrl: "/ngApp/Views/EventsList.html",
    //    controller:  "eventListController",
    //    controllerAs: 'vm'
    //})

    console.log("in List Events Controller");
    let vm = this;

    vm.message = "Scheduled Events";

    $scope.nonprofit;
    $scope.events;
    var nonprofitId = $routeParams.id;
    console.log("nonprofitId :: ", nonprofitId);
    console.log("$routeParams.id :: ", $routeParams.id);

    var getEventList = function () {
        $http.get(`/api/NonProfits/${nonprofitId}/events/list`)
            .then(function (eventListResult) {
                console.log("eventListResult, listing all Events :: ", eventListResult);
                var eventListDataResults = eventListResult.data;
                console.log("Events List eventListDataResult.data :: ", eventListDataResults);
                var listOfEvents = [];

                if (eventListDataResults.length > 0) {
                    Object.keys(eventListDataResults).forEach((key) => {
                        EventListDataResults[key].id = key;
                        listOfEvents.push(eventListDataResults[key]);
                    });
                }
                $scope.Events = listOfEvents;
                console.log($scope.Events);
            }).catch(function (eventsError) {
                console.log("error, listing all Events :: ", eventsError);
            });
    };
    getEventList();

}]);