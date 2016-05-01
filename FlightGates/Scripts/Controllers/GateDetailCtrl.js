app.controller("GateDetailCtrl", ["$scope", "$routeParams", "$http",
  function ($scope, $routeParams, $http) {
      $scope.GateId = $routeParams.gateId;

      $http.get("api/gates/" + $routeParams.gateId).success(function (data) {
          $scope.Gate = data;
      });
  }]);
