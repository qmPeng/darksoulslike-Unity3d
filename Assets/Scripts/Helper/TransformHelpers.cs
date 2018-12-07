using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformHelpers {

	//public static void hihi(this Transform trans, string say)
 //   {
 //       Debug.Log(trans.name + " says "+ say);
 //   }

    public static Transform DeepFind(this Transform parent, string targetName)
    {
        Transform tempTrans = null;
        foreach (Transform child in parent)
        {
            //Debug.Log(child.name);
            //DeepFind(child, targetName);
            if (child.name == targetName)
            {
                return child;
            }
            else
            {
               tempTrans = DeepFind(child, targetName);
                if(tempTrans != null)
                {
                    return tempTrans;
                }
            }
        }
        return null;
    }
}
