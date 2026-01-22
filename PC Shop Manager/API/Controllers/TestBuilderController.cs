using Microsoft.AspNetCore.Mvc;
using Domain.Builders;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestBuilderController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            var results = new List<string>();

            // 1. Happy Path: Build Gaming PC with Good PSU
            try {
                var pc = new ComputerBuilder()
                    .AsGamingPreset()
                    .WithPSU("1200W") 
                    .Build();
                results.Add($"[PASS] Gaming PC Built. Score: {pc.PerformanceScore}, Price: {pc.EstimatedPrice}");
            } catch (Exception ex) { results.Add($"[FAIL] Valid Gaming PC failed: {ex.Message}"); }

            // 2. Failure Case: Weak PSU
            try {
                var pc = new ComputerBuilder()
                    .AsGamingPreset() // Requires high watts
                    .WithPSU("300W")
                    .Build();
                results.Add($"[FAIL] Weak PSU check failed (Should have thrown exception).");
            } catch (Exception ex) { results.Add($"[PASS] Weak PSU caught: {ex.Message}"); }

            // 3. Failure Case: Bottleneck (i3 + 4090)
            try {
                var pc = new ComputerBuilder()
                    .WithCPU("Intel Core i3") 
                    .WithGPU("NVIDIA RTX 4090") 
                    .WithRAM("8GB")
                    .WithStorage("SSD")
                    .WithPSU("1000W")
                    .Build();
                results.Add($"[FAIL] Bottleneck check failed.");
            } catch (Exception ex) { results.Add($"[PASS] Bottleneck caught: {ex.Message}"); }

            return Ok(string.Join("\n", results));
        }
    }
}
