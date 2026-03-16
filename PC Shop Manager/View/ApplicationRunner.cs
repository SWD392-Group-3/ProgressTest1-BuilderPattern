using Application.DTOs;
using Application.Interfaces;

namespace View
{
    public class ApplicationRunner
    {
        private readonly IComputerService _computerService;
        private readonly ILaptopService _laptopService;

        public ApplicationRunner(IComputerService computerService, ILaptopService laptopService)
        {
            _computerService = computerService;
            _laptopService = laptopService;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════════╗");
                Console.WriteLine("║         PC BUILDER SHOP — GoF Demo       ║");
                Console.WriteLine("╠══════════════════════════════════════════╣");
                Console.WriteLine("║  ── DESKTOP (ComputerBuilder) ──          ║");
                Console.WriteLine("║  1. Buy Office PC  (Director preset)      ║");
                Console.WriteLine("║  2. Buy Gaming PC  (Director preset)      ║");
                Console.WriteLine("║  3. Build Custom PC                       ║");
                Console.WriteLine("║  4. View PC Orders                        ║");
                Console.WriteLine("╠══════════════════════════════════════════╣");
                Console.WriteLine("║  ── LAPTOP (LaptopBuilder) ──             ║");
                Console.WriteLine("║  5. Buy Office Laptop (Director preset)   ║");
                Console.WriteLine("║  6. Buy Gaming Laptop (Director preset)   ║");
                Console.WriteLine("║  7. Build Custom Laptop                   ║");
                Console.WriteLine("║  8. View Laptop Orders                    ║");
                Console.WriteLine("╠══════════════════════════════════════════╣");
                Console.WriteLine("║  0. Exit                                  ║");
                Console.WriteLine("╚══════════════════════════════════════════╝");
                Console.Write("Select an option: ");
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": await HandlePresetComputerOrder("Office");  break;
                        case "2": await HandlePresetComputerOrder("Gaming");  break;
                        case "3": await HandleCustomComputerOrder();           break;
                        case "4": await HandleViewComputerOrders();            break;
                        case "5": await HandlePresetLaptopOrder("Office");    break;
                        case "6": await HandlePresetLaptopOrder("Gaming");    break;
                        case "7": await HandleCustomLaptopOrder();             break;
                        case "8": await HandleViewLaptopOrders();              break;
                        case "0": return;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid option!");
                            Console.ResetColor();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n❌ Error: {ex.Message}");
                    Console.ResetColor();
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  COMPUTER handlers
        // ════════════════════════════════════════════════════════════════════

        private async Task HandlePresetComputerOrder(string type)
        {
            Console.Write("Customer name: ");
            string name = Console.ReadLine() ?? string.Empty;

            var request = new CreateOrderRequest
            {
                CustomerName = name,
                OrderType = type
            };

            var result = await _computerService.CreateOrderAsync(request);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ {type} PC order created for [{name.ToUpper()}]");
            Console.ResetColor();
            Console.WriteLine($"   CPU     : {result.CPU}");
            Console.WriteLine($"   GPU     : {(string.IsNullOrEmpty(result.GPU) ? "(integrated / none)" : result.GPU)}");
            Console.WriteLine($"   RAM     : {result.RAM}");
            Console.WriteLine($"   Storage : {result.Storage}");
            Console.WriteLine($"   RGB     : {(result.HasRGB ? "Yes" : "No")}");
            Console.WriteLine($"   Cooling : {(result.HasLiquidCooling ? "Liquid" : "Air")}");
            Console.WriteLine($"   Price   : ${result.EstimatedPrice:N2}");
            Console.WriteLine($"   Score   : {result.PerformanceScore} pts");
        }

        private async Task HandleCustomComputerOrder()
        {
            Console.Write("Customer name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("\n--- SELECT CPU ---");
            Console.WriteLine("1. Intel Core i9-13900K (High-End)");
            Console.WriteLine("2. Intel Core i7-12700K (Mid-Range)");
            Console.WriteLine("3. Intel Core i5-12400  (Budget)");
            Console.Write("Select option (1-3): ");
            string cpu = Console.ReadLine() switch
            {
                "1" => "Intel Core i9-13900K",
                "2" => "Intel Core i7-12700K",
                "3" => "Intel Core i5-12400",
                var custom => custom ?? string.Empty
            };

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
                var custom => custom ?? string.Empty
            };

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
                var custom => custom ?? string.Empty
            };

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
                var custom => custom ?? string.Empty
            };

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
                var custom => custom ?? string.Empty
            };

