using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField]
    float distanceToBackground = 100f;
    Camera viewCamera;

// Getter for Raycast Hit
    RaycastHit m_hit;
    public RaycastHit hit
    {
        get { return m_hit; }
    }

// Getter for Layer Hit

/// m_layerHit = layerHit
/// layerHit = currentLayerHit
    Layer m_layerHit;
    public Layer layerHit
    {
        get { return m_layerHit; }
    }

    public delegate void OnLayerChange(Layer newLayer); // declare new delegate type / add params to modify
        // .layerChangeObservers = .onLayerChange
    public event OnLayerChange layerChangeObservers; // instantiate an observer set




    void Start() // TODO Awake?
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                /// m_hit = raycastHit
                m_hit = hit.Value;
    // new      /// m_layerHit = layerHit
                if (m_layerHit != layer) // if layer has changed
                {
                    m_layerHit = layer;
                    layerChangeObservers(layer); // call the delegates
                }

                m_layerHit = layer;
                return;
            }
        }

        // Otherwise return background hit
        m_hit.distance = distanceToBackground;
        m_layerHit = Layer.RaycastEndStop;
        layerChangeObservers(layerHit);
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
