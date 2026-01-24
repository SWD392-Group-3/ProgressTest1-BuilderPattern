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
            Console.Write("CPU: ");
            string cpu = Console.ReadLine();
            Console.Write("GPU: ");
            string gpu = Console.ReadLine();
            Console.Write("RAM: ");
            string ram = Console.ReadLine();
            Console.Write("Storage: ");
            string storage = Console.ReadLine();
            Console.Write("PSU: ");
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
