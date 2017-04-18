(function () {

  const app = angular.module('app');

  app.controller('TaskListsController', function ($scope,
    TaskListsService,
    TaskListWithTasksFactory,
    $rootScope) {

    const listFeature = $rootScope.appData.flags.find(function(elem) {
      return elem.name == 'list';
    });

    $scope.featureEnabled = {}
    $scope.featureEnabled.list = listFeature && listFeature.value === 'enabled'

    $scope.formData = {};

    $scope.pageFns = {};

    var refreshTaskLists = function () {
      TaskListsService.readAll().then(function (response) {
        const data = response.data;
        if (data) {
          const taskListsWithTasks = TaskListWithTasksFactory.buildFromJsonCollection(data);
          $scope.pageData.taskListWithTasks = taskListsWithTasks
        }
      }, function () {});
    }

    $scope.pageFns.createTaskList = function () {
      TaskListsService.create($scope.formData.name).then(function (response) {
        const data = response.data;
        if (data) {
          // add the new taskList
          const taskListWithTasks = TaskListWithTasksFactory.buildFromJsonCollection(data);
          $scope.pageData.taskListWithTasks.push(taskListWithTasks);

          // reset the form
          $scope.formData.name = '';
        }
      }, function () {});
    }

    $scope.pageFns.syncList = function (list) {
      TaskListsService.read(list.listId).then(function (response) {
        const data = response.data;
        if (data) {
          TaskListWithTasksFactory.updateFromJson(list, data);
        }
      }, function () {});
    }

    $scope.pageData = {};
    $scope.pageData.taskListWithTasks = [];

    refreshTaskLists();

    // TODO consider refactoring this as this is moved to a directive
    $scope.pageFns.undoDeleteTaskList = function (list) {
      // TODO for now, will only focus on deleting and undeleting
      //      a task list
      //      won't touch tasks
      //      because that branches the undo here to two possibilities:
      //        1. undelete that taskList + bulk undelete all associated tasks
      //        2. only undelete the taskList
      TaskListsService.update(list.listId, {name: list.listName, isDeleted: false})
        .then(function (response) {
          const data = response.data;
          if (data) {
            TaskListWithTasksFactory.updateFromJson(list, data);
          }
        }, {})
    }
  });

})();
