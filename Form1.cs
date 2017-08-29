using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AnalisadorSintaxe
{


    public struct Tabela
    {
        public int id;
        public string gerador;      // ID do stCliente
        public string letraA;     // Tempo desde a última chegada (minutos)
        public string letraB;     // Tempo de Chegada no Relógio
        public string letraC;      // Tempo de Serviço (minutos)
        public string letraDollar;    // Tempo de Início do Serviço no Relógio
    }

    public struct TabelaTopDown
    {
        public string pilha;      // ID do stCliente
        public string entrada;     // Tempo desde a última chegada (minutos)
        public string acao;     // Tempo de Chegada no Relógio
    }



    public partial class Form1 : Form
    {
        //public int iTempoSim;
        //public int iContIDs;
        //public ArrayList Gerador;
        //public Random random;
        //public int iTotalTempoDosServicos;
        //public int iTotalTempoNaFila;
        //public int iTotalTempoNoSistema;
        //public int iTotalTempoLivreDoOperador;

        //private static Gramatica gramatica;
        //private static String sentenca;
        //private static Reconhece verifica;
        //private static FirstFollows first;

        // inicializacao das variaveis
        string ConverteCharSentenca = null;
        string ConverteCharPiha = null;
        string ConverteCharAcao = null;
        //criacao dos vetores
        List<String> VetorSentenca = new List<String>();
        List<String> VetorPilha = new List<String>();
        string Sentenca = "";
        string pilha = "$S";
        string Compara = "";
        string erro = "";
        int acao = 00;
        int row = 0; //celulas
        int exec = 0;


        public Form1()
        {
            InitializeComponent();

            //s
            dataGridView1.Rows.Add();
            dataGridView1.Rows[0].Cells[0].Value = "S";
            dataGridView1.Rows[0].Cells[1].Value = "S -> aA";
            dataGridView1.Rows[0].Cells[2].Value = "";
            dataGridView1.Rows[0].Cells[3].Value = "";
            dataGridView1.Rows[0].Cells[4].Value = "";

            //a
            dataGridView1.Rows.Add();
            dataGridView1.Rows[1].Cells[0].Value = "A";
            dataGridView1.Rows[1].Cells[1].Value = "A -> aBc";
            dataGridView1.Rows[1].Cells[2].Value = "";
            dataGridView1.Rows[1].Cells[3].Value = "";
            dataGridView1.Rows[1].Cells[4].Value = "A -> e";

            //b
            dataGridView1.Rows.Add();
            dataGridView1.Rows[2].Cells[0].Value = "B";
            dataGridView1.Rows[2].Cells[1].Value = "";
            dataGridView1.Rows[2].Cells[2].Value = "";
            dataGridView1.Rows[2].Cells[3].Value = " B -> cCa";
            dataGridView1.Rows[2].Cells[0].Value = "";

            //c
            dataGridView1.Rows.Add();
            dataGridView1.Rows[3].Cells[0].Value = "C";
            dataGridView1.Rows[3].Cells[1].Value = "";
            dataGridView1.Rows[3].Cells[2].Value = "C -> bB";
            dataGridView1.Rows[3].Cells[3].Value = "C -> a";
            dataGridView1.Rows[3].Cells[0].Value = "";


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //random = new Random();
            //Gerador = null;
        }

        private void buttonAnalisar_Click(object sender, EventArgs e)
        {

            textBox1.Text = "";
            VetorPilha.Clear();
            VetorSentenca.Clear();
            Sentenca = "";
            pilha = "$S";
            Compara = "";
            acao = 00;
            row = 0;
            lblResult.Text = "";
            exec = 0;
            erro = "";


            exec = 0;

        
            //sentenca ex: aabb insiro no final $    
            Sentenca = textBox1.Text + "$"; // sentenca q sera armazenada no VetorSentenca
           

            //verifica se esta vazio vetor(lista) sentenca e vetor(lista) pilha     
            if (VetorSentenca==null && VetorPilha==null)//is empty
            {
                AddVetorSentenca();//metodo aabb$
                AddVetorPilha();//metodo
            }
            ComparaVetores();

            



            //metodos contem gramatica fixa , o first fixo, e follows fixo
            //Entradaglc();
            //First();
            //Follows();
            //Tabela();

            ////valores da tabela
            //List<string> dadosS = new List<string>();
            //     dadosS.Add("S->aA");
            //List<string> dadosA = new List<string>();
            //    dadosA.Add("A->aBc");
            //    dadosA.Add("A->e");//ver 
            //List<string> dadosB = new List<string>();
            //    dadosB.Add("B->cCa");
            //List<string> dadosC = new List<string>();
            //    dadosC.Add("C->bB");
            //    dadosC.Add("C->a");

            //List<List<string>> dadosTabela = new List<List<string>>();
            //    dadosTabela.Add(dadosS);
            //    dadosTabela.Add(dadosA);
            //    dadosTabela.Add(dadosB);
            //    dadosTabela.Add(dadosC);

            // verifica = new Reconhece(sentenca, dadosTabela);
            //verifica.reconhecimento();


       }

        private void ComparaVetores()
        {
            for (int j = 0; j < VetorSentenca.Count; j++)
            {
                for (int k = 0; k < VetorPilha.Count; k++)
                {
                    if (VetorPilha[VetorPilha.Count - 1].Equals(VetorSentenca[0]))
                    { //se S igual a a"
                      // se ultimo do vetor pilha = ao primeiro vetor sentença
                  

                        //ler e desempilhar quando forem iguais
                                        
                        //ver se replace
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[2].Value = "Le e desempilha "+Sentenca[0];

                        
                        var x = VetorPilha.Count() - 1;
                        VetorSentenca.Remove(VetorSentenca[0]);
                        VetorPilha.Remove(VetorPilha[x]);
                         Console.WriteLine("Pilha: " + VetorPilha);
                         Console.WriteLine("Sentenca: " + VetorSentenca);
                                       

                        row++;

                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[2].Value = Compara;
                        dataGridView2.Rows[row].Cells[2].Value = "Le e desempilha " + Sentenca[0];

                     
                        novoVetorPilha();

                        j = 0;
                        k = 0;
                        break;

                    }
                    else if (VetorPilha[VetorPilha.Count() - 1].Equals("S") && VetorSentenca[0].Equals("a"))
                    {// se Sa
                    
                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[2].Value = "S->cAa ";
                       
                        Console.WriteLine("Sa");
                        acao = 01;

                        var x = VetorPilha.Count() - 1;
                        VetorPilha.Remove(VetorPilha[x]);//remove o a da pilha
                                              
                        valoresTabela();
                      
                        row++;
                        j = 0;
                        k = 0;
                        break;

                    }
                    else if (VetorPilha[VetorPilha.Count() - 1].Equals("S") && VetorSentenca[0].Equals("b"))
                    {//Sb
                     
                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[2].Value = "A->cB";


                        Console.WriteLine("Sb");
                        acao = 02;
                        var x = VetorPilha.Count() - 1;
                        VetorPilha.Remove(VetorPilha[x]);//remove o a da pilha
                        valoresTabela();
                      
                        row++;
                        j = 0;
                        k = 0;
                        break;

                    }
                    else if (VetorPilha[VetorPilha.Count() - 1].Equals("S") && VetorSentenca[0].Equals("c"))
                    { //Sc
                      
                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[2].Value = "A->B";
                        
                        Console.WriteLine("Sc");
                        acao = 03;
                        var x = VetorPilha.Count() - 1;
                        VetorPilha.Remove(VetorPilha[x]);//remove o a da pilha
                        valoresTabela();
                      
                        row++;
                        j = 0;
                        k = 0;
                        break;

                    }
                    else if (VetorPilha[VetorPilha.Count - 1].Equals("A") && VetorSentenca[0].Equals("a"))
                    { //Aa
                      
                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[2].Value = "B-> E";

                        acao = 04;
                        var x = VetorPilha.Count() - 1;
                        VetorPilha.Remove(VetorPilha[x]);//remove o a da pilha
                        valoresTabela();
                      
                        row++;
                        j = 0;
                        k = 0;
                        break;

                    }
                    else if (VetorPilha[VetorPilha.Count - 1].Equals("A") && VetorSentenca[0].Equals("b"))
                    {//Ab
                     
                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[2].Value = "B->bcB";

                        Console.WriteLine("bcB");
                        acao = 05;
                        var x = VetorPilha.Count() - 1;
                        VetorPilha.Remove(VetorPilha[x]);//remove o a da pilha
                        valoresTabela();
                                             
                        row++;
                        j = 0;
                        k = 0;
                        break;

                    }
                    else if (VetorPilha[VetorPilha.Count - 1].Equals("A") && VetorSentenca[0].Equals("c"))
                    {//Ab
                     
                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[2].Value = "B->bcB";

                        Console.WriteLine("bcB");
                        acao = 15;
                        var x = VetorPilha.Count() - 1;
                        VetorPilha.Remove(VetorPilha[x]);//remove o a da pilha
                       valoresTabela();

                        row++;
                        j = 0;
                        k = 0;
                        break;

                    }


                    //Ba, Bb, Bc, B$
                    else if ((VetorPilha[VetorPilha.Count - 1].Equals("B") && VetorSentenca[0].Equals("a")) ||
                             (VetorPilha[VetorPilha.Count - 1].Equals("B") && VetorSentenca[0].Equals("b")) ||
                             (VetorPilha[VetorPilha.Count - 1].Equals("B") && VetorSentenca[0].Equals("c")) ||
                             (VetorPilha[VetorPilha.Count - 1].Equals("B") && VetorSentenca[0].Equals("$")))
                    {
                       
                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[2].Value = "B->bcB";

                        acao = 06;
                        var x = VetorPilha.Count() - 1;
                        VetorPilha.Remove(VetorPilha[x]);
                        //remove o a da pilha
                           
                        dataGridView2.Rows[row].Cells[2].Value = "B->E";
                                        
                        row++;
                        valoresTabela();
                        j = 0;
                        k = 0;
                        break;

                    }
                    //Bd, 
                    else if (VetorPilha[VetorPilha.Count - 1].Equals("B") && VetorSentenca[0].Equals("d"))
                    {
                        
                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[2].Value = "B->bcB";


                        Console.WriteLine("B$");
                        acao = 07;
                        var x = VetorPilha.Count() - 1;
                        VetorPilha.Remove(VetorPilha[x]);//remove o a da pilha
                        valoresTabela();
                    
                        row++;
                        j = 0;
                        k = 0;
                        break;

                    }

                    //Ca, C$
                    else if ((VetorPilha[VetorPilha.Count - 1].Equals("C") && VetorSentenca[0].Equals("a")) ||
               (VetorPilha[VetorPilha.Count - 1].Equals("C") && VetorSentenca[0].Equals("$")))
                    {
                       
                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[2].Value = "B->bcB";

                        Console.WriteLine("Cb");
                        acao = 10;
                        var x = VetorPilha.Count() - 1;
                        VetorPilha.Remove(VetorPilha[x]);
                        dataGridView2.Rows[row].Cells[2].Value = "C->E";
                      
                        row++;
                        valoresTabela();
                        j = 0;
                        k = 0;
                        break;

                    }

                    //Cb
                    else if (VetorPilha[VetorPilha.Count - 1].Equals("C") && VetorSentenca[0].Equals("b"))
                    {
                       
                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[2].Value = "B->bcB";
                        
                        Console.WriteLine("Cb");
                        acao = 11;
                        var x = VetorPilha.Count() - 1;
                        VetorPilha.Remove(VetorPilha[x]);//remove o a da pilha
                        valoresTabela();
                        row++;
                        j = 0;
                        k = 0;
                        break;

                    }

                    //Cc
                    else if (VetorPilha[VetorPilha.Count - 1].Equals("C") && VetorSentenca[0].Equals("c"))
                    {
                       
                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[2].Value = "B->bcB";

                        Console.WriteLine("Dc");
                        acao = 12;
                        var x = VetorPilha.Count() - 1;
                        VetorPilha.Remove(VetorPilha[x]);//remove o a da pilha
                        valoresTabela();
                     
                        row++;
                        j = 0;
                        k = 0;
                        break;

                    }
                    //Dc
                    else if (VetorPilha[VetorPilha.Count - 1].Equals("D") && VetorSentenca[0].Equals("c"))
                    {
                       
                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[2].Value = "B->bcB";
                        
                        Console.WriteLine("Dd");
                        acao = 13;
                        var x = VetorPilha.Count() - 1;
                        VetorPilha.Remove(VetorPilha[x]);//remove o a da pilha
                        valoresTabela();
                                              
                        row++;
                        j = 0;
                        k = 0;
                        break;

                    }

                    //Dd
                    else if (VetorPilha[VetorPilha.Count - 1].Equals("D") && VetorSentenca[0].Equals("d"))
                    {
                     
                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[2].Value = "B->bcB";
                        
                        Console.WriteLine("Dd");
                        acao = 14;
                        var x = VetorPilha.Count() - 1;
                        VetorPilha.Remove(VetorPilha[x]);//remove o a da pilha
                        valoresTabela();

                        row++;
                        j = 0;
                        k = 0;
                        break;

                    }
                    //se nao cai em nenhuma das regras cai aqui
                    else
                    {                       
                        dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                        dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                  
                        row++;
                                          
                        dataGridView2.Rows[row-1].Cells[2].Value = "Erro em "+row+" iteracoes";
                        //campoAlfabeto.setBackground(Color.RED);
                        lblResult.Text = "Erro em " + row + " iteracoes";
                        j = VetorSentenca.Count();
                        k = VetorPilha.Count();
                        break;
                    }
                }


                if (exec == 1)
                {
                    break;
                }
                else
                {
                    exec = 0;
                }
            }

           
        }

        public void valoresTabela()
        {
            dataGridView2.Rows.Add();
            Compara = "";
           
            switch (acao)
            { //Exemplo PG 36 - Exericio 2

                case 01: //S e a = "S->aBc"	
                    Compara = "aAb";
                    Console.WriteLine("Celula/Linha: " + row + " Compara: " + Compara);
                    //tabelaExecucao.setValueAt("S->" + Compara, row, 2); //add na tabela
                    dataGridView2.Rows[row].Cells[2].Value = "S->"+Compara;
                    novoVetorPilha();
                    break;

                case 02: //S e b = "S->bBa"	
                    Compara = "";
                    Console.WriteLine("Celula/Linha: " + row + " Compara: " + Compara);
                    //tabelaExecucao.setValueAt("S->" + Compara, row, 2); //add na tabela
                    dataGridView2.Rows[row].Cells[2].Value = "S->" + Compara;
                    novoVetorPilha();
                    break;

                case 03: // S e c = "S->cC"
                    Compara = "";
                    Console.WriteLine("Celula/Linha: " + row + " Compara: " + Compara);
                    //tabelaExecucao.setValueAt("S->" + Compara, row, 2); // add na tabela
                    dataGridView2.Rows[row].Cells[2].Value = "S->" + Compara;

                    novoVetorPilha();
                    // Collections.reverse(VetorAcao); //inverte elementos VetorAcao
                    break;

                case 04: // A e a = "A->aBA"
                    Compara = "aBb";
                    Console.WriteLine("Celula/Linha: " + row + " Compara: " + Compara);
                    dataGridView2.Rows[row].Cells[2].Value = "A->" + Compara;
                    //tabelaExecucao.setValueAt("A->" + Compara, row, 2); // add na tabela
                                                                        // Collections.reverse(VetorAcao); //inverte elementos VetorAcao
                    novoVetorPilha();
                    break;

                case 05: // A e b = "A->bS"
                    Compara = "";
                    Console.WriteLine("Celula/Linha: " + row + " Compara A->b: " + Compara);
                   // tabelaExecucao.setValueAt("A->" + Compara, row, 2); // add na tabela
                    dataGridView2.Rows[row].Cells[2].Value = "A->" + Compara;
                    novoVetorPilha();
                    break;

                case 06: // B e a, B e b, B e c, B e $ = "B->E" - Caso remove da Pilha - Epsilon
                    Compara = "";
                    Console.WriteLine("Celula/Linha: " + row + " Compara B->a: " + Compara);
                    //tabelaExecucao.setValueAt("B->E", row, 2);
                    dataGridView2.Rows[row].Cells[2].Value = "B->E" + Compara;
                    dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                    dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                    novoVetorPilha();
                    break;

                case 07: // B e d = "B->dDc" - Caso remove da Pilha - Epsilon
                    Compara = "aAC";
                    Console.WriteLine("Celula/Linha: " + row + " Compara B->a: " + Compara);
                   // tabelaExecucao.setValueAt("B->" + Compara, row, 2);
                    dataGridView2.Rows[row].Cells[2].Value = "B->" + Compara;
                    novoVetorPilha();
                    break;

                case 10: // C e a, C e $ = "C->E" - Caso remove da Pilha - Epsilon
                    Compara = "";
                    Console.WriteLine("Celula/Linha: " + row + " Compara: " + Compara);
                 
                    dataGridView2.Rows[row].Cells[2].Value = "C->E" + Compara;
                    dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                    dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                    novoVetorPilha();
                    break;

                case 11: // C e b = "C->bdB"
                    Compara = "";
                    Console.WriteLine("Celula/Linha: " + row + " Compara: " + Compara);
                   // tabelaExecucao.setValueAt("C->" + Compara, row, 2); // add na tabel
                    dataGridView2.Rows[row].Cells[2].Value = "C->" + Compara;
                    novoVetorPilha();
                    break;

                case 12: // C e c = "D->cSA"
                    Compara = "cS";
                    Console.WriteLine("Celula/Linha: " + row + " Compara: " + Compara);
                   // tabelaExecucao.setValueAt("D->" + Compara, row, 2); // add na tabel
                    dataGridView2.Rows[row].Cells[2].Value = "D->" + Compara;
                    novoVetorPilha();
                    break;

                /*case 13: // D e c = "D->cBA"
                    Compara = "";
                    System.out.println("Celula/Linha: " + row + " Compara: " + Compara);
                    tabelaExecucao.setValueAt("D->" + Compara, row, 2); // add na tabel
                    novoVetorPilha();
                    break;*/

                /*case 14: // D e d = "D->dC"
                    Compara = "dC";
                    System.out.println("Celula/Linha: " + row + " Compara: " + Compara);
                    tabelaExecucao.setValueAt("D->" + Compara, row, 2); // add na tabel
                    novoVetorPilha();
                    break;*/
                case 15: // A e  c= "A->aBA"
                    Compara = "cS";
                    Console.WriteLine("Celula/Linha: " + row + " Compara: " + Compara);
                    //tabelaExecucao.setValueAt("A->" + Compara, row, 2); // add na tabela
                    dataGridView2.Rows[row].Cells[2].Value = "A->" + Compara;                                                    // Collections.reverse(VetorAcao); //inverte elementos VetorAcao
                    novoVetorPilha();
                    break;

            }
        }

        private void novoVetorPilha()
        {
            //aBc
            for (int m = 0; m < Compara.Length; m++)
            { // soma os tamanhos do VetorPilha com a veriavel Compara
              //string ComparaInvertida = new StringBuilder(Compara).Reverse().ToString(); //inverte ordem de compara para add no VetorPilha

                string ComparaInvertida = Reverse(Compara).ToString();

                Console.WriteLine("Compara Invertido: " + ComparaInvertida);
                char CaracteresPilhaNovo = ComparaInvertida[m]; // separa Compara em char
                ConverteCharPiha = CaracteresPilhaNovo.ToString();
                Console.WriteLine(VetorPilha);

                //lista pilha contem nova informacao aquela inversa
                VetorPilha.Add(ConverteCharPiha); //cria novo vetor pilha com o antigo + caracteres compara
            }
            Compara = "";
            termino();
            Console.WriteLine("Novo Vetor Pilha: " + VetorPilha);
            Console.WriteLine("Celula/Linha: " + row);
        }

        private void termino()
        {
            if (((VetorPilha.Count() == 1) && (VetorSentenca.Count() == 1)))
            {// $ e $


               
                dataGridView2.Rows[row].Cells[0].Value = VetorPilha.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
                dataGridView2.Rows[row].Cells[1].Value = VetorSentenca.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace(" ", "");
           
                row++;

                dataGridView2.Rows[row - 1].Cells[2].Value = "Ok em " + row + " iterações.";
                lblResult.Text = "Ok em " + row + " iterações.";
            }
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        private void AddVetorPilha()
        {
            // add no VetorPilha
           Console.WriteLine("Pilha: " + pilha);

            for (int i = 0; i < pilha.Length; i++)
            {//$S
                char CaracteresPilha = pilha[i];//posicao de cada caractere
                ConverteCharPiha = CaracteresPilha.ToString();
                VetorPilha.Add(ConverteCharPiha);
                Console.WriteLine("Vetor Pilha: " + VetorPilha);
                //Collections.reverse(VetorPilha);
            }
        }

        private void AddVetorSentenca()
        {
           

            dataGridView2.Rows.Add();
            dataGridView2.Rows[row].Cells[0].Value = "$S";
            dataGridView2.Rows[row].Cells[1].Value = Sentenca;


            // add no VetorSentenca
            for (int i = 0; i < Sentenca.Length; i++)
            {//essa sentenca aa ainda tem conteudo faco de novo
                char CaracteresSentenca = Sentenca[i];//contando sentenca
                ConverteCharSentenca = CaracteresSentenca.ToString();
                VetorSentenca.Add(ConverteCharSentenca);
            }

           Console.Write("Vetor Sentenca: " + VetorSentenca);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
            Console.WriteLine( textBox1.Text);
        }

        public static void Entradaglc()
        {
            List<string> glc = new List<string>();  

            glc.Add("S::=aA");
            glc.Add("A::=aBc|ε");
            glc.Add("B::=cCa");
            glc.Add("C::=bB|c");

            //depois de processar a Gramatica retirando os caracteres invalidos
           // gramatica = new Gramatica(glc, sentenca);
        }

        public static void First()
        {
            List<string> first = new List<string>();
            first.Add("S::=a");
            first.Add("A::=a,epsilon");
            first.Add("B::=c");
            first.Add("C::=b,c");

        }

        public static void Follows()
        {
           
            List<string> follow = new List<string>();
            follow.Add("S::=$");
            follow.Add("A::=$");
            follow.Add("B::=c,a");
            follow.Add("C::=a");

        }


        public static void Tabela()
        {

           
           

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnPassoaPasso_Click(object sender, EventArgs e)
        {

        }




        //private void VerificarFirst()
        //{
        //    //verificar first direto e indireto e retornar 
        //    first = new FirstFollows(gramatica, sentenca);
        //}

    }


}
