using System;
using Server.Targeting;
using Server.Network;
using Server.Spells;
using System.Collections.Generic;

using Kaltar.Util;

namespace Server.ACC.CSS.Systems.Aprendiz {
	
	public class ChuvaDeRaiosSpell : AprendizSpell {
		
		private static SpellInfo m_Info = new SpellInfo(
				"Chuva de Raios", 
				"Chuva de Raios",
				212,
				9041,
				Reagent.SulfurousAsh
		);

		public override SpellCircle Circle { get { return SpellCircle.First; } }
		public override double RequiredSkill{ get{ return 20.0; } }
		public override double CastDelay{ get{ return 3.0; } }
		public override int RequiredMana   { get{ return 20; } }
		
		public ChuvaDeRaiosSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info ) {
		}
		
		public override void OnCast() {

            if (CheckSequence())
            {
                TimeSpan delay = TimeSpan.FromSeconds(1);
                TimeSpan intervalo = TimeSpan.FromSeconds(3);

                Timer t = new InternalTimer(this, Caster, delay, intervalo, 3);
                t.Start();
            }

            FinishSequence();
        }

        private class InternalTimer : Timer
		{
			private Mobile Caster;
            private ChuvaDeRaiosSpell magia;

			public InternalTimer(ChuvaDeRaiosSpell magia, Mobile owner, TimeSpan delay, TimeSpan intervalo, int numeroVezes) : base(delay, intervalo, numeroVezes)
			{
                this.magia = magia;
				Caster = owner;
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
                Caster.FixedEffect(0x37C4, 10, 42, 4, 3);

                //lista dos mobiles que vao tomar dano
                List<Mobile> targets = new List<Mobile>();
                
                //pega os mobiles afetados
                IPooledEnumerable mobiles = Caster.GetMobilesInRange(4);
                foreach (Mobile m in mobiles)
                {
                    if (m == Caster)
                        continue;

                    if (SpellHelper.ValidIndirectTarget(Caster, m)
                        && Caster.CanBeHarmful(m, false)
                        && Caster.CanSee(m))
                    {
                        targets.Add(m);
                    }
                }
                mobiles.Free();

                foreach (Mobile mobile in targets)
                {
                    Caster.DoHarmful(mobile);

                    double damage = magia.GetNewAosDamage(5, 1, 10, null);

                    SpellHelper.Damage(magia, mobile, damage, 0, 0, 0, 0, 100);
                    
                    mobile.BoltEffect(0);
                    
                }
			}
		}
	}

}
