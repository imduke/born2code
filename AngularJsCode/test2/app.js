angular.module('app',['app3','app2']);
angular.module('app2',[]);
angular.module('app3',[]);

angular.module('app2').controller('Controller1',function($scope){
	$scope.name = 'Controller1 in app2';
});

angular.module('app3').controller('Controller1',function($scope){
	$scope.name = 'Controller1 in app3';
});