namespace _VanHelsingVR.Interaction
{
    public class PlayerDamagable : Damagable
    {
        public override void OnDamage()
        {
            //Animacion del golpe
            UnityEngine.Debug.Log("Player Has Been Hit");
            base.OnDamage();
        }
    }
}
