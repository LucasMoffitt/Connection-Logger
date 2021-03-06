﻿using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Connection_Logger
{
    public class CsvRow : List<string>
    {
        public string LineText { get; set; }
    }

    public class CsvFileWriter : StreamWriter
    {
        public CsvFileWriter(Stream stream) : base(stream)
        {
        }

        public CsvFileWriter(string filename) : base(filename)
        {
        }

        public void WriteRow(CsvRow row)
        {
            StringBuilder builder = new StringBuilder();
            bool firstColumn = true;
            foreach (string value in row)
            {
                if (!firstColumn)
                    builder.Append(',');

                if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                {
                    builder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                }
                else
                {
                    builder.Append(value);
                }

                firstColumn = false;
            }
            row.LineText = builder.ToString();
            WriteLine(row.LineText);
        }
    }
}