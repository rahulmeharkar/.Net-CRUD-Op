/// <reference path="../angular.min.js" />
/// <reference path="../jquery.min.js" />
/// <reference path="app.js" />
app.controller('photocontroller', fun1);
function fun1($scope,$http)
{
    $scope.albumcategary = [];
    $scope.GetCategary = function () {
        $http.get('/Home/GetCategary').then(function success(response) {
            $scope.albumcategary = response.data;
        },
        function failed(err) {
            console.log(JSON.stringify(err));
        });
    }
}