using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace Discord2Polygon;

[Function("sendToPolygon", "bool")]
public class SendToPolygonDTO : FunctionMessage
{
    [Parameter("uint256", "syncSumDiscordBalances", 1)]
    public int syncSumDiscordBalances { get; set; }
        
    [Parameter("address", "addressOfDiscordUser", 2)]
    public string addressOfDiscordUser { get; set; }
        
    [Parameter("uint256", "payoutAmount", 3)]
    public int payoutAmount { get; set; }
}