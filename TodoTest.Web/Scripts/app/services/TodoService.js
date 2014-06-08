Todo.service('TodoService', function ($log, $http, $q, HttpRequestUrlHelper) {

    function TodoService() {
        var self = this,
            baseEndpoint = "/todo/";

        self.get = function (page, pageSize, name) {

            var endpoint = baseEndpoint + "list",
            deferred = $q.defer(),
            request = {
                'name': name,
                'page': page,
                'pageSize': pageSize
            };

            $http.post(endpoint, request, {
            }).success(function (response) {
                deferred.resolve(response);
            }).error(function (error) {
                deferred.reject(error);
            });
            return deferred.promise;
        };

        self.add = function (name) {
            var endpoint = baseEndpoint + "add",
                deferred = $q.defer(),
                request = {
                    'name': name
                };

            $http.post(endpoint, request, {
            }).success(function (response) {
                deferred.resolve(response);
            }).error(function (error) {
                deferred.reject(error);
            });

            return deferred.promise;
        };

       
        self.update = function(todo) {
            var endpoint = baseEndpoint + "update",
               deferred = $q.defer(),
               request = {
                   'todo': todo
               };
            
            $http.post(endpoint, request, {
            }).success(function (response) {
                deferred.resolve(response);
            }).error(function (error) {
                deferred.reject(error);
            });

            return deferred.promise;
        };

        self.inputUpdate = function (name) {
          
          
            var endpoint = baseEndpoint + "inputUpdate",
               deferred = $q.defer(),
               request = {
                   'name': name
               };
            
            $http.post(endpoint, request, {
            }).success(function (response) {
              
                deferred.resolve(response);
            }).error(function (error) {
                deferred.reject(error);
            });
          
            return deferred.promise;
        };

        self.todolist = function (todo) {


            var endpoint = baseEndpoint + "AddToDoList",
               deferred = $q.defer(),
               request = {
                   'todo': todo
               };

            $http.post(endpoint, request, {
            }).success(function (response) {

                deferred.resolve(response);
            }).error(function (error) {
                deferred.reject(error);
            });

            return deferred.promise;
        };

        self.searchUpdate = function () {
            var endpoint = baseEndpoint + "searchUpdate",
               deferred = $q.defer();

            $http.post(endpoint, {
            }).success(function (response) {
                alert(response.$q);
                //deferred.resolve(response);
            }).error(function (error) {
                deferred.reject(error);
            });

            return deferred.promise;
        };
        
        self.remove = function (id, removename, name) {
            var endpoint = baseEndpoint + "remove",
                deferred = $q.defer(),
                request = {
                    'guid': id,
                    'removename': removename,
                    'name': name
                };

            $http.post(endpoint, request, {
            }).success(function (response) {
                deferred.resolve(response);
            }).error(function (error) {
                deferred.reject(error);
            });

            return deferred.promise;
        };
    }

    return new TodoService();
});
