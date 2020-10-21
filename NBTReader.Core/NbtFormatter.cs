using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using NBT;

namespace NBTReader.Core
{
    public class NbtFormatter
    {
        public static string Format(TagCompound tag, int indent, int size)
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            for (int i = 0; i < tag.Count; i++)
            {
                stringBuilder.AppendLine($"{new string(' ', indent*size)}{Format(tag[i], indent, size)}");
            }

            return stringBuilder.ToString();
        }

        public static string Format(Tag tag, int indent, int size)
        {
            switch (tag.Type)
            {
                case TagType.Byte:
                    return $"{new string(' ', indent*size)}{tag.Name.Trim()}: {tag.GetValue()}b";
                case TagType.Double:
                    return $"{new string(' ', indent*size)}{tag.Name.Trim()}: {tag.GetValue()}d";
                case TagType.Int:
                    return $"{new string(' ', indent*size)}{tag.Name.Trim()}: {tag.GetValue()}i";
                case TagType.Float:
                    return $"{new string(' ', indent*size)}{tag.Name.Trim()}: {tag.GetValue()}f";
                case TagType.Long:
                    return $"{new string(' ', indent*size)}{tag.Name.Trim()}: {tag.GetValue()}L";
                case TagType.Short:
                    return $"{new string(' ', indent*size)}{tag.Name.Trim()}: {tag.GetValue()}s";
                case TagType.List:
                    return $"{new string(' ', indent*size)}{tag.Name.Trim()}: {tag.GetValue()}";
                case TagType.Compound:
                    TagCompound tagCompound = tag as TagCompound;
                    StringBuilder stringBuilder = new StringBuilder($"{new string(' ', indent*size)}{tag.Name.Trim()}:\n");
                    for (int i = 0; i < tagCompound.Count; i++)
                        stringBuilder.AppendLine($"{Format(tagCompound[i], indent + 1, size)}");
                    return stringBuilder.ToString().TrimEnd();
                // return $"{new string(' ', indent)}{tag.Name.Trim()}:" +
                //       tagCompound.Flatten().Select(child => Format(child, indent + 4))
                //           .Aggregate((working, end) => (working + "\n" + end));
                case TagType.String:
                    return $"{new string(' ', indent*size)}{tag.Name.Trim()}: \"{tag.GetValue()}\"";
                case TagType.None:
                    return "";
                case TagType.IntArray:
                    TagIntArray tagIntArray = (TagIntArray) tag;
                    if (((IReadOnlyCollection<int>) tagIntArray.Value).Count == 0)
                        return
                            $"{new string(' ', indent * size)}{tag.Name.Trim()}: [ ]";
                    else
                        return
                            $"{new string(' ', indent * size)}{tag.Name.Trim()}: [" +
                            tagIntArray.Value.ToArray().Select(x => x.ToString())
                                .Aggregate((working, next) => working + ", " + next) + "]";
                    


            }

            return tag.ToString();
        }
    }
}