using System;
using Server.Items;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Aprendiz
{
	public class AprendizSpellbook : CSpellbook
	{
		public override School School{ get{ return School.Aprendiz; } }

		[Constructable]
		public AprendizSpellbook() : this( (ulong)0, CSSettings.FullSpellbooks ){
		}

		[Constructable]
		public AprendizSpellbook( bool full ) : this( (ulong)0, full ){
		}

		[Constructable]
		public AprendizSpellbook( ulong content, bool full ) : base( content, 0xEFA, full ){
			Hue = 0x0;
			Name = "Livro de Magia Aprendiz";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.AccessLevel == AccessLevel.Player ){
				Container pack = from.Backpack;
				if( !(Parent == from || (pack != null && Parent == pack)) )	{
					from.SendMessage( "O livro deve estar na sua mochila para ser aberto. Não pode estar dentro de outro container." );
					return;
				}
				else if( SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions( from, this.School ) ){
					return;
				}
			}

			from.CloseGump( typeof( AprendizSpellbookGump ) );
			from.SendGump( new AprendizSpellbookGump( this ) );
		}

		public AprendizSpellbook( Serial serial ) : base( serial ){
		}

		public override void Serialize( GenericWriter writer ){
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ){
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
