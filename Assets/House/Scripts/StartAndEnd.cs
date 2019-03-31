using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Loving
{
    class StartAndEnd : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
