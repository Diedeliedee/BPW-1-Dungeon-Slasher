using UnityEngine;

public class PlayerWeapon : Weapon
{
    [SerializeField] private GameObject m_slashEffect;
    [SerializeField] private Transform m_weaponPivot;

    protected override bool OnHitDamagable(IDamagable _damagable, int _damage, Vector3 _velocity)
    {
        if (!base.OnHitDamagable(_damagable, _damage, _velocity)) return false;

        var spawnedSlashEffect = Instantiate(m_slashEffect, transform.position, Quaternion.identity).transform;
        spawnedSlashEffect.position = m_weaponPivot.position;
        spawnedSlashEffect.rotation = m_weaponPivot.rotation;
        return true;
    }
}
