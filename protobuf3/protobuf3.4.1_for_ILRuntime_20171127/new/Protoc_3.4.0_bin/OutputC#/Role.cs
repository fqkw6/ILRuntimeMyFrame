// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: role.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using scg = global::System.Collections.Generic;
namespace Role {

  #region Messages
  /// <summary>
  ///客户端请求角色信息
  /// </summary>
  public sealed class req_get : pb::IMessage {
    private static readonly pb::MessageParser<req_get> _parser = new pb::MessageParser<req_get>(() => new req_get());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<req_get> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
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
        }
      }
    }

  }

  public sealed class role_info : pb::IMessage {
    private static readonly pb::MessageParser<role_info> _parser = new pb::MessageParser<role_info>(() => new role_info());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<role_info> Parser { get { return _parser; } }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    /// <summary>
    ///名字
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "level" field.</summary>
    public const int LevelFieldNumber = 2;
    private uint level_;
    /// <summary>
    ///官职等级
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint Level {
      get { return level_; }
      set {
        level_ = value;
      }
    }

    /// <summary>Field number for the "room_id" field.</summary>
    public const int RoomIdFieldNumber = 3;
    private uint roomId_;
    /// <summary>
    ///房间号，0表示不在房间
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint RoomId {
      get { return roomId_; }
      set {
        roomId_ = value;
      }
    }

    /// <summary>Field number for the "score" field.</summary>
    public const int ScoreFieldNumber = 4;
    private uint score_;
    /// <summary>
    ///积分
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint Score {
      get { return score_; }
      set {
        score_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (Level != 0) {
        output.WriteRawTag(16);
        output.WriteUInt32(Level);
      }
      if (RoomId != 0) {
        output.WriteRawTag(24);
        output.WriteUInt32(RoomId);
      }
      if (Score != 0) {
        output.WriteRawTag(32);
        output.WriteUInt32(Score);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Level != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Level);
      }
      if (RoomId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(RoomId);
      }
      if (Score != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Score);
      }
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
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 16: {
            Level = input.ReadUInt32();
            break;
          }
          case 24: {
            RoomId = input.ReadUInt32();
            break;
          }
          case 32: {
            Score = input.ReadUInt32();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///服务端响应角色信息
  /// </summary>
  public sealed class rsp_get : pb::IMessage {
    private static readonly pb::MessageParser<rsp_get> _parser = new pb::MessageParser<rsp_get>(() => new rsp_get());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<rsp_get> Parser { get { return _parser; } }

    /// <summary>Field number for the "info" field.</summary>
    public const int InfoFieldNumber = 1;
    private global::Role.role_info info_;
    /// <summary>
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Role.role_info Info {
      get { return info_; }
      set {
        info_ = value;
      }
    }

    /// <summary>Field number for the "map_id" field.</summary>
    public const int MapIdFieldNumber = 2;
    private uint mapId_;
    /// <summary>
    ///如果info 存在且room_id 不为0, map_id 表示地图id
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint MapId {
      get { return mapId_; }
      set {
        mapId_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (info_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Info);
      }
      if (MapId != 0) {
        output.WriteRawTag(16);
        output.WriteUInt32(MapId);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (info_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Info);
      }
      if (MapId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(MapId);
      }
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
          case 10: {
            if (info_ == null) {
              info_ = new global::Role.role_info();
            }
            input.ReadMessage(info_);
            break;
          }
          case 16: {
            MapId = input.ReadUInt32();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///客户端请求创建角色
  /// </summary>
  public sealed class req_create : pb::IMessage {
    private static readonly pb::MessageParser<req_create> _parser = new pb::MessageParser<req_create>(() => new req_create());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<req_create> Parser { get { return _parser; } }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
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
          case 10: {
            Name = input.ReadString();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///服务端相应创建角色
  /// </summary>
  public sealed class rsp_create : pb::IMessage {
    private static readonly pb::MessageParser<rsp_create> _parser = new pb::MessageParser<rsp_create>(() => new rsp_create());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<rsp_create> Parser { get { return _parser; } }

    /// <summary>Field number for the "info" field.</summary>
    public const int InfoFieldNumber = 1;
    private global::Role.role_info info_;
    /// <summary>
    ///新创建的role的信息
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Role.role_info Info {
      get { return info_; }
      set {
        info_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (info_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Info);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (info_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Info);
      }
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
          case 10: {
            if (info_ == null) {
              info_ = new global::Role.role_info();
            }
            input.ReadMessage(info_);
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///服务端推送角色信息改变
  /// </summary>
  public sealed class push_roleInfo : pb::IMessage {
    private static readonly pb::MessageParser<push_roleInfo> _parser = new pb::MessageParser<push_roleInfo>(() => new push_roleInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<push_roleInfo> Parser { get { return _parser; } }

    /// <summary>Field number for the "info" field.</summary>
    public const int InfoFieldNumber = 1;
    private global::Role.role_info info_;
    /// <summary>
    ///只发其中改变的信息
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Role.role_info Info {
      get { return info_; }
      set {
        info_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (info_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Info);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (info_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Info);
      }
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
          case 10: {
            if (info_ == null) {
              info_ = new global::Role.role_info();
            }
            input.ReadMessage(info_);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
