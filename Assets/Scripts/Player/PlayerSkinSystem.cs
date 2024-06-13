using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinSystem : MonoBehaviour
{
    public SkinnedMeshRenderer _meshRenderer;

    [SerializeField] private Material[] _skins;

    private void Start()
    {
        _meshRenderer.material= _skins[PlayerPrefs.GetInt("CurrentSkin")];
    }
}
