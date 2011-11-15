/*
 * User: Tiago Augusto
 * Date: 5/5/2006
 * Time: 20:58
 */
 
using Server;
using System;
using Server.Items;
using Server.Mobiles;

using Kaltar.Talentos;

namespace Kaltar.Util
{
	/// <summary>
	/// Contem métodos uteis para lidar com armaduras.
	/// </summary>
	public sealed class ArmaduraUtil
	{
		private static ArmaduraUtil instance = new ArmaduraUtil();
		
		public static ArmaduraUtil Instance {
			get {return instance;}
		}
		
		private ArmaduraUtil(){}
		
		public static bool temTalentoParaEquipar(Jogador jogador, BaseArmor armadura) {

			if(armadura is BaseShield) {
				return temTalentoParaEquiparEscudo(jogador, (BaseShield)armadura);
			}
			else {
				return temTalentoParaEquiparArmadura(jogador, armadura);
			}
	
			return false;
		}
		
		public static bool temTalentoParaEquiparEscudo(Jogador jogador, BaseShield escudo) {

			SistemaTalento sistemaTalento = jogador.getSistemaTalento();			
			/*
			if(isEscudoPequeno(escudo) && sistemaTalento.possuiTalento(IdHabilidadeTalento.escudoPequeno)) {
				return true;				
			}
			else if(isEscudoMedio(escudo) && sistemaTalento.possuiTalento(IDTalento.escudoMedio)) {
				return true;				
			}			
			else if(isEscudoGrande(escudo) && sistemaTalento.possuiTalento(IDTalento.escudoGrande)) {
				return true;				
			}
             */
			
			return false;
		}

		public static bool temTalentoParaEquiparArmadura(Jogador jogador, BaseArmor armadura) {
			
			SistemaTalento sistemaTalento = jogador.getSistemaTalento();			
			
            /*
			if(isRoupa(armadura)) {
				return true;
			}
			else if(isArmaduraLeve(armadura) && sistemaTalento.possuiTalento(IDTalento.armaduraLeve)) {
				return true;
			}
			else if(isArmaduraMedia(armadura) && sistemaTalento.possuiTalento(IDTalento.armaduraMedia)) {
				return true;
			}
			else if(isArmaduraPesada(armadura) && sistemaTalento.possuiTalento(IDTalento.armaduraPesada)) {
				return true;
			}
             */ 
			
			return false;
		}
		
		#region armadura
		public static bool isRoupa(BaseArmor armadura) {
			
			if(armadura.MaterialType.Equals(ArmorMaterialType.Cloth)) {
				return true;
			}
			
			return false;
		}
				
		public static bool isArmaduraLeve(BaseArmor armadura) {
			
			//FIXME testar as armaduas por outra forma, e nao pelo material.			
			if(armadura.MaterialType.Equals(ArmorMaterialType.Leather) ||
			   armadura.MaterialType.Equals(ArmorMaterialType.Studded)) {
				
				return true;
			}
			
			return false;
		}

		public static bool isArmaduraMedia(BaseArmor armadura) {
			
			//FIXME testar as armaduas por outra forma, e nao pelo material.			
			if(armadura.MaterialType.Equals(ArmorMaterialType.Ringmail) ||
			   armadura.MaterialType.Equals(ArmorMaterialType.Chainmail) ||
			   armadura.MaterialType.Equals(ArmorMaterialType.Bone)) {

				return true;
			}
			
			return false;
		}
		
		public static bool isArmaduraPesada(BaseArmor armadura) {
			
			//FIXME testar as armaduas por outra forma, e nao pelo material.			
			if(armadura.MaterialType.Equals(ArmorMaterialType.Plate) ||
			   armadura.MaterialType.Equals(ArmorMaterialType.Dragon)) {

				return true;
			}
			
			return false;
		}
		#endregion 
		
		#region escudo
		public static bool isEscudoPequeno(BaseShield escudo) {
			
			if(escudo is Buckler ||
			   escudo is WoodenShield) {
				
				return true;
			}
			
			return false;
		}
		
		public static bool isEscudoMedio(BaseShield escudo) {
			
			if(escudo is BronzeShield || 
			  escudo is MetalShield || 
			  escudo is ChaosShield || 
			  escudo is OrderShield) {
				
				return true;
			}
			
			return false;
		}
		
		public static bool isEscudoGrande(BaseShield escudo) {
			
			if(escudo is HeaterShield || 
			  escudo is MetalKiteShield || 
			  escudo is WoodenKiteShield) {
				
				return true;
			}			
			
			return false;
		}		
		#endregion
	}
}
