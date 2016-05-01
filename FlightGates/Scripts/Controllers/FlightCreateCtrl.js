app.controller("FlightCreateCtrl", ["$scope", "$routeParams", "$http",
  function ($scope, $routeParams, $http) {
      $scope.GateId = $routeParams.gateId;
      $scope.Flight = { GateId: $routeParams.gateId, ArrivalDateTime: "", DepartureDateTime: "" };

      $http.get("api/gates").success(function (data) {
          $scope.Gates = data;
      });

      $scope.SaveFlight = function () {
          $http.put("api/flights/", $scope.Flight).success(function (data) {
              if (data === true) {
                  $scope.UpdateMessage = "Flight has been created.";
              } else {
                  $scope.UpdateMessage = "Flight could not be created.";
              }
          });
      };
  }]);
