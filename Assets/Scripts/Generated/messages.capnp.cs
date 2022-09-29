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

    [System.CodeDom.Compiler.GeneratedCode("capnpc-csharp", "1.3.0.0"), TypeId(0xb63743cb4672477bUL)]
    public class Color : ICapnpSerializable
    {
        public const UInt64 typeId = 0xb63743cb4672477bUL;
        void ICapnpSerializable.Deserialize(DeserializerState arg_)
        {
            var reader = READER.create(arg_);
            R = reader.R;
            G = reader.G;
            B = reader.B;
            A = reader.A;
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            writer.R = R;
            writer.G = G;
            writer.B = B;
            writer.A = A;
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
        }

        public float R
        {
            get;
            set;
        }

        public float G
        {
            get;
            set;
        }

        public float B
        {
            get;
            set;
        }

        public float A
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
            public float R => ctx.ReadDataFloat(0UL, 0F);
            public float G => ctx.ReadDataFloat(32UL, 0F);
            public float B => ctx.ReadDataFloat(64UL, 0F);
            public float A => ctx.ReadDataFloat(96UL, 0F);
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(2, 0);
            }

            public float R
            {
                get => this.ReadDataFloat(0UL, 0F);
                set => this.WriteData(0UL, value, 0F);
            }

            public float G
            {
                get => this.ReadDataFloat(32UL, 0F);
                set => this.WriteData(32UL, value, 0F);
            }

            public float B
            {
                get => this.ReadDataFloat(64UL, 0F);
                set => this.WriteData(64UL, value, 0F);
            }

            public float A
            {
                get => this.ReadDataFloat(96UL, 0F);
                set => this.WriteData(96UL, value, 0F);
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("capnpc-csharp", "1.3.0.0"), TypeId(0xa91efae1aa564815UL)]
    public class PBRMetallicRoughness : ICapnpSerializable
    {
        public const UInt64 typeId = 0xa91efae1aa564815UL;
        void ICapnpSerializable.Deserialize(DeserializerState arg_)
        {
            var reader = READER.create(arg_);
            BaseColorFactor = CapnpSerializable.Create<CapnpGen.Color>(reader.BaseColorFactor);
            MetallicFactor = reader.MetallicFactor;
            RoughnessFactor = reader.RoughnessFactor;
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            BaseColorFactor?.serialize(writer.BaseColorFactor);
            writer.MetallicFactor = MetallicFactor;
            writer.RoughnessFactor = RoughnessFactor;
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
        }

        public CapnpGen.Color BaseColorFactor
        {
            get;
            set;
        }

        public float MetallicFactor
        {
            get;
            set;
        }

        public float RoughnessFactor
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
            public CapnpGen.Color.READER BaseColorFactor => ctx.ReadStruct(0, CapnpGen.Color.READER.create);
            public float MetallicFactor => ctx.ReadDataFloat(0UL, 0F);
            public float RoughnessFactor => ctx.ReadDataFloat(32UL, 0F);
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(1, 1);
            }

            public CapnpGen.Color.WRITER BaseColorFactor
            {
                get => BuildPointer<CapnpGen.Color.WRITER>(0);
                set => Link(0, value);
            }

            public float MetallicFactor
            {
                get => this.ReadDataFloat(0UL, 0F);
                set => this.WriteData(0UL, value, 0F);
            }

            public float RoughnessFactor
            {
                get => this.ReadDataFloat(32UL, 0F);
                set => this.WriteData(32UL, value, 0F);
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
            HomogeneousMatrix = reader.HomogeneousMatrix;
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            writer.HomogeneousMatrix.Init(HomogeneousMatrix);
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
        }

        public IReadOnlyList<float> HomogeneousMatrix
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
            public IReadOnlyList<float> HomogeneousMatrix => ctx.ReadList(0).CastFloat();
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(0, 1);
            }

            public ListOfPrimitivesSerializer<float> HomogeneousMatrix
            {
                get => BuildPointer<ListOfPrimitivesSerializer<float>>(0);
                set => Link(0, value);
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

    [System.CodeDom.Compiler.GeneratedCode("capnpc-csharp", "1.3.0.0"), TypeId(0xefc057b3fefd88b4UL)]
    public class SimpleModel : ICapnpSerializable
    {
        public const UInt64 typeId = 0xefc057b3fefd88b4UL;
        void ICapnpSerializable.Deserialize(DeserializerState arg_)
        {
            var reader = READER.create(arg_);
            Mesh = CapnpSerializable.Create<CapnpGen.Mesh>(reader.Mesh);
            Material = CapnpSerializable.Create<CapnpGen.PBRMetallicRoughness>(reader.Material);
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            Mesh?.serialize(writer.Mesh);
            Material?.serialize(writer.Material);
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
        }

        public CapnpGen.Mesh Mesh
        {
            get;
            set;
        }

        public CapnpGen.PBRMetallicRoughness Material
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
            public CapnpGen.Mesh.READER Mesh => ctx.ReadStruct(0, CapnpGen.Mesh.READER.create);
            public CapnpGen.PBRMetallicRoughness.READER Material => ctx.ReadStruct(1, CapnpGen.PBRMetallicRoughness.READER.create);
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(0, 2);
            }

            public CapnpGen.Mesh.WRITER Mesh
            {
                get => BuildPointer<CapnpGen.Mesh.WRITER>(0);
                set => Link(0, value);
            }

            public CapnpGen.PBRMetallicRoughness.WRITER Material
            {
                get => BuildPointer<CapnpGen.PBRMetallicRoughness.WRITER>(1);
                set => Link(1, value);
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("capnpc-csharp", "1.3.0.0"), TypeId(0xff304f417794565fUL)]
    public class SimpleModelStamped : ICapnpSerializable
    {
        public const UInt64 typeId = 0xff304f417794565fUL;
        void ICapnpSerializable.Deserialize(DeserializerState arg_)
        {
            var reader = READER.create(arg_);
            Stamp = CapnpSerializable.Create<CapnpGen.Time>(reader.Stamp);
            Pose = CapnpSerializable.Create<CapnpGen.Pose>(reader.Pose);
            Mesh = CapnpSerializable.Create<CapnpGen.SimpleModel>(reader.Mesh);
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

        public CapnpGen.SimpleModel Mesh
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
            public CapnpGen.SimpleModel.READER Mesh => ctx.ReadStruct(2, CapnpGen.SimpleModel.READER.create);
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

            public CapnpGen.SimpleModel.WRITER Mesh
            {
                get => BuildPointer<CapnpGen.SimpleModel.WRITER>(2);
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
            Data = reader.Data;
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            writer.Data.Init(Data);
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
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
            public IReadOnlyList<byte> Data => ctx.ReadList(0).CastByte();
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(0, 1);
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

    [System.CodeDom.Compiler.GeneratedCode("capnpc-csharp", "1.3.0.0"), TypeId(0xfe5ca357f9e345ffUL)]
    public class FiducialDef : ICapnpSerializable
    {
        public const UInt64 typeId = 0xfe5ca357f9e345ffUL;
        void ICapnpSerializable.Deserialize(DeserializerState arg_)
        {
            var reader = READER.create(arg_);
            Id = reader.Id;
            Pose = CapnpSerializable.Create<CapnpGen.Pose>(reader.Pose);
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            writer.Id = Id;
            Pose?.serialize(writer.Pose);
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
        }

        public string Id
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
            public string Id => ctx.ReadText(0, null);
            public CapnpGen.Pose.READER Pose => ctx.ReadStruct(1, CapnpGen.Pose.READER.create);
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(0, 2);
            }

            public string Id
            {
                get => this.ReadText(0, null);
                set => this.WriteText(0, value, null);
            }

            public CapnpGen.Pose.WRITER Pose
            {
                get => BuildPointer<CapnpGen.Pose.WRITER>(1);
                set => Link(1, value);
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("capnpc-csharp", "1.3.0.0"), TypeId(0xa8a45c0f2e4838d3UL)]
    public class FiducialTrackedObject : ICapnpSerializable
    {
        public const UInt64 typeId = 0xa8a45c0f2e4838d3UL;
        void ICapnpSerializable.Deserialize(DeserializerState arg_)
        {
            var reader = READER.create(arg_);
            Markers = reader.Markers?.ToReadOnlyList(_ => CapnpSerializable.Create<CapnpGen.FiducialDef>(_));
            applyDefaults();
        }

        public void serialize(WRITER writer)
        {
            writer.Markers.Init(Markers, (_s1, _v1) => _v1?.serialize(_s1));
        }

        void ICapnpSerializable.Serialize(SerializerState arg_)
        {
            serialize(arg_.Rewrap<WRITER>());
        }

        public void applyDefaults()
        {
        }

        public IReadOnlyList<CapnpGen.FiducialDef> Markers
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
            public IReadOnlyList<CapnpGen.FiducialDef.READER> Markers => ctx.ReadList(0).Cast(CapnpGen.FiducialDef.READER.create);
        }

        public class WRITER : SerializerState
        {
            public WRITER()
            {
                this.SetStruct(0, 1);
            }

            public ListOfStructsSerializer<CapnpGen.FiducialDef.WRITER> Markers
            {
                get => BuildPointer<ListOfStructsSerializer<CapnpGen.FiducialDef.WRITER>>(0);
                set => Link(0, value);
            }
        }
    }
}