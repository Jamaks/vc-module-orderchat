OrderChatModule.controller('Jamak.OrderChatModule.ChatDetailController',
    ['$scope', '$localStorage', '$http',
    function ($scope, $localStorage, $http) {
        var baseUri = "api/order/chat/";
        var blade = $scope.blade;

        //Init
        blade.chatMessages = [];
        blade.addMessages = addMessages;
        blade.getMessages = getMessages;
        getMessages(blade.currentEntity.customerOrder.customerId)

        function getMessages(roomId) {
            blade.isLoading = true;
            $http.post(baseUri + "room/messages/" + roomId).then(function (resp) {
                blade.isLoading = false;
                blade.chatMessages = resp.data;
                addMessages({ 'RoomId': roomId, 'Text': 'test string' })
            });
        }
        /** 
        * @param {Object} model  {RoomId:{string}, Text:{string}}
        */
        function addMessages(model) {
            blade.isLoading = true;
            $http.post(baseUri + "room/message/add", model).then(function (resp) {
                blade.isLoading = false;
                blade.chatMessages.push(resp.data);

            });
        }

    }]);