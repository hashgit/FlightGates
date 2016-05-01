app.controller("FlightCtrl", ["$scope", "$routeParams", "$http",
  function ($scope, $routeParams, $http) {
      $scope.FlightId = $routeParams.flightId;

      $http.get("api/flights/" + $routeParams.flightId).success(function (data) {
          $scope.Flight = data;
      });

      $http.get("api/gates").success(function (data) {
          $scope.Gates = data;
      });

      $scope.SaveFlight = function() {
          $http.post("api/flights/" + $scope.FlightId, $scope.Flight).success(function (data) {
              if (data === true) {
                  $scope.UpdateMessage = "Flight has been updated.";
              } else {
                  $scope.UpdateMessage = "Flight could not be updated.";
              }
          });
      };

      $scope.CancelFlight = function () {
          if ($scope.FlightId === 0) return;

          $http.delete("api/flights/" + $scope.FlightId).success(function (data) {
              if (data === true) {
                  $scope.UpdateMessage = "Flight has been updated.";
                  $scope.Flight = { GateId: 0, ArrivalDateTime: "", DepartureDateTime: "" };
                  $scope.FlightId = 0;
              } else {
                  $scope.UpdateMessage = "Flight could not be updated.";
              }
          });
      };
  }]);
