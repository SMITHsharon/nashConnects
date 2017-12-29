app.controller("eventListRegisteredController", ["$routeParams", "$scope", "$http", "$location", function ($routeParams, $scope, $http, $location) {

    console.log("in eventListRegisteredController");

    //.when("/events/list/registered",
    //    {
    //        // for user to list Events s/he has Registered For
    //        templateUrl: "/ngApp/Views/EventsListRegistered.html",
    //        controller:  "eventListRegisteredController",
    //        controllerAs: 'vm'
    //    })

    let vm = this;

    vm.message = "Registered Events";

    $scope.freelancer = {};
    $scope.eventGroups;
    $scope.userProfile = {};

    listOfEvents = [];

   

    $http.get("/api/Freelancers/current")
        .then(function (userProfile) {
            console.log("userProfile.data", userProfile.data);
            $scope.freelancer = userProfile.data;
            userid = userProfile.data.Id;
            listOfEvents = userProfile.data.RegEvents;
            console.log("userid :: ", userid);
            console.log("RegEvents :: ", listOfEvents);

            $scope.events = listOfEvents;
        })
        .catch((error) => {
            console.log("error, getting Freelancer Profile", error);
        });

    var getEventList = () => {

    };
    getEventList();


}]);