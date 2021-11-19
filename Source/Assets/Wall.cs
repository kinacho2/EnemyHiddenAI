using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] Collider _Collider;
    [SerializeField] MeshRenderer Renderer;
    [SerializeField] Material DefaultMaterial;
    [SerializeField] Material PerceivedMaterial;
    [SerializeField] Material EscapeMaterial;

    public Collider Collider => _Collider;

    private void Awake()
    {
        _Collider = GetComponent<Collider>();
        Renderer = GetComponent<MeshRenderer>();
        Renderer.material = DefaultMaterial;
        WallsCounter.Instance.AddWall(this);
    }

    public void Perceived(bool v)
    {
        if (v)
            Renderer.material = PerceivedMaterial;
        else
            Renderer.material = DefaultMaterial;
    }


    public void PossibleEscape(bool v)
    {
        if (v)
            Renderer.material = EscapeMaterial;
        else
            Renderer.material = DefaultMaterial;
    }
}
