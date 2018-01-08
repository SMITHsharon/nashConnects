app.controller("eventEditController", ["$routeParams", "$scope", "$http", "$location", function ($routeParams, $scope, $http, $location) {

    //.when("/nonprofit/:nonprofitid/event/:eventid/edit",
    //    {
    //      for NonProfit user to edit an Event
    //        templateUrl: "/ngApp/Views/EventEdit.html",
    //        controller: "eventEditController",
    //        controllerAs: 'vm'
    //    })

    let vm = this;

    vm.message = "Edit Event";

    $scope.editEvent;
    $scope.nonProfit;
    var nonProfitId = $routeParams.nonprofitid;
    var eventId = $routeParams.eventid;
    var thisEvent = {};

    let getThisNonProfit = (nonProfitId) => {
        $http.get(`api/nonprofits/${nonProfitId}`)
            .then((userProfileResult) => {
                $scope.nonProfit = userProfileResult.data;

                $http.get(`api/Events/${eventId}`)
                    .then((eventResult) => {
                        console.log("eventResult :: ", eventResult);
                        $scope.editEvent = eventResult.data;
                        //thisEvent = eventResult.data;
                        console.log("$scope.editEvent.data :: ", $scope.editEvent.data);

                    })
                    .catch((eventError) => {
                        console.error("error on getting event", eventError);
                    });

            })
            .catch((userProfileError) => {
                console.error("error on getting user profile", userProfileError);
            });
    };
    getThisNonProfit(nonProfitId);


    $scope.updateEvent = () => {

        let eventFields = $scope.editEvent;

        $http.put(`api/events/edit/${eventId}`,
            {
                EventName: eventFields.EventName,
                Description: eventFields.Description,

                StartDate: eventFields.StartDate,
                EndDate: eventFields.EndDate

                
            })
            .then((updateResult) => {
                console.log("updateResult");
                $location.path(`/nonprofit/${nonProfitId}/events/list`);
            })
            .catch((updateError) => {
                console.error("error on updating event", updateError);
            });
    };


}]);