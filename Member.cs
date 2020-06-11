using System;
using System.Collections.Generic;
using System.Text;

namespace VideoLibraryManagement
{
    class Member
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string resAddress { get; set; }
        public string userName { get; set; } //used to verify a member for login
        public int password { get; set; }
        public string contactNum { get; set; }

        public List<Movie> borrowedMovies = new List<Movie>(10); //stores borrowed movies
        
        public Member(string fName, string lName)
        {
            this.firstName = fName;
            this.lastName = lName;
            //username is a concatenation of a member's last and first name
            this.userName = lName + fName;
        }
    }
}
