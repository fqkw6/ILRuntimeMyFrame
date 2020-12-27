using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatchesUtil 
{
    static Dictionary<Material, Material> s_SharedMaterials = new Dictionary<Material, Material>();

    static public Material GetStoredSharedMaterial(Material material)
    {
        Material mat = null;
        if (material != null)
        {
            if (s_SharedMaterials.TryGetValue(material, out mat))
            {
                mat = new Material(material);
                s_SharedMaterials[material] = mat;
            }
        }
        return mat;
    }

    static public Material GetDefaultMaterial()
    {
        return Resources.Load<Material>("Material/SpritesBatches");
    }

    static public Font GetDefaultFont()
    {
        return Resources.GetBuiltinResource<Font>("Arial.ttf");
    }
}
