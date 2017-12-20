app.controller("npListController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/nonprofits/list",
    //{
    //for user to view a list of NonProfits
    //    templateUrl: "/ngApp/Views/npList.html",
    //    controller:  "npListController",
    //    controllerAs: 'vm'
    //})

    console.log("in NonProfits List Controller");
    let vm = this;

    vm.message = "Nash NonProfits";

    $scope.nonprofits;
    //$scope.addEvent;

    var getNonProfitList = function () {
        $http.get("/api/NonProfits/list")
            .then(function (result) {
                console.log("result, listing all NonProfits :: ", result);
                var dataResults = result.data;
                console.log("NonProfits List result.data :: ", dataResults);
                var listOfNonProfits = [];

                if (dataResults.length > 0) {
                    Object.keys(dataResults).forEach((key) => {
                        dataResults[key].id = key;
                        listOfNonProfits.push(dataResults[key]);
                    });
                }
                $scope.nonprofits = listOfNonProfits;
                console.log($scope.nonprofits);
            }).catch(function (error) {
                console.log("error, listing all NonProfits :: ", error);
            });
    };
    getNonProfitList();


    $scope.recommend = (nonprofitId) => {
        $http.put(`/api/NonProfits/likes/${nonprofitId}`)
            .then((likesAddResult) => {
                location.reload();
            })
            .catch((error) => {
                console.log("error on Likes count :: ", error);
            });
    };


    /*
    NOOOOOOOOO ... ??? 
    MOVED TO eventAddController
    $scope.postEvent = (nonprofitId) => {
        console.log("in postEvent");
        {
            //add this method to the nonprofit cs controller
            //db find this nonprofit ... add this event to event table
            //that creates the foreign key
            $http.post("/api/nonprofit/{id}/addevent",
                {
                    EventName: $scope.addEvent.EventName,
                    StartDate: $scope.addEvent.SchoolId,
                    EndDate: $scope.addEvent.NoteText,
                    Description: $scope.addEvent.EnrolledClassId
                })
                .then(function (newEventResult) {
                    console.log(newEventResult);
                    $location.url(`/api/NonProfits/Events/${nonprofitId}`);
                }).catch(error => console.error("error, creating new event", error));
        }
    };
    */

}]);