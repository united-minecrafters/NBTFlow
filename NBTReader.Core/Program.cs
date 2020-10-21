using System;
using System.IO;
using System.IO.Compression;
using NBT;
using NBT.Serialization;


namespace NBTReader.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fileStream = new FileStream(args[0], FileMode.Open);
            TagReader tagReader = new BinaryTagReader(fileStream);
            TagCompound root = tagReader.ReadDocument();
            Console.Out.Write(NbtFormatter.Format(root));
        }
    }
}