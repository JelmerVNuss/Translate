﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Translate.Translators.Utilities;

namespace Translate.Translators.Grammars
{
    class Grammar : TranslatorObject
    {
        protected string sourceLanguage;
        protected string targetLanguage;

        // A grammar is a list of strings containing the grammatical rules.
        // TODO For easier handling, parse this into a list of all the parts of the rule.
        IList<string> entries = new List<string>();

        public Grammar(string sourceLanguage, string targetLanguage, string id = "")
            : base(id)
        {
            this.sourceLanguage = sourceLanguage;
            this.targetLanguage = targetLanguage;
            
            Load();
        }

        public void Load()
        {
            string fileName = "gram_" + sourceLanguage + ".txt";
            string cd = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string path = Path.Combine(cd, @"res\Grammars", fileName);
            string[] lines = File.ReadAllLines(path);

            Console.WriteLine("Loading grammar: " + fileName);
            foreach (string line in lines)
            {
                // TODO Parse the rules into multiple parts for easier handling.
                string rule = line;
                this.entries.Add(rule);
            }
            Console.WriteLine("Grammar loaded: " + fileName);
        }

        public void Update()
        {
            string fileName = "gram_" + sourceLanguage + "_" + targetLanguage + ".txt";
            string cd = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string path = Path.Combine(cd, @"res\Grammars", fileName);

            Console.WriteLine("Updating grammar: " + fileName);

            // Adding the entries in grammar to the text file.
            string[] lines = File.ReadAllLines(path);
            int linesNr = lines.Length;
            int newLines = entries.Count - linesNr;
            if (newLines <= 0)
                return;

            // Appending each new entry to the text file.
            for (int i = newLines - 1; i < entries.Count; i++)
            {
                string rule = entries[i];
                AddEntry(rule, fileName, path);
            }
        }

        public void AddEntry(string rule, string fileName, string path)
        {
            string entry = rule;

            Console.WriteLine("Adding entry: " + entry + " to grammar " + fileName);

            using (StreamWriter w = File.AppendText(path))
                w.WriteLine(entry);

            Console.WriteLine("Entry added: " + entry + " to grammar " + fileName);
        }

        public Tuple<string, string> Languages
        {
            get { return new Tuple<string, string>(sourceLanguage, targetLanguage); }
        }

        public IList<string> Entries
        {
            get { return entries; }
        }
    }
}