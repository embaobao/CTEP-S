var Pos = function () { }
Pos.BaseUrl = '';
Pos.ActiveList = [];
Pos.Post = function (url, obj) { $.post(url, obj, function (data, status) { return { data, status }; }); };

pos.BaseUrl='1232';
console.log(pos);