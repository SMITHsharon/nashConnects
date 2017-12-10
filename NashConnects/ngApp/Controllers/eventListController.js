app.controller("eventListController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/events/list",
    //{
          //for user to list Events
    //    templateUrl: "/ngApp/Views/EventsList.html",
    //    controller:  "eventListController",
    //    controllerAs: 'vm'
    //})

    console.log("in List Events Controller");
    let vm = this;

    vm.message = "This is List Events Controller";

}
]);