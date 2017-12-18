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
    var listOfFreelancers = [];

    var getFreelancerList = function () {
        $http.get("/api/Freelancers/list/newsletter")
            .then((result) => {
                var dataResults = result.data;

                if (dataResults.length > 0) {
                    Object.keys(dataResults).forEach((key) => {
                        dataResults[key].id = key;
                        listOfFreelancers.push(dataResults[key]);
                    });
                }
                $scope.freelancers = listOfFreelancers;

            }).catch(function (error) {
                console.log("error, listing all Freelancers :: ", error);
            });
    };
    getFreelancerList();

}]);