            Console.Write("Liquid Cooling (y/n): ");
            bool isLiquidCooling = (Console.ReadLine() ?? string.Empty).ToLower() == "y";
            Console.Write("RGB Lighting (y/n): ");
            bool isRGBLighting = (Console.ReadLine() ?? string.Empty).ToLower() == "y";

            Console.WriteLine("\n--- Order Summary ---");
            Console.WriteLine($"  CPU     : {cpu}");
            Console.WriteLine($"  GPU     : {gpu}");
            Console.WriteLine($"  RAM     : {ram}");
            Console.WriteLine($"  Storage : {storage}");
            Console.WriteLine($"  PSU     : {psu}");
            Console.WriteLine($"  Cooling : {(isLiquidCooling ? "Liquid" : "Air")}");
            Console.WriteLine($"  RGB     : {(isRGBLighting ? "Yes" : "No")}");

            var request = new CreateOrderRequest
            {
                CustomerName    = name,
                OrderType       = "Custom",
                CustomCPU       = cpu,
                CustomGPU       = gpu,
                CustomRAM       = ram,
                CustomStorage   = storage,
                CustomPSU       = psu,
                IsLiquidCooling = isLiquidCooling,
                IsRGBLighting   = isRGBLighting
            };

            var result = await _computerService.CreateOrderAsync(request);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Custom PC order created. Price: ${result.EstimatedPrice:N2} | Score: {result.PerformanceScore} pts");
            Console.ResetColor();
        }

        private async Task HandleViewComputerOrders()
        {
            var list = await _computerService.GetAllComputersAsync();

            if (list == null || list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠️  No PC orders in the system yet.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"\n📋 PC ORDER LIST ({list.Count} orders)");
            Console.WriteLine(new string('=', 65));

            foreach (var item in list)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"🖥  CUSTOMER: {item.OrderName.ToUpper()}");
                Console.ResetColor();
                Console.WriteLine($"   ├── CPU           : {item.CPU}");
                Console.WriteLine($"   ├── GPU           : {(string.IsNullOrEmpty(item.GPU) ? "(integrated / none)" : item.GPU)}");
                Console.WriteLine($"   ├── RAM           : {item.RAM}");
                Console.WriteLine($"   ├── Storage       : {item.Storage}");
                Console.WriteLine($"   ├── RGB           : {(item.HasRGB ? "Yes" : "No")}");
                Console.WriteLine($"   ├── Liquid Cooling: {(item.HasLiquidCooling ? "Yes" : "No")}");
                Console.WriteLine($"   └── TOTAL PRICE   : ${item.EstimatedPrice:N2}");
                Console.WriteLine(new string('-', 65));
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  LAPTOP handlers
        // ════════════════════════════════════════════════════════════════════

        private async Task HandlePresetLaptopOrder(string type)
        {
            Console.Write("Customer name: ");
            string name = Console.ReadLine() ?? string.Empty;

            var request = new CreateLaptopOrderRequest
            {
                CustomerName = name,
                OrderType = type
            };

            var result = await _laptopService.CreateLaptopOrderAsync(request);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ {type} Laptop order created for [{name.ToUpper()}]");
            Console.ResetColor();
            Console.WriteLine($"   CPU     : {result.CPU}");
            Console.WriteLine($"   RAM     : {result.RAM}");
            Console.WriteLine($"   Storage : {result.Storage}");
            Console.WriteLine($"   Screen  : {result.ScreenSize}");
            Console.WriteLine($"   Battery : {result.BatteryCapacity}");
            Console.WriteLine($"   Weight  : {result.Weight}");
            Console.WriteLine($"   Price   : ${result.EstimatedPrice:N2}");
            Console.WriteLine($"   Score   : {result.PerformanceScore} pts");
        }

        private async Task HandleCustomLaptopOrder()
        {
            Console.Write("Customer name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("\n--- SELECT CPU ---");
            Console.WriteLine("1. Intel Core i9-13900H (High-End)");
            Console.WriteLine("2. Intel Core i7-1260P  (Mid-Range)");
            Console.WriteLine("3. Intel Core i5-1235U  (Budget)");
            Console.Write("Select option (1-3): ");
            string cpu = Console.ReadLine() switch
            {
                "1" => "Intel Core i9-13900H",
                "2" => "Intel Core i7-1260P",
                "3" => "Intel Core i5-1235U",
                var custom => custom ?? string.Empty
            };

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
                var custom => custom ?? string.Empty
            };

            Console.WriteLine("\n--- SELECT STORAGE ---");
            Console.WriteLine("1. 1TB NVMe SSD");
            Console.WriteLine("2. 512GB NVMe SSD");
            Console.WriteLine("3. 256GB SSD");
            Console.Write("Select option (1-3): ");
            string storage = Console.ReadLine() switch
            {
                "1" => "1TB NVMe SSD",
                "2" => "512GB NVMe SSD",
                "3" => "256GB SSD",
                var custom => custom ?? string.Empty
            };

            Console.WriteLine("\n--- SELECT SCREEN SIZE ---");
            Console.WriteLine("1. 17.3 inch QHD 165Hz");
            Console.WriteLine("2. 15.6 inch FHD 144Hz");
            Console.WriteLine("3. 14.0 inch FHD");
            Console.Write("Select option (1-3): ");
            string screen = Console.ReadLine() switch
            {
                "1" => "17.3 inch QHD 165Hz",
                "2" => "15.6 inch FHD 144Hz",
                "3" => "14.0 inch FHD",
                var custom => custom ?? string.Empty
            };

            Console.WriteLine("\n--- SELECT BATTERY ---");
            Console.WriteLine("1. 8000mAh (High Capacity)");
            Console.WriteLine("2. 5000mAh (Standard)");
            Console.WriteLine("3. 3500mAh (Compact)");
            Console.Write("Select option (1-3): ");
            string battery = Console.ReadLine() switch
            {
                "1" => "8000mAh",
                "2" => "5000mAh",
                "3" => "3500mAh",
                var custom => custom ?? string.Empty
            };

            Console.WriteLine("\n--- SELECT WEIGHT ---");
            Console.WriteLine("1. 2.5kg (Gaming)");
            Console.WriteLine("2. 1.8kg (Standard)");
            Console.WriteLine("3. 1.3kg (Ultrabook)");
            Console.Write("Select option (1-3): ");
            string weight = Console.ReadLine() switch
            {
                "1" => "2.5kg",
                "2" => "1.8kg",
                "3" => "1.3kg",
                var custom => custom ?? string.Empty
            };

            Console.WriteLine("\n--- Order Summary ---");
            Console.WriteLine($"  CPU     : {cpu}");
            Console.WriteLine($"  RAM     : {ram}");
            Console.WriteLine($"  Storage : {storage}");
            Console.WriteLine($"  Screen  : {screen}");
            Console.WriteLine($"  Battery : {battery}");
            Console.WriteLine($"  Weight  : {weight}");

            var request = new CreateLaptopOrderRequest
            {
                CustomerName     = name,
                OrderType        = "Custom",
                CustomCPU        = cpu,
                CustomRAM        = ram,
                CustomStorage    = storage,
                CustomScreenSize = screen,
                CustomBattery    = battery,
                CustomWeight     = weight
            };

            var result = await _laptopService.CreateLaptopOrderAsync(request);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Custom Laptop order created. Price: ${result.EstimatedPrice:N2} | Score: {result.PerformanceScore} pts");
            Console.ResetColor();
        }

        private async Task HandleViewLaptopOrders()
        {
            var list = await _laptopService.GetAllLaptopsAsync();

            if (list == null || list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠️  No Laptop orders in the system yet.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"\n📋 LAPTOP ORDER LIST ({list.Count} orders)");
            Console.WriteLine(new string('=', 65));

            foreach (var item in list)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"💻 CUSTOMER: {item.OrderName.ToUpper()}");
                Console.ResetColor();
                Console.WriteLine($"   ├── CPU     : {item.CPU}");
                Console.WriteLine($"   ├── RAM     : {item.RAM}");
                Console.WriteLine($"   ├── Storage : {item.Storage}");
                Console.WriteLine($"   ├── Screen  : {item.ScreenSize}");
                Console.WriteLine($"   ├── Battery : {item.BatteryCapacity}");
                Console.WriteLine($"   ├── Weight  : {item.Weight}");
                Console.WriteLine($"   ├── Score   : {item.PerformanceScore} pts");
                Console.WriteLine($"   └── PRICE   : ${item.EstimatedPrice:N2}");
                Console.WriteLine(new string('-', 65));
            }
        }
    }
}
