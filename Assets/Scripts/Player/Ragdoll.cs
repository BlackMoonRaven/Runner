using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skin;

    private void Start()
    {
        _skin.material = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkinSystem>()._meshRenderer.material;
    }
}
