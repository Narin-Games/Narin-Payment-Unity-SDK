using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SingletonMono<TChildType> : MonoBehaviour where TChildType: Component{

    public  static TChildType Instance {get { return _instance as TChildType;}}
    private static SingletonMono<TChildType> _instance;
    
    private void Awake() {
        if(null == _instance) {
            AwakeUnityListener();
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            if(_instance != this)
                Destroy(this.gameObject);
        }
    }

    protected virtual void AwakeUnityListener() {}
}
