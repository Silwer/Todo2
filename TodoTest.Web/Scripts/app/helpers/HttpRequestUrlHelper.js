Todo.service('HttpRequestUrlHelper', function ($log) {
    function HttpRequestUrlHelper() {
        var instance = this;
        instance.ensureUniqueRequestUrl = function (url) {
            var seperator = url.indexOf('?') === -1 ? '?' : '&';
            url = url + seperator + 'UniqueKey=' + new Date().getTime();
            $log.log('request url => ' + url);
            return url;
        };
    }

    return new HttpRequestUrlHelper();
});