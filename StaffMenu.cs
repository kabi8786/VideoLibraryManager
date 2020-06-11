using System;
using System.Collections.Generic;
using System.Text;

namespace VideoLibraryManagement
{
    class StaffMenu
    {

        /// <summary>
        /// Display staff menu options
        /// </summary>
        public static void Menu()
        {
            Console.WriteLine("\n==========Staff Menu==========");
            Console.WriteLine("1. Add a new movie DVD");
            Console.WriteLine("2. Remove a movie DVD");
            Console.WriteLine("3. Register a new member");
            Console.WriteLine("4. Find a registered member's phone number");
            Console.WriteLine("0. Return to main menu");
            Console.WriteLine("==============================");
            Console.Write("Please make a selection (1 - 4 or 0 to return to main menu): ");
        }

        /// <summary>
        /// Verify staff login to access staff menu
        /// </summary>
        /// <returns></returns>
        public static bool Login()
        {
            bool verified = false;
            //staff username and password user input
            Console.Write("Enter staff username: ");
            string user = Console.ReadLine();

            Console.Write("Enter staff password: ");
            string password = Console.ReadLine();

            if (user != "staff") //incorrect username
            {
                Console.WriteLine("Incorrect username for staff login.");
                verified = false;
            }
            else if (password != "today123") //incorrect password
            {
                Console.WriteLine("Incorrect password for staff login.");
                verified = false;
            }
            else //both are true
            {
                verified = true;
            }

            return verified;
        }

        /// <summary>
        /// Staff menu option handling
        /// </summary>
        public static void menuInput()
        {
            int response = int.Parse(Console.ReadLine());

            switch (response)
            {
                //if 1 is entered - call the add movie function then recall staff menu functions
                case 1:
                    Console.WriteLine("\nAdd a new movie DVD...");
                    addMovie();
                    menuFunctions();
                    break;
                //if 2 is entered - call the delete movie function then recall staff menu functions
                case 2:
                    Console.WriteLine("\nRemove a movie DVD...");
                    deleteMovie();
                    menuFunctions();
                    break;
                //if 3 is entered - call the register member function then recall staff menu functions
                case 3:
                    Console.WriteLine("\nRegister a new member...");
                    registerMember();
                    menuFunctions();
                    break;
                //if 4 is entered - call the find member number functin then recall staff menu functions
                case 4:
                    Console.WriteLine("\nFind registered member's phone number...");
                    findMemberNumber();
                    menuFunctions();
                    break;
                //if any other number is entered, return to main menu
                default:
                    Console.WriteLine("\nReturning to main menu...");
                    MainMenu.mainMenu();
                    break;
            }
        }

        /// <summary>
        /// Staff menu functionality - displaying menu and menu handling
        /// </summary>
        public static void menuFunctions()
        {
            Menu();
            menuInput();
        }

        /// <summary>
        /// Add a new member to the member collection
        /// </summary>
        static void registerMember()
        {
            //Retrieve new member's first and last name
            Console.Write("Please enter member's first name: ");
            string memberFirstName = Console.ReadLine();
            Console.Write("Please enter member's last name: ");
            string memberLastName = Console.ReadLine();

            //Concat new member's full name together and check if the new member doesn't already exist in member collection
            string memberUserName = memberLastName + memberFirstName;
            bool foundDupe = MemberCollection.findDuplicateMember(memberUserName);

            //If duplicate member found, don't add to collection
            if (foundDupe == true)
            {
                //State member already exists in collection
                Console.WriteLine("{0} {1} is already registered",
                    memberFirstName, memberLastName);
            }
            else //If member doesn't exist yet
            {
                //ask additional information about new member
                Member newMem = new Member(memberFirstName, memberLastName);
                Console.Write("Please enter member's contact number: ");
                newMem.contactNum = Console.ReadLine();

                Console.Write("Please enter member's address: ");
                newMem.resAddress = Console.ReadLine();

                Console.Write("Please enter a 4 digit password: ");
                string pass = Console.ReadLine();
                
                //continue prompting for a valid member password
                while (MemberMenu.memberPassVerification(pass) == false)
                {
                    Console.Write("Invalid password. Please re-enter a 4 digit password: ");
                    pass = Console.ReadLine();
                }
                
                //once a valid password is entered, update password for member object
                newMem.password = Convert.ToInt32(pass);

                //add new member to member collection
                MemberCollection.addMember(newMem);
                Console.WriteLine("Successfully added {0} {1}", newMem.firstName, newMem.lastName);
            }
        }

