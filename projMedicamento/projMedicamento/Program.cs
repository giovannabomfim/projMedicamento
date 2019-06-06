

using System;

namespace projFila2_Medicamento
{
    class Program
    {
        static Medicamento medicamento;
        static Medicamentos medicamentos;
        static Lote lote;

        static void Main(string[] args)
        {
            medicamentos = new Medicamentos();
            int opcao = 1;
            do
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("0. Finalizar processo");
                Console.WriteLine("1. Cadastrar medicamento");
                Console.WriteLine("2. Consultar medicamento (sintético)");
                Console.WriteLine("3. Consultar medicamento (analítico)");
                Console.WriteLine("4. Comprar medicamento (cadastrar lote)");
                Console.WriteLine("5. Vender medicamento (abater do lote mais antigo)");
                Console.WriteLine("6. Listar medicamentos (informar dados sintéticos)");
                Console.WriteLine();
                Console.Write("Opção: ");
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                    if (opcao > 6) {
                        Console.WriteLine("--> Opção inválida, digite um número entre 0 e 6 <--");
                        Console.ReadKey();
                    }
                    
                }
                catch {
                    Console.WriteLine("--> Opção inválida, digite um número entre 0 e 6 <--");
                    continue;
                }
                switch (opcao)
                {
                    case 1: cadastrarMedicamento(); break;
                    case 2: consultaSintetico(); break;
                    case 3: consultaAnalitico(); break;
                    case 4: comprarMedicamento(); break;
                    case 5: venderMedicamento(); break;
                    case 6: listarMedicamentos(); break;
                }

            } while (opcao != 0);
        
        }

        #region Métodos Funcionais

        static public void cadastrarMedicamento() {
            int idMed, idLote, qtde;
            string nome, laboratorio;
            DateTime venc;
            // Cadastro dos dados do medicamento
            Console.WriteLine("Preencha os dados a seguir:" +
                "");
            Console.WriteLine();
            Console.Write("Id: ");idMed = entraInt();
            Console.Write("Nome: ");nome = Console.ReadLine();
            Console.Write("Laboratório: ");laboratorio = Console.ReadLine();

            // Cadastrando o lote ao qual o medicamento pertence
            Console.Write("Id do Lote: ");idLote = entraInt();
            Console.Write("Quantidade: ");qtde = entraInt();
            Console.Write("\nData de Vencimento: ");
            venc = entraData();
            if (venc != DateTime.MinValue)
            {
                // Adicionando na lista de medicamentos
                medicamento = new Medicamento(idMed, nome, laboratorio);
                medicamentos.adicionar(medicamento);
                medicamento.comprar(new Lote(idLote, qtde, venc));
                Console.WriteLine("Medicamento adicionado com sucesso!");
            }
            else
                Console.WriteLine("Tente novamente.");
            Console.ReadKey();
        }

        static public void consultaSintetico(){
            Console.WriteLine("Digite o ID do medicamento. ");
            medicamento = new Medicamento(entraInt());
            medicamento = medicamentos.pesquisar(medicamento);
            if (medicamento != null)
                Console.WriteLine(medicamento.toString() + "\n");
            else Console.WriteLine("Medicamento não encontrado!");
            Console.ReadKey();
        }

        static public void consultaAnalitico(){
            Console.WriteLine("Digite o ID do medicamento.");
            medicamento = new Medicamento(entraInt());
            medicamento = medicamentos.pesquisar(medicamento);
            if (medicamento != null)
            {
                Console.WriteLine(medicamento.toString()+"\n");
                foreach (Lote lote in medicamento.Lotes)
                {
                    Console.WriteLine(lote.toString());
                }
            }
            else Console.WriteLine("Medicamento não encontrado!");
            Console.ReadKey();
        }

        static public void comprarMedicamento() {
            int idLote, qtde;
            DateTime venc;
            Console.WriteLine("Digite o ID do medicamento.");
            medicamento = new Medicamento(entraInt());
            medicamento = medicamentos.pesquisar(medicamento);
            if (medicamento != null)
            {
                Console.Write("Id do Lote: "); idLote = entraInt();
                Console.Write("Quantidade: "); qtde = entraInt();
                //data de vencimento
                Console.Write("\nData de Vencimento: ");
                venc = entraData();
                if (venc != DateTime.MinValue)
                {
                    medicamento.comprar(new Lote(idLote, qtde, venc));
                    Console.WriteLine("Medicamento adicionado com sucesso!");
                }
                else
                    Console.WriteLine("Tente novamente.");
            }
            else Console.WriteLine("Medicamento não encontrado!");
            Console.ReadKey();
        }

        static public void venderMedicamento() {
            int qtde;
            Console.WriteLine("Digite o ID do medicamento.");
            medicamento = new Medicamento(entraInt());
            medicamento = medicamentos.pesquisar(medicamento);
            if (medicamento != null)
            {
                Console.Write("Quantidade: ");
                qtde = entraInt();
                if (medicamento.vender(qtde))
                {
                    Console.WriteLine("Medicamento vendido!");
                    if (medicamentos.deletar(medicamento))
                        Console.WriteLine("Medicamento zerado e deletado...");
                    else Console.WriteLine("Resta: " + medicamento.qtdeDisponivel() + " no estoque...");
                }
                else Console.WriteLine("Quantidade insuficiente...");
            }
            else Console.WriteLine("Medicamento não encontrado!");
            Console.ReadKey();
        }

        static public void listarMedicamentos() {
            Console.WriteLine("Lista de todos os medicamentos");
            if (medicamentos.ListaMedicamentos.Count != 0)
            {
                foreach (Medicamento medicamento in medicamentos.ListaMedicamentos)
                {
                    Console.WriteLine("\n " + medicamento.toString());
                }
            }
            else Console.WriteLine("Estoque vazio...");
            Console.ReadKey();

        }

        /// <summary>
        /// Esta função previne a inserção de caracteres não numéricos
        /// </summary>
        /// <returns>Se o vaor fornecido for um número ela retorna o número válido</returns>
        static public int entraInt() {
            int num=0;
            while (num == 0) {
                try { num = int.Parse(Console.ReadLine()); }
                catch { Console.Write("Digite um número válido: "); num = 0; }
            }
                    
            return num;
        }

        static public DateTime entraData()
        {
            DateTime data;
            int dia, mes, ano;
            Console.Write("\nDia: ");
            dia = entraInt();
            Console.Write("Mês: ");
            mes = entraInt();
            Console.Write("Ano: ");
            ano = entraInt();
            try
            {
                data = new DateTime(ano, mes, dia);
                if (data.Ticks - DateTime.Now.Ticks > 0)
                    return data;
                else
                {
                    Console.WriteLine("Medicamento vencido! Cadastre outro.");
                    return DateTime.MinValue;
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Data inválida!\n\n");
                Console.ReadKey();
                return entraData();
            }
        }
        #endregion



    }
}
