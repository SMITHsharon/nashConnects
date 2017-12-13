app.controller("flEditController", ["$rootScope", "$scope", "$http", "$location", function ($rootScope, $scope, $http, $location) {

    //.when("/freelance/account,
    //{
          //for user to edit Freelance profile
    //    templateUrl: "/ngApp/Views/flProfile.html",
    //    controller: "flEditController",
    //    controllerAs: 'vm'
    //})



    console.log("in Freelancers Edit Controller");
    //let vm = this;

    $scope.message = "Freelance Profile";

    $scope.thisProfile = {};

    $http.get("/api/Freelancers/current")
        .then(function (result) {
            console.log("result.data", result.data);
           $scope.thisProfile = result.data;
        })
        .catch((error) => {
            console.log("getFreelancerProfile", error);
        });



}
]);