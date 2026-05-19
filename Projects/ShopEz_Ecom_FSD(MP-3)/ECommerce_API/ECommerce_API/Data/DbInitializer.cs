using ECommerce_API.Models;

namespace ECommerce_API.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Admin seeding (unchanged)
            var admin = context.Users.FirstOrDefault(u => u.Email == "admin@shopez.com");
            if (admin == null)
            {
                admin = new User
                {
                    Name = "Admin",
                    Email = "admin@shopez.com",
                    Password = "admin123",
                    Role = "Admin"
                };
                context.Users.Add(admin);
                context.SaveChanges();
            }

            if (context.Products.Any()) return;

            var products = new Product[]
            {
                // Electronics (15)
                new() { Name = "Wireless Mouse", Description = "Ergonomic wireless mouse with 2.4GHz connection.", Price = 1299, ImageUrl = "/images/products/wireless-mouse.jpg", Stock = 50, Category = "Electronics" },
                new() { Name = "Mechanical Keyboard", Description = "RGB mechanical keyboard with blue switches.", Price = 3499, ImageUrl = "/images/products/mechanical-keyboard.jpg", Stock = 30, Category = "Electronics" },
                new() { Name = "Gaming Headset", Description = "7.1 surround sound gaming headset with mic.", Price = 2899, ImageUrl = "/images/products/gaming-headset.jpg", Stock = 40, Category = "Electronics" },
                new() { Name = "USB-C Hub", Description = "7-in-1 USB-C hub with HDMI, USB 3.0, and SD card reader.", Price = 2299, ImageUrl = "/images/products/usb-c-hub.jpg", Stock = 60, Category = "Electronics" },
                new() { Name = "External SSD", Description = "1TB portable SSD with USB 3.2.", Price = 8999, ImageUrl = "/images/products/external-ssd.jpg", Stock = 20, Category = "Electronics" },
                new() { Name = "Webcam", Description = "1080p HD webcam with built-in microphone.", Price = 3999, ImageUrl = "/images/products/webcam.jpg", Stock = 25, Category = "Electronics" },
                new() { Name = "Monitor Light Bar", Description = "Screenbar with auto-dimming and touch control.", Price = 4499, ImageUrl = "/images/products/monitor-light-bar.jpg", Stock = 15, Category = "Electronics" },
                new() { Name = "Laptop", Description = "Intel i5, 16GB RAM, 512GB SSD.", Price = 64999, ImageUrl = "/images/products/laptop.jpg", Stock = 10, Category = "Electronics" },
                new() { Name = "Graphics Card", Description = "RTX 4060 8GB GDDR6.", Price = 35999, ImageUrl = "/images/products/graphics-card.jpg", Stock = 8, Category = "Electronics" },
                new() { Name = "CPU Cooler", Description = "Liquid cooler with RGB fans.", Price = 8999, ImageUrl = "/images/products/cpu-cooler.jpg", Stock = 12, Category = "Electronics" },
                new() { Name = "Motherboard", Description = "B660M chipset, DDR5 support.", Price = 12999, ImageUrl = "/images/products/motherboard.jpg", Stock = 9, Category = "Electronics" },
                new() { Name = "RAM 16GB", Description = "DDR5 5200MHz desktop memory.", Price = 4999, ImageUrl = "/images/products/ram-16gb.jpg", Stock = 30, Category = "Electronics" },
                new() { Name = "NVMe SSD", Description = "1TB PCIe 4.0 SSD up to 7000MB/s.", Price = 11999, ImageUrl = "/images/products/nvme-ssd.jpg", Stock = 25, Category = "Electronics" },
                new() { Name = "Power Supply", Description = "750W 80+ Gold modular PSU.", Price = 7499, ImageUrl = "/images/products/power-supply.jpg", Stock = 18, Category = "Electronics" },
                new() { Name = "PC Case", Description = "Mid‑Tower case with tempered glass.", Price = 4999, ImageUrl = "/images/products/pc-case.jpg", Stock = 15, Category = "Electronics" },

                // Accessories (5)
                new() { Name = "Laptop Stand", Description = "Adjustable aluminum laptop stand for desk.", Price = 1799, ImageUrl = "/images/products/laptop-stand.jpg", Stock = 45, Category = "Accessories" },
                new() { Name = "Desk Mat", Description = "Large waterproof desk mat (90x40cm).", Price = 999, ImageUrl = "/images/products/desk-mat.jpg", Stock = 70, Category = "Accessories" },
                new() { Name = "Bluetooth Speaker", Description = "Waterproof portable speaker with 20W output.", Price = 3999, ImageUrl = "/images/products/bluetooth-speaker.jpg", Stock = 42, Category = "Accessories" },
                new() { Name = "Wireless Charger", Description = "15W fast charger for Qi devices.", Price = 1899, ImageUrl = "/images/products/wireless-charger.jpg", Stock = 60, Category = "Accessories" },
                new() { Name = "LED Strip Lights", Description = "5m RGBIC LED strip with app control.", Price = 1499, ImageUrl = "/images/products/led-strip-lights.jpg", Stock = 90, Category = "Accessories" },

                // Mobiles (5)
                new() { Name = "Smartphone Gimbal", Description = "3-axis stabilizer for vlogging.", Price = 7999, ImageUrl = "/images/products/smartphone-gimbal.jpg", Stock = 18, Category = "Mobiles" },
                new() { Name = "Noise Cancelling Earbuds", Description = "Bluetooth 5.2 earbuds with ANC.", Price = 5499, ImageUrl = "/images/products/noise-cancelling-earbuds.jpg", Stock = 35, Category = "Mobiles" },
                new() { Name = "Power Bank", Description = "20000mAh fast charging power bank.", Price = 2499, ImageUrl = "/images/products/power-bank.jpg", Stock = 55, Category = "Mobiles" },
                new() { Name = "Smartwatch", Description = "Fitness tracker with heart rate monitor.", Price = 12999, ImageUrl = "/images/products/smartwatch.jpg", Stock = 22, Category = "Mobiles" },
                new() { Name = "Tablet", Description = "10-inch tablet, 128GB storage.", Price = 24999, ImageUrl = "/images/products/tablet.jpg", Stock = 14, Category = "Mobiles" },

                // Smart Home (5)
                new() { Name = "Smart Plug", Description = "Wi‑Fi smart plug with energy monitoring.", Price = 999, ImageUrl = "/images/products/smart-plug.jpg", Stock = 80, Category = "Smart Home" },
                new() { Name = "Office Chair", Description = "Ergonomic mesh chair with lumbar support.", Price = 11999, ImageUrl = "/images/products/office-chair.jpg", Stock = 20, Category = "Smart Home" },
                new() { Name = "Smart Bulb", Description = "Wi‑Fi enabled RGB smart bulb, voice control.", Price = 899, ImageUrl = "/images/products/smart-bulb.jpg", Stock = 100, Category = "Smart Home" },
                new() { Name = "Security Camera", Description = "1080p indoor/outdoor security cam.", Price = 5999, ImageUrl = "/images/products/security-camera.jpg", Stock = 12, Category = "Smart Home" },
                new() { Name = "Video Doorbell", Description = "HD video doorbell with two‑way audio.", Price = 8999, ImageUrl = "/images/products/video-doorbell.jpg", Stock = 8, Category = "Smart Home" }
            };

            // Batch insert (10 at a time)
            const int batchSize = 10;
            for (int i = 0; i < products.Length; i += batchSize)
            {
                var batch = products.Skip(i).Take(batchSize);
                context.Products.AddRange(batch);
                context.SaveChanges();
            }
        }
    }
}