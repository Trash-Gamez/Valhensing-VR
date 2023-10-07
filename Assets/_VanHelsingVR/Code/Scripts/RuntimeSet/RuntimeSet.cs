using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeSet<T> : ScriptableObject
{
    public List<T> List {get; private set;} = new List<T>();
    
    public void Add(T item) {
        if(List.Contains(item)) return;
        List.Add(item);
    }
    
    public void Remove(T item){
        if(!List.Contains(item)) return;
        List.Remove(item);
    }
}
