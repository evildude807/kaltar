using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Cleric
{
	public class ToqueDaResistenciaSpell : SeminaristaSpell {
		
		private static SpellInfo m_Info = new SpellInfo(
		                                                "Toque da Resistência", 
		                                                "Toque da Resistência",
		                                                //SpellCircle.Third,
		                                                212,
		                                                9041
		                                               );
		
		private static Hashtable tabelaEfeito = new Hashtable();
		
        public override SpellCircle Circle {
            get { return SpellCircle.First; }
        }

		public override int RequiredTithing{ get{ return 5; } }
		public override double RequiredSkill{ get{ return 0.0; } }
		public override double CastDelay{ get{ return 1.0; } }
		public override int RequiredMana   { get{ return    10; } }
		
		public ToqueDaResistenciaSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info ) {
		}

		/**
		 * True se o alvo esta sobre efeito de cura. 
		 */
		public static bool sobreEfeito(Mobile alvo) {
			return tabelaEfeito.Contains(alvo);
		}
		
		/*
		 * Remove o efeito de cura. 
		 */ 
		public static void removerEfeito( Mobile m ) {
			tabelaEfeito.Remove( m );
		}

		public static void adicionarEfeito( Mobile m) {
			tabelaEfeito.Add(m, m);
		}
		
		public override void OnCast() {
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m ) {
			
			if ( !Caster.CanSee( m ) ) {
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			if ( sobreEfeito( m ) ){
				Caster.LocalOverheadMessage( MessageType.Regular, 0x481, false, "O alvo já esta sobre o efeito de magia de proteção." );
			}
			else if ( CheckBSequence( m, false ) ) {
				SpellHelper.Turn( Caster, m );
				
				double valorSkill = Caster.Skills[SkillName.SpiritSpeak].Value;
				double bonus = Utility.Random((int)(valorSkill / 10), (int)(valorSkill / 10 + 3));
				
				TimeSpan duracao = TimeSpan.FromMinutes(Caster.Skills[SkillName.SpiritSpeak].Value / 10 + Utility.Random(2));
								
				ResistanceMod modFisico = new ResistanceMod( ResistanceType.Physical, (int)bonus );
				ResistanceMod modFire = new ResistanceMod( ResistanceType.Fire, (int)bonus );
				ResistanceMod modCold = new ResistanceMod( ResistanceType.Cold, (int)bonus );
				ResistanceMod modPoison = new ResistanceMod( ResistanceType.Poison, (int)bonus );
				ResistanceMod modEnergy = new ResistanceMod( ResistanceType.Energy, (int)bonus );
				
				m.AddResistanceMod( modFisico );
				m.AddResistanceMod( modFire );
				m.AddResistanceMod( modCold );
				m.AddResistanceMod( modPoison );
				m.AddResistanceMod( modEnergy );
			
				m.PlaySound( 0x202 );
				m.FixedParticles( 0x373A, 10, 15, 5012, 0x450, 3, EffectLayer.Waist );
				m.SendMessage( "Você esta sobre o efeito do Toque da Resistência" );
				
				adicionarEfeito(m);
				new ExpireTimer( m, modFisico, modFire, modCold, modPoison, modEnergy, duracao ).Start();
			}

			FinishSequence();
		}

		private class InternalTarget : Target {
			private ToqueDaResistenciaSpell m_Owner;

			public InternalTarget( ToqueDaResistenciaSpell owner ) : base( 1, false, TargetFlags.Beneficial ) {
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o ) {
				if ( o is Mobile ) {
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from ) {
				m_Owner.FinishSequence();
			}
		}
		
		private class ExpireTimer : Timer {
			private Mobile m_Mobile;
			
			private ResistanceMod modFisico;
			private ResistanceMod modFire;
			private ResistanceMod modCold;
			private ResistanceMod modPoison;
			private ResistanceMod modEnergy; 			
			private BuffInfo bf;
			
			public ExpireTimer( Mobile m, ResistanceMod modFisico,
			                   ResistanceMod modFire,
			                   ResistanceMod modCold,
			                   ResistanceMod modPoison,
			                   ResistanceMod modEnergy,
			                   TimeSpan delay ) : base( delay ) {
				m_Mobile = m;
				this.modFisico = modFisico;
				this.modFire = modFire;
				this.modCold = modCold;
				this.modPoison = modPoison;
				this.modEnergy = modEnergy;
				
				bf = new BuffInfo( BuffIcon.Bless, 1075843, new TextDefinition("Toque da Resistência"));
				BuffInfo.AddBuff( m,  bf);
			}

			public void DoExpire() {
				m_Mobile.RemoveResistanceMod( modFisico );
				m_Mobile.RemoveResistanceMod( modFire );
				m_Mobile.RemoveResistanceMod( modCold );
				m_Mobile.RemoveResistanceMod( modPoison );
				m_Mobile.RemoveResistanceMod( modEnergy );

				BuffInfo.RemoveBuff( m_Mobile,  bf);
				removerEfeito( m_Mobile );
				Stop();
			}

			protected override void OnTick() {
				if ( m_Mobile != null ) {
					m_Mobile.SendMessage( "O efeito da magia Toque de Resistência acabou." );
					DoExpire();
				}
			}
		}		
	}
}
