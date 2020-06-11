using System;
using System.Collections.Generic;
using System.Text;

namespace VideoLibraryManagement
{
    class MemberMenu
    {
        //stores the last verified member who successfully logins
        public static Member verifiedMember = null;
        /// <summary>
        /// Display member menu options
        /// </summary>
        public static void Menu()
        {
            Console.WriteLine("\n========Member Menu===========");
            Console.WriteLine("1. Display all movies"); //traversal of BST
            Console.WriteLine("2. Borrow a movie DVD");
            Console.WriteLine("3. Return a movie DVD");
            Console.WriteLine("4. List current borrowed movie DVDs");
            Console.WriteLine("5. Display top 10 most popular movies");
            Console.WriteLine("0. Return to main menu");
            Console.WriteLine("==============================");
            Console.Write("Please make a selection (1 - 5 or 0 to return to main menu): ");
        }

        /// <summary>
        /// Member login verification
        /// </summary>
        public static void memberLogin()
        {
            //Retrieve input for username and password
            Console.Write("Please enter member's username (LastnameFirstname): ");
            string username = Console.ReadLine();
            Console.Write("Please enter member's password (4 digits): ");
            string pass = Console.ReadLine();

            //if a valid password was given
            if (memberPassVerification(pass) == true){

                //Find a member that contains same login credentials
                verifiedMember = MemberCollection.findDuplicateMember(username, Convert.ToInt32(pass));

                //if the member could be found, display the member menu options and welcome user
                if (verifiedMember != null)
                {
                    Console.WriteLine("Welcome {0}...", verifiedMember.firstName);
                    menuFunctions();
                }
                else
                {
                    // if user credentials don't match, return to menu
                    Console.WriteLine("Could not verify user...");
                    Console.WriteLine("Returning to main menu...");
                    MainMenu.mainMenu();
                }
            }
            else
            {//if an invalid password was entered, return to menu
                Console.WriteLine("Invalid password entered...");
                Console.WriteLine("Returning to main menu...");
                MainMenu.mainMenu();
            }
            

        }
        /// <summary>
        /// Verifies that a given password is of valid length and type
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static bool memberPassVerification(string pass)
        {
            bool validPass = true;
            //if the given password isn't of length 4 or not an int
            if (pass.Length != 4 || !int.TryParse(pass, out int password))
            {
                validPass = false;
            }
            return validPass;
        }
        
        /// <summary>
        /// Member menu input handling
        /// </summary>
        static void menuInput()
        {
            int response = int.Parse(Console.ReadLine());

            switch (response)
            {
                case 1:
                    Console.WriteLine("\nDisplaying all movies...");
                    MovieCollection.inOrder();
                    menuFunctions();
                    break;
                case 2:
                    Console.WriteLine("\nBorrow a movie DVD...");
                    borrowMovie();
                    menuFunctions();
                    break;
                case 3:
                    Console.WriteLine("\nReturn a movie DVD...");
                    returnBorrowedMovie();
                    menuFunctions();
                    break;
                case 4:
                    Console.WriteLine("\nCurrent borrowed movie DVDs...");
                    MemberMovies.showBorrowedMovies();
                    menuFunctions();
                    break;
                case 5:
                    Console.WriteLine("\nTop 10 most popular movies...");
                    MovieCollection.top10Borrowed();
                    menuFunctions();
                    break;
                default:
                    Console.WriteLine("\nReturning to menu...");
                    MainMenu.mainMenu();
                    break;
            }

        }

        
        public static void menuFunctions()
        {
            Menu();
            menuInput();
        }

        /// <summary>
        /// Allows a member to borrow a movie from the movie collection and updates their movie records
        /// </summary>
        public static void borrowMovie()
        {
            Console.Write("What movie would you like to borrow? ");
            string borrowMovieTitle = Console.ReadLine();

            //determine if movie has been previously borrowed by current member
            if (MemberMovies.checkBorrowed(borrowMovieTitle))
            {
                Console.WriteLine("You have already borrowed {0}...", borrowMovieTitle);
            }
            //if member is about to exceed number of loaned movies limit
            else if (verifiedMember.borrowedMovies.Count == 10)
            {
                Console.WriteLine("You already have 10 movies borrowed. Cannot borrow anymore movies.");
            }
            else //if movie hasn't been borrowed by this member
            {
                //attempt to find the given movie title in the collection
                Movie loanedMovie = MovieCollection.Search(borrowMovieTitle);

                //given movie couldn't be found
                if (loanedMovie == null)
                {
                    Console.WriteLine("{0} does not exist in library...", borrowMovieTitle);
                }
                //no more copies available to loan out
                else if (loanedMovie.movieCopies == 0)
                {
                    Console.WriteLine("Sorry, there are no more copies of {0}...", borrowMovieTitle);
                }
                else //movie could be found - remove a copy from the movie collection and add to member's movie record
                {
                    loanedMovie.movieCopies--;
                    loanedMovie.timesRented++;
                    MemberMovies.addMovie(loanedMovie);
                }
                                    
            }
        }


        /// <summary>
        /// Allows a verified member to return a movie from their movie records
        /// </summary>
        public static void returnBorrowedMovie()
        {
            Console.Write("What movie would you like to return? ");
            string loanedMovieTitle = Console.ReadLine();

            //check if the member has borrowed the movie
            if (MemberMovies.checkBorrowed(loanedMovieTitle))
            {
                //remove the loaned movie from the member's movie record
                MemberMovies.returnMovie(loanedMovieTitle);
                Console.WriteLine("You have successfully returned {0}", loanedMovieTitle);

                //Return a copy of the movie to the movie collection (if movie hasn't been deleted from movie collection)
                Movie returnedMovie = MovieCollection.Search(loanedMovieTitle);
                if (returnedMovie != null)
                {
                    returnedMovie.movieCopies++;
                }
            }
            else //given movie title doesn't exist in the member's movie record
            {
                Console.WriteLine("You don't have the movie {0} to return...", loanedMovieTitle);
            }
            
        }
    }
}
