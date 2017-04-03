using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

public class Demo1ContextView : ContextView {
    void Awake()
    {
        this.context = new Demo1Context(this);//启动StrangeIoc框架
        
        //context.Start();
    }
}
