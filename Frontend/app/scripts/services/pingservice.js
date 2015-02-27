'use strict';

/**
 * @ngdoc service
 * @name frontendApp.PingService
 * @description
 * # PingService
 * Service in the frontendApp.
 */
angular.module('frontendApp')
  .service('PingService', function ($http, ApiPath) {
      return {
          ping: function () { return $http.get(ApiPath); }
      }
  });