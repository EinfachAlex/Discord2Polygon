using Nethereum.RPC.Eth.DTOs;

namespace Discord2Polygon;

public interface IDiscord2Polygon
{
    public Task<TransactionReceipt> sendToPolygon(SendToPolygonDTO sendToPolygonDTO);
    
    public event EventHandler<ReceiveFromPolygonDTO> receiveFromPolygon;
}