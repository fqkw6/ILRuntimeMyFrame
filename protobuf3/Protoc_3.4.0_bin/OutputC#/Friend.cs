// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: friend.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using scg = global::System.Collections.Generic;
namespace Friend {

  #region Messages
  /// <summary>
  ///定义好友结构
  /// </summary>
  public partial class friend_info : pb::IMessage {
    private static readonly pb::MessageParser<friend_info> _parser = new pb::MessageParser<friend_info>(() => new friend_info());
    public static pb::MessageParser<friend_info> Parser { get { return _parser; } }

    private ulong uid_;
    public ulong Uid {
      get { return uid_; }
      set {
        uid_ = value;
      }
    }

    private string name_ = "";
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    private uint level_;
    public uint Level {
      get { return level_; }
      set {
        level_ = value;
      }
    }

    private uint entry_;
    /// <summary>
    ///角色模板，头像？
    /// </summary>
    public uint Entry {
      get { return entry_; }
      set {
        entry_ = value;
      }
    }

    private bool isOnline_;
    /// <summary>
    ///是否在线
    /// </summary>
    public bool IsOnline {
      get { return isOnline_; }
      set {
        isOnline_ = value;
      }
    }

    private ulong recentOnline_;
    /// <summary>
    ///最近在线
    /// </summary>
    public ulong RecentOnline {
      get { return recentOnline_; }
      set {
        recentOnline_ = value;
      }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Uid != 0UL) {
        output.WriteRawTag(8);
        output.WriteUInt64(Uid);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (Level != 0) {
        output.WriteRawTag(24);
        output.WriteUInt32(Level);
      }
      if (Entry != 0) {
        output.WriteRawTag(32);
        output.WriteUInt32(Entry);
      }
      if (IsOnline != false) {
        output.WriteRawTag(40);
        output.WriteBool(IsOnline);
      }
      if (RecentOnline != 0UL) {
        output.WriteRawTag(48);
        output.WriteUInt64(RecentOnline);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Uid != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Uid);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Level != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Level);
      }
      if (Entry != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Entry);
      }
      if (IsOnline != false) {
        size += 1 + 1;
      }
      if (RecentOnline != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(RecentOnline);
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
            Uid = input.ReadUInt64();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
          case 24: {
            Level = input.ReadUInt32();
            break;
          }
          case 32: {
            Entry = input.ReadUInt32();
            break;
          }
          case 40: {
            IsOnline = input.ReadBool();
            break;
          }
          case 48: {
            RecentOnline = input.ReadUInt64();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///定义邀请信息
  /// </summary>
  public partial class invite_info : pb::IMessage {
    private static readonly pb::MessageParser<invite_info> _parser = new pb::MessageParser<invite_info>(() => new invite_info());
    public static pb::MessageParser<invite_info> Parser { get { return _parser; } }

    private ulong uid_;
    public ulong Uid {
      get { return uid_; }
      set {
        uid_ = value;
      }
    }

    private string name_ = "";
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    private uint entry_;
    public uint Entry {
      get { return entry_; }
      set {
        entry_ = value;
      }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Uid != 0UL) {
        output.WriteRawTag(8);
        output.WriteUInt64(Uid);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (Entry != 0) {
        output.WriteRawTag(24);
        output.WriteUInt32(Entry);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Uid != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Uid);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Entry != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Entry);
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
            Uid = input.ReadUInt64();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
          case 24: {
            Entry = input.ReadUInt32();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///客户端请求好友列表
  /// </summary>
  public partial class req_getFriends : pb::IMessage {
    private static readonly pb::MessageParser<req_getFriends> _parser = new pb::MessageParser<req_getFriends>(() => new req_getFriends());
    public static pb::MessageParser<req_getFriends> Parser { get { return _parser; } }

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

  /// <summary>
  ///服务端响应
  /// </summary>
  public partial class rsp_getFriends : pb::IMessage {
    private static readonly pb::MessageParser<rsp_getFriends> _parser = new pb::MessageParser<rsp_getFriends>(() => new rsp_getFriends());
    public static pb::MessageParser<rsp_getFriends> Parser { get { return _parser; } }

    private static readonly pb::FieldCodec<global::Friend.friend_info> _repeated_friends_codec
        = pb::FieldCodec.ForMessage(10, global::Friend.friend_info.Parser);
    private pbc::RepeatedField<global::Friend.friend_info> friends_ = new pbc::RepeatedField<global::Friend.friend_info>();
    /// <summary>
    ///好友信息
    /// </summary>
    public pbc::RepeatedField<global::Friend.friend_info> Friends {
      get { return friends_; }
      set { friends_ = value; }
    }

    private static readonly pb::FieldCodec<global::Friend.invite_info> _repeated_invites_codec
        = pb::FieldCodec.ForMessage(18, global::Friend.invite_info.Parser);
    private pbc::RepeatedField<global::Friend.invite_info> invites_ = new pbc::RepeatedField<global::Friend.invite_info>();
    /// <summary>
    ///邀请信息
    /// </summary>
    public pbc::RepeatedField<global::Friend.invite_info> Invites {
      get { return invites_; }
      set { invites_ = value; }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      friends_.WriteTo(output, _repeated_friends_codec);
      invites_.WriteTo(output, _repeated_invites_codec);
    }

    public int CalculateSize() {
      int size = 0;
      size += friends_.CalculateSize(_repeated_friends_codec);
      size += invites_.CalculateSize(_repeated_invites_codec);
      return size;
    }

    public void MergeFrom(pb::CodedInputStream input) {
      friends_.Clear();
      invites_.Clear();
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            friends_.AddEntriesFrom(input, _repeated_friends_codec);
            break;
          }
          case 18: {
            invites_.AddEntriesFrom(input, _repeated_invites_codec);
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///客户端请求添加好友
  /// </summary>
  public partial class req_add : pb::IMessage {
    private static readonly pb::MessageParser<req_add> _parser = new pb::MessageParser<req_add>(() => new req_add());
    public static pb::MessageParser<req_add> Parser { get { return _parser; } }

    private ulong uid_;
    /// <summary>
    ///如果能获取uid,优先使用uid
    /// </summary>
    public ulong Uid {
      get { return uid_; }
      set {
        uid_ = value;
      }
    }

    private string name_ = "";
    /// <summary>
    ///获取不到，就使用角色名字查找
    /// </summary>
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Uid != 0UL) {
        output.WriteRawTag(8);
        output.WriteUInt64(Uid);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Uid != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Uid);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
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
            Uid = input.ReadUInt64();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///服务端推送好友邀请
  /// </summary>
  public partial class push_invite : pb::IMessage {
    private static readonly pb::MessageParser<push_invite> _parser = new pb::MessageParser<push_invite>(() => new push_invite());
    public static pb::MessageParser<push_invite> Parser { get { return _parser; } }

    private ulong uid_;
    /// <summary>
    ///邀请人uid
    /// </summary>
    public ulong Uid {
      get { return uid_; }
      set {
        uid_ = value;
      }
    }

    private string name_ = "";
    /// <summary>
    ///邀请人名字
    /// </summary>
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Uid != 0UL) {
        output.WriteRawTag(8);
        output.WriteUInt64(Uid);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Uid != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Uid);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
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
            Uid = input.ReadUInt64();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///客户端确认邀请
  /// </summary>
  public partial class req_inviteSure : pb::IMessage {
    private static readonly pb::MessageParser<req_inviteSure> _parser = new pb::MessageParser<req_inviteSure>(() => new req_inviteSure());
    public static pb::MessageParser<req_inviteSure> Parser { get { return _parser; } }

    private ulong uid_;
    /// <summary>
    ///邀请人uid
    /// </summary>
    public ulong Uid {
      get { return uid_; }
      set {
        uid_ = value;
      }
    }

    private bool sure_;
    /// <summary>
    ///是否接受邀请
    /// </summary>
    public bool Sure {
      get { return sure_; }
      set {
        sure_ = value;
      }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Uid != 0UL) {
        output.WriteRawTag(8);
        output.WriteUInt64(Uid);
      }
      if (Sure != false) {
        output.WriteRawTag(16);
        output.WriteBool(Sure);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Uid != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Uid);
      }
      if (Sure != false) {
        size += 1 + 1;
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
            Uid = input.ReadUInt64();
            break;
          }
          case 16: {
            Sure = input.ReadBool();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///服务端推送新的好友
  /// </summary>
  public partial class push_newFriends : pb::IMessage {
    private static readonly pb::MessageParser<push_newFriends> _parser = new pb::MessageParser<push_newFriends>(() => new push_newFriends());
    public static pb::MessageParser<push_newFriends> Parser { get { return _parser; } }

    private global::Friend.friend_info info_;
    /// <summary>
    ///新加的好友信息
    /// </summary>
    public global::Friend.friend_info Info {
      get { return info_; }
      set {
        info_ = value;
      }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (info_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Info);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (info_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Info);
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
          case 10: {
            if (info_ == null) {
              info_ = new global::Friend.friend_info();
            }
            input.ReadMessage(info_);
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///客户端请求删除好友
  /// </summary>
  public partial class req_del : pb::IMessage {
    private static readonly pb::MessageParser<req_del> _parser = new pb::MessageParser<req_del>(() => new req_del());
    public static pb::MessageParser<req_del> Parser { get { return _parser; } }

    private ulong uid_;
    public ulong Uid {
      get { return uid_; }
      set {
        uid_ = value;
      }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Uid != 0UL) {
        output.WriteRawTag(8);
        output.WriteUInt64(Uid);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Uid != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Uid);
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
            Uid = input.ReadUInt64();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///服务端推送删除好友
  /// </summary>
  public partial class push_del : pb::IMessage {
    private static readonly pb::MessageParser<push_del> _parser = new pb::MessageParser<push_del>(() => new push_del());
    public static pb::MessageParser<push_del> Parser { get { return _parser; } }

    private ulong uid_;
    public ulong Uid {
      get { return uid_; }
      set {
        uid_ = value;
      }
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Uid != 0UL) {
        output.WriteRawTag(8);
        output.WriteUInt64(Uid);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Uid != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Uid);
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
            Uid = input.ReadUInt64();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
