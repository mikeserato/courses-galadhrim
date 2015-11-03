var movies = [];
var watchlist = [];

var pagination = {
	"pageSize": 3
};


(function init() {
	$.get("movies.json",function(data){
		movies = data;
		paginate();
		
		setPage(0);		
	});

	$("#watchlist-items").hide();
})();

/* builds the pagination for the page */
function paginate() {
	/* (ITEM #5) implement */
	var num = Math.ceil(movies.length/pagination.pageSize);
	for(var i=1;i<=num;i++){
		var btn = $("<button>"+i+"</button>").attr("value",i-1).on("click",function(){
			setPage($(this).attr("value"));
		});
		$("#pagination").append(btn);
	}
}


/* sets the current page */
function setPage(page) {
	/* (ITEM #6) implement */
		$.get("templates/movie.html",function(mdata){
			$("#catalog").empty();
			$.template("movie_list",mdata);
			for(var i=page*pagination.pageSize; i<page*pagination.pageSize+pagination.pageSize; i++){
				if(i==movies.length) break;
				$.tmpl("movie_list",movies[i]).appendTo( "#catalog" );
			}
		});
}


/* adds the movie to the watchlist */
function add(movie) {
	if (indexOf(watchlist, movie) < 0) {
		watchlist.push(movie);
		$("#counter").html(watchlist.length);
		$.get("templates/watchlist.html",function(data){
			$.template("watch_list",data);
			$.tmpl("watch_list",watchlist[watchlist.length-1]).appendTo( "#watchlist-items");	
		});
		alert("Movie added in the list.")
	} else {
		alert("Movie already in the list.");
	}
}


/* checks if target exists in the array; returns -1 if target is not found */
function indexOf(items, target) {
	for(var i=0; i<items.length; i++){
		if(items[i].title==target.title) return 1;
	}
	return -1;
}


/* renders the watchlist; fade in to show; fade out to hide */
function renderWatchlist() {
	/* (ITEM #9) implement */
	$.get("templates/watchlist.html",function(data){
		$.template("watch_list",data);
		$("#watchlist-items").empty();
		for(var i=0;i<watchlist.length;i++){
			$.tmpl("watch_list",watchlist[i]).appendTo( "#watchlist-items");	
		}
		$("#watchlist-items").fadeToggle();
	});
}


/* removes the movie from the watchlist */
function removie(movie) {
	for(var i=0; i<watchlist.length; i++){
		if(watchlist[i].title==movie.title) break;
	}
	watchlist.splice(i,1);
	$("#counter").html(watchlist.length);
	renderWatchlist();
	$("#watchlist-items").fadeToggle();
}