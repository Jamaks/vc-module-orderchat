OrderChatModule.controller('Jamak.OrderChatModule.ChatDetailController',
    ['$scope', '$localStorage', '$http',
    function ($scope, $localStorage, $http) {
        var baseUri = "api/order/chat/";
        var blade = $scope.blade;

        //Init
        blade.chatMessages = [];
        blade.addMessages = addMessages;
        blade.getMessages = getMessages;
        getMessages(blade.currentEntity.CustomerOrder.CustomerOrderId)

        function getMessages(roomId) {
            blade.isLoading = true;
            $http.post(baseUri + "room/messages/" + roomId).then(function (resp) {
                blade.chatMessages = resp.data;
            });
        }
        /** 
        * @param {Object} model  {RoomId:{string}, Text:{string}}
        */
        function addMessages(model) {
            blade.isLoading = true;
            $http.post(baseUri + "room/messages/add", model).then(function (resp) {
                blade.chatMessages.push(resp.data);
            });
        }

    }]);