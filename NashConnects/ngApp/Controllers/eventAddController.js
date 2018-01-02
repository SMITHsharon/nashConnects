
app.controller("eventAddController", ["$routeParams", "$scope", "$http", "$location", function ($routeParams, $scope, $http, $location) {

    //.when("/event/add/:nonprofitid",
    //{
          //for user to add an Event
    //    templateUrl: "/ngApp/Views/EventNew.html",
    //    controller:  "eventAddController",
    //    controllerAs: 'vm'
    //})

    let vm = this;

    vm.message = "Add Event";

    $scope.addEvent;
    $scope.nonProfit;
    var nonProfitId = $routeParams.nonprofitid;

    let getThisNonProfit = (nonProfitId) => {
        $http.get(`/api/nonprofits/${nonProfitId}`)
            .then((getResult) => {
                console.log("getResult :: ", getResult);
                $scope.nonProfit = getResult.data;
            })
            .catch((getError) => {
                console.error("error on getThisNonProfit", getError);
            }) 
    };
    getThisNonProfit(nonProfitId);


    $scope.postEvent = () => {
        {
            $http.post(`/api/nonprofits/${nonprofitId}/addevent`,
                {
                    EventName: $scope.addEvent.EventName,
                    StartDate: $scope.addEvent.StartDate,
                    EndDate: $scope.addEvent.EndDate,
                    Description: $scope.addEvent.Description
                })
                .then(function (newEventResult) {
                    console.log(newEventResult);
                    $location.url(`/nonprofit/${nonprofitId}/events/list`);
                }).catch(error => console.error("error, creating new event", error));
        }
    };

}]);