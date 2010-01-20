//old school Feb 2009

using System; 
using Server.Items; 
using System.Text; 
using Server.Gumps; 
using Server.Network; 

namespace Server.Items 
{ 
   [Serializable()] 
   public class WildCougardeed : Item 
   { 
      [Constructable] 
      public WildCougardeed() : base( 0x14F0 ) 
      { 
         Movable = true;
         Weight = 1.0; 
         Hue = 0; 
         Name = "Wild Cougar Deed"; //Change name here
      
      }

      public WildCougardeed( Serial serial ) : base( serial ) 
      { 
      } 

      public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 

         writer.Write( (int) 0 ); // version 
      } 

      public override void Deserialize( GenericReader reader ) 
      { 
         base.Deserialize( reader ); 

         int version = reader.ReadInt(); 
      } 
   } 
} 
