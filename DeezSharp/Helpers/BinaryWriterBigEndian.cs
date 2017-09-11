using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DeezSharp.Helpers
{
    internal class BinaryWriterBigEndian : BinaryWriter
    {
        protected BinaryWriterBigEndian() { }
        public BinaryWriterBigEndian(Stream output) : base(output) { }
        public BinaryWriterBigEndian(Stream output, Encoding encoding) : base(output, encoding) { }
        public BinaryWriterBigEndian(Stream output, Encoding encoding, bool leaveOpen) : base(output, encoding, leaveOpen) { }
        
        public override void Write(bool value) => base.Write(value);
        public override void Write(sbyte value) => base.Write(value);
        public override void Write(byte value) => base.Write(value);
        public override void Write(byte[] buffer) => base.Write(buffer);
        public override void Write(byte[] buffer, int index, int count) => base.Write(buffer, index, count);
        public override void Write(char ch) => base.Write(ch);
        public override void Write(char[] chars) => base.Write(chars);
        public override void Write(char[] chars, int index, int count) => base.Write(chars, index, count);
        public override void Write(string value) => base.Write(value);
        public override void Write(decimal value) => base.Write(value);

        public override void Write(short value)  => base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        public override void Write(ushort value) => base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        public override void Write(int value)    => base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        public override void Write(uint value)   => base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        public override void Write(long value)   => base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        public override void Write(ulong value)  => base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        public override void Write(float value)  => base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        public override void Write(double value) => base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
    }
}
