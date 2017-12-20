app.controller("eventAddController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/event/add/:nonprofitid",
    //{
          //for user to add an Event
    //    templateUrl: "/ngApp/Views/EventNew.html",
    //    controller:  "eventAddController",
    //    controllerAs: 'vm'
    //})

    console.log("in Add Event Controller");
    let vm = this;

    vm.message = "This is Add Event Controller";

    $scope.addEvent;

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
                    $location.url(`/api/nonprofit/{:id}/events/list`);
                }).catch(error => console.error("error, creating new event", error));
        }
    };

}]);