app.controller("flListController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/freelancers/list",
    //{
          //for user to view a list of Freelancers
    //    templateUrl: "/ngApp/Views/flList.html",
    //    controller:  "flListController",
    //    controllerAs: 'vm'
    //})

    console.log("in Newsletter Subscribers List Controller");
    let vm = this;
    vm.message = "Newsletter Subscribers";

    $scope.freelancerGroups = [];

    var getFreelancerList = function () {
        $http.get("/api/Freelancers/list")
            .then(function (result) {
                var freelancerGroupings = result.data;
                console.table(freelancerGroupings);
                
                $scope.freelancerGroups = freelancerGroupings;

            }).catch(function (error) {
                console.error("error, listing all Freelancers :: ", error);
            });
    };
    getFreelancerList();


    $scope.recommend = (freelancerId) => {
        $http.get(`/api/NonProfits/${freelancerId}`)
            .then((getResult) => {
                let thisProfile = getResult.data;
                let likesCount = thisProfile.RecommendCount + 1;

                $http.put(`/api/NonProfits/likes/${nonprofitId}`,
                    {
                        RecommendCount: likesCount,
                        UserName: thisProfile.UserName,
                        FirstName: thisProfile.FirstName,
                        LastName: thisProfile.LastName,
                        Email: thisProfile.Email,
                        Name: thisProfile.Name,
                        WebsiteURL: thisProfile.WebsiteURL,
                        CalendarLink: thisProfile.CalendarLink,
                        Description: thisProfile.Description,
                        Active: true,
                        Id: nonprofitId
                    })
                    .then((putResult) => {
                        console.log("incremented Likes count :: ", putResult);
                        location.reload();
                    })
                    .catch((error) => {
                        console.log("error on Likes count :: ", error);
                    })
            })
            .catch((error) => {
                console.log("error on getNonProfitProfile", error);
            });
    };

}]);