using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server;
using Server.Mobiles;

namespace Scripts.Invasion_System
{
	[CorpseName( "a terathan drone corpse" )]
    public class InvasionTerathanDrone : InvasionSpawn
	{
		[Constructable]
        public InvasionTerathanDrone(InvasionController c)
            : base(c, InvasionSpawnType.Arachnid, AIType.AI_Melee, FightMode.Closest, new double[]{10, 1, 0.2, 0.4})
		{
			Name = "a terathan drone";
			Body = 71;
			BaseSoundID = 594;

			SetStr( 36, 65 );
			SetDex( 96, 145 );
			SetInt( 21, 45 );

			SetHits( 22, 39 );
			SetMana( 0 );

			SetDamage( 6, 12 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 15, 25 );

			SetSkill( SkillName.Poisoning, 40.1, 60.0 );
			SetSkill( SkillName.MagicResist, 30.1, 45.0 );
			SetSkill( SkillName.Tactics, 30.1, 50.0 );
			SetSkill( SkillName.Wrestling, 40.1, 50.0 );

			Fame = 2000;
			Karma = -2000;

			VirtualArmor = 24;
			
			PackItem( new SpidersSilk( 2 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override int Meat{ get{ return 4; } }

		public InvasionTerathanDrone( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 589 )
				BaseSoundID = 594;
		}
	}
}