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
    }
    private static Scene instance;
    public static Scene Instance
    {
        get {
            if (instance == null)
            {
                instance = new GameObject("Scene").AddComponent<Scene>();
            }

            return instance;
        }
    }

}