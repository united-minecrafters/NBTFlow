using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using NBT;
using NBT.Serialization;
using CommandLine;
using System.Linq;


namespace NBTReader.Core
{
    class Program
    {
        private static string _filename;
        private static int _indents;
        private static bool stdin;

        public class Options
        {
            [Value(0, MetaName = "filename", HelpText = "File name")]
            public string FileName { get; set; }

            [Option('i', "indent", Default = 1)]
            public int IndentSize { get; set; }

            [Option('s', "stdin", Default=false)]
            public bool stdin {get; set;}

        }

        public static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
            var fileStream = !stdin ? new FileStream(_filename, FileMode.Open) : Console.OpenStandardInput();
            TagReader tagReader = new BinaryTagReader(fileStream);
            var root = tagReader.ReadDocument();
            Console.Out.Write(NbtFormatter.Format(root,0, _indents));
        }
        
        static void RunOptions(Options opts)
        {
            _filename = opts.FileName;
            _indents = opts.IndentSize;
            stdin = opts.stdin;
        }
        static void HandleParseError(IEnumerable<Error> errs)
        {
            //handle errors
        }
    }
}
