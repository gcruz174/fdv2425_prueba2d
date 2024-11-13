using UnityEngine;

public class ScrollingBackgroundParallax : MonoBehaviour
{
    private Material[] _materials;

    private void Start()
    {
        _materials = GetComponent<Renderer>().materials;
    }

    private void Update()
    {
        for (var i = 0; i < _materials.Length; i++)
        {
            var offset = _materials[i].mainTextureOffset;
            offset.x += Time.deltaTime * (i + 1) / 20;
            _materials[i].mainTextureOffset = offset;
        }
    }
}
