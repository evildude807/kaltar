using System;
using Server;
using Server.Items;

namespace Kaltar.Armas
{
	/// <summary>
	/// Description of LancaBase.
	/// </summary>
	public abstract class LancaBase : Arma {

		#region kaltar
		public override CategoriaArma categoria { get{return CategoriaArma.pontiaguda;} }
		#endregion		
		
		public override int DefHitSound{ get{ return 0x23C; } }
		public override int DefMissSound{ get{ return 0x238; } }

		public override SkillName DefSkill{ get{ return SkillName.Fencing; } }
		public override WeaponType DefType{ get{ return WeaponType.Piercing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce2H; } }

		public LancaBase( int itemID ) : base( itemID ) {
		}

		public LancaBase( Serial serial ) : base( serial ) {
		}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
