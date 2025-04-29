namespace _VR_Helsing.Gun
{
    public sealed class GunStateFactory
    {
        public readonly BaseGunState IdleState;
        public readonly BaseGunState ActiveState;
        public readonly BaseGunState ReloadState;
        public readonly BaseGunState ShootState;
        public readonly BaseGunState ChangeWeaponState;
        
        private GunStateMachine _ctx;
        public GunStateFactory(GunStateMachine context)
        {
            _ctx = context;

            IdleState = new IdleGunState(_ctx, this);
            ActiveState = new ActiveGunState(_ctx, this);
            ReloadState = new ReloadGunState(_ctx, this);
            ShootState = new ShootGunState(_ctx, this);
            ChangeWeaponState = new ChangeWeaponGunState(_ctx, this);
        }

    }
}