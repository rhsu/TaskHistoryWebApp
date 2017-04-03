(function () {
  const app = angular.module('app');

  app.directive('taskListAddTaskForm', function (TaskService,
    TaskTableViewFactory,
    TaskListsService,
    TaskListWithTasksFactory) {

    return {
      restrict: 'E',
      templateUrl: 'Content/task-list-add-task-form.html',
      scope: {
        list: '='
      },
      link: function ($scope, elem, attr, ctrl) {

        /*var refreshTaskList = function () {
          TaskListsService.read($scope.list.listId).then(function (response) {
            const data = response.data;
            if (data) {
              const taskListsWithTasks = TaskListWithTasksFactory.buildFromJson(data);
              console.log(taskListWithTask);
              $scope.list = taskListsWithTasks;
            }
          }, function () {});
        }*/

        $scope.directiveFns = {};
        $scope.directiveFns.createTaskOnList = function () {

          TaskService.createTaskOnList($scope.list.listId,
            $scope.list.taskFormName).then(function (response) {
              const data = response.data;
              if (data) {
                const newTask = TaskTableViewFactory.buildFromJson(data);
                $scope.list.tasks.push(newTask);
              }

          }, function () {});

        }
      }
    }
  });

})();
