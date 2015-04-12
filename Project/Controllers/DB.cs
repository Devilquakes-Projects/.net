// Auther: Joren Martens
// Date: 30/03/2015 16:12
// resources 
//      @ http://stackoverflow.com/questions/2837020/open-existing-file-append-a-single-line
//      @ https://msdn.microsoft.com/en-us/library/vstudio/cc148994%28v=vs.100%29.aspx
//      @ Course .net
//
// Alle messageboxes moeten nog aangepast worden met een error handler
// Probeer niet iets te doen dat niet kan, heb niet overal een messagebox gezet dus het kan zijn
// dat het programma dan crasht. als er iets echt mis is contacteer mij dan.
// Daarna kan je System.windows deleten

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project.Controllers
{
    public static class DB
    {
        private static string destination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "01DBProject");

        /// <summary>
        /// Creates a new database with fields.
        /// </summary>
        /// <param name="fields">Array of fields you want to make.</param>
        public static void MakeDB(string dbName, string[] fields)
        {
            if (dbName == null)
            {
                throw new ArgumentNullException("database name");
            }
            if (fields == null)
            {
                throw new ArgumentNullException("fields");
            }

            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "01DBProject"));
            string newFile = Path.Combine(destination, String.Format("{0}.txt", dbName.ToUpper()));

            if (File.Exists(newFile))
            {
                Console.WriteLine("DB already exists.");
            }
            else
            {

                StreamWriter outputStream = File.CreateText(newFile);
                outputStream.Write("ID");
                outputStream.Write("|VISIBLE");
                foreach (string field in fields)
                {
                    outputStream.Write(String.Format("|{0}", field.ToUpper()));
                }
                outputStream.WriteLine();
                outputStream.Close();

            }
        }

        /// <summary>
        /// Deletes a database
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        public static void DeleteDb(string dbName)
        {
            if (dbName == null)
            {
                throw new ArgumentNullException("database name");
            }

            string file = Path.Combine(destination, String.Format("{0}.txt", dbName.ToUpper()));
            File.Delete(file);
        }

        /// <summary>
        /// Displays the fields of the database.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        public static string[] GetStructure(string dbName)
        {
            if (dbName == null)
            {
                throw new ArgumentNullException("database name");
            }

            string file = Path.Combine(destination, String.Format("{0}.txt", dbName.ToUpper()));
            if (!File.Exists(file))
            {
                Console.WriteLine("DB not found.");
                return null;
            }
            else
            {
                StreamReader inputStream = File.OpenText(file);
                string line = inputStream.ReadLine();
                return line.Split('|');
            }
        }

        /// <summary>
        /// Makes a new record at the end of the document.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        /// <param name="records">One record per field except ID and visibility.</param>
        /// <param name="isVisible">Set visibility.</param>
        public static void AddRecord(string dbName, string[] records, bool isVisible = true)
        {
            if (dbName == null)
            {
                throw new ArgumentNullException("database name");
            }
            if (records == null)
            {
                throw new ArgumentNullException("records");
            }

            string file = Path.Combine(destination, String.Format("{0}.txt", dbName.ToUpper()));
            if (!File.Exists(file))
            {
                Console.WriteLine("DB not found.");
            }
            else
            {
                // Check if records is the same length as structure
                // error if it isn't
                // + 2 for ID and visible
                if (FileStructureLength(dbName) != ((records.Length) + 2))
                {
                    Console.WriteLine("Records is not the same length as the file structure (+2 for ID/VISIBILITY).");
                }
                else
                {
                    string lastLine = File.ReadLines(file).Last();
                    string id = "";

                    if (lastLine.Split('|')[0].Equals("ID"))
                    {
                        id = "1";
                    }
                    else
                    {
                        id = Convert.ToString(Convert.ToInt32(lastLine.Split('|')[0]) + 1);
                    }

                    using (StreamWriter stream = File.AppendText(file))
                    {
                        stream.Write(id);
                        string visible = (isVisible) ? "true" : "false";
                        stream.Write(String.Format("|{0}", visible));
                        foreach (string record in records)
                        {
                            stream.Write(String.Format("|{0}", record));
                        }
                        stream.WriteLine();
                    }
                }
            }
        }

        /// <summary>
        /// Set visibility to hidden.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        /// <param name="id">Id of the record.</param>
        public static void HideRecord(string dbName, int id)
        {
            string[] records = { "false" };
            RewriteRecord(dbName, id, records);
        }

        /// <summary>
        /// Set visibility to show.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        /// <param name="id">Id of the record.</param>
        public static void ShowRecord(string dbName, int id)
        {
            string[] records = { "true" };
            RewriteRecord(dbName, id, records);
        }

        /// <summary>
        /// Changes the data of a record.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        /// <param name="id">Id of the record.</param>
        /// <param name="records">One record per field except ID and visibility.</param>
        public static void ChangeRecord(string dbName, int id, string[] records)
        {
            RewriteRecord(dbName, id, records);
        }

        /// <summary>
        /// Search the document where colmName is EQUAL to searchVar.
        /// It stops at the first record it finds.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        /// <param name="colmName">Name of the colom.</param>
        /// <param name="searchVar">A word you want to search for.</param>
        /// <param name="caseSensitive">True for case sensitive, false for turning it off.</param>
        /// <param name="onlyVisible">True for only the records that are visible, false for the hidden records.</param>
        /// <returns>Array of the first record.</returns>
        public static string[] FindFirst(string dbName, string colmName, string searchVar, bool caseSensitive = false, bool onlyVisible = true)
        {
            List<string[]> data = SearchDB(dbName, colmName, searchVar, caseSensitive, onlyVisible, true);
            if (data.Any())
            {
                return data.First();
            }
            return null;
        }

        /// <summary>
        /// Search the document where colmName is EQUAL to searchVar.
        /// It stops at the first record it finds.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        /// <param name="colmName">Name of the colom.</param>
        /// <param name="searchVar">A word you want to search for.</param>
        /// <param name="caseSensitive">True for case sensitive, false for turning it off.</param>
        /// <param name="onlyVisible">True for only the records that are visible, false for the hidden records.</param>
        /// <returns>List of all record it could find.</returns>
        public static List<string[]> FindAll(string dbName, string colmName, string searchVar, bool caseSensitive = false, bool onlyVisible = true)
        {
            return SearchDB(dbName, colmName, searchVar, caseSensitive, onlyVisible, false);
        }

        /// <summary>
        /// Get the full DB.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        /// <returns>List of all record.</returns>
        public static List<string[]> GetDB(string dbName)
        {
            if (dbName == null)
            {
                throw new ArgumentNullException("database name");
            }

            string file = Path.Combine(destination, String.Format("{0}.txt", dbName.ToUpper()));
            if (!File.Exists(file))
            {
                Console.WriteLine("DB not found");
                return null;
            }
            else
            {
                StreamReader inputStream = File.OpenText(file);
                string[] foundRecord = new string[FileStructureLength(dbName)];
                List<string[]> foundRecords = new List<string[]>();

                string line = inputStream.ReadLine();
                line = inputStream.ReadLine();
                while (line != null)
                {
                    foundRecord = line.Split('|');
                    foundRecords.Add(foundRecord);
                    line = inputStream.ReadLine();
                }
                return foundRecords;
            }
        }

        /// <summary>
        /// Looks how many fields are there.
        /// Used in other static methods.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        /// <returns>The number of fields.</returns>
        private static int FileStructureLength(string dbName)
        {
            string file = Path.Combine(destination, String.Format("{0}.txt", dbName.ToUpper()));
            string filestructure = File.ReadLines(file).First();
            return filestructure.Split('|').Length;
        }

        /// <summary>
        /// Rewrites a existing record.
        /// Used in other static methods.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        /// <param name="id">ID of the record.</param>
        /// <param name="recordsAll">One record per field except ID.</param>
        private static void RewriteRecord(string dbName, int id, string[] recordsAll)
        {
            if (dbName == null)
            {
                throw new ArgumentNullException("database name");
            }
            if (recordsAll == null)
            {
                throw new ArgumentNullException("records");
            }

            string[] records = null;

            if (recordsAll[0].Equals("true") || recordsAll[0].Equals("false"))
            {
                // get line and change visibility
                records = FindFirst(dbName, "id", Convert.ToString(id));
                records[1] = (recordsAll[1].Equals("true")) ? "false" : "true";
            }
            else
            {
                // check for same length
                if (FileStructureLength(dbName) != ((recordsAll.Length) + 2))
                {
                    Console.WriteLine("Records komt niet overeen met de database");
                    return;
                }
                else
                {
                    // get line and change the string
                    records = FindFirst(dbName, "id", Convert.ToString(id));
                    int recordId = 2;

                    foreach (string record in recordsAll)
                    {
                        records[recordId] = record;
                        recordId++;
                    }
                }
            }

            // update txt file
            bool first = true;
            string file = Path.Combine(destination, String.Format("{0}.txt", dbName.ToUpper()));
            string tempFile = Path.Combine(destination, String.Format("{0}_temp.txt", dbName.ToLower()));
            StreamReader inputStream = File.OpenText(file);
            StreamWriter outputStream = File.CreateText(tempFile);

            string line = inputStream.ReadLine();
            while (line != null)
            {
                if (line.Split('|')[0].Equals(Convert.ToString(id)))
                {
                    // change content
                    foreach (string record in records)
                    {
                        if (first)
                        {
                            outputStream.Write(String.Format("{0}", record));
                            first = false;
                        }
                        else
                        {
                            outputStream.Write(String.Format("|{0}", record));
                        }
                    }
                    outputStream.WriteLine();
                }
                else
                {
                    // copy content
                    outputStream.WriteLine(line);
                }
                line = inputStream.ReadLine();
            }
            // delete temp file and rename back to original
            outputStream.Close();
            inputStream.Close();
            File.Delete(file);
            File.Move(tempFile, file);
        }

        /// <summary>
        /// Used in other static methods.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        /// <param name="colmName">Name of the colom.</param>
        /// <param name="searchVar">A word you want to search for.</param>
        /// <param name="caseSensitive">True for case sensitive, false for turning it off.</param>
        /// <param name="onlyVisible">True for only the records that are visible, false for the hidden records.</param>
        /// <param name="onlyFirst">True for 1 record, false for a list of all records.</param>
        /// <returns>List of all record it could find except when onlyFirst is true.</returns>
        private static List<string[]> SearchDB(string dbName, string colmName, string searchVar, bool caseSensitive, bool onlyVisible, bool onlyFirst)
        {
            if (dbName == null)
            {
                throw new ArgumentNullException("database name");
            }
            if (colmName == null)
            {
                throw new ArgumentNullException("column name");
            }
            if (searchVar == null)
            {
                throw new ArgumentNullException("search string");
            }

            string file = Path.Combine(destination, String.Format("{0}.txt", dbName.ToUpper()));
            if (!File.Exists(file))
            {
                Console.WriteLine("DB not found");
                return new List<string[]>();
            }
            else
            {
                StreamReader inputStream = File.OpenText(file);
                string line = inputStream.ReadLine();
                string[] foundRecord = new string[FileStructureLength(dbName)];
                string word = null;
                bool colmExists = false;
                bool checkVisible;
                int colmIndex = 0;
                colmName = (caseSensitive) ? colmName : colmName.ToLower();
                searchVar = (caseSensitive) ? searchVar : searchVar.ToLower();
                bool stopSearch = false;
                List<string[]> foundRecords = new List<string[]>();

                for (int i = 0; i < FileStructureLength(dbName); i++)
                {
                    word = line.Split('|')[i].ToLower();

                    if (word == colmName)
                    {
                        colmExists = true;
                        colmIndex = i;
                    }
                }

                if (colmExists)
                {
                    while ((line != null) && stopSearch == false)
                    {
                        word = line.Split('|')[colmIndex];
                        word = (caseSensitive) ? word : word.ToLower();

                        if (searchVar.Equals(word))
                        {
                            foundRecord = null;

                            if (onlyVisible)
                            {
                                checkVisible = Convert.ToBoolean(line.Split('|')[1]);
                                if (checkVisible)
                                {
                                    foundRecord = line.Split('|');
                                    foundRecords.Add(foundRecord);
                                    line = inputStream.ReadLine();
                                    stopSearch = (onlyFirst) ? true : false;
                                }
                                else
                                {
                                    line = inputStream.ReadLine();
                                }
                            }
                            else
                            {
                                foundRecord = line.Split('|');
                                foundRecords.Add(foundRecord);
                                line = inputStream.ReadLine();
                                stopSearch = (onlyFirst) ? true : false;
                            }
                        }
                        else
                        {
                            line = inputStream.ReadLine();
                        }
                    }

                    if (foundRecords.Any())
                    {
                        inputStream.Close();
                        return foundRecords;
                    }
                    else
                    {
                        inputStream.Close();
                        return new List<string[]>();
                    }
                }
                else
                {
                    inputStream.Close();
                    return new List<string[]>();
                }
            }
        }

        // Auther: Gregory
        // Date: 05/04/2015 11:30
        // Implemented 2 methods

        /// <summary>
        /// Returns length of Visible lines and total lines (column names of DB excluded).
        /// Example:
        ///     int visibleLines, totalLines;
        ///     DBController.LineCount("MyDatabase", out visibleLines, out totalLines);
        /// </summary>
        /// <param name="dbName">Name of DB File.</param>
        public static void LineCount(string dbName, out int visibleLines, out int totalLines)
        {
            try
            {
                string[] test;

                visibleLines = 0;
                totalLines = -1;

                string readFile = Path.Combine(destination, String.Format("{0}.txt", dbName.ToUpper()));
                StreamReader read = File.OpenText(readFile);

                string reader = read.ReadLine();//12/04: nieuwe check, ging goed tot aan 9 regels en dan werkte vorige versie nietmeer! --> source: http://www.dotnetperls.com/split
                
                while (reader != null)
                {
                    test = reader.Split('|');
                    if (test[1].Equals("true"))
                    {
                        visibleLines++;
                    }
                    totalLines++;
                    reader = read.ReadLine();
                }
            }
            catch (FileNotFoundException)
            {
                visibleLines = 0;
                totalLines = 0;
            }
        }


        /// <summary>
        /// Changes record, excluding the first 2 values of provided array, usefull if using readFirst()/ReadAll(). 
        /// </summary>
        /// <param name="dbName">Name of DB File.</param>
        /// <param name="colmName">Column Name (top row of DBFile)</param>
        /// <param name="[]records">array you wish to replace current row with, NOTE: you can use DBController.FindFirst() for this!</param>
        public static void ChangeFromRead(string dbName, int colmName, string[] records)//added by Greg: 06/04/15.
        {
            try
            {
                string[] newArray = new string[records.Length - 2];

                for (int i = 2; i < records.Length; i++)
                {
                    newArray[i - 2] = records[i];
                }
                records = null;

                //ChangeRecord(dbName, colmName, newArray);   //not performant: fixed at 07/04: 12:10-12:15 by Gregory
                RewriteRecord(dbName, colmName, newArray);
            }
            catch
            {
                Console.WriteLine("unhandled exception in ChangeCell...");
            }
        }
    }
}
