///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  DataStructuresProject1
//	File Name:         Tools.cs
//	Description:       Utility class with various methods to execute in the driver.
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Allen Oliver
//	Created:           Thursday, September 6, 2018
//	Copyright:         Allen Oliver, 2018
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataStructuresProject1
{
    static class Tools
    {
        //delimeters to check for 
        public static string delims = " ,.?!-;:{}[]()$'\n''\r'";
        

        #region MessageBoxes

        /// <summary>
        /// Shows a welcoming MessageBox to the user.
        /// </summary>
        /// <param name="message">The welcome message for the MessageBox.</param>
        /// <param name="caption">The caption for the MessageBox..</param>
        /// <param name="author">The author of the project.</param>
        public static void WelcomeMessage(String message, String caption, String author)
        {
            MessageBox.Show(message + "\n" + author, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows a goodbye MessageBox to the user.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        public static void GoodbyeMessage(String message)
        {
            MessageBox.Show(message, "Goodbye!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Cosmetics

        /// <summary>
        /// Sets up the console with a color, title, and various other cosmetic properties.
        /// </summary>
        public static void Setup()
        {
            Console.Title = "Project 1: Strings, Files, and Lists | Allen Oliver ";
            Console.Clear();
            //Set up colors
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            
            //show welcome message on start up.
            WelcomeMessage("Welcome to project one!", "Welcome!", "Allen Oliver");
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Outputs the list.
        /// </summary>
        /// <param name="listToOutput">The list to output.</param>
        /// <returns></returns>
        public static void OutputList(List<string>listToOutput)
        {
            var count = 1;
            foreach (var index in listToOutput)
            {
                Console.WriteLine($"{count}. {index}");
                count++;
            }
        }

        /// <summary>
        /// Cleans the input string.
        /// </summary>
        /// <param name="work">The string to be formatted.</param>
        /// <returns>The cleaned string</returns>
        public static string CleanString(ref string work)
        {
            //trim the white space from the string
            work.Trim();
            //replace unwanted new lines with desireable ones
            work.Replace("\r\n", "\n");
            return work;
        }

        /// <summary>
        /// Parses the specified original string on the delimiters supplied.
        /// </summary>
        /// <param name="original">The original string to be parsed.</param>
        /// <param name="delimiters">The delimiters to parse on.</param>
        /// <returns> a list of parsed strings including delimiters</returns>
        public static List<string> Parse(string original, string delimiters)
        {
            //set up local variables
            List<string> tokens = new List<string>();

            //check for empty string
            if (!string.IsNullOrEmpty(original))
            {
                
                int index = 0;
                do
                {
                    int delimIndex = original.IndexOfAny(delims.ToCharArray(), index);
                    if (delimIndex >= 0)
                    {
                        if (delimIndex > index)
                            //part before the delimiter
                            tokens.Add(original.Substring(index, delimIndex - index)); 
                        //the delimiter
                        tokens.Add(new string(original[delimIndex], 1));
                        index = delimIndex + 1;
                        continue;
                    }

                    //No delimiters were found, but at least one character remains. Add the rest and stop.
                    tokens.Add(original.Substring(index, original.Length - index));
                    //exit from loop
                    break;

                } while (index < original.Length);
            }
            else
            {
                Console.WriteLine("There is no string to parse!");
            }

            return tokens;
        }

        /// <summary>
        /// Formats the specified list with padding.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="leftMargin">The left margin of the console.</param>
        /// <param name="rightMargin">The right margin of the console.</param>
        /// <returns></returns>
        public static string Format(List<string> list, int leftMargin, int rightMargin)
        {
            //local scoped variables
            StringBuilder sb = new StringBuilder();
            string fullListString = "";

            //add full list of delimiters and words to a string
            foreach (string listItem in list)
            {
                fullListString += listItem;
            }
            
            //split to string array
            string[] sentence = fullListString.Split(' ');
            //empty string
            string line = "";
            foreach (string word in sentence)
            {
                //check that each string when added to empty string isn't longer than the right margin
                if ((line + word).Length > rightMargin)
                {
                    //add to the string builder
                    sb.AppendLine(line.PadLeft(leftMargin + line.Length, ' '));
                    line = "";
                }
                //add delimeter to the empty string
                line += $"{word} ";
            }

            //if not empty, add the empty string to the string builder
            if (line.Length > 0)
                sb.AppendLine(line.PadLeft(leftMargin+line.Length,' '));

            //output to the console 
            Console.WriteLine(sb.ToString().PadLeft(leftMargin + line.Length, ' '));
            return "";
        }

        /// <summary>
        /// Formats the specified string with padding.
        /// </summary>
        /// <param name="stringToPad">The string to format.</param>
        /// <param name="leftMargin">The left margin.</param>
        /// <param name="rightMargin">The right margin.</param>
        /// <returns></returns>
        public static string PadString(string stringToPad, int leftMargin, int rightMargin)
        {
            Console.WriteLine("\t {0}",stringToPad);
            return "";
        }

        #endregion

        #region OpenFile

        /// <summary>
        /// Opens the file from disk.
        /// </summary>
        /// <returns></returns>
        public static string OpenFileFromDisk()
        {
            //local variables
            List<string> tokens = new List<string>();
            string input="";

            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                //add some filters
                openFile.Filter = "Text Files|*.txt;*.text|All Files|*.*";
                openFile.InitialDirectory = @"..\..\TestData";
                openFile.Title = "Open a file!";
                //if the user doesn't cancel
                if (DialogResult.Cancel != openFile.ShowDialog())
                {
                    StreamReader sr = new StreamReader(openFile.FileName);

                    try
                    { 

                        while (sr.Peek() != -1)
                        {
                            //read whole file
                            input = sr.ReadToEnd();
                        }
                        return input;
                    }
                    catch (Exception)
                    {
                        throw new Exception("There was an error reading the file you selected");
                    }
                    finally
                    {
                        if (sr != null)
                        {
                            //close reader
                            sr.Close();
                        }
                    }
                }
                return "Nothing!";
            }
            catch (Exception)
            {
                throw new Exception("There was an error opening the file you selected");
            }
        }
        #endregion
    }
}
