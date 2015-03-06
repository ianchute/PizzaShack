'use strict';

/**
 * @ngdoc service
 * @name frontendApp.StatusCodeService
 * @description
 * # StatusCodeService
 * Service in the frontendApp.
 */
angular.module('frontendApp')
  .service('StatusCodeService', function () {
      return {
          getStatusName: function (status) {

              var codeNameMap = {
                  0: 'Empty',
                  100: 'Continue',
                  101: 'Switching Protocols',
                  102: 'Processing',
                  200: 'OK',
                  201: 'Created',
                  202: 'Accepted',
                  203: 'Non-Authoritative Information',
                  204: 'No Content',
                  205: 'Reset Content',
                  206: 'Partial Content',
                  207: 'Multi-Status',
                  208: 'Already Reported',
                  226: 'IM Used',
                  300: 'Multiple Choices',
                  301: 'Moved Permanently',
                  302: 'Found',
                  303: 'See Other',
                  304: 'Not Modified',
                  305: 'Use Proxy',
                  306: 'Switch Proxy',
                  307: 'Temporary Redirect',
                  308: 'Permanent Redirect',
                  400: 'Bad Request',
                  401: 'Unauthorized',
                  402: 'Payment Required',
                  403: 'Forbidden',
                  404: 'Not Found',
                  405: 'Method Not Allowed',
                  406: 'Not Acceptable',
                  407: 'Proxy Authentication Required',
                  408: 'Request Timeout',
                  409: 'Conflict',
                  410: 'Gone',
                  411: 'Length Required',
                  412: 'Precondition Failed',
                  413: 'Request Entity Too Large',
                  414: 'Request-URI Too Long',
                  415: 'Unsupported Media Type',
                  416: 'Requested Range Not Satisfiable',
                  417: 'Expectation Failed',
                  418: 'I\'m a teapot',
                  419: 'Authentication Timeout',
                  420: 'Method Failure / Enhance Your Calm',
                  422: 'Unprocessable Entity',
                  423: 'Locked',
                  424: 'Failed Dependency',
                  426: 'Upgrade Required',
                  428: 'Precondition Required',
                  429: 'Too Many Requests',
                  431: 'Request Header Fields Too Large',
                  440: 'Login Timeout (Microsoft)',
                  444: 'No Response (Nginx)',
                  449: 'Retry With (Microsoft)',
                  450: 'Blocked by Windows Parental Controls (Microsoft)',
                  // 451: 'Unavailable For Legal Reasons (Internet draft)',
                  // 451: 'Redirect (Microsoft)',
                  452: 'Conference Not Found',
                  453: 'Not Enough Bandwidth',
                  454: 'Session Not Found',
                  455: 'Method Not Valid in This State',
                  456: 'Header Field Not Valid for Resource',
                  457: 'Invalid Range',
                  458: 'Parameter Is Read-Only',
                  459: 'Aggregate operation not allowed',
                  460: 'Only aggregate operation allowed',
                  461: 'Unsupported transport',
                  462: 'Destination unreachable',
                  463: 'Key management Failure',
                  494: 'Request Header Too Large (Nginx)',
                  495: 'Cert Error (Nginx)',
                  496: 'No Cert (Nginx)',
                  497: 'HTTP to HTTPS (Nginx)',
                  498: 'Token expired/invalid (Esri)',
                  //499: 'Client Closed Request (Nginx)',
                  //499: 'Token required (Esri)',
                  500: 'Internal Server Error',
                  501: 'Not Implemented',
                  502: 'Bad Gateway',
                  503: 'Service Unavailable',
                  504: 'Gateway Timeout',
                  505: 'HTTP Version Not Supported',
                  506: 'Variant Also Negotiates',
                  507: 'Insufficient Storage',
                  508: 'Loop Detected',
                  509: 'Bandwidth Limit Exceeded',
                  510: 'Not Extended',
                  511: 'Network Authentication Required',
                  551: 'Option not supported',
                  598: 'Network read timeout error',
                  599: 'Network connect timeout error'
              };

              return codeNameMap[status];
          }
      };
  });
