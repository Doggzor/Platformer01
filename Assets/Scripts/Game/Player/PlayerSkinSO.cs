using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkin", menuName = "Game/Player/Skin")]
public class PlayerSkinSO : ScriptableObject
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
