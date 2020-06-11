using System;
using System.Collections.Generic;
using System.Text;

namespace VideoLibraryManagement
{
    class MemberCollection
    {
        public static Member[] memColl; //stores all registered members
        private const int MAXMEMBERS = 10; 
        static int numRegisteredMembers = 0; //increments with each member added

        /// <summary>
        /// Constructor: initialise size of Member Collection 
        /// </summary>
        static MemberCollection()
        {
            memColl = new Member[MAXMEMBERS];
        }

        /// <summary>
        /// Adds a new member to the member collection
        /// </summary>
        /// <param name="member"></param>
        public static void addMember(Member member)
        {
            //if member collection is full, don't add new member to member collection
            if (numRegisteredMembers == MAXMEMBERS)
            {
                Console.WriteLine("Member collection is full. Cannot add more members...");

            }
            else //else, add new member to Member collection and increment number of registered members
            {
                memColl[numRegisteredMembers] = member;
                numRegisteredMembers++;
            }
        }

        /// <summary>
        /// When registered a member, checks if that member already
        /// exists in the member collection
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool findDuplicateMember(string userName)
        {
            bool foundDuplicateMember = false;

            //iterate through memColl
            foreach (Member registeredMembers in memColl)
            {
                //if a registered member has the same username as given, a duplicate member is found
                if (registeredMembers != null)
                {
                    if (registeredMembers.userName == userName)
                    {
                        foundDuplicateMember = true;
                    }
                }
                
            }
            return foundDuplicateMember;
        }

        /// <summary>
        /// Given a registered member's first and last name, 
        /// return their contact number
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Last"></param>
        public static void findNumber(string First, string Last)
        {
            bool foundNumber = false;
            //iterate through memColl
            for (int i = 0; i < numRegisteredMembers; i++)
            {
                if (memColl[i] != null)
                {
                    //if given values of first and last name matches an existing
                    //member's first and last name in the collection
                    if (memColl[i].firstName == First &&
                        memColl[i].lastName == Last)
                    {
                        //display the contact number of the found member
                        Console.WriteLine("Phone number for {0} {1} was found to be: {2}",
                            First, Last, memColl[i].contactNum);
                        foundNumber = true;
                        break; //stop searching
                    }
                }
                
            }
            //if a member could not be found after iterating through collection
            if (foundNumber == false)
            {
                Console.WriteLine("Phone number for {0} {1} could not be found...", First, Last);
            }
        }

        /// <summary>
        /// Returns a member that matches the given username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Member findDuplicateMember(string username, int password)
        {
            Member foundMember = null;
            //iterate through the collection
            foreach (Member member in memColl)
            {
                if (member != null)
                {
                    //if member's login credentials match given credentials
                    if (member.userName == username && member.password == password)
                    {
                        //return this particular member
                        foundMember = member;
                    }
                }
            }
            return foundMember;
        }
    }
}
