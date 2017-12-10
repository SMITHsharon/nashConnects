app.controller("flViewController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/freelance/profile/:id",
    //{
          //for user to view a Freelance profile
    //    templateUrl: "/ngApp/Views/flProfile.html",
    //    controller:  "flViewController",
    //    controllerAs: 'vm'
    //});

    console.log("in FreeLance View Controller");
    let vm = this;

    vm.message = "This is Freelance View Profile";


}
]);