using System;
using Server;
using Server.Mobiles;

using Kaltar.Talentos;

namespace Kaltar.Util
{
	/// <summary>
	/// Description of ResistenciaUtil.
	/// </summary>
	public sealed class ResistenciaUtil {
		private static ResistenciaUtil instance = new ResistenciaUtil();
		public static ResistenciaUtil Instance {
			get {
				return instance;
			}
		}
		
		private ResistenciaUtil() {
		}
		
		/**
		 * Retorna o bônus que o jogador tem para o tipo de resistencia.
		 */ 
		public static int bonusResistencia(Jogador jogador, ResistanceType type) {
			
		 	//FIXME criar os talentos de resistencia e adicionar os bonus
			
			//if(type == ResistanceType.Physical) {
			//	if(jogador.getSistemaTalento().possuiTalento(IDTalento.))
			//}
			
			return 0;
		}
		
	}
}
