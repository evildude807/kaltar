using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
	public class DefRuneCrafting : CraftSystem
	{
		public override SkillName MainSkill
		{
			get{ return SkillName.Inscribe; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044009; } // <CENTER>INSCRIPTION MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefRuneCrafting();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefRuneCrafting() : base( 1, 1, 1.25 )// base( 1, 2, 1.7 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !(from is PlayerMobile && from.Skills[SkillName.Magery].Base >= 100.0) )
				return 1044153; // You don't have the required skill
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;

		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x1F5 ); // magic

			//if ( from.Body.Type == BodyType.Human && !from.Mounted )
			//	from.Animate( 9, 5, 1, true, false, 0 );

			//new InternalTimer( from ).Start();
		}

		// Delay to synchronize the sound with the hit on the anvil
		private class InternalTimer : Timer
		{
			private Mobile m_From;

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( 0.7 ) )
			{
				m_From = from;
			}

			protected override void OnTick()
			{
				m_From.PlaySound( 0x2A );
			}
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				from.PlaySound( 65 ); // rune breaking
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				//from.PlaySound( 65 ); // rune breaking
				//if ( quality == 0 )
					//return 502785; // You were barely able to make this item.  It's quality is below average.
				//else if ( makersMark && quality == 2 )
					//return 1044156; // You create an exceptional quality item and affix your maker's mark.
				//else if ( quality == 2 )
					//return 1044155; // You create an exceptional quality item.
				//else				
					return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = AddCraft( typeof( BlankRune ), 1044050, "blank rune", 45.0, 80.0, typeof( RoughStone ), "Rough Stone", 1, "A rough stone is needed to make that." );

			index = AddCraft( typeof( AgloRune ), "Runes", "Aglo Rune", 70.5, 102.5, typeof( MandrakeRoot ), "Mandrake Root", 5, 1044365 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( AhmRune ), "Runes", "Ahm Rune", 75.0, 120.0, typeof( SpidersSilk ), "Spiders Silk", 5, 1044368 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( AnRune ), "Runes", "An Rune", 60.5, 100.0, typeof( BatWing ), "Bat Wing", 3, "You do not have enough bat wing to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( ArmaRune ), "Runes", "Arma Rune", 65.5, 100.0, typeof( PigIron ), "Pig Iron", 3, "You do not have enough pig iron to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( BalRune ), "Runes", "Bal Rune", 75.5, 105.0, typeof( Nightshade ), "Nightshade", 5, 1044366 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( BehRune ), "Runes", "Beh Rune", 70.5, 99.5, typeof( Bloodmoss ), "Bloodmoss", 5, 1044362 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( BetRune ), "Runes", "Bet Rune", 52.5, 100.0, typeof( BlackPearl ), "Black Pearl", 3, 1044361 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( CahRune ), "Runes", "Cah Rune", 60.0, 95.0, typeof( Garlic ), "Garlic", 3, "A blank rune is needed to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( CharRune ), "Runes", "Char Rune", 65.5, 105.0, typeof( GraveDust ), "Grave Dust", 3, "You do not have enough grave dust to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( CorpRune ), "Runes", "Corp Rune", 85.5, 120.0, typeof( GraveDust ), "Grave Dust", 7, "You do not have enough grave dust to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( DelRune ), "Runes", "Del Rune", 70.5, 100.0, typeof( BlackPearl ), "Black Pearl", 5, 1044361 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( DesRune ), "Runes", "Des Rune", 75.5, 102.0, typeof( DaemonBlood ), "Daemon Blood", 5, "You do not have enough daemon blood to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( DiumRune ), "Runes", "Dium Rune", 70.5, 100.0, typeof( Garlic ), "Garlic", 5, "A blank rune is needed to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( ExRune ), "Runes", "Ex Rune", 80.5, 110.0, typeof( MandrakeRoot ), "Mandrake Root", 7, 1044365 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( FlamRune ), "Runes", "Flam Rune", 70.5, 100.0, typeof( SulfurousAsh ), "Sulfurous Ash", 5, 1044367 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( FlamusRune ), "Runes", "Flamus Rune", 70.5, 100.0, typeof( SulfurousAsh ), "Sulfurous Ash", 5, 1044367 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( FrioRune ), "Runes", "Frio Rune", 70.5, 105.0, typeof( BlackPearl ), "Black Pearl", 5, 1044361 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( FurisRune ), "Runes", "Furis Rune", 70.5, 100.0, typeof( PigIron ), "Pig Iron", 5, "You do not have enough pig iron to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( GraRune ), "Runes", "Gra Rune", 85.5, 120.0, typeof( Nightshade ), "Nightshade", 7, 1044366 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( GravRune ), "Runes", "Grav Rune", 70.5, 100.0, typeof( SpidersSilk ), "Spiders Silk", 5, 1044368 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( HurRune ), "Runes", "Hur Rune", 70.5, 100.0, typeof( BlackPearl ), "blank rune", 5, 1044361 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( InRune ), "Runes", "In Rune", 50.5, 90.0, typeof( Garlic), "Garlic", 3, "A blank rune is needed to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( JuxRune ), "Runes", "Jux Rune", 60.5, 95.0, typeof( BatWing ), "Bat Wing", 3, "You do not have enough bat wing to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( KalRune ), "Runes", "Kal Rune", 60.5, 95.0, typeof( SulfurousAsh ), "Sulfurous Ash", 3, 1044367 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( LorRune ), "Runes", "Lor Rune", 50.5, 90.0, typeof( SpidersSilk ), "Spiders Silk", 3, 1044368 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( LumRune ), "Runes", "Lum Rune", 60.5, 90.0, typeof( Ginseng ), "Ginseng", 3, 1044364 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( MalasRune ), "Runes", "Malas Rune", 70.5, 100.0, typeof( NoxCrystal ), "Nox Crystal", 5, "You do not have enough nox crystal to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( ManiRune ), "Runes", "Mani Rune", 85.5, 120.0, typeof( Ginseng ), "Ginseng", 7, 1044364 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( MuRune ), "Runes", "Mu Rune", 80.5, 115.0, typeof( Ginseng ), "Ginseng", 7, 1044364 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( NoxRune ), "Runes", "Nox Rune", 70.5, 100.0, typeof( NoxCrystal ), "Nox Crystal", 5, "You do not have enough nox crystal to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( OmRune ), "Runes", "Om Rune", 75.5, 115.0, typeof( GraveDust ), "Grave Dust", 5, "You do not have enough grave dust to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( OrtRune ), "Runes", "Ort Rune", 80.5, 120.0, typeof( DaemonBlood ), "Daemon Blood", 7, "You do not have enough daemon blood to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( PasRune ), "Runes", "Pas Rune", 75.5, 115.0, typeof( DaemonBlood ), "Daemon Blood", 5, "You do not have enough daemon blood to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( PorRune ), "Runes", "Por Rune", 85.5, 120.0, typeof( BatWing ), "Bat Wing", 7, "You do not have enough bat wing to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( QuasRune ), "Runes", "Quas Rune", 80.5, 120.0, typeof( Nightshade ), "Nightshade", 3, 1044366 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( RaRune ), "Runes", "Ra Rune", 70.5, 100.0, typeof( SulfurousAsh ), "Sulfurous Ash", 5, 1044367 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( RelRune ), "Runes", "Rel Rune", 65.5, 95.0, typeof( SpidersSilk ), "Spiders Silk", 3, 1044368 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( SanctRune ), "Runes", "Sanct Rune", 70.5, 105.0, typeof( PigIron ), "Pig Iron", 5, "You do not have enough pig iron to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( SarRune ), "Runes", "Sar Rune", 75.5, 120.0, typeof( Bloodmoss ), "Bloodmoss", 5, 1044362 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( SummRune ), "Runes", "Summ Rune", 70.5, 105.0, typeof( BlackPearl ), "Black Pearl", 5, 1044361 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( TymRune ), "Runes", "Tym Rune", 75.5, 115.0, typeof( Bloodmoss ), "Bloodmoss", 5, 1044362 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( UmRune ), "Runes", "Um Rune", 75.5, 115.0, typeof( MandrakeRoot ), "Mandrake Root", 5, 1044365 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( UusRune ), "Runes", "Uus Rune", 75.5, 115.0, typeof( Bloodmoss ), "Bloodmoss", 5, 1044362 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( VasRune ), "Runes", "Vas Rune", 80.5, 120.0, typeof( MandrakeRoot ), "Mandrake Root", 7, 1044365 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( WisRune ), "Runes", "Wis Rune", 80.5, 120.0, typeof( Ginseng ), "Ginseng", 7, 1044364 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( XenRune ), "Runes", "Xen Rune", 60.5, 100.0, typeof( Garlic ), "Garlic", 3, "A blank rune is needed to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( YlemRune ), "Runes", "Ylem Rune", 85.5, 120.0, typeof( DaemonBlood ), "Daemon Blood", 7, "You do not have enough daemon blood to make that." );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
			index = AddCraft( typeof( ZuRune ), "Runes", "Zu Rune", 50.0, 80.5, typeof( BlackPearl ), "Black Pearl", 3, 1044361 );
				AddRes( index, typeof ( BlankRune ), "Blank Rune", 1, "You do not have enough blank runes to make that."  );
		}
	}
}