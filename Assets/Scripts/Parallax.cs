using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layerPos;
        public float parallaxFactor;
    }

    [SerializeField] private List<ParallaxLayer> layers;

    private Transform camTransform;
    private Vector2 currentPos;
    private Vector2 pastPos;

    public void Initialize(Transform camera)
    {
        camTransform = camera;

        currentPos = camTransform.position;
        pastPos = camTransform.position;
    }

    private void Update()
    {
        if (camTransform == null)
            return;

        currentPos = camTransform.position;

        Vector2 deltaPos = currentPos - pastPos;

        foreach (ParallaxLayer layer in layers)
        {
            Vector3 changePos = new Vector3(
                deltaPos.x * layer.parallaxFactor,
                deltaPos.y * layer.parallaxFactor
            );

            layer.layerPos.position += changePos;
        }

        pastPos = currentPos;
    }
}