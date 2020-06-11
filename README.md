# VidMan

## Main Menu
- `mainMenu()` outputs main menu options in command line. 
- `mainInput()` handles given user input for staff and member menus. Entering `1 or 2` in console requires additional successful verification to access the menu. Entering `0` ends the program. 

## Member
- Creates a member given their first and last name. 
- Able to store their phone number, residential address, username and password (a 4 digit pin used for verification to the member menu). 
- Members are able to borrow up to 10 unique movies at once. 

## Movie 
- Creates a movie given their title. As movies are stored in a BST, left and right movies are initialised as null. 
- Able to store additional information concerning:
    - starring actors
    - movie director
    - year released
    - duration of movie (minutes)
    - number of copies available for renting
    - genre (Drama, Adventure, Family, Action, SciFi, Comedy, Animated, Thriller, Other)
    - classification (G, PG, M, MA)
    - total number of times movie DVDs have been borrowed by members
    
## Member Collection
- `addMember(Member)` adds a new member to the `member collection`.
- `findDuplicateMember(string)` checks whether a member already exists in the `member collection`.
- `findNumber(string, string)` returns a member's registered phone number given the member's full name.
- `findDuplicateMember(string, int)` returns a member that has the same login credentials as given username and password.

## Movie Collection
- `Search(string)` and `findMovie(string, Movie)` recursively traverses BST to find and return the given movie object.
- `Add(string)` and `insertMovie(Movie, string)` recursively traverses BST and attempts to insert the given movie title into the `movie collection`. If successful, adds movie object to `stored movies` array also.
- `Delete(Movie)` and `deleteMovie(Movie, Movie)` recursively finds the movie to delete and accounts for different 'delete cases' commonly encountered in BST. `inOrderSuccessor(Movie)` is used to return the successor that replaces the deleted movie in case 3 (node with 2 children).
- `inOrder()` and `TraverseInOrder(Movie)` outputs all the movies and their details stored in the collection alphabetically.
- `removeMovie(Movie)` removes the given movie object from the `stored movies` array
- `top10Borrowed()` calls `insertionSort()` to sort `stored movies` array by total times borrowed in descending order. 


## Member Movies
- `checkBorrowed(string)` checks whether the given movie title has already been loaned by the current verified member.
- `addMovie(Movie)` adds a movie to the member's `borrowed movies` record.
- `showBorrowedMovies()`outputs all movies rented out by current verified member
- `returnMovie(string)` removes given movie title from `borrowed movies` record and "returns" movie back to `movie collection`.
- `deleteLoanedMovies(string)` iterates through `member collection` and removes given movie title from all member's `borrowed movie` records.

## Staff Menu
- `Login()` validates inputs from console for staff username and password. 
- `registerMember()` attempts to add a new member to the `member collection`. After full name of member is given, determine if they are already in the `member collection`. If they are not in the collection, additional information about the member and their password is asked for. 
- `findMemberNumber()` prompts for member full name to obtain their registered phone number.
- `addMovie()` attempts to add a new movie title to the `movie collection`. After a movie title is given, determine if the movie already exists. If so, prompt for additional copies to add for that movie. Otherwise, add the movie to the BST and ask for additional information about the movie. 
- `deleteMovie()` searches and deletes movies from all member's `borrowed movie` records, and the `movie collection`.

## Member Menu
- `borrowMovie()` asks currently verified member to rent a movie they are not currently borrowing.
- `returnBorrowedMovie()` asks currently verified member which movie from their `borrowed movie` record that they wish to return to the `movie collection`
