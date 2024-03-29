using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server;
using Server.Mobiles;

namespace Scripts.Invasion_System
{
	[CorpseName( "a gibberling corpse" )]
    public class InvasionGibberling : InvasionSpawn
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.Dismount;
		}

		[Constructable]
        public InvasionGibberling(InvasionController c)
            : base(c, InvasionSpawnType.AOS, AIType.AI_Melee, FightMode.Closest, new double[]{10, 1, 0.2, 0.4})
		{
			Name = "a InvasionGibberling";
			Body = 307;
			BaseSoundID = 422;

			SetStr( 141, 165 );
			SetDex( 101, 125 );
			SetInt( 56, 80 );

			SetHits( 85, 99 );

			SetDamage( 12, 17 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Fire, 40 );
			SetDamageType( ResistanceType.Energy, 60 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 45.1, 70.0 );
			SetSkill( SkillName.Tactics, 67.6, 92.5 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 1500;
			Karma = -1500;

			VirtualArmor = 27;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override int TreasureMapLevel{ get{ return 1; } }

		public InvasionGibberling( Serial serial ) : base( serial )
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