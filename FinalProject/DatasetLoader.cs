using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;
using System.Threading.Tasks;

namespace FinalProject
{
    /**
     * This is the derived class that loads the CSV file and extrapolates those values.
     * Author: Erik Njolstad
     * Date Modified: 11/10/2017
     */
    public class DatasetLoader : WriteLines
    {
        private List<string> list = new List<string>();
        private string[] lines;
        private string firstLine;
        private const string FileName = "00010014-eng.csv";
        private const int MaxValues = 1542;

        /**
         * Default constructor which calls GetFirstLine(), GetLines() and GetList().
         */
        public DatasetLoader() : base()
        {
            GetColumnHeader();
            GetLines();
            GetList();
        }

        /**
         * Gets the column header from the CSV file.
         */
        public override string GetColumnHeader()
        {
            firstLine = File.ReadAllLines(FileName).First();
            string[] words = firstLine.Split(new char[] {' ', ',', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            list = words.ToList();
            return firstLine;
        }

        /**
         * Gets the lines from the CSV file and limits the number of records to 1542.
         */
        public override string[] GetLines()
        {
            lines = File.ReadAllLines(FileName).Skip(1).Take(MaxValues).ToArray();
            return lines;
        }

        /**
         * Returns the list.
         */
        public override List<string> GetList()
        {
            return list;
        }

        /**
         * Calls the CreateTable, AutoInsertDataToTable and Choices function from the
         * Program class upon execution.
         */
        public static void Main(string[] args)
        {
            Program prog = new Program();
            prog.CreateTable();
            prog.AutoInsertDataToTable();
            prog.Execute();
        }
    }
}
