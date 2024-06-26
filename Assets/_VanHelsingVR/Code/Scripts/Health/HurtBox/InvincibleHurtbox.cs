namespace _VanHelsingVR.Health
{
    public class InvincibleHurtbox : Hurtbox
    {
        protected override bool OnBeforeHit(Hitbox _)
        {
            return false;
        }

        protected override bool OnBeforePunch(PunchableHitbox _)
        {
            return false;
        }

        protected override bool OnBeforeHitScan()
        {
            return false;
        }
    }
}
