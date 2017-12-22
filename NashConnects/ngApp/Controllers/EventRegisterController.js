app.controller("eventRegisterController", ["$routeParams", "$scope", "$http", "$location", function ($routeParams, $scope, $http, $location) {

    console.log("in Event Register Controller");
    console.log("$routeParams :: ", $routeParams);
    var eventId = $routeParams.eventId;
    var nonprofitId = $routeParams.nonprofitid;
    console.log("eventId :: ", eventId);
    console.log("nonprofitid :: ", nonprofitId);

    let vm = this;

    vm.message = "Event Registration";

    $scope.thisProfile = {};
    $scope.thisEvent = {};
    $scope.nonProfitName;

    // get NonProfit name
    $http.get(`/api/NonProfits/${nonprofitId}`)
        .then((npResult) => {
            var nonProfitResult = npResult.data;
            console.log("nonProfitResult.Name :: ", nonProfitResult.Name);
            $scope.nonProfitName = nonProfitResult.Name;
            var nonProfitId = nonProfitResult.Id;
            
        }).catch((npError) => {
            console.log("error, getting NonProfit Name :: ", npError);
        });


    // get User info
    $http.get("/api/Freelancers/current")
        .then((result) => {
            $scope.thisProfile = result.data;
            userId = result.data.Id;
        })
        .catch((getUserError) => {
            console.log("getFreelancerProfile", getUserError);
        });


    // get Event info
    $http.get(`/api/NonProfits/${nonprofitId}/events/${eventId}`)
        .then((eventResult) => {
            $scope.thisEvent = eventResult.data;
        })
        .catch((eventError) => {
            console.log("error on getEvent", eventError);
        });

    $scope.postRegistration = (userId, eventId) => {
        console.log("passing userId, eventId :: ", userId, eventId);
        // display confirmation message
        // on OK
        $location.url(`/nonprofit/${nonprofitId}/events/list`)
    }

}]);