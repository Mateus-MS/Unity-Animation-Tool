using UnityEngine;

[ExecuteAlways]
public class Scene : MonoBehaviour
{
    public Timeline timeline;

    public void OnEnable()
    {
        this.timeline = FindFirstObjectByType<Timeline>();

        // Auto-create if none exists
        if (this.timeline == null)
        {
            GameObject go = new GameObject("Timeline");
            this.timeline = go.AddComponent<Timeline>();
        }

        this.Track();
    }
    private static Scene instance;
    public static Scene Instance
    {
        get
        {
            instance = FindFirstObjectByType<Scene>();

            // Auto-create if none exists
            if (instance == null)
            {
                GameObject go = new GameObject("Scene");
                instance = go.AddComponent<Scene>();
            }

            return instance;
        }
    }

    // This method should be overrided by the children.
    // This is where all `TimelineElement` should be declared.
    public virtual void Track() { }

}