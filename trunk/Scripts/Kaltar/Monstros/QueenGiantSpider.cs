using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "Il corpo di una Regina Ragno Gigante" )]
	public class ReginaGigante : BaseCreature
	{
		int i = 100;

		[Constructable]
		public ReginaGigante () : base(  AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "una regina Ragno Gigante";
			Body = 11;
			BaseSoundID = 1170;
			

			SetStr( 196, 220 );
			SetDex( 126, 145 );
			SetInt( 70, 110 );
			SetHits( 250, 330 );
			SetDamage( 25, 33 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Poison, 60 );

			SetResistance( ResistanceType.Physical, 50, 65 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 90, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.MagicResist, 62.0, 72.0 );
			SetSkill( SkillName.Tactics, 55.1, 70.0 );
			SetSkill( SkillName.Wrestling, 70.0, 80.0 );

			VirtualArmor = 38;

			PackItem( new SpidersSilk( 8 ) );
		}
		
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }

		public override void OnActionCombat()
		{
			Mobile combatant = Combatant;
			

			if (i == 100)
			{

				switch ( Utility.Random( 3 ) )
				{
					case 0:
					{ 
						Effects.SendMovingEffect(this, combatant, 0x36E4, 2, 0, false, false , 0x480,0);

						SpiderWeb ragnatela = new SpiderWeb( TimeSpan.FromSeconds(10));
						ragnatela.MoveToWorld( new Point3D( combatant.X, combatant.Y, combatant.Z ),Map);
					break;
					}
				}
				i -= 99;
			}
			i += 1;
		}
		public ReginaGigante( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
