Todo.controller('TodoController', function ($scope, $log, TodoService) {

    $scope.currentPage = 1;
    $scope.pageSize = 5;
    $scope.todos = [];
    $scope.pagecounter = [];

    $scope.newTodoName = "";
    $scope.pageright = "";
    $scope.pageleft = "";

    $scope.init = function () {
        //var promise = TodoService.get($scope.pageSize, $scope.pageSize);
      
        $scope.then(function (response) {
            $scope.todos = response;
            $scope.todos2 = response;
            $scope.pagecounter = response;
        });
    };

    $scope.paginator = function (page) {
       
        var promise = TodoService.get(page, $scope.pageSize, $scope.newTodoName);
        promise.then(function (response) {
            $scope.todos2 = new Array();

            var s = eval(response);
            var l = JSON.parse(s);

            for (var i = 0; i < l.length; i++) {

                $scope.todos2.push(new TodoViewModel(l[i].Id, l[i].Name, false));
            }

        });
    };

    $scope.addTodo = function () {
        if (!$scope.newTodoName) return;
        
        var promise = TodoService.add($scope.newTodoName);
        promise.then(function (response) {
            $scope.todos.push(new TodoViewModel(response.Id, response.Name, false));
            if (_.size($scope.todos2) > 0)
            {
                TodoService.todolist(response);
                $scope.todos2.push(new TodoViewModel(response.Id, response.Name, false));
            }
  
            $scope.newTodoName = "";
        });
    };

    $scope.inputUpdate = function (ev)
    {
        if (!$scope.newTodoName) return;
        if (ev.which != 13) {
            setTimeout(function () {


                var promise = TodoService.inputUpdate($scope.newTodoName);

                promise.then(function (response) {

                    $scope.todos2 = new Array();

                    var s = eval(response);
                    var l = JSON.parse(s);

                    for (var i = 0; i < l.length; i++) {

                        $scope.todos2.push(new TodoViewModel(l[i].Id, l[i].Name, false));
                    }

                    var count = ($scope.todos2.length / 10) + 1;
                    for (var i = 0; i < count; i++) {
                        $scope.pagecounter.push(i);
                     }
                });
            }, 1000);
        }
        else
        {
            $scope.todos = $scope.todos2;
            TodoService.inputUpdate();
        }


        return _.size($scope.todos2) > 0;
        
    }

    $scope.searchUpdate = function () {
        if (!$scope.newTodoName) return;

        var promise = TodoService.add($scope.newTodoName);
        promise.then(function (response) {
            $scope.todos2.push(new TodoViewModel(response.Id, response.Name, false));
            $scope.newTodoName = "";
        });
     }



    $scope.removeTodo = function (id, removename, name) {
        var promise = TodoService.remove(id, removename, name);
        promise.then(function (response) {
            if (removename == "paginator")
            {
                $scope.todos2 = _.reject($scope.todos2, function (x) { return x.Id === id; });
            }
            else
            {
                $scope.todos = _.reject($scope.todos, function (x) { return x.Id === id; });
            }
            
        });
    };

    $scope.completeTodo = function(todo) {
        var promise = TodoService.update(todo);
        promise.then(function (response) {
            // ui updated by bindings
        });
    };

    $scope.hasAnyTodos = function () {
        return _.size($scope.todos) > 0;
    };

    $scope.fromtodos2totodo = function ()
    {
        $scope.todos = $scope.todos2;
        TodoService.inputUpdate();
    
    }

    function TodoViewModel(id, name, isComplete) {
        this.Id = id;
        this.Name = name;
        this.Completed = isComplete;
    }
});