﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        delegate void AccountStateHandler(object sender, AccountEventArgs e);
        class AccountEventArgs
        {
            public string Message { get; }
            public int Sum { get; }
            public AccountEventArgs(string message, int sum)
            {
                Message = message; Sum = sum;
            }
        }

        class Account
        {
            int _sum;
            public event AccountStateHandler Added;
            public event AccountStateHandler Withdrawn;
            public Account(int sum)
            {
                _sum = sum;
            }
            public void Put(int sum)
            {
                _sum += sum;
                if (Added != null) Added(this,
                    new AccountEventArgs($"На счет пришло {sum}", sum));
            }

            public void Withdraw(int sum)
            {
                if (_sum >= sum)
                {
                    _sum -= sum;

                    if (Withdrawn != null)
                        Withdrawn(this, new AccountEventArgs($"Со счета снято {sum}", sum));
                }
                else
                {
                    if (Withdrawn != null)
                        Withdrawn(this, new AccountEventArgs("На счете недостаточно средств", 0));
                }
            }
        }

        static void Main(string[] args)
        {
            Account account = new Account(200);
            // Добавляем обработчики события
            account.Added += delegate (object sender, AccountEventArgs e)
            {
                Console.WriteLine($"Сумма транзакции: {e.Sum}");
                Console.WriteLine(e.Message);
            };
            account.Put(230);

            Console.Read();
        }
    }
}
