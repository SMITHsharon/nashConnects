app.controller("flListController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/freelancers/list",
    //{
          //for user to view a list of Freelancers
    //    templateUrl: "/ngApp/Views/flList.html",
    //    controller:  "flListController",
    //    controllerAs: 'vm'
    //})

    console.log("in Freelancers List Controller");
    let vm = this;
    vm.message = "This is Nash Freelancers List";

    $scope.freelancers;

    var getFreelancerList = function () {
        $http.get("/api/Freelancers/list")
            .then(function (result) {
                console.log("result, listing all Freelancers :: ", result);
                var dataResults = result.data;
                console.log("Freelancers List result.data :: ", dataResults);
                var listOfFreelancers = [];

                if (dataResults.length > 0) {
                    Object.keys(dataResults).forEach((key) => {
                        dataResults[key].id = key;
                        listOfFreelancers.push(dataResults[key]);
                    });
                }
                $scope.freelancers = listOfFreelancers;
                console.log($scope.freelancers);
            }).catch(function (error) {
                console.log("error, listing all Freelancers :: ", error);
            });
    };
    getFreelancerList();

}
]);