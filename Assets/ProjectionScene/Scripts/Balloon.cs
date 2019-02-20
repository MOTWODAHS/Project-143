using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Balloon: MonoBehaviour
{
    //Maxiumn number of balloons allowed in scene.
    public const int MAX = 6;
    //Range in sceen the balloons should reside in.
    private const float Y_MIN = 1f;
    private const float Y_MAX = 4f;
    private const float X_MIN = -6f;
    private const float X_MAX = 6f;

    private float targetX;
    private float targetY;
    //Current numbers of balloon in the scene.
    private static int size = 0;
    //Collection of balloon.
    private static Queue<Balloon> balloons = new Queue<Balloon>();
    //Z depth of the balloon so they do not overlap.
    private static int currentLayer = 0;
    //Section of x coordinate to pick from.
    private static float[,] sections = new float[,]
    {
        {-6f, -4f}, {-4f, -2f}, {-2f, 0f}, {0f, 2f}, {2f, 4f}, {4f, 6f}
    }; 


    //Ballon enters the scene.
    void Start()
    {
        currentLayer = (currentLayer + 1) % MAX;
        balloons.Enqueue(this);
        size++;
        targetX = Random.Range(sections[currentLayer,0], sections[currentLayer, 1]);
        targetY = Random.Range(Y_MIN, Y_MAX);
        this.gameObject.transform.DOMove(new Vector3(targetX, targetY, currentLayer), 5).OnComplete(StartAnimation);
    }

    void StartAnimation()
    {
        this.gameObject.transform.DOShakePosition(5f, 0.1f, 1, fadeOut:false).SetLoops(-1);
    }

    //Returns how many ballons is in the scene.
    public static int Count() { return Balloon.size; }

    //Pop the first one in the queue.
    public static void ExitFirst()
    {
        Balloon first = balloons.Dequeue();
        size--;
        first.Exit();
    }

    //Ballon exit the scene.
    public void Exit()
    {
        this.gameObject.transform.DOMove(new Vector2(targetX, 7f), 3).OnComplete(() => {Destroy(this.gameObject); });
    }

    //Ballon move around in the scene.
    public void Move()
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        
    }
}
