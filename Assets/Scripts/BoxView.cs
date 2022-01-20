using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxView : MonoBehaviour
{

    public Animator Animator;
    public Action OpenEnd;

    public void OnOpenEnd() => OpenEnd?.Invoke();

}
