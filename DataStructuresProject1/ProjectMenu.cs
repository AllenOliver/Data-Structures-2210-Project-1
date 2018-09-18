///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  DataStructuresProject1
//	File Name:         ProjectMenu.cs
//	Description:       Menu class that validates input from user
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Allen Oliver
//	Created:           Saturday, September 8, 2018
//	Copyright:         Allen Oliver, 2018
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;


namespace DataStructuresProject1
{
    class ProjectMenu
    {
        #region Properties

        private List<string> MenuItems = new List<string>();
        public string Title {get;set;}

        #endregion

        #region Constructor

        public ProjectMenu(string title)
        {
            this.Title = title;
            Tools.Setup();
        } 

        #endregion

        #region Add To Menu

        /// <summary>
        /// Plus Operator to add items to the menu.
        /// </summary>
        /// <param name="menu">The menu to add to.</param>
        /// <param name="item">The item to be added.</param>
        /// <returns>
        /// The Final Menu
        /// </returns>
        public void AddToMenu(ProjectMenu menu, string item)
        {
            menu.MenuItems.Add(item);

        }

        #endregion

        #region User Input and Validation

        public void ShowChoices()
        {
            Console.Clear();
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Tools.PadString(Title, 10, 10);
            Console.Write("\t");
            for (int j = 0; j < Title.Length; j++ )
            {
                Console.Write("-");
            }
            Console.WriteLine("\n");
            int i = 1;
            foreach (var item in MenuItems)
            {
                Tools.PadString(string.Format("{0}.)  {1}", i, item), 10, 10);
                i++;
            }
        }
        /// <summary>
        /// Gets the user choice.
        /// </summary>
        /// <returns></returns>
        public int GetUserChoice()
        {
            int choice = -1;
            string cin;

            while (true)
            {
                ShowChoices();
                cin = Console.ReadLine();
                if (!int.TryParse(cin, out choice))
                {
                    Console.WriteLine("That is not a number, please enter a number between 1 and {0}", MenuItems.Count);
                }
                else
                {
                    if (choice > MenuItems.Count)
                    {
                        Console.WriteLine("That item is larger than {0}. Please Try Again.", MenuItems.Count);
                    }
                    else
                    {
                        Console.Clear();
                        return choice;
                    }
                }
            }
        }

        #endregion
    }
}
