using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace Discord2Polygon;

[Event("receiveFromPolygonEvent")]
public class ReceiveFromPolygonDTO : FunctionMessage
{
    [Parameter("uint256", "discordID", 1, true)]
    public ulong discordID {get; set;}

    [Parameter("uint256", "blockNumber", 2, true)]
    public ulong blockNumber {get; set;}

    [Parameter("uint256", "amount", 3, false)]
    public ulong amount {get; set;}
}