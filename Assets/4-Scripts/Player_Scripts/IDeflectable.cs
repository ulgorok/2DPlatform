using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=Ci1KWAjfL1I 

public interface IDeflectable
{

    public void Deflect(Vector2 direction);

    public float ReturnSpeed { get; set; }
    public bool IsDeflecting { get; set; }
}
