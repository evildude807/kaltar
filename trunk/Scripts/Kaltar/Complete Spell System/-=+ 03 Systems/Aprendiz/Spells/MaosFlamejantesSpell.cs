using System;
using Server.Targeting;
using Server.Network;
using Server.Spells;
using System.Collections.Generic;

using Kaltar.Util;

namespace Server.ACC.CSS.Systems.Aprendiz {
	
	public class MaosFlamejantesSpell : AprendizSpell {
		
		private static SpellInfo m_Info = new SpellInfo(
				"Mãos Flamejantes", 
				"Mãos Flamejantes",
				212,
				9041,
				Reagent.SulfurousAsh
		);

		public override SpellCircle Circle { get { return SpellCircle.First; } }
		public override double RequiredSkill{ get{ return 15.0; } }
		public override double CastDelay{ get{ return 1.0; } }
		public override int RequiredMana   { get{ return 10; } }
		
		public MaosFlamejantesSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info ) {
		}
		
		public override void OnCast() {
		
			if(CheckSequence()) {
				
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
                        && Caster.CanSee(m)
                        && SummonUtil.Instance.estaNoArcoDeVisao(Caster, m))
                    {
                        targets.Add(m);
                    }
                }
                mobiles.Free();

                foreach (Mobile mobile in targets)
                {
					Caster.DoHarmful( mobile );

                    //dano 2d6 + 10
                    int damage = GetNewAosDamage(10, 3, 6, mobile);

                    Caster.MovingParticles(mobile, 0x36CB, 5, 0, false, true, 0, 0, 3006, 4006, 0, 0);
                    Caster.PlaySound(0x1E5);

                    SpellHelper.Damage(this, mobile, damage, 0, 100, 0, 0, 0);
	            }
			}
			
			FinishSequence();
		}
	}
}
