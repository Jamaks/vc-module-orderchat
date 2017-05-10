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
    // http request info
	$http.post('api/order/chat/room/info/' + $scope.blade['CustomerOrder'].CustomerOrderId)
        .then(function (resp) {
            $scope.chatInfo = resp.data;
        })
}]);