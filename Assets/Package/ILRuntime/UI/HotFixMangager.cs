using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;
using System.IO;
public class HotFixMangager : Singleton<HotFixMangager>
{

    AppDomain mAppDomain;

    public void InitApp(byte [] dllBytes,byte [] pdbBytes)
    {
        mAppDomain = new AppDomain();
        MemoryStream dll = (dllBytes != null) ? new MemoryStream(dllBytes) : null;
        MemoryStream pdb = (pdbBytes != null) ? new MemoryStream(pdbBytes) : null;
        mAppDomain.LoadAssembly(dll, pdb, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        /// todo
    }

    public AppDomain GetAppDomain()
    {
        return mAppDomain;
    }
    public override void Dispose()
    {
        
    }
}
