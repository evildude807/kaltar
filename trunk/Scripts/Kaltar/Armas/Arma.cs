using System;
using Server;
using Server.Items;
	
namespace Kaltar.Armas
{
	/// <summary>
	/// Description of Arma.
	/// </summary>
	public abstract class Arma : BaseWeapon {
		
		public virtual CategoriaArma categoria { get{return CategoriaArma.espada;} }

		public Arma( int itemID ) : base( itemID ) {
		}		
		
		public Arma( Serial serial ) : base( serial ) {
			
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
