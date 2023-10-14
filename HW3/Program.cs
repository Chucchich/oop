using System.Text;

namespace HW3
{
    // Lớp Bank (Ngân hàng)
    class Bank
    {
        private List<Account> accounts;

        public Bank()
        {
            accounts = new List<Account>();
        }

        public void OpenAccount(string accountNumber, string accountHolderName, string identificationNumber, decimal initialBalance, decimal interestRate)
        {
            Account account = new Account(accountNumber, accountHolderName, identificationNumber, initialBalance, interestRate);
            accounts.Add(account);
        }

        public void Deposit(string accountNumber, decimal amount, DateTime transactionDate)
        {
            Account account = FindAccount(accountNumber);
            if (account != null)
            {
                account.Deposit(amount, transactionDate);
            }
            else
            {
                Console.WriteLine("Tài khoản không tồn tại.");
            }
        }

        public void Withdraw(string accountNumber, decimal amount, DateTime transactionDate)
        {
            Account account = FindAccount(accountNumber);
            if (account != null)
            {
                account.Withdraw(amount, transactionDate);
            }
            else
            {
                Console.WriteLine("Tài khoản không tồn tại.");
            }
        }

        public void CalculateInterest()
        {
            foreach (Account account in accounts)
            {
                account.CalculateInterest();
            }
        }

        public void PrintReport()
        {
            foreach (Account account in accounts)
            {
                Console.WriteLine("Số hiệu tài khoản: " + account.AccountNumber);
                Console.WriteLine("Số tiền hiện có: " + account.Balance + " Euros");
                Console.WriteLine("Các giao dịch:");
                foreach (Transaction transaction in account.Transactions)
                {
                    Console.WriteLine("- Ngày: " + transaction.TransactionDate.ToString("dd/MM/yyyy") + ", Kiểu: " + transaction.TransactionType + ", Số tiền: " + transaction.Amount + " Euros");
                }
                Console.WriteLine();
            }
        }

        private Account FindAccount(string accountNumber)
        {
            foreach (Account account in accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }
            return null;
        }
    }

    // Lớp Account (Tài khoản)
    class Account
    {
        public string AccountNumber { get; private set; }
        public string AccountHolderName { get; private set; }
        public string IdentificationNumber { get; private set; }
        public decimal Balance { get; private set; }
        public decimal InterestRate { get; private set; }
        public List<Transaction> Transactions { get; private set; }

        public Account(string accountNumber, string accountHolderName, string identificationNumber, decimal initialBalance, decimal interestRate)
        {
            AccountNumber = accountNumber;
            AccountHolderName = accountHolderName;
            IdentificationNumber = identificationNumber;
            Balance = initialBalance;
            InterestRate = interestRate;
            Transactions = new List<Transaction>();
        }

        public void Deposit(decimal amount, DateTime transactionDate)
        {
            Balance += amount;
            Transaction transaction = new Transaction(transactionDate, TransactionType.Nhập, amount);
            Transactions.Add(transaction);
        }

        public void Withdraw(decimal amount, DateTime transactionDate)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Transaction transaction = new Transaction(transactionDate, TransactionType.Rút, amount);
                Transactions.Add(transaction);
            }
            else
            {
                Console.WriteLine("Số dư không đủ để thực hiện giao dịch.");
            }
        }

        public void CalculateInterest()
        {
            decimal interest = Balance * InterestRate;
            Balance += interest;
        }
    }

    // Lớp Transaction (Giao dịch)
    class Transaction
    {
        public DateTime TransactionDate { get; private set; }
        public TransactionType TransactionType { get; private set; }
        public decimal Amount { get; private set; }

        public Transaction(DateTime transactionDate, TransactionType transactionType, decimal amount)
        {
            TransactionDate = transactionDate;
            TransactionType = transactionType;
            Amount = amount;
        }
    }

    // Enum TransactionType (Kiểu giao dịch)
    enum TransactionType
    {
        Nhập,
        Rút
    }

    // Lớp chính (Main class)
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding=Encoding.UTF8;
            Console.OutputEncoding=Encoding.UTF8;
            Bank bank = new Bank();

            // Mở tài khoản mới
            bank.OpenAccount("001", "Alice", "901", 100, 0.05m);
            bank.OpenAccount("002", "Bob", "902", 50, 0.05m);
            bank.OpenAccount("003", "Alice", "903", 200, 0.05m);

            // Thực hiện giao dịch
            bank.Deposit("001", 50, DateTime.Now);
            bank.Withdraw("002", 20, DateTime.Now);
            bank.Deposit("003", 100, DateTime.Now);

            // Tính lãi suất
            bank.CalculateInterest();

            // In báo cáo
            bank.PrintReport();
        }
    }
}