using System;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Items;

namespace Server.ACC.CSS.Systems.Natureza
{
	public class AgilidadeFelinaSpell : NaturezaSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
                "Agilidade felina", "Agilidade felina",
                212,
                9061,
                Reagent.Bloodmoss,
                Reagent.MandrakeRoot
			);

        public AgilidadeFelinaSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info) { }
		public override SpellCircle Circle { get { return SpellCircle.First; } }
        public override double RequiredSkill { get { return 10.0; } }
        public override double CastDelay { get { return 4.0; } }
        public override int RequiredMana { get { return 30; } }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckBSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                SpellHelper.AddStatBonus(Caster, m, StatType.Dex);

                m.FixedParticles(0x375A, 10, 15, 5010, EffectLayer.Waist);
                m.PlaySound(0x28E);

                int percentage = (int)(SpellHelper.GetOffsetScalar(Caster, m, false) * 100);
                TimeSpan length = SpellHelper.GetDuration(Caster, m);

                BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Agility, 1075841, length, m, percentage.ToString()));
            }

            FinishSequence();
        }

        private class InternalTarget : Target {
            
            private AgilidadeFelinaSpell m_Owner;

            public InternalTarget(AgilidadeFelinaSpell owner)
                : base(10, false, TargetFlags.Beneficial)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    m_Owner.Target((Mobile)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
	}
}