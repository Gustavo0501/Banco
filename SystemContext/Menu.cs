using Banco.AccountContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.SystemContext
{
    public class Menu
    {
        public Menu(User currentUser)
        {
            CurrentUser = currentUser;
        }

        public User CurrentUser { get; set; }

        private void ShowMenu()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($"Bem-vindo {CurrentUser.Name}\n");
            Console.WriteLine("[1] - Consultar Saldo");
            Console.WriteLine("[2] - Gerar Extrato");
            Console.WriteLine("[3] - Sacar");
            Console.WriteLine("[4] - Depositar");
            Console.WriteLine("[5] - Pix");
            Console.WriteLine("[6] - Configurações de Conta");
            Console.WriteLine("[0] - Voltar para o menu inicial");
        }

        public void ShowOptions()
        {

            int choice = -1;
            do
            {
                ShowMenu();
                try
                {
                    choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            CheckAccountBalance();
                            break;
                        case 2:
                            ShowStatement();
                            break;
                        case 3:
                            WithdrawCash();
                            break;
                        case 4:
                            CashDeposit();
                            break;
                        case 5:
                            Pix();
                            break;
                        case 6:
                            AccountSettings();
                            break;
                        case 0:
                            Console.WriteLine("Voltando ao início...");
                            Program.Home();
                            break;
                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }
                }

                catch (FormatException)
                {
                    Console.WriteLine("Você deve digitar apenas números!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Algo deu errado!");
                }
            
            } while (choice != 0);
            
        }

        private void CheckAccountBalance()
        {
            Console.Clear();
            Console.WriteLine($"Seu saldo: R$ {CurrentUser.CheckBalance()}");
            
            Console.WriteLine("\nVoltando ao menu...");
     
        }

        private void ShowStatement()
        {
            Console.Clear();
            CurrentUser.ShowAccountStatement();

            Console.WriteLine("Voltando ao menu...");
            
        }

        private void WithdrawCash()
        {
            Console.Clear();
            Console.WriteLine("Digite o valor que deseja sacar: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            CurrentUser.Withdraw(amount);

            Console.WriteLine("Voltando ao menu...");
            
        }

        private void CashDeposit()
        {
            Console.Clear();
            Console.WriteLine("Digite o valor que deseja depositar: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            CurrentUser.Deposit(amount);

            Console.WriteLine("\nOperação concluída");
            Console.WriteLine("Voltando ao menu...");
            
        }

        private void Pix()
        {
            Console.Clear();
            Console.WriteLine("Digite a chave Pix do destinatário: ");
            string chavePix = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(chavePix))
                throw new Exception("A chave pix não deve estar em branco!");

            CurrentUser.Pix(chavePix);

            Console.WriteLine("Voltando ao menu...");
            
        }

        private void AccountSettings()
        {
            int option;

            do
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("[1] - Visualizar dados pessoais");
                Console.WriteLine("[2] - Alterar email");
                Console.WriteLine("[3] - Alterar senha");
                Console.WriteLine("[0] - Voltar ao menu anterior");
                
                option = int.Parse(Console.ReadLine());

                switch(option)
                {
                    case 1:
                        CurrentUser.ShowPessoalInfo();
                        
                        break;
                    case 2:
                        Console.WriteLine("Digite o novo email: ");
                        string email = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(email))
                            throw new Exception("O email não pode estar em branco!");

                        CurrentUser.ChangeEmail(email);

                        break;
                    case 3:
                        Console.WriteLine("Digite sua senha atual: ");
                        string password = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(password))
                            throw new Exception("A senha não pode estar em branco!");

                        CurrentUser.ChangePassword(password);

                        break;
                    case 0:
                        Console.WriteLine("Voltando ao menu anterior...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

            } while (option != 0);

        }
    }
}
