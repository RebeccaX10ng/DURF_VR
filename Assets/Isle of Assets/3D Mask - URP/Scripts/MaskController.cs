using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MaskURP
{
    [HelpURL("https://assetstore.unity.com/packages/slug/227655")]
    [DisallowMultipleComponent]
    public class MaskController : MonoBehaviour
    {
        public static MaskController Instance { get; private set; }

        public Material MaskMat;

        public MaskingOptions MaskingOption;

        [HideInInspector]
        public MeshRenderer[] MeshRendererObjectsToMask;

        [HideInInspector]
        public SkinnedMeshRenderer[] SkinnedMeshRendererObjectsToMask;

        [HideInInspector]
        public string Tag;

        public enum MaskingOptions { Objects, Tag }

        /// <summary>
        /// Make the object sensitive to the mask
        /// </summary>
        /// <param name="meshRenderer"></param>
        public void Mask(MeshRenderer meshRenderer)
        {
            meshRenderer.material.renderQueue = MaskMat.renderQueue + 1;
        }

        /// <summary>
        /// Make the object sensitive to the mask
        /// </summary>
        /// <param name="skinnedMeshRenderer"></param>
        public void Mask(SkinnedMeshRenderer skinnedMeshRenderer)
        {
            skinnedMeshRenderer.material.renderQueue = MaskMat.renderQueue + 1;
        }

        /// <summary>
        /// Make the object not sensitive to the mask
        /// </summary>
        /// <param name="meshRenderer"></param>
        public void Unmask(MeshRenderer meshRenderer)
        {
            meshRenderer.material.renderQueue = MaskMat.renderQueue;
        }

        /// <summary>
        /// Make the object not sensitive to the mask
        /// </summary>
        /// <param name="skinnedMeshRenderer"></param>
        public void Unmask(SkinnedMeshRenderer skinnedMeshRenderer)
        {
            skinnedMeshRenderer.material.renderQueue = MaskMat.renderQueue;
        }

        /// <summary>
        /// Saving this instance for convenient calling it from other scripts
        /// </summary>
        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// Initialization at startup
        /// </summary>
        private void Start()
        {
            if (MaskingOption == MaskingOptions.Objects)
            {
                foreach (MeshRenderer objectToMask in MeshRendererObjectsToMask)
                    if (objectToMask != null)
                        Mask(objectToMask);
                foreach (SkinnedMeshRenderer objectToMask in SkinnedMeshRendererObjectsToMask)
                    if (objectToMask != null)
                        Mask(objectToMask);
                return;
            }
            GameObject[] tagObjects = GameObject.FindGameObjectsWithTag(Tag);
            foreach (GameObject obj in tagObjects)
            {
                if (obj.TryGetComponent(out MeshRenderer meshRenderer))
                    Mask(meshRenderer);
                else if (obj.TryGetComponent(out SkinnedMeshRenderer skinnedMeshRenderer))
                    Mask(skinnedMeshRenderer);
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(MaskController))]
    public class MaskControllerEditor : Editor
    {
        private SerializedProperty meshRendererObjectsToMask, skinnedMeshRendererObjectsToMask, tag;

        /// <summary>
        /// Fetch the object from the GameObject script to display in the inspector
        /// </summary>
        private void OnEnable()
        {
            meshRendererObjectsToMask = serializedObject.FindProperty("MeshRendererObjectsToMask");
            skinnedMeshRendererObjectsToMask = serializedObject.FindProperty("SkinnedMeshRendererObjectsToMask");
            tag = serializedObject.FindProperty("Tag");
        }

        /// <summary>
        /// Implementation of the interface in the editor
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            MaskController maskController = (MaskController)target;
            if (maskController.MaskingOption == MaskController.MaskingOptions.Objects)
            {
                EditorGUILayout.PropertyField(meshRendererObjectsToMask, new GUIContent("MeshRenderer Objects To Mask"));
                EditorGUILayout.PropertyField(skinnedMeshRendererObjectsToMask, new GUIContent("SkinnedMeshRenderer Objects To Mask"));
            }
            else if (maskController.MaskingOption == MaskController.MaskingOptions.Tag)
                EditorGUILayout.PropertyField(tag, new GUIContent("Tag"));
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}