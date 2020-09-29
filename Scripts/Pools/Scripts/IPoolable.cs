﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    void Initialize();
    void Activate();
    void Deactivate();
}
