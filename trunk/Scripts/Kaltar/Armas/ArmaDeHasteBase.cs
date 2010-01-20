using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Engines.Harvest;
using Server.ContextMenus;

namespace Kaltar.Armas
{
	/// <summary>
	/// Description of ArmaDeHasteBase.
	/// </summary>
	public abstract class ArmaDeHasteBase : Arma {

		#region kaltar
		public override CategoriaArma categoria { get{return CategoriaArma.machado;} }
		#endregion		
		
		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x238; } }

		public override SkillName DefSkill{ get{ return SkillName.Swords; } }
		public override WeaponType DefType{ get{ return WeaponType.Polearm; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash2H; } }

		public virtual HarvestSystem HarvestSystem{ get{ return Lumberjacking.System; } }

		private int m_UsesRemaining;
		private bool m_ShowUsesRemaining;

		public ArmaDeHasteBase( int itemID ) : base( itemID ) {
			m_UsesRemaining = 150;
		}

		public ArmaDeHasteBase( Serial serial ) : base( serial ) {
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining {
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool ShowUsesRemaining {
			get { return m_ShowUsesRemaining; }
			set { m_ShowUsesRemaining = value; InvalidateProperties(); }
		}

		public virtual int GetUsesScalar() {
			if ( Quality == WeaponQuality.Exceptional )
				return 200;

			return 100;
		}

		public override void UnscaleDurability() {
			base.UnscaleDurability();

			int scale = GetUsesScalar();

			m_UsesRemaining = ((m_UsesRemaining * 100) + (scale - 1)) / scale;
			InvalidateProperties();
		}

		public override void ScaleDurability() {
			base.ScaleDurability();

			int scale = GetUsesScalar();

			m_UsesRemaining = ((m_UsesRemaining * scale) + 99) / 100;
			InvalidateProperties();
		}

		public override void OnDoubleClick( Mobile from ) {
			if ( HarvestSystem == null )
				return;

			if ( IsChildOf( from.Backpack ) || Parent == from )
				HarvestSystem.BeginHarvesting( from, this );
			else
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) {
			base.GetContextMenuEntries( from, list );

			if ( HarvestSystem != null )
				BaseHarvestTool.AddContextMenuEntries( from, this, list, HarvestSystem );
		}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (bool) m_ShowUsesRemaining );
			writer.Write( (int) m_UsesRemaining );
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_ShowUsesRemaining = reader.ReadBool();
			m_UsesRemaining = reader.ReadInt();
		}
	}
}
