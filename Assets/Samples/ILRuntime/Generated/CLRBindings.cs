using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
{
    class CLRBindings
    {

        internal static ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector3> s_UnityEngine_Vector3_Binding_Binder = null;

        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            UIMangager_Binding.Register(app);
            System_String_Binding.Register(app);
            UnityEngine_Debug_Binding.Register(app);
            ProtoBufferTool_Binding.Register(app);
            Message_NetMessageBase_Binding.Register(app);
            Google_Protobuf_ByteString_Binding.Register(app);
            EventManager_Binding.Register(app);
            Mu_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_Int32_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_Int32_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_Int32_Int32_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_Int32_Binding_KeyCollection_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_Int32_Binding_KeyCollection_Binding_Enumerator_Binding.Register(app);
            UnityEngine_GameObject_Binding.Register(app);
            UnityEngine_LayerMask_Binding.Register(app);
            UnityEngine_Camera_Binding.Register(app);
            UnityEngine_Object_Binding.Register(app);
            UICanvas_Binding.Register(app);
            System_Object_Binding.Register(app);
            System_Type_Binding.Register(app);
            System_Activator_Binding.Register(app);
            System_Collections_Generic_List_1_IViewBaseAdaptor_Binding_Adaptor_Binding.Register(app);
            UnityEngine_Component_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_GameObject_Binding.Register(app);
            UnityEngine_Transform_Binding.Register(app);
            System_Text_StringBuilder_Binding.Register(app);
            System_Collections_IEnumerable_Binding.Register(app);
            System_Collections_IEnumerator_Binding.Register(app);
            System_IDisposable_Binding.Register(app);
            System_Array_Binding.Register(app);
            System_Reflection_MemberInfo_Binding.Register(app);
            System_Reflection_PropertyInfo_Binding.Register(app);
            System_Reflection_MethodBase_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_ILTypeInstance_Binding.Register(app);
            SingletonTemplate_1_ConfLoader_Binding.Register(app);
            ConfLoader_Binding.Register(app);
            System_IO_BinaryReader_Binding.Register(app);
            System_IO_Stream_Binding.Register(app);
            xbuffer_boolBuffer_Binding.Register(app);
            xbuffer_intBuffer_Binding.Register(app);
            xbuffer_stringBuffer_Binding.Register(app);
            xbuffer_floatBuffer_Binding.Register(app);
            xbuffer_longBuffer_Binding.Register(app);
            System_Int32_Binding.Register(app);
            Google_Protobuf_ProtoPreconditions_Binding.Register(app);
            Google_Protobuf_CodedOutputStream_Binding.Register(app);
            Google_Protobuf_CodedInputStream_Binding.Register(app);
            Google_Protobuf_MessageParser_1_Adapt_IMessage_Binding_Adaptor_Binding.Register(app);
            Google_Protobuf_Collections_RepeatedField_1_ByteString_Binding.Register(app);
            Google_Protobuf_Collections_MapField_2_String_UInt64_Binding.Register(app);
            Google_Protobuf_FieldCodec_Binding.Register(app);
            Google_Protobuf_Collections_MapField_2_String_UInt64_Binding_Codec_Binding.Register(app);
            SingletonTemplate_1_TimeCounter_Binding.Register(app);
            TimeCounter_Binding.Register(app);
            SingletonTemplate_1_MonoMemoryProfiler_Binding.Register(app);
            MonoMemoryProfiler_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_List_1_Int32_Binding.Register(app);
            CLRBindingTestClass_Binding.Register(app);
            CoroutineDemo_Binding.Register(app);
            UnityEngine_Time_Binding.Register(app);
            UnityEngine_WaitForSeconds_Binding.Register(app);
            System_NotSupportedException_Binding.Register(app);
            TestDelegateMethod_Binding.Register(app);
            TestDelegateFunction_Binding.Register(app);
            System_Action_1_String_Binding.Register(app);
            DelegateDemo_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Int32_Binding.Register(app);
            LitJson_JsonMapper_Binding.Register(app);
            System_Diagnostics_Stopwatch_Binding.Register(app);
            UnityEngine_Vector3_Binding.Register(app);
            System_Boolean_Binding.Register(app);
            UnityEngine_Quaternion_Binding.Register(app);
            UnityEngine_Vector2_Binding.Register(app);

            ILRuntime.CLR.TypeSystem.CLRType __clrType = null;
            __clrType = (ILRuntime.CLR.TypeSystem.CLRType)app.GetType (typeof(UnityEngine.Vector3));
            s_UnityEngine_Vector3_Binding_Binder = __clrType.ValueTypeBinder as ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector3>;
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            s_UnityEngine_Vector3_Binding_Binder = null;
        }
    }
}
