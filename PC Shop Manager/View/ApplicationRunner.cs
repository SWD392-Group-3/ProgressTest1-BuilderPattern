using Application.DTOs;
using Application.Interfaces;

namespace View
{
    public class ApplicationRunner
    {
        private readonly IComputerService _computerService;

        public ApplicationRunner(IComputerService computerService)
        {
            _computerService = computerService;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== PC BUILDER SHOP DEMO ===");
                Console.WriteLine("1. Buy Office PC (Office Preset)");
                Console.WriteLine("2. Buy Gaming PC (Gaming Preset)");
                Console.WriteLine("3. Build Custom PC (Custom Builder)");
                Console.WriteLine("4. View Order List");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                var choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1":
                            await HandlePresetOrder("Office");
                            break;
                        case "2":
                            await HandlePresetOrder("Gaming");
                            break;
                        case "3":
                            await HandleCustomOrder();
                            break;
                        case "4":
                            await HandleViewOrders();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid option!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private async Task HandlePresetOrder(string type)
        {
            Console.Write("Customer name: ");
            string name = Console.ReadLine();

            var request = new CreateOrderRequest
            {
                CustomerName = name,
                OrderType = type
            };

            var result = await _computerService.CreateOrderAsync(request);

            Console.WriteLine($"✅ {type} order created. Price: {result.EstimatedPrice}");
        }

        private async Task HandleCustomOrder()
        {
            Console.Write("Customer name: ");
            string name = Console.ReadLine();

            // --- CPU ---
            Console.WriteLine("\n--- SELECT CPU ---");
            Console.WriteLine("1. Intel Core i9-13900K (High-End)");
            Console.WriteLine("2. Intel Core i7-12700K (Mid-Range)");
            Console.WriteLine("3. Intel Core i5-12400 (Budget)");
            Console.Write("Select option (1-3): ");
            string cpu = Console.ReadLine() switch
            {
                "1" => "Intel Core i9-13900K",
                "2" => "Intel Core i7-12700K",
                "3" => "Intel Core i5-12400",
                var custom => custom
            };

            // --- GPU ---
            Console.WriteLine("\n--- SELECT GPU ---");
            Console.WriteLine("1. NVIDIA RTX 4090 (Ultra)");
            Console.WriteLine("2. NVIDIA RTX 3060 (Balanced)");
            Console.WriteLine("3. NVIDIA GTX 1660 (Entry)");
            Console.Write("Select option (1-3): ");
            string gpu = Console.ReadLine() switch
            {
                "1" => "NVIDIA RTX 4090",
                "2" => "NVIDIA RTX 3060",
                "3" => "NVIDIA GTX 1660",
                var custom => custom
            };

            // --- RAM ---
            Console.WriteLine("\n--- SELECT RAM ---");
            Console.WriteLine("1. 32GB DDR5");
            Console.WriteLine("2. 16GB DDR4");
            Console.WriteLine("3. 8GB DDR4");
            Console.Write("Select option (1-3): ");
            string ram = Console.ReadLine() switch
            {
                "1" => "32GB DDR5",
                "2" => "16GB DDR4",
                "3" => "8GB DDR4",
                var custom => custom
            };

            // --- Storage ---
            Console.WriteLine("\n--- SELECT STORAGE ---");
            Console.WriteLine("1. 1TB NVMe SSD");
            Console.WriteLine("2. 512GB SSD");
            Console.WriteLine("3. 1TB HDD");
            Console.Write("Select option (1-3): ");
            string storage = Console.ReadLine() switch
            {
                "1" => "1TB NVMe SSD",
                "2" => "512GB SSD",
                "3" => "1TB HDD",
                var custom => custom
            };

            // --- PSU ---
            Console.WriteLine("\n--- SELECT PSU ---");
            Console.WriteLine("1. Corsair 1000W");
            Console.WriteLine("2. Cooler Master 750W");
            Console.WriteLine("3. Generic 500W");
            Console.Write("Select option (1-3): ");
            string psu = Console.ReadLine() switch
            {
                "1" => "Corsair 1000W",
                "2" => "Cooler Master 750W",
                "3" => "Generic 500W",
                var custom => custom
            };

            Console.Write("Liquid Cooling (y/n): ");
            bool isLiquidCooling = Console.ReadLine().ToLower() == "y";
            Console.Write("RGB Lighting (y/n): ");
            bool isRGBLighting = Console.ReadLine().ToLower() == "y";

            Console.WriteLine($"\n--- Order Summary ---");
            Console.WriteLine($"  CPU     : {cpu}");
            Console.WriteLine($"  GPU     : {gpu}");
            Console.WriteLine($"  RAM     : {ram}");
            Console.WriteLine($"  Storage : {storage}");
            Console.WriteLine($"  PSU     : {psu}");
            Console.WriteLine($"  Liquid Cooling: {(isLiquidCooling ? "Yes" : "No")}");
            Console.WriteLine($"  RGB Lighting  : {(isRGBLighting ? "Yes" : "No")}");

            var request = new CreateOrderRequest
            {
                CustomerName = name,
                OrderType = "Custom",
                CustomCPU = cpu,
                CustomGPU = gpu,
                CustomRAM = ram,
                CustomStorage = storage,
                CustomPSU = psu,
                IsLiquidCooling = isLiquidCooling,
                IsRGBLighting = isRGBLighting
            };

            var result = await _computerService.CreateOrderAsync(request);
            Console.WriteLine($"\n✅ Custom order created. Price: {result.EstimatedPrice}");
        }

        private async Task HandleViewOrders()
        {
            var list = await _computerService.GetAllComputersAsync();

            if (list == null || list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠️  There are currently no orders in the system.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"\n📋 ORDER LIST ({list.Count} orders)");
            Console.WriteLine(new string('=', 60));

            foreach (var item in list)
            {
                string priceFormat = item.EstimatedPrice.ToString("N0");
                string rgbStatus = item.HasRGB ? "Yes" : "No";
                string waterCoolingStatus = item.HasLiquidCooling ? "Yes" : "No";

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"🔸 ORDER ID: {item.Id} | CUSTOMER: {item.OrderName.ToUpper()}");
                Console.ResetColor();

                Console.WriteLine($"   ├──  CPU           : {item.CPU}");
                Console.WriteLine($"   ├──  RAM           : {item.RAM}");
                Console.WriteLine($"   ├──  GPU           : {item.GPU}");
                Console.WriteLine($"   ├──  Storage       : {item.Storage}");
                Console.WriteLine($"   ├──  RGB           : {rgbStatus}");
                Console.WriteLine($"   ├──  Liquid Cooling: {waterCoolingStatus}");
                Console.WriteLine($"   └──  TOTAL PRICE   : {priceFormat} VND");

                Console.WriteLine(new string('-', 60));
            }
        }
    }
}
