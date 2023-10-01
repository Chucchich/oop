using System;
using System.Collections.Generic;
using System.Text;

class Employee
{
    public string Name { get; set; }

    public Employee(string name)
    {
        Name = name;
    }
}

class Item
{
    public string Name { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }

    public Item(string name, double price, double discount)
    {
        Name = name;
        Price = price;
        Discount = discount;
    }
}

class BillLine
{
    public Item Item { get; set; }
    public int Quantity { get; set; }

    public BillLine(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }
}

class GroceryBill
{
    private Employee Clerk { get; set; }
    protected List<BillLine> BillLines { get; set; }

    public GroceryBill(Employee clerk)
    {
        Clerk = clerk;
        BillLines = new List<BillLine>();
    }

    public void Add(BillLine billLine)
    {
        BillLines.Add(billLine);
    }

    public double GetTotal()
    {
        double total = 0.0;
        foreach (var billLine in BillLines)
        {
            total += (billLine.Item.Price - billLine.Item.Discount) * billLine.Quantity;
        }
        return total;
    }

    public void PrintReceipt()
    {
        Console.WriteLine($"Hóa đơn cho nhân viên: {Clerk.Name}");
        foreach (var billLine in BillLines)
        {
            Console.WriteLine($"{billLine.Item.Name} x{billLine.Quantity}: ${billLine.Item.Price * billLine.Quantity}");
        }
        Console.WriteLine($"Tổng: ${GetTotal()}");
    }
}

class DiscountBill : GroceryBill
{
    private bool IsPreferredCustomer { get; set; }

    public DiscountBill(Employee clerk, bool preferred) : base(clerk)
    {
        IsPreferredCustomer = preferred;
    }

    public int GetDiscountCount()
    {
        int count = 0;
        foreach (var billLine in BillLines)
        {
            if (billLine.Item.Discount > 0)
            {
                count += billLine.Quantity;
            }
        }
        return count;
    }

    public double GetDiscountAmount()
    {
        double amount = 0.0;
        foreach (var billLine in BillLines)
        {
            amount += billLine.Item.Discount * billLine.Quantity;
        }
        return amount;
    }

    public double GetDiscountPercent()
    {
        if (IsPreferredCustomer)
        {
            double originalTotal = base.GetTotal();
            double discountedTotal = originalTotal - GetDiscountAmount();
            return (GetDiscountAmount() / originalTotal) * 100.0;
        }
        return 0.0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;
        Employee clerk = new Employee("John");
        Item item1 = new Item("Candy Bar", 1.35, 0.25);
        Item item2 = new Item("Soda", 2.0, 0.0);
        Item item3 = new Item("Chips", 1.5, 0.1);

        BillLine billLine1 = new BillLine(item1, 2);
        BillLine billLine2 = new BillLine(item2, 3);
        BillLine billLine3 = new BillLine(item3, 1);

        GroceryBill groceryBill = new GroceryBill(clerk);
        groceryBill.Add(billLine1);
        groceryBill.Add(billLine2);
        groceryBill.Add(billLine3);

        groceryBill.PrintReceipt();

        Console.WriteLine();

        DiscountBill discountBill = new DiscountBill(clerk, true);
        discountBill.Add(billLine1);
        discountBill.Add(billLine2);
        discountBill.Add(billLine3);

        discountBill.PrintReceipt();
        Console.WriteLine($"Số lượng mặt hàng được giảm giá: {discountBill.GetDiscountCount()}");
        Console.WriteLine($"Tổng số tiền được giảm giá: ${discountBill.GetDiscountAmount()}");
        Console.WriteLine($"Phần trăm giảm giá: {discountBill.GetDiscountPercent()}%");
    }
}
