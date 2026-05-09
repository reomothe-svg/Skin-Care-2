using System;
using System.Collections.Generic;

namespace SkinCare
{
    public class Product
    {
        private string name;
        private string description;
        private string category;
        private double price;

        public Product(string name, string description, string category, double price)
        {
            this.name = name;
            this.description = description;
            this.category = category;
            this.price = price;
        }

        public string GetName() { return name; }
        public string GetDescription() { return description; }
        public string GetCategory() { return category; }
        public double GetPrice() { return price; }

        public void SetName(string name) { this.name = name; }
        public void SetDescription(string description) { this.description = description; }
        public void SetCategory(string category) { this.category = category; }
        public void SetPrice(double price) { this.price = price; }
    }
}