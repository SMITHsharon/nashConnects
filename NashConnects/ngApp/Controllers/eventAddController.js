app.controller("eventAddController", ["$routeParams", "$scope", "$http", "$location", function ($routeParams, $scope, $http, $location) {

    //.when("/event/add/:nonprofitid",
    //{
          //for user to add an Event
    //    templateUrl: "/ngApp/Views/EventNew.html",
    //    controller:  "eventAddController",
    //    controllerAs: 'vm'
    //})

    console.log("in Add Event Controller");
    let vm = this;

    vm.message = "Add Event";

    $scope.addEvent;
    $scope.nonProfit;
    var nonprofitId = $routeParams.nonprofitid;

    let getThisNonProfit = (nonprofitId) => {
        console.log("getting nonProfit");
        $http.get(`/api/nonprofits/${nonprofitId}`)
            .then((getResult) => {
                console.log("getResult.data", getResult.data);
                var nonProfitName = getResult.data.Name;
                console.log("nonProfitName = ", nonProfitName);
                $scope.nonProfit = nonProfitName;
                console.log("$scope.nonProfit :: ", $scope.nonProfit); 
            })
            .catch((getError) => {
                console.error("error on getThisNonProfit", getError);
            }) 
    };
    getThisNonProfit(nonprofitId);


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