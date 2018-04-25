using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class UnityExtensions
{
    static public void SafeDestroy(this GameObject obj)
    {
        if (obj != null)
        {
            if (Application.isEditor)
            {
                Object.DestroyImmediate(obj);
            }
            else
            {
                Object.Destroy(obj);
            }
        }
    }

    static public void SafeDestroy(this Component component)
    {
        if (component != null)
        {
            SafeDestroy(component.gameObject);
        }
    }
}
