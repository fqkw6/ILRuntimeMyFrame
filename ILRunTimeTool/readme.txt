����google�ٷ�3.4.1�汾��protobuf c#-runtime�Լ�3.4.0�汾��protoc�޸Ķ�����������ILRuntime��ͬʱ�Ƴ��˲����õĹ��ܣ����������ɴ�������.
������ILRuntimeʱ��protobuf-runtime���������̣������ȸ������ý���pb�Ĵ�������CLR�ϣ��ṩ����Ľ����ٶ�; ��.proto���ɵ�c#��������ȸ�dll����(���ȸ�).

��֪���ƣ�
  1��ֻ֧��proto3Э�飬��֧��proto2
  2, ��֧��WellKnownTypes��Ҳ����.proto�ﲻ֧��Any��TimeSpan��Duration��google.protobuf��Ԥ�����.proto����
  3����֧��JsonParser��FileDescriptor�ȷ��书��
  4��.proto�ﲻ֧��Map��oneof - ʵ����Ӧ�����õ��˷��䣬ûϸ������ȷ���ܷ��ڲ�֧�ַ�������֧�����.
  5��protoc.exe��֧������·��,Ҳ����.proto��Ҫ��������Ŀ¼��

�޸ĵ����ݣ�
  ע��runtime���ֱ����Ͽ��ԸĶ����٣��Ƴ�IMessage�ĸ��ּ̳кͲ����õ��Ľӿڷ����������±��������У��������ڸ���ϰ�ߣ��Ѳ��ᣨ���������protoc���ɵĴ��룩�õ���runtime����ȫ���������ˣ�
      ����RelfectionĿ¼��WellKnownĿ¼��JsonFromatterĿ¼�ȶ�����������. �ⲿ�ֲ�����Ҳ��û�κ�����ģ����������������ϣ����protoc�����Ĳ��ֹ��ܼӻ���Ҳ�����㡣

  1, runtime
     ����.net framework 3.5
  
  2, runtime & protroc 
     �Ƴ��˷��䲿��
  
  3��runtime 
     �Ƴ���WellKnownTypes
     ע: WellKnownTypesֻ�Ǵ�googleĬ�϶����һ��(����).proto�ļ����ɵĴ��룬��runtime�Դ�����Ϊ���Ǹ���runtime,�����Դ�����Щ���루���ϵ�protoc���ɣ����б�������������Ҫ������ܣ�����һ�±������ͺã��������鷳����������.

  4, runtime & protoc
     �Ƴ���Clone��(������Ŀ)�ò����ķ���
  
  5, runtime & protoc
     �Ƴ��˸���(������Ŀ)����Ҫ�ļ̳кͽӿڣ��Ծ���ILRuntime�Ŀ���̳�.
  
  6, runtime & protoc
     �Ƴ���ToString()��������ΪĬ��ʵ����Ҫ�õ�FileDescriptor; �ṩ��Dumper.DumpAsString()���������ToString��������ӡһ��pb obj������filed��name��value�����ڵ���.
     ע��Dumperֻ����Ͼ������protoc���ɴ����������ʹ�ã���Ϊ�����и�����: pb obj�����е�public NonStatic getter����.proto�ж����field����ԭ��protoc���ɵĴ��벢�������������.


Ŀ¼�ṹ��
1��old - δ�޸ĵ�ԭ��pb:
   1.1��Google.Protobuf
       protobuf��runtime

   1.2  Protoc_3.4.0_bin
       Ԥ���ɺõ�windows��.proto����������.proto����c#�ļ�

   1.3 protoc_3.4.0_src
       protocԴ�룬������vs2013����

2, new - �޸ĺ�Ŀ�����ILRuntime��pb:
  2.1��Google.Protobuf
       protobuf��runtime������������
  
  2.2, Protoc_3.4.0_bin
      Ԥ���ɺõ�windows��.proto����������.proto����c#�ļ�����Щ�����ļ������ȸ�����

  2.3 protoc_3.4.0_src
     �޸ĺ��protocԴ�룬������vs2013����
  
  *2.4, Adapt_IMessage.cs��AdaptHelper.cs
      ILRuntime����̳���������ֻ��Ҫ����IMessage�ӿھ��У�.proto���ɴ��붼�Ǽ̳�������ӿ�.�ŵ������̣�ILRutime��ʼ��ʱע���������̳�����
      �������ILRuntime������Ҫ���ļ�.

3, test - �����õ�.proto��test case
