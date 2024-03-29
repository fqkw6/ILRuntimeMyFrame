// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: mase.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using scg = global::System.Collections.Generic;
namespace Message {

  #region Messages
  /// <summary>
  /// 消息开始
  /// </summary>
  public sealed class myperson : pb::IMessage {
    private static readonly pb::MessageParser<myperson> _parser = new pb::MessageParser<myperson>(() => new myperson());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<myperson> Parser { get { return _parser; } }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private uint id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "array_value" field.</summary>
    public const int ArrayValueFieldNumber = 2;
    private static readonly pb::FieldCodec<pb::ByteString> _repeated_arrayValue_codec
        = pb::FieldCodec.ForBytes(18);
    private readonly pbc::RepeatedField<pb::ByteString> arrayValue_ = new pbc::RepeatedField<pb::ByteString>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<pb::ByteString> ArrayValue {
      get { return arrayValue_; }
    }

    /// <summary>Field number for the "projects" field.</summary>
    public const int ProjectsFieldNumber = 3;
    private static readonly pbc::MapField<string, ulong>.Codec _map_projects_codec
        = new pbc::MapField<string, ulong>.Codec(pb::FieldCodec.ForString(10), pb::FieldCodec.ForUInt64(16), 26);
    private readonly pbc::MapField<string, ulong> projects_ = new pbc::MapField<string, ulong>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::MapField<string, ulong> Projects {
      get { return projects_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(Id);
      }
      arrayValue_.WriteTo(output, _repeated_arrayValue_codec);
      projects_.WriteTo(output, _map_projects_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Id);
      }
      size += arrayValue_.CalculateSize(_repeated_arrayValue_codec);
      size += projects_.CalculateSize(_map_projects_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Id = input.ReadUInt32();
            break;
          }
          case 18: {
            arrayValue_.AddEntriesFrom(input, _repeated_arrayValue_codec);
            break;
          }
          case 26: {
            projects_.AddEntriesFrom(input, _map_projects_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
