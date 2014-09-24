using System;
using System.Text;
using System.IO;
using SevenZip;

public class Lzma
{
    public static byte[] CompressString(string s)
    {
        byte[] uncompressed = Encoding.UTF8.GetBytes(s);
        return CompressBuffer(uncompressed);
    }

    public static byte[] CompressBuffer(byte[] buff)
    {
        CoderPropID[] propIDs = 
                {
                    CoderPropID.DictionarySize,
                    CoderPropID.PosStateBits,
                    CoderPropID.LitContextBits,
                    CoderPropID.LitPosBits,
                    CoderPropID.Algorithm,
                    CoderPropID.NumFastBytes,
                    CoderPropID.MatchFinder,
                    CoderPropID.EndMarker
                };
        object[] encode_properties = 
                {
                    1<<23,
                    2,
                    3,
                    0,
                    2,
                    128,
                    "bt4",
                    false
                };
        SevenZip.Compression.LZMA.Encoder encoder = new SevenZip.Compression.LZMA.Encoder();
        MemoryStream inStream = new MemoryStream(buff);
        MemoryStream outStream = new MemoryStream();
        encoder.SetCoderProperties(propIDs, encode_properties);
        encoder.WriteCoderProperties(outStream);
        for (int i = 0; i < 8; i++)
        {
            outStream.WriteByte((byte)(inStream.Length >> (8 * i)));
        }
        // 压缩
        encoder.Code(inStream, outStream, -1, -1, null);
        inStream.Close();
        byte[] result = outStream.ToArray();
        outStream.Close();
        return result;
    }

    public static byte[] UncompressBuffer(byte[] buff)
    {
        MemoryStream inStream = new MemoryStream(buff);
        MemoryStream outStream = new MemoryStream();

        byte[] decode_properties = new byte[5];
        int n = inStream.Read(decode_properties, 0, 5);
        if (n != 5) return null;


        SevenZip.Compression.LZMA.Decoder decoder = new SevenZip.Compression.LZMA.Decoder();
        decoder.SetDecoderProperties(decode_properties);

        long outSize = 0;
        for (int i = 0; i < 8; i++)
        {
            int v = inStream.ReadByte();
            if (v < 0)
            {
                outSize = 0;
                break;
            }
            outSize |= ((long)v) << (8 * i);
        }
        if (outSize <= 0) return null;
        long compressedSize = inStream.Length - inStream.Position;
        decoder.Code(inStream, outStream, compressedSize, outSize, null);
        inStream.Close();

        byte[] result = outStream.ToArray();
        outStream.Close();
        return result;
    }

    public static string UncompressString(byte[] buff)
    {
        byte[] uncompressed = UncompressBuffer(buff);
        return Encoding.UTF8.GetString(uncompressed);
    }
}
