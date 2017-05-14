OrderChatModule.controller('Jamak.OrderChatModule.ChatDetailController',
    ['$scope', '$localStorage', '$http',
    function ($scope, $localStorage, $http) {
        var baseUri = "api/order/chat/";
        var blade = $scope.blade;

        //Init
        blade.chatMessages = [];
        blade.addMessages = addMessages;
        blade.getMessages = getMessages;
        blade.newMess = "";
        getMessages(blade.currentEntity.customerOrder.customerId)


        blade.sendMessage = function (mess) {
            addMessages({ 'RoomId': blade.currentEntity.customerOrder.customerId, 'Text': mess })
        }
        function getMessages(roomId) {
            blade.isLoading = true;
            $http.post(baseUri + "room/messages/" + roomId).then(function (resp) {
                blade.isLoading = false;
                blade.chatMessages = resp.data;
            });
        }
        /** 
        * @param {Object} model  {RoomId:{string}, Text:{string}}
        */
        function addMessages(model) {
            blade.isLoading = true;
            $http.post(baseUri + "room/message/add", model).then(function (resp) {
                blade.isLoading = false;
                blade.chatMessages.messages.push(resp.data);
                blade.newMess = "";
            });
        }

    }]);