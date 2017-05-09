var moduleName = "Jamak.OrderChatModule";

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

var OrderChatModule=angular.module(moduleName, [])
.run(
  ['platformWebApp.widgetService', '$http', '$compile',
	function (widgetService, $http, $compile) {
	  
      var chatWitget = {
            controller: 'Jamak.OrderChatModule.ChatWitget',
            template: 'Modules/Jamak.OrderChatModule/Scripts/witgets/orderChatWidget.tpl.html'
        };
        widgetService.registerWidget(chatWitget, 'customerOrderDetailWidgets');
	}]);
    