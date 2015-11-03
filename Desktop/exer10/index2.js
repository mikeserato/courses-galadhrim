var movies = [];
var watchlist = [];

var pagination = {
	"pageSize": 3
};


(function init() {
	/* (ITEM #1) load the template movie.html */
	$("#movies").load("movie.html")
	
	/* (ITEM #2) load the data from movies.json; limit the movies to be shown based on pagination.pageSize */

	$.getJSON("movies.json", storeToMovies); 

	console.log(movies);

	/* (ITEM #3) load the template watchlist.html */
	$("#watchlist").load("templates/watchlist.html")
	
	paginate();

	/* (ITEM #4) hide the movies in the watchlist */
	$("#watchlist").hide();
	
})();


function storeToMovies(data) {
	for(i = 0; i<data.length; i++) movies.push(i);
	console.log(movies);
}

/* builds the pagination for the page */
function paginate() {
	/* (ITEM #5) implement */
}


/* sets the current page */
function setPage(page) {
	/* (ITEM #6) implement */
}


/* adds the movie to the watchlist */
function add(movie) {
	if (indexOf(watchlist, movie) < 0) {
		/* (ITEM #7) add the movie to the watchlist */
	} else {
		alert("Movie already in the list.");
	}
}


/* checks if target exists in the array; returns -1 if target is not found */
function indexOf(items, target) {
	/* (ITEM #8) implement */
}


/* renders the watchlist; fade in to show; fade out to hide */
function renderWatchlist() {
	/* (ITEM #9) implement */
}


/* removes the movie from the watchlist */
function remove(movie) {
	/* (ITEM #10) implement, do not forget to update the counter */
}