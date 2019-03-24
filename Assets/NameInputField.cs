using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Loving
{
    public class NameInputField : TapableObject
    {
        public override void OnTap()
        {
            this.GetComponent<InputField>().Select();
            this.GetComponent<InputField>().ActivateInputField();
        }
    }
}
