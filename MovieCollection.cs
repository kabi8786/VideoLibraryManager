using System;
using System.Collections.Generic;
using System.Text;

namespace VideoLibraryManagement
{
    class MovieCollection
    {
        public static Movie Root { get; set; }
        static int totalMovies; //keeps track of how many movies are added to the movie colleciton
        public static Movie[] storedMovies = new Movie[50]; //stores a copy of all movies added to the movie collection

        /// <summary>
        /// Constructor: setting root of BST to null
        /// </summary>
        static MovieCollection()
        {
            Root = null;
        }

        /// <summary>
        /// Calls the recursive function findMovie
        /// Returns the resulting movie
        /// </summary>
        /// <param name="movieName"></param>
        /// <returns></returns>
        public static Movie Search(string movieName)
        {
            return findMovie(movieName, Root);
        }

        /// <summary>
        /// Finds a movie that shares the same given movieName
        /// from the BST
        /// </summary>
        /// <param name="movieName"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private static Movie findMovie(string movieName, Movie parent)
        {
            //if the current node isn't empty
            if (parent != null)
            {
                //check if the current node's movie name matches
                if (movieName == parent.movieName)
                {
                    //if true, return the current node
                    return parent;
                }
                //if given movieName alphabetically comes before current movie's name
                else if (string.Compare(movieName, parent.movieName, StringComparison.OrdinalIgnoreCase) == -1)
                {
                    //search through the left side of the BST
                    return findMovie(movieName, parent.leftMovie);
                }
                else //if given movieName alphabetically comes after current movie's name
                {
                    //search through the right side of the BST
                    return findMovie(movieName, parent.rightMovie);
                }
            }
            return null;
        }

        /// <summary>
        /// Calls the recursive function insertMovie
        /// The value of Root is updated with each recursion
        /// </summary>
        /// <param name="newMovieTitle"></param>
        public static void Add(string newMovieTitle)
        {
            Root = insertMovie(Root, newMovieTitle);
        }

        /// <summary>
        /// Determines where the newMovie should be placed in the BST
        /// Each movie successfully added to the collection is also added to the storedMovies array
        /// </summary>
        /// <param name="root"></param>
        /// <param name="newMovieTitle"></param>
        /// <returns></returns>
        static Movie insertMovie(Movie root, string newMovieTitle)
        {
            //if the current BST root is empty
            if (root == null)
            {
                //create a new movie node
                root = new Movie(newMovieTitle);
                //add that movie to the stored movies array
                storedMovies[totalMovies] = root;
                //increment total movies
                totalMovies++;
                return root;
            }

            //if duplicate movie object is found that shares same name as newMovieTitle
            if (newMovieTitle == root.movieName)
            { //ask for additional copies
                Console.WriteLine("Duplicate movie title found!");
                Console.Write("How many more copies of {0} would you like to add? ");
                int additionalCopies = int.Parse(Console.ReadLine());
                //update movie copies of the found duplicate movie
                root.movieCopies += additionalCopies;
                return root;
            }

            /*Continue traversing down the tree*/

            //if the new movie alphabetically comes before current movie node's title
            if (string.Compare(newMovieTitle, root.movieName, StringComparison.OrdinalIgnoreCase) < 0)
            {
                //attempt to add the new movie on the left branch of the current movie node
                root.leftMovie = insertMovie(root.leftMovie, newMovieTitle);
            }
            //if the new movie alphabetically comes after the current movie node's title
            else if (string.Compare(newMovieTitle, root.movieName, StringComparison.OrdinalIgnoreCase) > 0)
            {
                //attempt to add the new movie on the right branch of the current movie node
                root.rightMovie = insertMovie(root.rightMovie, newMovieTitle);
            }
            return root;

        }

        /// <summary>
        /// Calls the recursive function deleteMovie
        /// </summary>
        /// <param name="movieName"></param>
        public static void Delete(Movie removeMovie)
        {
            Root = deleteMovie(Root, removeMovie);
        }

        /// <summary>
        /// Finds the movie to delete
        /// </summary>
        /// <param name="root"></param>
        /// <param name="movieName"></param>
        /// <returns></returns>
        static Movie deleteMovie(Movie root, Movie removeMovie)
        {
            /*Base case*/
            if (root == null)
            {
                return root;
            }

            /*Not base case, continue traversing down the tree*/
            if (string.Compare(removeMovie.movieName, root.movieName, StringComparison.OrdinalIgnoreCase) < 0)
            {
                root.leftMovie = deleteMovie(root.leftMovie, removeMovie);
            }
            else if (string.Compare(removeMovie.movieName, root.movieName, StringComparison.OrdinalIgnoreCase) > 0)
            {
                root.rightMovie = deleteMovie(root.rightMovie, removeMovie);
            }
            else
            {
                /*Current movie has either 1 child or no child*/

                //if the current root's left child is empty, return right child
                if (root.leftMovie == null)
                {
                    return root.rightMovie;
                }
                //if the current root's right child is empty, return left child
                else if (root.rightMovie == null)
                {
                    return root.leftMovie;
                }

                /*Node with 2 children - Replace the current node with its inorder successor*/
                root.movieName = inOrderSuccessor(root.rightMovie);
                //delete the original reference of the inorder successor from the BST
                root.rightMovie = deleteMovie(root.rightMovie, root);
            }
            return root;
        }

