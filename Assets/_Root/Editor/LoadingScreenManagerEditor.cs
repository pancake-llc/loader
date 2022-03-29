#if UNITY_EDITOR
using System.Collections.Generic;
using Pancake.Loader;
using UnityEditor;
using UnityEngine;

namespace Pancake.LoaderEditor
{
    [CustomEditor(typeof(LoadingScreenManager))]
    public class LoadingScreenManagerEditor : Editor
    {
        private LoadingScreenManager _loadingScreenManager;
        private readonly List<string> _prefabLoadingNames = new List<string>();

        private void OnEnable() { Load(); }

        private void Load()
        {
            _loadingScreenManager = (LoadingScreenManager)target;
            _loadingScreenManager.loadingScreens.Clear();
            _prefabLoadingNames.Clear();
            var results = Resources.LoadAll<GameObject>("[Loader]/Prefabs");
            foreach (var obj in results)
            {
                _loadingScreenManager.loadingScreens.Add(obj);
                _prefabLoadingNames.Add(obj.name);
            }
        }

        public override void OnInspectorGUI()
        {
            if (_loadingScreenManager == null || _prefabLoadingNames.Count == 0) Load();

            var customSkin = (GUISkin)Resources.Load("loader-dark-skin");

            var dontDestroyOnLoad = serializedObject.FindProperty("dontDestroyOnLoad");
            var prefabLoadingName = serializedObject.FindProperty("prefabLoadingName");
            var selectedLoadingIndex = serializedObject.FindProperty("selectedLoadingIndex");
            var unityEventOnBegin = serializedObject.FindProperty("onBeginEvents");
            var unityEventOnFinish = serializedObject.FindProperty("onFinishEvents");

            if (_prefabLoadingNames.Count == 1 || _prefabLoadingNames.Count >= 1)
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.Space(4);
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Selected Screen"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                selectedLoadingIndex.intValue = EditorGUILayout.Popup(selectedLoadingIndex.intValue, _prefabLoadingNames.ToArray());
                prefabLoadingName.stringValue = _loadingScreenManager.loadingScreens[selectedLoadingIndex.intValue]
                    .ToString()
                    .Replace(" (UnityEngine.GameObject)", "")
                    .Trim();
                GUILayout.EndHorizontal();
                GUILayout.Space(6);
                EditorGUI.indentLevel = 1;
                EditorGUILayout.PropertyField(dontDestroyOnLoad, new GUIContent("Don't Destroy On Load"), true);
                EditorGUI.indentLevel = 0;
                GUILayout.Space(4);
                EditorGUILayout.PropertyField(unityEventOnBegin, new GUIContent("OnBegin"), true);
                GUILayout.Space(4);
                EditorGUILayout.PropertyField(unityEventOnFinish, new GUIContent("OnFinish"), true);
                GUILayout.Space(4);
                GUILayout.EndVertical();
            }

            else EditorGUILayout.HelpBox("There isn't any loading screen prefab in Resources > [Loader] > Prefabs folder!", MessageType.Warning);

            GUILayout.Space(6);
            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif