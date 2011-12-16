using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;
using Kaltar.Util;

namespace Kaltar.Talentos {

    public sealed class Flanquear : HabilidadeTalento
    {
		
		private static Flanquear instance = new Flanquear();
		public static Flanquear Instance {
			get {return instance;}
		}		
		
		private Flanquear() {
			id = (int)IdHabilidadeTalento.flanquear;
			nome = "Flanquear";
            descricao = "Seus ataques pelos flancos são direcionados a pontos vitais, convertendo um simples ataque em um ataque mortal. <br/> Ataques feitos pelos costas recebem bônus no dano. (Destreza/15 Destreza/10).";
            preRequisito = "Classe: Ladino, Talento: Emboscada";
            nivelMaximo = 2;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            //Tiago, colocar o requisito do talento emboscada
            if (jogador.getSistemaClasse().getClasse() is Gatuno)
            {
                return true;
            }

            return false;
		}

        public override int danoBonus(HabilidadeNode node, Jogador atacante, Mobile defensor)
        {
            int bonus = 0;

            Point3D locA = atacante.Location;
            Point3D locD = defensor.Location;

            Console.WriteLine("flanquear 1");

            //esta no alcance de 1 tile
            if (Utility.InRange(locA, locD, 1))
            {
                Console.WriteLine("flanquear 2");

                Direction dirD = defensor.Direction;

                int x = locD.X;
                int y = locD.Y;

                //ajusta o valor de X
                if (Direction.Mask.Equals(dirD) || Direction.West.Equals(dirD) || Direction.Left.Equals(dirD))
                {
                    x++;
                }
                else if (Direction.Down.Equals(dirD) || Direction.East.Equals(dirD) || Direction.Right.Equals(dirD))
                {
                    x--;
                }

                //ajusta o valor de Y
                if (Direction.Mask.Equals(dirD) || Direction.North.Equals(dirD) || Direction.Right.Equals(dirD))
                {
                    y++;
                }
                else if (Direction.Left.Equals(dirD) || Direction.South.Equals(dirD) || Direction.Down.Equals(dirD))
                {
                    y--;
                }

                //se o atacante estiver nas costas do adversário
                if (locA.X == x && locA.Y == y)
                {
                    int divisor = node.Nivel == 1 ? 15 : 10;
                    bonus = atacante.Dex / divisor;

                    atacante.SendMessage("Bonus dano flanqueado: {0}", bonus);

                    //mensagem para atacante e defensor
                    atacante.PublicOverheadMessage(Server.Network.MessageType.Regular, 0, false, "Flanqueando!");
                    defensor.PrivateOverheadMessage(Server.Network.MessageType.Regular, 0, false, "Você foi flanqueando!", defensor.NetState);
                }
            }

            return bonus;
        }

	}
}
