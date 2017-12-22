app.controller("eventRegisterController", ["$routeParams", "$scope", "$http", "$location", function ($routeParams, $scope, $http, $location) {

    console.log("in Event Register Controller");
    console.log("$routeParams :: ", $routeParams);
    var eventId = $routeParams.eventId;
    console.log("eventId :: ", eventId);

    let vm = this;

    vm.message = "Event Registration";

    $scope.thisProfile = {};
    $scope.thisEvent = {};

    // get user info
    $http.get("/api/Freelancers/current")
        .then((result) => {
            console.log("result.data", result.data);
            $scope.thisProfile = result.data;
            userid = result.data.Id;
            console.log("userid :: ", userid);
        })
        .catch((getUserError) => {
            console.log("getFreelancerProfile", getUserError);
        });

    // get event info
    $http.get(`/api/Events/${eventId}`)
        .then((eventResult) => {
            console.log("eventResult :: ", eventResult);
            console.log("eventResult.data :: ", eentResult.data);
            $scope.thisEvent = eventResult.data;
        })
        .catch((eventError) => {
            console.log("error on getEvent", eventError);
        });

}]);