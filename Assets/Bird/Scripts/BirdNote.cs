using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Curve;
using BansheeGz.BGSpline.Components;

public class BirdNote : MonoBehaviour
{
    public GameObject[] notePrefabs;
    private BGCurve curve;
    private BGCcMath math;
    private List<GameObject> notes;

    private void Start()
    {
        curve = GetComponent<BGCurve>();
        math = curve.GetComponent<BGCcMath>();
        notes = new List<GameObject>();
    }

    public void AddNote(int i)
    {

        int count = notes.Count + 1;
        float dist = math.GetDistance();

        int j = 0;
        foreach(GameObject note in notes)
        {
            float distance = dist / count * j;
            Vector3 position = math.CalcPositionByDistance(distance);
            note.transform.position = position;
            j++;
        }


        float newdist = dist / count * j;
        Vector3 newPos = math.CalcPositionByDistance(newdist);
        GameObject obj = Instantiate(notePrefabs[i], newPos, Quaternion.identity);
        notes.Add(obj);

    }
}
