using System;
using System.Collections.Generic;
using System.Text;

namespace VideoLibraryManagement
{
    class Movie
    {

        public Movie leftMovie;
        public Movie rightMovie;

        public string movieName { get; set; }
        public string starringActor { get; set; }
        public string director { get; set; }
        public int duration { get; set; } //in minutes
        public int releaseDate { get; set; } //in years
        public int movieCopies { get; set; } //decrease if borrowed, increase if returned

        public int timesRented { get; set; } //total number of times movie DVDs have been borrowed
        public enum Genre //different movie genre options
        {
            Drama = 1, Adventure, Family, Action, SciFi,
            Comedy, Animated, Thriller, Other
        } 
        public Genre movieGenre { get; set; } //stores genre of movie

        public enum Classification { G = 1, PG, M, MA } //different movie ratings

        public Classification movieRating { get; set; } //stores classification of the movie

        /// <summary>
        /// Constructor: create a new movie object with the given movieTitle
        /// </summary>
        /// <param name="movieTitle"></param>
        public Movie(string movieTitle)
        {
            this.movieName = movieTitle;
            this.leftMovie = null;
            this.rightMovie = null;
        }
    }
}
