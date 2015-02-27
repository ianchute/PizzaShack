'use strict';

describe('Service: PingService', function () {

  // load the service's module
  beforeEach(module('frontendApp'));

  // instantiate service
  var PingService;
  beforeEach(inject(function (_PingService_) {
    PingService = _PingService_;
  }));

  it('should do something', function () {
    expect(!!PingService).toBe(true);
  });

});
