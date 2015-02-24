'use strict';

describe('Controller: DeliveriesCtrl', function () {

  // load the controller's module
  beforeEach(module('frontendApp'));

  var DeliveriesCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    DeliveriesCtrl = $controller('DeliveriesCtrl', {
      $scope: scope
    });
  }));

  it('should attach a list of awesomeThings to the scope', function () {
    expect(scope.awesomeThings.length).toBe(3);
  });
});
