'use strict';

/**
 * @ngdoc service
 * @name frontendApp.FlavorRepository
 * @description
 * # FlavorRepository
 * Service in the frontendApp.
 */
angular.module('frontendApp')
  .service('FlavorRepository', function (ApiPath, $http) {
      var PizzaFlavorApiPath = ApiPath + 'PizzaFlavors/';
      return {
          list: function () { return $http.get(PizzaFlavorApiPath); },
          add: function (flavor) { return $http.post(PizzaFlavorApiPath, flavor); },
          getById: function (id) { return $http.get(PizzaFlavorApiPath + id); },
          deleteById: function (id) { return $http.delete(PizzaFlavorApiPath + id); }
      };
  });
