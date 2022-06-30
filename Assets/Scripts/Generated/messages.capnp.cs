using Capnp;
using Capnp.Rpc;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CapnpGen
{
    [System.CodeDom.Compiler.GeneratedCode("capnpc-csharp", "1.3.0.0"), TypeId(0x85f02567d51564deUL)]
    public class Time : ICapnpSerializable
    {
        public const UInt64 typeId = 0x85f02567d51564deUL;
        void ICapnpSerializable.Deserialize(DeserializerState arg_)
        {
            var reader = READER.create(arg_);
            Millis = reader.Millis;
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            writer.Millis = Millis;
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
        }

        public ulong Millis
        {
            get;
            set;
        }

        public struct READER
        {
            readonly DeserializerState ctx;
            public READER(DeserializerState ctx)
            {
                this.ctx = ctx;
            }

            public static READER create(DeserializerState ctx) => new READER(ctx);
            public static implicit operator DeserializerState(READER reader) => reader.ctx;
            public static implicit operator READER(DeserializerState ctx) => new READER(ctx);
            public ulong Millis => ctx.ReadDataULong(0UL, 0UL);
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(1, 0);
            }

            public ulong Millis
            {
                get => this.ReadDataULong(0UL, 0UL);
                set => this.WriteData(0UL, value, 0UL);
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("capnpc-csharp", "1.3.0.0"), TypeId(0xd52ca0ac8a5bbb8cUL)]
    public class Vector3 : ICapnpSerializable
    {
        public const UInt64 typeId = 0xd52ca0ac8a5bbb8cUL;
        void ICapnpSerializable.Deserialize(DeserializerState arg_)
        {
            var reader = READER.create(arg_);
            X = reader.X;
            Y = reader.Y;
            Z = reader.Z;
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            writer.X = X;
            writer.Y = Y;
            writer.Z = Z;
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
        }

        public float X
        {
            get;
            set;
        }

        public float Y
        {
            get;
            set;
        }

        public float Z
        {
            get;
            set;
        }

        public struct READER
        {
            readonly DeserializerState ctx;
            public READER(DeserializerState ctx)
            {
                this.ctx = ctx;
            }

            public static READER create(DeserializerState ctx) => new READER(ctx);
            public static implicit operator DeserializerState(READER reader) => reader.ctx;
            public static implicit operator READER(DeserializerState ctx) => new READER(ctx);
            public float X => ctx.ReadDataFloat(0UL, 0F);
            public float Y => ctx.ReadDataFloat(32UL, 0F);
            public float Z => ctx.ReadDataFloat(64UL, 0F);
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(2, 0);
            }

            public float X
            {
                get => this.ReadDataFloat(0UL, 0F);
                set => this.WriteData(0UL, value, 0F);
            }

            public float Y
            {
                get => this.ReadDataFloat(32UL, 0F);
                set => this.WriteData(32UL, value, 0F);
            }

            public float Z
            {
                get => this.ReadDataFloat(64UL, 0F);
                set => this.WriteData(64UL, value, 0F);
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("capnpc-csharp", "1.3.0.0"), TypeId(0xa235492b8b42eaaeUL)]
    public class Quaternion : ICapnpSerializable
    {
        public const UInt64 typeId = 0xa235492b8b42eaaeUL;
        void ICapnpSerializable.Deserialize(DeserializerState arg_)
        {
            var reader = READER.create(arg_);
            X = reader.X;
            Y = reader.Y;
            Z = reader.Z;
            W = reader.W;
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            writer.X = X;
            writer.Y = Y;
            writer.Z = Z;
            writer.W = W;
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
        }

        public float X
        {
            get;
            set;
        }

        public float Y
        {
            get;
            set;
        }

        public float Z
        {
            get;
            set;
        }

        public float W
        {
            get;
            set;
        }

        public struct READER
        {
            readonly DeserializerState ctx;
            public READER(DeserializerState ctx)
            {
                this.ctx = ctx;
            }

            public static READER create(DeserializerState ctx) => new READER(ctx);
            public static implicit operator DeserializerState(READER reader) => reader.ctx;
            public static implicit operator READER(DeserializerState ctx) => new READER(ctx);
            public float X => ctx.ReadDataFloat(0UL, 0F);
            public float Y => ctx.ReadDataFloat(32UL, 0F);
            public float Z => ctx.ReadDataFloat(64UL, 0F);
            public float W => ctx.ReadDataFloat(96UL, 0F);
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(2, 0);
            }

            public float X
            {
                get => this.ReadDataFloat(0UL, 0F);
                set => this.WriteData(0UL, value, 0F);
            }

            public float Y
            {
                get => this.ReadDataFloat(32UL, 0F);
                set => this.WriteData(32UL, value, 0F);
            }

            public float Z
            {
                get => this.ReadDataFloat(64UL, 0F);
                set => this.WriteData(64UL, value, 0F);
            }

            public float W
            {
                get => this.ReadDataFloat(96UL, 0F);
                set => this.WriteData(96UL, value, 0F);
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("capnpc-csharp", "1.3.0.0"), TypeId(0xceaf562ebd624d26UL)]
    public class Pose : ICapnpSerializable
    {
        public const UInt64 typeId = 0xceaf562ebd624d26UL;
        void ICapnpSerializable.Deserialize(DeserializerState arg_)
        {
            var reader = READER.create(arg_);
            Position = CapnpSerializable.Create<CapnpGen.Vector3>(reader.Position);
            Orientation = CapnpSerializable.Create<CapnpGen.Quaternion>(reader.Orientation);
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            Position?.serialize(writer.Position);
            Orientation?.serialize(writer.Orientation);
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
        }

        public CapnpGen.Vector3 Position
        {
            get;
            set;
        }

        public CapnpGen.Quaternion Orientation
        {
            get;
            set;
        }

        public struct READER
        {
            readonly DeserializerState ctx;
            public READER(DeserializerState ctx)
            {
                this.ctx = ctx;
            }

            public static READER create(DeserializerState ctx) => new READER(ctx);
            public static implicit operator DeserializerState(READER reader) => reader.ctx;
            public static implicit operator READER(DeserializerState ctx) => new READER(ctx);
            public CapnpGen.Vector3.READER Position => ctx.ReadStruct(0, CapnpGen.Vector3.READER.create);
            public CapnpGen.Quaternion.READER Orientation => ctx.ReadStruct(1, CapnpGen.Quaternion.READER.create);
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(0, 2);
            }

            public CapnpGen.Vector3.WRITER Position
            {
                get => BuildPointer<CapnpGen.Vector3.WRITER>(0);
                set => Link(0, value);
            }

            public CapnpGen.Quaternion.WRITER Orientation
            {
                get => BuildPointer<CapnpGen.Quaternion.WRITER>(1);
                set => Link(1, value);
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("capnpc-csharp", "1.3.0.0"), TypeId(0x8857e0e32918e581UL)]
    public class PoseStamped : ICapnpSerializable
    {
        public const UInt64 typeId = 0x8857e0e32918e581UL;
        void ICapnpSerializable.Deserialize(DeserializerState arg_)
        {
            var reader = READER.create(arg_);
            Stamp = CapnpSerializable.Create<CapnpGen.Time>(reader.Stamp);
            Pose = CapnpSerializable.Create<CapnpGen.Pose>(reader.Pose);
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            Stamp?.serialize(writer.Stamp);
            Pose?.serialize(writer.Pose);
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
        }

        public CapnpGen.Time Stamp
        {
            get;
            set;
        }

        public CapnpGen.Pose Pose
        {
            get;
            set;
        }

        public struct READER
        {
            readonly DeserializerState ctx;
            public READER(DeserializerState ctx)
            {
                this.ctx = ctx;
            }

            public static READER create(DeserializerState ctx) => new READER(ctx);
            public static implicit operator DeserializerState(READER reader) => reader.ctx;
            public static implicit operator READER(DeserializerState ctx) => new READER(ctx);
            public CapnpGen.Time.READER Stamp => ctx.ReadStruct(0, CapnpGen.Time.READER.create);
            public CapnpGen.Pose.READER Pose => ctx.ReadStruct(1, CapnpGen.Pose.READER.create);
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(0, 2);
            }

            public CapnpGen.Time.WRITER Stamp
            {
                get => BuildPointer<CapnpGen.Time.WRITER>(0);
                set => Link(0, value);
            }

            public CapnpGen.Pose.WRITER Pose
            {
                get => BuildPointer<CapnpGen.Pose.WRITER>(1);
                set => Link(1, value);
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("capnpc-csharp", "1.3.0.0"), TypeId(0xe3568a4577f4a5aeUL)]
    public class Mesh : ICapnpSerializable
    {
        public const UInt64 typeId = 0xe3568a4577f4a5aeUL;
        void ICapnpSerializable.Deserialize(DeserializerState arg_)
        {
            var reader = READER.create(arg_);
            Vertices = reader.Vertices;
            Faces = reader.Faces;
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            writer.Vertices.Init(Vertices);
            writer.Faces.Init(Faces);
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
        }

        public IReadOnlyList<float> Vertices
        {
            get;
            set;
        }

        public IReadOnlyList<int> Faces
        {
            get;
            set;
        }

        public struct READER
        {
            readonly DeserializerState ctx;
            public READER(DeserializerState ctx)
            {
                this.ctx = ctx;
            }

            public static READER create(DeserializerState ctx) => new READER(ctx);
            public static implicit operator DeserializerState(READER reader) => reader.ctx;
            public static implicit operator READER(DeserializerState ctx) => new READER(ctx);
            public IReadOnlyList<float> Vertices => ctx.ReadList(0).CastFloat();
            public IReadOnlyList<int> Faces => ctx.ReadList(1).CastInt();
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(0, 2);
            }

            public ListOfPrimitivesSerializer<float> Vertices
            {
                get => BuildPointer<ListOfPrimitivesSerializer<float>>(0);
                set => Link(0, value);
            }

            public ListOfPrimitivesSerializer<int> Faces
            {
                get => BuildPointer<ListOfPrimitivesSerializer<int>>(1);
                set => Link(1, value);
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("capnpc-csharp", "1.3.0.0"), TypeId(0x867e63b587fcc2d1UL)]
    public class MeshStamped : ICapnpSerializable
    {
        public const UInt64 typeId = 0x867e63b587fcc2d1UL;
        void ICapnpSerializable.Deserialize(DeserializerState arg_)
        {
            var reader = READER.create(arg_);
            Stamp = CapnpSerializable.Create<CapnpGen.Time>(reader.Stamp);
            Pose = CapnpSerializable.Create<CapnpGen.Pose>(reader.Pose);
            Mesh = CapnpSerializable.Create<CapnpGen.Mesh>(reader.Mesh);
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            Stamp?.serialize(writer.Stamp);
            Pose?.serialize(writer.Pose);
            Mesh?.serialize(writer.Mesh);
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
        }

        public CapnpGen.Time Stamp
        {
            get;
            set;
        }

        public CapnpGen.Pose Pose
        {
            get;
            set;
        }

        public CapnpGen.Mesh Mesh
        {
            get;
            set;
        }

        public struct READER
        {
            readonly DeserializerState ctx;
            public READER(DeserializerState ctx)
            {
                this.ctx = ctx;
            }

            public static READER create(DeserializerState ctx) => new READER(ctx);
            public static implicit operator DeserializerState(READER reader) => reader.ctx;
            public static implicit operator READER(DeserializerState ctx) => new READER(ctx);
            public CapnpGen.Time.READER Stamp => ctx.ReadStruct(0, CapnpGen.Time.READER.create);
            public CapnpGen.Pose.READER Pose => ctx.ReadStruct(1, CapnpGen.Pose.READER.create);
            public CapnpGen.Mesh.READER Mesh => ctx.ReadStruct(2, CapnpGen.Mesh.READER.create);
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(0, 3);
            }

            public CapnpGen.Time.WRITER Stamp
            {
                get => BuildPointer<CapnpGen.Time.WRITER>(0);
                set => Link(0, value);
            }

            public CapnpGen.Pose.WRITER Pose
            {
                get => BuildPointer<CapnpGen.Pose.WRITER>(1);
                set => Link(1, value);
            }

            public CapnpGen.Mesh.WRITER Mesh
            {
                get => BuildPointer<CapnpGen.Mesh.WRITER>(2);
                set => Link(2, value);
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("capnpc-csharp", "1.3.0.0"), TypeId(0x8602833409843774UL)]
    public class Image : ICapnpSerializable
    {
        public const UInt64 typeId = 0x8602833409843774UL;
        void ICapnpSerializable.Deserialize(DeserializerState arg_)
        {
            var reader = READER.create(arg_);
            Width = reader.Width;
            Height = reader.Height;
            Data = reader.Data;
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            writer.Width = Width;
            writer.Height = Height;
            writer.Data.Init(Data);
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
        }

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public IReadOnlyList<byte> Data
        {
            get;
            set;
        }

        public struct READER
        {
            readonly DeserializerState ctx;
            public READER(DeserializerState ctx)
            {
                this.ctx = ctx;
            }

            public static READER create(DeserializerState ctx) => new READER(ctx);
            public static implicit operator DeserializerState(READER reader) => reader.ctx;
            public static implicit operator READER(DeserializerState ctx) => new READER(ctx);
            public int Width => ctx.ReadDataInt(0UL, 0);
            public int Height => ctx.ReadDataInt(32UL, 0);
            public IReadOnlyList<byte> Data => ctx.ReadList(0).CastByte();
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(1, 1);
            }

            public int Width
            {
                get => this.ReadDataInt(0UL, 0);
                set => this.WriteData(0UL, value, 0);
            }

            public int Height
            {
                get => this.ReadDataInt(32UL, 0);
                set => this.WriteData(32UL, value, 0);
            }

            public ListOfPrimitivesSerializer<byte> Data
            {
                get => BuildPointer<ListOfPrimitivesSerializer<byte>>(0);
                set => Link(0, value);
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("capnpc-csharp", "1.3.0.0"), TypeId(0xa03b782c689feae5UL)]
    public class ImageStamped : ICapnpSerializable
    {
        public const UInt64 typeId = 0xa03b782c689feae5UL;
        void ICapnpSerializable.Deserialize(DeserializerState arg_)
        {
            var reader = READER.create(arg_);
            Stamp = CapnpSerializable.Create<CapnpGen.Time>(reader.Stamp);
            Pose = CapnpSerializable.Create<CapnpGen.Pose>(reader.Pose);
            Image = CapnpSerializable.Create<CapnpGen.Image>(reader.Image);
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            Stamp?.serialize(writer.Stamp);
            Pose?.serialize(writer.Pose);
            Image?.serialize(writer.Image);
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
        }

        public CapnpGen.Time Stamp
        {
            get;
            set;
        }

        public CapnpGen.Pose Pose
        {
            get;
            set;
        }

        public CapnpGen.Image Image
        {
            get;
            set;
        }

        public struct READER
        {
            readonly DeserializerState ctx;
            public READER(DeserializerState ctx)
            {
                this.ctx = ctx;
            }

            public static READER create(DeserializerState ctx) => new READER(ctx);
            public static implicit operator DeserializerState(READER reader) => reader.ctx;
            public static implicit operator READER(DeserializerState ctx) => new READER(ctx);
            public CapnpGen.Time.READER Stamp => ctx.ReadStruct(0, CapnpGen.Time.READER.create);
            public CapnpGen.Pose.READER Pose => ctx.ReadStruct(1, CapnpGen.Pose.READER.create);
            public CapnpGen.Image.READER Image => ctx.ReadStruct(2, CapnpGen.Image.READER.create);
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(0, 3);
            }

            public CapnpGen.Time.WRITER Stamp
            {
                get => BuildPointer<CapnpGen.Time.WRITER>(0);
                set => Link(0, value);
            }

            public CapnpGen.Pose.WRITER Pose
            {
                get => BuildPointer<CapnpGen.Pose.WRITER>(1);
                set => Link(1, value);
            }

            public CapnpGen.Image.WRITER Image
            {
                get => BuildPointer<CapnpGen.Image.WRITER>(2);
                set => Link(2, value);
            }
        }
    }
}