using UnityEngine;
using System;
using Shapes;
using System.Collections.Generic;

public abstract class Drawer : ImmediateModeShapeDrawer
{
    public Timeline timeline;

    private new void OnEnable()
    {
        base.OnEnable();

        if(timeline == null)
        {
            timeline = gameObject.GetComponent<Timeline>() ?? gameObject.AddComponent<Timeline>();
        }

        this.Before();
    }

    public void Awake(){
        this.timeline.progress = 0f;
    }

    public override void DrawShapes( Camera cam ){
		if (Application.isPlaying)
        {
            timeline.progress += Time.deltaTime / timeline.queueEndTime;
        } else {
            timeline.Update(cam);
        }

		using( Draw.Command( cam ) ){
			this.Render();
		}
	}

    public new void OnDisable()
    {
        base.OnDisable();
        this.After();
    }

    public abstract void Before();
    public abstract void Render();
    public abstract void After();

}