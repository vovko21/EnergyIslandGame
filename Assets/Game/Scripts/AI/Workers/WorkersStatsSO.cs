using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WorkerStats")]
public class WorkersStatsSO : ScriptableObject
{
    [field: Header("Main settings")]
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public List<int> Price { get; private set; }
}