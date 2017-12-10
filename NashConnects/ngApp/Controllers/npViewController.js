app.controller("flViewController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/nonprofit/profile/:id",
    //{
          //for user to view a NonProfit profile
    //    templateUrl: "/ngApp/Views/npProfile.html",
    //    controller:  "npViewController",
    //    controllerAs: 'vm'
    //});

    console.log("in FreeLance View Controller");
    let vm = this;

    vm.message = "This is Freelance View Profile";


}
]);