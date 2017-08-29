using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AnalisadorSintaxe
{
    class FirstFollows
    {
        private Gramatica gramatica;
        private List<string> firstdiretos;
        private List<string> firstindiretos;
        private string sentenca;

       
        public FirstFollows(Gramatica gramatica, string sentenca)
        {
            this.gramatica = gramatica;//recebo tanto produtor tanto producoes a serem realizadas sem separar pela barra
            this.sentenca = sentenca;//a ser reconhecida ou nao

            //first lower case before
            //https://www.jambe.co.nz/UNI/FirstAndFollowSets.html
            //Rules for First Sets
            

            //ideia aqui é pegar palavras antes e apos a barra vertical que sao producoes

            foreach (string b in gramatica.getRegras())
            {
             

                if (b.IndexOf("|") != -1)
                {
                    //pegar antes da barra
                    int pos  = b.IndexOf("|");
                    Console.WriteLine(b.Substring(0,pos));

                    //pegar apos a barra
                    var palavras = b.Split('|');
                    var palavra = palavras[1];
                    Console.WriteLine(palavra);
                }


            }

            /*If X is a terminal then First(X) is just X!
                If there is a Production X → ε then add ε to first(X)
                If there is a Production X → Y1Y2..Yk then add first(Y1Y2..Yk) to first(X)
                First(Y1Y2..Yk) is either
                First(Y1)(if First(Y1) doesn't contain ε)
                OR(if First(Y1) does contain ε) then First (Y1Y2..Yk) is everything in First(Y1) < except for ε > as well as everything in First(Y2..Yk)
                If First(Y1) First(Y2)..First(Yk) all contain ε then add ε to First(Y1Y2..Yk) as well.*/


            //            proc First(α: string of symbols)
            //Repeat {
            //                Para todas as produções α → X1 X2 X3 … Xn do
            //                    if X1 ∈T then // caso simples onde X1 é um terminal
            // First(α) := First(α) ∪ { X1}
            // else { // caso menos simples: X1 é um não-terminal
            //                    First(α) = First(α) ∪ First{ X1} \ { ε};
            //                    for (i = 1; i <= n; i++)
            //                    {
            //                        if ε is in First(X1) and in First(X2) and in… First(Xi - 1)
            //                   First(α) := First(α) ∪ First(Xi
            //                   ) \ { ε}
            //                    }
            //                }
            //                if (α => *ε)
            //                    then First(α) := First(α) ∪ { ε}
            //                end do
            //}
            //            until no change in any First(α)











                //            proc Follow(A ∈ N)
                //Follow(S) := {$};
                //            Repeat
                // foreach p ∈ P do
                //            { // Laço sobre as produções
                // case p == X → αA // a produção termina por A
                //Follow(A) := Follow(A) ∪ Follow(X);
                // case p == X → αAβ { // a produção NÃO termina
                //                    por A Follow(A) := Follow(A) ∪ First(β)\{ ε}; if ε ∈ First(β) then Follow(A) := Follow(A) ∪ Follow(X); end
                // }
                //            }
                // }
                //        until no change in any Follow()











            //Rules for Follow Sets
            /*First put $ (the end of input marker) in Follow(S)(S is the start symbol)
            If there is a production A → aBb, (where a can be a whole string) then everything in FIRST(b) except for ε is placed in FOLLOW(B).
            If there is a production A → aB, then everything in FOLLOW(A) is in FOLLOW(B)
            If there is a production A → aBb, where FIRST(b) contains ε, then everything in FOLLOW(A) is in FOLLOW(B)*/


        }
    }
}
