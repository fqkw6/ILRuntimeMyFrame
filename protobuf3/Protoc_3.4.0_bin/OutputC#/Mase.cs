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
  public partial class myperson : pb::IMessage {
    private static readonly pb::MessageParser<myperson> _parser = new pb::MessageParser<myperson>(() => new myperson());
    public static pb::MessageParser<myperson> Parser { get { return _parser; } }

    private uint id_;
    public uint Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    private static readonly pb::FieldCodec<pb::ByteString> _repeated_arrayValue_codec
        = pb::FieldCodec.ForBytes(18);
    private pbc::RepeatedField<pb::ByteString> arrayValue_ = new pbc::RepeatedField<pb::ByteString>();
    public pbc::RepeatedField<pb::ByteString> ArrayValue {
      get { return arrayValue_; }
      set { arrayValue_ = value; }
    }

    private static readonly pbc::MapField<string, ulong>.Codec _map_projects_codec
        = new pbc::MapField<string, ulong>.Codec(pb::FieldCodec.ForString(10), pb::FieldCodec.ForUInt64(16), 26);
    private readonly pbc::MapField<string, ulong> projects_ = new pbc::MapField<string, ulong>();
    public pbc::MapField<string, ulong> Projects {
      get { return projects_; }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(Id);
      }
      arrayValue_.WriteTo(output, _repeated_arrayValue_codec);
      projects_.WriteTo(output, _map_projects_codec);
    }

    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Id);
      }
      size += arrayValue_.CalculateSize(_repeated_arrayValue_codec);
      size += projects_.CalculateSize(_map_projects_codec);
      return size;
    }

    public void MergeFrom(pb::CodedInputStream input) {
      arrayValue_.Clear();
      projects_.Clear();
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