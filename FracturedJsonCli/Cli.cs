﻿using System;
using System.IO;
using FracturedJson.V3;

namespace FracturedJsonCli
{
    /// <summary>
    /// Commandline app to format JSON using FracturedJson.Formatter.  Output is to standard out.  Input
    /// is expected from standard in, or you can use --file and give it a file name.
    /// </summary>
    internal static class Cli
    {
        internal static int Main(string[] args)
        {
            try
            {
                var inputText = File.ReadAllText(args[0]);
                var options = new FracturedJsonOptions()
                {
                    CommentPolicy = CommentPolicy.Preserve,
                    PreserveBlankLines = true,
                    MaxTotalLineLength = 800,
                    MaxInlineLength = 120,
                };
                var formatter = new Formatter() { Options = options };
                var formattedDoc = formatter.Reformat(inputText, 0);
                Console.WriteLine(formattedDoc);
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return 1;
            }
        }
    }
}