using System;
using UnityEngine;

public class Abeille : Entity
{
    public enum State
    {
        Patrouille,
        Chase,
        Stunt,
        }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
