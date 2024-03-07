using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRendererHandler : MonoBehaviour
{
    public List<MeshRenderer> meshes = new List<MeshRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        GetChildMeshRenderers(transform);
    }


    public void SetActiveMeshes(bool active)
    {
        foreach(MeshRenderer mesh in meshes)
        {
            mesh.enabled = active;
        }
    }

    void GetChildMeshRenderers(Transform parent)
    {
        foreach (Transform child in parent)
        {
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshes.Add(meshRenderer);
            }
            GetChildMeshRenderers(child);
        }
    }

}
