using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionSceneManager : MonoBehaviour
{
    [Header("Objects")]
    public GameObject balloon;
    public Transform balloons;
    [Space]
    public GameObject bird;
    public Transform birds;
    [Space]
    public GameObject messageSystem;

    //Adding a balloon to the scene.
    [ContextMenu("Add Balloon")]
    public void AddBalloon()
    {
        if (Balloon.Count() >= Balloon.MAX)
        {
            Balloon.ExitFirst();
        }
        GameObject balloonObj = Instantiate(balloon, balloons);
    }

    //Adding a default bird to the scne.
    [ContextMenu("Add Bird")]
    public void AddBird()
    {
        if (Bird.Count() >= Bird.MAX)
        {
            Bird.ExitFirst();
        }
        GameObject birdObj = Instantiate(bird, birds);
    }

    [ContextMenu("Send Message to Power Line")]
    public void SendToPowerLine()
    {
        messageSystem.GetComponent<PowerlineMessage>().AddMessageToQueue("Hello");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddBalloon();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            AddBird();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SendToPowerLine();
        }
    }
}
