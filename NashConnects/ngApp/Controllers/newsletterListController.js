app.controller("newsletterListController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/freelancers/list/newsletter",
    //{
    //for user to view a list of Freelancers
    //    templateUrl: "/ngApp/Views/flListNewsletters.html",
    //    controller:  "newsletterListController",
    //})

    let vm = this;
    vm.message = "Subscribed to Newsletter";

    $scope.freelancers = [];
    listOfFreelancers = [];

    var getFreelancerList = () => {
        $http.get("/api/Freelancers/list/newsletter")
            .then((result) => {
                listOfFreelancers = result.data;
                console.log("listOfFreelancers :: ", listOfFreelancers);
                console.log("listOfFreelancers[0] :: ", listOfFreelancers[0]);

                /*
                if (dataResults.length > 0) {
                    Object.keys(dataResults).forEach((key) => {
                        dataResults[key].id = key;
                        listOfFreelancers.push(dataResults[key]);
                    });
                }
                */

                $scope.freelancers = listOfFreelancers;
                console.log("$scope.freelancers :: ", $scope.freelancers);

            }).catch((error) => {
                console.log("error, listing all Freelancers :: ", error);
            });
    };
    getFreelancerList();

}]);