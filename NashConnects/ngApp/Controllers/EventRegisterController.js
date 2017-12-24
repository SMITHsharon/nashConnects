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
    //$http.get(`/api/NonProfits/${nonprofitId}/events/${eventId}`)
    $http.get(`/api/NonProfits/${nonprofitId}/events/${eventId}`)
        .then((eventResult) => {
            $scope.thisEvent = eventResult.data;
            console.log("eventResult", eventResult);
            console.log("eventId :: ", eventId);
        })
        .catch((eventError) => {
            console.log("error on getEvent", eventError);
        });
    

    $scope.postRegistration = (userId) => {
        console.log("passing eventId, userId:: ", eventId, userId);
        $http.post(`/api/Freelancers/${userId}/register/${eventId}`)
        .then((registrationResult) => {
            // display confirmation message
            console.log("registrationResult :: ", registrationResult);
            $location.url(`/nonprofit/${nonprofitId}/events/list`)
        })
        .catch((registrationError) => {
            console.error("error on User Register for Event :: ", registrationError);
        });
    };

}]);