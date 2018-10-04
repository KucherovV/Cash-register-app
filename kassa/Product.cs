using System;

public class Product 
{
    private string name;
    private int price;
    private int amount;
    private int id;

	public Product()
	{
	}

    public Product(string name, int  price, int amount, int id)
    {
        this.name = name;
        this.price = price;
        this.amount = amount;
        this.id = id;
    }

    public string Name
    {
        get { return this.name; }

        set
        {
            this.name = value;
        }
    }

    public int Price
    {
        get { return this.price; }

        set
        {
            this.price = value;
        }
    }

    public int Amount
    {
        get { return this.amount; }

        set
        {
            amount = value;
        }
    }

    public int Id
    {
        get { return this.id; }

        set
        {
            this.id = value;
        }
    }
}
