using System;

namespace bankProject
{
    class Program
    {
        interface IClient
        {
            string getName();
            void setName(string name);
            string getType();
        }

        interface IAccount
        {
            double getMoney();
            void setMoney(double money);
            void getInfo(); 
        }

        class Cart : IAccount, IClient
        {
            protected string name;
            protected string type;
            protected double period;
            protected double money;
            protected double rate;

            public Cart(string name, double money, string type)
            {
                this.name = name;
                this.money = money;
                this.type = type;
            }

            public string getName()
            {
                return name;
            }

            public void setName(string name)
            {
                this.name = name;
            }

            public double getMoney()
            {
                return money;
            }

            public void setMoney(double money)
            {
                this.money = money;
            }

            public virtual void setRate(double money, double period)
            {
                if ( money > 0 && period > 1)
                        rate = 5.00;
            }

            public double getRate()
            {
                return rate;
            }
            public string getType()
            {
                return type;
            }
  
            public double getPeriod()
            {
                return period;
            }

            public void setPeriod(double period)
            {
                this.period = period;
            }
            public virtual double GetSum(double money, double rate, double period)
            {
                return money;
            }
            public virtual void getInfo()
            {
                Console.WriteLine($"Client name is {name}. Your type is {type}. You have a {money} USD");
            }
        }

        class Сumulative : Cart
        {
            public Сumulative(string name, double money, string type) : base(name, money, type)
            {
            }

            public void Put(double sum)
            {
                money += sum;
            }
            public void Withdraw(double sum)
            {
                if (money >= sum) money -= sum;
            }
            public override void getInfo()
            {
                Console.WriteLine($"Customer name {name}. The type of card is {type}. Accumulation amount- {money}");
            }
        }

        class Deposit : Cart
        {   
            public Deposit(string name, double money, string type, double period) : base(name, money, type)
            {
                this.period = period;
            }

            public override void setRate(double money, double period)
            {
                if (period > 3.00 && money > 200.00)
                {
                    rate = 5.00;
                }
                else
                {
                    rate = 3.00;
                }
            }
           
            public override double GetSum(double money, double rate, double period)
            {
                double result = money;
                for (double i = 1; i <= period; i++)
                    result = result + result * rate / 100;
                return result;
            }
           
            public override void getInfo()
            {
                Console.WriteLine($"Customer name {name}. The type of card is {type}. Deposit amount- {money}.");
            }
        }

        class Credit : Cart
        {
            double sumProcent = 0;
           
            public Credit(string name, double money, string type, double period) : base(name, money, type)
            {
                this.period = period;
            }
            public override void setRate(double money, double period)
            {
                if (period < 3.00 && money < 200.00)
                {
                    rate = 0.20;
                }
                else
                {
                    rate = 0.30;
                }
            }

            public override double GetSum(double money, double rate, double period)
            {
                double rateMonth;
                sumProcent = 0;
                for (double i = 1; i <= period; i++)
                {
                   rateMonth = money * rate / period;
                   sumProcent += rateMonth;
                }
               
                return sumProcent;
            }
            public override void getInfo()
            {
                Console.WriteLine($"Customer name {name}. The type of card is {type}. Amount of credit {money}." +
                    $" Loan repayment period -{period}");
            }
        }
            static void Main(string[] args)
        {
            Console.WriteLine("Glad to welcome you to the bank");
            Console.WriteLine("Сhoose a service: \n 1.\tСumulative card;\n 2.\tDeposit card;\n 3.\tCredit card;\n 4.\tExit;\n  ");
            int answer = Convert.ToInt32(Console.ReadLine());
            switch (answer)
            {
                case 1:
                    Сumulative cumulative = new Сumulative("Client", 0, "Сumulative");
                    Console.WriteLine("Enter your name: ");
                    cumulative.setName(Console.ReadLine());
                    Console.WriteLine("Enter amount of money: ");
                    cumulative.setMoney(Convert.ToDouble(Console.ReadLine()));
                    cumulative.getInfo();
                    Console.WriteLine("Фdding, withdrawing money");
                    Console.WriteLine("Сhoose a operation: \n 1.\tAdd money;\n 2.\tWithdraw money;\n 3.\tExit;\n ");
                    int choose = Convert.ToInt32(Console.ReadLine());
                    switch (choose)
                    {
                        case 1:
                            Console.WriteLine("Enter recharge amount: ");
                            cumulative.Put(Convert.ToDouble(Console.ReadLine()));
                            cumulative.getInfo();
                            break;
                        case 2:
                            Console.WriteLine("Enter withdrawal amount: ");
                            cumulative.Withdraw(Convert.ToDouble(Console.ReadLine()));
                            cumulative.getInfo();
                            break;
                        case 3:
                            Console.WriteLine("You enter exit");
                            break;
                    }
                    break;
                case 2:
                    Deposit deposit = new Deposit("Client", 0, "Deposit", 0);
                    Console.WriteLine("Enter your name: ");
                    deposit.setName(Console.ReadLine());
                    Console.WriteLine("Enter deposit amount: ");
                    deposit.setMoney(Convert.ToDouble(Console.ReadLine()));
                    Console.WriteLine("Enter deposit period: ");
                    deposit.setPeriod(Convert.ToDouble(Console.ReadLine()));
                    deposit.getInfo();
                    double money = deposit.getMoney();
                    double period = deposit.getPeriod();
                    deposit.setRate(money, period);
                    double rate = deposit.getRate();
                    double depMoney = deposit.GetSum(money, rate, period);
                    double result = depMoney - money;
                    result = Math.Round(result, 2);
                    Console.WriteLine("You've earned  " + result);
                    break;
                case 3:
                    Credit credit = new Credit("Client", 0, "Deposit", 0);
                    Console.WriteLine("Enter your name: ");
                    credit.setName(Console.ReadLine());
                    Console.WriteLine("Enter deposit amount: ");
                    credit.setMoney(Convert.ToDouble(Console.ReadLine()));
                    Console.WriteLine("Enter deposit period: ");
                    credit.setPeriod(Convert.ToDouble(Console.ReadLine()));
                    credit.getInfo();
                    double money1 = credit.getMoney();
                    double period1 = credit.getPeriod();
                    credit.setRate(money1, period1);
                    double rate1 = credit.getRate();
                    Console.WriteLine($"This is a rate  {rate1}");
                    double procents = credit.GetSum(money1, rate1, period1);
                    procents = Math.Round(procents, 2);
                    Console.WriteLine("You overpaid  " + procents);
                    break;
                case 4:
                    Console.WriteLine("You enter exit");
                    break;
                default:
                    Console.WriteLine("Erroneous input");
                    break;
            }
        }
    }
}