        /// <summary>
        /// Retrieves member's phone number given their full name
        /// </summary>
        static void findMemberNumber()
        {
            Console.Write("Please enter member's first name: ");
            string fName = Console.ReadLine();
            Console.Write("Please enter member's last name: ");
            string lName = Console.ReadLine();

            MemberCollection.findNumber(fName, lName);

        }

        /// <summary>
        /// Add a new movie title into the movie collection
        /// </summary>
        static void addMovie()
        {
            //Retrieve a new movie title and determine whether the movie title already exists in movie collection
            Console.Write("Movie title: ");
            string movieTitle = Console.ReadLine();
            Movie duplicateMovie = MovieCollection.Search(movieTitle);

            //If duplicate movie is found, ask to add additional copies instead
            if (duplicateMovie != null)
            {
                Console.WriteLine("Duplicate movie title found in movie collection!");
                Console.Write("How many additional copies would you like to add? ");
                duplicateMovie.movieCopies += Convert.ToInt32(Console.ReadLine());
            }
            else //if new movie doesn't exist in movie collection yet
            {
                //Add new movie to BST
                MovieCollection.Add(movieTitle);

                //Find the new movie in the collection
                Movie currentMovie = MovieCollection.Search(movieTitle);

                //Ask for additional info about the movie and update movie properties
                Console.Write("Starring actor(s): ");
                currentMovie.starringActor = Console.ReadLine();
                Console.Write("Director: ");
                currentMovie.director = Console.ReadLine();
                Console.Write("Duration (Minutes): ");
                currentMovie.duration = Convert.ToInt32(Console.ReadLine());
                Console.Write("Release Date (Year): ");
                currentMovie.releaseDate = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n1. Drama\n2. Adventure\n3. Family\n4. Action" +
                    "\n5. Sci-Fi\n6. Comedy\n7. Animated\n8. Thriller\n9. Other");
                Console.Write("Genre (1 - 9): ");
                currentMovie.movieGenre = (Movie.Genre)int.Parse(Console.ReadLine());
                Console.WriteLine("\n1. General (G)\n2. Parental Guidance (PG)" +
                    "\n3. Mature (M15+)\n4. Mature Accompanied (MA15+)");
                Console.Write("Classification (1 - 4): ");
                currentMovie.movieRating = (Movie.Classification)int.Parse(Console.ReadLine());

                Console.Write("Available copies: ");
                currentMovie.movieCopies = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("{0} has been added to the movie collection...", currentMovie.movieName);
            }
        }

        /// <summary>
        /// Removes the given movie from the BST
        /// Also deletes the all copies of the movie from every member's movie record
        /// </summary>
        static void deleteMovie()
        {
            //Retrieve movie title to remove and check if that movie exists in the movie collection
            Console.Write("Enter a movie title to remove from the collection: ");
            string removeMovieTitle = Console.ReadLine();
            Movie removedMovie = MovieCollection.Search(removeMovieTitle);


            //Couldn't find the intended movie to delete
            if (removedMovie == null)
            {
                Console.WriteLine("{0} does not exist in collection to delete...", removeMovieTitle);
            }
            else //if the movie could be found
            {
                //remove all instances of that movie from all member movie collections
                MemberMovies.deleteLoanedMovies(removeMovieTitle);
                //remove movie from main movie collection
                MovieCollection.Delete(removedMovie);
                //remove movie from stored movie array
                MovieCollection.removeMovie(removedMovie);
                Console.WriteLine("You have successfully removed {0}...", removeMovieTitle);
                
            }
        }
    }
}
