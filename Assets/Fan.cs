using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour, ITargetObject {
    [SerializeField] private Transform _viewCenter;
    public Transform targetTransform => _viewCenter;
}
