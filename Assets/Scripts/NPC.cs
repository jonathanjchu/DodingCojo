using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPC : MovingObject
{
    public Text dialogText;
    public string npcName;


    protected override void OnCantMove<T>(T component)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
