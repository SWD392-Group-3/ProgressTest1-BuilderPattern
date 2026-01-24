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
                Console.WriteLine("1. Mua máy văn phòng (Office Preset)");
                Console.WriteLine("2. Mua máy chơi game (Gaming Preset)");
                Console.WriteLine("3. Tự build máy (Custom Builder)");
                Console.WriteLine("4. Xem danh sách đơn hàng");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng: ");
                var choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1":
                            await XuLyMuaMayPreset("Office");
                            break;
                        case "2":
                            await XuLyMuaMayPreset("Gaming");
                            break;
                        case "3":
                            await XuLyMuaMayCustom();
                            break;
                        case "4":
                            await XuLyXemDanhSach();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Sai cú pháp!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi: {ex.Message}");
                }
                Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
                Console.ReadKey();
            }
        }

        private async Task XuLyMuaMayPreset(string type)
        {
            Console.Write("Tên khách: ");
            string name = Console.ReadLine();

            var request = new CreateOrderRequest
            {
                CustomerName = name,
                OrderType = type
            };

            var result = await _computerService.CreateOrderAsync(request);

            Console.WriteLine($"✅ Đã tạo đơn {type}. Giá: {result.EstimatedPrice}");
        }

        private async Task XuLyMuaMayCustom()
        {
            Console.Write("Tên khách: ");
            string name = Console.ReadLine();
            Console.WriteLine("\n--- CHỌN CPU ---");
            Console.WriteLine("1. Intel Core i9-13900K (High-End)");
            Console.WriteLine("2. Intel Core i7-12700K (Mid-Range)");
            Console.WriteLine("3. Intel Core i5-12400 (Budget)");
            Console.Write("Nhập tên CPU (hoặc chọn theo menu trên): ");
            string cpu = Console.ReadLine();

            Console.WriteLine("\n--- CHỌN GPU ---");
            Console.WriteLine("1. NVIDIA RTX 4090 (Ultra)");
            Console.WriteLine("2. NVIDIA RTX 3060 (Balanced)");
            Console.WriteLine("3. NVIDIA GTX 1660 (Entry)");
            Console.Write("Nhập tên GPU: ");
            string gpu = Console.ReadLine();

            Console.WriteLine("\n--- CHỌN RAM ---");
            Console.WriteLine("1. 32GB DDR5");
            Console.WriteLine("2. 16GB DDR4");
            Console.WriteLine("3. 8GB DDR4");
            Console.Write("Nhập RAM: ");
            string ram = Console.ReadLine();

            Console.WriteLine("\n--- CHỌN STORAGE ---");
            Console.WriteLine("1. 1TB NVMe SSD");
            Console.WriteLine("2. 512GB SSD");
            Console.WriteLine("3. 1TB HDD");
            Console.Write("Nhập Storage: ");
            string storage = Console.ReadLine();

            Console.WriteLine("\n--- CHỌN PSU ---");
            Console.WriteLine("1. Corsair 1000W");
            Console.WriteLine("2. Cooler Master 750W");
            Console.WriteLine("3. Generic 500W");
            Console.Write("Nhập PSU: ");
            string psu = Console.ReadLine();
            Console.Write("Liquid Cooling (y/n): ");
            bool isLiquidCooling = Console.ReadLine().ToLower() == "y";
            Console.Write("RGB Lighting (y/n): ");
            bool isRGBLighting = Console.ReadLine().ToLower() == "y";

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
            Console.WriteLine($"✅ Đã tạo đơn Custom. Giá: {result.EstimatedPrice}");
        }

        private async Task XuLyXemDanhSach()
        {
            var list = await _computerService.GetAllComputersAsync();

            if (list == null || list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠️  Hiện tại chưa có đơn hàng nào trong hệ thống.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"\n📋 DANH SÁCH ĐƠN HÀNG ({list.Count} đơn)");
            Console.WriteLine(new string('=', 60));

            foreach (var item in list)
            {
                string priceFormat = item.EstimatedPrice.ToString("N0");
                string rgbStatus = item.HasRGB ? "Có" : "Không";
                string waterCoolingStatus = item.HasLiquidCooling ? "Có" : "Không";

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"🔸 ĐƠN HÀNG ID: {item.Id} | KHÁCH: {item.OrderName.ToUpper()}");
                Console.ResetColor();

                Console.WriteLine($"   ├── 🖥️  CPU      : {item.CPU}");
                Console.WriteLine($"   ├── 🧠 RAM      : {item.RAM}");
                Console.WriteLine($"   ├── 🎮 GPU      : {item.GPU}");
                Console.WriteLine($"   ├── 💾 Storage  : {item.Storage}");
                Console.WriteLine($"   ├── 🌈 RGB      : {rgbStatus}");
                Console.WriteLine($"   ├── ❄️  Tản nước : {waterCoolingStatus}");
                Console.WriteLine($"   └── 💰 TỔNG TIỀN: {priceFormat} VNĐ");

                Console.WriteLine(new string('-', 60));
            }
        }
    }
}
