'use strict';

describe('Directive: psNavbar', function () {

  // load the directive's module
  beforeEach(module('frontendApp'));

  var element,
    scope;

  beforeEach(inject(function ($rootScope) {
    scope = $rootScope.$new();
  }));

  it('should make hidden element visible', inject(function ($compile) {
    element = angular.element('<ps-navbar></ps-navbar>');
    element = $compile(element)(scope);
    expect(element.text()).toBe('this is the psNavbar directive');
  }));
});
