using System;

public class Product
{
    private string name;
    private double amount;
    private double price;
    
	public Product()
	{
	}

    public Product(string name, double amount, double price)
    {
        this.name = name;
        this.amount = amount;
        this.price = price;
    }

    public string Name
    {
        get { return this.name; }

        set
        {
            this.name = value;
        }
    }

    public double Amount
    {
        get { return this.amount; }

        set
        {
            this.amount = value;
        }
    }

    public double Price
    {
        get { return this.price; }

        set
        {
            this.price = price;
        }
    }


}
