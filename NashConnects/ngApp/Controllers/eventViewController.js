app.controller("eventViewController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/event/add",
    //{
    //for user to add an Event
    //    templateUrl: "/ngApp/Views/EventDetail.html",
    //    controller:  "eventViewController",
    //    controllerAs: 'vm'
    //})

    console.log("in View Event Controller");
    let vm = this;

    vm.message = "This is View Event Controller";


}
]);