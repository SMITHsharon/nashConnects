app.controller("eventRegisterController", ["$routeParams", "$scope", "$http", "$location", function ($routeParams, $scope, $http, $location) {

    var eventId = $routeParams.eventId;
    var nonprofitId = $routeParams.nonprofitid;

    let vm = this;

    vm.message = "Event Registration";

    $scope.thisProfile = {};
    $scope.thisEvent = {};
    $scope.nonProfitName;

    // get NonProfit name
    $http.get(`/api/NonProfits/${nonprofitId}`)
        .then((npResult) => {
            var nonProfitResult = npResult.data;
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


    $http.get(`/api/NonProfits/${nonprofitId}/events/${eventId}`)
        .then((eventResult) => {
            $scope.thisEvent = eventResult.data;
        })
        .catch((eventError) => {
            console.log("error on getEvent", eventError);
        });
    

    $scope.postRegistration = (userId) => {
        $http.post(`/api/Freelancers/${userId}/register/${eventId}`)
        .then((registrationResult) => {
            $location.url(`/nonprofit/${nonprofitId}/events/list`)
        })
        .catch((registrationError) => {
            console.error("error on User Register for Event :: ", registrationError);
        });
    };


    $scope.cancel = () => {
        $location.url(`/nonprofit/${nonprofitId}/events/list`)
    };

}]);