using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CMCore.Utilities.Extensions.Unity
{
    [AddComponentMenu("Button (CGUI)")]
    public class CGUIButton : Button
    {
        public bool childrenTargetImages;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            var targetColor =
                state == SelectionState.Disabled ? colors.disabledColor :
                state == SelectionState.Highlighted ? colors.highlightedColor :
                state == SelectionState.Normal ? colors.normalColor :
                state == SelectionState.Pressed ? colors.pressedColor :
                state == SelectionState.Selected ? colors.selectedColor : Color.white;

            var self = GetComponent<Graphic>();
            var children = GetComponentsInChildren<Graphic>().ToList();

            if (children.Contains(self))
                children.Remove(self);
            
            self.CrossFadeColor(targetColor, instant ? 0f : colors.fadeDuration, true, true);
            
            
            if (!childrenTargetImages) return;
            
            foreach (var graphic in children)
            {
                graphic.CrossFadeColor(targetColor, instant ? 0f : colors.fadeDuration, true, true);
            }
        }
    }


    #if UNITY_EDITOR
    [CustomEditor(typeof(CGUIButton)), CanEditMultipleObjects]
    public class CGUIButtonEditor : UnityEditor.Editor
    {
        private SerializedProperty _childrenTargetImages;
        private SerializedProperty _onClick;
        private SerializedProperty _interactable;

        private bool showProperties = false; // Foldout state
        private void OnEnable()
        {
            _childrenTargetImages = serializedObject.FindProperty("childrenTargetImages");
            _onClick = serializedObject.FindProperty("m_OnClick");
            _interactable = serializedObject.FindProperty("m_Interactable");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
          
            showProperties = EditorGUILayout.Foldout(showProperties, "Properties", true);

            if (showProperties)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.BeginVertical();
                EditorGUILayout.PropertyField(_childrenTargetImages);
                EditorGUILayout.PropertyField(_interactable);
                EditorGUILayout.EndVertical();
                EditorGUILayout.PropertyField(_onClick);
                EditorGUI.indentLevel--;

            }
            

            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}