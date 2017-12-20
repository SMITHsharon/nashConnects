app.controller("eventAddController", ["$routeParams", "$scope", "$http", "$location", function ($routeParams, $scope, $http, $location) {

    //.when("/event/add/:nonprofitid",
    //{
          //for user to add an Event
    //    templateUrl: "/ngApp/Views/EventNew.html",
    //    controller:  "eventAddController",
    //    controllerAs: 'vm'
    //})

    console.log("in Add Event Controller");
    let vm = this;

    vm.message = "Add Event";

    $scope.addEvent;
    var nonprofitId = $routeParams.nonprofitid;

    $scope.postEvent = () => {
        console.log("in postEvent");
        {
            //add this method to the nonprofit cs controller
            //db find this nonprofit ... add this event to event table
            //that creates the foreign key
            $http.post(`/api/nonprofits/${nonprofitId}/addevent`,
                {
                    EventName: $scope.addEvent.EventName,
                    StartDate: $scope.addEvent.StartDate,
                    EndDate: $scope.addEvent.EndDate,
                    Description: $scope.addEvent.Description
                })
                .then(function (newEventResult) {
                    console.log(newEventResult);
                    $location.url(`/api/nonprofit/{:id}/events/list`);
                }).catch(error => console.error("error, creating new event", error));
        }
    };


    //filter dates  that are past today and set selectable to false
    /*
    $scope.startDateBeforeRender = function ($dates) {
        const todaySinceMidnight = new Date();
        todaySinceMidnight.setUTCHours(0, 0, 0, 0);
        $dates.filter(function (date) {
            return date.utcDateValue < todaySinceMidnight.getTime();
        }).forEach(function (date) {
            date.selectable = false;
        });
    };
    */

    /* Bindable functions
 -----------------------------------------------*/
    $scope.endDateBeforeRender = endDateBeforeRender
    $scope.endDateOnSetTime = endDateOnSetTime
    $scope.startDateBeforeRender = startDateBeforeRender
    $scope.startDateOnSetTime = startDateOnSetTime

    function startDateOnSetTime() {
        $scope.$broadcast('start-date-changed');
    };

    function endDateOnSetTime() {
        $scope.$broadcast('end-date-changed');
    };

    function startDateBeforeRender($dates) {
        if ($scope.dateRangeEnd) {
            var activeDate = moment($scope.dateRangeEnd);

            $dates.filter(function (date) {
                return date.localDateValue() >= activeDate.valueOf()
            }).forEach(function (date) {
                date.selectable = false;
            })
        }
    };

    function endDateBeforeRender($view, $dates) {
        if ($scope.dateRangeStart) {
            var activeDate = moment($scope.dateRangeStart).subtract(1, $view).add(1, 'minute');

            $dates.filter(function (date) {
                return date.localDateValue() <= activeDate.valueOf()
            }).forEach(function (date) {
                date.selectable = false;
            })
        }
    };

}]);