using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class UICanvas_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(global::UICanvas);
            args = new Type[]{typeof(UnityEngine.RenderMode), typeof(UnityEngine.Camera), typeof(System.Int32)};
            method = type.GetMethod("SetCanvas", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetCanvas_0);
            args = new Type[]{typeof(UnityEngine.UI.CanvasScaler.ScaleMode), typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode), typeof(System.Single)};
            method = type.GetMethod("SetCanvasScaler", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetCanvasScaler_1);
            args = new Type[]{typeof(System.String)};
            method = type.GetMethod("SetLayer", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetLayer_2);


        }


        static StackObject* SetCanvas_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 4);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @order = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.Camera @camera = (UnityEngine.Camera)typeof(UnityEngine.Camera).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            UnityEngine.RenderMode @renderMode = (UnityEngine.RenderMode)typeof(UnityEngine.RenderMode).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            global::UICanvas instance_of_this_method = (global::UICanvas)typeof(global::UICanvas).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetCanvas(@renderMode, @camera, @order);

            return __ret;
        }

        static StackObject* SetCanvasScaler_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 4);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Single @matchWidthOrHeight = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.UI.CanvasScaler.ScreenMatchMode @screenMatchMode = (UnityEngine.UI.CanvasScaler.ScreenMatchMode)typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            UnityEngine.UI.CanvasScaler.ScaleMode @scaleMode = (UnityEngine.UI.CanvasScaler.ScaleMode)typeof(UnityEngine.UI.CanvasScaler.ScaleMode).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            global::UICanvas instance_of_this_method = (global::UICanvas)typeof(global::UICanvas).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetCanvasScaler(@scaleMode, @screenMatchMode, @matchWidthOrHeight);

            return __ret;
        }

        static StackObject* SetLayer_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @layerName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            global::UICanvas instance_of_this_method = (global::UICanvas)typeof(global::UICanvas).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetLayer(@layerName);

            return __ret;
        }



    }
}
