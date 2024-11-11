using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletAbilityManagerSO")]
public class BulletAbilityManagerSO : ScriptableObject
{
    private static BulletAbilityManagerSO instance;
    public static BulletAbilityManagerSO Instance
    {
        get
        {
            if (instance == null)
                return instance = Resources.Load<BulletAbilityManagerSO>("BulletAbility/BulletAbilityManagerSO");
            return instance;
        }
    }

    [SerializeField] private BulletAbility[] bulletAbility;

    public BulletAbility GetBulletAbility(string abilityName)
    {
        for(int i = 0; i < bulletAbility.Length; i++)
        {
            if (bulletAbility[i].abilityName.Equals(abilityName))
                return bulletAbility[i];
        }
        return null;
    }
}
