﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

namespace Loving
{
    class NameTagPiece : DoorWindowPiece
    {
        public GameObject pencilButtonObj;

        private bool pencilButton = false;

        private void TogglePencilButton()
        {
            pencilButton = !pencilButton;
            if (pencilButton)
            {
                pencilButtonObj.SetActive(true);
            }
            else
            {
                pencilButtonObj.SetActive(false);
            }
        }
        protected override void transformStartedHandler(object sender, EventArgs e)
        {
            base.transformStartedHandler(sender, e);
            if (pencilButton)
            {
                TogglePencilButton();
            }

        }
        protected override void transformCompletedHandler(object sender, EventArgs e)
        {
            transformer.enabled = false;
            ToggleScale();
            thisBound = GetComponent<BoxCollider2D>().bounds;

            int placementType = PlacementType();
            bool overlap = Overlap();

            if (placementType == 0)
            {
                ResetTransform();
            }
            if (placementType == -1)
            {
                ResetTransform();
            }
            else if (placementType == 1)
            {
                placed = true;
                TogglePencilButton();
                game.PlayDropDownSound();
            }

        }
    }
}
