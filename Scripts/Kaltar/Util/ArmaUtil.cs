/*
 * User: Tiago Augusto
 * Date: 5/5/2006
 * Time: 20:58
 */
 
using Server;
using System;
using Server.Items;
using Server.Mobiles;

using Kaltar.Armas;
using Kaltar.Talentos;

namespace Kaltar.Util
{
	/// <summary>
	/// Contem métodos uteis para lidar com armas.
	/// </summary>
	public sealed class ArmaUtil {
		private static ArmaUtil instance = new ArmaUtil();
		
		public static ArmaUtil Instance {
			get {return instance;}
		}
		
		private ArmaUtil() {
		}
	
		public static bool temTalentoParaEquipar(Jogador jogador, BaseWeapon arma) {
			SistemaTalento sistemaTalento = jogador.getSistemaTalento();
			
			//FIXME quando as armas forem criadas. elas teram as categorias corretas e
			//será mais fácil realizar o teste
			
			if(isItemComun(arma)) {
				return true;
			}
			else if(isArmaComum(arma, CategoriaArma.espada) && sistemaTalento.possuiTalento(IDTalento.armaComumEspada)) {
				return true;
			}
			else if(isArmaComum(arma, CategoriaArma.machado) && sistemaTalento.possuiTalento(IDTalento.armaComumMachado)) {
				return true;
			}
			else if(isArmaComum(arma, CategoriaArma.amasso) && sistemaTalento.possuiTalento(IDTalento.armaComumAmasso)) {
				return true;
			}
			else if(isArmaComum(arma, CategoriaArma.pontiaguda) && sistemaTalento.possuiTalento(IDTalento.armaComumPontiaguda)) {
				return true;
			}
			else if(isArmaComum(arma, CategoriaArma.distancia) && sistemaTalento.possuiTalento(IDTalento.armaComumDistancia)) {
				return true;
			}
			else if(isArmaMarcial(arma, CategoriaArma.espada) && sistemaTalento.possuiTalento(IDTalento.armaMarcialEspada)) {
				return true;
			}
			else if(isArmaMarcial(arma, CategoriaArma.machado) && sistemaTalento.possuiTalento(IDTalento.armaMarcialMachado)) {
				return true;
			}
			else if(isArmaMarcial(arma, CategoriaArma.amasso) && sistemaTalento.possuiTalento(IDTalento.armaMarcialAmasso)) {
				return true;
			}
			else if(isArmaMarcial(arma, CategoriaArma.pontiaguda) && sistemaTalento.possuiTalento(IDTalento.armaMarcialPontiaguda)) {
				return true;
			}
			else if(isArmaMarcial(arma, CategoriaArma.distancia) && sistemaTalento.possuiTalento(IDTalento.armaMarcialDistancia)) {
				return true;
			}
			
			return false;
		}
		
		public static bool isItemComun(BaseWeapon arma){
			
			if(arma is Hatchet ||
			   arma is Pickaxe || 
			   arma is ShepherdsCrook || 
			   arma is Dagger || 
			   arma is ButcherKnife ||
			   arma is Cleaver ||
			   arma is SkinningKnife) {
				
				return true;
			}
			
			return false;
		}		
		
		public static bool isArmaComum(BaseWeapon arma, CategoriaArma categoria) {
		 	
		 	if(categoria == CategoriaArma.espada) {
				if(arma is Cutlass) {						
					return true;
				}
		 	}
		 	else if(categoria == CategoriaArma.machado) {
		 		if(arma is Axe) {						
					return true;
				}
		 	}
		 	else if(categoria == CategoriaArma.amasso) {
				if(arma is Mace ||
			  		arma is Club ||
			  		arma is QuarterStaff ||
			  		arma is BlackStaff || 
			  		arma is GnarledStaff) {
					return true;					
				}
		 	}
		 	else if(categoria == CategoriaArma.pontiaguda) {
		 		if(arma is Pitchfork ||
				   arma is ShortSpear) {
					return true;
				}
		 	}
		 	else if(categoria == CategoriaArma.distancia) {
		 		if(arma is Bow ||
					arma is Crossbow) {
					return true;						
				}
		 	}

		 	return false;
		}

		public static bool isArmaMarcial(BaseWeapon arma, CategoriaArma categoria) {

		 	if(categoria == CategoriaArma.espada) {
				if(arma is Broadsword || 
			  		arma is Longsword ||
			  		arma is Katana || 
			  		arma is Scimitar ||
			  		arma is VikingSword ||
			  		arma is ThinLongsword) {
		 			
					return true;		 				
	 			}
		 	}
		 	else if(categoria == CategoriaArma.machado) {
		 		if(arma is BattleAxe || 
			  		arma is DoubleAxe || 
			  		arma is ExecutionersAxe || 
			  		arma is TwoHandedAxe || 
			  		arma is WarAxe ||
		 			arma is Bardiche ||
					arma is Halberd) {
		 			
		 			return true;
		 		}
		 	}
		 	else if(categoria == CategoriaArma.amasso) {
		 		if(arma is HammerPick ||
			  		arma is WarMace ||
			  		arma is Maul ||
			  		arma is WarHammer) {
		 			
		 			return true;
		 		}
		 	}
		 	else if(categoria == CategoriaArma.pontiaguda) {
		 		if(arma is Spear ||
					arma is Kryss) {
		 			
		 			return true;
		 		}
		 	}
		 	else if(categoria == CategoriaArma.distancia) {
		 		if(arma is CompositeBow ||
		 		   arma is HeavyCrossbow) {
		 			
		 			return true;
		 		}
		 	}
		 	
		 	return false;
		}		 
	}
}
