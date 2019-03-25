﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameController
{
    void StartGame();

    void Proceed();

    int GetGameStage();
}

