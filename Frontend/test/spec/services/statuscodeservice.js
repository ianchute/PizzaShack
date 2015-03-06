'use strict';

describe('Service: StatusCodeService', function () {

  // load the service's module
  beforeEach(module('frontendApp'));

  // instantiate service
  var StatusCodeService;
  beforeEach(inject(function (_StatusCodeService_) {
    StatusCodeService = _StatusCodeService_;
  }));

  it('should do something', function () {
    expect(!!StatusCodeService).toBe(true);
  });

});
