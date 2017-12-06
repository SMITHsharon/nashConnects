app.controller("homeController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    console.log("in homeController");
    let vm = this;

    vm.message = "This is home";

    
}
]);