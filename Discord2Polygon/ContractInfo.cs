namespace Discord2Polygon;

public struct ContractInfo
{
    public string address { get; }
    public string abi { get; }
    public int decimals { get; }

    public ContractInfo(string address, string abi, int decimals)
    {
        this.address = address;
        this.abi = abi;
        this.decimals = decimals;
    }
}