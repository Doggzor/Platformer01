using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "Game/Skins/Player Skin")]
public class PlayerSkinPrefab : ScriptableObject
{
    public Sprite
        head,
        eye_front,
        eye_back,
        body,
        arm_front,
        arm_back,
        tail,
        leg_front,
        leg_back;
}
