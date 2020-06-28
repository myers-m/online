using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Imanager
{
    void Manager(string name,object need);

    object Get(string need);
}
