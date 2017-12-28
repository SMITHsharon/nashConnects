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
                
                $scope.freelancers = result.data;

            }).catch((error) => {
                console.log("error, listing all Freelancers :: ", error);
            });
    };
    getFreelancerList();

}]);