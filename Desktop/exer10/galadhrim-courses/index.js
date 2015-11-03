var express = require('express');
var app = express();
app.use('/', express.static(__dirname + '/public'));

var server = app.listen(process.ENV.port, function() {
	var host = server.address().address;
	var port = server.address().port;

	console.log('NodeJS Web server is running at http://%s:%s', host, port);
});

