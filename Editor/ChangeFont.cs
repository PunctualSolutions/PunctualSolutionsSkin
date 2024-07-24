#region

using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

#endregion

namespace PunctualSolutions.Skin.Editor
{
    public static class ChangeFont
    {
        public static void Change(Font font)
        {
            Change(AssetDatabase.GetAssetPath(font));
        }

        [MenuItem("Font/Test")]
        public static void ChangeA()
        {
            Change("Assets/Resources/Font/CascadiaCodePL.ttf");
        }

        [MenuItem("Font/Test2")]
        public static void ChangeB()
        {
            Change("Assets/Resources/Font/NotoSansMonoCJKsc-Bold.otf");
        }

        static void Change(string fontPath)
        {
            var fieldInfo = Assembly.Load("UnityEditor.CoreModule").GetType("UnityEditor.UIElements.UIElementsEditorUtility")
                                    .GetField("s_DefaultCommonDarkStyleSheet", BindingFlags.Static | BindingFlags.NonPublic);
            var styleSheet = (StyleSheet)fieldInfo!.GetValue(null);
            var strings    = (string[])typeof(StyleSheet).GetField("strings", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(styleSheet);
            for (var i = 0; i < strings.Length; i++)
            {
                if (strings[i] == "UIPackageResources/Fonts/Inter/Inter-Regular SDF.asset") strings[i] = fontPath;
                if (strings[i] == "Fonts/System/System Normal.ttf") strings[i]                         = fontPath;
            } 
        }
    }
}