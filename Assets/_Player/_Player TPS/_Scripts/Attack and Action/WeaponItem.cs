using UnityEngine;


[CreateAssetMenu(menuName = "Item/Weapon Item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("Idle Animations")]
    public string Right_Idle;
    public string Left_Idle;

    [Header("One Handed Attack Animations")]
    public string OH_Light_Attack_1;
    public string OH_Light_Attack_2;
    [Header("Two Handed Attack Animations")] 
    public string OH_Heavy_Attack_1;
    public string OH_Heavy_Attack_2;




}
