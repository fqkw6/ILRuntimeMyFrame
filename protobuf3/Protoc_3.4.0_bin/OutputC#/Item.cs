// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: item.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using scg = global::System.Collections.Generic;
namespace Item {

  #region Messages
  public partial class one_item : pb::IMessage {
    private static readonly pb::MessageParser<one_item> _parser = new pb::MessageParser<one_item>(() => new one_item());
    public static pb::MessageParser<one_item> Parser { get { return _parser; } }

    private uint id_;
    /// <summary>
    ///物品id，见cfg_item
    /// </summary>
    public uint Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    private uint amount_;
    /// <summary>
    ///数量
    /// </summary>
    public uint Amount {
      get { return amount_; }
      set {
        amount_ = value;
      }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(Id);
      }
      if (Amount != 0) {
        output.WriteRawTag(16);
        output.WriteUInt32(Amount);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Id);
      }
      if (Amount != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Amount);
      }
      return size;
    }

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
          case 16: {
            Amount = input.ReadUInt32();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///服务端推送消息,物品变化
  /// </summary>
  public partial class push_itemChange : pb::IMessage {
    private static readonly pb::MessageParser<push_itemChange> _parser = new pb::MessageParser<push_itemChange>(() => new push_itemChange());
    public static pb::MessageParser<push_itemChange> Parser { get { return _parser; } }

    private static readonly pb::FieldCodec<global::Item.one_item> _repeated_changes_codec
        = pb::FieldCodec.ForMessage(10, global::Item.one_item.Parser);
    private pbc::RepeatedField<global::Item.one_item> changes_ = new pbc::RepeatedField<global::Item.one_item>();
    /// <summary>
    ///变化的物品
    /// </summary>
    public pbc::RepeatedField<global::Item.one_item> Changes {
      get { return changes_; }
      set { changes_ = value; }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      changes_.WriteTo(output, _repeated_changes_codec);
    }

    public int CalculateSize() {
      int size = 0;
      size += changes_.CalculateSize(_repeated_changes_codec);
      return size;
    }

    public void MergeFrom(pb::CodedInputStream input) {
      changes_.Clear();
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            changes_.AddEntriesFrom(input, _repeated_changes_codec);
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///客户端请求物品
  /// </summary>
  public partial class req_getItem : pb::IMessage {
    private static readonly pb::MessageParser<req_getItem> _parser = new pb::MessageParser<req_getItem>(() => new req_getItem());
    public static pb::MessageParser<req_getItem> Parser { get { return _parser; } }

    public void WriteTo(pb::CodedOutputStream output) {
    }

    public int CalculateSize() {
      int size = 0;
      return size;
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
        }
      }
    }

  }

  public partial class rsp_getItem : pb::IMessage {
    private static readonly pb::MessageParser<rsp_getItem> _parser = new pb::MessageParser<rsp_getItem>(() => new rsp_getItem());
    public static pb::MessageParser<rsp_getItem> Parser { get { return _parser; } }

    private static readonly pb::FieldCodec<global::Item.one_item> _repeated_items_codec
        = pb::FieldCodec.ForMessage(10, global::Item.one_item.Parser);
    private pbc::RepeatedField<global::Item.one_item> items_ = new pbc::RepeatedField<global::Item.one_item>();
    /// <summary>
    /// 所有物品
    /// </summary>
    public pbc::RepeatedField<global::Item.one_item> Items {
      get { return items_; }
      set { items_ = value; }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      items_.WriteTo(output, _repeated_items_codec);
    }

    public int CalculateSize() {
      int size = 0;
      size += items_.CalculateSize(_repeated_items_codec);
      return size;
    }

    public void MergeFrom(pb::CodedInputStream input) {
      items_.Clear();
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            items_.AddEntriesFrom(input, _repeated_items_codec);
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///客户端请求使用物品
  /// </summary>
  public partial class req_useItem : pb::IMessage {
    private static readonly pb::MessageParser<req_useItem> _parser = new pb::MessageParser<req_useItem>(() => new req_useItem());
    public static pb::MessageParser<req_useItem> Parser { get { return _parser; } }

    private uint id_;
    /// <summary>
    ///物品id
    /// </summary>
    public uint Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    private uint amount_;
    /// <summary>
    ///使用数量
    /// </summary>
    public uint Amount {
      get { return amount_; }
      set {
        amount_ = value;
      }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(Id);
      }
      if (Amount != 0) {
        output.WriteRawTag(16);
        output.WriteUInt32(Amount);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Id);
      }
      if (Amount != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Amount);
      }
      return size;
    }

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
          case 16: {
            Amount = input.ReadUInt32();
            break;
          }
        }
      }
    }

  }

  public partial class rsp_useItem : pb::IMessage {
    private static readonly pb::MessageParser<rsp_useItem> _parser = new pb::MessageParser<rsp_useItem>(() => new rsp_useItem());
    public static pb::MessageParser<rsp_useItem> Parser { get { return _parser; } }

    public void WriteTo(pb::CodedOutputStream output) {
    }

    public int CalculateSize() {
      int size = 0;
      return size;
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
