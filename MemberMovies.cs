using System;
using System.Collections.Generic;
using System.Text;

namespace VideoLibraryManagement
{
    class MemberMovies
    {
        /// <summary>
        /// Determines whether the loanedMovie is already borrowed by the current verified member
        /// </summary>
        /// <param name="movieTitle"></param>
        /// <returns></returns>
        public static bool checkBorrowed(string loanMovieTitle)
        {
            bool borrowed = false;

            //iterate through verified member's movie record
            foreach (Movie movie in MemberMenu.verifiedMember.borrowedMovies)
            { 
                //if the given loaned movie could be found in their movie record
                if (movie != null && movie.movieName == loanMovieTitle)
                {
                    borrowed = true;
                    break; //stop searching
                }
                else //movie not found
                {
                    borrowed = false;
                }
            }
            return borrowed;
        }

        /// <summary>
        /// Adds a movie to the member's record
        /// </summary>
        /// <param name="newMovie"></param>
        public static void addMovie(Movie loanedMovie)
        {
            MemberMenu.verifiedMember.borrowedMovies.Add(loanedMovie);
            Console.WriteLine("You have successfully borrowed {0}", loanedMovie.movieName);
        }

        /// <summary>
        /// Prints what is currently stored in the member's movie record
        /// </summary>
        public static void showBorrowedMovies()
        {
            int count = 0;
            foreach (Movie movie in MemberMenu.verifiedMember.borrowedMovies)
            {
                if (movie != null)
                {
                   Console.WriteLine(movie.movieName);
                    count++;
                }
            }
            //if it turns out that the member hasn't borrowed any movies yet
            if (count == 0)
            {
                Console.WriteLine("No movies have been borrowed yet.");
            }
        }

        /// <summary>
        /// For the current verified member, return a given movie to the movie collection
        /// </summary>
        /// <param name="movie"></param>
        public static void returnMovie(string movie)
        {
            //check if the member record contains the movie they want to return
            if (checkBorrowed(movie) == true)
            {
                foreach (Movie movies in MemberMenu.verifiedMember.borrowedMovies)
                {
                    //find the movie in the movie record
                    if (movies.movieName == movie)
                    {
                        //remove that movie from the movie record
                        MemberMenu.verifiedMember.borrowedMovies.Remove(movies);
                        break;
                    }
                }
            }
            
        }

        /// <summary>
        /// After a staff member removes a movie from the movie collection,
        /// all instances of that movie is deleted from each member's movie record
        /// </summary>
        /// <param name="loanedMovieName"></param>
        public static void deleteLoanedMovies(string loanedMovie)
        {
            //for all members in the member collection
            foreach (Member member in MemberCollection.memColl)
            {
                if (member != null)
                {
                    //remove all instances of the given movie from all movie records
                    MemberMenu.verifiedMember = member;
                    returnMovie(loanedMovie);
                }
                
            }
        }
    }
}
