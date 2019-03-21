using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Curve;
using BansheeGz.BGSpline.Components;

namespace Singing
{
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
            Debug.Log("Add Bird Note");
            int count = notes.Count + 1;
            float dist = math.GetDistance();

            int j = 0;
            foreach (GameObject note in notes)
            {
                float distance = dist / count * j;
                Vector3 position = math.CalcPositionByDistance(distance);
                note.transform.position = new Vector3(position.x, position.y * Random.Range(0.8f, 1.2f));
                j++;
            }


            float newdist = dist / count * j;
            Vector3 newPos = math.CalcPositionByDistance(newdist);
            newPos = new Vector3(newPos.x, newPos.y * Random.Range(0.8f, 1.2f), newPos.z);
            GameObject obj = Instantiate(notePrefabs[i], newPos, Quaternion.identity);
            obj.transform.localScale *= Mathf.Pow(1.1f, count);
            notes.Add(obj);

        }

        public void ClearSong()
        {
            foreach (GameObject note in notes)
            {
                note.GetComponent<Note>().Destroy();
            }
            notes = new List<GameObject>();

        }
    }
}
