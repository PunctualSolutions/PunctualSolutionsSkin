#region

using UnityEditor;
using UnityEngine;
using static UnityEditor.AssetDatabase;

#endregion

namespace PunctualSolutions.Skin.Editor
{
    public static class SkinSettings
    {
        const string GroupKey = "PunctualSolutions.Skin";
        const string FontKey  = GroupKey + ".Font";

        public static Font Font
        {
            get => GetAsset<Font>(FontKey);
            set
            {
                ChangeFont.Change(value);
                SetAsset(FontKey, value);
            }
        }

        static SkinSettings()
        {
            if (Font)
                ChangeFont.Change(Font);
        }

        static T GetAsset<T>(string key) where T : Object
        {
            var configValue     = EditorUserSettings.GetConfigValue(key);
            var guidToAssetPath = GUIDToAssetPath(configValue);
            return LoadAssetAtPath<T>(guidToAssetPath);
        }

        static void SetAsset<T>(string name, T value) where T : Object
        {
            #if UNITY_6000
            if (TryGetGUIDAndLocalFileIdentifier(value, out var guid, out var id))
            #else
            if (TryGetGUIDAndLocalFileIdentifier(value.GetInstanceID(), out var guid, out long id))
            #endif
                EditorUserSettings.SetConfigValue(name, guid);
        }
    }
}