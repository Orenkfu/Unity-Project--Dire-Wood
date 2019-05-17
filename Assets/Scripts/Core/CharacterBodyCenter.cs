using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBodyCenter : MonoBehaviour {
    //attach this script to the center of the body so projectiles can target it
    [Header("Serialize the center of mass to this field ", order =0), Space(1)]
    [Header("so it will be accessible to projectiles.", order =1), Space(1)]
    [SerializeField] public Transform bodyCenter;
}
