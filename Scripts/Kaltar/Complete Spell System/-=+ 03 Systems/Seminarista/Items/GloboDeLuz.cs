using System;
using Server;

namespace Server.Items
{
	public class GloboDeLuz : Item {

		private Mobile caster;
		private Timer duracao;
		
		public GloboDeLuz(Mobile caster) : base( 6255 ) {
			Movable = false;
			Light = LightType.Circle225;
			Weight = 2.0;
			
			this.caster = caster;
			this.duracao = new InternalTimer(this, TimeSpan.FromMinutes(5));				
		}
	
		public GloboDeLuz( Serial serial ) : base( serial )	{
		}
		
        private class InternalTimer : Timer {
			
            private GloboDeLuz item;

            public InternalTimer(GloboDeLuz item, TimeSpan duracao) : base(duracao) {
                this.item = item;
            }

            protected override void OnTick() {
            	Effects.PlaySound( item.Location, item.Map, 0x3be);
				
                item.Delete();
            }
        }
		
		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
