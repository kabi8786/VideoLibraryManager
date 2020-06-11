using System;

namespace VideoLibraryManagement
{
    class MainMenu
    {
        static void Main(string[] args)
        {
            bool runProgram = true;

            //displays main menu
            mainMenu();

            //main program loop - ends once '0' is entered 
            //from the main menu
            while (runProgram)
            {
                if (mainInput() == true)
                {
                    runProgram = false;
                    break;
                }
            }
        }

        /// <summary>
        /// Main menu output on command line
        /// </summary>
        public static void mainMenu()
        {
            Console.WriteLine("\nWelcome to the Community Library");
            Console.WriteLine("==========Main Menu==========");
            Console.WriteLine("1. Staff Login");
            Console.WriteLine("2. Member Login");
            Console.WriteLine("0. Exit");
            Console.WriteLine("=============================");
            Console.Write("\nPlease make a selection (1 - 2, or 0 to exit): ");
        }

        /// <summary>
        /// Handling menu option inputs for staff and member login
        /// </summary>
        /// <returns></returns>
        static bool mainInput()
        {
            bool endProgram = false;
            int response = int.Parse(Console.ReadLine());

            switch (response)
            {
                case 1:
                    Console.WriteLine("\nStaff verification...");
                    //staff input and verification
                    if (StaffMenu.Login() == true)
                    {
                        Console.WriteLine("Successful Login!\nLoading staff menu...");
                        StaffMenu.menuFunctions();
                    }
                    else
                    {
                        //return to main menu
                        Console.WriteLine("\nReturning to main menu...");
                        mainMenu();
                    }
                    break;
                case 2:
                    Console.WriteLine("\nMember verification...");
                    //member input and verification
                    MemberMenu.memberLogin();
                    break;
                default:
                    Console.WriteLine("\nEnd Program...");
                    endProgram = true;
                    break;
            }//end switch

            return endProgram;
        }//end MainInput
    }
}
