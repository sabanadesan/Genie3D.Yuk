﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using IniParser.Exceptions;
using IniParser.Model;

namespace IniParser
{
    public class FileIniDataParser
    {

        private IniDataParser _parser;

        public FileIniDataParser()
        {
            _parser = new IniDataParser();
            _parser.Scheme.CommentString = "#";
        }

        public void WriteFile(string fileName, string path, IniData data)
        {
            SectionCollection sections = data.Sections;
            foreach (Section section in sections)
            {
                System.Console.WriteLine("[{0}]", section.Name);

                foreach (Property property in section.Properties)
                {
                    System.Console.WriteLine("{0}={1}", property.Key, property.Value);
                }
            }
        }

        public IniData ReadAndParseIniFile(string fileName, string path, Encoding fileEncoding)
        {
            var filePath = Path.Combine(path, fileName);

            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"File does not exists at path {filePath}.");
            }

            try
            {
                // (FileAccess.Read) we want to open the ini only for reading 
                // (FileShare.ReadWrite) any other process should still have access to the ini file 
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs, fileEncoding))
                    {
                        return _parser.Parse(sr.ReadToEnd());
                    }
                }
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ParsingException($"Could not parse file {filePath}", ex);
            }

        }

    }
}
