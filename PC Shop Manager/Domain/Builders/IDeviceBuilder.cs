namespace Domain.Builders
{
    public interface IDeviceBuilder<T>
    {
        IDeviceBuilder<T> WithOrderName(string orderName);
        IDeviceBuilder<T> WithCPU(string cpu);
        IDeviceBuilder<T> WithRAM(string ram);
        IDeviceBuilder<T> WithStorage(string storage);
        T Build();
    }
}
