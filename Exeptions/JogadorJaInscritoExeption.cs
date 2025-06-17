using System;

namespace Exeptions
{
    public class JogadorJaInscritoExepitions : Exception
    {
        public JogadorJaInscritoExepitions(string ra)
            : base($"O jogador com o Ra: {ra}, ja está inscrito na partida, tente de novo. ")
        { 

        }
    }

}