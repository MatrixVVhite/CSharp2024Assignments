public abstract class Weapon
{

}

public class MeleeWeapon : Weapon
{
	public uint Damage { get; private set; }

	public MeleeWeapon(uint damage)
	{
		Damage = damage;
	}
}

public class Staff : Weapon
{
	public uint Heal { get; private set; }

	public Staff(uint heal)
	{
		Heal = heal;
	}
}
