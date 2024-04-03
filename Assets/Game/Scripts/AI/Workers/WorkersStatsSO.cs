using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WorkerStats")]
public class WorkersStatsSO : ScriptableObject
{
    [field: Header("Main settings")]
    [field: SerializeField] public List<int> Price { get; private set; }
}