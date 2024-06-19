using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MaskURP
{
    [DisallowMultipleComponent]
    public class MaskReveal : MonoBehaviour
    {
        public Material RevealMat;

        [HideInInspector]
        public MeshRenderer[] MeshRendererObjectsToReveal;

        [HideInInspector]
        public SkinnedMeshRenderer[] SkinnedMeshRendererObjectsToReveal;

        [HideInInspector]
        public string Tag;

        public enum RevealOptions { Objects, Tag }
        public RevealOptions RevealOption;
        
        private int originalRenderQueue;

        /// <summary>
        /// Make the object sensitive to the reveal
        /// </summary>
        /// <param name="meshRenderer"></param>
        public void Reveal(MeshRenderer meshRenderer)
        {
            originalRenderQueue = meshRenderer.material.renderQueue; // 记录原始的渲染队列
            meshRenderer.material.renderQueue = RevealMat.renderQueue + 1; // 将渲染队列设置为 RevealMat 的渲染队列 + 1
        }

        /// <summary>
        /// Make the object sensitive to the reveal
        /// </summary>
        /// <param name="skinnedMeshRenderer"></param>
        public void Reveal(SkinnedMeshRenderer skinnedMeshRenderer)
        {
            originalRenderQueue = skinnedMeshRenderer.material.renderQueue; // 记录原始的渲染队列
            skinnedMeshRenderer.material.renderQueue = RevealMat.renderQueue + 1; // 将渲染队列设置为 RevealMat 的渲染队列 + 1
        }

        /// <summary>
        /// Make the object not sensitive to the mask
        /// </summary>
        /// <param name="meshRenderer"></param>
        public void Unreveal(MeshRenderer meshRenderer)
        {
            meshRenderer.material.renderQueue = RevealMat.renderQueue + 1;
        }

        /// <summary>
        /// Make the object not sensitive to the mask
        /// </summary>
        /// <param name="skinnedMeshRenderer"></param>
        public void Unreveal(SkinnedMeshRenderer skinnedMeshRenderer)
        {
            skinnedMeshRenderer.material.renderQueue = RevealMat.renderQueue + 1;
        }
        
        /// <summary>
        /// Initialization at startup
        /// </summary>
        private void Start()
        {
            if (RevealOption == RevealOptions.Objects)
            {
                foreach (MeshRenderer objectToReveal in MeshRendererObjectsToReveal)
                    if (objectToReveal != null)
                        Reveal(objectToReveal);
                foreach (SkinnedMeshRenderer objectToReveal in SkinnedMeshRendererObjectsToReveal)
                    if (objectToReveal != null)
                        Reveal(objectToReveal);
                return;
            }
            GameObject[] tagObjects = GameObject.FindGameObjectsWithTag(Tag);
            foreach (GameObject obj in tagObjects)
            {
                if (obj.TryGetComponent(out MeshRenderer meshRenderer))
                    Reveal(meshRenderer);
                else if (obj.TryGetComponent(out SkinnedMeshRenderer skinnedMeshRenderer))
                    Reveal(skinnedMeshRenderer);
            }
        }
    }

    [CustomEditor(typeof(MaskReveal))]
    public class MaskRevealEditor : Editor
    {
        private SerializedProperty meshRendererObjectsToReveal, skinnedMeshRendererObjectsToReveal, tag;

        /// <summary>
        /// Fetch the object from the GameObject script to display in the inspector
        /// </summary>
        private void OnEnable()
        {
            meshRendererObjectsToReveal = serializedObject.FindProperty("MeshRendererObjectsToReveal");
            skinnedMeshRendererObjectsToReveal = serializedObject.FindProperty("SkinnedMeshRendererObjectsToReveal");
            tag = serializedObject.FindProperty("Tag");
        }

        /// <summary>
        /// Implementation of the interface in the editor
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            MaskReveal maskReveal = (MaskReveal)target;
            if (maskReveal.RevealOption == MaskReveal.RevealOptions.Objects)
            {
                EditorGUILayout.PropertyField(meshRendererObjectsToReveal, new GUIContent("MeshRenderer Objects To Reveal"));
                EditorGUILayout.PropertyField(skinnedMeshRendererObjectsToReveal, new GUIContent("SkinnedMeshRenderer Objects To Reveal"));
            }
            else if (maskReveal.RevealOption == MaskReveal.RevealOptions.Tag)
                EditorGUILayout.PropertyField(tag, new GUIContent("Tag"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}
