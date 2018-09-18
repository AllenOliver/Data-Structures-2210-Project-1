///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  DataStructuresProject1
//	File Name:         Project1Driver.cs
//	Description:       Main Program Driver
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Allen Oliver
//	Created:           Thursday, September 6, 2018
//	Copyright:         Allen Oliver, 2018
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace DataStructuresProject1
{
    class Project1Driver
    {
        [STAThread]
        static void Main(string[] args)
        {

            //Add close window event to trigger the goodbye message.
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CloseMessageEvent);
            
            //create new menu
            ProjectMenu menu = new ProjectMenu("Project 1: Strings, Files, and Lists | Allen Oliver");
            
            //Add menu choices
            menu.AddToMenu(menu, "Open A File");
            menu.AddToMenu(menu, "Display a text file");
            menu.AddToMenu(menu, "Exit");

            Choices choice = (Choices)menu.GetUserChoice();
            while (choice != Choices.Exit)
            {
                switch (choice)
                {

                    #region Open File Choice
                    case Choices.OpenFile:
                        List<string> listFromFile = new List<string>();
                        Tools.PadString("Here is your file parsed!", 10, 10);
                        Tools.PadString("========================", 10, 10);

                        //open file and stuff into string
                        string file = Tools.OpenFileFromDisk();
                        Tools.CleanString(ref file);
                        listFromFile = Tools.Parse(file, Tools.delims);
                        Tools.OutputList(listFromFile);

                        //wait
                        Console.WriteLine("Press any key to format the document.");
                        Console.WriteLine();
                        Console.ReadKey();

                        Tools.Format(listFromFile, 10, 75);
                        Tools.PadString("Press any key to return to menu...", 10, 10);

                        Console.ReadKey();
                        Console.Clear();
                        break;
                    #endregion

                    #region Display File Choice
                    case Choices.ClearMenu:
                        //get file form disk
                        string displayFile = Tools.OpenFileFromDisk();
                        //do some outputs
                        Console.WriteLine("Text file:");
                        Console.WriteLine();

                        Tools.PadString(displayFile, 10, 75);
                        Console.WriteLine();
                        Tools.PadString("Press any key to return to menu...", 10, 10);

                        Console.ReadKey();
                        Console.Clear();

                        break;
                    #endregion

                    #region Exit Program
                    case Choices.Exit:
                        Console.WriteLine("You selected Close");
                        Console.ReadKey();
                        Console.Clear();
                        break; 
                    #endregion
                }  
                choice = (Choices)menu.GetUserChoice();
            }  
        }

        /// <summary>
        /// Event to  call the goodbye message from tools class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        static void CloseMessageEvent(object sender, EventArgs e)
        {
            Tools.GoodbyeMessage("Goodbye!");
        }
    }
}
