using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //clase que guarda posicion y grado de parallax de fondos
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layerPos;
        public float parallaxFactor;
    }

    [SerializeField]private List<ParallaxLayer> layers;
    [SerializeField] private Transform camPos;

    private Vector2 currentPos;
    private Vector2 pastPos;

    private void Start()
    {
        currentPos = camPos.position;
        pastPos = camPos.position;
    }
    //mover imagenes de fondo en base a movimiento de camara y grado de parallax
    void Update()
    {
        currentPos = camPos.position;
        Vector2 deltaPos = currentPos - pastPos; 
        foreach (ParallaxLayer layer in layers)
        {
            Vector3 changePos = new Vector3(deltaPos.x * layer.parallaxFactor, deltaPos.y * layer.parallaxFactor);
            layer.layerPos.position += changePos;
        }
        pastPos = currentPos;
    }
}
