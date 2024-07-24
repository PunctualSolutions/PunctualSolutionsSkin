#region

using System.Drawing.Printing;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

#endregion

namespace PunctualSolutions.Skin.Editor
{
    public class SkinSettingsProvider : SettingsProvider
    {
        const string FontKey = "font";

        StyleSheet      _styleSheet;
        VisualTreeAsset _template;

        SkinSettingsProvider() : base("PunctualSolutions/Skin", SettingsScope.User)
        {
        }

        const string Path = "Packages/cn.punctual-solutions.skin/UI/Skin";

        VisualTreeAsset Template
        {
            get
            {
                if (!_template) _template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{Path}.uxml");
                return _template;
            }
        }


        StyleSheet StyleSheet
        {
            get
            {
                if (!_styleSheet) _styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>($"{Path}.uss");
                return _styleSheet;
            }
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            Template.CloneTree(rootElement);
            rootElement.styleSheets.Add(StyleSheet);
            var field = rootElement.Q<ObjectField>(FontKey);
            field.SetValueWithoutNotify(SkinSettings.Font);
            field.RegisterValueChangedCallback(x =>
            {
                SkinSettings.Font = x.newValue as Font;
            });
        }

        public override void OnDeactivate()
        {
        }

        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            var provider = new SkinSettingsProvider();
            return provider;
        }
    }
}