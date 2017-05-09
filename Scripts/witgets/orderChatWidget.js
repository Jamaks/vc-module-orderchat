OrderChatModule.controller('Jamak.OrderChatModule.ChatWitget', ['$scope','$http', 'platformWebApp.bladeNavigationService', function ($scope, $http,bladeNavigationService) {
	$scope.chatInfo = {};

	$scope.openChatBlade = function () {
		var newBlade = {
			id: 'operationChat',
			title: 'Chat',
			currentEntity: $scope.blade,
			controller: 'Jamak.OrderChatModule.ChatDetailController',
			template: 'Modules/Jamak.OrderChatModule/Scripts/blades/chatDetail.tpl.html'
		};
		bladeNavigationService.showBlade(newBlade, $scope.blade);
	};
    //TODO: http request info
    
}]);