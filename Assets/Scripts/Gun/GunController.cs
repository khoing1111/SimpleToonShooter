﻿using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Gun
{
    public class GunController : ScriptableObject
    {
        [MenuItem("Tools/MyTool/Do It in C#")]
        static void DoIt()
        {
            EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
        }
    }
}