﻿/// <reference path="../angular.min.js" />
var app = angular.module('PhotoGalary', []);
app.filter('jsonDate', ['$filter', function ($filter) {
    return function (input, format) {
        return (input)
                ? $filter('date')(parseInt(input.substr(6)), format)
            : '';
    };

}])