        /// <summary>
        /// Returns the inorder successor of the given root
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        static string inOrderSuccessor(Movie root)
        {
            string current = root.movieName;

            while (root.leftMovie != null)
            {
                //store the left child's name
                current = root.leftMovie.movieName;
                //replace the root with its left child
                root = root.leftMovie;
            }
            return current;
        }

        /// <summary>
        /// Calls the recursive function TraverseInOrder
        /// </summary>
        public static void inOrder()
        {
            TraverseInOrder(Root);
        }

        /// <summary>
        /// Traverses through the BST and displays details about non-null movie objects
        /// </summary>
        /// <param name="root"></param>
        static void TraverseInOrder(Movie root)
        {
            if (root != null)
            {
                //Attempt to traverse the leftMovie reference - if null, doesn't display details
                TraverseInOrder(root.leftMovie);
                //Display details about the current node
                Console.WriteLine("\nMovie name: {0}", root.movieName);
                Console.WriteLine("Starring actor(s): {0}", root.starringActor);
                Console.WriteLine("Director: {0}", root.director);
                Console.WriteLine("Duration (Hours): {0}", root.duration);
                Console.WriteLine("Release date (Year): {0}", root.releaseDate);
                Console.WriteLine("Genre: {0}", root.movieGenre);
                Console.WriteLine("Classification: {0}", root.movieRating);
                Console.WriteLine("Available copies: {0}", root.movieCopies);
                Console.WriteLine("Times borrowed: {0}", root.timesRented);
                //Attempt to traverse the rightMovie reference - if null, doesn't display details
                TraverseInOrder(root.rightMovie);
            }
        }

        /// <summary>
        /// Removes given movieTitle from the storedMovies array
        /// </summary>
        /// <param name="movieTitle"></param>
        /// <param name="totalMovies"></param>
        /// <param name="array"></param>
        public static void removeMovie(Movie removedMovie)
        {
            int removeMovieIndex;
            //find the index for the movie we want to remove
            for (removeMovieIndex = 0; removeMovieIndex < totalMovies; removeMovieIndex++)
            {
                if (storedMovies[removeMovieIndex] == removedMovie)
                {
                    break; //stop searching
                }
            }

            if (removeMovieIndex < totalMovies)
            {
                //decrease total movies
                totalMovies--;
                for (int j = removeMovieIndex; j < totalMovies; j++)
                {
                    //shift elements that occurred after removedMovie Index
                    storedMovies[j] = storedMovies[j + 1];
                    storedMovies[j + 1] = null; //make original copy of storedMovies[j + 1] null
                }
            }
        }

        /// <summary>
        /// Calls helper functions to display top 10 borrowed movies
        /// </summary>
        public static void top10Borrowed()
        {
            insertionSort(); 
            showtop10();
        }

        /// <summary>
        /// Iterates and only display the first 10 elements of the sorted storedMovies array
        /// </summary>
        public static void showtop10()
        {
            for (int i = 0; i < 10; i++)
            {
                if (storedMovies[i] != null)
                {
                    Console.WriteLine("\n{0}. {1}", i + 1, storedMovies[i].movieName);
                    Console.WriteLine("Times rented: {0}", storedMovies[i].timesRented);
                }
            }
     
        }

        /// <summary>
        /// Sorts storedMovies array in descending order by timesRented value using insertion sort
        /// </summary>
        static void insertionSort()
        {
            Movie temp;
            int j;
            //start from 2nd element
            for (int i = 1; i < totalMovies; i++)
            {
                //current movie to compare to sorted part of array
                temp = storedMovies[i];
                //last index of sorted part of array
                j = i - 1;
                //while the current movie to compare is larger than previously sorted
                //movies
                while (j >= 0 && storedMovies[j].timesRented < temp.timesRented)
                {
                    //swap positions
                    storedMovies[j + 1] = storedMovies[j];
                    j--;
                }
                //if the current movie was smaller than one of the sorted movies, 
                //the current movie is placed after that element
                storedMovies[j + 1] = temp;
            }
        }
        
        
    }
}
