app.controller("eventListRegisteredController", ["$routeParams", "$scope", "$http", "$location", function ($routeParams, $scope, $http, $location) {

    //.when("/events/list/registered",
    //    {
    //        // for user to list Events s/he has Registered For
    //        templateUrl: "/ngApp/Views/EventsListRegistered.html",
    //        controller:  "eventListRegisteredController",
    //        controllerAs: 'vm'
    //    })

    let vm = this;

    vm.message = "Registered Events";

   
    $scope.eventGroups;
    listOfEvents = [];

    $scope.freelancer = {};
    let userid;

    var getUser = () => {
        $http.get("/api/Freelancers/current")
            .then((result) => {
                $scope.freelancer = result.data;
                userid = result.data.Id;
            })
            .catch((error) => {
                console.log("getFreelancerProfile", error);
            });
    };

   
    var getEvents = (userid) => {
        $http.get(`/api/Freelancers/${userid}/registeredEvents`)
            .then((eventsResult) => {
                $scope.eventGroups = eventsResult.data.RegEvents;
            })
            .catch((error) => {
                console.log("error, getting Registered Events", error);
            });
    };

    getUser();
    getEvents(userid);

}]);