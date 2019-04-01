using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Talking
{
    class TextInputField : MonoBehaviour
    {
        public KeyBoardController controller;
        void Update()
        {
            GetComponent<TextMeshPro>().text = controller.inputString;
        }
    }
}

