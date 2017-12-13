app.controller("logoutController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/",
    //{
    //    templateUrl: "/ngApp/Views/home.html",
    //    controller:  "logoutController",
    //    controllerAs: 'vm'
    //})

    console.log("in Logout Controller");
    let vm = this;

    vm.message = "This is Logout // reusing home.html";


}
]);